#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/Nodes.cs
// id          : 20130819°1101
// summary     : This file stores nodes class 'Nodes' with subclasses to define various treeview node types.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Infant
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyLib
{
   /// <summary>This class homes subclasses to define various treeview node types.</summary>
   /// <remarks>id : 20130819°1102</remarks>
   public class Nodes
   {

      /// <summary>This subclass defines a Database node.</summary>
      /// <remarks>id : 20130819°1111</remarks>
      public class Database : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the name of this database.</summary>
         /// <remarks>id : 20130819°1112</remarks>
         public string DbName { get; set; }

         /// <summary>This property gets/sets the server of this database.</summary>
         /// <remarks>id : 20130819°1113</remarks>
         public Server DbServer { get; set; }

         /// <summary>This property gets/sets the fields of this table.</summary>
         /// <remarks>id : 20130819°1114</remarks>
         public Table[] DbTables { get; set; }

         /// <summary>This property gets/sets the fields of this table.</summary>
         /// <remarks>id : 20130819°1115</remarks>
         public View[] DbViews { get; set; }
      }


      /// <summary>This subclass defines a Field node.</summary>
      /// <remarks>id : 20130819°1121</remarks>
      public class Fields : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the datatype of this field.</summary>
         /// <remarks>id : 20130819°1122</remarks>
         public string FldDataType { get; set; }

         /// <summary>This property gets/sets the flag whether this field is nullable or not.</summary>
         /// <remarks>id : 20130819°1125</remarks>
         public bool FldIsNullable { get; set; }

         /// <summary>This property gets/sets the flag whether this field is a primary key field or not.</summary>
         /// <remarks>id : 20130825°1304</remarks>
         public bool FldIsPrimary { get; set; }

         /// <summary>This property gets/sets the flag whether this field is a multi-column-primary key field or not.</summary>
         /// <remarks>id : 20130828°1142</remarks>
         public bool FldIsPrimaryMulticol { get; set; }

         /// <summary>This property gets/sets the length of this field.</summary>
         /// <remarks>
         /// id : 20130819°1123
         /// note : Change type to Int64? (see issue 20130828°0922)
         /// </remarks>
         public int FldLength { get; set; }

         /// <summary>This property gets/sets the name of this field.</summary>
         /// <remarks>id : 20130819°1124</remarks>
         public string FldName { get; set; }

         /// <summary>This property gets/sets the table of this field.</summary>
         /// <remarks>id : 20130819°1126</remarks>
         public Table FldTable { get; set; }
      }


      /// <summary>This subclass defines an Index node.</summary>
      /// <remarks>id : 20130819°1131</remarks>
      public class Indices : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the IsPrimary flag of this index.</summary>
         /// <remarks>id : 20130825°1301</remarks>
         public bool NdxIsPrimary { get; set; }

         /// <summary>This property gets/sets the IsPrimary flag of this index.</summary>
         /// <remarks>id : 20130825°1303</remarks>
         public bool NdxIsUnique { get; set; }

         /// <summary>This property gets/sets the fields of this table.</summary>
         /// <remarks>id : 20130825°1302</remarks>
         public string[] NdxFieldnames { get; set; }

         /// <summary>This property gets/sets the fields of this table.</summary>
         /// <remarks>id : 20130819°1132</remarks>
         public Fields[] NdxFields { get; set; }

         /// <summary>This property gets/sets the name of this index.</summary>
         /// <remarks>id : 20130819°1133</remarks>
         public string NdxName { get; set; }

         /// <summary>This property gets/sets the table of this index.</summary>
         /// <remarks>id : 20130819°1134</remarks>
         public Table NdxTable { get; set; }

      }


      /// <summary>This subclass defines a Server node.</summary>
      /// <remarks>id : 20130819°1103</remarks>
      public class Server : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the Databases of this server.</summary>
         /// <remarks>id : 20130819°1104</remarks>
         public Database[] SrvDatabases { get; set; }

         /// <summary>This property gets/sets the URL of this server.</summary>
         /// <remarks>id : 20130819°1105</remarks>
         public string SrvAddress { get; set; }
      }


      /// <summary>This subclass defines a Stored Procedure node.</summary>
      /// <remarks>id : 20130819°1153</remarks>
      public class Storproc : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the statement of this stored procedure.</summary>
         /// <remarks>id : 20130819°1154</remarks>
         public string StprocStatement { get; set; }

         /// <summary>This property gets/sets the Database of this stored procedure.</summary>
         /// <remarks>id : 20130819°1155</remarks>
         public Database StprocDatabase { get; set; }

         /// <summary>This property gets/sets the name of this stored procedure.</summary>
         /// <remarks>id : 20130819°1156</remarks>
         public string StprocName { get; set; }
      }


      /// <summary>This subclass defines a Table node.</summary>
      /// <remarks>id : 20130819°1141</remarks>
      public class Table : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the database of this table.</summary>
         /// <remarks>id : 20130819°1142</remarks>
         public Database TblDatabase { get; set; }

         /// <summary>This property gets/sets the array of fields in this table.</summary>
         /// <remarks>id : 20130819°1143</remarks>
         public Fields[] TblFields { get; set; }

         /// <summary>This property gets/sets the array of indices for this table.</summary>
         /// <remarks>id : 20130819°1144</remarks>
         public Indices[] TblIndices { get; set; }

         /// <summary>This property gets/sets the name of this table.</summary>
         /// <remarks>id : 20130819°1145</remarks>
         public string TblName { get; set; }

      }


      /// <summary>This subclass defines a View node.</summary>
      /// <remarks>id : 20130819°1151</remarks>
      public class View : System.Windows.Forms.TreeNode
      {
         /// <summary>This property gets/sets the database of this table.</summary>
         /// <remarks>id : 20130819°1152</remarks>
         public Database ViewDatabase { get; set; }

         /// <summary>This property gets/sets the fields of this view.</summary>
         /// <remarks>id : 20130819°1157</remarks>
         public Fields[] ViewFields { get; set; }

         /// <summary>This property gets/sets the name of this table.</summary>
         /// <remarks>id : 20130819°1158</remarks>
         public string ViewName { get; set; }

      }

   }
}
