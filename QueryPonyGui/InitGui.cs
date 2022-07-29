#region Fileinfo
// file        : 20130707°0901 QueryPony/QueryPonyGui/InitGui.cs
// summary     : Class 'Inits' performs some initialisation tasks
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
#endregion Fileinfo

using QueryPonyLib;

namespace QueryPonyGui
{
   /// <summary>This class performs some initialisation tasks</summary>
   /// <remarks>
   /// id : 20130707°0902
   /// note : This class was created to home lines which shall be shifted away from the
   ///    static entry point Main() to a very early moment in class MainForm because
   ///    they want library QueryPonyLib. This shift shall counteract issue 20130706°2221
   ///    'Library usage causes FileNotFoundException even before program entry'.
   /// </remarks>
   internal class Inits
   {
      /// <summary>This method performs possible initialization tasks</summary>
      /// <remarks>id : 20130707°0903 (after 20130604°1913)</remarks>
      /// <returns>Success flag (proforma?)</returns>
      internal bool DoInitialization()
      {
         // Finetune application window title (20130715°1011)
         string sVersion = AboutForm.AssemblyVersion;
         MainForm._mainform.Text = "QueryPony" + " (" + sVersion + ")";

         /*
         ////Note 20130726°1431 ''
         Location : Around debugging issue 20130726°1231
         Title : Considerations about the chronology of the assembly loading
         Finding : Below 'InitLib lib = new InitLib' seems to be the first moment,
                    the library is wanted. How can I proof this?
         */

         // Provide the basic console character output delegate for library (line 20130821°0940)
         IOBus_OutputChars webriOutputCharDelegate = new IOBus_OutputChars(MainForm._mainform.writeChar); //// chg 20210522°1031`xx

         // Provide the basic console lnie output delegate for library (line 20130819°0902)
         IOBus_OutputLine webriOutputDelegate = new IOBus_OutputLine(MainForm._mainform.writeLine);

         // Initialize library [line 20130819°0904]
         var quPoLib = new QueryPonyLib.InitLib ( Program.PathConfigDirUser                 // Possibly useless after refactoring 2021
                                    , webriOutputCharDelegate                  // Character writing facility
                                     , webriOutputDelegate                     // Line writing facility
                                      );

         // Output very first log message [seq 20130707°0906]
         //------------------------------------------------
         // note : Utils.outputLine() must only be done behind Settings.Default.Upgrade().
         //    outputLine() seems not allowed here yet (DirectoryNotFoundException). Try to
         //    reactivate outputLine() later, after the issues has settled, to find out the exact
         //    earliest moment. [note 20130707°1003] (this note obsolet by 20130711°0913 ?)
         // note : The Settings folder may not yet exist here, but outputLine() can create
         //    it. The moment to watch out for, is the availability of the library. And in
         //    fact not when running from the build folder, where libraries may lurk around,
         //    but from any folder, where no libraries exist, so it is garanteed, the
         //    built-in libraries are to be used. (20130711°0913)
         //------------------------------------------------
         if (Glb.Debag.Execute_No)
         {
            string sOut = "[Debug]" + " " + Glb.Resources.AssemblyNameLib + Glb.sBlnk + "was initialized (one) (debug 20130707°0906).";
            Utils.outputLine(sOut);
         }

         return true;
      }
   }
}
