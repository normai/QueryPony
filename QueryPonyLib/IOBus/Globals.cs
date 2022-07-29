#region FileInfo
// file        : 20111109°1041 /QueryPony/IOBus/Globals.cs
// summary     : Class 'Gb' defines cross-project global constants
// license     : GNU AGPL v3
// copyright   : © 2011 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// versions    : 20121215°2102 20130102°1715
// note        :
// callers     :
#endregion

using System;

// [ns 20111105°1811]
namespace IOBus
{
   /// <summary>This static class defines cross-project global constants</summary>
   /// <remarks>
   /// id : 20111109°1042
   /// note : This constants are not related to the IOBus functionality, but of cross-project interest.
   /// </remarks>
   public static class Gb
   {
      /// <summary>This class provides some text bricks</summary>
      /// <remarks>id : 20130824°1311</remarks>
      public static class Bricks
      {
         /// <summary>This constant "`" tells the backtick used for quoting names in SQL statements</summary>
         /// <remarks>id : 20130824°1312</remarks>
         public const string Backtick = "`";

         /// <summary>This constant tells "\\" for a backslash</summary>
         /// <remarks>id : 20060313°1318</remarks>
         public const string Bs = "\\";

         /// <summary>This constant tells ":" for a colon</summary>
         /// <remarks>id : 20060313°1345</remarks>
         public const string Co = ":";

         /// <summary>This constant tells ": " for a colon plus blank</summary>
         /// <remarks>id : 20060313°1324</remarks>
         public const string CoSp = ": ";

         /// <summary>This constant tells "\r\n" for a carriage return line feed</summary>
         /// <remarks>
         /// id : 20060313°1322
         /// todo : Fetch this value from the .NET Environment.NewLine property? This were
         ///    nice for some places, for other places perhaps not so. [todo 20130113°1541]
         /// </remarks>
         public const string Cr = "\r\n";

         /// <summary>This constant tells "\r\n\r\n" for a double linebreak</summary>
         /// <remarks>
         /// id : 20060313°1323
         /// todo : Possibly fetch value from the .NET Environment.NewLine property. (todo 20130113°154102)
         /// </remarks>
         public const string CrCr = "\r\n\r\n";

         /// <summary>This constant tells "=" for an equation sign</summary>
         /// <remarks>id : 20060313°1320</remarks>
         public const string Eq = "=";

         /// <summary>This constant "<N/A>" tells the text brick for 'not available'</summary>
         /// <remarks>id : 20100109°2243</remarks>
         public const string NotAvailBracketed = "<N/A>";

         /// <summary>This constant ("\\") tells the delimiter between the folders in a path</summary>
         /// <remarks>id : 20130520°1441 (after 20010907°1927)</remarks>
         public const string PathBkslsh = "\\";

         /// <summary>This constant ("/") tells a slash-delimiter between folders in a path</summary>
         /// <remarks>id : 20130605°1431</remarks>
         public const string PathSlash = "/";

         /// <summary>This constant tells "\"" for a double quote</summary>
         /// <remarks>id : 20060313°1325</remarks>
         public const string Qt = "\"";

         /// <summary>This constant tells " " for a blank</summary>
         /// <remarks>id : 20060313°1319</remarks>
         public const string Sp = " ";

         /// <summary>This constant tells " = " for blank-equation-blank</summary>
         /// <remarks>id : 20100109°2242</remarks>
         public const string SpEqSp = " = ";

         /// <summary>This constant tells "\t" for a tab</summary>
         /// <remarks>id : 20060313°1326</remarks>
         public const string Tb = "\t";
      }

      /// <summary>This class provides helper flags for development</summary>
      /// <remarks>
      /// id : 20130821°1611
      /// note : This constants are not declared 'const' but 'static' just to avoid
      ///    compiler warnings 'Unreachable code detected'.
      /// </remarks>
      public static class Debag
      {
         /// <summary>This constant tells 'false' for temporarily shutdown sequences</summary>
         /// <remarks>id : 20111109°0911</remarks>
         public static bool Execute_No = false;

         /// <summary>This constant tells 'true' for temporarily shutdown but then re-opened sequences</summary>
         /// <remarks>id : 20111109°0912</remarks>
         public static bool Execute_Yes = true;

         /// <summary>This constant tells 'false' for shutdown but not deleted sequences</summary>
         /// <remarks>id : 20060313°1303</remarks>
         public static bool Shutdown_Anyway = false;

         /// <summary>This const 'false' tells to skip a sequence which is archived and shall remain under syntax control</summary>
         /// <remarks>id : 20130712°1501</remarks>
         public static bool Shutdown_Archived = false;

         /// <summary>This constant 'false' tells the flag for a sequence to be shutdown temporarily</summary>
         /// <remarks>id : 20130217°1451</remarks>
         public static bool Shutdown_Alternatively = false;

         /// <summary>This const 'false' tells to skip a sequence because it is shutdown permanently</summary>
         /// <remarks>id : 20130619°0921</remarks>
         public static bool Shutdown_Forever = false;

         /// <summary>This const 'false' tells to skip a sequence because it is be temporarily shutdown</summary>
         /// <remarks>id : 20130117°1142</remarks>
         public static bool Shutdown_Temporarily = false;

         /////// <summary>This constant tells 'false' for temporarily shutdown sequences</summary>
         /////// <remarks>id : 20060313°1302</remarks>
         ////public static bool Shutdown_To_Open_Again = false;                // Not used

         /// <summary>This constant tells 'false' for shutdown but not deleted sequences</summary>
         /// <remarks>id : 20130116°1721</remarks>
         public static bool Shutdown_To_Recycle = false;
      }

      /// <summary>This class stores elements around value formatting</summary>
      /// <remarks>id : 20131201°0841</remarks>
      public static class Formats
      {
         /// <summary>This enum defines the three SQLite Datetime formatting options</summary>
         /// <remarks>id : 20131201°0851</remarks>
         public enum SqlitDatetypeStorage
         {
            /// <summary>This enum value indicates the option to store the SQLite Datetime as an Integer</summary>
            /// <remarks>id : 20131201°0852</remarks>
            AsInteger,

            /// <summary>This enum value indicates the option to store the SQLite Datetime as a Real</summary>
            /// <remarks>id : 20131201°0853</remarks>
            AsReal,

            /// <summary>This enum value indicates the option to store the SQLite Datetime as Text</summary>
            /// <remarks>id : 20131201°0854</remarks>
            AsText
         }
      }

      /// <summary>This sub class provides some SQL keywords to be known in the code</summary>
      /// <remarks>id : 20130824°0951</remarks>
      public static class Sql
      {
         /// <summary>This field "From" stores a fieldname to avoid in SQLite (see issue 20130824°0913 'keywords as SQLite field values')</summary>
         /// <remarks>id : 20130824°0954</remarks>
         public static string Currency = "currency";

         /// <summary>This field "Default" stores a fieldname to avoid in SQLite (see issue 20130824°0913 'keywords as SQLite field values')</summary>
         /// <remarks>id : 20130824°0952</remarks>
         public static string Default = "default";

         /// <summary>This field "Default" stores a fieldname to avoid in SQLite (see issue 20130824°0913 'keywords as SQLite field values')</summary>
         /// <remarks>id : 20130824°1313</remarks>
         public static string Exists = "exists";

         /// <summary>This field "From" stores a fieldname to avoid in SQLite (see issue 20130824°0913 'keywords as SQLite field values')</summary>
         /// <remarks>id : 20130824°0953</remarks>
         public static string From = "from";
      }
   }

   /// <summary>This class constitutes a custom assembly attribute</summary>
   /// <remarks>
   /// id : 20130901°0501 (20130618°0521)
   /// ref : 20130618°0505 'article creating global attributes in cs'
   /// </remarks>
   public class CustomAssemblyAttribuz : Attribute
   {
      /// <summary>This property gets/sets the custom assembly attribute value</summary>
      /// <remarks>id : 20130901°0505</remarks>
      public string License { get; set; }

      /// <summary>This property gets/sets the custom assembly attribute value</summary>
      /// <remarks>id : 20130901°0504</remarks>
      public string Slogan { get; set; }

      /// <summary>This property gets/sets the custom assembly attribute value</summary>
      /// <remarks>id : 20130901°0502</remarks>
      public string Timestampf { get; set; }

      /// <summary>This property gets/sets the custom assembly attribute value</summary>
      /// <remarks>id : 20130901°0503</remarks>
      public string VersionNote { get; set; }
   }
}
