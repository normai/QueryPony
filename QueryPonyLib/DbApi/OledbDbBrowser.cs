#region Fileinfo
// file        : 20130604°0931 (20130605°1701) /QueryPony/QueryPonyLib/DbApi/OledbDbBrowser.cs
// summary     : This file stores class 'OleDbBrowser' to constitute
//                an implementation of IBrowser for OleDb.
// license     : GNU AGPL v3
// copyright   : © 2013 dl3bak@qsl.net
// copyright   : © 2013 - 2021 Norbert C. Maier, dl3bak@qsl.net
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of IBrowser for OleDb</summary>
   /// <remarks>
   /// id : 20130604°0932 (20130605°1702)
   /// note : Access modifier changed from 'internal' to 'public' during refactor 20130619°1311, so
   ///    it can be accessed from eventhandler 20130604°0109 in ConnectForm.cs. [chg 20130619°1341]
   /// </remarks>
   public class OleDbBrowser : IDbBrowser
   {
      /// <summary>This subclass constitutes the TreeNode objects for OleDb tables</summary>
      /// <remarks>id : 20130604°0934 (20130605°1704)</remarks>
      private class OleDbNode : TreeNode
      {
         /// <summary>This field stores the node type (why?)</summary>
         /// <remarks>id : 20130604°0935 (20130605°1705)</remarks>
         internal int _type = Glb.NodeTypeNdxs.Invalid;

         /// <summary>This property gets the DragText if an OleDb treenode is dragged. (20130605°1706)</summary>
         /// <remarks>id : 20130604°0936</remarks>
         internal string _dragText
         {
            get
            {
               string sRet = this.Text;
               // If token (name) contains blank or hyphen, wrap it in squarebrackets [note 20130723°0904]
               sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "[]");
               return sRet;
            }
         }

         /// <summary>This constructor creates a new OleDb table treenode</summary>
         /// <remarks>id : 20130604°0937 (20130605°1707)</remarks>
         /// <param name="sText">The label text to be shown for the node</param>
         /// <param name="iType">The node's type in the enum-like class NodeTypeNdxs format</param>
         public OleDbNode(string sText, int iType) : base(sText)
         {
            this._type = iType;
         }
      }

      /// <summary>This field stores the DbClient given in the constructor</summary>
      /// <remarks>id : 20130604°0933 (20130605°1703)</remarks>
      private DbClient _dbClient;

      /// <summary>This constructor creates a new OleDbBrowser object for the given DbClient</summary>
      /// <remarks>id : 20130604°0938 (20130605°1708)</remarks>
      /// <param name="dbClient">The DbClient for which to create this OleDbBrowser object</param>
      public OleDbBrowser(DbClient dbClient)
      {
         this._dbClient = dbClient;
      }

      /// <summary>This property gets the OldDb DbClient for which this DbBrowser was created</summary>
      /// <remarks>id : 20130604°0939 (20130605°1709)</remarks>
      public DbClient DbClient
      {
         get { return _dbClient; }
      }

      /// <summary>This method delivers a clone of this OleDb DbBrowser for another OleDb DbClient</summary>
      /// <remarks>
      /// id : 20130604°0941 (20130605°1710)
      /// note : What may this method be good for? It seems not be called at all. And it
      ///    seems not necessary to satisfy any interface implementation. (20130720°122106)
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      public IDbBrowser Clone(DbClient newDbClient)
      {
         OleDbBrowser sb = new OleDbBrowser(newDbClient);
         return sb;
      }

      /// <summary>This method creates the context menu for the given OleDb table node</summary>
      /// <remarks>id : 20130604°0945 (20130605°1714)</remarks>
      /// <param name="node">The OleDb treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given OleDb treenode</returns>
      public StringCollection GetActionList(TreeNode node)
      {
         if (! (node is OleDbNode))
         {
            return null;
         }

         OleDbNode on = (OleDbNode)node;
         StringCollection output = new StringCollection();

         if (on._type >= Glb.NodeTypeNdxs.Tables0)                             // 0
         {
            output.Add(Glb.TvContextMenuItems.SelectAllFrom + " " + on._dragText); // "select * from"
            output.Add(Glb.TvContextMenuItems.InsertAllFields);                // "(insert all fields)"
            output.Add(Glb.TvContextMenuItems.InsertAllFieldsTblPrefixed);     // "(insert all fields, table prefixed)"
         }

         return output.Count == 0 ? null : output;
      }

      /// <summary>This method retrieves the command string behind a table node's context menu item</summary>
      /// <remarks>id : 20130604°0946 (20130605°1715)</remarks>
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      public string GetActionText(TreeNode node, string sAction)
      {
         if (! (node is OleDbNode))
         {
            return null;
         }

         OleDbNode on = (OleDbNode)node;
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
               string s = (sb.Length == 0 ? "" : ", ") + prefix + ((OleDbNode)subNode)._dragText;
               chars += s.Length;
               sb.Append(s);
            }
            return sb.Length == 0 ? null : sb.ToString();
         }

         return null;
      }

      /// <summary>This method retrieves a connectionstring from a file</summary>
      /// <remarks>id : 20130604°0948 (20130605°1717)</remarks>
      /// <param name="dbfileName">The file from which to read the connectionstring</param>
      /// <returns>The wanted connectionstring</returns>
      public static string GetConnectString(string sDbFileName)
      {
         string sRet = null;

         if (! System.IO.File.Exists(sDbFileName))
         {
            return sRet;
         }

         string lowerDbFileName = sDbFileName.ToLower();

         if (lowerDbFileName.EndsWith(".connectstring"))
         {
            sRet = getConnectStringFromFileContent(sDbFileName);
         }
         else if (lowerDbFileName.EndsWith(".udl"))
         {
            sRet = getConnectStringFromFileContent(sDbFileName);
         }
         else if (lowerDbFileName.LastIndexOf('.') > 0)
         {
            sRet = getConnectStringForDatabaseFile(sDbFileName);
         }
         else
         {
            // Todo : Supplement error processing. (todo 20130709°0944)
            sRet = null;                                                       // Better than any invalid string?
         }

         return sRet;
      }

      /// <summary>This method retrieves the list of databases available on this server</summary>
      /// <remarks>id : 20130604°0947 (20130605°1716)</remarks>
      /// <returns>The wanted list of databases</returns>
      public string[] GetDatabases()
      {
         string[] result = GetOleDbBrowserValues ( "CATALOG_NAME"
                                                  , OleDbSchemaGuid.Catalogs
                                                   , null
                                                    );
         if (result == null)
         {
            result = new string[] { _dbClient.Database };
         }

         return result;
      }

      /// <summary>This method returns text from the given treenode, suitable for dropping into a query window</summary>
      /// <remarks>
      /// id : 20130604°0944 (20130605°1713)
      /// todo : Streamline usage of dragtext and SqlTokenTicks() throughout the various DbBrowser implmementations. [todo 20130723°0907`05]
      /// </remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      public string GetDragText(TreeNode node)
      {
         string sRet = "";

         if (node is OleDbNode)
         {
            sRet = ((OleDbNode)node)._dragText;

            // Not yet sure whether this is located right here [line 20130719°0933]
            sRet = IOBus.Utils.Strings.SqlTokenTicks(sRet, " -", "``");        // If token contains ' ' or '-' then wrap it with backticks
         }

         return sRet;
      }

      /// <summary>This method builds the initial browser tree for this OleDb DbBrowser</summary>
      /// <remarks>id : 20130604°0942 (20130605°1711)</remarks>
      /// <returns>The wanted array of OleDb table treenodes</returns>
      public TreeNode[] GetObjectHierarchy()
      {
         TreeNode[] top = new TreeNode[]
         {
            new TreeNode (Glb.NodeItems.Tables),                               // Experiment // [0] no more "TABLE" but "Tables"
            new TreeNode (Glb.NodeItems.Views),                                // Experiment // [1] no more "VIEW" but "Views"
            new TreeNode (Glb.NodeItems.Tables),                               // Experiment // [2] no more "SYSTEM TABLE" but "System Tables"
            new TreeNode (Glb.NodeItems.SystemViews)                           // Experiment // [3] no more "SYSTEM VIEW" but "System Views"
         };

         int iCurNodeType = Glb.NodeTypeNdxs.Tables0;                          // Start index (0) of the nodes array
         foreach (TreeNode node in top)
         {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Provisory hardcoded sequence while refactor 'separating node-text and filter-string' [20130614°1122]
            string sFilter = "";
            switch (iCurNodeType)
            {
               case Glb.NodeTypeNdxs.Tables0: sFilter = Glb.SchemaFilter.Table; break; // 0 : "TABLE"
               case Glb.NodeTypeNdxs.Views1: sFilter = Glb.SchemaFilter.View; break; // 1 : "VIEW"
               case Glb.NodeTypeNdxs.SystemTables2: sFilter = Glb.SchemaFilter.SystemTable; break; // 2 : "SYSTEM TABLE"
               case Glb.NodeTypeNdxs.SystemViews3: sFilter = Glb.SchemaFilter.SystemView; break; // 3 : "SYSTEM VIEW"
               default: break;                                                 // Fatal
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CreateNodeHierachy(top, iCurNodeType++, sFilter);                  // , node.Text); // Don't use just the node text as filter [20130614°1121]
         }

         return top;
      }

      /// <summary>This method returns the subtree for the given table/view level treenode</summary>
      /// <remarks>
      /// id : 20130604°0943 (20130605°1712)
      /// note : TreeNode[].Add("SELECT [COLUMN_NAME] FROM [Columns] WHERE [Tablename] = {filter}")
      /// callers : Only GetActionText()
      /// todo : Replace below calls to fetchFieldProperties() with one single call for
      ///    for retrieving all properties, and extract the wanted lists via LINQ. Having
      ///    only one call to retrieve the complete schema, and then extracting the wanted
      ///    values from that will probably save much runtime. [todo 20130819°140102]
      /// </remarks>
      /// <param name="node">The treenode for which to get the subtree</param>
      /// <returns>The wanted array of treenodes or null</returns>
      public TreeNode[] GetSubObjectHierarchy(TreeNode node)
      {
         // Show the column breakdown for the selected table
         if (! (node is OleDbNode))
         {
            return null;
         }

         // Debug [seq 20130825°1212]
         if (IOBus.Gb.Debag.Shutdown_Temporarily)
         {
            Nodes.Table n = new Nodes.Table();
            string sTblDbg = node.Text;                                        // Wrong?
            n.TblName = node.Text;
            ConnSettingsLib csDbg = node.Tag as ConnSettingsLib;
            bool b = GetSubObjectHierarchy2(n);
         }

         // () Retrieve columns
         string[] arFields = GetOleDbBrowserValues ( "COLUMN_NAME"
                                                    , OleDbSchemaGuid.Columns
                                                     , new object[] { GetDatabaseFilter(), null, node.Text }
                                                      );
         if (arFields == null)
         {
            return null;
         }

         // Prepare result array length
         int iNodeArrayLength = arFields.Length;

         // Switch on/off view with or without index nodes
         bool bAlsoAttachIndexNodes = false; // possible outsource to user settings
         Nodes.Indices[] ndxs = null;
         if (bAlsoAttachIndexNodes)
         {
            // () Tetrieve indices [seq 20130825°1411]
            Nodes.Table ndTbl = new Nodes.Table();
            ndTbl.TblName = node.Text;                                         // Provisory filling only
            ndxs = SchemaGetIndices(ndTbl);

            // Adjust result array length
            iNodeArrayLength += ndxs.Length;
         }

         // Prepare return array for the two child node sets columns plus indices
         TreeNode[] tnsRet = new OleDbNode[iNodeArrayLength];
         int iCount = 0;

         // Process columns
         foreach (string sName in arFields)
         {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // [seq 20130819°1301]
            // Here is the place to retrieve the field properties.
            // Here first time the new Nodes class is used.

            Nodes.Server server = new Nodes.Server();
            server.SrvAddress = DbClient.ConnSettings.DatabaseServerAddress;

            Nodes.Database database = new Nodes.Database();
            database.DbName = DbClient.ConnSettings.DatabaseName;
            database.DbServer = server;

            Nodes.Table table = new Nodes.Table();
            table.TblDatabase = database;
            table.TblName = node.Text;

            Nodes.Fields field = new Nodes.Fields();
            field.FldTable = table;
            field.FldName = sName;

            // Retrieve the wanted additional information
            bool b = fetchFieldPropertiesPROVISORY(field);

            string sAppend = " (" + field.FldDataType + (field.FldLength > 0 ? "/" + field.FldLength.ToString() : "") + ")";
            string sLabel = sName + sAppend;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            OleDbNode column = new OleDbNode(sLabel, Glb.NodeTypeNdxs.Invalid);

            tnsRet[iCount++] = column;
         }

         // process indices [seq 20130825°1412]
         if (bAlsoAttachIndexNodes)
         {
            for (int i = 0; i < ndxs.Length; i++)
            {
               OleDbNode nodeNdx = new OleDbNode(ndxs[i].NdxName, Glb.NodeTypeNdxs.Invalid);
               tnsRet[iCount++] = nodeNdx;
            }
         }

         return tnsRet;
      }

      /// <summary>This struct is just a syntax experiment</summary>
      /// <remarks>
      /// id : 20130825°1241
      /// callers : None so far
      /// note : Type DBID needs project reference to 'Microsoft.VisualBasic.Compatibility.Data'
      /// note : Written after ref 20130825°1231 'thread: memory layout of C# OleDb structs'
      /// note : Why this exeriment? I am looking for how to dissect the COLUMN_FLAGS with
      ///    the help of enum DBCOLUMNFLAGSENUM. So how to make the flags visible in .NET?
      ///    By knowing about how to deal with DBCOLUMNINFO, I may come closer to those flags.
      /// </remarks>
      [System.Runtime.InteropServices.StructLayout ( System.Runtime.InteropServices.LayoutKind.Sequential
                                                    , CharSet = System.Runtime.InteropServices.CharSet.Unicode
                                                     , Pack = 1)
      ]
      private struct DBCOLUMNINFO
      {
         public IntPtr pwszName;
         public IntPtr pTypeInfo;
         // iOrdinal is a platform-sized unsigned integer, not a pointer
         public UIntPtr iOrdinal;
         public uint dwFlags;
         public UIntPtr ulColumnSize;
         public ushort wType;
         public byte bPrecision;
         public byte bScale;
      }

      /// <summary>This method supplies the subtree hierarchy to the given table node</summary>
      /// <remarks>
      /// id : 20130819°1515 (20130819°1501)
      /// todo : This method now runs in parallel to the original GetSubObjectHierarchy(),
      ///    just using the new Nodes class instead the old nodes. If it that runs fine,
      ///    I want try to replace the old method. The advantage were the use of the new
      ///    Nodes subclasses, which carry better type and detail information than the
      ///    original nodes. [todo 20130819°1521]
      /// todo : This method is a bit awkward. Perhaps replace it by a method
      ///    analogous 20130825°1325 SchemaGetIndices(). [todo 20130826°1321]
      /// callers : Only method 20130818°1537 cloneTable() (not yeet GetActionText())
      /// ref : 20130825°1233 'thread: get primary key'
      /// note : Sequence 20130825°1251 'retrieve primary key fields' was shifted
      ///    to method 20130826°1201 schemaGetPrimarykeys_ARCHIVAL().
      /// note : Sequence 20130825°1253 'retrieve available collections' was shifted
      ///    to method 20130826°1225 SchemaGetCollections().
      /// note : (debug 20130825°1221) I abandoned the experiments to extract single
      ///    flags from the DbColumnFlags. As far as I see from the flag names, they do
      ///    not contain a primary key info anyway. For the flag names, see e.g. ref
      ///    20130825°1232 'msdn: OleDb structs and enums'. //DBCOLUMNFLAGSENUM
      /// </remarks>
      /// <param name="node">The table node to be supplemented with a subtree hierarchy</param>
      /// <returns>Success flag</returns>
      public bool GetSubObjectHierarchy2(Nodes.Table node)
      {
         // (B) Prerequisites
         DataTable datatableCols = null;
         OleDbConnection con = ((OledbDbClient)DbClient).Connection;
         object[] oRestrict = null;
         string sDb = GetDatabaseFilter(); // seems to be null always

         // (C) Retrieve the indices [seq 20130825°1252]
         // We need to call SchemaGetIndices() to have the primary keys cache filled
         Nodes.Indices[] ndxs = SchemaGetIndices(node);
         string[] primkeys = null;
         if (_schemaTablePrimaryKeys.ContainsKey(node.TblName))
         {
            primkeys = _schemaTablePrimaryKeys[node.TblName];
         }

         // (D) Retrieve the fields [seq 20130819°1522]
         // issue : The fields array is sorted alphabetically, but we want it physically [issue 20130826°1322]
         // (D.1) Set criteria for table info
         Guid guid = (Guid)OleDbSchemaGuid.Columns;
         oRestrict = new object[] { sDb                                        // Catalog, may be null
                                   , null                                      // Owner
                                    , node.TblName                             // TableName, e.g. 'Addresses'
                                     , null                                    // TableType
                                      };

         // (D.2) Retrieve column schema for this table
         datatableCols = con.GetOleDbSchemaTable(guid, oRestrict);

         // (D.3) Debug — Analyse result table
         if (IOBus.Gb.Debag.Execute_Yes)
         {
            datatableCols.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_oledb_columns.xml");
         }

         // Linq - sort result after the table's physical field order [seq 20130828°0911]
         // Note : Above GetOleDbSchemaTable() seems to deliver the DataTable only
         //    sorted alphabetically after column names. I saw no sort option anywhere.
         EnumerableRowCollection<DataRow> rows = from x in datatableCols.AsEnumerable()
                                                  orderby x.Field<Int64>("ORDINAL_POSITION")
                                                   select x
                                                    ;

         // We need the rows in a DataTable [seq 20130828°0912]
         // Issue : This transmformation is only necessary, because below code is not adjusted.
         //    With a littel adjustion below, it should be superfluous. (issue 20130828°0913)
         DataTable dtOrdered = rows.CopyToDataTable();
         dtOrdered.TableName = "Hello"; // (avoid exception 20130828°0914)

         // (D.x) Debug — Analyse result table
         if (IOBus.Gb.Debag.Execute_Yes)
         {
            // If property dt.TableName is not set, then WriteXml thows InvalidOperationException 'Cannot
            //  serialize the DataTable. DataTable name is not set.' (exception seen 20130828°0914)
            dtOrdered.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_oledb_columns_ORDPOS.xml");
         }

         // (D.4) Loop over all rows (each describig a column) of the given table
         for (int iRow = 0; iRow < dtOrdered.Rows.Count; iRow++)
         {
            string sDbg = ""; Int16 i16Dbg = 0; Int32 i32Dbg = 0; Int64 i64Dbg = 0; Guid guidDbg;

            // (D.4.1) Comfort variable
            DataRow dr = dtOrdered.Rows[iRow];

            // (D.4.2) Read field properties [seq 20130819°1523]
            // note : (remember exception 20130828°0921) InvalidCastException 'Unable
            //    to cast object of type "System.DBNull' to type 'System.String'." with
            //    a line 'sDbg = (string)dr.ItemArray[0];'.
            sDbg = (dr.ItemArray[0].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[0] : "";           // 0 TABLE_CATALOG
            sDbg = (dr.ItemArray[1].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[1] : "";           // 1 TABLE_SCHEMA
            String sTableDBG = (string)dr.ItemArray[2];                                                    // 2 TABLE_NAME (may be casted because we are sure it is never DBNull)
            String sColName = (string)dr.ItemArray[3];                                                     // 3 COLUMN_NAME (sure it is never DBNull)
            guidDbg = (dr.ItemArray[4].GetType() != typeof(DBNull)) ? (Guid)dr.ItemArray[4] : Guid.Empty;  // 4 COLUMN_GUID
            i64Dbg = (dr.ItemArray[5].GetType() != typeof(DBNull)) ? (Int64)dr.ItemArray[5] : -1;          // 5 COLUMN_PROPID
            Int64 iPosition = (Int64)dr.ItemArray[6];                                                      // 6 ORDINAL_POSITION (sure it is never DBNull)
            Boolean bHasDefaultDBG = (Boolean)dr.ItemArray[7];                                             // 7 COLUMN_HASDEFAULT (sure it is never DBNull)
            sDbg = dr.ItemArray[8].ToString();                                                             // 8 COLUMN_DEFAULT String
            Int64 iColFlagsDBG = (Int64)dr.ItemArray[9];                                                   // 9 COLUMN_FLAGS (sure it is never DBNull)
            Boolean bIsNullable = (Boolean)dr.ItemArray[10];                                               // 10 IS_NULLABLE (sure it is never DBNull)
            Int32 iDataType = (Int32)dr.ItemArray[11];                                                     // 11 DATA_TYPE (sure it is never DBNull)
            guidDbg = (dr.ItemArray[12].GetType() != typeof(DBNull)) ? (Guid)dr.ItemArray[12] : Guid.Empty; // 12 TYPE_GUID
            Int64 iCharMaxLen = (dr.ItemArray[13].GetType() != typeof(DBNull)) ? (Int64)dr.ItemArray[13] : 0; //  13 CHARACTER_MAXIMUM_LENGTH
            i64Dbg = (dr.ItemArray[14].GetType() != typeof(DBNull)) ? (Int64)dr.ItemArray[14] : -1;         // 14 CHARACTER_OCTET_LENGTH Int64
            i32Dbg = (dr.ItemArray[15].GetType() != typeof(DBNull)) ? (Int32)dr.ItemArray[15] : -1;         // 15 NUMERIC_PRECISION Int32
            i16Dbg = (dr.ItemArray[16].GetType() != typeof(DBNull)) ? (Int16)dr.ItemArray[16] : ((Int16) (-1)); // 16 NUMERIC_SCALE Int16
            i64Dbg = (dr.ItemArray[17].GetType() != typeof(DBNull)) ? (Int64)dr.ItemArray[17] : -1;        // 17 DATETIME_PRECISION Int64
            sDbg = (dr.ItemArray[18].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[18] : "";         // 18 CHARACTER_SET_CATALOG String
            sDbg = (dr.ItemArray[19].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[19] : "";         // 19 CHARACTER_SET_SCHEMA String
            sDbg = (dr.ItemArray[20].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[20] : "";         // 20 CHARACTER_SET_NAME String
            sDbg = (dr.ItemArray[21].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[21] : "";         // 21 COLLATION_CATALOG String
            sDbg = (dr.ItemArray[22].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[22] : "";         // 22 COLLATION_SCHEMA String
            sDbg = (dr.ItemArray[23].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[23] : "";         // 23 COLLATION_NAME String
            sDbg = (dr.ItemArray[24].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[24] : "";         // 24 DOMAIN_CATALOG String
            sDbg = (dr.ItemArray[25].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[25] : "";         // 25 DOMAIN_SCHEMA String
            sDbg = (dr.ItemArray[26].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[26] : "";         // 26 DOMAIN_NAME String
            sDbg = (dr.ItemArray[27].GetType() != typeof(DBNull)) ? (string)dr.ItemArray[27] : "";         // 27 DESCRIPTION String

            // (D.4.3) Adjust datatype
            string sDataType = fetchFieldProperties_DataType((OleDbType)iDataType);

            // (D.4.4) Create field node
            Nodes.Fields nField = new Nodes.Fields();

            // (D.4.5) Fill field node
            nField.Text = sColName;
            nField.FldDataType = sDataType;

            // issue : Without the explicit cast, we get compiler error "Cannot
            //    implicitly convert type 'long' to 'int'." Perhaps rather adjust
            //    type of 'FldLength' to Int64? (issue 20130828°0922)
            nField.FldLength = (int)iCharMaxLen;

            nField.FldName = sColName;
            nField.FldIsNullable = bIsNullable;
            nField.FldTable = null;

            // Lookup and set primkey flag
            if (primkeys != null)
            {
               if (Array.IndexOf(primkeys, sColName) > -1)
               {
                  nField.FldIsPrimary = true;
               }
               if (primkeys.Length > 1)
               {
                  nField.FldIsPrimaryMulticol = true;
               }
            }

            // (D.4.6) Add fieldnode
            node.Nodes.Add(nField);
         }

         return true;
      }

      /// <summary>This subclass experimentally provides a DataTable buffer to try to improve performance</summary>
      /// <remarks>id : 20130819°1351</remarks>
      private class SchemaBuffer_EXPERIMENTAL
      {
         /// <summary>This field stores the DataTable to be buffered</summary>
         /// <remarks>id : 20130819°1352</remarks>
         public DataTable Datatable;

         /// <summary>This field stores the catalog to be compared against</summary>
         /// <remarks>id : 20130819°1353</remarks>
         public string Catalog;

         /// <summary>This field stores the tablename to be compared against</summary>
         /// <remarks>id : 20130819°1354</remarks>
         public string TableName;

         /// <summary>This field stores the guid to be compared against</summary>
         /// <remarks>id : 20130819°1355</remarks>
         public Guid Guid;
      }

      /// <summary>This field stores the working instance of the schema table buffer</summary>
      /// <remarks>id : 20130819°1356</remarks>
      private static SchemaBuffer_EXPERIMENTAL _schemaBuffer = null;

      /// <summary>This method fills in details about the given field</summary>
      /// <remarks>id : 20130819°1311</remarks>
      private bool fetchFieldPropertiesPROVISORY(Nodes.Fields field)
      {
         string sServer = field.FldTable.TblDatabase.DbServer.SrvAddress;
         string sDatabase = field.FldTable.TblDatabase.DbName;

         OleDbConnection con = ((OledbDbClient)DbClient).Connection;

         string sDb = GetDatabaseFilter();

         Guid guid = (Guid)OleDbSchemaGuid.Columns;

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // [seq 20130819°1356]
         // summary : Improve performance by using a schema table buffer.
         // note : This improfes performance from about 30 seconds to about 2 seconds.
         //    Interestingly, the time to expand a table node is noticeable related
         //    to the amount of fields it will show. So this field details retrieving
         //    method seems to play an important roll for the expansion time.
         // todo : Two seconds are still too slow. There must be more to improve,
         //    perhaps in the caller, who already requested a pretty identical schema.
         //    [todo 20130819°1401]

         DataTable datatable = null;

         // Test whether to use buffer or not
         bool bUseBuffer = false;
         if (_schemaBuffer != null)
         {
            if (     (_schemaBuffer.Catalog == sDb)
                &&   (_schemaBuffer.Guid == guid)
                 &&  (_schemaBuffer.TableName == field.FldTable.TblName)
                  )
            {
               bUseBuffer = true;
            }
         }

         // Retrieve schema, either from buffer or from new creation
         if (bUseBuffer)
         {
            // Reuse existing schema
            datatable = _schemaBuffer.Datatable;
         }
         else
         {
            // Set criteria
            object[] oRestrict = new object[] { sDb                            // Catalog
                                               , null                          // Owner
                                                , field.FldTable.TblName       // Table, e.g. 'Addresses'
                                                 , null                        // TableType
                                                  };
            // Retrieve result DataTable
            datatable = con.GetOleDbSchemaTable(guid, oRestrict);

            // Fill/refresh buffer
            if (_schemaBuffer == null)
            {
               _schemaBuffer = new SchemaBuffer_EXPERIMENTAL();
            }
            _schemaBuffer.Datatable = datatable;
            _schemaBuffer.Catalog = sDb;
            _schemaBuffer.Guid = guid;
            _schemaBuffer.TableName = field.FldTable.TblName;
         }
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         if (IOBus.Gb.Debag.Execute_No)
         {
            datatable.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_oledb.xml");
         }

         // Retrieve field details from schema table
         if (IOBus.Gb.Debag. Shutdown_Alternatively)
         {
            // Algorithm one [seq 20130819°1321] proof-of-concept

            for (int iRow = 0; iRow < datatable.Rows.Count; iRow++)
            {
               DataRow dr = datatable.Rows[iRow];

               string sTable = dr.ItemArray[2].ToString();                     // TABLE_NAME
               string sColName = dr.ItemArray[3].ToString();                   // COLUMN_NAME
               int iPosition = Int32.Parse(dr.ItemArray[6].ToString());        // ORDINAL_POSITION
               bool bHasDefault = Boolean.Parse(dr.ItemArray[7].ToString());   // COLUMN_HASDEFAULT
               int iColFlags = Int32.Parse(dr.ItemArray[9].ToString());        // COLUMN_FLAGS
               bool bIsNullable = Boolean.Parse(dr.ItemArray[10].ToString());  // IS_NULLABLE
               int iDataType = Int32.Parse(dr.ItemArray[11].ToString());       // DATA_TYPE
               string sCharMaxLen = dr.ItemArray[13].ToString();               // CHARACTER_MAXIMUM_LENGTH

               // (.) Fill in MaxLen
               int iCharMaxLen = 0;
               try
               {
                  iCharMaxLen = Int32.Parse(sCharMaxLen);
               }
               catch { }

               if (sTable == field.FldTable.TblName)                           // Should always be true
               {
                  if (sColName == field.FldName)
                  {
                     // (.1) Fill in DataType
                     field.FldDataType = fetchFieldProperties_DataType((OleDbType)iDataType);

                     // (.2) Fill in MaxLen
                     field.FldLength = iCharMaxLen;

                     // (.3) Fill in Nullable
                     field.FldIsNullable = bIsNullable;
                  }
               }
            }
         }
         else
         {
            // Algorithm two [seq 20130819°1331]
            // note : ref 20130819°1332 'totaldotnet: linq to dataset and datatable'
            // note : rows is of type System.Data.EnumerableRowCollection<System.Data.DataRow>

            // Linq
            var rows = from x in datatable.AsEnumerable()
                       where x.Field<string>("COLUMN_NAME") == field.FldName
                        select x;
            DataRow row = System.Linq.Enumerable.First(rows);

            // (.1) Fill in DataType
            int iDataType = Int32.Parse(row.ItemArray[11].ToString());

            field.FldDataType = fetchFieldProperties_DataType((OleDbType)iDataType);

            // (.2) Fill in MaxLen
            int iMaxLen = 0;
            try
            {
               iMaxLen = Int32.Parse(row.ItemArray[13].ToString());
            }
            catch { }
            // Curiously, type Boolean always shows maxlen two, reset this zero
            if ((OleDbType)iDataType == OleDbType.Boolean)
            {
               iMaxLen = 0;
            }
            field.FldLength = iMaxLen;

            // (.3) Fill in Nullable
            field.FldIsNullable = Boolean.Parse(row.ItemArray[10].ToString());
         }

         return true;
      }

      /// <summary>This method translates the OleDb native datatype int to a useful string</summary>
      /// <remarks>
      /// id : 20130819°1341
      /// ref : 20130819°0951 'msdn: GetSchema column data types'
      /// </remarks>
      /// <param name="dbtype">The datatype in the Windows native enum format</param>
      /// <returns>The wanted datatype string</returns>
      private string fetchFieldProperties_DataType(OleDbType dbtype)
      {
         string sRet = "";

         switch (dbtype)
         {
            case OleDbType.BigInt: sRet = "BigInt"; break;
            default: sRet = dbtype.ToString(); break;
         }

         return sRet;
      }

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>
      /// id : 20130826°1225 (20130826°1211)
      /// todo : Make this called from somewhere, e.g. from a Database Node context menu. [todo 20130826°1231]
      /// </remarks>
      /// <returns>The wanted array of available collections</returns>
      public string[] SchemaGetCollections()
      {
         string[] collections = null;

         // Retrieve available collections [seq 20130825°1253]
         // Experiment after ref 20130825°1234 'msdn: provider-specific schema collections'
         // Finding : For the Pardox database, this yields the following collections:
         //    Columns, DataSourceInformation, DataTypes, Indexes, MetaDataCollections
         //    Procedures, ReservedWords, Restrictions, Tables, Views. Note, that
         //    'Primary_Keys' is not mentioned.
         // Note : This basic information will become interesting if we may see, that different
         //    Jet Enging data providers for diffent databases provide different collections.
         //    Any task like 'extract all available information' will start with this call.
         if (IOBus.Gb.Debag.Shutdown_Temporarily)
         {
            OleDbConnection con = ((OledbDbClient)DbClient).Connection;
            DataTable dt = con.GetSchema();
            dt.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_collections.xml");
         }

         return collections;
      }

      /// <summary>This field stores the cached result of the first index schema retrieval</summary>
      /// <remarks>id : 20130825°1331</remarks>
      private DataTable _schemaTableIndexes = null;

      /// <summary>This field stores a helper dictionary with primary key fields</summary>
      /// <remarks>
      /// id : 20130826°1313
      /// note : I want such helper var because (1) the key field names shall be easily
      ///    available when building the field list of a table (2) the keyfields are
      ///    known only when retrieving the indices, which is an expensive operation.
      ///    Thus a comfortable cache is wanted for the key fields of all tables.
      /// note : This cache is filled from method 20130825°1325 SchemaGetIndices()
      ///    and read from method 20130819°1515 GetSubObjectHierarchy2().
      /// </remarks>
      private System.Collections.Generic.Dictionary<string, string[]> _schemaTablePrimaryKeys = new System.Collections.Generic.Dictionary<string, string[]>();

      /// <summary>This interface method retrieves the array of Index Nodes for the given table</summary>
      /// <remarks>id : 20130825°1325 (20130825°1311)</remarks>
      /// <param name="sTablename">The tablename for the indices to retrieve</param>
      /// <returns>The wanted array of Index Nodes</returns>
      public Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable)
      {
         Nodes.Indices[] ndxs = null;
         string sTablename = ndTable.TblName;

         // Provide full indexes schema table
         if (_schemaTableIndexes == null)
         {
            // Preparations
            string sDb = GetDatabaseFilter(); // seems to be null always
            OleDbConnection con = ((OledbDbClient)DbClient).Connection;

            // Retrieve schema
            Guid guidIdxs = (Guid)OleDbSchemaGuid.Indexes;
            object[] oRestrict = new object[] { sDb                            // Catalog, may be null
                                               , null                          // Owner
                                                , null // node.TblName         // TableName, e.g. 'Addresses' <= then it yields an empty result
                                                 , null                        // TableType
                                                  };
            // (C.2) Retrieve column schema for this table
            _schemaTableIndexes = con.GetOleDbSchemaTable(guidIdxs, oRestrict);

            if (IOBus.Gb.Debag.Execute_Yes)
            {
               _schemaTableIndexes.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_indices.xml");
            }
         }

         // Linq
         var rows = from x in _schemaTableIndexes.AsEnumerable()
                    where x.Field<string>("TABLE_NAME") == sTablename
                    select x;

         // Prepare primary keys array
         string[] primkeys = null;

         // Build the wanted list of indices
         // Note : This were perhaps done more elegant with Linq, but let's start with an old fashioned loop.
         foreach (DataRow dr in rows)
         {
            // Read the field properties [seq 20130825°1332]
            // Note : For a more tight type handling compare sequence 20130819°1523
            //
            // 0                                                               // 0 TABLE_CATALOG String
            // 1                                                               // 1 TABLE_SCHEMA String
            // string sTablename = (string)dr.ItemArray[2];                    // 2 TABLE_NAME String
            // 3                                                               // 3 INDEX_CATALOG  String
            // 4                                                               // 4 INDEX_SCHEMA  String
            String sIndexname = (string)dr.ItemArray[5];                       // 5 INDEX_NAME  String
            Boolean bPrimaryKey = (Boolean)dr.ItemArray[6];                    // 6 PRIMARY_KEY Boolean
            Boolean bUnique = (Boolean)dr.ItemArray[7];                        // 7 UNIQUE Boolean
            Boolean bClustered = (Boolean)dr.ItemArray[8];                     // 8 CLUSTERED Boolean
            Int32 iDataType = (Int32)dr.ItemArray[9];                          // 9 TYPE Int32
            Int32 iFillFactor = (Int32)dr.ItemArray[10];                       // 10 FILL_FACTOR Int32
            Int32 iInitialSize = (Int32)dr.ItemArray[11];                      // 11 INITIAL_SIZE Int32
            Int32 iNulls = (Int32)dr.ItemArray[12];                            // 12 NULLS Int32
            Boolean bSortBookmark = (Boolean)dr.ItemArray[13];                 // 13 SORT_BOOKMARKS Boolean
            Boolean bAutoUpdate = (Boolean)dr.ItemArray[14];                   // 14 AUTO_UPDATE Boolean
            Int32 iNullCollation = (Int32)dr.ItemArray[15];                    // 15 NULL_COLLATION Int32
            Int64 iOrdinalPosition = (Int64)dr.ItemArray[16];                  // 16 ORDINAL_POSITION Int64
            String sColumnName = (string)dr.ItemArray[17];                     // 17 COLUMN_NAME String
            // 18                                                              // 18 COLUMN_GUID Guid
            // 19                                                              // 19 COLUMN_PROPID Int64
            Int16 iCollation = (Int16)dr.ItemArray[20];                        // 20 COLLATION Int16
            Decimal dCardinality = (Decimal)dr.ItemArray[21];                  // 21 CARDINALITY Decimal
            Int32 iPages = (Int32)dr.ItemArray[22];                            // 22 PAGES Int32
            // 23                                                              // 23 FILTER_CONDITION String
            Boolean bIntegrated = (Boolean)dr.ItemArray[24];                   // 24 INTEGRATED Boolean

            if (ndxs == null)
            {
               // Very first indexname/fieldname
               ndxs = new Nodes.Indices[] { new Nodes.Indices() };
               ndxs[0].NdxName = sIndexname;
               ndxs[0].NdxFieldnames = new string[] { (iOrdinalPosition.ToString() + sColumnName) };
               ndxs[0].NdxIsPrimary = bPrimaryKey;
               ndxs[0].NdxIsUnique = bUnique;
               ndxs[0].Name = sIndexname;
               ndxs[0].Text = sIndexname;
            }
            else
            {
               // Search indexname
               bool bFound = false;
               for (int i = 0; i < ndxs.Length; i++)
               {
                  if (ndxs[i].NdxName != sIndexname)
                  {
                     continue;
                  }
                  // Found, has already fieldname(s)
                  string[] ss = ndxs[i].NdxFieldnames;
                  Array.Resize(ref ss, ss.Length + 1);
                  ss[ss.Length - 1] = iOrdinalPosition.ToString() + sColumnName;
                  ndxs[i].NdxFieldnames = ss;
                  bFound = true;
               }
               if (! bFound)
               {
                  // New indexname, new fieldname
                  Array.Resize(ref ndxs, ndxs.Length + 1);
                  ndxs[ndxs.Length - 1] = new Nodes.Indices();
                  ndxs[ndxs.Length - 1].NdxName = sIndexname;
                  ndxs[ndxs.Length - 1].NdxFieldnames = new string[] { (iOrdinalPosition.ToString() + sColumnName) };
                  ndxs[ndxs.Length - 1].NdxIsPrimary = bPrimaryKey;
                  ndxs[ndxs.Length - 1].NdxIsUnique = bUnique;
                  ndxs[ndxs.Length - 1].Name = sIndexname;
                  ndxs[ndxs.Length - 1].Text = sIndexname;
               }
            }

            // Todo : More properties from above can be drawn out of the conditions to here. [todo 20130828°1143]
            // Note : In method 20130828°1111 tableCreate_sqlCreateIndex(), the table name is no
            //    more given as string but read from Table Node. So we need to supplement that here.
            ndxs[ndxs.Length - 1].NdxTable = ndTable;

            // Process possible primary key field [seq 20130826°1314]
            // Note : We build this array here before below field sorting sequence. Has this
            //    any implications, or is it correct? Is below sorting sequence superfluous?
            if (bPrimaryKey)
            {
               if (primkeys == null)
               {
                  primkeys = new string[] { sColumnName };
               }
               else
               {
                  Array.Resize(ref primkeys, primkeys.Length + 1);
                  primkeys[primkeys.Length - 1] = sColumnName;
               }
            }

         }

         // Sort fieldnames of each index after ordinal position
         // Note : In the test, I found the fieldnames already always ordered correctly. Perhaps this is superfluous?
         for (int iNode = 0; iNode < ndxs.Length; iNode++)
         {
            string[] ss = ndxs[iNode].NdxFieldnames;
            Array.Sort(ss);
            for (int i = 0; i < ss.Length; i++)
            {
               while (Char.IsDigit(ss[i].Substring(0,1), 0))
               {
                  ss[i] = ss[i].Substring(1, ss[i].Length - 1);
               }
            }
            ndxs[iNode].NdxFieldnames = ss;
         }

         // Fill primary keys cache [seq 20130826°1315]
         if (primkeys != null)
         {
            if (! _schemaTablePrimaryKeys.ContainsKey(sTablename))
            {
               _schemaTablePrimaryKeys.Add(sTablename, primkeys);
            }
         }

         return ndxs;
      }

      /// <summary>This method retrieves an *experimental* schema object for debug purposes</summary>
      /// <remarks>
      /// id : 20130819°0935 (20130819°0921)
      /// callers : None.
      /// note : This method was initially called from CloneDb.cs, but that call is
      ///    replaced by a call to [20130819°1515] GetSubObjectHierarchy2(). The
      ///    initial idea was to provide one single method for various schema request,
      ///    but now I rather implement the various concret requests as dedicated
      ///    sequences. Probably GetOleDbBrowserValues() with it's complicated
      ///    parameters had a similar intention. Let's see, which of the ways will
      ///    turn out to be the most convenient. [note 20130820°2111]
      /// ref : 20130720°1233 'MSDN, ADO.NET Schema Restrictions'
      /// ref : 20130819°0951 'MSDN: GetSchema column data types'
      /// ref : 20130819°0952 'MSDN: GetSchema column flags'
      /// ref : 20130819°0953 'Stackoverflow: get column metadata'
      /// </remarks>
      /// <returns>The wanted schema object, e.g. a DataTable or a XML table</returns>
      public object SchemaGetSchema()
      {
         object o = null;

         OleDbConnection con = null;
         try
         {
            con = ((OledbDbClient)DbClient).Connection;
         }
         catch (Exception ex)
         {
            string s = ex.Message;
            return ex;
         }

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // [seq 20130819°0942] Compare sequence 20130608°0111
         if (! Glb.Debag.Execute_No)
         {
            string sDb = GetDatabaseFilter();

            object[] oRestrict = new object[] { sDb                            // Catalog
                                               , null                          // Owner
                                                , "Addresses"                  // Table
                                                 , null                        // TableType
                                                  };

            Guid guidSchema = (Guid)OleDbSchemaGuid.Tables;
            object[] oRestricts = new object[] { sDb, null, "Addresses", null };

            DataTable dtDbg1 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null);
            DataTable dtDbg2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);
            //  DataTable dtDbg2 = con.GetSchema("", null);
            DataTable dtDbg3 = con.GetOleDbSchemaTable(guidSchema, oRestricts);
            DataTable dtDbg4 = con.GetSchema("Tables", null);

            dtDbg1.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema1.xml");
            dtDbg2.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema2.xml");
            dtDbg3.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema3.xml");
            dtDbg4.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema4.xml");
         }

         return o;
      }

      /// <summary>This method retrieves the primary keys of the given table (not applicable)</summary>
      /// <remarks>
      /// id : 20130826°1201
      /// note : The sequence after ref 20130825°1233 'thread: get primary key' is
      ///    shifted here from method 20130819°1515 GetSubObjectHierarchy2() to be
      ///    archived. With the Paradox tables, the algorithm does not work as expected,
      ///    but throws exceptions. Perhaps it work with other Jet Enging data providers.
      /// note : The primary keys now are retrieved together with the other indices.
      /// </remarks>
      private void schemaGetPrimarykeys_ARCHIVAL()
      {
         // (B) Retrieve primary key fields [seq 20130825°1251]
         // This attempt just does not work as expected. But syntax is fine.
         if (IOBus.Gb.Debag.Shutdown_Forever)
         {
            // (B.1) Set criteria for table info
            OleDbConnection con = ((OledbDbClient)DbClient).Connection;
            string sDb = GetDatabaseFilter(); // seems to be null always
            Guid guidPrims = (Guid)OleDbSchemaGuid.Primary_Keys;
            object[] oRestrict = new object[] { sDb                            // Catalog, may be null
                                               , null                          // Owner
                                                , ""           // node.TblName // TableName, e.g. 'Addresses'
                                                 , null                        // TableType
                                                  };

            // (B.2) Retrieve column schema for this table
            // This fails with OleDbException 'The parameter is incorrect.'
            DataTable datatablePrim = con.GetOleDbSchemaTable(guidPrims, oRestrict);

            // After ref 20130825°1233 'thread: get primary key'
            // This fails with ArgumentException 'The requested collection (IndexColumns) is not defined.'
            DataTable primaryKeys = con.GetSchema("IndexColumns", new string[] { sDb, null, "", null });
         }
      }

      /// <summary>This method retrieves the list of tables in this DbBrowser's DbClient</summary>
      /// <remarks>
      /// id : 20130819°0716 (20130819°0701)
      /// todo : This method shares code/functionality with method 20130605°1722
      ///    CreateNodeHierachy(). Possibly streamline the metadata retrieval into
      ///    methods like GetTables(), GetFields(), GetViews() etc. and use those
      ///    in GetObjectHierarchy() and the like. [todo 20130819°0911]
      /// note : The filter value may be e.g. "Tables", "Views", ...
      // ref : 20130720°1233 'msdn: ADO.NET Schema Restrictions'
      // ref : 20130825°1235 'Microsoft: Retrieve column schema by DataReader'
      /// </remarks>
      /// <returns>The wanted list of tables</returns>
      public string[] SchemaGetTables()
      {
         string[] ssTables = null;
         string sFilter = sFilter = Glb.SchemaFilter.Table;
         object[] ooRestrictions = new object[] { GetDatabaseFilter(), null, null, sFilter };
         ssTables = GetOleDbBrowserValues ( "TABLE_NAME"
                                           , OleDbSchemaGuid.Tables
                                            , ooRestrictions
                                             );
         return ssTables;
      }

      #region Implementation Helpers

      /// <summary>This method is an implementation helper ...</summary>
      /// <remarks>
      /// id : 20130604°0954 (20130605°1722)
      /// note : top[curNodeType].Add("SELECT [TABLE_NAME] FROM [Tables] WHERE [Tabletyp] = {filter}")
      /// callers : Only GetObjectHierarchy()
      /// </remarks>
      /// <param name="top">The treenodes array, to one of it's nodes shall a subtree be added</param>
      /// <param name="curNodeType">The pointer into the treenodes array, pointing to the treenode where subtree shall be added</param>
      /// <param name="filter">The filter string corresponding to the treenode type to be processed, e.g. 'TABLE', 'VIEW'</param>
      private void CreateNodeHierachy(TreeNode[] top, int iCurNodeType, string sFilter)
      {
         string[] arTablenames = GetOleDbBrowserValues
                                ( "TABLE_NAME"
                                 , OleDbSchemaGuid.Tables
                                  , new object[] { GetDatabaseFilter(), null, null, sFilter }
                                   );

         if (arTablenames == null)
         {
            return;
         }

         foreach (string sTablename in arTablenames)
         {
            OleDbNode node = new OleDbNode(sTablename, iCurNodeType);

            top[iCurNodeType].Nodes.Add(node);

            // Add a dummy sub-node to user tables and views so they'll have a clickable expand sign
            //  allowing us to have GetSubObjectHierarchy called so the user can view the columns
            node.Nodes.Add(new TreeNode());
         }
      }

      /// <summary>This method opens the database file through OleDb</summary>
      /// <remarks>
      /// id : 20130604°0949 (20130605°1718)
      /// note : Example HandleCmdLineParameterOpenDbFile("Northwind.mdb")
      //     The file "QExpress.mdb.ConnectTemplate" contains a string.Format
      //     for the OleDbConnectString with {0} as placeholder for the filename.
      /// </remarks>
      /// <param name="dbfileName">...</param>
      /// <returns>...</returns>
      private static string getConnectStringForDatabaseFile(string dbfileName)
      {
         int fileTypPos = dbfileName.LastIndexOf('.') + 1;
         string templateFileName = "QExpress." + dbfileName.Substring(fileTypPos)
                                  + ".ConnectTemplate"
                                   ;

         string sConnectTemplate = string.Empty;;

         // load Template from working or exe-directory
         string sPathCurrDir = Path.Combine(Directory.GetCurrentDirectory(), templateFileName);
         string sPathExec = Path.Combine(IOBus.Utils.Pathes.ExecutableFullFolderName(), templateFileName);
         if ( (Utils.ReadFromFile(sPathCurrDir, out sConnectTemplate))
             || (Utils.ReadFromFile(sPathExec, out sConnectTemplate))
              )
         {
            return string.Format(sConnectTemplate, dbfileName);
         }
         else
         {
            string msg = string.Format ( "OleDb-ConnectTemplate-file \"{0}\" not found.\n\n"
                                        + "It contains Information for opening Databasefile \"{1}\""
                                         , templateFileName
                                          , dbfileName
                                           );
            System.Windows.Forms.MessageBox.Show(msg);
         }
         return null;
      }

      /// <summary>
      /// This method returns the connectionstring to open a database through an UDL
      ///  or connect file. It reads *.connect; *.udl and *.dsn filename extensions.
      /// </summary>
      /// <remarks>
      /// id : 20130604°0951 (20130605°1719)
      /// note : Filename extensions
      ///
      ///          ; *.dsn = Microsofts ODBC-File Format
      ///          [ODBC]
      ///          DRIVER=Driver do Microsoft Access (*.mdb)
      ///          FIL=MS Access
      ///          DBQ=D:\Eigene Dateien\Eigene Datenbanken\Northwind.mdb
      ///
      ///          ; *.udl = Microsofts OleDB-File Format
      ///          [oledb]
      ///          ; Everything after this line is an OLEDB initstring
      ///          Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Eigene Dateien\Eigene Datenbanken\Northwind.mdb;Persist Security Info=False
      ///
      ///          ; *.connect = QueryExpress-specific Connect format
      ///          Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Eigene Dateien\Eigene Datenbanken\Northwind.mdb;Persist Security Info=False
      ///
      /// </remarks>
      /// <param name="dbfileName">The filename of the file, the OleDb connectionstring shall be taken from.</param>
      /// <returns>The wanted OleDb connectionstring</returns>
      private static string getConnectStringFromFileContent(string sFilename)
      {
         string fileContent = string.Empty;
         string sResult = string.Empty;
         if (Utils.ReadFromFile(Path.Combine(
                                    Directory.GetCurrentDirectory()
                                     , sFilename)
                                      , out fileContent)
                                       )
         {
            string[] lines = fileContent.Split('\n', '\r');
            foreach (string line in lines)
            {
               if ((line.Trim() != string.Empty)
                   && (!line.Trim().StartsWith(";"))
                    && (!line.Trim().StartsWith("["))
                     )
               {
                  if (sResult != string.Empty)
                  {
                     sResult += ";";
                  }
                  sResult += line;
               }
            }

         }
         else
         {
            // [seq 20130719°081202]
            string sErr = "Error with file " + sFilename;
            System.Windows.Forms.MessageBox.Show(sErr, sFilename);
         }

         return sResult;
      }

      /// <summary>This method is an implementation helper, it delivers the database name of this DbClient</summary>
      /// <remarks>id : 20130604°0953 (20130605°1721)</remarks>
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

      /// <summary>This method is an implementation helper to perform the OleDb-specific internal query</summary>
      /// <remarks>
      /// id : 20130604°0955 (20130605°1723)
      /// note : The OleDb-specific internal query looks like this:
      ///
      ///           SELECT {resultColumnName}
      ///           FROM {schema}
      ///           WHERE {restrictions}
      ///
      /// </remarks>
      /// <param name="resultColumnName">The wanted result column name</param>
      /// <param name="schema">The OleDb-specific schema Guid object</param>
      /// <param name="restrictions">The restriction objects array</param>
      /// <returns>String array with fields from the result table</returns>
      private string[] GetOleDbBrowserValues ( string sResultColName           // E.g. "TABLE_NAME"
                                              , Guid schema
                                               , object[] restrictions
                                                )
      {
         OleDbConnection con = null;
         DataTable tab = null;
         string[] ssRet = null;

         try
         {
            con = ((OledbDbClient)DbClient).Connection;
            tab = con.GetOleDbSchemaTable ( schema                             // I.e. OleDbSchemaGuid.Columns or .Tables
                                           , restrictions                      // I.e. new object[] {null, null, null, "TABLE"}
                                            );

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Testing GetOleDbSchemaTable() [sequence 20130608°0111]
            if (IOBus.Gb.Debag.Execute_Yes)
            {
               Guid guidSchema = (Guid)OleDbSchemaGuid.Tables;
               string sDb = GetDatabaseFilter();
               object[] oRestricts = new object[] { sDb, null, "Addresses", null };
               DataTable dtDbg1 = con.GetOleDbSchemaTable(guidSchema, oRestricts);
               DataTable dtDbg2 = con.GetSchema("Tables", null);
               dtDbg1.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema5.xml");
               dtDbg2.WriteXml(InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "debug_schema6.xml");
            }

            // [note 20130608°0113]
            // An SQL 'CREATE TABLE' statement as we can get with SQLite seems not to be
            //  available with OleDb. All table information always just looks like this:
            //   <Tables>
            //     <TABLE_NAME>Addresses</TABLE_NAME>
            //     <TABLE_TYPE>TABLE</TABLE_TYPE>
            //     <DATE_CREATED>2013-05-28T23:18:08+02:00</DATE_CREATED>
            //     <DATE_MODIFIED>2013-05-28T23:18:08+02:00</DATE_MODIFIED>
            //   </Tables>

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            DataColumn col = tab.Columns[sResultColName];                      // ["TABLE_TYPE"];

            ssRet = new string[tab.Rows.Count];
            int count = 0;
            foreach (DataRow row in tab.Rows)
            {
               ssRet[count++] = row[col].ToString().Trim();
            }
         }
         catch (Exception)
         {
         }
         finally
         {
            if (tab != null)
            {
               tab.Dispose();
            }
         }
         return ssRet;
      }

      #endregion Implementation Helpers

   }

   //==========================================================
   /*

   issue 20130826°1331 'index schema wrong'
   Title : With one specific table, which has in Paradox defined
      a primary key on field no 1, the OleDb index schema is wrong.
      Instead indicating a primary key field, it tells a curious
      index name, and no primary key property instead.
   Note : Here a the snippet from the index schema. The first index
      is wrong as described, the second shows a correct index.
      //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      // <?xml version="1.0" standalone="yes"?>
      // <DocumentElement>
      //   <Indexes>
      //     <TABLE_NAME>_Internetpartner-Merging-Excel_</TABLE_NAME>
      //     <INDEX_NAME>_Internetpartner-Merging-Exc</INDEX_NAME>
      //     <PRIMARY_KEY>false</PRIMARY_KEY>
      //     <UNIQUE>true</UNIQUE>
      //     <CLUSTERED>false</CLUSTERED>
      //     <TYPE>1</TYPE>
      //     <FILL_FACTOR>100</FILL_FACTOR>
      //     <INITIAL_SIZE>4096</INITIAL_SIZE>
      //     <NULLS>0</NULLS>
      //     <SORT_BOOKMARKS>false</SORT_BOOKMARKS>
      //     <AUTO_UPDATE>true</AUTO_UPDATE>
      //     <NULL_COLLATION>4</NULL_COLLATION>
      //     <ORDINAL_POSITION>1</ORDINAL_POSITION>
      //     <COLUMN_NAME>Fid</COLUMN_NAME>
      //     <COLLATION>1</COLLATION>
      //     <CARDINALITY>4</CARDINALITY>
      //     <PAGES>1</PAGES>
      //     <INTEGRATED>true</INTEGRATED>
      //   </Indexes>
      //   <Indexes>
      //     <TABLE_NAME>Accttrans</TABLE_NAME>
      //     <INDEX_NAME>Accttrans#PX</INDEX_NAME>
      //     <PRIMARY_KEY>true</PRIMARY_KEY>
      //     <UNIQUE>true</UNIQUE>
      //     <CLUSTERED>true</CLUSTERED>
      //     <TYPE>1</TYPE>
      //     <FILL_FACTOR>100</FILL_FACTOR>
      //     <INITIAL_SIZE>4096</INITIAL_SIZE>
      //     <NULLS>0</NULLS>
      //     <SORT_BOOKMARKS>false</SORT_BOOKMARKS>
      //     <AUTO_UPDATE>true</AUTO_UPDATE>
      //     <NULL_COLLATION>4</NULL_COLLATION>
      //     <ORDINAL_POSITION>1</ORDINAL_POSITION>
      //     <COLUMN_NAME>Acctid</COLUMN_NAME>
      //     <COLLATION>1</COLLATION>
      //     <CARDINALITY>0</CARDINALITY>
      //     <PAGES>1</PAGES>
      //     <INTEGRATED>true</INTEGRATED>
      //   </Indexes>
      //   ...
      //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
   solution :
   status : ?
   note :
   ܀

   ref 20130825°1235 'Microsoft: Retrieve column schema by DataReader'
   title : Article 'How To Retrieve Column Schema by Using the DataReader GetSchemaTable Method and Visual C# .NET'
   url : http://support.microsoft.com/kb/310107
   usage : Method 20130819°1515 GetSubObjectHierarchy2()
   note : This describes an alternative method to retrieve column properties, not
      via Connection.GetOleDbSchemaTable() but via OleDbDataReader.GetSchemaTable().
   note :
   ܀

   ref 20130825°1234 'msdn: provider-specific schema collections'
   title : Article 'Understanding the Provider-Specific Schema Collections'
   url : http://msdn.microsoft.com/en-us/library/ms254969%28v=vs.85%29.aspx
   usage : Method 20130819°1515 GetSubObjectHierarchy2()
   note :
   ܀

   ref 20130825°1233 'thread: get primary key'
   title : Thread 'GetSchema and PrimaryKeys - getting primary key column name'
   url : http://social.msdn.microsoft.com/Forums/en-US/d58687c9-bb35-4f5f-ba81-52cd7fceab70/getschema-and-primarykeys-getting-primary-key-column-name
   usage : Method 20130819°1515 GetSubObjectHierarchy2()
   note :
   ܀

   ref 20130825°1232 'msdn: OleDb structs and enums'
   title : Article 'OLE DB Structures and Enumerated Types'
   url : http://msdn.microsoft.com/en-us/library/ms716934.aspx
   usage : Method 20130819°1515 GetSubObjectHierarchy2()
   note : This page tells e.g.:
      // typedef DWORD DBCOLUMNFLAGS;
      // enum DBCOLUMNFLAGSENUM {
      //    DBCOLUMNFLAGS_ISBOOKMARK,
      //    DBCOLUMNFLAGS_MAYDEFER,
      //    DBCOLUMNFLAGS_WRITE,
      //    DBCOLUMNFLAGS_WRITEUNKNOWN,
      //    DBCOLUMNFLAGS_ISFIXEDLENGTH,
      //    DBCOLUMNFLAGS_ISNULLABLE,
      //    DBCOLUMNFLAGS_MAYBENULL,
      //    DBCOLUMNFLAGS_ISLONG,
      //    DBCOLUMNFLAGS_ISROWID,
      //    DBCOLUMNFLAGS_ISROWVER,
      //    DBCOLUMNFLAGS_CACHEDEFERRED,
      //    DBCOLUMNFLAGS_SCALEISNEGATIVE,
      //    DBCOLUMNFLAGS_RESERVED,
      //    DBCOLUMNFLAGS_ISROWURL,
      //    DBCOLUMNFLAGS_ISDEFAULTSTREAM,
      //    DBCOLUMNFLAGS_ISCOLLECTION,
      //    DBCOLUMNFLAGS_ISSTREAM,
      //    DBCOLUMNFLAGS_ISROWSET,
      //    DBCOLUMNFLAGS_ISROW,
      //    DBCOLUMNFLAGS_ROWSPECIFICCOLUMN
      // };
   note : Just informal
   ܀

   ref 20130825°1231 'thread: memory layout of C# OleDb structs'
   Title : Thread 'Problem with memory layout of arrays of C# structs with 64-bit COM interop (OLE DB)'
   URL : http://social.msdn.microsoft.com/Forums/vstudio/pt-BR/f341fba6-c0a3-471c-9aec-13406af0e3a7/problem-with-memory-layout-of-arrays-of-c-structs-with-64bit-com-interop-ole-db
   Usage : Experimental struct OledbDbBrowser.DBCOLUMNINFO 20130825°1241
   Note : Interesting background about 64-bit issue.
   ܀

   issue 2013081°0941 'oledb schema retrieval'
   Question : What is the difference between GetSchema() and GetOleDbSchemaTable()?
   Answer :
   Status : ?
   ܀

   */
   //==========================================================
}
