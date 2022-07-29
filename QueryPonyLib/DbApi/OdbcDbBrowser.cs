#region Fileinfo
// file        : 20130604°0831 /QueryPony/QueryPonyLib/DbApi/OdbcDbBrowser.cs
// summary     : Class 'OdbcDbBrowser' constitutes an implementation of IDbBrowser
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// encoding    : UTF-8-with-BOM
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of IBrowser</summary>
   /// <remarks>id : 20130604°0832</remarks>
   internal class OdbcDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for ODBC tables</summary>
      /// <remarks>id : 20130604°0833</remarks>
      class OdbcNode : TreeNode
      {
         /// <summary>This internal field stores the treenode type (why?)</summary>
         /// <remarks>id : 20130604°0834</remarks>
         internal string _type = "";

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0835</remarks>
         internal string _name;

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0852</remarks>
         internal string _owner;

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0853</remarks>
         internal string _safeName;

         /// <summary>This internal field stores the DragText if an ODBC treenode is dragged</summary>
         /// <remarks>id : 20130604°0854</remarks>
         internal string _dragText;

         /// <summary>This constructor creates a new ODBC table treenode</summary>
         /// <remarks>id : 20130604°0836</remarks>
         /// <param name="text">...</param>
         public OdbcNode(string sText) : base(sText)
         {
         }
      }

      /// <summary>This private const ...</summary>
      /// <remarks>id : 20130604°0837</remarks>
      const int i_Timeout = 5;

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130604°0838</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new OdbcDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130604°0839</remarks>
      /// <param name="dbClient">The DbClient for which to create this OdbcDbBrowser object</param>
      public OdbcDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the ODBC DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130604°0841</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0844</remarks>
      /// <param name="dt">...</param>
      /// <param name="schemaName">...</param>
      /// <param name="schemaType">...</param>
      /// <param name="SchemaColumnName">...</param>
      /// <param name="NameColumnName">...</param>
      private void AppendSchemas ( DataTable dt                                // Should this be a ref parameter? Yes, complex types are treated as references. [debug 20130719°1424]
                                  , string sSchemaName                         //
                                   , string sSchemaType                        //
                                    , string sSchemaColumnName                 // E.g. "TABLE_SCHEM"
                                     , string sNameColumnName                  // E.g. "TABLE_NAME"
                                      )
      {
         OdbcConnection con = null;
         DataTable dt2 = null;

         //----------------------------------------------------
         // issue 20130719°1429 ''
         // title : ODBC representation incomplete
         // reason : The schema retrieving algorithms are flawed.
         // todo : (1) See todos 20130719°1427/1428, solve them perhaps empirically.
         //         (2) Provide documentation to understand the algorithms per se.
         // status : At least the tables are shown, this is fine for now.
         // note : Are there really no simpler ways to retrieve the ODBC schema informations?
         // note : To comprehend this debug session, view all '20130719.142' locations.
         //----------------------------------------------------

         try
         {
            con = ((OdbcConnection) ((OdbcClient) DbClient).Connection);

            // Intervention [seq 20130719°1426]
            // This line also throws an exception causing a messagebox pop, but then continues.
            //    To get rid of the messagebox provisory, wrap it in an individual try envelop.
            try
            {
               dt2 = con.GetSchema(sSchemaName);                               // I.e. ODBC SchemaGuid.Columns or .Tables , restrictions // I.e. new object[] {null, null, null, "TABLE"}
            }
            catch (Exception ex)
            {
               // Todo : Analyse and fix this situation. [todo 20130719°1428]
               string sErr = ex.Message;
            }

            if (dt2 != null)
            {
               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
               // Debug [session 20130719°1422]
               // result : I shut down two bad lines by wrapping them in individual try envelops,
               //    then the ODBC feature is working, though only tables are shown, not the other
               //    objects. For the two rendered harmless lines are see interventions 20130719°1426
               //    and 20130719°1425. To list all locations involved in this debug sesseion search
               //    for '20130719.142'

               if (Glb.Debag.Execute_No)
               {
                  string sDbg1 = "", sDbg2 = "";
                  string sFile1 = InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_odbc1.xml";
                  string sFile2 = InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_odbc2.xml";
                  dt.WriteXml(sFile1);                                         // Empty, shall be filled during this method
                  dt2.WriteXml(sFile2);                                        // Filled with the list of tables as expected
                  sDbg1 = System.IO.File.ReadAllText(sFile1);
                  sDbg2 = System.IO.File.ReadAllText(sFile2);
               }

               // sFile2 during the first round looks like this:
               //----------------------
               // <?xml version="1.0" standalone="yes"?>
               // <DocumentElement>
               //   <Tables>
               //     <TABLE_CAT>C:\NETDIR\JOESGARAGE\FIRMENDATEN</TABLE_CAT>
               //     <TABLE_NAME>Addresses</TABLE_NAME>
               //     <TABLE_TYPE>TABLE</TABLE_TYPE>
               //   </Tables>
               //   <Tables>
               //     <TABLE_CAT>C:\NETDIR\JOESGARAGE\FIRMENDATEN</TABLE_CAT>
               //     <TABLE_NAME>Articlegroups</TABLE_NAME>
               //     <TABLE_TYPE>TABLE</TABLE_TYPE>
               //   </Tables>
               // </DocumentElement>
               //----------------------

               // sFile1 after the last round looks like this:
               //----------------------
               // <?xml version="1.0" standalone="yes"?>
               // <NewDataSet>
               //   <Schema>
               //     <type>U</type>
               //     <object>Addresses</object>
               //   </Schema>
               //   <Schema>
               //     <type>U</type>
               //     <object>Articlegroups</object>
               //   </Schema>
               // </NewDataSet>
               //----------------------

               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

               DataColumn SchemaColumn = dt2.Columns[sSchemaColumnName];       // E.g. "TABLE_SCHEM"
               DataColumn NameColumn = dt2.Columns[sNameColumnName];           // E.g. "TABLE_NAME"

               DataColumn NewTypeColumn = dt.Columns["type"];
               DataColumn NewObjectColumn = dt.Columns["object"];
               DataColumn NewOwnerColumn = dt.Columns["owner"];
               foreach (DataRow dr in dt2.Rows)
               {
                  DataRow NewDataRow = dt.NewRow();
                  NewDataRow[NewTypeColumn] = sSchemaType;

                  // [intervention 20130719°1425]
                  // This line is sometimes bad, try wrapping it individually — Yes this helps to some degree.
                  try
                  {
                     // Throws exception "Unable to cast object of type 'System.DBNull' [line 20130719°1421]
                     NewDataRow[NewOwnerColumn] = (string)dr[SchemaColumn];
                  }
                  catch (Exception ex)
                  {
                     string s = ex.Message;
                     // Todo 20130719°1427 : Analyse and fix this situation.
                  }

                  NewDataRow[NewObjectColumn] = (string)dr[NameColumn];
                  dt.Rows.Add(NewDataRow);
               }
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.ToString());
         }
         finally
         {
            if (dt2 != null)
            {
               dt2.Dispose();
            }
         }
      }

      /// <summary>This method delivers a clone of this ODBC DbBrowser for another ODBC DbClient</summary>
      /// <remarks>
      /// id : 20130604°0851
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122105)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         OdbcDbBrowser sb = new OdbcDbBrowser(newDbClient);
         return sb;
      }

      /// <summary>This method creates a DataSet with one table of name 'Schema' with four columns and returns it</summary>
      /// <remarks>
      /// id : 20130604°0842
      /// note : This method is particular for the ODBC DbBrowser. The other DbBroswers have no such method.
      /// </remarks>
      /// <returns>The wanted DataSet with one table of name 'Schema' and four columns</returns>
      private DataSet CreateSchemaDataset()
      {
         DataSet dsSchema = new DataSet();

         DataTable dtSchema = dsSchema.Tables.Add("Schema");
         dtSchema.Columns.Add("type", typeof(string));
         dtSchema.Columns.Add("shipped", typeof(string));
         dtSchema.Columns.Add("object", typeof(string));
         dtSchema.Columns.Add("owner", typeof(string));

         return dsSchema;
      }

      /// <summary>This method creates the context menu for the given ODBC table node</summary>
      /// <remarks>id : 20130604°0847</remarks>
      /// <param name="node">The ODBC treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given ODBC treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is OdbcNode))
         {
            return null;
         }

         OdbcNode sn = (OdbcNode)node;
         StringCollection output = new StringCollection();

         if (sn._type == Glb.NodeTypes.UserTable                               // "U"
             || sn._type == Glb.NodeTypes.SystemTable                          // "S"
              || sn._type == Glb.NodeTypes.View                                // "V"
               )
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + sn._safeName); // "select * from"

            //output.Add("sp_help " + sn.safeName);
            //if (sn.type != "V")
            //{
            //   output.Add("sp_helpindex " + sn.safeName);
            //   output.Add("sp_helpconstraint " + sn.safeName);
            //   output.Add("sp_helptrigger " + sn.safeName);
            //}

            output.Add(Glb.TvContextMenuItems.InsertAllFields);                // "(insert all fields)"
            output.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);     // "(insert all fields, table prefixed)"
         }

         //if (sn.type == "V" || sn.type == "P" || sn.type == "FN")
         //   output.Add("View / Modify " + sn.name);

         //if (sn.type == "CO" && ((ODBCNode)sn.Parent).type == "U")
         //   output.Add("Alter column...");

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130604°0848</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is OdbcNode))
         {
            return null;
         }

         OdbcNode sn = (OdbcNode)node;

         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))         // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith("(insert all fields"))
         {
            StringBuilder sb = new StringBuilder();

            // if the table-prefixed option has been selected, add the table name to all the fields
            string prefix = sAction == Glb.TvContextMenuItems.InsertAllFields ? "" : sn._safeName + "."; // "(insert all fields)"
            int chars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(node))
            {
               if (chars > 50)
               {
                  chars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((OdbcNode)subNode)._dragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130604°0849</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         return new string[] { _dbClient.Database };
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>
      /// id : 20130604°0846
      /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout
      ///    the various DbBrowser implmementations. (todo 20130723°090704)
      /// </remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted dragtext</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = "";

         // Todo 20130719°0935`12 : What about using method 20130719°0932 SqlTokenTicks() here?
         if (node is OdbcNode)
         {
            sRet = ((OdbcNode)node)._dragText;
         }

         return sRet;
      }

      /// <summary>This method builds the initial browser tree for this ODBC DbBrowser</summary>
      /// <remarks>id : 20130604°0843</remarks>
      /// <returns>The wanted array of ODBC table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] treenodesRet = new TreeNode[]
         {
            new TreeNode ("User Tables"),
            new TreeNode ("System Tables"),
            new TreeNode ("Views"),
            new TreeNode ("User Stored Procs"),
            new TreeNode ("System Stored Procs"),
            new TreeNode ("Functions")
         };

         // Debug [experiment 20130719°1423] Trying "TABLE_SCHEMA" instead "TABLE_SCHEM" — No, the mutilated words seem correct
         DataSet ds = CreateSchemaDataset();
         string[,] schemas = {   { "TABLES"     , Glb.NodeTypes.UserTable , "TABLE_SCHEM"     , "TABLE_NAME" }
                              ,  { "VIEWS"      , Glb.NodeTypes.View      , "TABLE_SCHEM"     , "TABLE_NAME" }
                               , { "PROCEDURES" , Glb.NodeTypes.Procedure , "PROCEDURE_SCHEM" , "PROCEDURE_NAME" }
                                };
         for (int i = 0; i < 3; i++)
         {
            AppendSchemas(ds.Tables[0], schemas[i, 0], schemas[i, 1], schemas[i, 2], schemas[i, 3]);
         }

         foreach (DataRow row in ds.Tables[0].Rows)
         {
            string type = row["type"].ToString();

            int position;
            if (type == Glb.NodeTypes.UserTable)                               // "U" user table
            {
               position = 0;
            }
            else if (type == Glb.NodeTypes.SystemTable)                        // "S" system table
            {
               position = 1;
            }
            else if (type == Glb.NodeTypes.View)                               // "V" view
            {
               position = 2;
            }
            else if (type == Glb.NodeTypes.Function)                           // "FN" function
            {
               position = 5;
            }
            else                                                               // user stored proc
            {
               position = 3;
            }

            string prefix = row["owner"].ToString() == "dbo" ? "" : row["owner"].ToString() + ".";
            OdbcNode node = new OdbcNode(prefix + row["object"].ToString());
            node._type = type;
            node._name = row["object"].ToString();
            node._owner = row["owner"].ToString();

            // If the object name contains a space, wrap the "safe name" in square brackets
            if (IOBus.Gb.Debag.Shutdown_Anyway)                                       // If new algorithm has proofen good, delete original algorithm [shutdown 20130719°1442]
            {
               // Original algorithm
               if (node._owner.IndexOf(' ') >= 0 || node._name.IndexOf(' ') >= 0)
               {
                  node._safeName = "[" + node._name + "]";
                  node._dragText = "[" + node._owner + "].[" + node._name + "]";
               }
               else
               {
                  node._safeName = node._name;
                  node._dragText = node._owner + "." + node._name;
               }
            }
            else
            {
               // New algorithm,  not yet much tested [seq 20130719°1443]
               node._owner = IOBus.Utils.Strings.SqlTokenTicks(node._owner, " -", "[]"); // If token contains ' ' or '-' then wrap it with squarebrackets
               node._name = IOBus.Utils.Strings.SqlTokenTicks(node._name, " -", "[]"); // If token contains ' ' or '-' then wrap it with squarebrackets
               node._safeName = node._name;
               node._dragText = node._owner + "." + node._name;
            }

            treenodesRet[position].Nodes.Add(node);

            // Add a dummy sub-node to user tables and views so they'll have a clickable expand sign
            //  allowing us to have GetSubObjectHierarchy called so the user can view the columns
            if (type == Glb.NodeTypes.UserTable                                // "U"
                || type == Glb.NodeTypes.View                                  // "V"
                 )
            {
               node.Nodes.Add(new TreeNode());
            }
         }
         return treenodesRet;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0845</remarks>
      /// <param name="node">...</param>
      /// <returns>...</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         // show the column breakdown for the selected table
         if (node is OdbcNode)
         {
            string table = node.Text;

            OdbcConnection con = null;
            DataTable tab = null;

            try
            {
               con = ((OdbcConnection)((OdbcClient)DbClient).Connection);

               //// //schema ie ODBC SchemaGuid.Columns or .Tables , restrictions; ie new object[] {null, null, null, "TABLE"}
               ////tab = con.GetSchema("Columns", new string[] { null, ((OdbcNode)node)._owner, ((OdbcNode)node)._name }); // [issue 20200928°1321] 'con.GetSchema() exception'

               // Owner workaround for issue 20200928°1321 [seq 20200928°1323]
               string sOwner = ((OdbcNode)node)._owner == "" ? null : ((OdbcNode)node)._owner;
               string[] sHelp = new string[] { null, sOwner, ((OdbcNode)node)._name };
               tab = con.GetSchema("Columns", sHelp);

               if (tab != null)
               {
                   DataColumn NameColumn = tab.Columns["COLUMN_NAME"];
                   DataColumn TypeColumn = tab.Columns["DATA_TYPE"];
                   DataColumn NullableColumn = tab.Columns["NULLABLE"];
                   TreeNode[] tn = new OdbcNode[tab.Rows.Count];
                   int count = 0;

                   foreach (DataRow row in tab.Rows)
                   {
                      //string nullable = row[NullableColumn].ToString().StartsWith("Y") ? "null" : "not null";

                      OdbcNode column = new OdbcNode(row[NameColumn].ToString()); // + " ("
                                                                               // + row[TypeColumn].ToString() + length + ", " + nullable + ")");
                      column._type = Glb.NodeTypes.Column;                     // "CO" column
                      column._dragText = row[NameColumn].ToString();
                      if (column._dragText.IndexOf(' ') >= 0)
                      {
                         column._dragText = "[" + column._dragText + "]";
                      }
                      column._safeName = column._dragText;

                      tn[count++] = column;
                   }
                   return tn;
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
            finally
            {
               if (tab != null) { tab.Dispose(); }
            }
         }
         return null;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1514 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1224 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1324 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0934 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0715 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }
   }
}
