#region Fileinfo
// file        : 20130616°1501 (20130605°1701) /QueryPony/QueryPonyLib/DbApi/PgsqlDbBrowser.cs
// summary     : Class 'PgsqlDbBrowser' constitute an implementation of IDbBrowser for PostgreSQL
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from SqliteDbBrowser.cs and modified (20130616°1501)
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of IBrowser for PostgreSQL</summary>
   /// <remarks>id : 20130616°1502 (20130605°1702)</remarks>
   internal class PgsqlDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for PostgreSQL tables</summary>
      /// <remarks>id : 20130616°1504 (20130605°1704)</remarks>
      class PgsqlNode : TreeNode
      {
         /// <summary>This field stores the node type (why?)</summary>
         /// <remarks>id : 20130616°1505 (20130605°1705)</remarks>
         internal int _type = Glb.NodeTypeNdxs.Invalid;                        // -1

         /// <summary>This property gets the DragText if a PostgreSQL table treenode is dragged</summary>
         /// <remarks>id : 20130616°1506 (20130605°1706)</remarks>
         internal string _dragText
         {
            get
            {
               string sRet = this.Text;
               // If the token contains a blank or hyphen, wrap it in e.g. backticks [seq 20130723°0906]
               sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "``");
               return sRet;
            }
         }

         /// <summary>This constructor creates a new PostgreSQL table treenode</summary>
         /// <remarks>id : 20130616°1507 (20130605°1707)</remarks>
         /// <param name="text">The wanted node text, e.g. ...</param>
         /// <param name="type">The wanted node type, e.g. -1 or 0, 1, etc for the nodes array index.</param>
         public PgsqlNode(string sText, int iType) : base(sText)
         {
            this._type = iType;
         }
      }

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130616°1503 (20130605°1703)</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new PgsqlDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130616°1508 (20130605°1708)</remarks>
      /// <param name="dbClient">The DbClient for which to create this PgsqlDbBrowser object</param>
      public PgsqlDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the PostgreSQL DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130616°1509 (20130605°1709)</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this PostgreSQL DbBrowser for another PostgreSQL DbClient</summary>
      /// <remarks>
      /// id : 20130616°1510 (20130605°1710)
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122108)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         PgsqlDbBrowser sb = new PgsqlDbBrowser(newDbClient);
         return sb;
      }

      /// <summary>This method creates the context menu for the given PostgreSQL table node</summary>
      /// <remarks>id : 20130616°1514 (20130605°1714)</remarks>
      /// <param name="node">The PostgreSQL treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given PostgreSQL treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         // Paranoia
         if (! (node is PgsqlNode))
         {
            return null;
         }

         PgsqlNode on = (PgsqlNode)node;
         StringCollection output = new StringCollection();

         if (on._type >= 0)
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + on._dragText); // "select * from"
            output.Add(Glb.TvContextMenuItems.InsertAllFields);                // "(insert all fields)"
            output.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);     // "(insert all fields, table prefixed)"
         }

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130616°1515 (20130605°1715)</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is PgsqlNode))
         {
            return null;
         }

         PgsqlNode on = (PgsqlNode)node;
         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))         // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith("(insert all fields"))
         {
            StringBuilder sb = new StringBuilder();

            // If the table-prefixed option has been selected, add the table name to all the fields
            string prefix = sAction == Glb.TvContextMenuItems.InsertAllFields ? "" : on._dragText + "."; // "(insert all fields)"
            int chars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(node))
            {
               if (chars > 50)
               {
                  chars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((PgsqlNode)subNode)._dragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }

      /// <summary>This method delivers a connectionstring it has read from the given file</summary>
      /// <remarks>id : 20130616°1517 (20130605°1717)</remarks>
      /// <param name="dbfileName">The filename of the file the connectionstring shall be read from.</param>
      /// <returns>The wanted connectionstring or null</returns>
      public static string GetConnectString(string sDbFilename)
      {
         string sRet = null; // default

         if (! System.IO.File.Exists(sDbFilename))
         {
            return sRet;
         }

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
            // Fatal
            sRet = null; // perhaps better than any filename?
            // Todo : Supplement fatal error processing. [todo 20130709°0945]
         }

         return sRet;
      }

      /// <summary>
      /// This method returns the connectionstring to open a database through an UDL
      ///  or connect file. It reads *.connect; *.udl and *.dsn filename extensions.
      /// </summary>
      /// <remarks>
      /// id : 20130616°1519 (20130605°1719)
      /// note : Explanation of file extensions see this method in OleDbBrowser.cs
      /// todo : This method is just inherited from copying the file from OledbBrowser.cs. It is
      ///         not proofen yet, that it makes sense with PostgreSQL as well. [20130605°171902]
      /// </remarks>
      /// <param name="dbfileName">The filename of the file, the PostgreSQL connectionstring shall be taken from.</param>
      /// <returns>The wanted PostgreSQL connectionstring</returns>
      public static string getConnectStringFromFileContent(string sFilename)
      {
         string result = string.Empty;
         string fileContent;
         if (Utils.ReadFromFile(Path.Combine(Directory.GetCurrentDirectory(), sFilename)
                                  , out fileContent
                                   ))
         {
            string[] lines = fileContent.Split('\n', '\r');
            foreach (string line in lines)
            {
               if (    (line.Trim() != string.Empty)
                   &&  (! line.Trim().StartsWith(";"))
                    && (! line.Trim().StartsWith("["))
                     )
               {
                  if (result != string.Empty)
                  {
                     result += ";";
                  }
                  result += line;
               }
            }
         }
         else
         {
            // [seq 20130719°0812`03]
            string sErr = "Error with file " + sFilename;
            System.Windows.Forms.MessageBox.Show(sErr, sFilename);
         }

         return result;
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130616°1516 (20130605°1716)</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         // Note 20130720°1422 : just one null for the array would suffice as well
         string[] result = GetPgsqlBrowserValues("Databases", new string[] { null, null, null, null });

         if (result == null)
         {
            result = new string[] { _dbClient.Database };
         }
         return result;
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>
      /// id : 20130616°1513 (20130605°1713)
      /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout
      ///    the various DbBrowser implmementations. (todo 20130723°090706)
      /// </remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = "";
         if (node is PgsqlNode)
         {
            sRet = ((PgsqlNode)node)._dragText;
         }
         return sRet;
      }

      /// <summary>This method builds the initial browser tree for this PostgreSQL DbBrowser</summary>
      /// <remarks>id : 20130616°1511 (20130605°1711)</remarks>
      /// <returns>The wanted array of PostgreSQL object treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] treenodesRet = new TreeNode[]
         {
            new TreeNode (Glb.NodeItems.Tables),                               // [0] "Tables"
            new TreeNode (Glb.NodeItems.Views),                                // [1] "Views"
         };

         int iCurNodeType = Glb.NodeTypeNdxs.Tables0;                          // initial nodes array index 0
         foreach (TreeNode node in treenodesRet)
         {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Provisory hardcoded sequence while separating node-text and filter-string [seq 20130614°1122]
            string sFilter = "";
            switch (iCurNodeType)
            {
               case Glb.NodeTypeNdxs.Tables0: sFilter = Glb.SchemaFilter.Table; break;  // 0 : "TABLE"
               case Glb.NodeTypeNdxs.Views1: sFilter = Glb.SchemaFilter.View; break;  // 1 : "VIEW"
               default: break;                                                 // Fatal
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CreateNodeHierachy(treenodesRet, iCurNodeType++, sFilter);
         }

         return treenodesRet;
      }

      /// <summary>This method creates the column nodes for the selected table</summary>
      /// <remarks>id : 20130616°1512 (20130605°1712)</remarks>
      /// <param name="node">The node to expand, e.g. node.Text = "Addresses"</param>
      /// <returns>Array with the wanted nodes</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         TreeNode[] tn = null;
         if (node is PgsqlNode)
         {
            string sTable = node.Text;
            string sDb = GetDatabaseFilter();
            string[] arRestrict = { null, null, sTable, null };
            string[] arFields = GetPgsqlBrowserValues("Columns", arRestrict);

            if (arFields != null)
            {
               tn = new PgsqlNode[arFields.Length];
               int count = 0;

               foreach (string sFieldname in arFields)
               {
                  PgsqlNode nColumn = new PgsqlNode(sFieldname, -1);
                  tn[count++] = nColumn;
               }
            }
         }
         return tn;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1517 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1227 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1327 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0937 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0718 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }

      #region Implementation Helpers

      /// <summary>This method ... is called one time for each of the (four) initial browser tree nodes</summary>
      /// <remarks>
      /// id : 20130616°1522 (20130605°1722)
      /// note : Used like top[curNodeType].Add("SELECT [TABLE_NAME] FROM [Tables] WHERE [Tabletyp] = {filter}").
      /// </remarks>
      /// <param name="top">The database node with it's still empty object subnodes Tables, Views.</param>
      /// <param name="curNodeType">The subnode we shall expand, e.g. Tables shall be appended the existing tables</param>
      /// <param name="filter">The object filter to be used for the collection to be appended, e.g. Tables, Views</param>
      /// <returns>The wanted subhierarchy for the given node type (e.g. Tables, Views)</returns>
      private void CreateNodeHierachy ( TreeNode[] tnDatabase                  // The complete database treeview object
                                       , int iCurNodeType                      // Call counter: 0, 1, 2, 3
                                        , string sFilter                       // E.g. "TABLE", "VIEW", "SYSTEM TABLE", "SYSTEM VIEW"
                                         )
      {
         string[] arResult = null;
         if (sFilter == Glb.SchemaFilter.Table) { sFilter = "Tables"; }        // "TABLE"
         if (sFilter == Glb.SchemaFilter.View) { sFilter = "Views"; }          // "VIEW"
         if (sFilter == Glb.SchemaFilter.SystemTable) { sFilter = ""; }        // "SYSTEM TABLE"
         if (sFilter == Glb.SchemaFilter.SystemView) { sFilter = ""; }         // "SYSTEM VIEW"
         string sDbName = GetDatabaseFilter();                                 // "main"

         /*
         note 20130720°1252 ''
         text : The ingredient to receive not all e.g. 160 PostgreSQL tables but just the
            tables of the selected database is the restiction two "owner = 'public'".
            (Compare reference 20130720°1232 'MSDN Forum, PostgreSQL metadata'
            and 20130720°1233 'MSDN, ADO.NET Schema Restrictions'
         status :
         */

         string[] arRestrict = { sDbName, "public", null, null };              // Note element two 'owner = "public"' [line 20130720°1244]
         arResult = GetPgsqlBrowserValues(sFilter, arRestrict);

         if (arResult != null)
         {
            foreach (string str in arResult)
            {
               PgsqlNode node = new PgsqlNode(str, iCurNodeType);

               tnDatabase[iCurNodeType].Nodes.Add(node);

               // Add a dummy sub-node to user tables and views so they'll have a
               //  clickable expand sign allowing us to have GetSubObjectHierarchy
               //  called so the user can view the columns
               node.Nodes.Add(new TreeNode());
            }
         }
      }

      /// <summary>This method ... opens a database file through OleDb</summary>
      /// <remarks>
      /// id : 20130616°1518 (20130605°1718)
      /// example: HandleCmdLineParameterOpenDbFile("Northwind.mdb")
      ///          The file "QExpress.mdb.ConnectTemplate" contains a string.Format
      ///          for the OleDbConnectString with {0} as placeholder for the filename.
      /// </remarks>
      /// <param name="dbfileName">...</param>
      /// <returns>...</returns>
      private static string getConnectStringForDatabaseFile(string sDbFilename)
      {
         int fileTypPos = sDbFilename.LastIndexOf('.') + 1;
         string templateFileName = "QExpress." + sDbFilename.Substring(fileTypPos)
                                  + ".ConnectTemplate"
                                   ;

         string connectTemplate;

         // Load template from working or exe-directory
         if (( Utils.ReadFromFile ( Path.Combine
                                   ( Directory.GetCurrentDirectory()
                                    , templateFileName
                                     ), out connectTemplate
                                      ))
            || ( Utils.ReadFromFile ( Path.Combine
                                     ( IOBus.Utils.Pathes.ExecutableFullFolderName()
                                      , templateFileName
                                       ), out connectTemplate
                                        )))
         {
            return string.Format(connectTemplate, sDbFilename);
         }
         else
         {
            string msg = string.Format("PostgreSQL-ConnectTemplate-file \"{0}\" not found.\n\n"
                                        + "It contains Information for opening Databasefile \"{1}\""
                                         , templateFileName
                                          , sDbFilename
                                           );
            System.Windows.Forms.MessageBox.Show(msg);
         }
         return null;
      }

      /// <summary>This method delivers the name of the connected database</summary>
      /// <remarks>id : 20130616°1521 (20130605°1721)</remarks>
      /// <returns>The wanted name of the database this DbClient is connected to</returns>
      private string GetDatabaseFilter()
      {
         string result = _dbClient.Database;
         if ((result != null) && (result.Length == 0))
         {
            return null;
         }
         return result;
      }

      // Replaced by method 20130905°0912
      /*
      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130616°1520 (20130605°1720)
      /// todo : Shift this method out of this class into a global place.
      /// </remarks>
      /// <returns>...</returns>
      private static string GetExecPath()
      {
         string sRet = "";
         string s = System.Reflection.Assembly.GetExecutingAssembly().Location;
         sRet = System.IO.Path.GetDirectoryName(s);
         return sRet;
      }
      */

      /// <summary>This method ... performs a PostgreSQL-specific internal query</summary>
      /// <remarks>
      /// id : 20130616°1523 (20130605°1723)
      /// note : The query looks like 'SELECT {resultColumnName} FROM {schema} WHERE {restrictions}'
      /// </remarks>
      /// <param name="resultColumnName">...</param>
      /// <param name="restrictions">...</param>
      /// <returns>String-Array with Fields from </returns>
      private string[] GetPgsqlBrowserValues ( string sResultColName           // E.g. "Tables"
                                              , string[] arRestrictions        // Array of 4 strings - check: What do the fields mean exactly?
                                               )
      {
         Npgsql.NpgsqlConnection con = null;
         DataTable dt = null;
         string[] arRet = null;

         try
         {
            con = ((PgsqlDbClient) DbClient).Connection;

            // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
            // Possibly perform some test calls

            // Debug [seq 20130720°1401]
            if (Glb.Debag.Execute_No)
            {
               /*
               Note 20130720°1424 ''
               This command reveals what schemas are available at all. For PostgreSQL
                  this are: MetaDataCollections, Restrictions, Databases, Tables,
                  Columns, Views, Users, Indexes, IndexColumns. This looks like
                  a list useful for building the nodes below the server node? Not
                  really, because e.g. Tables are subnodes of the Database nodes.
               Status : ?
               */

               dt = con.GetSchema();
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema1.xml");

               dt = con.GetSchema("Tables");
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema2.xml");

               dt = con.GetSchema("Columns");
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema3.xml");

               string[] arRestrictions2 = { null, null, "Addresses", null };
               dt = con.GetSchema("Columns", arRestrictions2);
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema4.xml");

               dt = con.GetSchema("Databases", new string[] { null, null, null, null });
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema5.xml");
            }
            // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

            // Execute the wanted query
            dt = con.GetSchema(sResultColName, arRestrictions);

            if (Glb.Debag.Execute_No)
            {
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema6.xml");
            }

            // Probably a workaround due to not exactly knowing the parameterization [line 20130607°1211]
            string sCol = "";

            if (sResultColName == "Databases") { sCol = "database_name"; }     // Empirical fix [line 20130720°1423]
            else if (sResultColName == "Catalogs") { sCol = "CATALOG_NAME"; }  // Superfluous
            else if (sResultColName == "Columns") { sCol = "COLUMN_NAME"; }    // Correct
            else if (sResultColName == "Tables") { sCol = "TABLE_NAME"; }      // Correct
            else if (sResultColName == "Views") { return arRet; }              // Provisory [line 20130720°1243] Avoid below exception
            else
            {
               // Program flow error?
               return arRet;
            }

            // Sequence outcommentd 20130720°1425
            /*
            switch (sResultColName)
            {
               case "Catalogs": sCol = "CATALOG_NAME"; break;
               case "Columns": sCol = "COLUMN_NAME"; break;
               case "Tables": sCol = "TABLE_NAME"; break;

               case "Views": sCol = ""; return arRet;                          // Avoid below exception [workaround 20130720°1243]
               default: break;
            }
            */

            // Note 20130720°1241 : BTW, read from the debugger: The four cells
            //  of dt.Columns[] are: 'table_catalog', 'table_schema', 'table_name', 'table_type'.

            DataColumn col = dt.Columns[sCol];

            // Continue with lines from the original OleDb method
            arRet = new string[dt.Rows.Count];
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
               arRet[count++] = row[col].ToString().Trim();
            }
         }
         catch (Exception ex)
         {
            // Exceptions encountered:
            //
            // - While setting this DbBrowser in motion first time (20130720°1242):
            //    "'column' argument cannot be null. Parameter name: column"
            //
            // - While trying to retrieve the databases with getDatabases()
            //   Exception "Specified method is not supported." (20130720°1421)

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
