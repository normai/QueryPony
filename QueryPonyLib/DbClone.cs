#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/CloneDb.cs
// id          : 20130818°1531
// summary     : This file stores class 'CloneDb' to facilitate a database cloning.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;
using SyTh = System.Threading;

namespace QueryPonyLib
{

   /// <summary>This class facilitates database cloning.</summary>
   /// <remarks>
   /// id : 20130818°1532
   /// note : See todo 20130818°1545 'make class DbClone static'
   /// todo : Implement a mechanism to gracefully interrupt and exit a clone run. [todo 20130824°1121]
   /// todo : See todo 20130825°1211 'provide Clone class public methods'
   /// callers : So far only QueryForm.cs::cloneDatabase()
   /// </remarks>
   public class Clone
   {

      /// <summary>This field stores the max number of records copied while debugging (0 = no limit).</summary>
      /// <remarks>id : 20130823°1421</remarks>
      private int _iDebugMaxRecToFill = 0; // 11;

      /// <summary>This field stores an indent.</summary>
      /// <remarks>id : 20130823°1521</remarks>
      private string _sIndent = "        ";

      /// <summary>This field 'true/false' stores the flag which algorithm to use for DataTime translation.</summary>
      /// <remarks>id : 20130824°0923</remarks>
      private bool _bUseDatatimeAlgo = true;

      /// <summary>This field stores the flag whether to use workaround for issue 20130824°0911 'special chars in SQLite field values' or not.</summary>
      /// <remarks>id : 20130824°0921</remarks>
      private bool _bUseWorkaroundForIssue20130824o0911 = true;

      /// <summary>This field stores the flag whether to use workaround for issue 20130824°0912 'umlauts in SQLite field values' or not.</summary>
      /// <remarks>id : 20130824°0922</remarks>
      private bool _bUseWorkaroundFieldvalWithUmlauts = true;

      /// <summary>This field stores the flag whether to apply or not workaround for issue 20130826°1331 'index schema wrong'.</summary>
      /// <remarks>id : 20130828°1131</remarks>
      private bool _bUseWorkaroundTableUnderlineInternet = true;

      /// <summary>This field stores the umlaut 'oe' ('÷' = 247 = 0xf7 = 'ö') as it comes via OleDb from a Paradox table (empirical finding).</summary>
      /// <remarks>id : 20130824°0931</remarks>
      private string _sUmlautOe = "÷";


      /// <summary>This constructor creates a new CloneDb object.</summary>
      /// <remarks>
      /// id : 20130818°1533
      /// note : Watch out, who is responsible for calling Dispose() on the objects.
      ///    In error situations, we often find orphane processes left behind.
      /// note : See todo 20130823°1223 'Class DbClone parameterization'. Todo done.
      /// </remarks>
      public Clone()
      {
      }


      /// <summary>This method clones the source db to the target db.</summary>
      /// <remarks>
      /// id : 20130820°2121
      /// ref : 'msdn: backgroundworker component' (20130820°2122)
      /// ref : 'Rob Philpott: Background Thread' (20130812°1101)
      /// note : See note 20130818°1538 'ping server'
      /// </remarks>
      /// <param name="dbSrc">The source DbClient</param>
      /// <param name="csTgt">The target ConnSettings</param>
      /// <returns>Success flag</returns>
      public bool CloneDb(DbClient dbSrc, ConnSettingsLib csTgt)
      {
         string s = "";

         s = "DbClone is starting.";
         IOBusConsumer.writeHost(s);

         // do cloning in separate thread
         System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
         worker.DoWork += delegate { doClone(dbSrc, csTgt); };
         worker.RunWorkerAsync();

         s = "DbClone thread is running.";
         IOBusConsumer.writeHost(s);

         // not sure whether this is necessary, but it seems not to hurt either
         worker.Dispose();

         return true;
      }


      /// <summary>This method clones the source db to the target db.</summary>
      /// <remarks>id : 20130818°1536</remarks>
      /// <param name="dbcSource">The source DbClient</param>
      /// <param name="csTarget">The target ConnSettings</param>
      /// <returns>Success flag</returns>
      private bool doClone(DbClient dbcSource, ConnSettingsLib csTarget)
      {
         bool bRet = true;
         string s = "";

         s = "DbClone create new database '" + csTarget.DatabaseName + "'.";
         IOBusConsumer.writeHost(s);

         // create database
         DbClient dbcTarget = DbCreate.CreateDatabase(csTarget);

         // paranoia
         if (dbcTarget == null)
         {
            return false;
         }

         // complete the connection
         dbcTarget.Connect();

         // retrieve list of source database tables
         IDbBrowser dbbrowserSource = DbClientFactory.GetBrowser(dbcSource);
         string[] tables = dbbrowserSource.SchemaGetTables();

         // paranoia
         if (tables == null)
         {
            s = "Error retrieving database tables list." + IOBus.Gb.Bricks.Sp + "[20130818°1542]";
            IOBusConsumer.writeHost(s);
            dbcTarget.Dispose();
            return false;
         }


         // loop over all tables of the source database
         for (int i = 0; i < tables.Length; i++)
         {

            //+++++++++++++++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++
            // debug setting [seq 20131201°0351]
            if (IOBus.Gb.Debag.ExecuteNO)
            {
               if (i < 18) { continue; }
               if (i > 18) { break; }
               if (i > 333) { break; }
            }
            //+++++++++++++++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++

            s = "DbClone create table " + (i + 1).ToString() + " '" + tables[i] + "'.";
            IOBusConsumer.writeHost(s);

            // create target table
            if (! tableCreate(dbcTarget, tables[i], dbbrowserSource))
            {
               s = "Error cloning table failed : '" + tables[i] + "'." + IOBus.Gb.Bricks.Sp + "[#20130818.1543]";
               IOBusConsumer.writeHost(s);
               bRet = false;
               break;
            }

            // fill target table
            if (! tableFill(dbcSource, dbcTarget, tables[i]))
            {
               s = "Error fill cloned table failed : '" + tables[i] + "'." + IOBus.Gb.Bricks.Sp + "[#20130818.1544]";
               IOBusConsumer.writeHost(s);
               bRet = false;
               break;
            }
         }

         // cleanup, otherwise, orphane process is left behind
         dbcTarget.Dispose();

         // notification
         s = "DbClone finished.";
         IOBusConsumer.writeHost(s);

         return bRet;
      }


      /// <summary>This method executes a SQL statement.</summary>
      /// <remarks>
      /// id : 20130821°1511
      /// ref : See todo 20130823°1221 'dbc.Execute vs. dbc.ExecuteOnWorker()'
      /// </remarks>
      /// <param name="dbc">The DbClient on which to execute the SQL statement</param>
      /// <param name="sSql">The SQL statement to execute</param>
      /// <param name="sErr">The error string being null for 'no error'</param>
      /// <returns>The wanted result DataSet</returns>
      private System.Data.DataSet executeStatement(DbClient dbc, string sSql, out string sErr)
      {
         sErr = null;

         // (.1) prepare execution
         // note : Nice try, but this seconds, will not apply, somehow it will remain the 15 from default.
         dbc.QueryOptions.ExecutionTimeout = 33;

         // (.2) execute
         // note : About how to run a query compare method 20130604°2059 QueryForm.cs::Execute().
         // note : Remember a while issue 20130821°1131 'clone query error: operation is not implemented'.
         dbc.Execute(sSql);

         // (.3) read result
         // note : Errors seen e.g. seen 20130821°1112 "The method or operation
         //    is not implemented." coming from DoConnect() 20130821°1132
         sErr = dbc.ErrorMessage;
         TimeSpan ts = dbc.ExecDuration;
         DbClient.RunStates dcrs = dbc.RunState;
         System.Data.DataSet dataset = dbc.DataSet;

         return dataset;
      }


      /// <summary>This field stores a memory for already processed cases to avoid repetitive notifications.</summary>
      /// <remarks>id : 20130824°0941</remarks>
      private string[] _ssRenamed = { };


      /// <summary>This method cleans a field or table name from underlines and dashes.</summary>
      /// <remarks>
      /// id : 20130823°1551
      /// note : For SQL keyword-name mangling see issue 20130824°0913 and
      ///    method 20130719°0932 IOBus.Utils.SqlTokenTicks().
      /// </remarks>
      /// <param name="sName">The field or table name to be cleaned</param>
      /// <returns>The cleaned name</returns>
      private string getCleanTablename(string sName)
      {
         // remember
         string sNameOrg = sName;

         // (.) mangle
         // (.1) umlaut mangling (see issue 20130824°0912)
         // e.g. field 'Vormerkung_möglich' to 'Vormerkung_moeglich'
         if (_bUseWorkaroundFieldvalWithUmlauts)
         {
            if (sName.Contains(_sUmlautOe)) // "÷"
            {
               sName = sName.Replace(_sUmlautOe, "oe");
            }
         }

         // (.2) standard mangling
         sName = sName.Replace("-", "");
         sName = sName.Replace("_", "");

         // notification
         if (sNameOrg != sName)
         {
            if (Array.IndexOf(_ssRenamed, sNameOrg) < 0)
            {
               Array.Resize(ref _ssRenamed, _ssRenamed.Length + 1);
               _ssRenamed[_ssRenamed.Length - 1] = sNameOrg;

               // notification
               string s = _sIndent + "Rename '" + sNameOrg + "' to '" + sName + "'";
               IOBusConsumer.writeHost(s);
            }
         }

         return sName;
      }


      /// <summary>This method prepares a field value to be used in the SQL statement.</summary>
      /// <remarks>
      /// id : 20130823°1552
      /// ref : About SQLite field types see 20130821°1141 'sqlite: datatypes'
      /// </remarks>
      /// <param name="sVal">The field value to be escaped</param>
      /// <param name="sEsc">The the wrapper character, e.g. "'" or '"'</param>
      /// <returns>The wanted field escaped value</returns>
      ////private string getSqlValueFromObject(object oVal, string sQuot)
      private string getSqlValueFromObject(object oVal, Type type, string sQuot)
      {
         string s = "", sVal = "";

         // dispatch types
         ////Type type = oVal.GetType();
         if (type == typeof(System.Boolean))
         {
            sVal = "0";
            if ((System.Boolean) oVal)
            {
               sVal = "1";
            }
         }
         else if (type == typeof(System.Byte))
         {
            sVal = oVal.ToString();
         }
         else if (type == typeof(System.Byte[]))
         {
            // See issue 20130824°0914 'SQLite fields of DataType Byte[]'
            //  provisory solution e.g. for field 'ObjLang.audio'
            sVal = sQuot + sQuot; //// "''";
         }
         else if (type == typeof(System.Double))
         {
            sVal = oVal.ToString();
         }
         else if (type == typeof(System.Int16))
         {
            sVal = oVal.ToString();
         }
         else if (type == typeof(System.Int32))
         {
            sVal = oVal.ToString();
         }
         else if (type == typeof(System.DateTime))
         {

            // grip DateTime value [seq 20131201°0832]
            // note : Remember issue 20131201°0833 'Find a DateTime null value for SQLite'.
            ////DateTime dt = (DateTime) oVal; // throws InvalidCastException
            DateTime dt = DateTime.MinValue;                                           // '0001-01-01 00:00:00'
            if (IOBus.Gb.Debag.ExecuteNO)
            {
               dt = new DateTime(1970, 1, 1, 1, 0, 0, 0);                              // begin of UNIX time
            }
            if (oVal.GetType() != typeof(DBNull))
            {
               dt = Convert.ToDateTime(oVal);
            }


            // format value [seq 20130823°1611]
            if (_bUseDatatimeAlgo)
            {
               if (IOBus.Gb.Debag.ShutdownForever)
               {
                  // [seq 20130823°1612]
                  // note : This sequence produces a corrupt field entry. See
                  //    issue 20131201°0811 'SQLite String not recognized as DateTime'.
                  Double dbl = dt.ToOADate();
                  sVal = dbl.ToString();
               }
               else
               {
                  // [seq 20131201°0831]
                  // ref : See SQLite reference chapter 'Datatypes In SQLite Version 3'
                  //    at http://www.sqlite.org/datatype3.html [20131128°0522]. This tells:
                  //    ----------------------------------------
                  //    1.2 Date and Time Datatype
                  //    SQLite does not have a storage class set aside for storing dates and/or times.
                  //    Instead, the built-in Date And Time Functions of SQLite are capable of storing
                  //    dates and times as TEXT, REAL, or INTEGER values:
                  //     - TEXT as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
                  //     - REAL as Julian day numbers, the number of days since noon in Greenwich
                  //       on November 24, 4714 B.C. according to the proleptic Gregorian calendar.
                  //     - INTEGER as Unix Time, the number of seconds since 1970-01-01 00:00:00 UTC.
                  //    Applications can chose to store dates and times in any of these formats and
                  //    freely convert between formats using the built-in date and time functions.
                  //   ----------------------------------------
                  // note : Real and integer were probably the more effecive storage format, but
                  //    more conventient for us to write the code quickly, seems the Text option.
                  IOBus.Gb.Formats.SqlitDatetypeStorage option = IOBus.Gb.Formats.SqlitDatetypeStorage.AsText;
                  if (option == IOBus.Gb.Formats.SqlitDatetypeStorage.AsInteger)
                  {
                     // not yet implemented
                     sVal = sQuot + sQuot;
                  }
                  else if (option == IOBus.Gb.Formats.SqlitDatetypeStorage.AsReal)
                  {
                     // not yet implemented
                     sVal = sQuot + sQuot;
                  }
                  else if (option == IOBus.Gb.Formats.SqlitDatetypeStorage.AsText)
                  {
                     sVal = dt.Year.ToString("0000");
                     sVal += "-" + dt.Month.ToString("00");
                     sVal += "-" + dt.Day.ToString("00");
                     sVal += " " + dt.Hour.ToString("00");
                     sVal += ":" + dt.Minute.ToString("00");
                     sVal += ":" + dt.Second.ToString("00");
                     sVal += "." + dt.Millisecond.ToString("000");
                     sVal = sQuot + sVal + sQuot;
                  }
                  else
                  {
                     // program flow error
                     sVal = sQuot + sQuot;
                  }
               }
            }
            else
            {
               // ?
               sVal = sQuot + sQuot;
            }

         }
         else if (type == typeof(System.DBNull))
         {
            sVal = sQuot + sQuot;
         }
         else if (type == typeof(System.String))
         {
            sVal = oVal.ToString();

            //note : This seems not a workaround anymore, but a solid feature. [todo 20130825°1202]
            if (_bUseWorkaroundForIssue20130824o0911)
            {
               // (.1) a quote itself
               // See issue 20130824°0911 'special chars in SQLite field values'.
               if (sVal.Contains(sQuot))
               {
                  sVal = sVal.Replace(sQuot, "*");
               }

               // (.2) dedicated special case
               // note : For CRLF, there seems a 0x03/0x01 to come via OleDb from Paradox table.
               // note : Remember issue 20130824°1131 'chinese and null characters in memo field'.
               if (IOBus.Gb.Debag.ShutdownAlternatively)
               {
                  char[] chars = { Convert.ToChar(3), Convert.ToChar(1) };
                  s = new string(chars);
               }
               else
               {
                  s = char.ConvertFromUtf32(3) + char.ConvertFromUtf32(1); // empirical finding
               }
               if (sVal.Contains(s))
               {
                  sVal = sVal.Replace(s, "*"); // is not enough in the dedicated case
                  sVal = "*"; // brute force repair
                  s = _sIndent + "ATTENTION - Chinese chars in source val, val fully replaced by \"*\".";
                  IOBusConsumer.writeHost(s);
               }

               // (.3) prophylactic paranoia (generic against issue 20130824°1131)
               // todo : Possibly replace this by an algorithm to replace e.g. all chars < 33 [todo 20130825°1201]
               if ( sVal.Contains(char.ConvertFromUtf32(0))
                   || sVal.Contains(char.ConvertFromUtf32(1))
                    || sVal.Contains(char.ConvertFromUtf32(3))
                     )
               {
                  sVal = "*"; // brute force repair
                  s = _sIndent + "ATTENTION - Chinese characters ... value fully replaced by \"*\".";
                  IOBusConsumer.writeHost(s);
               }

            }
            else
            {
               string x = "\\" + sQuot;
               sVal = sVal.Replace(sQuot, x);
            }


            sVal = sQuot + sVal + sQuot;
         }
         else
         {
            s = "Type unknow : " + type.FullName;
            IOBusConsumer.writeHost(s);
         }


         // [seq 20130823°1621] don't leave the value totally blank, otherwise SQLite exception 'syntax error ...'
         if (sVal == "")
         {
            sVal = sQuot + sQuot;
         }

         return sVal;
      }


      /// <summary>This method clones one table from the source DbClient to the target DbClient.</summary>
      /// <remarks>id : 20130818°1537</remarks>
      /// <param name="dbTgt">The target DbClient</param>
      /// <param name="sTable">The name of the table to be cloned</param>
      /// <returns>Success flag</returns>
      private bool tableCreate(DbClient dbcTarget, string sTableName, IDbBrowser dbbrowserSource)
      {
         string s = "";

         // (D)
         // (D.1) prepare a table node as parameter for building the SQL statement(s)
         // This table node will carry the fields and indices lists.
         Nodes.Table tablenode = new Nodes.Table();
         tablenode.TblDatabase = null;                                                 // perhaps fill this
         tablenode.TblFields = null;                                                   // perhaps fill below
         tablenode.TblIndices = null;                                                  // perhaps fill below
         tablenode.TblName = sTableName;                                               // o.k.
         tablenode.Text = sTableName;                                                  //

         // (D.2) retrieve the field list for this table including field properties
         // note : See todo 20130826°1321 'GetSubObjectHierarchy2() is awkward'.
         if (! dbbrowserSource.GetSubObjectHierarchy2(tablenode))
         {
            s = "ERROR GetSubObjectHierarchy2() failed." + " " + "[20130818°1545]";
            IOBusConsumer.writeHost(s);
            return false;
         }

         // (D.3) retrieve indices (sequence 20130826°1311)
         Nodes.Indices[] ndxs = dbbrowserSource.SchemaGetIndices(tablenode);
         tablenode.TblIndices = ndxs;


         // (E) create table
         // (E.1) build create statement
         string sSql = tableCreate_sqlCreateTable(tablenode);

         // (E.2) paranoia
         if (dbcTarget.ErrorMessage != null)
         {
            s = "Orphane error message deleted: " + dbcTarget.ErrorMessage + " " + "[20130823°1601]";
            IOBusConsumer.writeHost(s);
            dbcTarget.ErrorMessage = null;
         }

         // (E.3) execute create statement
         // note : here also executeStatement() would work (see todo 20130823°1221)
         System.Data.DataSet dataset = dbcTarget.ExecuteOnWorker(sSql, 3333);

         // (E.4) process error
         if (dbcTarget.ErrorMessage != null)
         {
            // todo : This error leaves an orphane process behind, fix this. [todo 20130823°1531]

            s = dbcTarget.ErrorMessage.Replace("\r\n", " - "); // cosmetics
            s = "ERROR creating table '" + sTableName + "' : \"" + s + "\". [#20130821.1133]"
               + " Possibly orphane process left behind. The offending SQL statement:"
                + IOBus.Gb.Bricks.Cr + "---------------------------------------"
                 + IOBus.Gb.Bricks.Cr + sSql
                  + "---------------------------------------"
                   ;
            IOBusConsumer.writeHost(s);
            return false;
         }


         // (F) create indices (sequence 20130828°1112)
         for (int iNdx = 0; iNdx < tablenode.TblIndices.Length; iNdx++)
         {
            bool bIsPrimaryMulti = (tablenode.TblIndices.Length > 1);

            sSql = tableCreate_sqlCreateIndex(tablenode.TblIndices[iNdx], bIsPrimaryMulti);
            if (sSql != "")
            {
               System.Data.DataSet ds = dbcTarget.ExecuteOnWorker(sSql, 3333);
            }
            if (dbcTarget.ErrorMessage != null)
            {
               s = dbcTarget.ErrorMessage.Replace("\r\n", " - "); // cosmetics
               s = "ERROR creating index '" + sTableName + "' : \"" + s + "\"."
                  + " " + "[#20130828.1113]" + " The offending SQL statement:"
                   + IOBus.Gb.Bricks.Cr + "---------------------------------------"
                    + IOBus.Gb.Bricks.Cr + sSql
                     + IOBus.Gb.Bricks.Cr + "---------------------------------------"
                      ;
               IOBusConsumer.writeHost(s);
               return false;
            }
         }


         return true;
      }


      /// <summary>This method builds the SQL statement to create an index.</summary>
      /// <remarks>
      /// id : 20130828°1111
      /// ref : E.g. 20130828°1121 'Stacko: SQLite index columns'
      /// note : See issue 20130828°1151 'SQLite allows no two indices with the same name'
      /// </remarks>
      /// <param name="node">The Table Node including the indices list</param>
      /// <returns>The wanted create index statement</returns>
      private string tableCreate_sqlCreateIndex(Nodes.Indices node, bool bIsPrimaryMulti)
      {
         string sSql = "";

         // workaround for sqlite tablenames
         string sTablename = getCleanTablename(node.NdxTable.TblName);

         // tick tablename
         sTablename = IOBus.Utils.Strings.SqlTokenTicks(sTablename, " -", "``");

         // comfort
         string sIndexname = node.NdxName;
         sIndexname = getCleanTablename(sIndexname);
         string[] ssFields = node.NdxFieldnames;
         bool bIsPrimary = node.NdxIsPrimary;

         // skip primary index
         if (bIsPrimary)
         {
            // see issue 20130828°1141 'SQLite has no combined primary keys'
            if (!bIsPrimaryMulti)
            {
               return sSql;
            }
         }

         // process curious exception
         // note : See issue 20130826°1331 'index schema wrong'
         if (_bUseWorkaroundTableUnderlineInternet)
         {
            if (sTablename == "InternetpartnerMergingExcel")
            {
               return sSql;
            }
         }

         // manipulate index name (sequence 20130828°1212)
         // note : See issue 20130828°1211 'SQLite no index names with cross'.
         if (sIndexname.EndsWith("#PX"))
         {
            sIndexname = "PRIMARI";
         }

         // manipulate index name (sequence 20130828°1152)
         //  fighting issue 20130828°1151 'SQLite allows no two indices with the same name'
         sIndexname = sTablename + "_" + sIndexname;

         // open line
         sSql = "CREATE INDEX" + " " + sIndexname + " " + "ON" + " " + sTablename + " (";

         // loop over the fields of this index
         for (int iCol = 0; iCol < ssFields.Length; iCol++)
         {
            if (iCol > 0)
            {
               sSql += ", ";
            }
            sSql += getCleanTablename(ssFields[iCol]);
         }

         // close line
         sSql += ");";

         return sSql;
      }


      /// <summary>This method builds the SQL statement to create a table.</summary>
      /// <remarks>
      /// id : 20130821°1151
      /// ref : About SQLite field types see 20130821°1141 'sqlite: datatypes'
      /// note : See issue 20130828.1141 'SQLite has no combined primary keys'
      /// </remarks>
      /// <param name="node">The Table Node including the field list</param>
      /// <returns>The wanted create table statement</returns>
      private string tableCreate_sqlCreateTable(Nodes.Table node)
      {
         string s = "";

         // workaround for sqlite tablenames
         string sTablename = getCleanTablename(node.TblName);

         // tick tablename
         sTablename = IOBus.Utils.Strings.SqlTokenTicks(sTablename, " -", "``");

         // (.1) build statement header
         string sSql = "CREATE TABLE" + " " + sTablename;

         // (.2) build statement body
         // loop over the fields of the given table
         for (int i = 0; i < node.Nodes.Count; i++)
         {
            Nodes.Fields nField = (Nodes.Fields)node.Nodes[i];

            // notification
            if (IOBus.Gb.Debag.ExecuteNO)
            {
               s = "         " + i.ToString()
                  + "\t " + nField.FldName
                   + "\t " + nField.FldDataType
                    + "\t " + nField.FldLength.ToString()
                     + "\t " + nField.FldIsNullable.ToString()
                      ;
               IOBusConsumer.writeHost(s);
            }
            else
            {
               s = ".";
               if (i == node.Nodes.Count - 1)
               {
                  s = " " + (i + 1).ToString() + " cols";
               }
               IOBusConsumer.writeHostChar(s);
            }

            //-----------------------------------------
            // determine and adjust fielname and datatype (possibly to be outsourced)

            // (.1) fieldname
            string sFieldname = "";
            switch (nField.FldName)
            {
               case "": sFieldname = ""; break;
               default: sFieldname = nField.FldName; break;
            }

            // workaround 20130823°1542 against issue 20130823.1541
            sFieldname = getCleanTablename(sFieldname);

            // clean fieldname
            sFieldname = IOBus.Utils.Strings.SqlTokenTicks(sFieldname, " -", "``");

            // (.2) datatype
            string sDataType = nField.FldDataType;
            if (    (nField.FldLength > 0)                                             // do not print colum width if there is none
                &&  (nField.FldDataType != "Boolean")                                  // do not print colum width for booleans (which have '2' internally)
                 && false                                                              // SQLite never has any columns width anyway, not even WChar
                  )
            {
               // append width info to column type
               sDataType += "(" + nField.FldLength.ToString() + ")";
            }
            //-----------------------------------------

            // (.) write line
            // (.1) open line
            if (i < 1)
            {
               sSql += IOBus.Gb.Bricks.Cr + "( "; // open field list
            }
            else
            {
               sSql += IOBus.Gb.Bricks.Cr + ", ";
            }

            // (.2) write fieldname
            sSql += sFieldname;

            // (.3) write datatype
            sSql += " " + sDataType;

            // (.4) write primarykey
            if (nField.FldIsPrimary)
            {
               // see issue 20130828°1141 'SQLite has no combined primary keys'
               if (! nField.FldIsPrimaryMulticol)
               {
                  sSql += " " + "PRIMARY KEY";
               }
            }

            // process curious exception
            // note : See issue 20130826°1331 'index schema wrong'
            if (_bUseWorkaroundTableUnderlineInternet)
            {
               if ((sTablename == "InternetpartnerMergingExcel") && (sFieldname == "Fid"))
               {
                  sSql += " " + "PRIMARY KEY";
               }
            }

            // (.5) write nullable
            if (!nField.FldIsNullable)
            {
               sSql += " " + "NOT NULL";
            }
         }

         // (.3) close field list
         sSql += IOBus.Gb.Bricks.Cr + ")" + IOBus.Gb.Bricks.Cr;

         return sSql;
      }


      /// <summary>This method fills the newly created cloned table.</summary>
      /// <remarks>
      /// id : 20130823°1211 (20130821°1501)
      /// note : Now dbcSource.ExecuteOnWorker() is used in sequence 20130823°1216.
      ///    But originally method 20130821°1511 executeStatement() was attempted
      ///    to be used, only which fails by issue 20130822°2111.
      /// note : Sequences 20130823°1212/1212 which wrapped the complete method code
      ///    for GUI embedding after 20130604°1431 are removed. (20130824°1111)
      /// </remarks>
      /// <param name="dbcSource">The source DbClient</param>
      /// <param name="dbcTarget">The target DbClient</param>
      /// <param name="sTable">The name of the table to be cloned</param>
      /// <returns>Success flag</returns>
      private bool tableFill(DbClient dbcSource, DbClient dbcTarget, string sTable)
      {
         string s = "", sErr = null, sSql = "", sTableClean = "";

         // paranoia
         if (dbcSource.ErrorMessage != null)
         {
            s = "ERROR orphan message " + " : \"" + dbcSource.ErrorMessage + "\"" + " " + "[#20130823.1511]";
            IOBusConsumer.writeHost(s);
            dbcSource.ErrorMessage = null;
         }

         // (.) retrieve record count of source table (sequence 20130823°1215)
         // (.1) execute statement
         // (.1.1) build
         sTableClean = IOBus.Utils.Strings.SqlTokenTicks(sTable, " -", "``");
         sSql = "SELECT COUNT(*) FROM " + sTableClean + ";";

         // (.1.2) the calling (seq 20130823°1216)
         System.Data.DataSet dataset = dbcSource.ExecuteOnWorker(sSql, 3333);

         // (.1.3) process error
         if (dbcSource.ErrorMessage != null)
         {
            s = _sIndent + "Well known error \"" + dbcSource.ErrorMessage + "\" " + "[#20130823.1512]";
            dbcSource.ErrorMessage = null;
            IOBusConsumer.writeHost(s);
         }

         // (.2) interpret result
         int iRecCount = -1;
         if (dataset != null)
         {
            System.Data.DataTable tbl = dataset.Tables[0];
            string sCount = tbl.Rows[0].ItemArray[0].ToString();
            iRecCount = int.Parse(sCount);
         }

         s = _sIndent + "Fill in " + iRecCount.ToString() + " records ";
         IOBusConsumer.writeHost(s);

         // loop over source table to read each single record
         sErr = tableFillRecords(dbcSource, dbcTarget, sTable);
         if (sErr != null)
         {
            s = "Error filling records into cloned table : \"" + sErr + "\" [#20130823.1513]";
            IOBusConsumer.writeHost(s);
            return false;
         }

         return true;
      }


      /// <summary>This method fills the newly created cloned table with records.</summary>
      /// <remarks>
      /// id : 20130823°1231
      /// note : See issue 20130823°1411 'OleDb has no LIMIT clause'. The original
      ///    plan was, to scan the table at the caller and pass this method a record
      ///    number and then processes one record only here. But the missing LIMIT/OFFSET
      ///    forces us to scan the table here after having retrieved it completely.
      /// ref : 'thread: no LIMIT clause in OleDb' 20130823°1321
      /// note : In sequence 20130823°1331 this line is wanted to retrieve only one record:
      ///    // "sSql1 = "SELECT * FROM" + " " + sTbl + " " + "LIMIT 1 OFFSET" + " " + iLoop.ToString();
      ///    But sorrily OleDb does not know LIMIT/OFFSET (see issue 20130823°1411).
      /// </remarks>
      /// <param name="dbTarget">The target DbClient</param>
      /// <param name="iCount">The number of records in the source table</param>
      /// <returns>Success flag</returns>
      private string tableFillRecords(DbClient dbcSource, DbClient dbcTarget, string sTable)
      {
         string s = "", sMsg = "", sTbl = "", sErr = null, sSql1 = "";
         int iCountNotify = 0;

         // mangle table name
         sTbl = IOBus.Utils.Strings.SqlTokenTicks(sTable, " -", "``");

         // build command (seq 20130823°1331) (see issue 20130823°1411)
         sSql1 = "SELECT * FROM" + " " + sTbl;

         // execute command
         System.Data.DataSet dataset1 = dbcSource.ExecuteOnWorker(sSql1, 3333);

         // extract record from result DataSet (we know, it is only one table)
         if (dataset1 == null)
         {
            sErr = "ERROR : \"" + dbcSource.ErrorMessage + "\" [#20130823.1233]";
            return sErr;
         }
         System.Data.DataTable tbl = dataset1.Tables[0];

         // workaround
         string sTblCln = getCleanTablename(sTable);
         sTblCln = IOBus.Utils.Strings.SqlTokenTicks(sTblCln, " -", "``");

         // loop over the records of the source table
         for (int iRec = 0; iRec < tbl.Rows.Count; iRec++)
         {
            // stop at possible record limit
            if ((_iDebugMaxRecToFill > 0) && (iRec > _iDebugMaxRecToFill))
            {
               sMsg = _sIndent + "Max. records to copy in debug mode = " + _iDebugMaxRecToFill.ToString() + ".";
               IOBusConsumer.writeHost(sMsg);
               break;
            }

            // notification (the more records, the less but wider dots)
            if (iRec < 100)
            {
               IOBusConsumer.writeHostChar(".");
            }
            else if (iRec < 1000)
            {
               iCountNotify += 1;
               if (iCountNotify > 9)
               {
                  iCountNotify = 0;
                  IOBusConsumer.writeHostChar(" .");
               }
            }
            else
            {
               iCountNotify += 1;
               if (iCountNotify > 99)
               {
                  iCountNotify = 0;
                  IOBusConsumer.writeHostChar("  .");
               }
            }


            // (.) build command to insert one target record
            // (.1) write header
            // note : This uses the syntax flavour with colum list to avoid dependency on the field order.
            string sSql2 = "INSERT INTO" + " " + sTblCln + " " + IOBus.Gb.Bricks.Cr + "( ";

            // (.2) process the single fields
            // (.2.1) preparations
            string sColumns = "";
            string sValues = "";

            // (.2.2) loop over columns of this record to write the field names/values
            for (int iCol = 0; iCol < tbl.Rows[iRec].ItemArray.Length; iCol++)
            {
               object oVal = tbl.Rows[iRec].ItemArray[iCol];
               string sColumnName = tbl.Columns[iCol].ColumnName;
               Type datatype = tbl.Columns[iCol].DataType;


               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
               // debug issue 20130824°0911 'special chars in SQLite field values'
               if (oVal.ToString().Contains("Joe"))
               {
                  System.Threading.Thread.Sleep(1); // breakpoint
               }
               // debug issue 20130824°0914 'SQLite fields of DataType Byte[]'
               if (sColumnName == "audio")
               {
                  System.Threading.Thread.Sleep(1); // breakpoint
               }
               if (sColumnName == "Datum" || sColumnName == "Nichtmahnen") // for Datetime
               {
                  SyTh.Thread.Sleep(1); // breakpoint
               }
               var v = oVal;
               Type t = v.GetType();
               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

               string sVal = getSqlValueFromObject(oVal, datatype, "'");
               if (iCol > 0)
               {
                  sColumns += IOBus.Gb.Bricks.Cr + ", ";
                  sValues += IOBus.Gb.Bricks.Cr + ", ";
               }

               s = getCleanTablename(sColumnName);
               s = IOBus.Utils.Strings.SqlTokenTicks(s, " -", "``");
               sColumns += s;

               sValues += sVal;
            }
            // (.2.3) close fields list
            sSql2 += sColumns + IOBus.Gb.Bricks.Cr + ") VALUES" + IOBus.Gb.Bricks.Cr + "( " + sValues;

            // (.3) write footer
            sSql2 += IOBus.Gb.Bricks.Cr + ");" + IOBus.Gb.Bricks.Cr;

            // (.) send INSERT command
            System.Data.DataSet dataset2 = dbcTarget.ExecuteOnWorker(sSql2, 3333);

            // (.) error?
            if (dbcTarget.ErrorMessage != null)
            {
               sMsg = "ERROR: \"" + dbcTarget.ErrorMessage + "\" [20130823°1234]. Offending statement = "
                     + IOBus.Gb.Bricks.Cr + "-----------------------------------------"
                      + IOBus.Gb.Bricks.Cr + sSql2
                       + "-----------------------------------------"
                        ;
               dbcTarget.ErrorMessage = null;
               IOBusConsumer.writeHost(sMsg);
               break;
            }
         }

         return sErr;
      }

   }


   //==========================================================
   /*


   issue 20130828°1211 'SQLite no index names with cross'
   title : In SQLite index names must not contain a cross.
   symptoms : OleDb delivers primary indices with names like 'Customers#PX'.
      But SQLite dislikes the number sign in the index name, it answers with
      error "SQL logic error or missing database - near "#PX": syntax error".
   location : Method 20130828°1151 tableCreate_sqlCreateIndex().
   workaround : Manipulate index name (sequence 20130828°1212).
   solution :
   status :


   issue 20130828°1151 'SQLite allows no two indices with the same name'
   title : SQLite allows no two indices with the same name.
   symptoms : Attempting to create an index with a name already used by
      an index for another table yields error "SQL logic error or missing
      database - index FinishedAccounttwo already exists".
   location : Method 20130828°1151 tableCreate_sqlCreateIndex().
   workaround : Manipulate index names to be unique across the database,
      see sequence 20130828°1152, which boldly prepends the table name.
   solution :
   status :


   issue 20130828°1141 'SQLite has no combined primary keys'
   title :
   symptoms : Attempting to create a table with more than one primary key
      fields, the statement yields error "SQL logic error or missing
      database - table "Addresses" has more than one primary key".
   location : Method 20130821°1151 tableCreate_sqlCreateTable().
   workaround : Create a UNIQUE INDEX instead, the fields possibly NOT NULL
   ref : See e.g. 20130828°1132 'Stacko: SQL multi-primary key'
   solution :
   status : Solved


   ref 20130828°1132 'Stacko: SQL multi-primary key'
   title : Thread 'SQLite multi-Primary Key on a Table, one of them is Auto Increment'
   url : http://stackoverflow.com/questions/6154730/sqlite-multi-primary-key-on-a-table-one-of-them-is-auto-increment
   usage : Method 20130821°1151 tableCreate_sqlCreateTable()
   note :


   ref 20130828°1121 'Stacko: SQLite index columns'
   title : Thread 'Using SQLite how do I index columns in a CREATE TABLE statement?'
   url : http://stackoverflow.com/questions/1676448/using-sqlite-how-do-i-index-columns-in-a-create-table-statement
   usage : E.g. method 20130828°1111 tableCreate_sqlCreateIndex()
   note :


   todo 20130825°1211 'provide Clone class public methods'
   title : Provide Clone class public methods e.g. CloneTable
   descript : In practice, we probably want e.g. clone a single existing table
      to an already existing other database from the GUI or from commandline.
   note : This comes along with the general request for come command object
      to make menu items work as independend actions.
   status : Open
   priority : Low


   issue 20130824°1131 'chinese and null characters in memo field'
   title : Chinese and null characters from field ObjLang.namelong
      where goodname = 'abfragekaskade systemtes'
   description : Snippet from transcript #20130824.1134:
      // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      // 49: DbClone create table 20 'ObjLang'...... 6 cols
      // 50:         Fill in 11946 records .................................................................................................... . .
      // 51: ERROR: "SQL logic error or missing database
      // unrecognized token: "'*"" [#20130823.1234]. Offending statement =
      // -----------------------------------------
      // INSERT INTO ObjLang
      // ( oid
      // , language
      // , goodname
      // , namelong
      // , helptext
      // , audio
      // ) VALUES
      // ( 2010
      // , 1
      // , 'abfragekaskade systemtes'
      // , '*
      // 52: DbClone create table 21 'ObjProp'.... 4 cols
      // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
   finding : In the debugger, I see a whole chinese string, but in the text
      view only those two characters. No idea, what exactly is going on here.
   ref : Screenshots 20130824°1132 and #20130824.1133
   workaround : If the two chars are found, the whole value is set '*'.
   solution :
   status :


   issue 20130824°0915 'names beginning with ciphers'
   title : Table and fieldnames beginning with ciphers
   location : Method 20130823°1551 getCleanTablename()
   description :
   workaround :
   solution : Wrap name in quotes (seq 20130824°1221)
   status : Open


   issue 20130824°0914 'SQLite fields of DataType Byte[]'
   title : What are the exact rules for Blob fields?
   location : Method 20130823°1552 getSqlValueFromObject()
   description : E.g. 'ObjLang.audio'
   workaround : Provisory set null
   solution :
   status : Open


   issue 20130824°0913 'keywords as SQLite field names'
   title : What are the exact rules
   location : Method 20130823°1551 getCleanTablename(), then
      method 20130719°0932 IOBus.Utils.SqlTokenTicks()
   description : The fieldname 'From' or 'Default' make the insert statement fail.
   workaround :
   solution : Quote those words (e.g. 20130824°0952)
   status : Open


   issue 20130824°0912 'umlauts in SQLite field values'
   title : How exactly to umlauts?
   location : Method 20130823°1551 getCleanTablename()
   description :
   workaround : Manual mangling on demand
   solution :
   status : Open


   issue 20130824°0911 'special chars in SQLite field values'
   title : How exactly to write quotes and possible other special chars into a field?
   location : Method 20130823°1552 getSqlValueFromObject()
   description :
   workaround : Manual mangling on demand
   solution :
   status : Open


   issue 20130823.1541 'no dashes in sqlite fieldnames'
   title : If an SQLite table create statement contains fieldnames with dashes,
      the create will fail.
   finding : I tried wrapping fieldnames with dashes in backticks, this did not help.
   workaround : Remove any dashes
   status :
   todo : Find exact rules, possibly the dashes could be preserved
   priority :


   issue 20130823°1411 'OleDb has no LIMIT clause'
   title : 'OleDb SQL does not know the LIMIT clause'
   symptom : An SQL statement executed by the OleDb DbClient will yield
      null if it contains the LIMIT/OFFSET clause.
   description :
   ref : See 'thread: no LIMIT clause in OleDb' (20130823°1321)
   workaround : Use only statements without LIMIT clause.
   solution : None
   status :


   ref 20130823°1321 'thread: no LIMIT clause in OleDb'
   title : Thread 'LIMIT Clause is OleDbCommand'
   url : http://forums.asp.net/t/1114677.aspx/1
   usage : Method 20130823°1231 cloneTableFillRecords()
   note : The bottom line is 'no LIMIT clause in OleDb'.


   issue 20130823°1311 'OleDb tablenames with underline or dash'
   title :
   description : For OleDb, the 'SELECT COUNT(*)' command throws exception
      'Index not found' (20130823°1232) with table _Internetpartner-Merging-Excel_.
      For other tables, the same command does work. And for the same table
      the 'SELECT *' does work. It seems to be some OleDb quirk with the
      underline or dash.
   location : Method 20130823°1211 cloneTableFill()
   workaround : Skip the offending table for now.
   solution :
   status :
   priority :
   note : Searching the internet for 'oledb tablename dash underscore' and the
      like does not yield much results. Often Excel and Jet engine is mentioned.
   note : For the experiments list see
   note : Archived code from sequence 20130823°1215:
      // if (! IOBus.Gb.Debag.ShutdownArchived)
      // {
      //    // note : Keep this experiment list until issue 20130823°1311 is solved
      //    sSql = "select count(*) from " + sTbl + ";";                       // exception 'Index not found' (20130823°1232) (see issue 20130823°1311)
      //    sSql = "SELECT COUNT(1) FROM " + sTbl + ";";                       // exception 'Index not found'
      //    sSql = "SELECT COUNT() FROM " + sTbl + ";";                        // '... wrong number of arguments ...'
      //    sSql = "SELECT * FROM `_Internetpartner-Merging-Excel_`";          // THIS WORKS
      //    sSql = "SELECT COUNT(*) FROM Addresses";                           // THIS WORKS
      //    sSql = "SELECT COUNT(*) FROM \"_Internetpartner-Merging-Excel_\""; // 'Syntax error in query.  Incomplete query clause.'
      //    sSql = "SELECT COUNT(*) FROM _Internetpartner-Merging-Excel_";     // 'Syntax error in FROM clause.'
      //    sSql = "SELECT COUNT(*) FROM [_Internetpartner-Merging-Excel_]";   // 'Index not found'
      //    sSql = "SELECT COUNT(*) FROM ([_Internetpartner-Merging-Excel_])"; // 'Index not found'
      //    sSql = "SELECT COUNT(*) FROM `_Internetpartner-Merging-Excel_$`";  // "The Microsoft Jet database engine could not find the object '_Internetpartner-Merging-Excel_$'. ..."
      //    sSql = "SELECT COUNT(*) FROM `_Internetpartner-Merging-Excel_`";   // 'Index not found'
      //    sSql = "SELECT COUNT(*) FROM _Internetpartner\\-Merging-Excel_";   // 'Syntax error in query. Incomplete query clause.'
      //    sSql = "SELECT COUNT(*) FROM `_Internetpartner\\-Merging-Excel_`"; // "The Microsoft Jet database engine could not find the object '_Internetpartner\-Merging-Excel_'. ..."
      //    sSql = "SELECT (COUNT(*)) FROM `_Internetpartner-Merging-Excel_`"; // 'Index not found'
      // }


   todo 20130823°1221 'dbc.Execute vs. dbc.ExecuteOnWorker()'
   title : Difference between method 20130604°0337 dbclient.Execute() and
      method 20130604°0338 dbclient.ExecuteOnWorker().
   location : class 20130818°1532 DbClone
   description : Find out, what service 'DataSet executeStatement()' would offer,
      and how exactly it is intertweened with QueryPonyGui. After all, QueryPonyLib
      shall be a library independend on QueryPonyGui. So that interference seems
      to be a good service, but the dependancy must be resolved e.g. by replacing
      it with passing a delegate.
   ref : Method 20130821°1511 executeStatement()
   ref : Compare note 20130823°1224


   todo 20130822°2112 'introduce splashscreen delegate'
   title : Introduce a splashscreen delegate to be used by the library
   description : E.g. in method 20130823°1211 cloneTableFill(), we would
      like to display the splashscreen during making the database connection.
      But we dislike to call the GUI from the library, this would violate
      the design rules. But if we had got a delegate from the GUI during
      initialisation, that could be used.
   location : class 20130619°1221 InitLib
   priority : medium


   issue 20130822°2111 'DbClient triggers QueryForm methods'
   title : SQL command yielding an answer table fails with QueryForm involved
   symptoms : When DbClone.cloneTableFill() executes the SQL statement to retrieve
      the record count of the source table, it fails, throwing exceptions with
      hard to locate origin. Somehow is the QueryForm involved via events.
   note : The breakpoint list is '20130822.2112xx'.
   location : Symptoms at DbClone.cloneTableFill()
   finding : The separation of GUI and Lib is not yet complete. Somehow, in
      the process of delivering the query result, QueryForm.cs gets involved.
      This happens via some events registered by QueryForm objects, and ignited
      by DbClient objects.
   solution : Do not use 'DataSet executeStatement()' but 'DataSet
      dbclient.ExecuteOnWorker()' [solution 20130823°1214]. This solves the
      issue sufficiently. But curiosity about executeStatement() remains.
   status : Solved partially
   note : Compare todo 20130823°1221


   todo 20130818°1545 'make class DbClone static'
   title : Declare class 20130818°1532 DbClone as static
   location : File 20130818°1531 DbClone.cs
   descript : Cloning is not a typical object but a singular action. This
      class were also fine as a static class, without constructor, and the
      parameters not being passed as class properties, but as ordinary
      parameters on the calling line. Perhaps change this class to being
      static.
   priority :
   status : Open


   issue 20130818°1538 'ping server'
   note : Somehow ping target server. It is not yet clear how to proceed with a
      pure server connection with a not yet existing database. BTW, the _csTgt
      connection settings are such 'uncomplete' settings.
   status : Open


   */
   //==========================================================

}
