#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DbApi/SqliteDbBrowser.cs
// id          : 20130605°1701 (20130604°0931)
// summary     : This file stores class 'SqliteDbBrowser' to constitute
//                an implementation of IDbBrowser for SQLite.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        :
// note        : File cloned from OledbBrowser.cs and modified (20130605°1701)
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite; // [20130606°1329]
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QueryPonyLib
{

   /// <summary>This class constitutes an implementation of IBrowser for SQLite.</summary>
   /// <remarks>id : 20130605°1702 (20130604°0932)</remarks>
   internal class SqliteDbBrowser : IDbBrowser
   {

      /// <summary>This subclass constitutes the TreeNode objects for SQLite tables.</summary>
      /// <remarks>id : 20130605°1704 (20130604°0934)</remarks>
      class SqliteNode : TreeNode
      {

         /// <summary>This field stores the node type (why?).</summary>
         /// <remarks>id : 20130605°1705 (20130604°0935)</remarks>
         internal int _type = Glb.NodeTypeNdxs.Invalid;                                // -1


         /// <summary>This property gets the DragText if a SQLite table treenode is dragged.</summary>
         /// <remarks>
         /// id : 20130605°1706 (20130604°0936)
         /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout
         ///    the various DbBrowser implmementations. (todo 20130723°090707)
         /// </remarks>
         internal string _sDragText
         {
            get
            {
               string sRet = this.Text;

               // if the token contains a blank or hyphen, wrap it in e.g. squarebrackets (20130723°0902)
               sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "[]");

               return sRet;
            }
         }


         /// <summary>This constructor creates a new SQLite table treenode.</summary>
         /// <remarks>id : 20130605°1707 (20130604°0937)</remarks>
         /// <param name="text">The wanted node text, e.g. ...</param>
         /// <param name="type">The wanted node type, e.g. -1 or 0, 1, etc for the nodes array index.</param>
         public SqliteNode(string sText, int iType) : base(sText)
         {
            this._type = iType;
         }
      }


      /// <summary>This field stores the DbClient given in the constructor.</summary>
      /// <remarks>id : 20130605°1703 (20130604°0933)</remarks>
      private DbClient _dbClient;


      /// <summary>This constructor creates a new SqliteDbBrowser object for the given DbClient.</summary>
      /// <remarks>id : 20130605°1708 (20130604°0938)</remarks>
      /// <param name="dbClient">The DbClient for which to create this SqliteDbBrowser object</param>
      public SqliteDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }


      /// <summary>This property gets the SQLite DbClient for which this DbBrowser was created.</summary>
      /// <remarks>id : 20130605°1709 (20130604°0939)</remarks>
      public DbClient DbClient
      {
         get
         {
            return _dbClient;
         }
      }


      /// <summary>This method delivers a clone of this SQLite DbBrowser for another SQLite DbClient.</summary>
      /// <remarks>
      /// id : 20130605°1710 (20130604°0941)
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122109)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient dbcNew)
      {
         SqliteDbBrowser sdbb = new SqliteDbBrowser(dbcNew);
         return sdbb;
      }


      /// <summary>This method creates the context menu for the given SQLite table node.</summary>
      /// <remarks>id : 20130605°1714 (20130604°0945)</remarks>
      /// <param name="node">The SQLite treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given SQLite treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         // paranoia
         if (! (node is SqliteNode))
         {
            return null;
         }

         SqliteNode on = (SqliteNode)node;
         StringCollection scOutput = new StringCollection();

         if (on._type >= 0)
         {
            scOutput.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + on._sDragText);  // "select * from"
            scOutput.Add(Glb.TvContextMenuItems.InsertAllFields);                      // "(insert all fields)"
            scOutput.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);           // "(insert all fields, table prefixed)"
         }

         return scOutput.Count == 0 ? null : scOutput;
      }


      /// <summary>This method retrieves the command string behind a table node's context menu item.</summary>
      /// <remarks>id : 20130605°1715 (20130604°0946)</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is SqliteNode))
         {
            return null;
         }

         SqliteNode on = (SqliteNode)node;
         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))                 // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith("(insert all fields"))
         {
            StringBuilder sb = new StringBuilder();

            // if the table-prefixed option has been selected, add the table name to all the fields
            string sPrefix = (sAction == Glb.TvContextMenuItems.InsertAllFields ? "" : on._sDragText + "."); // "(insert all fields)"
            int iChars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(node))
            {
               if (iChars > 50)
               {
                  iChars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + sPrefix + ((SqliteNode)subNode)._sDragText;
               iChars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130605°1717 (20130604°0948)</remarks>
      /// <param name="dbfileName">...</param>
      /// <returns>...</returns>
      public static string GetConnectString(string sDbFilename)
      {
         string sRet = null;

         if (System.IO.File.Exists(sDbFilename))
         {
            string lowerDbFileName = sDbFilename.ToLower();

            if (lowerDbFileName.EndsWith(".connectstring"))
            {
               sRet = getConnectStringFromFileContent(sDbFilename);
            }
            else if (lowerDbFileName.EndsWith(".udl"))
            {
               sRet = getConnectStringFromFileContent(sDbFilename);
            }
            else if (lowerDbFileName.LastIndexOf('.') > 0)
            {
               sRet = getConnectStringForDatabaseFile(sDbFilename);
            }
            else
            {
               // todo : Supplement more serious error processing. (todo 20130709°0946)
               sRet = null; // perhaps better than any arbitrary value?
            }
         }

         return sRet;
      }


      /// <summary>
      /// This method returns the connectionstring to open a database through an UDL
      ///  or connect file. It reads *.connect; *.udl and *.dsn filename extensions.
      /// </summary>
      /// <remarks>
      /// id : 20130605°1719 (20130604°0951)
      /// note : Explanation of file extensions see this method in OleDbBrowser.cs
      /// todo : This method is just inherited from copying the file from OledbBrowser.cs. It
      ///         is not proofen yet, that it makes sense with SQLite as well. [20130605°171902]
      /// </remarks>
      /// <param name="dbfileName">The filename of the file, the SQLite connectionstring shall be taken from.</param>
      /// <returns>The wanted SQLite connectionstring</returns>
      public static string getConnectStringFromFileContent(string sFilename)
      {
         string sResult = string.Empty;
         string sFileContent;

         if ( Utils.ReadFromFile ( Path.Combine(Directory.GetCurrentDirectory()
                                  , sFilename
                                   ), out sFileContent
                                    ))
         {
            string[] ssLines = sFileContent.Split('\n', '\r');
            foreach (string sLine in ssLines)
            {
               if (    (sLine.Trim() != string.Empty)
                   &&  (! sLine.Trim().StartsWith(";"))
                    && (! sLine.Trim().StartsWith("["))
                     )
               {
                  if (sResult != string.Empty)
                  {
                     sResult += ";";
                  }
                  sResult += sLine;
               }
            }
         }
         else
         {
            // (seq 20130719°081204)
            string sErr = "Error with file " + sFilename;
            System.Windows.Forms.MessageBox.Show(sErr, sFilename);
         }

         return sResult;
      }


      /// <summary>This method retrieves the list of databases available on this server.</summary>
      /// <remarks>id : 20130605°1716 (20130604°0947)</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         string[] ssResult = GetSqliteBrowserValues ("Catalogs", null);
         if (ssResult == null)
         {
            ssResult = new string[] { _dbClient.Database };
         }
         return ssResult;
      }


      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window.</summary>
      /// <remarks>id : 20130605°1713 (20130604°0944)</remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = "";

         if (node is SqliteNode)
         {
            sRet = ((SqliteNode)node)._sDragText;
         }

         return sRet;
      }


      /// <summary>This method builds the initial browser tree for this SQLite DbBrowser.</summary>
      /// <remarks>id : 20130605°1711 (20130604°0942)</remarks>
      /// <returns>The wanted array of SQLite table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] top = new TreeNode[]
         {
            new TreeNode (Glb.NodeItems.Tables),                                       // [0] "Tables"
            new TreeNode (Glb.NodeItems.Views),                                        // [1] "Views"
            new TreeNode (Glb.NodeItems.SystemTables),                                 // [2] "System Tables"
            new TreeNode (Glb.NodeItems.SystemViews)                                   // [3] "System Views"
         };

         int iCurNodeType = Glb.NodeTypeNdxs.Tables0;                                  // initial nodes array index 0
         foreach (TreeNode node in top)
         {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // provisory hardcoded sequence while separating node-text and filter-string [20130614°1122]
            string sFilter = "";
            switch (iCurNodeType)
            {
               case Glb.NodeTypeNdxs.Tables0: sFilter = Glb.SchemaFilter.Table; break;  // 0 : "TABLE"
               case Glb.NodeTypeNdxs.Views1: sFilter = Glb.SchemaFilter.View; break;   // 1 : "VIEW"
               case Glb.NodeTypeNdxs.SystemTables2: sFilter = Glb.SchemaFilter.SystemTable; break;  // 2 : "SYSTEM TABLE"
               case Glb.NodeTypeNdxs.SystemViews3: sFilter = Glb.SchemaFilter.SystemView; break;  // 3 : "SYSTEM VIEW"
               default: break;                                                         // fatal
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CreateNodeHierachy(top, iCurNodeType++, sFilter);
         }

         return top;
      }


      /// <summary>This method creates the column nodes for the selected table.</summary>
      /// <remarks>id : 20130605°1712 (20130604°0943)</remarks>
      /// <param name="node">The node to expand, e.g. node.Text = "Addresses"</param>
      /// <returns>Array with the wanted nodes</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         TreeNode[] tn = null;
         if (node is SqliteNode)
         {
            string sTable = node.Text;
            string sDb = GetDatabaseFilter();
            string[] arRestrict = { null, null, sTable, null };
            string[] arFields = GetSqliteBrowserValues("Columns", arRestrict);

            if (arFields != null)
            {
               tn = new SqliteNode[arFields.Length];
               int count = 0;

               foreach (string sFieldname in arFields)
               {
                  SqliteNode nColumn = new SqliteNode(sFieldname, -1);
                  tn[count++] = nColumn;
               }
            }
         }
         return tn;
      }


      /// <summary>This method supplies the subtree hierarchy to the given table node.</summary>
      /// <remarks>id : 20130819°1518 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }


      /// <summary>This interface method retrieves the collections available for this data provider.</summary>
      /// <remarks>id : 20130826°1228 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] ssCollections = null;
         return ssCollections;
      }


      /// <summary>This interface method retrieves the array of Index Nodes for the given table.</summary>
      /// <remarks>id : 20130825°1328 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }


      /// <summary>This method retrieves an experimental schema object for debug purposes.</summary>
      /// <remarks>id : 20130819°0938 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }


      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient.</summary>
      /// <remarks>
      /// id : 20130819°0711 (20130819°0701)
      /// todo : This method is implemented for only OleDb so far. Implement the others. [todo 20130819°0719]
      /// </remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         if (IOBus.Gb.Debag.ExecuteYES)
         {
            string[] sDummy = { "N/A" };
            return sDummy;
         }

         string[] ssTables = null;

         string[] arResult = null;

         string sFilter = "Tables";

         string sDbName = GetDatabaseFilter();                                         // "main"

         string[] arRestrict = { null, null, null, null };                             // formerly { sDb, null, null, filter };
         arResult = GetSqliteBrowserValues(sFilter, arRestrict);


         return ssTables;
      }


      #region Implementation Helpers


      /// <summary>This method ... is called one time for each of the four initial browser tree nodes.</summary>
      /// <remarks>
      /// id : 20130605°1722 (20130604°0954)
      /// note : Used like top[curNodeType].Add("SELECT [TABLE_NAME] FROM [Tables] WHERE [Tabletyp] = {filter}").
      /// </remarks>
      /// <param name="top">...</param>
      /// <param name="curNodeType">...</param>
      /// <param name="filter">...</param>
      /// <returns>Nothing</returns>
      private void CreateNodeHierachy ( TreeNode[] top                                 // complete treeview object
                                       , int iCurNodeType                              // call counter: 0, 1, 2, 3
                                        , string sFilter                               // e.g. "TABLE", "VIEW", "SYSTEM TABLE", "SYSTEM VIEW"
                                         )
      {
         string[] arResult = null;

         ////result = GetOleDbBrowserValues ( "TABLE_NAME"
         ////                                 , OleDbSchemaGuid.Tables
         ////                                  , new object[] { GetDatabaseFilter(), null, null, filter }
         ////                                   );
         if (sFilter == Glb.SchemaFilter.Table) { sFilter = "Tables"; }                // "TABLE"
         if (sFilter == Glb.SchemaFilter.View) { sFilter = "Views"; }                  // "VIEW"
         if (sFilter == Glb.SchemaFilter.SystemTable) { sFilter = ""; }                // "SYSTEM TABLE"
         if (sFilter == Glb.SchemaFilter.SystemView) { sFilter = ""; }                 // "SYSTEM VIEW"
         string sDbName = GetDatabaseFilter();                                         // "main"
         string[] arRestrict = { null, null, null, null };                             // formerly { sDb, null, null, filter };
         arResult = GetSqliteBrowserValues(sFilter, arRestrict);

         if (arResult != null)
         {
            foreach (string str in arResult)
            {
               SqliteNode node = new SqliteNode(str, iCurNodeType);

               top[iCurNodeType].Nodes.Add(node);

               // Add a dummy sub-node to user tables and views so they will have a
               //  clickable expand sign allowing us to have GetSubObjectHierarchy
               //  called so the user can view the columns.
               node.Nodes.Add(new TreeNode());
            }
         }
      }


      /// <summary>This method ... opens a database file through OleDb.</summary>
      /// <remarks>
      /// id : 20130605°1718 (20130604°0949)
      /// example : HandleCmdLineParameterOpenDbFile("Northwind.mdb")
      ///    The file "QExpress.mdb.ConnectTemplate" contains a string.Format
      ///    for the OleDbConnectString with {0} as placeholder for the filename.
      /// </remarks>
      /// <param name="dbfileName">...</param>
      /// <returns>...</returns>
      private static string getConnectStringForDatabaseFile(string sDbFilename)
      {
         int iFileTypPos = sDbFilename.LastIndexOf('.') + 1;
         string sFilenameTemplate = "QExpress." + sDbFilename.Substring(iFileTypPos)
                                   + ".ConnectTemplate"
                                    ;

         string sConnectTemplate;

         // load Template from working or exe-directory
         if (( Utils.ReadFromFile ( Path.Combine
                                   ( Directory.GetCurrentDirectory()
                                    , sFilenameTemplate
                                     ), out sConnectTemplate
                                      ))
            || ( Utils.ReadFromFile ( Path.Combine
                                     //// ( GetExecPath()
                                     ( IOBus.Utils.Pathes.ExecutableFullFolderName()
                                      , sFilenameTemplate
                                       ), out sConnectTemplate
                                        )))
         {
            return string.Format(sConnectTemplate, sDbFilename);
         }
         else
         {
            string sMsg = string.Format ( "SQLite-ConnectTemplate-file \"{0}\" not found.\n\n"
                                        + "It contains Information for opening Databasefile \"{1}\""
                                         , sFilenameTemplate
                                          , sDbFilename
                                           );
            System.Windows.Forms.MessageBox.Show(sMsg);
         }

         return null;
      }


      /// <summary>This method delivers the name of the connected database.</summary>
      /// <remarks>id : 20130605°1721 (20130604°0953)</remarks>
      /// <returns>The wanted name of the database this DbClient is connected to</returns>
      private string GetDatabaseFilter()
      {
         string sResult = _dbClient.Database;
         if ((sResult != null) && (sResult.Length == 0))
         {
            return null;
         }
         return sResult;
      }

      /// todo : This method occurres redundant in several classes. Make it
      ///    an utility method in IOBus. [todo 20130727°0921 - done 20130905°0912]
      /*
      /// <summary>This method delivers the path of the executable.</summary>
      /// <remarks>id : 20130605°1720 (20130604°0952)</remarks>
      /// <returns>The wanted executable's path</returns>
      private static string GetExecPath()
      {
         string s = System.Reflection.Assembly.GetExecutingAssembly().Location;
         string sRet = System.IO.Path.GetDirectoryName(s);
         return sRet;
      }
      */

      /// <summary>This method performs a SQLite-specific internal query.</summary>
      /// <remarks>
      /// id : 20130605°1723 (20130604°0955)
      /// note : The query looks like 'SELECT {resultColumnName} FROM {schema} WHERE {restrictions}'
      /// ref : 'blog: getting started with sqlite and vs2010' (20130607°1101)
      /// ref : 'msdn: schemaeinschränkungen' (20130606°2323)
      /// ref : 'msdn: getschema und schemaauflistungen' (20130606°2322)
      /// ref : 'tread: reading sqlite table information' (20130606°2321)
      /// ref : 'thread: table schema does not work with sqlite' (20130606°1332)
      /// </remarks>
      /// <param name="resultColumnName">...</param>
      /// <param name="restrictions">...</param>
      /// <returns>String-Array with Fields from </returns>
      private string[] GetSqliteBrowserValues ( string sResultColName                  // e.g. "Tables"
                                               , string[] arRestrictions               // array of 4 strings - check: What do the fields mean exactly?
                                                )
      {

         SQLiteConnection conn = null;
         DataTable dt = null;
         string[] arRet = null;

         try
         {
            conn = ((SqliteDbClient) DbClient).Connection;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // perform some test calls
            if (Glb.Debag.ExecuteNo)
            {
               // debug
               dt = conn.GetSchema();
               dt.WriteXml(InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema1.xml");

               dt = conn.GetSchema("Tables");
               dt.WriteXml(InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema2.xml");

               dt = conn.GetSchema("Columns");
               dt.WriteXml(InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema3.xml");

               string[] arRestrictions2 = { null, null, "Addresses", null };
               dt = conn.GetSchema("Columns", arRestrictions2);
               dt.WriteXml(InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema4.xml");
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            // execute the wanted query
            dt = conn.GetSchema(sResultColName, arRestrictions);

            if (Glb.Debag.ExecuteNo)
            {
               dt.WriteXml(InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema5.xml");
            }


            ////DataColumn col = tab.Columns[resultColumnName]; // ["TABLE_TYPE"]
            // probably a workaround due to not exactly knowing the parameterization [20130607°1211]
            string sCol = "";

            if (sResultColName == "Catalogs") { sCol = "CATALOG_NAME"; }
            if (sResultColName == "Columns") { sCol = "COLUMN_NAME"; }
            if (sResultColName == "Tables") { sCol = "TABLE_NAME"; }
            switch (sResultColName)
            {
               case "Catalogs": sCol = "CATALOG_NAME"; break;
               case "Columns": sCol = "COLUMN_NAME"; break;
               case "Tables": sCol = "TABLE_NAME"; break;
               default : break;
            }

            DataColumn col = dt.Columns[sCol];

            // (continue with lines from the original OleDb method)
            arRet = new string[dt.Rows.Count];
            int iCount = 0;
            foreach (DataRow row in dt.Rows)
            {
               arRet[iCount++] = row[col].ToString().Trim();
            }
         }
         catch (Exception ex)
         {
            string sMsg = ex.Message;
         }
         finally
         {
            if (dt != null)
            {
               dt.Dispose();
            }
         }

         return arRet;
      }

      #endregion Implementation Helpers
   }
}
