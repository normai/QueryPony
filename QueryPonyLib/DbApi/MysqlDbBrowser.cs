#region Fileinfo
// file        : 20130612°0901 (20130604°0701) /QueryPony/QueryPonyLib/DatabaseApi/MysqlDbBrowser.cs
// summary     : Class 'MysqlDbBrowser' constitutes an implementation of IDbBrowser for MySQL
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// encoding    : UTF-8-with-BOM
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// status      : Experimental
// note        : File cloned from MssqlBrowser.cs and modified (20130612°0901)
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of IBrowser for MySQL</summary>
   /// <remarks>
   /// id : 20130612°0902 (20130604°0702)
   /// note : Other implementations of IBrowser are e.g.
   ///    ODBC (<see cref="ODBCBrowser"/>), OLEDB (<see cref="OledbBrowser"/>),
   ///    Oracle (<see cref="OracleBrowser"/>), MsSqlServer (<see cref="SqlBrowser"/>).
   /// </remarks>
   internal class MysqlDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for MySQL tables</summary>
      /// <remarks>id : 20130612°0903 (20130604°0703)</remarks>
      class MysqlNode : TreeNode
      {
         /// <summary>This field stores the node type (why?)</summary>
         /// <remarks>id : 20130612°0917 (20130604°0717)</remarks>
         internal int _type = Glb.NodeTypeNdxs.Invalid;                        // -1

         /// <summary>This property gets the DragText if a MySQL treenode is dragged</summary>
         /// <remarks>
         /// id : 20130615°1226 (after 20130612°0853, 20130605°1706, 20130604°0653)
         /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout the various DbBrowser implmementations. [todo 20130723°0907`03]
         /// </remarks>
         internal string _dragText
         {
            get
            {
               string sRet = this.Text;

               // If token contains blank or hyphen, wrap it in e.g. squarebrackets [line 20130723°0905]
               sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "[]");

               return sRet;
            }
         }

         /// <summary>This constructor creates a new MySQL table treenode</summary>
         /// <remarks>
         /// id : 20130612°0916 (20130604°0716)
         /// todo : Eliminate this constuctor, it is replaced by constructor 20130614°1452.
         /// </remarks>
         /// <param name="text">...</param>
         private MysqlNode(string sText) : base(sText) // 'private' makes it impossible to be accessed
         {
         }

         /// <summary>This constructor creates a new MySQL table treenode</summary>
         /// <remarks>
         /// id : 20130614°1452 (20130604°0937)
         /// note : When deriving this class from MssqlNode, we had a node with only the text
         ///        parameter, because the MS-SQL node types are based on schema-given values
         ///        (CO/FN/P/S/U/V), whereas the OleDb node types are based on the node creation
         ///        counter. Since for MySQL we decided to use the creation-counter-based node
         ///        types, we had to supplement the two-parameter constructor.
         /// </remarks>
         /// <param name="text">...</param>
         /// <param name="type">...</param>
         public MysqlNode(string sText, int iType) : base(sText)
         {
            this._type = iType;
         }
      }

      /// <summary>This private const ...</summary>
      /// <remarks>id : 20130612°0904 (20130604°0704)</remarks>
      const int timeout = 5;

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130612°0905 (20130604°0705)</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new MysqlDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130612°0906 (20130604°0706)</remarks>
      /// <param name="dbClient">The DbClient for which to create this MysqlDbBrowser object</param>
      public MysqlDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the MySQL DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130612°0907 (20130604°0707)</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this MySQL DbBrowser for another MySQL DbClient</summary>
      /// <remarks>
      /// id : 20130612°0915 (20130604°0715)
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122104)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         MysqlDbBrowser sb = new MysqlDbBrowser(newDbClient);
         return sb;
      }

      /// <summary>This method creates the context menu for the given MySQL table node</summary>
      /// <remarks>id : 20130612°0912 (20130604°0945)</remarks>
      /// <param name="node">The MySQL treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given MySQL treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is MysqlNode))
         {
            return null;
         }

         MysqlNode on = (MysqlNode)node;
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
      /// <remarks>id : 20130615°1225 (20130612°0913 20130604°0946)</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is MysqlNode)) { return null; }

         MysqlNode on = (MysqlNode)node;
         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))         // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith(Glb.TvContextMenuItems.InsertAllFields))       // "(insert all fields)"
         {
            StringBuilder sb = new StringBuilder();

            // If the table-prefixed option has been selected, add the table name to all the fields
            string prefix = sAction == Glb.TvContextMenuItems.InsertAllFields  // "(insert all fields)"
                           ? ""
                            : on._dragText + "."
                             ;
            int chars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(node))
            {
               if (chars > 50)
               {
                  chars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((MysqlNode)subNode)._dragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }


      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130612°0914 (20130605°1716 20130604°0947)</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         //----------------------------------------------------
         // note 20130630°0921
         // Values used in other databases for the 'collection name':
         //  - MySQL  : "Databases"
         //  - SQLite : "Catalogs"
         //  - ?      : "Schemata"
         //----------------------------------------------------
         string[] arRet = GetMysqlBrowserValues("Databases", null);
         if (arRet == null)
         {
            arRet = new string[] { _dbClient.Database };
         }
         return arRet;
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>id : 20130612°0911 (20130604°0711)</remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         if (node is MysqlNode)
         {
            return ((MysqlNode)node)._dragText;
         }
         else
         {
            return "";
         }
      }

      /// <summary>This method defines items in the root node for this MySQL DbClient</summary>
      /// <remarks>
      /// id : 20130615°1221 (20130612°0908 20130605°1711 20130604°0708)
      /// note : MSDN forum thread "display the list of tables in a mysql database" (20130612°1511)
      ///    http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/c533b095-5f35-4992-b90c-a9a46aff4f50
      /// </remarks>
      /// <returns>The wanted array of MySQL table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         // set up the basic nodes for a MySQL database
         //----------------------------------------------------
         // attention : As long as we use the node-creation-counter node types style,
         //     adhere to the strict order convention (20130614°1521)
         // note : Just some table names from the 'information_schema' table are seen so far.
         // issue : Find out how exactly the items for a MySQL database should look
         //    like (e.g. with help from HeidiSQL). and how exactly they interact the
         //    with the schema table. (issue 20130612°1411)
         //----------------------------------------------------
         TreeNode[] top = new TreeNode[]
         {
            new TreeNode (Glb.NodeItems.Tables),                               // No more "TABLES" but "Tables" (20130614°1151)
            new TreeNode (Glb.NodeItems.Views),                                // "Routines"
            new TreeNode (Glb.NodeItems.SystemTables),                         // "Processes"
            new TreeNode (Glb.NodeItems.SystemViews),                          // No more "VIEWS" but "Views" (20130614°1151)
            new TreeNode (Glb.NodeItems.Routines),                             // "Routines"
            new TreeNode (Glb.NodeItems.Processes)                             // "Processes"
         };

         int iCurNodeType = Glb.NodeTypeNdxs.Invalid;                          // Start value -1
         foreach (TreeNode node in top)
         {
            iCurNodeType++;

            // Colorize nodes to tag them as working ones and dummy ones
            System.Drawing.Color c = new System.Drawing.Color();
            switch (iCurNodeType)
            {
               case Glb.NodeTypeNdxs.Tables0: c = System.Drawing.Color.Blue; break;
               case Glb.NodeTypeNdxs.Views1: c = System.Drawing.Color.LightGray; break;
               case Glb.NodeTypeNdxs.SystemTables2: c = System.Drawing.Color.LightGray; break;
               case Glb.NodeTypeNdxs.SystemViews3: c = System.Drawing.Color.LightGray; break;
               default: c = System.Drawing.Color.OrangeRed; break;
            }
            top[iCurNodeType].ForeColor = c;

            //-------------------------------------------------
            // Debug sequence shifted from here to file 20130614o1432.linesdump.txt [cutout 20130727°0911]
            //-------------------------------------------------

            string sFilter = "";
            switch (iCurNodeType)
            {
               // The outcommented '+=' lines are relics from the 'workaround' for issue 20130615°1121
               case Glb.NodeTypeNdxs.Tables0: sFilter = Glb.SchemaFilter.Table; break;  // 0 : "TABLE"
               case Glb.NodeTypeNdxs.Views1: sFilter = Glb.SchemaFilter.View; break;  // 1 : "VIEW"
               case Glb.NodeTypeNdxs.SystemTables2: sFilter = Glb.SchemaFilter.SystemTable; break;  // 2 : "SYSTEM TABLE"
               case Glb.NodeTypeNdxs.SystemViews3: sFilter = Glb.SchemaFilter.SystemView; break;  // 3 : "SYSTEM VIEW"
               default: continue; // break ;
            }

            // Create and add the respective nodes (tables, views, ...)
            CreateNodeHierachy(top, iCurNodeType, sFilter);

         }

         return top;
      }

      /// <summary>This method creates the column nodes for the selected table</summary>
      /// <remarks>id : 20130615°1224 (20130612°0909 20130605°1712 20130604°0943)</remarks>
      /// <param name="node">The node to expand, e.g. node.Text = "Addresses"</param>
      /// <returns>Array with the wanted nodes</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         TreeNode[] tn = null;
         if (node is MysqlNode)
         {
            string sTable = node.Text;
            string sDb = GetDatabaseFilter();
            string[] arRestrict = { null, null, sTable, null };
            string[] arFields = GetMysqlBrowserValues("Columns", arRestrict);

            if (arFields != null)
            {
               tn = new MysqlNode[arFields.Length];
               int count = 0;

               foreach (string sFieldname in arFields)
               {
                  MysqlNode nColumn = new MysqlNode(sFieldname, -1);
                  tn[count++] = nColumn;
               }
            }
         }
         return tn;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1513 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1223 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1323 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0933 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0714 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }

      #region Implementation Helpers

      /// <summary>This method is helps implementation by returning the name of the currently opened database</summary>
      /// <remarks>id : 20130615°1132 (20130604°0953)</remarks>
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

      /// <summary>This method helps implementation by ... is called one time for each of the four initial browser tree nodes</summary>
      /// <remarks>
      /// id : 20130615°1223 (20130605°1722 20130604°0954)
      /// note : Works like ... top[curNodeType].Add("SELECT [TABLE_NAME] FROM [Tables] WHERE [Tabletyp] = {filter}").
      /// </remarks>
      /// <param name="top">...</param>
      /// <param name="curNodeType">...</param>
      /// <param name="filter">...</param>
      /// <returns>...</returns>
      private void CreateNodeHierachy ( TreeNode[] top                         // Complete treeview object
                                       , int curNodeType                       // Call counter: 0, 1, 2, 3
                                        , string filter                        // E.g. "TABLE", "VIEW", "SYSTEM TABLE", "SYSTEM VIEW"
                                         )
      {
         string[] result = null;
         if (filter == Glb.SchemaFilter.Table) { filter = "Tables"; }          // "TABLE"
         if (filter == Glb.SchemaFilter.View) { filter = "Views"; }            // "VIEW"
         if (filter == Glb.SchemaFilter.SystemTable) { filter = ""; }          // "SYSTEM TABLE"
         if (filter == Glb.SchemaFilter.SystemView) { filter = ""; }           // "SYSTEM VIEW"

         string sDb = GetDatabaseFilter();                                     // "main"
         string[] arRestrict = { null, null, null, null };                     // Formerly { sDb, null, null, filter };
         result = GetMysqlBrowserValues(filter, arRestrict);

         if (result != null)
         {
            foreach (string str in result)
            {
               MysqlNode node = new MysqlNode(str, curNodeType);

               top[curNodeType].Nodes.Add(node);

               // Add a dummy sub-node to user tables and views so they'll have a clickable expand sign
               //  allowing us to have GetSubObjectHierarchy called so the user can view the columns
               node.Nodes.Add(new TreeNode());
            }
         }
      }

      /// <summary>This method is an implementation helper to perform the MySQL-specific internal query.</summary>
      /// <remarks>
      /// id : 20130615°1222 (20130615°1133 20130605°1723 20130604°0955)
      /// callers : - GetDatabases()          with ( "Catalogs", null )
      ///           - CreateNodeHierachy()    with ( "Tables"/"Views"/..., { null, null, null, null } )
      ///           - GetSubObjectHierarchy() with ( "Columns", { null, null, sTable, null } )
      /// </remarks>
      /// <param name="resultColumnName">...</param>
      /// <param name="restrictions">...</param>
      /// <returns>String-Array with fields from ...</returns>
      private string[] GetMysqlBrowserValues ( string sCollectionName          // E.g. "Tables", "Views", "Columns"
                                              , string[] arRestrictions        // E.g. { null, null, null, null }, { null, null, <tablename>, null }
                                               )
      {
         string[] arRet = null;

#if MYSQL20130619YES

         MySql.Data.MySqlClient.MySqlConnection conn = null;
         DataTable datatable = null;

         try
         {
            conn = ((MysqlDbClient)DbClient).Connection;
            datatable = conn.GetSchema ( sCollectionName                       // I.e. OleDbSchemaGuid.Columns or .Tables
                                        , arRestrictions                       // I.e. new object[] {null, null, null, "TABLE"}
                                         );

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // issue 20130630°0931 ''
            // title : Write datatable.WriteXml() not to a file but to a string
            // question : How can I parameterize datatable.WriteXml(), so that it does not
            //    write to a file but directly to a string.
            // note : WriteXml() has several overloads, some with TextWriter or Stream as
            //    parameter. I experimented about that but found no solution so far.
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            // Debug [seq 20130630°0922]
            if (Glb.Debag.Execute_Yes)
            {
               string sDbg = "";
               string sFile = InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_mysql_schema.xml";
               datatable.WriteXml(sFile);
               sDbg = System.IO.File.ReadAllText(sFile);
            }

            string sTabColName = "";
            switch (sCollectionName)
            {
               case "Databases" : sTabColName = "database_name"   ; break;
               case "Columns"   : sTabColName = "COLUMN_NAME"     ; break;
               case "Tables"    : sTabColName = "TABLE_NAME"      ; break ;
               case "Views"     : sTabColName = "VIEW_DEFINITION" ; break ;
               default          : break;                                       // Todo : Implement error message [todo 20130615°1311]
            }

            DataColumn col = datatable.Columns[sTabColName];                   // Test // ["TABLE_NAME"] ["COLUMN_NAME"];

            arRet = new string[datatable.Rows.Count];
            int count = 0;
            foreach (DataRow row in datatable.Rows)
            {
               arRet[count++] = row[col].ToString().Trim();
            }
         }
         catch (Exception ex)
         {
            string sMsg = ex.Message;
         }
         finally
         {
            if (datatable != null)
            {
               datatable.Dispose();
            }
         }
#endif

         return arRet;

      }

      #endregion Implementation Helpers

   }
}
