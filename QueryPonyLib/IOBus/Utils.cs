#region Fileinfo
// file        : 20130707°1841 /QueryPony/IOBus/Utils.cs
// summary     : This file stores class 'Utils' and some subclasses/structs to provide miscellaneous methods.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace IOBus
{
   /// <summary>This class provides miscellaneous methods</summary>
   /// <remarks>id : 20130707°1842</remarks>
   public static class Utils
   {
      /// <summary>This subclass provides miscellaneous pathes infos</summary>
      /// <remarks>id : 20130905°0911</remarks>
      public static class Pathes
      {
         /// <summary>This method retrieves the folder where the executable resides</summary>
         /// <remarks>id : 20130905°0912 (20130605°1720 20130604°0952)</remarks>
         /// <returns>The wanted executable path</returns>
         public static string ExecutableFullFolderName()
         {
            string s = "";
            s = ExecutableFullFileName();
            s = System.IO.Path.GetDirectoryName(s);
            return s;
         }

         /// <summary>This method retrieves the executable's fullname, means path plus filename</summary>
         /// <remarks>id : 20130905°0913</remarks>
         /// <returns>The wanted executable's full filename</returns>
         public static string ExecutableFullFileName()
         {
            string s = "";

            // [seq 20130905°0914]
            if (IOBus.Gb.Debag.Shutdown_Archived)
            {
               s = System.Reflection.Assembly.GetExecutingAssembly().Location;
               // E.g "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\iobus.dll"

               s = System.Reflection.Assembly.GetCallingAssembly().Location;
               // E.g. "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\trektrols.dll"
            }

            s = System.Reflection.Assembly.GetEntryAssembly().Location;
            // E.g. "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\trekta.exe"

            return s;
         }
      }

      /// <summary>This struct stores one resource file plus it's target folder</summary>
      /// <remarks>id : 20130707°1901 (20130605°1511)</remarks>
      public struct Resofile
      {
         /// <summary>This field stores the assembly from which the file shall be extracted</summary>
         /// <remarks>
         /// id : 20130709°1411
         /// note : This field is probably redundant. The source assembly could as well be
         ///    known from the Resource Filename ... but I am not sure how to extract it
         ///    from that string, so I quick'n'dirty provide this field. [note 20130709°1412]
         /// note : This field was introduced with sequence 20130709°1351 'provide demo database',
         ///    where I first time send an extraction request list with mixed-source-assemblies
         ///    (files from QueryPonyGui and QueryPonyLib). [note 20130709°1413]
         /// </remarks>
         public System.Reflection.Assembly SourceAssembly;

         /// <summary>This field stores the assembly resource name, e.g. 'QueryPonyGui.docs.joesgarage.sqlite3'</summary>
         /// <remarks>id : 20130707°1902</remarks>
         public String ResoFilename;

         /// <summary>This field stores the target folder, e.g. 'c:\tmp\docs'</summary>
         /// <remarks>id : 20130707°1903</remarks>
         public String TargetFolder;

         /// <summary>This field stores the target filename under which it will be stored on the drive, e.g. 'index.html'</summary>
         /// <remarks>id : 20130707°1904</remarks>
         public String TargetFilename;

         /// <summary>This constructor creates a new Resofile object</summary>
         /// <remarks>id : 20130707°1905</remarks>
         public Resofile(System.Reflection.Assembly asmSource, string sResoName, string sTargetFolder, string sTargetFilename)
         {
            SourceAssembly = asmSource;
            ResoFilename = sResoName;
            TargetFolder = sTargetFolder;
            TargetFilename = sTargetFilename;
         }
      }

      /// <summary>This subclass provides miscellaneous string methods</summary>
      /// <remarks>id : 20130719°0931</remarks>
      public static class Strings
      {
         /// <summary>This method shortens a long string to be better suited as display text by cutting the middle</summary>
         /// <remarks>
         /// Ident : 20130719°0951
         /// Note : This method might be useful for similar purposes with other objects as well.
         /// Todo : The algorithm is not pinpoint, the parameters are not scrutinized. Make it foolproof. [todo 20210523°1521]
         /// Note : Remember possible ellipsis • "…" • System.Drawing.StringTrimming.EllipsisCharacter;
         /// </remarks>
         /// <param name="sLong">The long string possibly to be shortened</param>
         /// <param name="iMaxLen">The maximum wanted string length or 0 for a default value</param>
         /// <returns>The wanted possibly shortened string, if shortened then with ellipsis dots in the middle</returns>
         public static string Ram(string sLongText, int iMaxLen = 32, string sEllipsis = " .. ")
         {
            string sRet = sLongText;

            // Paranoia
            sRet = sRet ?? "";                                                 // Coalesce expression, since C# 8.0, instead 'sRet = (sRet == null) ? "" : sRet;' [line 20210523°1511]

            // Calculate
            int iHalfMaxLen = (iMaxLen - sEllipsis.Length) / 2;

            // Ram [seq 20130719°0954]
            ////if (sRet.Length > iMaxLen)
            if (sRet.Length > iMaxLen)
            {
               string s1 = sRet.Substring(0, iHalfMaxLen);
               string s2 = sRet.Substring(sRet.Length - iHalfMaxLen, iHalfMaxLen);

               // Recombine
               sRet = s1 + sEllipsis + s2;
            }

            // Ready
            return sRet;
         }

         /// <summary>This method wraps a string in the given backticks (or others) if it contains the given bad characters</summary>
         /// <remarks>id : 20130719°0932</remarks>
         /// <param name="sToken">The string to possibly be wrapped</param>
         /// <param name="sBadChars">The characters which cause wrapping</param>
         /// <param name="sWapper">The leading and the trailing wrapper character</param>
         /// <returns>The wanted possibly wrapped string</returns>
         public static string SqlTokenTicks(string sToken, string sBadChars, string sWapper)
         {
            string s = "";
            string sTok = sToken;

            // (.1) Standard tikking [seq 20130719°0937]
            // Remember issue 20130719°1441 'IOBus output to status line'
            if (sWapper.Length != 2)
            {
               // Paranoia invalid parameter
               s = "Error in method SqlTokenTicks(), parameter sWapper must be two characters wide"
                  + " but it is \"" + sWapper + "\""
                   ;
               IOBus.Dialogs.dialogErr(s);
            }
            else
            {
               for (int i = 0; i < sBadChars.Length; i++)
               {
                  if (sTok.Contains(sBadChars.Substring(i, 1)))
                  {
                     sTok = sWapper.Substring(0, 1) + sTok + sWapper.Substring(1, 1);
                  }
               }
            }

            // (.2) Tick specific words — See issue 20130824°0913 'keywords as SQLite field names'
            s = sTok.ToLower();
            if ((s == Gb.Sql.Default)
                || (s == Gb.Sql.Exists)
                 || (s == Gb.Sql.From)
                  || (s == Gb.Sql.Currency)
                   )
            {
               sTok = sWapper.Substring(0, 1) + sTok + sWapper.Substring(1, 1);
            }

            // (.3) Mangle names beginning with number [seq 20130824°1221]
            // See issue 20130824°0915 'names beginning with ciphers'
            if ((sTok.Length > 0) && Char.IsDigit(sTok, 0))                    // Fix issue 20200928°1311 'IsDigit throws if sTok is empty'
            {
               sTok = sWapper.Substring(0, 1) + sTok + sWapper.Substring(1, 1);
            }

            return sTok;
         }
      }

      /// <summary>This method splits a combined URL into the plain URL and the portnumber</summary>
      /// <remarks>id : 20130716°0611</remarks>
      /// <param name="sUrlFull">The given potentially combined URL</param>
      /// <param name="sUrlPlain">The wanted plain URL</param>
      /// <param name="iPortnumber">The wanted portnumber</param>
      /// <returns>Possible error message</returns>
      public static string extractPortnumberFromUrl(string sUrlFull, out string sUrlPlain, out int iPortnumber) // "Warning CS3001: Argument type 'out uint' is not CLS-compliant .." [fix 20180819°0121]
      {
         string sRet = "";
         sUrlPlain = "";
         iPortnumber = 0;

         // Process and set port number [seq 20130709`0941]
         string[] ar = sUrlFull.Split(':');
         if (ar.Length == 1)
         {
            // Server is e.g. "127.0.0.1", "localhost", supplement port explicitly. Not sure whether this is necessary
            sUrlPlain = ar[0];
            iPortnumber = 0;                                                   // 3306
         }
         else if (ar.Length == 2)
         {
            // Port was given with the server url, e.g. "127.0.0.1:3307"
            sUrlPlain = ar[0];
            int ui = 0;
            try
            {
               ui = int.Parse(ar[1]);
            }
            catch (System.Exception ex)
            {
               // Todo : Supplement fatal error processing. [todo 20130709°0943]
               sRet = ex.Message;
            }
            sUrlPlain = ar[0];
            iPortnumber = ui;
         }
         else
         {
            // Todo : Supplement fatal error processing [todo 20130709°0942]
            sRet = "Something is wrong with URL '" + sUrlFull + "'.";
         }
         return sRet;
      }

      /// <summary>This method is a debug sequence to list all available resources in an assembly</summary>
      /// <remarks>
      /// id : method 20130707°1844
      /// todo : Merge here nearly identical method 20130706°1052 [todo 20130519°1422]
      /// ref : 20121230°1512 'MSDN: Suche Ressourcennamen in Assembly'
      /// callers : • method 20130707°1843 provideResourceFiles
      /// </remarks>
      /// <param name="sAssemblyname">The assembly from which to list resources</param>
      /// <returns>The wanted array with assembly name strings</returns>
      public static string[] listAvailableResources(string sAssemblyname)
      {
         System.Reflection.Assembly thisExe = null;
         if (sAssemblyname == "")
         {
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();       // This will always be '..IOBus..'
         }
         else
         {
            thisExe = System.Reflection.Assembly.Load(sAssemblyname);
         }
         string[] resources = thisExe.GetManifestResourceNames();

         // Recycle sequence if output is wanted as string instead array
         if (IOBus.Gb.Debag.Shutdown_To_Recycle)
         {
            string list = "";
            foreach (string resource in resources)
            {
               list += resource + IOBus.Gb.Bricks.Cr;
            }
            string s2 = new System.Reflection.AssemblyName(sAssemblyname).Name;
            // List e.g. = "EnumProgs.enumprogslib.dll\r\nEnumProgs.Properties.Resources.resources\r\nEnumProgs.Form1.resources\r\n"
         }

         return resources;
      }

      /// <summary>This method extracts the resourcefiles from the assembly to the application folder (if not already done)</summary>
      /// <remarks>
      /// id : 20130707°1843 (after ? 20130116°1751)
      /// Todo : Merge here nearly identical method 20130116°1751 [todo 20130519°1412]
      /// See : ref 20130116°1623 Method after 'Jon Skeet: Write file from assembly to disk'
      /// See : note 20130707°1855 'shorter sequence with .NET 4.0'
      /// See : Todo 20130707°1855 'Shorter sequence with .NET 4.0' — Is it worked off? Check it out!
      /// Callers :
      /// </remarks>
      public static void provideResourceFiles(Resofile[] resos)
      {
         string s = string.Empty;

         // Extract embedded files [seq 20130116°1621]
         string sAsmName_ = resos[0].SourceAssembly.FullName;
         string[] arDbg = listAvailableResources(sAsmName_);

         // Loop over array with the requested files and possibly expand wildcards
         IOBus.Utils.Resofile[] resosExpa = { };
         for (int iReq = 0; iReq < resos.Length; iReq++)
         {
            // Guarantee directory
            if (!System.IO.Directory.Exists(resos[iReq].TargetFolder))
            {
               System.IO.Directory.CreateDirectory(resos[iReq].TargetFolder);
            }

            // Extract with wildcard (feature 20130708°1201)
            // Note : If the resource ends with the wildcard, the target filename will be
            //    ignored (it may be empty), it will be deduced from the resource name then.
            System.Collections.ArrayList alResos = new System.Collections.ArrayList();

            // Expand?
            if (resos[iReq].ResoFilename.EndsWith("*"))
            {
               for (int iReso = 0; iReso < arDbg.Length; iReso++)
               {
                  if (arDbg[iReso].StartsWith(resos[iReq].ResoFilename.TrimEnd('*')))
                  {
                     string sTgtfile = arDbg[iReso].Substring(resos[iReq].ResoFilename.Length - 1);

                     Array.Resize(ref resosExpa, resosExpa.Length + 1);
                     Resofile rf = new Resofile(resos[iReq].SourceAssembly, arDbg[iReso], resos[iReq].TargetFolder, sTgtfile);
                     resosExpa[resosExpa.Length - 1] = rf;
                  }
               }
            }
            else
            {
               Array.Resize(ref resosExpa, resosExpa.Length + 1);
               resosExpa[resosExpa.Length - 1] = resos[iReq];
            }
         }

         // Loop over array with the requested files and possibly expand wildcards
         for (int iXtrct = 0; iXtrct < resosExpa.Length; iXtrct++)
         {
            // Provide full filename in advanced
            string sFullfile = System.IO.Path.Combine(resosExpa[iXtrct].TargetFolder, resosExpa[iXtrct].TargetFilename);

            //---------------------------------------------------------
            // Note 20130707°1853 : Now, after this method is outsourced to the IOBus library,
            //    getting the assemybly by 'Assembly asm = Assembly.GetExecutingAssembly();' is of
            //    no use anymore, since will always be "iobus, Version=0.2.4.30314, Culture=neutral,
            //    PublicKeyToken=null". We always need the assembly given explicitly.
            //---------------------------------------------------------

            // Do the extraction [seq 20130116°1625]
            if (!System.IO.File.Exists(sFullfile))
            {
               using (System.IO.Stream input = resosExpa[iXtrct].SourceAssembly.GetManifestResourceStream(resosExpa[iXtrct].ResoFilename))
               {
                  //-------------------------------------------
                  // Note 20130709°1352 'Remember exception'
                  // If e.g. target filename is blank, then the below given filename is a directory, then below
                  //  line throws exeption (facecd when extracting joesgarage.sqlite from ConnectionSettingsGui()
                  //  but forgot setting target filename). Exception 'System.UnauthorizedAccessException',
                  //  Message="Access to the path 'C:\\Documents and Settings\\Frank\\Local Settings\\
                  //  Application Data\\www.trilo.de\\QueryPony.exe_Url_euqpzyfib35yvw55i13etxckcivppvbm\\
                  //  0.1.0.1561\\docs' is denied."
                  //-------------------------------------------

                  using (System.IO.Stream output = System.IO.File.Create(sFullfile))
                  {
                     CopyStream(input, output);
                  }
               }
            }
         }
      }

      /// <summary>This method copies a stream to a file</summary>
      /// <remarks>
      /// id : 20130707°1845 (20130519°1431 20130116°1631 20130702°0531)
      /// todo : Merge here identical methods 20130116°1631, 20130519°1431,
      ///         20130702°0531 QueryPony::Utils.cs (todo 20130519°1432)
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' [ref 20130116°1623]
      /// callers : provideResourceFiles
      /// </remarks>
      /// <param name="input">The input stream</param>
      /// <param name="output">The output stream</param>
      private static void CopyStream(System.IO.Stream input, System.IO.Stream output)
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

         // Perform the finally wanted task
         byte[] buffer = new byte[8192];
         int bytesRead;
         while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            output.Write(buffer, 0, bytesRead);
         }
      }
   }
}
