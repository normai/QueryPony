#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Program.cs
// id          : 20130604°1911
// summary     : This file stores class 'Program' to constitute the program entry point.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This class holds the program entry point.</summary>
   /// <remarks>
   /// id : 20130604°1912
   /// todo : Solve issue 20130624°0911 'commandline parameters fail working'
   /// </remarks>
   public static class Program
   {

      /// <summary>This property stores the path for the User Configuration Folder.</summary>
      /// <remarks>id : 20130707°0911</remarks>
      public static string PathConfigDirUser { get; set; }

      /// <summary>This property stores the path of the SQLite demo database file.</summary>
      /// <remarks>id : 20130702°0533</remarks>
      public static string SqliteDemoJoesgarage { get; set; }

      /// <summary>This property stores the path of the SQLite demo database file.</summary>
      /// <remarks>id : 20130704°1313</remarks>
      public static string SqliteDemoJoespostbox { get; set; }


      /// <summary>This method constitutes the main entry point for the application.</summary>
      /// <remarks>id : 20130604°1913</remarks>
      /// <param name="args">The arguments coming from the commanline that started this program</param>
      [STAThread]
      private static void Main(string[] args)
      {
         string sErr = "", sMsg = "";

         // was the program started with commandline parameters?
         if (args.Length > 0)
         {
            // if the help screen was displayed, then ready
            if (DisplayHelpIfNeeded(args))
            {
               return;
            }
         }

         // preparation set properties
         initProperties();

         // install single-file-deployment facility (line 20130706°1053)
         // note : findings 20130726°1423 'canary library access' about issue 20130726°1231'
         SingleFileDeployment.provideSingleFileDeployment();


         // (sequence 20130812°1331)
         // Wrap in try loop against issue 20130812°1321 'start with exception OnConfigRemoved'
         // note : This does not remedy the situation, it just displays a dialog with
         //    a more meaningful and more user friendly message, than a native exception.
         // note : This sequence is not tested yet. It is not so easy to reproduce the
         //    situation. After I inserted this sequence, the configuration path was
         //    different and pristine again, and the error was gone. Just renaming the
         //    path is not enough to reproduce this error, since there must be some
         //    specific circumstances (I don't know which exactly).
         bool bIsFirstRun = false;
         try
         {
            bIsFirstRun = QueryPonyGui.Properties.Settings.Default.IsFirstRun;
         }
         catch (Exception ex)
         {
            sErr = "Something is wrong with the configuration."
                  + Globs.sCr + "It looks like the configuration file has been removed."
                   + Globs.sCr + Globs.sCr + "Configuration Path:"
                    + Globs.sCr + Globs.sCr + "   " + PathConfigDirUser
                     + Globs.sCr + Globs.sCr + "Exception Details:"
                      + Globs.sCr + Globs.sCr + "   " + ex.Message
                       + Globs.sCr + Globs.sCr + "Sorry and bye."
                        ;
            MessageBox.Show(sErr, "Fatal Error");
            return;
         }


         // (sequence 20130812°1332)
         if (QueryPonyGui.Properties.Settings.Default.IsFirstRun)
         {
            // debug facility
            // note : Remember issue 20130621°1212 'debug settings IsFirstRun = false'.
            if (Globs.Debag.DebugMessage_Program_SettingsFile)
            {
               System.Diagnostics.Debugger.Break();

               string sTest = "";
               QueryPonyGui.Properties.Settings.Default.Upgrade();
               sTest = Properties.Settings.Default.Delimiter;

               // now the file exists, and you have the chance to manipulate it
               if (Globs.Debag.ExecuteNo)
               {
                  System.Diagnostics.Process.Start("notepad.exe", Program.PathConfigDirUser);
               }

               // read the possibly manipulated settings
               QueryPonyGui.Properties.Settings.Default.Reload();

               // view your manipulation
               sTest = Properties.Settings.Default.Delimiter;
            }

            // create new settings
            QueryPonyGui.Properties.Settings.Default.Upgrade();
            QueryPonyGui.Properties.Settings.Default.IsFirstRun = false;
            QueryPonyGui.Properties.Settings.Default.Save();

            // possible notification
            if (Globs.Behavior.OnIsFirstRun_ShowSettingsUpgradedDialogbox)
            {
               // notification
               sMsg = "Settings Upgraded."
                     + Globs.sCr + Globs.sCr + "User Settings File:"
                      + Globs.sCr + Globs.sCr + " - " + Program.PathConfigDirUser
                       + Globs.sCr + Globs.sCr + "Application Settings File:"
                        + Globs.sCr + Globs.sCr + " - " + Program.PathConfigDirUser
                         ;
               System.Windows.Forms.MessageBox.Show(sMsg);
            }
         }


         // (seqence 20130727°0901) using low level WriteLine() because
         //  it is not sure whether other file write methods are available yet
         string sLine = "                         ----------------------------------------------------------------\r\n";
         string sTime = " " + DateTime.Now.ToString(QueryPonyLib.Glb.sFormat_Timestamp);
         string sFile = System.IO.Path.Combine(Program.PathConfigDirUser, "logfile.txt");
         sLine += sTime + "  " + "[Debug]" + " " + "Program start, IsFirstRun = "
                + QueryPonyGui.Properties.Settings.Default.IsFirstRun.ToString()
                 ;
         using (System.IO.StreamWriter file = new System.IO.StreamWriter(sFile, true))
         {
            file.WriteLine(sLine);
         }


         // start business as usual
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         // note : (exception 20130726°1402) This is a typical line for popping
         //  exception 'StackOverflow exception was unhandled', e.g. while debugging
         //  issue 20130726°1231 when iobus.dll was missing and in other situations
         //  with one of the single-file-delivery issues.
         Application.Run(new MainForm(args));
      }


      /// <summary>This method initializes the Program properties.</summary>
      /// <remarks>
      /// id : 20130902°0641
      /// note : Remember finished issue 20130902°0621 'Properties left unset if started from a host'.
      /// </remarks>
      public static void initProperties()
      {
         // (seqence 20130902°0642)
         // note : Compare method 20130619°0323 AboutForm.cs::displayMachineInfo()
         string sPathConfigFileUser = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
         string sPathConfigFileApp = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).FilePath;
         Program.PathConfigDirUser = System.IO.Path.GetDirectoryName(sPathConfigFileUser);
      }


      /// <summary>This eventhandler processes the SettingsSaving event (not called, not implemented).</summary>
      /// <remarks>id : 20130604°1914</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      static void Default_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
      {
         string sMsg = "The method or operation is not implemented." + " " + "[20130604°1914]";
         throw new Exception(sMsg);
      }


      /// <summary>
      /// This method determines whether the commandline contains a '?' or 'help'
      ///  token, and if yes, then displays a help message.
      /// </summary>
      /// <remarks>id : 20130604°1915</remarks>
      /// <param name="args">The arguments array from the program start commandline</param>
      /// <returns>Flag whether a help screen was shown or not</returns>
      private static bool DisplayHelpIfNeeded(string[] args)
      {
         bool bRet = false;
         CommandLineParams cmdLine = new CommandLineParams(args);
         if ((cmdLine["?"] != null) || (cmdLine["help"] != null))
         {
            System.Console.WriteLine(string.Format ( "{0} - {1}"
                                                    , AboutForm.AssemblyTitleo
                                                     , AboutForm.AssemblyVersion
                                                      ));
            System.Console.WriteLine("-------------------------------------------------------------");
            System.Console.WriteLine("Command Line Arguments");
            System.Console.WriteLine("   -?, -help : Help");
            System.Console.WriteLine("   -cn [connection_name] : Load connection by name");
            System.Console.WriteLine("   -s [SQL_Server_Name] : Connect to SQL Server by Name");
            System.Console.WriteLine("   -os [Oracle_Data_Source] : Connect to Oracle by Data Source Name");
            System.Console.WriteLine("   -e : Use Trusted Connection");
            System.Console.WriteLine("   -u [User_Name] : User Name");
            System.Console.WriteLine("   -p [Password] : Password");
            System.Console.WriteLine("   -i [FileName] : Open SQL File");
            bRet = true;
         }
         return bRet;
      }
   }
}
