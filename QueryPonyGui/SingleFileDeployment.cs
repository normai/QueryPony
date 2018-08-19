#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/SingleFileDeployment.cs
// id          : 20130831°1611
// summary     : This file stores class 'SingleFileDeployment' to provide the single-file-deployment feature.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// encoding    : UTF-8-with-BOM
// authors     : ncm
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Reflection; // Assembly
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This class constitutes the program entry point.</summary>
   /// <remarks>
   /// id : 20130831°1612
   /// </remarks>
   public static class SingleFileDeployment
   {

      /// <summary>This method facilitates single-file-deployment.</summary>
      /// <remarks>
      /// id :  20130706°1051 (20130625°0901 20130116°1701 20121230°1501)
      /// note : Remember issue 20130706°1031 'resource-based library policy check fail'
      /// note : Remember issue 20130706°2221 'FileNotFoundException before program entry'
      /// note : Remember issue 20130726°1411 'what exactly is AppDomain.CurrentDomain'
      /// ref : 20121229°1502 'Jeffrey Richter: Excerpt 2 from CLR ...'
      ///       20121230°1501 'provideSingleFileDeployment() prototype'
      ///       20130706°2201 'MSDN: Understanding The CLR Binder'
      /// </remarks>
      internal static void provideSingleFileDeployment()
      {

         // write assembly loadings to logfile (seqence 20130726°1421)
         // note : Satisfy curiosity while debugging issues like 20130707°1011 and 20130726°1231.
         AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
         {
            string s = "";
            Assembly asm = args.LoadedAssembly;
            String sAsmName = asm.FullName;

            // log loaded assemblies (seqence 20130727°0901) higher level file write methods seem not yet available
            string sTime = " " + DateTime.Now.ToString(QueryPonyLib.Glb.sFormat_Timestamp);
            string sLine = sTime + "  " + "[Debug]" + " " + "Assembly loaded: " + sAsmName;
            string sFile = System.IO.Path.Combine(Program.PathConfigDirUser, "logfile.txt");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(sFile, true))
            {
               file.WriteLine(sLine);
            }

            // log some assemblies more detailled, specifically the 'random named' (sequence 20130808°1531)
            // note : Learn about issue 20130731°0131 finding 20130808°1513 'random named assembly'.
            // finding : Obviously, it is the XML writer, dedicated to the class ServerList.
            //    Now how to explicitly create that (compare experiment 20130808°1551)
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


         // attach the crucial AssemblyResolve eventhandler (sequence 20130706°105102)
         AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
         {

            // for debugging, see what's available
            // This is fine, one of about 21 resources is 'QueryPonyGui.QueryPonyLib.dll'. (note 20130707°1002)
            string[] arDbg = listAvailableResources("");
            if (Globs.Debag.ExecuteNo)
            {
               // This is evil, it causes endless recursion. (note 20130707°100202)
               string[] arDbg2 = listAvailableResources(Globs.Resources.AssemblyNameLib);
            }

            // comfort
            string sAsmName = new System.Reflection.AssemblyName(args.Name).Name;

            // (line 20130127°1531)
            String sResourceName = new System.Reflection.AssemblyName(args.Name).Name;

            // blanket name manipulation, e.g. "QueryPonyLib.System.Data.SQLite.dll"
            sResourceName = "QueryPonyGui" + "." + sResourceName + ".dll";

            // use one of the two possible assemblies (supplement 20130706°1055)
            Assembly asmUse = null;
            if (sResourceName.StartsWith("QueryPonyGui"))
            {
               asmUse = Assembly.GetExecutingAssembly();

               // provisory manipulation (20130707°1921)
               // note : Deal with issue 20130708°0711 'MySql.Data.dll assembly name spelling'
               // todo : Implement a more intelligent generic algorithm (todo 20130708°0911)
               if      (sResourceName == "QueryPonyGui.iobus.dll")              { sResourceName = "QueryPonyGui.libs.iobus.dll"; }
               else if (sResourceName == "QueryPonyGui.MySql.Data.dll")         { sResourceName = "QueryPonyGui.libs.mysql.data.dll"; } // note the cases a la 20130708°0711
               else if (sResourceName == "QueryPonyGui.Mono.Security.dll")      { sResourceName = "QueryPonyGui.libs.Mono.Security.dll"; }
               else if (sResourceName == "QueryPonyGui.Newtonsoft.Json.dll")    { sResourceName = "QueryPonyGui.libs.Newtonsoft.Json.dll"; }
               else if (sResourceName == "QueryPonyGui.Npgsql.dll")             { sResourceName = "QueryPonyGui.libs.Npgsql.dll"; }
               else if (sResourceName == "QueryPonyGui.QueryPonyLib.dll")       { sResourceName = "QueryPonyGui.libs.QueryPonyLib.dll"; }
               else if (sResourceName == "QueryPonyGui.System.Data.SQLite.dll") { sResourceName = "QueryPonyGui.libs.System.Data.SQLite.dll"; }
               else
               {
                  // fatal - program flow error, theroretically not possible
               }
            }
            else
            {
               // Will this branch ever ever be executed? It seems now superfluous. (note 20130707°1004)
               asmUse = Assembly.Load("QueryPonyLib");
               if (sResourceName == "QueryPonyLib.MySql.Data.dll")
               {
                  sResourceName = "QueryPonyLib.mysql.data.dll";
               }
            }

            // fetch the wanted library
            // note : E.g. sResourceName = "QueryPonyLib.MySql.Data.dll" maybe wrong.
            using (var stream = asmUse.GetManifestResourceStream(sResourceName))
            {

               // (experiment 20130707°1036)
               // note : Find out about issue 20130707°1011 'who is requesting XmlSerializers.dll'
               // ref : 20130707°1032 'how to list all loaded assemblies'
               if (sResourceName == "QueryPonyGui.QueryPony.XmlSerializers.dll")
               {
                  // note : block shutdown 20130808°1553 while debugging exception 20130808°1552
                  if (Globs.Debag.ExecuteNo)
                  {
                     // (sequence 20130707°1033)
                     AssemblyName[] asmnamsDbg = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

                     // (sequence 20130707°1034)
                     Assembly asmDbg = Assembly.Load("QueryPonyLib, Version=2.1.2.25112, Culture=neutral, PublicKeyToken=null");
                     string[] sResosDbg = asmDbg.GetManifestResourceNames();
                  }
                  return null; // curiously, this seems fine (20130707°1035)
               }


               // finally the actual job - load assembly
               Assembly asm = null;
               if (sResourceName != "QueryPonyGui.libs.System.Data.SQLite.dll")
               {
                  // option 1 - load straight forward
                  Byte[] assemblyData = new Byte[stream.Length];
                  stream.Read(assemblyData, 0, assemblyData.Length);
                  asm = Assembly.Load(assemblyData);
               }
               else
               {
                  // option 2 - load around the corner (seq 20130706°1034)
                  // This solves issue 20130706°1031 'policy check'
                  string sFile1Tmp = Program.PathConfigDirUser + "\\" + sAsmName + ".dll"; // "tmp.bin";
                  using (System.IO.Stream output = System.IO.File.Create(sFile1Tmp))
                  {
                     CopyStream_LOCAL_DUPLICATE(stream, output);
                  }
                  asm = System.Reflection.Assembly.LoadFrom(sFile1Tmp);

                  // cleanup temporary file (20130724°0821)
                  // note : Nice try, but does not work. Possibly delete it at a later moment or never?
                  // finding : The file is locked as long as the progam is running.
                  // todo : Investigate the situation and see what deleting options are
                  //    available, perhaps on program exit. (todo 20130724°0822)
                  if (Globs.Debag.ExecuteNo)
                  {
                     System.IO.File.Delete(sFile1Tmp);
                  }
               }

               return asm;
            }
         };

      }


      /// <summary>This method is a debug sequence to list all available resources in an assembly.</summary>
      /// <remarks>
      /// id : 20130706°1052 (20130625°0902 20130116°1711)
      /// todo : Outsource/merge this method into some utiliy place e.g.
      ///    method 20130519°1421 from InitRessos.cs [todo 20130519°142202]
      /// ref : 20121230°1512 'MSDN: Suchen von Ressourcennamen'
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
            // try envelop to find out about curious recursion (experiment 20130707°1001)
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

         // recycle sequence if output is wanted as string instead array
         if (Globs.Debag.ShutdownTemporarily) // shutdown to recycle
         {
            string list = "";
            foreach (string resource in resources)
            {
               list += resource + Globs.sCr;
            }
            string s = new System.Reflection.AssemblyName(sAssemblyname).Name;
            // e.g. s = "EnumProgs.enumprogslib.dll\r\nEnumProgs.Properties.Resources.resources\r\nEnumProgs.Form1.resources\r\n"
         }
         return resources;
      }


      /// <summary>This method copies a stream to a file.</summary>
      /// <remarks>
      /// id : 20130707°0907 (20130702°0531 20130116°1631 20130519°1431)
      /// note : Method quick'n'dirty duplicated here from Utils.cs because we cannot use
      ///    QueryPonyLib here yet. Perhaps it turns out, that it is a bad idea anyway
      ///    to call provideSingleFileDeployment() from this class, then eliminate
      ///    this method. I just call provideSingleFileDeployment() already here in class
      ///    Program, because I am curious, what exactly is the earliest possible moment.
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' (20130116°1623)
      /// </remarks>
      /// <param name="input">The input stream</param>
      /// <param name="output">The output stream</param>
      public static void CopyStream_LOCAL_DUPLICATE(System.IO.Stream input, System.IO.Stream output)
      {
         // insert null checking here for production
         if (input == null)
         {
            return;
         }
         if (output == null)
         {
            return;
         }

         // the wanted task
         byte[] buffer = new byte[8192];
         int bytesRead;
         while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            output.Write(buffer, 0, bytesRead);
         }
      }

   }
}
