#region Fileinfo
// file        : 20200523°0311 /QueryPony/QueryPonyLib/Resolver.cs
// summary     : Load embedded assemblies on demand
// license     : GNU AGPL v3
// copyright   : © 2020 - 2022 Norbert C. Maier
// authors     :
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;
using SyIo = System.IO;
using SyRe = System.Reflection;

namespace QueryPonyLib
{
   /// <summary>
   /// This class provides the mechanism to have embedded files extracted at runtime
   /// Compare file 20130831°1611 /QueryPony/QueryPonyGui/SingleFileDeployment.cs
   /// </summary>
   /// <remarks>class 20200523°0321</remarks>
   internal static class Resolver
   {
      /// <summary>
      /// This method registers the AssemblyResolve event handler
      /// </summary>
      /// <remarks>
      /// id : method 20200523°0331
      /// note : See docs.microsoft.com/en-us/archive/blogs/microsoft_press/jeffrey-richter-excerpt-2-from-clr-via-c-third-edition [ref 20200522°0516]
      /// note : See stackoverflow.com/questions/21637830/getmanifestresourcestream-returns-null [ref 20200523°0351]
      /// </remarks>
      internal static void Register()
      {
         // The working horse for single-file-delivery feature [seq 20200523°0341]
         // Compare seq 20130706°1051`02 in SingleFileDeployment.cs
         AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
         {
            Console.WriteLine("AssemblyResolve : " + args.Name);               // E.g. "System.Data.SQLite, Version=1.0.112.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139"

            // Debug [seq 20200523°0343]
            string[] arResos = SyRe.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Console.WriteLine("The list of existing embedded resources :");
            var iCount = 0;
            foreach (string sReso in arResos)
            {
               Console.WriteLine(" - [" +  iCount.ToString() + "] \"" + sReso + "\""); // E.g. "QueryPonyLib.libs.System.Data.SQLite.dll"
               iCount++;
            }

            // Extract file
            String sResoName = new SyRe.AssemblyName(args.Name).Name + ".dll";
            Console.WriteLine("Now extracting : \"" + sResoName + "\"");       // E.g. "AssemblyLoadingAndReflection.System.Data.SQLite.dll"
            using (var stream = SyRe.Assembly.GetExecutingAssembly().GetManifestResourceStream("QueryPonyLib.libs.System.Data.SQLite.dll")) // Remember ref 20200523°0351
            {
               Byte[] assemblyData = new Byte[stream.Length];
               string sGetName = "N/A";
               SyRe.Assembly asmLoaded = null;
               try
               {
                  // See issue 20200523°0411 'FileLoadException unverifiable executable'.
                  if (sResoName != "System.Data.SQLite.dll") {
                     stream.Read(assemblyData, 0, assemblyData.Length);
                     asmLoaded = SyRe.Assembly.Load(assemblyData);     // Will not work with System.Data.SQLite.dll
                  }
                  else {
                     asmLoaded = LoadAroundTheCorner(stream, assemblyData);
                  }
                  sGetName = asmLoaded.GetName().Name;
                  return asmLoaded;
               }
               catch (Exception excpt)
               {
                  // Remember issue 20200523°0411 'Attempt to load an unverifiable executable'
                  Console.WriteLine("Assembly.Load(" + sResoName + ") with " + excpt.GetType().Name); // E.g. "System.Data.SQLite.dll"
               }
               System.Threading.Thread.Sleep(777);
               Console.WriteLine("Loading? " + sGetName);
               return null;                                                    // Type SyRe.Assembly()
            }
         };

         // Register eventhandler [seq 20200523°0511]
         // The AssemblyLoad event is fired after the AssemblyResolve event,
         //  if that one failed loading and instead threw the exception.
         AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
         {
            if (Glb.Debag.Tell_AssemblyLoad_Event)
            {
               string sAsmbly = args.LoadedAssembly.FullName;                  // Same as args.LoadedAssembly.GetName()
               sAsmbly = IOBus.Utils.Strings.Ram(sAsmbly, 84);
               IOBusConsumer._writeHostLine(" * LoadedAssembly = " + sAsmbly);
            }
         };
      }

      /// <summary>
      /// This loads an assembly not directly from the Bytes array into memory,
      ///  but first extracts the bytes into a file, and then loads it. This is a
      ///  workaound for issue 20200523°0411 'FileLoadException unverifiable executable'.
      /// </summary>
      /// <remarks>
      /// id : method 20210520°1231
      /// Compare file 20130831°1611 SingleFileDeployment.cs, seq 20130706°1034
      /// </remarks>
      private static SyRe.Assembly LoadAroundTheCorner(System.IO.Stream stream, Byte[] asmData)
      {
         SyRe.Assembly asmbly = null;

         // Get a folder name [seq 20210520°1241 (after 20130902°0642)]
         string sPathConfigFileUser = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
         string sTmpDir = System.IO.Path.GetDirectoryName(sPathConfigFileUser);
         if (!SyIo.Directory.Exists(sTmpDir))
         {
            SyIo.DirectoryInfo di = SyIo.Directory.CreateDirectory(sTmpDir);
         }

         // Create the file
         string sFile1Tmp = sTmpDir + "\\" + "System.Data.SQLite.dll";
         using (SyIo.Stream output = SyIo.File.Create(sFile1Tmp))
         {
            Utils.CopyStream(stream, output);
         }
         asmbly = System.Reflection.Assembly.LoadFrom(sFile1Tmp);

         // Cleanup temporary file [seq 20130724°0821]
         // note : Nice try, but File.Delete() does not work, file is locked. Try later or never?
         // See : Todo 20130724°0822 'Investigate deleting options for temporary file'
         /*
         if (Globs.Debag.Execute_No)                                           // Glb.Debag.Execute_No seems not possible here
         {
            System.IO.File.Delete(sFile1Tmp);
         }
         */

         return asmbly;
      }
   }
}
