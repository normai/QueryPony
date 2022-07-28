#region Fileinfo
// file        : 20130604°1021 /QueryPony/QueryPonyLib/DbApi/OracleDbBrowser.cs
// summary     : This file stores class 'OracleDbBrowser' to constitute
//                a simple implementation of IDbBrowser for Oracle.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>
   /// This class constitutes a simple implementation of IBrowser
   ///  for Oracle. No support for SPs, packages, etc.
   /// </summary>
   /// <remarks>id : 20130604°1022</remarks>
   internal class OracleDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for Oracle tables</summary>
      /// <remarks>id : 20130604°1023</remarks>
      class OracleNode : TreeNode
      {
         /// <summary>This internal field stores the node type (why?)</summary>
         /// <remarks>id : 20130604°1036</remarks>
         internal string _type = "";

         /// <summary>This property gets the DragText if an Oracle treenode is dragged</summary>
         /// <remarks>id : 20130604°1037</remarks>
         internal string _dragText = "";

         /// <summary>This constructor creates a new Oracle table treenode</summary>
         /// <remarks>id : 20130604°1038</remarks>
         public OracleNode(string sText) : base(sText) { }
      }

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1024</remarks>
      const int i_Timeout = 5;

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130604°1025</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new OracleDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130604°1026</remarks>
      /// <param name="dbClient">The DbClient for which to create this OracleDbBrowser object</param>
      public OracleDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the Oracle DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130604°1027</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this Oracle DbBrowser for another Oracle DbClient</summary>
      /// <remarks>
      /// id : 20130604°1035
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122107)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         OracleDbBrowser ob = new OracleDbBrowser(newDbClient);
         return ob;
      }

      /// <summary>This method creates the context menu for the given Oracle table node</summary>
      /// <remarks>id : 20130604°1032</remarks>
      /// <param name="node">The Oracle treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given Oracle treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is OracleNode))
         {
            return null;
         }

         OracleNode on = (OracleNode)node;
         StringCollection output = new StringCollection();

         if (   on._type == Glb.NodeTypes.Tttttt                               // "T"
             || on._type == Glb.NodeTypes.View                                 // "V"
              )
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + on._dragText); // "select * from"
         }

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130604°1033</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is OracleNode))
         {
            return null;
         }

         OracleNode on = (OracleNode)node;
         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom))         // "select * from"
         {
            return sAction;
         }
         else
         {
            return null;
         }
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130604°1034</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         return new string[] { _dbClient.Database };
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>
      /// id : 20130604°1031
      /// todo : Throughout the various *DbBrowser implementations, the usage of method GetDragText()
      ///    and the property _dragText is inconsistent. Sometimes this and sometimes that is used,
      ///    and the application of SqlTokenTicks sometimes has effect, sometimes has no effect.
      ///    Streamline the usage of the dragtext and SqlTokenTicks(). (todo 20130723°0907)
      /// </remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = null;

         if (node is OracleNode)
         {
            sRet = ((OracleNode)node)._dragText;

            // If token contains ' ' or '-' then wrap it with backticks, just on
            //  suspicion, not sure whether this is located right here (20130719°0934)
            sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "``");
         }

         return sRet;
      }

      /// <summary>This method retrieves the treenodes for the this Oracle DbBrowser</summary>
      /// <remarks>id : 20130604°1028</remarks>
      /// <returns>The wanted array of Oracle table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] tnRet = new TreeNode[]
         {
            new TreeNode ("User Tables"),
            new TreeNode ("User Views"),
         };

         DataSet ds = _dbClient.ExecuteOnWorker("select TABLE_NAME from USER_TABLES order by TABLE_NAME", i_Timeout);
         if (ds == null || ds.Tables.Count == 0)
         {
            return null;
         }

         foreach (DataRow row in ds.Tables[0].Rows)
         {
            OracleNode oraclenode = new OracleNode(row[0].ToString());
            oraclenode._type = Glb.NodeTypes.Tttttt;                           // "T";
            oraclenode._dragText = oraclenode.Text;
            tnRet[0].Nodes.Add(oraclenode);

            // Add a dummy sub-node to user tables and views so they'll have a
            //  clickable expand sign allowing us to have GetSubObjectHierarchy
            //  called so the user can view the columns
            oraclenode.Nodes.Add(new TreeNode());
         }

         ds = _dbClient.ExecuteOnWorker("select VIEW_NAME from USER_VIEWS order by VIEW_NAME", i_Timeout);
         if (ds == null || ds.Tables.Count == 0)
         {
            return tnRet;
         }

         foreach (DataRow row in ds.Tables[0].Rows)
         {
            OracleNode node = new OracleNode(row[0].ToString());
            node._type = Glb.NodeTypes.View;                                   // "V"
            node._dragText = node.Text;
            tnRet[1].Nodes.Add(node);

            // Add a dummy sub-node to user tables and views so they'll have a
            //  clickable expand sign allowing us to have GetSubObjectHierarchy
            //  called so the user can view the columns
            node.Nodes.Add(new TreeNode());
         }

         return tnRet;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1029</remarks>
      /// <param name="node">...</param>
      /// <returns>...</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         // Show the column breakdown for the selected table

         if (node is OracleNode)
         {
            OracleNode on = (OracleNode)node;
            if ((on._type == Glb.NodeTypes.Tttttt)                             // "T"
                || (on._type == Glb.NodeTypes.View)                            // "V"
                 )
            {
               DataSet ds = _dbClient.ExecuteOnWorker("select COLUMN_NAME name, DATA_TYPE type"
                                                       + ", DATA_LENGTH clength, DATA_PRECISION nprecision"
                                                        + ", DATA_SCALE nscale, NULLABLE nullable"
                                                         + " from USER_TAB_COLUMNS"
                                                          + " where TABLE_NAME = '" + on.Text + "'"
                                                           + " order by name"
                                                            , i_Timeout
                                                             );
               if (ds == null || ds.Tables.Count == 0) { return null; }

               TreeNode[] tn = new OracleNode[ds.Tables[0].Rows.Count];
               int count = 0;

               foreach (DataRow row in ds.Tables[0].Rows)
               {
                  string length;
                  if (row["clength"].ToString() != "")
                  {
                     length = "(" + row["clength"].ToString() + ")";
                  }
                  else if (row["nprecision"].ToString() != "")
                  {
                     length = "(" + row["nprecision"].ToString() + "," + row["nscale"].ToString() + ")";
                  }
                  else
                  {
                     length = "";
                  }

                  string nullable = row["nullable"].ToString().StartsWith("Y") ? "null" : "not null";

                  OracleNode column = new OracleNode(row["name"].ToString()
                                                      + " (" + row["type"].ToString() + length
                                                       + ", " + nullable
                                                        + ")"
                                                         );

                  column._dragText = row["name"].ToString();

                  tn[count++] = column;
               }
               return tn;
            }
         }
         return null;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1516 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1226 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1326 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0936 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0717 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }
   }
}
