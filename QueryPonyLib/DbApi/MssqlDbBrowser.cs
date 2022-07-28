#region Fileinfo
// file        : 20130604°0701 /QueryPony/QueryPonyLib/DbApi/MssqlDbBrowser.cs
// summary     : This file stores class 'MssqlDbBrowser' to constitute
//                an implementation of IDbBrowser for MS-SQL.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SyDa = System.Data;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of IBrowser for MS-SQL Server</summary>
   /// <remarks>
   /// id : 20130604°0702
   /// note : (20130604°070202) There are different implementations for
   ///         ODBC (<see cref="ODBCBrowser"/>), OLEDB (<see cref="OledbBrowser"/>),
   ///         Oracle (<see cref="OracleBrowser"/>), MsSqlServer (<see cref="SqlBrowser"/>).
   /// </remarks>
   internal class MssqlDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for MS-SQL tables</summary>
      /// <remarks>id : 20130604°0703</remarks>
      class MssqlNode : TreeNode
      {
         /// <summary>This internal field stores the node type (why?)</summary>
         /// <remarks>id : 20130604°0717</remarks>
         internal string _type = "";

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0718</remarks>
         internal string _name;

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0651</remarks>
         internal string _owner;

         /// <summary>This internal field stores ...</summary>
         /// <remarks>id : 20130604°0652</remarks>
         internal string _safeName;

         /// <summary>This internal field stores the DragText if a MS-SQL treenode is dragged</summary>
         /// <remarks>id : 20130604°0653</remarks>
         internal string _dragText;

         /// <summary>This constructor creates a new MS-SQL table treenode</summary>
         /// <remarks>id : 20130604°0716</remarks>
         /// <param name="text">...</param>
         public MssqlNode(string sText) : base(sText)
         {
         }
      }

      /// <summary>This private const (5) tells the milliseconds timeout to use with ...</summary>
      /// <remarks>id : 20130604°0704</remarks>
      private const int i_Timeout = 5;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0705</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new MssqlDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130604°0706</remarks>
      /// <param name="dbClient">The DbClient for which to create this MssqlDbBrowser object</param>
      public MssqlDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the MS-SQL DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130604°0707</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this MS-SQL DbBrowser for another MS-SQL DbClient</summary>
      /// <remarks>
      /// id : 20130604°0715
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122103)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient dbclientNew)
      {
         MssqlDbBrowser sb = new MssqlDbBrowser(dbclientNew);
         return sb;
      }

      /// <summary>This method creates the context menu for the given MS-SQL table node</summary>
      /// <remarks>id : 20130604°0712</remarks>
      /// <param name="node">The MS-SQL treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given MS-SQL treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is MssqlNode))
         {
            return null;
         }

         MssqlNode sn = (MssqlNode)node;
         StringCollection output = new StringCollection();

         if (   sn._type == Glb.NodeTypes.UserTable                            // "U"
             ||  sn._type == Glb.NodeTypes.SystemTable                         // "S"
              || sn._type == Glb.NodeTypes.View                                // "V"
               )
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + sn._dragText); // "select * from"
            output.Add(Glb.TvContextMenuItems.SpHelp + " "+ sn._safeName);     // "sp_help"
            if (sn._type != Glb.NodeTypes.View)                                //  "V"
            {
               output.Add(Glb.TvContextMenuItems.SpHelpIndex + " " + sn._safeName); // "sp_helpindex"
               output.Add(Glb.TvContextMenuItems.SpHelpConstraint + " " + sn._safeName); // "sp_helpconstraint"
               output.Add(Glb.TvContextMenuItems.SpHelpTrigger + " " + sn._safeName); // "sp_helptrigger"
            }
            output.Add(Glb.TvContextMenuItems.InsertAllFields);                // "(insert all fields)"
            output.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);     // "(insert all fields, table prefixed)"
         }

         if (    sn._type == Glb.NodeTypes.View                                // "V"
             ||  sn._type == Glb.NodeTypes.Procedure                           // "P"
              || sn._type == Glb.NodeTypes.Function                            // "FN"
                )
         {
            output.Add(Glb.TvContextMenuItems.ViewModify + " " + sn._name);    // "View / Modify"
         }

         if (   sn._type == Glb.NodeTypes.Column                               // "CO"
             && ((MssqlNode)sn.Parent)._type == Glb.NodeTypes.UserTable        // "U"
              )
         {
            output.Add(Glb.TvContextMenuItems.AlterColumn);                    // "alter column..."
         }

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130604°0713</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode treenode, string sAction)
      {
         // paranoia
         if (! (treenode is MssqlNode))
         {
            return null;
         }

         MssqlNode tnMssql = (MssqlNode)treenode;

         if (sAction.StartsWith(Glb.TvContextMenuItems.SelectAllFrom) || sAction.StartsWith("sp_")) // "select * from"
         {
            return sAction;
         }

         if (sAction.StartsWith("(insert all fields"))
         {
            StringBuilder sb = new StringBuilder();

            // If the table-prefixed option has been selected, add the table name to all the fields
            string prefix = sAction == Glb.TvContextMenuItems.InsertAllFields ? "" : tnMssql._safeName + "."; // "(insert all fields)"

            int chars = 0;
            foreach (TreeNode subNode in GetSubObjectHierarchy(treenode))
            {
               if (chars > 50)
               {
                  chars = 0;
                  sb.Append("\r\n");
               }
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((MssqlNode)subNode)._dragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         if (sAction.StartsWith(Glb.TvContextMenuItems.ViewModify))            // "View / Modify"
         {
            SyDa.DataSet ds = _dbClient.ExecuteOnWorker("sp_helptext [" + tnMssql._dragText + "]", i_Timeout);
            if (ds == null || ds.Tables.Count == 0)
            {
               return null;
            }

            StringBuilder sb = new StringBuilder();
            bool altered = false;
            foreach (SyDa.DataRow row in ds.Tables[0].Rows)
            {
               string line = row[0].ToString();
               if (! altered && line.Trim().ToUpper().StartsWith("CREATE"))
               {
                  sb.Append(Glb.SqlKwds.Alter + line.Trim().Substring(6, line.Trim().Length - 6) + "\r\n"); // "ALTER"
                  altered = true;
               }
               else
               {
                  sb.Append(line);
               }
            }
            return sb.ToString().Trim();
         }

         if (sAction == Glb.TvContextMenuItems.AlterColumn)                    // "alter column..."
         {
            return "alter table " + ((MssqlNode)tnMssql.Parent)._dragText + " alter column " + tnMssql._safeName + " ";
         }

         return null;
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130604°0714</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         // Cool, but only supported in SQL Server 2000+
         SyDa.DataSet ds = _dbClient.ExecuteOnWorker("dbo.sp_MShasdbaccess", i_Timeout);

         // Works in SQL Server 7...
         if (ds == null || ds.Tables.Count == 0)
         {
            ds = _dbClient.ExecuteOnWorker("select name from master.dbo.sysdatabases order by name", i_Timeout);
         }

         if (ds == null || ds.Tables.Count == 0)
         {
            return null;
         }

         string[] sa = new string[ds.Tables[0].Rows.Count];
         int count = 0;
         foreach (SyDa.DataRow row in ds.Tables[0].Rows)
         {
            sa[count++] = row[0].ToString().Trim();
         }
         return sa;
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>
      /// id : 20130604°0711
      /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout
      ///    the various DbBrowser implmementations. (todo 20130723°090702)
      /// </remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = "";

         if (node is MssqlNode)
         {
            sRet = ((MssqlNode)node)._dragText;

            // If token contains blank or hyphen, wrap it e.g. in squarebrackets (20130723°0904)
            sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "[]");
         }

         return sRet;
      }

      /// <summary>This method builds the initial browser tree for this MS-SQL DbBrowser</summary>
      /// <remarks>id : 20130604°0708</remarks>
      /// <returns>The wanted array of MS-SQL table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] top = new TreeNode[]
         {
            new TreeNode ("User Tables"),
            new TreeNode ("System Tables"),
            new TreeNode ("Views"),
            new TreeNode ("User Stored Procs"),
            new TreeNode ("MS Stored Procs"),
            new TreeNode ("Functions")
         };

         string Query;
         //Query = "select type, ObjectProperty (id, N'IsMSShipped') shipped"
         //       + ", object_name(id) object, user_name(uid) owner"
         //        + " from sysobjects"
         //         + " where type in (N'U', N'S', N'V', N'P', N'FN')"
         //          + " order by object, owner"
         //           ;
         Query = "select table_type as type, table_name as object, table_schema"
                + " " + "as [schema]"
                 + " " + "from INFORMATION_SCHEMA.TABLES"
                  + " UNION"
                   + " select routine_type, routine_name, routine_schema"
                    + " " + "from INFORMATION_SCHEMA.ROUTINES"
                     ;

         SyDa.DataSet ds = _dbClient.ExecuteOnWorker(Query, i_Timeout);
         if (ds == null || ds.Tables.Count == 0) { return null; }

         foreach (SyDa.DataRow row in ds.Tables[0].Rows)
         {
            string type = row["type"].ToString();
            int position;
            switch (type)
            {
               case "BASE TABLE":
                  type = Glb.NodeTypes.UserTable;                              // "U"
                  position = 0;
                  break;
               case "VIEW":
                  type = Glb.NodeTypes.View;                                   // "V"
                  position = 2;
                  break;
               case "PROCEDURE":
                  type = Glb.NodeTypes.Procedure;                              // "P"
                  position = 3;
                  break;
               case "FUNCTION":
                  type = Glb.NodeTypes.Function                                // "FN"
                  ; position = 5;
                  break;
               default:
                  type = Glb.NodeTypes.SystemTable;                            // "S"
                  position = 1;
                  break;
            }

            //if (type == "U") position = 0;                                   // user table      - U
            //else if (type == "S") position = 1;                              // system table    - S
            //else if (type == "V") position = 2;                              // view            - V
            //else if (type == "FN") position = 5;                             // function        - FN
            //else if ((int)row["shipped"] == 0) position = 3;                 // user stored proc
            //else position = 4;                                               // MS stored proc

            string prefix = row["schema"].ToString(); // == "dbo" ? "" : row["owner"].ToString() + ".";
            MssqlNode node = new MssqlNode(row["object"].ToString() + " (" + prefix + ")"); // new SqlNode(prefix + row["object"].ToString());
            node._type = type;
            node._name = row["object"].ToString();
            node._owner = row["schema"].ToString();

            // If the object name contains a space, wrap the "safe name" in square brackets
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
            top[position].Nodes.Add(node);

            // add a dummy sub-node to user tables and views so they'll have a clickable expand sign
            //  allowing us to have GetSubObjectHierarchy called so the user can view the columns
            if (type == Glb.NodeTypes.UserTable                                     // "U"
                || type == Glb.NodeTypes.View                                          // "V"
                 )
            {
               node.Nodes.Add(new TreeNode());
            }
         }
         return top;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0709</remarks>
      /// <param name="node">The treenode for which to retrieve subnodes</param>
      /// <returns>The array of wanted treenodes.</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         // Show the column breakdown for the selected table
         if (node is MssqlNode)
         {
            MssqlNode sn = (MssqlNode)node;

            // Break down columns for user tables and views
            if (sn._type == Glb.NodeTypes.UserTable                            // "U"
                || sn._type == Glb.NodeTypes.View                              // "V"
                 )
            {
               SyDa.DataSet ds = _dbClient.ExecuteOnWorker ( "select COLUMN_NAME name, DATA_TYPE type"
                                                       + ", CHARACTER_MAXIMUM_LENGTH clength, NUMERIC_PRECISION nprecision"
                                                        + ", NUMERIC_SCALE nscale, IS_NULLABLE nullable"
                                                         + " from INFORMATION_SCHEMA.COLUMNS"
                                                          + " where TABLE_CATALOG = db_name()"
                                                           + " and TABLE_SCHEMA = '" + sn._owner + "'"
                                                            + " and TABLE_NAME = '" + sn._name + "'"
                                                             + " order by ORDINAL_POSITION"
                                                              , i_Timeout
                                                               );
               if (ds == null || ds.Tables.Count == 0) { return null; }

               TreeNode[] tn = new MssqlNode[ds.Tables[0].Rows.Count];
               int count = 0;

               foreach (SyDa.DataRow row in ds.Tables[0].Rows)
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

                  MssqlNode column = new MssqlNode(row["name"].ToString() + " ("
                                                    + row["type"].ToString() + length + ", " + nullable + ")"
                                                     );
                  column._type = Glb.NodeTypes.Column;                         // "CO" column
                  column._dragText = row["name"].ToString();
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
         return null;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>id : 20130819°1512 (20130819°1501)</remarks>
      /// <param name="node">The node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         return true;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1222 (20130826°1211)</remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;
         return collections;
      }

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1322 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         return ndxs;
      }

      /// <summary>This method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0932 (20130819°0921)</remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;
         return o;
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0713 (20130819°0701)</remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] tables = null;
         return tables;
      }
   }
}
