#region Fileinfo
// file        : 20130619°1221 /QueryPony/QueryPonyLib/InitLib.cs
// summary     : Class 'Init' provides initialization methods for this library
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// versions    : 20130619°1211 project QueryPonyLib split off from project QueryPony
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;
using SyIo = System.IO;

namespace QueryPonyLib
{
   /// <summary>This class provides initialization methods for this library</summary>
   /// <remarks>
   /// id : class 20130619°1222
   /// note : Calling single-file-delivery here is useless (see finding 20130726°1411)
   /// </remarks>
   public class InitLib
   {
      /// <summary>This property gets/sets the User Settings Directory (where e.g. the logfile file is located)</summary>
      /// <remarks>id : property 20210522°1313</remarks>
      public static string Settings2Dir { get; set; }

      /// <summary>This property gets/sets the User Settings Directory (where e.g. the logfile file is located)</summary>
      /// <remarks>
      /// id : property 20130625°0903
      /// Check : Misleading identifyer name!</remarks>
      public static string Settings1Dir { get; set; }

      /// <summary>This property gets the logfile path</summary>
      /// <remarks>id : property 20130625°0904</remarks>
      public static string PathLogfile
      {
         get
         {
            string sRet = InitLib.Settings1Dir + IOBus.Gb.Bricks.PathBkslsh + "logfile.txt";
            return sRet;
         }
      }

      /// <summary>This constructor initializes this library</summary>
      /// <remarks>
      /// id : ctor 20130620°0911
      /// note : A static method for initialization will not do it, because this
      ///    library may be used by multiple clients at the same time, e.g. QuPpCmd
      ///    plus QuPpGui, and those have different output facilities.
      /// </remarks>
      /// <param name="sOutputDir">Directory for some use -- Probably useless</param>
      /// <param name="writeHostChar">Delegate for a character writing facility</param>
      /// <param name="writeHostLine">Delegate for a line writing facility<param>
      public InitLib ( string sOutputDir
                      , IOBus_OutputChars writeHostChar
                       , IOBus_OutputLine writeHostLine
                        )
      {
         // Provide communication facilities [seq 20130819°0903]
         IOBusConsumer._writeHostChar = writeHostChar;
         IOBusConsumer._writeHostLine = writeHostLine;
         IOBusConsumer.dlgtInputLinei = null;                                  // Avoid compiler warning 'never set' [line 20130828°1421]
         IOBusConsumer.dlgtKeypressi = null;                                   // Avoid compiler warning 'never set' [line 20130828°1421`02]

         // Make debug output folder available program-wide
         InitLib.Settings1Dir = sOutputDir;

         // Prime single file deployment [seq 20210523°1321]
         Resolver.Register();

         // Set user folder [seq 20210522°1313 (after 20210520°1241, 20130902°0642)]
         // CHECK — Redundant seq 20210520°1241 in Resolver.cs must possibly be adjusted
         string sFil = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
         string sDir = System.IO.Path.GetDirectoryName(sFil);
         if (! SyIo.Directory.Exists(sDir))
         {
            SyIo.Directory.CreateDirectory(sDir);
         }
         InitLib.Settings2Dir = sDir;

         string s = "QueryPonyLib is initializing ...";
         IOBusConsumer.writeHost(s);

         // Trigger SQLite demo database extraction [seq 20210522°1411]
         this.ExtractRessources();
      }

      /// <summary>
      ///  This method shall extract the ressources on library initialization
      /// </summary>
      /// <remarks>
      /// id : method 20210522°1421
      /// </remarks>
      private void ExtractRessources() {

         // For more details how to do, compare
         //  ConnSettingsGui.cs seq 20130709°1351 'Guarantee SQLite demo database files'

         // Prepare ingredients
         System.Reflection.Assembly asmSource1 = System.Reflection.Assembly.GetExecutingAssembly();
         string sAsmResourceName2 = Glb.Resources.JoespostboxSqliteResourcename;  // "QueryPonyLib.docs.joespostbox.20130703o1243.sqlite3"

         // Prepare extraction list
         IOBus.Utils.Resofile[] resos = {
            ////new IOBus.Utils.Resofile(asmSource1, sAsmResourceName2, InitLib.Settings2Dir, "joespostbox.201307031243.sqlite3")
            new IOBus.Utils.Resofile(asmSource1, sAsmResourceName2, InitLib.Settings2Dir, "joespostbox.20130703o1243.sqlite3") // [try fix 20220731°0951`02]
         };

         // Perform extraction
         IOBus.Utils.provideResourceFiles(resos);
      }

      /// <summary>
      ///  Debug facility to see the library loaded even before it is initialized
      /// </summary>
      /// <remarks>id : method 20220805°1221</remarks>
      /// <returns>String as the ping answer</returns>
      public static String PingLib()
      {
         String sRet = "Hallo QueryPonyLib ..";
         return sRet;
      }
   }
}
