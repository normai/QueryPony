#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/InitLib.cs
// id          : 20130619°1221
// summary     : This file stores class 'Init' to provide initialization methods for this library.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// versions    : 20130619°1211 project QueryPonyLib split off from project QueryPony
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyLib
{

   /// <summary>This class provides initialization methods for this library.</summary>
   /// <remarks>
   /// id : 20130619°1222
   /// note : Calling single-file-delivery here is useless (see finding 20130726°1411).
   /// </remarks>
   public class InitLib
   {

      /// <summary>This property gets/sets the User Settings Directory (where e.g. the logfile file is located).</summary>
      /// <remarks>id : 20130625°0903</remarks>
      public static string SettingsDir { get; set; }


      /// <summary>This property gets the logfile path.</summary>
      /// <remarks>id : 20130625°0904</remarks>
      public static string PathLogfile
      {
         get
         {
            ////string sRet = InitLib.SettingsDir + Glb.sDelim_PathBkslsh + "logfile.txt";
            string sRet = InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "logfile.txt";
            ////string sRet = InitLib.SettingsDir + "\\" + "logfile.txt";
            return sRet;
         }
      }


      /// <summary>This constructor initializes this library.</summary>
      /// <remarks>id : 20130620°0911</remarks>
      public InitLib ( string sOutputDir
                      , IOBus.IOBus_OutputLine writeHostChar // (added 20130821°0938)
                       , IOBus.IOBus_OutputLine writeHostLine // (added 20130819°0901)
                        )
      {

         // (seq 20130819°0903)
         IOBusConsumer._writeHostChar = writeHostChar; // (added 20130821°0937)
         IOBusConsumer._writeHostLine = writeHostLine;
         IOBusConsumer.dlgtInputLinei = null; // avoid compiler warning 'never set' (20130828°1421)
         IOBusConsumer.dlgtKeypressi = null; // avoid compiler warning 'never set' (20130828°142102)

         // make debug output folder available program-wide
         SettingsDir = sOutputDir;

         string s = "[Debug] QueryPonyLib initializes.";
         IOBusConsumer.writeHost(s);

         // (sequence 20130709°1341) restore joespostbox.sqlite availability
         if (IOBus.Gb.Debag.ExecuteNO)
         {
            string sMsg = "Settings Folder = " + IOBus.Gb.Bricks.Sp + SettingsDir + IOBus.Gb.Bricks.CrCr + "(Debug 20130709°1342)";
            System.Windows.Forms.MessageBox.Show(sMsg);
         }
      }

   }
}
