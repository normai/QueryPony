#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/MenuItems.cs
// id          : 20130707°1831
// summary     : This file stores class 'MenuItems' to provide methods to process menu items.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyGui
{
   /// <summary>This class provides methods to process menu items.</summary>
   /// <remarks>id : 20130707°1832</remarks>
   internal class MenuItems
   {

      /// <summary>This method executes the 'View User Manual In Browser' main menu item.</summary>
      /// <remarks>id : 20130707°1812 (20130116°1603 20091121°1553)</remarks>
      internal void main_help_viewDocInBrowser()
      {
         string s = "";

         // preparation
         System.Reflection.Assembly asmSource = System.Reflection.Assembly.GetExecutingAssembly();
         string sAsmResourceName = "QueryPonyGui.docs.*"; // wildcard feature (20130708°1202)
         string sTargetFolder = Program.PathConfigDirUser + "\\" + "docs"; // remember finished issue 20130902°0621 'Properties left unset if started from a host'
         string sFullfilename = System.IO.Path.Combine(sTargetFolder, "index.html");

         // determine target files
         IOBus.Utils.Resofile[] resos = { new IOBus.Utils.Resofile(asmSource, sAsmResourceName, sTargetFolder, "")
                                         , new IOBus.Utils.Resofile(asmSource, "QueryPonyGui.docs.img.*", sTargetFolder + "\\" + "img", "")
                                          };

         // determine assembly from which to extract files
         System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

         // perform the extraction
         IOBus.Utils.provideResourceFiles(resos);

         // paranoia
         if (! System.IO.File.Exists(sFullfilename))
         {
            s = "Error - File does not exist: \"" + sTargetFolder + "\".";
            System.Windows.Forms.MessageBox.Show(s);
            return;
         }

         // finally open HTML file in browser (suffix 'html' must be registered on the station)
         try
         {
            // e.g. Process.Start("notepad.exe", "readme.txt");
            System.Diagnostics.Process.Start(sFullfilename);
         }
         catch
         {
            s = "Error showing file \"" + sFullfilename + "\".";
            System.Windows.Forms.MessageBox.Show(s);
         }
      }
   }
}
