#region Fileinfo
// File shutdown 20220806°0921

// file        : 20130831°1611 /QueryPony/QueryPonyGui/SingleFileDeployment.cs
// summary     : Class 'SingleFileDeployment' provides the single-file-deployment feature
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// encoding    : UTF-8-with-BOM
// authors     : ncm
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Diagnostics;                              // class StackFrame
using System.Reflection;                               // For Assembly
using System.Windows.Forms;
using QueryPonyLib;                                    // For Glb.Debag.Execute_No — Seems not possible (log 20190410°0751) compare issue 20130706°2221

namespace QueryPonyGui
{
   /// <summary>This class holds the single-file-deployment feature</summary>
   /// <remarks>
   /// id : class 20130831°1612
   /// </remarks>
   internal static class SingleFileDeployment
   {
      /// <summary>This method facilitates single-file-deployment</summary>
      /// <remarks>
      /// id : method 20130706°1051
      /// see : summary 20190410°0721 'single-file-delivery'
      /// callers : Only • method 20130604°1913 Main()
      /// </remarks>
      internal static void provideSingleFileDeployment()
      {

         // Set event handler to log assembly loadings [seq 20130726°1421]
         //  This satisfies curiosity with debugging issues like 20130707°1011 and 20130726°1231.
         AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
         {
            string s = "";
            Assembly asm = args.LoadedAssembly;
            String sAsmName = asm.FullName;         //       String sFullyQuali = System.Reflection.AssemblyName.GetAssemblyName(@"G:\work\downtown\queryponydev\trunk\QueryPony\QueryPonyLib\bin\x64\Debug\QueryPonyLib.dll").FullName;


            // Log loaded assemblies [seq 20130727°0901] higher level file write methods seem not yet available
            string sTime = " " + DateTime.Now.ToString(QueryPonyLib.Glb.sFormat_Timestamp); //// [chg 20210522°1031`04]
            string sLine = sTime + "  " + "[Debug]" + " " + "Assembly loading: " + sAsmName;
            string sFile = System.IO.Path.Combine(Program.PathConfigDirUser, "logfile.txt");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(sFile, true))
            {
               file.WriteLine(sLine);
            }

            // Log the 'random named' assemblies more detailled [seq 20130808°1531] debug
            // Note : Remember issue 20130731°0131 finding 20130808°1513 'random named assembly'
            //    and compare seq 20130808°1551, where the XML writer was removed.
            if (       sAsmName.StartsWith("WindowsBase")
                ||     sAsmName.StartsWith("QueryPonyLib")
                 ||    sAsmName.StartsWith("iobus")
                  ||   sAsmName.StartsWith("System.Transactions")
                   ||  sAsmName.StartsWith("System.EnterpriseServices")
                    || sAsmName.StartsWith("System.Data.SQLite")
                     )
            {
               return;
            }
            Type[] tt = asm.GetTypes();
            for (int i = 0; i < tt.Length; i++)
            {
               s = "                                     " + tt[i].FullName;
               string sFile2 = System.IO.Path.Combine(Program.PathConfigDirUser, "logfile.txt");
               using (System.IO.StreamWriter file = new System.IO.StreamWriter(sFile2, true))
               {
                  file.WriteLine(s);
               }
            }
            return;
         };


         // Attach the crucial AssemblyResolve eventhandler [seq 20130706°1051`02]
         //  This is the working horse for the single-file-delivery feature
         AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
         {
            // For debugging, see what's available
            //// // This is fine, one of about 21 resources is 'QueryPonyGui.QueryPonyLib.exe' [note 20130707°1002]  // [note 20220731°0912] No. This are 44 and no 'QueryPonyGui.QueryPonyLib.exe'
            string[] arDbg = listAvailableResources("");
            Array.Sort(arDbg);
            String sIgniter = new StackFrame(1).GetMethod().Name;              // Not so helpful [added 20220731°0931] sIgniter = "OnAssemblyResolveEvent"
            if (Globs.Debag.Execute_No)                                        // Toggle Yes/No
            {
               // Evil. This sometimes causes endless recursion, means stack overflow [note 20130707°100202]
               string[] arDbg2 = listAvailableResources(Globs.Resources.AssemblyNameLib); // This contains e.g. 'QueryPonyLib.libs.System.Data.SQLite.dll' [note 20220731°1213]
               Array.Sort(arDbg2);
            }

            // Comfort
            string sAsmName = new System.Reflection.AssemblyName(args.Name).Name;

            // [line 20130127°1531]
            String sResourceName = new System.Reflection.AssemblyName(args.Name).Name;

            // Complete filename [seq 20190410°0633]
            // Blanket name manipulation, e.g. "QueryPonyLib.System.Data.SQLite.dll"
            switch (sResourceName) {
               ////case "QueryPonyLib": sResourceName += ".exe"; break;        // Library inside an executable
               case "QueryPonyLib": sResourceName += ".dll"; break;            // Fix 20220804°0923
               default: sResourceName += ".dll"; break;
            }
            sResourceName = "QueryPonyGui" + "." + sResourceName;              // Supplement namespace

            // Debug [seq 20190410°0741]
            if (IOBus.Gb.Debag.Execute_Yes) {                                  // Toggle flag on demand
               if (sResourceName.Contains("System.Data.SQLite"))
               {
                  MessageBox.Show("Debug 20190410°0741", "Debug");             // Never?
               }
            }

            // Use one of the two possible assemblies [supplement 20130706°1055]
            Assembly asmUse = null;
            if (sResourceName.StartsWith("QueryPonyGui"))
            {
               asmUse = Assembly.GetExecutingAssembly();

               // Provisory manipulation [seq 20130707°1921]
               // Note : Deal with issue 20130708°0711 'MySql.Data.dll assembly name spelling'
               // Todo : Implement a more intelligent generic algorithm [todo 20130708°0911]
               if      (sResourceName == "QueryPonyGui.iobus.dll")              { sResourceName = "QueryPonyGui.libs.iobus.dll"; }  //// Should be superfluous [note 20220731°1212]
               else if (sResourceName == "QueryPonyGui.MySql.Data.dll")         { sResourceName = "QueryPonyGui.libs.mysql.data.dll"; }  // Note the cases a la 20130708°0711
               else if (sResourceName == "QueryPonyGui.Mono.Security.dll")      { sResourceName = "QueryPonyGui.libs.Mono.Security.dll"; }
               else if (sResourceName == "QueryPonyGui.Newtonsoft.Json.dll")    { sResourceName = "QueryPonyGui.libs.Newtonsoft.Json.dll"; }
               else if (sResourceName == "QueryPonyGui.Npgsql.dll")             { sResourceName = "QueryPonyGui.libs.Npgsql.dll"; }
               ////else if (sResourceName == "QueryPonyGui.QueryPonyLib.exe")       { sResourceName = "QueryPonyGui.libs.QueryPonyLib.exe"; } /// Shutdown 20220804°0921
               else if (sResourceName == "QueryPonyGui.QueryPonyLib.dll")       { sResourceName = "QueryPonyGui.libs.QueryPonyLib.dll"; } /// Try fix 20220804°0922
               ////else if (sResourceName == "QueryPonyGui.System.Data.SQLite.dll") { sResourceName = "QueryPonyGui.libs32bit.System.Data.SQLite.dll"; }
               else if (sResourceName == "QueryPonyGui.System.Data.SQLite.dll") {
                  ////sResourceName = "QueryPonyLib.libs.System.Data.SQLite.dll";  // Adjust 20220731°1211
                  return null; // Experiment 20220804°0924 Heureka — Then QueryPonyLib.Resolver.cs will take over.

                  //-----------------------------------
                  //   todo 20220804°0925 'Handle SQLite inside QueryPonyLib only'
                  //   do : Remove all SQLite handling below in this file, and possibly
                  //        above, because this is handled now in QueryPonyLib.Resolver.cs
                  //   ⬞
                  //-----------------------------------
               }
               else
               {
                  // Fatal error
                  // [note 20220731°0921] This sequence executes
                  //   • Before form is visible 2 * with QueryPonyGui.QueryPony.resources.dll
                  //   • Before form is visible 2 * with QueryPonyGui.QueryPony.XmlSerializers.dll
                  //   • After  form is visible 2 * with QueryPonyGui.QueryPony.resources.dll
                  if (Globs.Debag.Execute_No)                                        //
                  {
                     string sMsg = "[Fatal 20220729°1211] Program flow error, theroretically not possible"
                                  + Glb.sCrCr + "sResourceName = " + sResourceName
                                   ;
                     MessageBox.Show(sMsg, "Notification");
                  }
                  return null; // Experimental [line 20220731°0941]
               }
            }
            else
            {
               // Will this branch ever ever be executed? It seems now superfluous [note 20130707°1004]
               asmUse = Assembly.Load("QueryPonyLib");
               if (sResourceName == "QueryPonyLib.MySql.Data.dll")
               {
                  sResourceName = "QueryPonyLib.mysql.data.dll";
               }
            }

            // Fetch the wanted library
            // note : E.g. sResourceName = "QueryPonyLib.MySql.Data.dll" maybe wrong.
            using (var stream = asmUse.GetManifestResourceStream(sResourceName))
            {
               // Experiment [seq 20130707°1036]
               // note : Find out about issue 20130707°1011 'Who is requesting XmlSerializers.dll'
               // nef : Article 'How to list all loaded assemblies' [ref 20130707°1032]
               if (sResourceName == "QueryPonyGui.QueryPony.XmlSerializers.dll")
               {
                  // Note : Block shutdown 20130808°1553 while debugging exception QueryPonyLib.exe20130808°1552
                  if (Globs.Debag.Execute_No)                                  // Glb.Debag.Execute_No seems not possible here
                  {
                     // [line 20130707°1033] Compare method 20210523°1331 listLoadedAssemblies
                     AssemblyName[] asmsNamsDbg = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

                     // [seq 20130707°1034]
                     Assembly asmDbg = Assembly.Load("QueryPonyLib, Version=2.1.2.25112, Culture=neutral, PublicKeyToken=null");
                     string[] sResosDbg = asmDbg.GetManifestResourceNames();
                  }
                  return null;                                                 // Curiously, this seems fine [note 20130707°1035]
               }

               // Finally the actual job — Load assembly
               Assembly asm = null;
               ////if (sResourceName != "QueryPonyLib.libs32bit.System.Data.SQLite.dll") // E.g. sResourceName = "QueryPonyGui.QueryPony.resources.dll" [note 20220729°1141]
               ////if (sResourceName != "QueryPonyGui.libs32bit.System.Data.SQLite.dll") // [fix 20220731°0953] // E.g. sResourceName = "QueryPonyGui.QueryPony.resources.dll" [note 20220729°1141]
               if (sResourceName != "QueryPonyGui.libs.System.Data.SQLite.dll") // [fix 20220731°1141] // E.g. sResourceName = "QueryPonyGui.QueryPony.resources.dll" [note 20220729°1141]
               {
                  // Option 1 — Load straight forward
                  Byte[] assemblyData = new Byte[stream.Length];
                  stream.Read(assemblyData, 0, assemblyData.Length);
                  asm = Assembly.Load(assemblyData);
               }
               else
               {
                  // Option 2 — Load around the corner [seq 20130706°1034]
                  // This solves issue 20130706°1031 'Policy check'
                  // Compare method 20210520°1231 LoadAroundTheCorner in file 20200523°0311 Resolver.cs
                  string sFile1Tmp = Program.PathConfigDirUser + "\\" + sAsmName + ".dll"; // "tmp.bin";
                  using (System.IO.Stream output = System.IO.File.Create(sFile1Tmp))
                  {
                     CopyStream_LOCAL_DUPLICATE(stream, output);
                  }
                  asm = System.Reflection.Assembly.LoadFrom(sFile1Tmp);

                  /*
                  Todo 20130724°0822 'Investigate deleting options for temporary file'
                  Matter : Temporary file is locked as long as the progam is running.
                  Do : Investigate which deleting options are available, perhaps on program exit.
                  Location : seq 20130724°0821 'Cleanup temporary file'
                  Status : Open?
                  */

                  // Cleanup temporary file [seq 20130724°0821]
                  // note : Nice try, but File.Delete() does not work, file is locked. Try later or never?
                  // See : todo 20130724°0822 'Investigate deleting options for temporary file'
                  if (Globs.Debag.Execute_No)                                  // Glb.Debag.Execute_No seems not possible here
                  {
                     System.IO.File.Delete(sFile1Tmp);
                  }
               }

               return asm;
            }
         };
      }

      /// <summary>This method is a debug sequence to list all available resources in an assembly</summary>
      /// <remarks>
      /// id : method 20130706°1052
      /// todo 20130519°1422`02 : Merge this method into some utiliy e.g. method 20130707°1844
      /// ref : 20121230°1512 'MSDN: Suchen von Ressourcennamen'
      /// callers : • method 20130706°1051 provideSingleFileDeployment 2 times • 20130707°1843 provideResourceFiles
      /// </remarks>
      /// <param name="sAssemblyname">The assembly of which we want list the resources</param>
      /// <returns>The wanted array with ressouce name strings</returns>
      public static string[] listAvailableResources(string sAssemblyname)
      {
         System.Reflection.Assembly thisExe = null;
         if (sAssemblyname == "")
         {
            thisExe = Assembly.GetExecutingAssembly();
         }
         else
         {
            // Try envelop to find out about curious recursion (experiment 20130707°1001)
            try
            {
               thisExe = Assembly.Load(sAssemblyname);
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
         return resources;
      }

      /// <summary>This method copies a stream to a file</summary>
      /// <remarks>
      /// id : 20130707°0907 (after 20130702°0531 20130519°1431 20130116°1631)
      /// note : Method quick'n'dirty duplicated here from Utils.cs because we cannot use
      ///    QueryPonyLib here yet. Perhaps it turns out, that it is a bad idea anyway
      ///    to call provideSingleFileDeployment() from this class, then eliminate
      ///    this method. I just call provideSingleFileDeployment() already here in class
      ///    Program, because I am curious, what exactly is the earliest possible moment.
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' [ref 20130116°1623]
      /// </remarks>
      /// <param name="input">The input stream</param>
      /// <param name="output">The output stream</param>
      public static void CopyStream_LOCAL_DUPLICATE(System.IO.Stream input, System.IO.Stream output)
      {
         // Insert null checking here for production
         if (input == null)
         {
            return;
         }
         if (output == null)
         {
            return;
         }

         // Do the wanted task
         byte[] buffer = new byte[8192];
         int bytesRead;
         while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            output.Write(buffer, 0, bytesRead);
         }
      }
   }
}
