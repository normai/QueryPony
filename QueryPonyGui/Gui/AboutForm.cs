#region Fileinfo
// file        : 20130604°0011 /QueryPony/QueryPonyGui/Gui/AboutForm.cs
// summary     : Class 'AboutForm' constitutes the About Form
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using GlobalCustomAttributes;
using QueryPonyLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;                                                       // For Assembly

namespace QueryPonyGui
{
   /// <summary>This class constitutes the About Form</summary>
   /// <remarks>id : 20130604°0012</remarks>
   public partial class AboutForm : Form
   {
      /// <summary>This constructor creates an About Form</summary>
      /// <remarks>id : 20130604°0013</remarks>
      public AboutForm()
      {
         InitializeComponent();

         label_SignPost.Text = MainForm._signPost;
         label_SignPost.ForeColor = MainForm._signPostColor;

         displayAssemblyInfo();
         displayTextfiles();
         displayMachineInfo();
      }

      /// <summary>This method puts the assembly infos into their textboxes</summary>
      /// <remarks>id : 20130619°0321</remarks>
      private void displayAssemblyInfo()
      {
         this.Text = String.Format("About {0}", AssemblyTitleo);
         this.textbox_ProductName.Text = AssemblyProduct;
         this.textbox_AssemblyVersion.Text = AssemblyVersion;
         this.textbox_FileVersion.Text = AssemblyFileVersion;
         this.textbox_Summary.Text = AssemblyDescription;
         this.textbox_Authors.Text = AssemblyAuthors;
         this.textbox_Company.Text = "This program came to you from " + AssemblyCompany;
      }

      /// <summary>This method puts the textfiles contents into their textboxes</summary>
      /// <remarks>id : 20130619°0322</remarks>
      private void displayTextfiles()
      {
         // Preparations
         Assembly asm = Assembly.GetExecutingAssembly();

         // Debug
         if (Glb.Debag.Execute_No)
         {
            // Get miscellaneous values
            AssemblyName asmName = asm.GetName();
            string sAsmName = asmName.Name;                                    // This is "QueryPony", but we need "QueryPonyGui"
            string sAsmFullname = asmName.FullName;                            // E.g. = "QueryPony, Version=2.1.1.4689, Culture=neutral, PublicKeyToken=null"

            // Get an assembly different from the executing one
            Assembly asmLib = Assembly.Load(Glb.Resources.AssemblyNameLib);    // "QueryPonyLib"
            string[] arDbgLib = asmLib.GetManifestResourceNames();

            // Inspect list of available resources
            string[] arDbg = asm.GetManifestResourceNames();

            // Retrieve the namespace (not directly possibly, but could be extracted from 'DeclaringType.FullName'
            MethodBase mbCurMet2 = System.Reflection.MethodBase.GetCurrentMethod();
            string sFullMethodName = mbCurMet2.DeclaringType.FullName + "." + mbCurMet2.Name; // = "QueryPonyGui.AboutForm.displayTextfiles"
         }

         // Extract namespace from DeclaringType [seq 20130620°1701]
         // Note : This method assumes, that the type name always ends in a method name
         //    without dots, means all components except the last one belong to the namespace.
         string sNamespace = "";
         MethodBase mbCurMet = System.Reflection.MethodBase.GetCurrentMethod();  // = "displayTextfiles"
         string sTypeName = mbCurMet.DeclaringType.FullName;                   // = "QueryPonyGui.AboutForm"
         string[] ar = sTypeName.Split(Glb.cDot);                              // '.'
         for (int i = 0; i < (ar.Length - 1); i++)
         {
            if (i > 0) { sNamespace += Glb.sDot; }                             // Behind namespace use a dot (not testet, just concluded)
            sNamespace += ar[i];
         }

         // Provide files list
         Dictionary<TextBox, string> texts = new Dictionary<TextBox, string>();
         texts.Add(textbox_Agpl, Glb.Resources.Agpl);
         texts.Add(textbox_Authors, Glb.Resources.Authors);
         texts.Add(textbox_Changelog, Glb.Resources.Changelog);
         texts.Add(textbox_Issues, Glb.Resources.Issues);
         texts.Add(textbox_License, Glb.Resources.License);
         texts.Add(textbox_Summary, Glb.Resources.Summary);
         texts.Add(textbox_Thirdparty, Glb.Resources.Thirdparty);

         // Print files in texboxes
         foreach (KeyValuePair<TextBox, string> kvp in texts)
         {
            string[] arDebug = asm.GetManifestResourceNames();
            string sResourceFile = kvp.Value;

            // Use one of the two possible assemblies
            Assembly asmUse = null;
            if (kvp.Value.StartsWith(Glb.Resources.AssemblyNameGui))           // "QueryPonyGui"
            {
               asmUse = Assembly.GetExecutingAssembly();
            }
            else
            {
               asmUse = Assembly.Load(Glb.Resources.AssemblyNameLib);          // "QueryPonyLib"
            }

            // Read the wanted resource file
            using (System.IO.Stream stream = asmUse.GetManifestResourceStream(sResourceFile))
            {
               using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
               {
                  string sText = reader.ReadToEnd();
                  kvp.Key.Text = sText;
               }
            }
         }
      }

      /// <summary>This method puts the machine infos into their textboxes</summary>
      /// <remarks>
      /// id : 20130619°0323
      /// note : Compare Program.cs::initProperties() sequence 20130902°0642.
      /// </remarks>
      private void displayMachineInfo()
      {
         string sPathUser = System.Configuration.ConfigurationManager.OpenExeConfiguration
                           ( System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal
                            ).FilePath
                             ;
         string sPathApp = System.Configuration.ConfigurationManager.OpenExeConfiguration
                          ( System.Configuration.ConfigurationUserLevel.None
                           ).FilePath
                            ;

         if (IOBus.Gb.Debag.Execute_No)
         {
            // Finding: Here we have filename '\user.config' attached as opposed to Program.PathConfigDirUser.
            string s = Program.PathConfigDirUser;
         }

         textbox_UserSettingsFolder.Text = sPathUser;
         textbox_AppSettingsFolder.Text = sPathApp;                            // E.g. "G:\work\downtown\queryponydev\trunk\querypony\QueryPonyGui\bin\x86\Debug\QueryPony.exe.config"
      }

      #region Assembly Attribute Accessors

      /// <summary>This property gets the assembly attribute 'AssemblyTitle'</summary>
      /// <remarks>
      /// id : 20130604°0014
      /// note : What I do not yet understand about the attribute workings is the fact,
      ///    that in AssemblyInfo.cs the attribute is called 'AssemblyTitle'. But with
      ///    'Find All References', I cannot find this exact identifier anywhere. Just
      ///    similar names e.g. 'AssemblyTitleAttribute'. Does the compiler somehow
      ///    mangle identifiers? [note 20130618°0532]
      /// </remarks>
      public static string AssemblyTitleo
      {
         get
         {
            string sRet = "";

            // Get all title attributes on this assembly
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            // If there is at least one title attribute
            if (attributes.Length > 0)
            {
               // Select the first one
               AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

               // If it is not an empty string, return it
               if (titleAttribute.Title != "")
               {
                  sRet = titleAttribute.Title;
               }
               else
               {
                  // If there was no title attribute, or if the title attribute was the empty string, return the .exe name
                  sRet = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
               }
            }
            return sRet;
         }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyVersion'</summary>
      /// <remarks>id : 20130604°0015</remarks>
      public static string AssemblyVersion
      {
         get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyFileVersion'</summary>
      /// <remarks>id : 20130613°1121</remarks>
      public static string AssemblyFileVersion
      {
         get
         {
            string sRet = "N/A";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            if ( attributes.Length > 0 )
            {
               AssemblyFileVersionAttribute att = (AssemblyFileVersionAttribute) attributes[0];
               if (att.Version != "")
               {
                  sRet = att.Version;
               }
            }
            return sRet;
         }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyDescription'</summary>
      /// <remarks>id : 20130604°0016</remarks>
      public static string AssemblyDescription
      {
         get
         {
            string sRet = "";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
               sRet = ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
            return sRet;
         }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyProduct'</summary>
      /// <remarks>id : 20130604°0017</remarks>
      public static string AssemblyProduct
      {
         get
         {
            string sRet = "";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length > 0)
            {
               sRet = ((AssemblyProductAttribute)attributes[0]).Product;
            }
            return sRet;
         }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyCopyright'</summary>
      /// <remarks>id : 20130604°0018</remarks>
      public static string AssemblyCopyright
      {
         get
         {
            string sRet = "";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length > 0)
            {
               sRet = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
            return sRet;
         }
      }

      /// <summary>This property gets the assembly attribute 'AssemblyCompany'</summary>
      /// <remarks>id : 20130604°0019</remarks>
      public static string AssemblyCompany
      {
         get
         {
            string sRet = "";
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length > 0)
            {
               sRet = ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
            return sRet;
         }
      }

      /// <summary>This property gets the custom assembly attribute 'CustomAuthorsAttribute'</summary>
      /// <remarks>id : 20130618°0531</remarks>
      public static string AssemblyAuthors
      {
         get
         {
            string sRet = "";
            object[] attribs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(CustomAuthorsAttribute), false);
            if (attribs.Length > 0)
            {
               sRet = ((CustomAuthorsAttribute)attribs[0]).Authors;
            }
            return sRet;
         }
      }

      #endregion Assembly Attribute Accessors

      /// <summary>This eventhandler processes the OK button Click event</summary>
      /// <remarks>id : 20130604°0020</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void button_Close_Click(object sender, EventArgs e)
      {
         this.Close(); // (supplemented 20130902°0631)
      }

      /// <summary>This eventhandler processes the 'Open Settings Folder' buttons</summary>
      /// <remarks>
      /// id : 20130619°0411
      /// todo : See todo 20130619°0431 'About dialogbox closes after any button usage'.
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event itself</param>
      private void buttons_OpenSettingsFolder_Click(object sender, EventArgs e)
      {
         string sErr = "";
         Control control = (Control)sender;
         string sControlName = control.Name;
         Button button = (Button)control;

         // Determine folder
         string sPathFile = "";
         if (button == button_OpenAppSettingsFolder)
         {
            sPathFile = textbox_AppSettingsFolder.Text;
         }
         else if (button == button_OpenUserSettingsFolder)
         {
            sPathFile = textbox_UserSettingsFolder.Text;
         }
         else
         {
            sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCr + "[Error 20130619°0412]";
            System.Diagnostics.Debugger.Break();
         }

         // Open Explorer
         string sPathDir = System.IO.Directory.GetParent(sPathFile).FullName;
         if (System.IO.Directory.Exists(sPathDir))
         {
            try
            {
               System.Diagnostics.Process.Start(Glb.Files.WindowsExplorer, sPathDir); // "explorer.exe"
            }
            catch (Exception ex)
            {
               // program flow error
               sErr = ex.Message + Glb.sCr + "[Error 20130619°0413]";
               System.Diagnostics.Debugger.Break();
            }
         }
         else
         {
            // Fatal
         }
      }

      /// <summary>This eventhandler processes button ViewLogfile's Click event to open the logfile in Notepad</summary>
      /// <remarks>id : 20130625°0941</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_ViewLogfile_Click(object sender, EventArgs e)
      {
         // Open notepad.exe
         if (System.IO.File.Exists(InitLib.PathLogfile))
         {
            try
            {
               System.Diagnostics.Process.Start(Glb.Files.NotepadExe, InitLib.PathLogfile); // "notepad.exe"
            }
            catch (Exception ex)
            {
               // Program flow error
               string sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCr + ex.Message + Glb.sCr + "[Error 20130625°0943]";
               System.Diagnostics.Debugger.Break();
            }
         }
         else
         {
            // Fatal
            string sMsg = "No such logfile exists: " + Glb.sCrCr + "   " + InitLib.PathLogfile;
            MessageBox.Show(sMsg, "Notification");
         }
      }
   }
}
