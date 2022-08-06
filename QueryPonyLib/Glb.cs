#region Fileinfo
// file        : 20130613°1201 /QueryPony/QueryPonyLib/Globals.cs
// summary     : Class 'Glb' and it's subclasses provide some global constants
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// versions    : 20130613°1201 file newly created
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System; // for AttributeUsage

namespace QueryPonyLib
{
   /// <summary>This class provides some global constants</summary>
   /// <remarks>id : 20130608°0121</remarks>
   public static class Glb
   {
      /// <summary>This subclass provides constants to tell behavioral values (possibly to be exchanged by user settings later)</summary>
      /// <remarks>id : 20130617°1541</remarks>
      public static class Behavior
      {
         /// <summary>This constant '2' tells which server to select on startup in the server checkbox on the Connect Form</summary>
         /// <remarks>
         /// id : 20130620°1611
         /// note : This does not work, the value of Settings.Default.LastSelectedConnection is stronger.
         /// </remarks>
         public static int OnFirststart_SelectConnectionNumber_NOMOREUSED = 2;

         /// <summary>This constant 'false/true' tells whether to show the 'Connection Edited' dialogbox or not</summary>
         /// <remarks>id : 20130707°1041</remarks>
         public static bool Dialogbox_CreateNewConnection = false; // true; // false;

         /// <summary>This constant 'false/true' tells whether the main program window obeys the setting '' or just never maximizes</summary>
         /// <remarks>
         /// id : 20130608°0214
         /// note : The reason for the existence this value is, that during development,
         ///    the default setting of MaximizeMainWindow often got lost somehow, and
         ///    annoyingly the Program then opened maximized.
         /// todo : Eliminate this 'constant', if the program behaviour is no more annoying. (todo 20130723°0911)
         /// </remarks>
         public static bool StartupWindowAllowMaximize = false;
      }

      /// <summary>This subclass provides database specific constants</summary>
      /// <remarks>id : 20130723°0931</remarks>
      public static class DbSpecs
      {
         /// <summary>This const (5984) tells the default port number for CouchDB</summary>
         /// <remarks>id : 20130723°0932</remarks>
         public const uint CouchDefaultPortnum = 5984;

         /// <summary>This const (3306) tells the default port number for MySQL</summary>
         /// <remarks>id : 20130723°0933</remarks>
         public const uint MysqlDefaultPortnum = 3306;

         /// <summary>
         /// This const (7777) tells the default port number for Oracle
         ///  (this maybe just a humble beginning, Oracle has many default portnumbers and ranges).
         /// </summary>
         /// <remarks>id : 20130723°0934</remarks>
         public const uint OracleDefaultPortnum = 7777;

         /// <summary>This const (5432) tells the default port number for PostgreSQL</summary>
         /// <remarks>id : 20130723°0935</remarks>
         public const uint PgsqlDefaultPortnum = 5432;

         /// <summary>This const ":" tells the separator between the url and the portnumber</summary>
         /// <remarks>id : 20130723°0936</remarks>
         public const string sSepaUrlPort = ":";
      }

      /// <summary>This subclass provides constants to tell execution or not-execution of sequences</summary>
      /// <remarks>
      /// id : 20130618°0403
      /// note : This constant identifiers are not declared 'const' but static,
      ///         just to avoid compiler warning 'unreachable code detected'.
      /// </remarks>
      public static class Debag
      {
         /// <summary></summary>
         /// <remarks>Ident 20210524°1311</remarks>
         public static bool Tell_AssemblyLoad_Event = false;

         /// <summary>This const 'false/true' tells whether to fire the debugger if the Connect Button was pressed (toggle this value on demand)</summary>
         /// <remarks>id : 20130704°1241</remarks>
         public static bool Debug_ConnectForm_ButtonConnect = false; // true;

         /// <summary>This const 'false/true' tells whether to fire the debugger if a context menu item is selected (toggle this value on demand)</summary>
         /// <remarks>id : 20130615°1322</remarks>
         public static bool Debug_MainMenu_DoExecuteQuery = false;

         /// <summary>This const 'false/true' tells whether to fire the debugger if a user SQL command will be executed (toggle this value on demand)</summary>
         /// <remarks>id : 20130615°1321</remarks>
         public static bool Debug_MainMenu_DoBrowserAction = false;

         /// <summary>This const 'false' tells not to execute a debug/test sequence currently — Toggle Yes/No on demand</summary>
         /// <remarks>id : 20130608°0122</remarks>
         public static bool Execute_No = false;

         /// <summary>This const 'true' tells to execute a debug/test sequence currently — Toggle Yes/No on demand</summary>
         /// <remarks>id : 20130608°0123</remarks>
         public static bool Execute_Yes = true;

         /// <summary>This const 'false' tells not to show this debug message</summary>
         /// <remarks>id : 20130622°1121</remarks>
         public static bool ShowDebugMessage_FALSE = false;

         /// <summary>This const 'false' tells not to show this debug message</summary>
         /// <remarks>id : 20130622°1122</remarks>
         public static bool ShowDebugMessage_TRUE = true;

         /// <summary>This const 'false/true' tells to skip a sequence of the shutdown MDI feature (possibly to be recovered)</summary>
         /// <remarks>id : 20130619°0923</remarks>
         public static bool ShutdownFeatureMdiWindows = false;
      }

      /// <summary>This constant " - " tells a hyphen surrounded by blanks</summary>
      /// <remarks>id : 20130608°0215 (20010819°2257)</remarks>
      public const string sBlHyBl = " - ";

      /// <summary>This constant " " tells a blank</summary>
      /// <remarks>id : 20130608°0211 (20060313°1319)</remarks>
      public const string sBlnk = " ";

      /// <summary>This constant "\r\n" tells a CRLF (carriage return line feed)</summary>
      /// <remarks>id : 20130608°0212 (20060313°1322)</remarks>
      public const string sCr = "\r\n";

      /// <summary>This constant "\r\n" tells a double 'carriage return line feed'</summary>
      /// <remarks>id : 20130623°0702 (20060313°1323)</remarks>
      public const string sCrCr = "\r\n\r\n";

      /// <summary>This constant "." tells a dot (string type)</summary>
      /// <remarks>id : 20130620°1702 (20010816°1717)</remarks>
      public const string sDot = ".";

      /// <summary>This constant '.' tells a dot (char type)</summary>
      /// <remarks>id : 20130620°1703</remarks>
      public const char cDot = '.';

      /// <summary>This constant "\r\n" tells a CRLF (carriage return line feed)</summary>
      /// <remarks>id : 20130623°0703 (20060313°1326)</remarks>
      public const string sTb = "\t";

      /// <summary>This constant "_" tells an underline</summary>
      /// <remarks>id : 20130608°0216 (20010816°2247)</remarks>
      public const string sUlin = "_";

      /// <summary>This constant "yyyy-MM-dd\\'HH:mm:ss.ff" tells a format string for timestamps</summary>
      /// <remarks>
      /// id : 20130625°0933 (20051109°1917)
      /// note : Various flavours e.g. "yyyy-MM-dd,HH:mm:ss", "yyyy-MM-dd\\'HH:mm:ss.ff".
      /// </remarks>
      public const string sFormat_Timestamp = "yyyy-MM-dd\\'HH:mm:ss.ff";

      /// <summary>This subclass provides constants to tell behavioral values (possibly to be exchanged by user settings later)</summary>
      /// <remarks>id : 20130617°1543</remarks>
      public class Errors
      {
         /// <summary>This constant "Program flow error, theoretically not possible" tells a program flow error string</summary>
         /// <remarks>id : 20130623°0801 (20010817°1847)</remarks>
         public const string TheoreticallyNotPossible = "Program flow error, theoretically not possible";
      }

      /// <summary>This subclass provides constants to tell various hardcoded filenames</summary>
      /// <remarks>id : 20130619°0421</remarks>
      public class Files
      {
         /// <summary>This const "explorer.exe" tells the Explorer program file name</summary>
         /// <remarks>id : 20130619°0422</remarks>
         public const string WindowsExplorer = "explorer.exe";

         /// <summary>This const "notepad.exe" tells the Windows Notepad program file name</summary>
         /// <remarks>id : 20130625°0942</remarks>
         public const string NotepadExe = "notepad.exe";
      }

      /// <summary>This subclass provides constants to tell the available Root Node Items</summary>
      /// <remarks>
      /// id : 20130613°1411
      /// note : This subclass would also fit into IBrowser or *Browser classes. But
      ///    I want collect them in one place to get an overview over the various
      ///    features the different databases sport, and want generalize them.
      /// todo : Use clearly distinctable constants for the differrent usages as
      ///    node-item strings, SLQ filter-token or even others. [todo 20130613°141102]
      /// todo : Possibly integrate this with new class 'Nodes' [todo 20130819°1201]
      /// </remarks>
      public class NodeItems
      {
         /// <summary>This const "Processes" tells a treeview node item text (dummy so far)</summary>
         /// <remarks>id : 20130613°1411</remarks>
         public const string Processes = "Processes";

         /// <summary>This const "Routines" tells a treeview node item text (dummy so far)</summary>
         /// <remarks>id : 20130613°1412</remarks>
         public const string Routines = "Routines";

         /// <summary>This const "System Tables" tells a treeview node item text</summary>
         /// <remarks>id : 20130613°1421</remarks>
         public const string SystemTables = "System Tables";

         /// <summary>This const "System Views" tells a treeview node item text</summary>
         /// <remarks>id : 20130613°1422</remarks>
         public const string SystemViews = "System Views";

         /// <summary>This const "Tables" tells a treeview node item text</summary>
         /// <remarks>id : 20130613°1416</remarks>
         public const string Tables = "Tables";

         /// <summary>This const "Views" tells a treeview node item text</summary>
         /// <remarks>id : 20130613°1418</remarks>
         public const string Views = "Views";
      }

      /// <summary>
      /// This subclass provides constants to tell treeview base node types in
      ///  the 'creation-counter style', means indices into the nodes array.
      /// </summary>
      /// <remarks>
      /// id : 20130614°1511
      /// note : This node types depend on creating the nodes in the exact order!
      /// note : This class helps to analyse the occurrences of the node array
      ///    indices in the code by using the VS 'Find all references' feature.
      /// note : Compare quicknotes.txt paragraph 20130614°1452.
      /// todo : Eliminate this class and replace it by the use of meaningful values,
      ///    e.g. like given in class 20130613°1431 NodeTypes, or by an enumeration
      ///    class. (todo 20130723°1031)
      /// todo : Possibly integrate this with new class 'Nodes' [todo 20130819°120102]
      /// </remarks>
      public class NodeTypeNdxs
      {
         /// <summary>This const (-1) tells the treeview array index for the 'Invalid' node (MySQL/OleDb/SQLite)</summary>
         /// <remarks>id : 20130614°1512</remarks>
         public const int Invalid = -1;

         /// <summary>This const (0) tells the treeview array index for the 'Tables' node (MySQL/OleDb/SQLite)</summary>
         /// <remarks>id : 20130614°1513</remarks>
         public const int Tables0 = 0;

         /// <summary>This const (1) tells the treeview array index for the 'Views' node (MySQL/OleDb/SQLite)</summary>
         /// <remarks>id : 20130614°1514</remarks>
         public const int Views1 = 1;

         /// <summary>This const (2) tells the treeview array index for the 'System Tables' node (MySQL/OleDb/SQLite)</summary>
         /// <remarks>id : 20130614°1515</remarks>
         public const int SystemTables2 = 2;

         /// <summary>This const (3) tells thetreeview array index for the 'System Views' node (MySQL/OleDb/SQLite)</summary>
         /// <remarks>id : 20130614°1516</remarks>
         public const int SystemViews3 = 3;
      }

      /// <summary>This subclass provides constants to tell available treeview node types (MS-SQL/ODBC style)</summary>
      /// <remarks>
      /// id : 20130613°1431
      /// todo : Possibly integrate this with new class 'Nodes' [todo 20130819°120103]
      /// </remarks>
      public class NodeTypes
      {
         /// <summary>This const "CO" tells the TreeView 'Column' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1432</remarks>
         public const string Column = "CO";

         /// <summary>This const "FN" tells the TreeView 'Function' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1433</remarks>
         public const string Function = "FN";

         /// <summary>This const "P" tells the TreeView 'Procedure' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1434</remarks>
         public const string Procedure = "P";

         /// <summary>This const "S" tells the TreeView 'System Table' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1435</remarks>
         public const string SystemTable = "S";

         /// <summary>This const "S" tells the TreeView 'T...' node type for Oracle</summary>
         /// <remarks>id : 20130613°1435</remarks>
         public const string Tttttt = "T";

         /// <summary>This const "U" tells the TreeView 'User Table' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1436</remarks>
         public const string UserTable = "U";

         /// <summary>This const "V" tells the TreeView 'View' node type for MS-SQL and ODBC</summary>
         /// <remarks>id : 20130613°1437</remarks>
         public const string View = "V";
      }

      /// <summary>This subclass provides constants to tell resource filenames</summary>
      /// <remarks>id : 20130619°0301</remarks>
      public class Resources
      {
         /// <summary>This const "QueryPonyGui.docs.agpl-3.0.txt" tells a resource file name</summary>
         /// <remarks>id : 20130724°1611</remarks>
         public const string Agpl = "QueryPonyGui.docs.agpl-3.0.txt";

         /// <summary>This const "QueryPonyGui" tells a resource file name</summary>
         /// <remarks>
         /// id : 20130706°0911
         /// todo : This constant is only used in project QueryPonyGui in the About Form.
         ///    Either shift it there, or eliminate it. The About Form can retrieve it's
         ///    own assembly name at runtime. [todo 20130707°0936]
         /// </remarks>
         public const string AssemblyNameGui = "QueryPonyGui";

         /// <summary>This const "QueryPonyLib" tells a ressource file name</summary>
         /// <remarks>
         /// id : 20130706°0912
         /// todo : This constant was duplicated/outsourced to QueryPonyGui 20130707°0934. In
         ///    this project here it is superfluous, because the assembly knows it's own name.
         ///    Replace the few occurrences with runtime name retrieval. [todo 20130707°0935]
         /// </remarks>
         public const string AssemblyNameLib = "QueryPonyLib";

         /// <summary>This const "QueryPonyGui.docs.authors.txt" tells a resource file name</summary>
         /// <remarks>id : 20130619°0303</remarks>
         public const string Authors = "QueryPonyGui.docs.authors.txt";

         /// <summary>This const "QueryPonyGui.docs.changelog.txt" tells a resource file name</summary>
         /// <remarks>id : 20130619°0302</remarks>
         public const string Changelog = "QueryPonyGui.docs.changelog.txt";

         /// <summary>This const "QueryPonyGui.docs.issues.txt" tells a resource file name</summary>
         /// <remarks>id : 20130619°0304</remarks>
         public const string Issues = "QueryPonyGui.docs.issues.txt";

         /// <summary>This const "joesgarage.sqlite3" tells a resource file name</summary>
         /// <remarks>id : 20130704°1321</remarks>
         public const string JoesgarageSqliteFilename = "joesgarage.sqlite3";

         /// <summary>This const "QueryPonyGui.docs.joesgarage.sqlite3" tells a resource file name</summary>
         /// <remarks>id : 20130704°1312</remarks>
         public const string JoesgarageSqliteResourcename = "QueryPonyGui.docs." + JoesgarageSqliteFilename;

         /// <summary>This const "QueryPonyLib.docs.joespostbox.20130703o1243.sqlite3" tells a resource file name</summary>
         /// <remarks>id : 20130704°1322</remarks>
         ////public const string JoespostboxSqliteFilename = "joespostbox.201307031243.sqlite3";
         public const string JoespostboxSqliteFilename = "joespostbox.20130703o1243.sqlite3"; // [fix 20220731°0951`01]

         /// <summary>This const ~"QueryPonyLib.docs.joespostbox.20130703o1243.sqlite3" tells a resource file name</summary>
         /// <remarks>id : 20130704°1311</remarks>
         public const string JoespostboxSqliteResourcename = "QueryPonyLib.docs." + JoespostboxSqliteFilename;

         /// <summary>This const "docs.license.txt" tells a resource file name</summary>
         /// <remarks>id : 20130619°0305</remarks>
         public const string License = "QueryPonyLib.docs.license.txt";

         /// <summary>This const "QueryPonyGui.docs.summary.txt" tells a resource file name</summary>
         /// <remarks>id : 20130619°0306</remarks>
         public const string Summary = "QueryPonyGui.docs.summary.txt";

         /// <summary>This const "QueryPonyGui.docs.thirdparty.txt" tells a resource file name</summary>
         /// <remarks>id : 20130724°1612</remarks>
         public const string Thirdparty = "QueryPonyGui.docs.thirdparty.txt";
      }

      /// <summary>This subclass provides constants to tell schema filter expressions</summary>
      /// <remarks>
      /// id : 20130614°1131
      /// note : Those strings have originally been used as both ContextMenuItem-text and
      ///        filter-strings. This class shall help to separate the two different usages.
      /// </remarks>
      public class SchemaFilter
      {
         /// <summary>This const "TABLE" tells a schema filter string (no more a TreeView Node Item)</summary>
         /// <remarks>id : 20130613°1415</remarks>
         public const string Table = "TABLE";

         /// <summary>This const "VIEW" tells a schema filter string (no more a TreeView Node Item)</summary>
         /// <remarks>id : 20130613°1417</remarks>
         public const string View = "VIEW";

         /// <summary>This const "SYSTEM TABLE" tells a TreeView Node Item</summary>
         /// <remarks>id : 20130613°1413</remarks>
         public const string SystemTable = "SYSTEM TABLE";

         /// <summary>This const "SYSTEM VIEW" tells a TreeView Node Item</summary>
         /// <remarks>id : 20130613°1414</remarks>
         public const string SystemView = "SYSTEM VIEW";
      }

      /// <summary>This subclass provides constants to tell miscellaneous SQL bricks</summary>
      /// <remarks>id : 20130919°0611</remarks>
      public class Sql
      {
         /// <summary>This const "\"" tells a SQL value quote</summary>
         /// <remarks>id : 20130613°1212</remarks>
         public const string QtVal = "\"";
      }

      /// <summary>This subclass provides constants to tell SQL keywords</summary>
      /// <remarks>id : 20130613°1211</remarks>
      public class SqlKwds
      {
         /// <summary>This const "ALTER" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1212</remarks>
         public const string Alter = "ALTER";

         /// <summary>This const "COLUMN" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1213</remarks>
         public const string Columng = "COLUMN";

         /// <summary>This const "CREATE" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1214</remarks>
         public const string Create = "CREATE";

         /// <summary>This const "DISTINCT" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1215</remarks>
         public const string Distinct = "DISTINCT";

         /// <summary>This const "FROM" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1216</remarks>
         public const string From = "FROM";

         /// <summary>This const "ORDER BY" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1217</remarks>
         public const string Orderby = "ORDER BY";

         /// <summary>This const "SELECT" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1218</remarks>
         public const string Select = "SELECT";

         /// <summary>This const "TABLE" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1219</remarks>
         public const string Table = "TABLE";

         /// <summary>This const "WHERE" tells one SQL keyword</summary>
         /// <remarks>id : 20130613°1220</remarks>
         public const string Where = "WHERE";
      }

      /// <summary>This subclass provides constants to tells the available Browser TreeView Context Menu Items</summary>
      /// <remarks>id : 20130613°1311</remarks>
      public class TvContextMenuItems
      {
         /// <summary>This const "Alter column..." tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1312</remarks>
         public const string AlterColumn = "Alter column...";

         /// <summary>This const "(insert all fields)" tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1313</remarks>
         public const string InsertAllFields = "(insert all fields)";

         /// <summary>This const "(insert all fields, table prefixed)" tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1314</remarks>
         public const string InsertAllFieldsTblPrefixed = "(insert all fields, table prefixed)";

         /// <summary>This const "select * from " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1312</remarks>
         public const string SelectAllFrom = "select * from";

         /// <summary>This const "sp_help " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1315</remarks>
         public const string SpHelp = "sp_help";

         /// <summary>This const "sp_helpconstraint " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1316</remarks>
         public const string SpHelpConstraint = "sp_helpconstraint";

         /// <summary>This const "sp_helpindex " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1317</remarks>
         public const string SpHelpIndex = "sp_helpindex";

         /// <summary>This const "sp_helptrigger " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1318</remarks>
         public const string SpHelpTrigger = "sp_helptrigger";

         /// <summary>This const "View / Modify " tells a Context Menu Item string</summary>
         /// <remarks>id : 20130613°1319</remarks>
         public const string ViewModify = "View / Modify";
      }
   }
}

// [ns 20130618°0516 experimental]
namespace GlobalCustomAttributes
{
   /// <summary>This enum defines values for a custom test assembly attribute</summary>
   /// <remarks>id : 20130618°0512</remarks>
   public enum AssemblyPluginTestType
   {
      Browser,
      Host,
      Library,
      Skins
   }

   /// <summary>This class constitutes a custom test assembly attribute</summary>
   /// <remarks>
   /// id : 20130618°0511
   /// ref : 20130618°0503 'thread custom assembly information'
   /// </remarks>
   [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
   public sealed class AssemblyPluginTestAttribute : Attribute
   {
      /// <summary>This field stores the custom test assembly attribute value</summary>
      /// <remarks>id : 20130618°0513</remarks>
      private readonly AssemblyPluginTestType _type;

      /// <summary>This property gets the custom test assembly attribute value</summary>
      /// <remarks>id : 20130618°0514</remarks>
      public AssemblyPluginTestType PluginType
      {
         get { return _type; }
      }

      /// <summary>This constructor creates the custom test assembly attribute</summary>
      /// <remarks>id : 20130618°0515</remarks>
      public AssemblyPluginTestAttribute(AssemblyPluginTestType type)
      {
         _type = type;
      }
   }

   /// <summary>This class constitutes a custom assembly attribute</summary>
   /// <remarks>
   /// id : 20130618°0521
   /// reference : 20130618°0505 'article creating global attributes in cs' heureka
   /// </remarks>
   public class CustomAuthorsAttribute : Attribute
   {
      /// <summary>This property gets/sets the custome assembly attribute value</summary>
      /// <remarks>id : 20130618°0522</remarks>
      public string Authors { get; set; }
   }
}
