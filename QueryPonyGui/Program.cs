#region Fileinfo
// file        : 20130604°1911 /QueryPony/QueryPonyGui/Program.cs
// summary     : Class 'Program' constitutes the program entry point
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Linq;                                     // Where
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This class holds the program entry point</summary>
   /// <remarks>
   /// id : 20130604°1912
   /// </remarks>
   public static class Program
   {
      /// <summary>This property stores the path for the User Configuration Folder</summary>
      /// <remarks>id : 20130707°0911</remarks>
      public static string PathConfigDirUser { get; set; }

      /// <summary>This property stores the path of the SQLite demo database file</summary>
      /// <remarks>id : 20130702°0533</remarks>
      public static string SqliteDemoJoesgarage { get; set; }

      /// <summary>This property stores the path of the SQLite demo database file</summary>
      /// <remarks>id : 20130704°1313</remarks>
      public static string SqliteDemoJoespostbox { get; set; }

      /// <summary>This method constitutes the main entry point for the application</summary>
      /// <remarks>id : method 20130604°1913</remarks>
      /// <param name="args">The arguments coming from the commanline that started this program</param>
      [STAThread]
      private static void Main(string[] args)
      {
         string sErr = "", sMsg = "";

         // Was the program started with commandline parameters?
         if (args.Length > 0)
         {
            // If the help screen was displayed, then ready
            if (DisplayHelpIfNeeded(args))
            {
               return;
            }
         }

         // Preparation set properties
         InitProperties();

         // Install single-file-deployment facility [line 20130706°1053]
         ////SingleFileDeployment.provideSingleFileDeployment();  // [shutdown 20220806°0921 Finally]

         // Detect first run [seq 20130812°1331]
         // Wrap in try loop against issue 20130812°1321 'Start with exception OnConfigRemoved'
         // Note : This does not remedy the situation, it just displays a dialog with
         //    a more meaningful and more user friendly message, than a native exception.
         // Note : This sequence is not tested yet. It is not so easy to reproduce the
         //    situation. After I inserted this sequence, the configuration path was
         //    different and pristine again, and the error was gone. Just renaming the
         //    path is not enough to reproduce this error, since there must be some
         //    specific circumstances, I do not know which exactly.
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

         // [seq 20130812°1332]
         if (QueryPonyGui.Properties.Settings.Default.IsFirstRun)
         {
            // Debug facility
            // Note : Remember issue 20130621°1212 'Debug settings IsFirstRun = false'.
            if (Globs.Debag.DebugMessage_Program_SettingsFile)
            {
               System.Diagnostics.Debugger.Break();

               string sTest = "";
               QueryPonyGui.Properties.Settings.Default.Upgrade();
               sTest = Properties.Settings.Default.Delimiter;

               // Now the file exists, and you have the chance to manipulate it
               if (Globs.Debag.Execute_No)
               {
                  System.Diagnostics.Process.Start("notepad.exe", Program.PathConfigDirUser);
               }

               // Read the possibly manipulated settings
               QueryPonyGui.Properties.Settings.Default.Reload();

               // View your manipulation
               sTest = Properties.Settings.Default.Delimiter;
            }

            // Create new settings
            QueryPonyGui.Properties.Settings.Default.Upgrade();
            QueryPonyGui.Properties.Settings.Default.IsFirstRun = false;
            QueryPonyGui.Properties.Settings.Default.Save();

            // Possible notification
            if (Globs.Behavior.OnIsFirstRun_ShowSettingsUpgradedDialogbox)
            {
               // Notification
               sMsg = "Settings Upgraded."
                     + Globs.sCr + Globs.sCr + "User Settings File:"
                      + Globs.sCr + Globs.sCr + " - " + Program.PathConfigDirUser
                       + Globs.sCr + Globs.sCr + "Application Settings File:"
                        + Globs.sCr + Globs.sCr + " - " + Program.PathConfigDirUser
                         ;
               System.Windows.Forms.MessageBox.Show(sMsg);
            }
         }

         // Use low level WriteLine because .. [seq 20130727°0901]
         // It is not sure whether other file write methods are available yet
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

         // Start business as usual
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         // Set up event handler after Paul Rohde 2011-Jul-13 [line 20220805°1312`xx]
         AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;

         // Debug below issue 20220804°0931 'StackOverflow while form creation' [seq 20220805°1211]
         // Look whether assembly QueryPonyLib is available
         string[] arDbg = Program.listAvailableResources("");
         if (Globs.Debag.Execute_No)                                           // Eliminate sequence on later cleanup [seq 20220805°1243]
         {
            // Try/inspect Assembly.LoadFrom [seq 20220805°1242]
            String sFullFileNam = @"G:\work\downtown\queryponydev\trunk\QueryPony\QueryPonyLib\bin\x64\Debug\QueryPonyLib.dll";
            System.Reflection.Assembly a1 = System.Reflection.Assembly.LoadFrom(sFullFileNam);

            // Try/inspect Assembly.Load [seq 20220805°1243]
            String sDbgFullQuali1 = System.Reflection.AssemblyName.GetAssemblyName(sFullFileNam).FullName;
            String sDbgFullQuali2 = "SampleAssembly, Version=1.0.2004.0, Culture=neutral, PublicKeyToken=8744b20f8da049e3";
            System.Reflection.Assembly a2 = System.Reflection.Assembly.Load(sDbgFullQuali2);  // => StackOverflowException
         }

         // Start GUI [line 20130726°1402]
         // Remember issue 20220804°0931 'StackOverflow while form creation'
         var fm = new MainForm(args);
         Application.Run(fm);
      }

      /// <summary>This method initializes the Program properties</summary>
      /// <remarks>
      /// id : 20130902°0641
      /// note : Remember finished issue 20130902°0621 'Properties left unset if started from a host'.
      /// </remarks>
      public static void InitProperties()
      {
         // [seq 20130902°0642]
         // note : Compare method 20130619°0323 AboutForm.cs::displayMachineInfo()
         string sPathConfigFileUser = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
         string sPathConfigFileApp = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).FilePath;
         Program.PathConfigDirUser = System.IO.Path.GetDirectoryName(sPathConfigFileUser);
      }

      /// <summary>This eventhandler processes the SettingsSaving event (not called, not implemented)</summary>
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


      /// <summary>
      ///  Possible lean alternative to former file 20130831°1611 with method provideSingleFileDeployment()
      /// </summary>
      /// <remarks>
      ///  id : method 20220805°1311
      ///  ref : blogs.interknowlogy.com/2011/07/13/merging-a-wpf-application-into-a-single-exe/ [ref 20220805°1232]
      ///  note : Just a try while debugging issue 20220804°0931 'StackOverflowException'
      /// </remarks>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      /// <returns>The wanted assembly or null</returns>
      ////private static System.Reflection.Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
      public static System.Reflection.Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
      {
         var thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();

         // Get the Name of the AssemblyFile
         var assemblyName = new System.Reflection.AssemblyName(e.Name);
         var dllName = assemblyName.Name + ".dll";

         // Load from Embedded Resources — This function is not called if the assembly
         //  is already in the same folder as the app.
         var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(dllName));
         if (resources.Any())
         {

            // Ninetynine percent of cases will only have one matching item, but if
            //  you don't, you will have to change the logic to handle those cases.
            var resourceName = resources.First();
            using (var stream = thisAssembly.GetManifestResourceStream(resourceName))
            {
               if (stream == null) return null;
               var block = new byte[stream.Length];

               // Safely try to load the assembly.
               try
               {
                  stream.Read(block, 0, block.Length);
                  return System.Reflection.Assembly.Load(block);
               }
               catch (System.IO.IOException)
               {
                  return null;
               }
               catch (BadImageFormatException)
               {
                  return null;
               }
            }
         }

         // In the case the resource doesn't exist, return null.
         return null;
      }

      /// <summary>This method is a debug sequence to list all available resources in an assembly</summary>
      /// <remarks>
      /// id : method 20130706°1052
      /// note : Compare nearly identical method 20130707°1844 in QueryPonyLib/IOBus/Utils.cs.
      ///    Just that in the library does not help, we need the method before the library is loaded.
      /// callers : • method 20130604°1913 Main()
      /// </remarks>
      /// <param name="sAssemblyname">The assembly of which we want list the resources</param>
      /// <returns>The wanted array with ressouce name strings</returns>
      private static string[] listAvailableResources(string sAssemblyname)
      {
         System.Reflection.Assembly thisExe = null;
         if (sAssemblyname == "")
         {
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
         }
         else
         {
            // Try envelop to find out about curious recursion (experiment 20130707°1001)
            try
            {
               thisExe = System.Reflection.Assembly.Load(sAssemblyname);
            }
            catch (Exception ex)
            {
               string sErr = ex.Message;
            }
         }
         string[] resources = thisExe.GetManifestResourceNames();

         // Recycle sequence if output is wanted as string instead array
         if (Globs.Debag.ShutdownTemporarily)                                  // Shutdown to recycle
         {
            string list = "";
            foreach (string resource in resources)
            {
               list += resource + Globs.sCr;
            }
            string s = new System.Reflection.AssemblyName(sAssemblyname).Name;
            // E.g. s = "EnumProgs.enumprogslib.dll\r\nEnumProgs.Properties.Resources.resources\r\nEnumProgs.Form1.resources\r\n"
         }
         Array.Sort(resources);
         return resources;
      }
   }
}
