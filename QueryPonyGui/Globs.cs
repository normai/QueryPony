#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Globs.cs
// id          : 20130709°0921
// summary     : This file stores class 'Globs' to provide some project specific constants.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyGui
{

   /// <summary>This class provides some project specific constants.</summary>
   /// <remarks>
   /// id : 20130707°0921
   /// note : This class was created to ducplicate/outsource some constants from
   ///    QueryPonyLib while solving issue 20130706°2221, because QueryPonyLib
   ///    is no more available here.
   /// </remarks>
   ////internal class Globs
   ////public class Globs
   internal class Globs
   {

      /// <summary>This subclass provides some constants (ducplicated/outsourced from QueryPonyLib while solving issue 20130706°2221).</summary>
      /// <remarks>id : 20130707°0931 (20130617°1541)</remarks>
      public class Behavior
      {

         /// <summary>This constant 'false/true' tells whether to show the Settings-Upgraded dialogbox or not.</summary>
         /// <remarks>id : 20130707°0932 (20130608°0213)</remarks>
         public static bool OnIsFirstRun_ShowSettingsUpgradedDialogbox = false; // true; // false;

         /// <summary>This constant 'false/true' tells whether to open the Settings Form as modal dialog or as form-on-tab.</summary>
         /// <remarks>id : 20130809°1513</remarks>
         public static bool OpenSettingsFormInModalDialog = false;

      }


      /// <summary>This subclass provides some constants (ducplicated/outsourced from QueryPonyLib while solving issue 20130706°2221).</summary>
      /// <remarks>id : 20130707°0922</remarks>
      public class Debag
      {

         /// <summary>This const 'false/true' tells whether to show the 'Main Form Close' debug message or not.</summary>
         /// <remarks>id : 20130709°1214</remarks>
         public static bool DebugMessage_MainForm_Close = false; // true; // false;

         /// <summary>This const 'false/true' tells whether to fire the debugger if a context menu item is selected (toggle this value on demand).</summary>
         /// <remarks>id : 20130707°0923 (20130621°1211)</remarks>
         public static bool DebugMessage_Program_SettingsFile = false; // true;

         /// <summary>This const 'false' tells whether to execute a debug/test sequence or not (toggle this value on demand).</summary>
         /// <remarks>id : 20130707°0924 (20130608°0122)</remarks>
         public static bool ExecuteNo = false;

         /// <summary>This const 'false' tells to skip a sequence because it is temporarily shutdown.</summary>
         /// <remarks>id : 20130707°0937 (20130619°0922)</remarks>
         public static bool ShutdownTemporarily = false;

      }


      /// <summary>This subclass provides constants to tell resource filenames.</summary>
      /// <remarks>id : 20130707°0933 (20130619°0301)</remarks>
      public class Resources
      {

         /// <summary>This const "QueryPonyLib" tells a resource file name.</summary>
         /// <remarks>id : 20130707°0934 (20130706°0912)</remarks>
         public const string AssemblyNameLib = "QueryPonyLib";

      }

      /// <summary>This constant "\r\n" tells a CRLF (carriage return line feed).</summary>
      /// <remarks>id : 20130707°0925 (20130608°0212 20060313°1322)</remarks>
      public const string sCr = "\r\n";

   }
}
