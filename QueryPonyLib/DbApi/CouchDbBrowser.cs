#region Fileinfo
// file        : 20130616°1601 (20130605°1701) /QueryPony/QueryPonyLib/DbApi/CouchDbBrowser.cs
// summary     : Class 'CouchDbBrowser' constitutes an implementation of IBrowser for CouchDB
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : (20130616°1601) File cloned from SqliteDbBrowser.cs and modified
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
   /// <summary>This class constitutes an implementation of IBrowser for CouchDB</summary>
   /// <remarks>id : 20130616°1602 (20130605°1702)</remarks>
   internal class CouchDbBrowser : IDbBrowser
   {
      /// <summary>
      /// This subclass constitutes the TreeNode objects for CouchDB tables (but
      ///  CouchDB has no tables but documents, don't know yet what that will become).
      /// </summary>
      /// <remarks>id : 20130616°1604 (20130605°1704)</remarks>
      class CouchNode : TreeNode
      {
         /// <summary>This field stores the node type (why?)</summary>
         /// <remarks>id : 20130616°1605 (20130605°1705)</remarks>
         internal int _iType = Glb.NodeTypeNdxs.Invalid;                       // -1

         /// <summary>This property gets the DragText if a CouchDB (table?) treenode is dragged</summary>
         /// <remarks>
         /// id : 20130616°1606 (20130605°1706)
         /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout
         ///    the various DbBrowser implmementations. (todo 20130723°090701)
         /// </remarks>
         internal string _sDragText
         {
            get
            {
               string sRet = this.Text;
               // If token contains blank or hyphen, wrap it in e.g. singlequotes (20130723°0903)
               sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "''");
               return sRet;
            }
         }

         /// <summary>This constructor creates a new CouchDB (table?) treenode</summary>
         /// <remarks>id : 20130616°1607 (20130605°1707)</remarks>
         /// <param name="text">The wanted node text, e.g. ...</param>
         /// <param name="type">The wanted node type, e.g. -1 or 0, 1, etc for the nodes array index.</param>
         public CouchNode(string text, int type) : base(text)
         {
            this._iType = type;
         }
      }

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130616°1603 (20130605°1703)</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new CouchDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130616°1608 (20130605°1708)</remarks>
      /// <param name="dbClient">The DbClient for which to create this CouchDbBrowser object</param>
      public CouchDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the CouchDB DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130616°1609 (20130605°1709)</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this CouchDB DbBrowser for another CouchDB DbClient</summary>
      /// <remarks>
      /// id : 20130616°1610 (20130605°1710)
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122102)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         CouchDbBrowser couchdbbrowser = new CouchDbBrowser(newDbClient);
         return couchdbbrowser;
      }

      /// <summary>This method creates the context menu for the given CouchDB (table) node</summary>
      /// <remarks>id : 20130616°1614 (20130605°1714)</remarks>
      /// <param name="node">The CouchDB treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given CouchDB treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is CouchNode))
         {
            return null;
         }

         CouchNode on = (CouchNode)node;
         StringCollection output = new StringCollection();

         if (on._iType >= 0)
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + on._sDragText); // "select * from"
            output.Add(Glb.TvContextMenuItems.InsertAllFields);                // "(insert all fields)"
            output.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);     // "(insert all fields, table prefixed)"
         }

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130616°1615 (20130605°1715)</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is CouchNode))
         {
            return null;
         }

         CouchNode on = (CouchNode)node;
         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))         // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith("(insert all fields"))
         {
            StringBuilder sb = new StringBuilder();

            // If the table-prefixed option has been selected, add the table name to all the fields
            string prefix = sAction == Glb.TvContextMenuItems.InsertAllFields ? "" : on._sDragText + "."; // "(insert all fields)"
            int chars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(node))
            {
               if (chars > 50)
               {
                  chars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((CouchNode)subNode)._sDragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>
      /// id : 20130616°1616 (20130605°1716)
      /// issue : In this method, with the portnumber, a cast from uint to int is
      ///    used. If the both types are the same length, then an uint can store higher
      ///    portnumbers than an int. Clarify the exact ranges and circumstances.
      ///    Perhaps we have to apply some paranoia to avoid possible high portnumbers
      ///    to get malformed. (issue 20130723°1011)
      /// </remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         string[] ssResult = { }; // breakpoint

         if (IOBus.Gb.Debag.Shutdown_Anyway)
         {
            ssResult = GetCouchBrowserValues("Catalogs", null);
            if (ssResult == null)
            {
               ssResult = new string[] { _dbClient.Database };
            }
         }

         //----------------------------------------------------
         // Experimental first time accessing CouchDB somehow [seq 20130723°0951]
         // chg 20190410°0444 'shutdown Divan'
         /*
         Divan.CouchServer server = new Divan.CouchServer("localhost", (int)Glb.DbSpecs.CouchDefaultPortnum);

         // Note : Compare CouchTest.cs line154
         System.Collections.Generic.IList<string> iliDbs = null;
         iliDbs = server.GetDatabaseNames();

         // Cast List to array (there is certainly a more elegant method)
         Array.Resize(ref ssResult, iliDbs.Count);
         for (int i = 0; i < iliDbs.Count; i++)
         {
            ssResult[i] = iliDbs[i];
         }
         */
         //----------------------------------------------------

         return ssResult;
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>id : 20130616°1613 (20130605°1713)</remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         if (node is CouchNode)
         {
            return ((CouchNode)node)._sDragText;
         }
         else
         {
            return "";
         }
      }

      /// <summary>This method builds the initial browser tree for this CouchDB DbBrowser</summary>
      /// <remarks>id : 20130616°1611 (20130605°1711)</remarks>
      /// <returns>The wanted array of CouchDB (table) treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] top = new TreeNode[]
         {
            new TreeNode (Glb.NodeItems.Tables),                               // [0] "Tables"
            new TreeNode (Glb.NodeItems.Views),                                // [1] "Views"
         };

         int iCurNodeType = Glb.NodeTypeNdxs.Tables0;                          // Initial nodes array index 0

         foreach (TreeNode node in top)
         {
            string sFilter = "";

            CreateNodeHierachy(top, iCurNodeType++, sFilter);
         }

         return top;
      }

      /// <summary>This method creates the column nodes for the selected table</summary>
      /// <remarks>id : 20130616°1612 (20130605°1712)</remarks>
      /// <param name="node">The node to expand, e.g. node.Text = "Addresses"</param>
      /// <returns>Array with the wanted nodes</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         TreeNode[] tn = null;
         if (node is CouchNode)
         {
            string sTable = node.Text;
            string sDb = GetDatabaseFilter();
            string[] arRestrict = { null, null, sTable, null };
            string[] arFields = GetCouchBrowserValues("Columns", arRestrict);

            if (arFields != null)
            {
               tn = new CouchNode[arFields.Length];
               int count = 0;

               foreach (string sFieldname in arFields)
               {
                  CouchNode nColumn = new CouchNode(sFieldname, -1);
                  tn[count++] = nColumn;
               }
            }
         }
         return tn;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1511 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1221 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1321 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0931 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0712 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }

      #region Implementation Helpers

      /// <summary>This method retrieves the name of the connected database</summary>
      /// <remarks>id : 20130616°1621 (20130605°1721)</remarks>
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

      /// <summary>This method is called one time for each of the initial browser tree nodes to provide a subhierearchy</summary>
      /// <remarks>
      /// id : 20130616°1622 (20130605°1722)
      /// note : Used like top[curNodeType].Add("SELECT [TABLE_NAME] FROM [Tables] WHERE [Tabletyp] = {filter}").
      /// </remarks>
      /// <param name="top">...</param>
      /// <param name="curNodeType">...</param>
      /// <param name="filter">...</param>
      /// <returns>...</returns>
      private void CreateNodeHierachy ( TreeNode[] top                         // Complete treeview object
                                       , int iCurNodeType                      // Call counter: 0, 1, 2, 3
                                        , string sFilter                       // E.g. "TABLE", "VIEW", "SYSTEM TABLE", "SYSTEM VIEW"
                                         )
      {
         string[] arResult = { "Dummy1", "Dummy2" };

         if (arResult != null)
         {
            foreach (string str in arResult)
            {
               CouchNode node = new CouchNode(str, iCurNodeType);

               top[iCurNodeType].Nodes.Add(node);
            }
         }

         return;
      }

      /// <summary>This method ... performs a CouchDB-specific internal query</summary>
      /// <remarks>
      /// id : 20130616°1623 (20130605°1723)
      /// note : The query looks like 'SELECT {resultColumnName} FROM {schema} WHERE {restrictions}'
      /// </remarks>
      /// <param name="resultColumnName">...</param>
      /// <param name="restrictions">...</param>
      /// <returns>String-Array with Fields from </returns>
      private string[] GetCouchBrowserValues ( string sResultColName                   // e.g. "Tables"
                                              , string[] arRestrictions                // array of 4 strings - check: What do the fields mean exactly?
                                               )
      {
         string[] arRet = null;
         System.Data.CouchDB.CouchDBConnection con = null;
         DataTable dt = null;

         try
         {
            con = ((CouchDbClient)DbClient).Connection;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Perform some test calls
            if (Glb.Debag.Execute_No)
            {
               // Debug
               dt = con.GetSchema();
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema1.xml");

               dt = con.GetSchema("Tables");
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema2.xml");

               dt = con.GetSchema("Columns");
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema3.xml");

               string[] arRestrictions2 = { null, null, "Addresses", null };
               dt = con.GetSchema("Columns", arRestrictions2);
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema4.xml");
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            // Execute the wanted query
            dt = con.GetSchema(sResultColName, arRestrictions);

            if (Glb.Debag.Execute_No)
            {
               dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_couchdb_schema5.xml");
            }

            // Workaround for not knowing the exact parameterization [seq 20130607°1211]
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

            DataColumn col = dt.Columns[sCol];                                 // ["TABLE_TYPE"]?

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
