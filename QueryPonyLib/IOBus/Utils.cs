#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/IOBus/Utils.cs
// id          : 20130707°1841
// summary     : This file stores class 'Utils' and some subclasses/structs to provide miscellaneous methods.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace IOBus
{

   /// <summary>This class provides miscellaneous methods.</summary>
   /// <remarks>id : 20130707°1842</remarks>
   public static class Utils
   {

      /// <summary>This subclass provides miscellaneous pathes infos.</summary>
      /// <remarks>id : 20130905°0911</remarks>
      public static class Pathes
      {

         /// <summary>This method retrieves the folder where the executable resides.</summary>
         /// <remarks>id : 20130905°0912 (20130605°1720 20130604°0952)</remarks>
         /// <returns>The wanted executable path</returns>
         ////private static string GetExecPath()
         public static string ExecutableFullFolderName()
         {
            string s = "";
            s = ExecutableFullFileName();
            s = System.IO.Path.GetDirectoryName(s);
            return s;
         }


         /// <summary>This method retrieves the executable's fullname (path plus filename).</summary>
         /// <remarks>id : 20130905°0913</remarks>
         /// <returns>The wanted executable's full filename</returns>
         public static string ExecutableFullFileName()
         {
            string s = "";

            // (seq 20130905°0914)
            if (IOBus.Gb.Debag.ShutdownArchived)
            {
               s = System.Reflection.Assembly.GetExecutingAssembly().Location;
               // e.g "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\iobus.dll"

               s = System.Reflection.Assembly.GetCallingAssembly().Location;
               // e.g. "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\trektrols.dll"
            }

            s = System.Reflection.Assembly.GetEntryAssembly().Location;
            // e.g. "G:\work\downtown\queryponydev\trunk\QueryPonyGui\bin\x86\Debug\trekta.exe"

            return s;
         }
      }


      /// <summary>This subclass provides miscellaneous string methods.</summary>
      /// <remarks>id : 20130719°0931</remarks>
      public static class Strings
      {

         /// <summary>This method wraps a string in the given backticks (or others) if it contains the given bad characters.</summary>
         /// <remarks>id : 20130719°0932</remarks>
         /// <param name="sToken">The string to possibly be wrapped</param>
         /// <param name="sBadChars">The characters which cause wrapping</param>
         /// <param name="sWapper">The leading and the trailing wrapper character</param>
         /// <returns>The wanted possibly wrapped string</returns>
         public static string SqlTokenTicks(string sToken, string sBadChars, string sWapper)
         {
            string s = "";
            string sTok = sToken;

            // (.1) standard tikking
            if (sWapper.Length != 2)
            {
               // (issue 20130719°1441)
               // title : How can IOBus itself output to the status line?
               // descript : Outputting to the status line is not so easy from
               //    inside IOBus. Nor can IOBus use it's own delegate, nor has
               //    it a reference back to the GUI, which could do the output.
               // workaround : Retreat to the unloved MessageBox.Show().
               // status : Solved reasonable
               // priority : low

               // paranoia invalid parameter
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

            // (.2) tick specific words (see issue 20130824°0913 'keywords as SQLite field names')
            s = sTok.ToLower();
            if (     (s == Gb.Sql.Default)
                ||   (s == Gb.Sql.Exists)
                 ||  (s == Gb.Sql.From)
                  || (s == Gb.Sql.Currency)
                   )
            {
               sTok = sWapper.Substring(0, 1) + sTok + sWapper.Substring(1, 1);
            }

            // (.3) mangle names beginning with number (seq 20130824°1221)
            // See issue 20130824°0915 'names beginning with ciphers'
            if (Char.IsDigit(sTok, 0))
            {
               ////sTok = IOBus.Gb.Bricks.Backtick + sTok + IOBus.Gb.Bricks.Backtick; // "`"
               sTok = sWapper.Substring(0, 1) + sTok + sWapper.Substring(1, 1);
            }

            return sTok;
         }


         /// <summary>This method shortens a long string to be better suited as display text by cutting the middle.</summary>
         /// <remarks>
         /// id : 20130719°0951
         /// note : This method might be useful for similar purposes with other objects as well.
         /// </remarks>
         /// <param name="sLong">The long string possibly to be shortened</param>
         /// <param name="iMaxLen">The maximum wanted string length or 0 for a default value</param>
         /// <returns>The wanted possibly shortened string, if shortened then with ellipsis dots in the middle</returns>
         public static string ShortenDisplayString(string sLongText, int iMaxLen)
         {

            const int iMaxLenDefault = 32;
            string sRet = sLongText;

            // paranoia
            if (sRet == null)
            {
               sRet = "";
            }

            // paranoia or default value, respectively
            if (iMaxLen < 4)
            {
               iMaxLen = iMaxLenDefault;
            }
            int iHalfMaxLen = iMaxLen / 2;

            if (sRet.Length > iMaxLen)
            {
               string s1 = sRet.Substring(0, iHalfMaxLen);
               string s2 = sRet.Substring(sRet.Length - iHalfMaxLen, iHalfMaxLen);

               // (sequence 20130719°0954)
               if (Gb.Debag.ShutdownAlternatively)
               {
                  sRet = s1 + "..." + s2;
               }
               else if (Gb.Debag.ShutdownAlternatively)
               {
                  sRet = s1 + System.Drawing.StringTrimming.EllipsisCharacter + s2;
               }
               else
               {
                  sRet = s1 + "…" + s2;
               }
            }

            return sRet;
         }
      }


      /// <summary>This struct stores one resource file plus it's target folder.</summary>
      /// <remarks>id : 20130707°1901 (20130605°1511)</remarks>
      public struct Resofile
      {
         /// <summary>This field stores the assembly from which the file shall be extracted.</summary>
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

         /// <summary>This field stores the assembly resource name, e.g. 'QueryPonyGui.docs.joesgarage.sqlite3'.</summary>
         /// <remarks>id : 20130707°1902</remarks>
         public String ResoFilename;

         /// <summary>This field stores the target folder, e.g. 'c:\tmp\docs'.</summary>
         /// <remarks>id : 20130707°1903</remarks>
         public String TargetFolder;

         /// <summary>This field stores the target filename under which it will be stored on the drive, e.g. 'index.html'.</summary>
         /// <remarks>id : 20130707°1904</remarks>
         public String TargetFilename;

         /// <summary>This constructor creates a new Resofile object.</summary>
         /// <remarks>id : 20130707°1905</remarks>
         public Resofile(System.Reflection.Assembly asmSource, string sResoName, string sTargetFolder, string sTargetFilename)
         {
            SourceAssembly = asmSource;
            ResoFilename = sResoName;
            TargetFolder = sTargetFolder;
            TargetFilename = sTargetFilename;
         }
      }


      /// <summary>This method extracts the resourcefiles from the assembly to the application folder (if not already done).</summary>
      /// <remarks>
      /// id : 20130707°1843 (20130519°1411 20130116°1751)
      /// todo : Merge here nearly identical method 20130116°1751 (todo 20130519°1412)
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' (20130116°1623)
      /// note : The following even shorter sequence does not work for us because
      ///    Stream.CopyTo() exists in .NET 4.0, not yet 3.5 (sequence 20130116°1624):
      ///    //------------------------------------------------
      ///    //using (Stream stream = new FileStream(sTarget, FileMode.Create))
      ///    //{
      ///    //   Assembly.GetExecutingAssembly().GetManifestResourceStream("[Project].[File]").CopyTo(stream);
      ///    //}
      ///    //------------------------------------------------
      /// callers :
      /// </remarks>
      public static void provideResourceFiles(Resofile[] resos)
      {
         string s = string.Empty;

         // extract embedded files (sequence 20130116°1621)
         string sAsmName_ = resos[0].SourceAssembly.FullName;
         string[] arDbg = listAvailableResources(sAsmName_);

         // loop over array with the requested files and possibly expand wildcards
         IOBus.Utils.Resofile[] resosExpa = { };
         for (int iReq = 0; iReq < resos.Length; iReq++)
         {

            // garantee directory
            if (! System.IO.Directory.Exists(resos[iReq].TargetFolder))
            {
               System.IO.Directory.CreateDirectory(resos[iReq].TargetFolder);
            }

            // extract with wildcard (feature 20130708°1201)
            // note : If the resource ends with the wildcard, the target filename will be
            //    ignored (it may be empty), it will be deduced from the resource name then.
            System.Collections.ArrayList alResos = new System.Collections.ArrayList();

            // expand?
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


         // loop over array with the requested files and possibly expand wildcards
         for (int iXtrct = 0; iXtrct < resosExpa.Length; iXtrct++)
         {

            // provide full filename in advanced
            string sFullfile = System.IO.Path.Combine(resosExpa[iXtrct].TargetFolder, resosExpa[iXtrct].TargetFilename);

            //---------------------------------------------------------
            // (note 20130707°1853) Now, after this method is outsourced to the IOBus library,
            //    getting the assemybly by 'Assembly asm = Assembly.GetExecutingAssembly();' is of
            //    no use anymore, since will always be "iobus, Version=0.2.4.30314, Culture=neutral,
            //    PublicKeyToken=null". We always need the assembly given explicitly.
            //---------------------------------------------------------

            // do the extraction (sequence 20130116°1625)
            if (! System.IO.File.Exists(sFullfile))
            {
               using (System.IO.Stream input = resosExpa[iXtrct].SourceAssembly.GetManifestResourceStream(resosExpa[iXtrct].ResoFilename))
               {
                  //-------------------------------------------
                  // (note 20130709°1352 remember exception)
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


      /// <summary>This method is a debug sequence to list all available resources in an assembly.</summary>
      /// <remarks>
      /// id : 20130707°1844 (20130519°1421 20130116°1711)
      /// todo : Merge here nearly identical method 20130116°1711 (todo 20130519°1422)
      /// ref : 20121230°1512 'MSDN: Suche Ressourcennamen in Assembly'
      /// callers :
      /// </remarks>
      /// <param name="sAssemblyname">The assembly from which to list resources</param>
      /// <returns>The wanted array with assembly name strings</returns>
      public static string[] listAvailableResources(string sAssemblyname)
      {
         System.Reflection.Assembly thisExe = null;
         if (sAssemblyname == "")
         {
            thisExe = System.Reflection.Assembly.GetExecutingAssembly(); // this will always be '..IOBus..'
         }
         else
         {
            thisExe = System.Reflection.Assembly.Load(sAssemblyname);
         }
         string[] resources = thisExe.GetManifestResourceNames();


         // recycle sequence if output is wanted as string instead array
         if (IOBus.Gb.Debag.ShutdownToRecycle)
         {
            string list = "";
            foreach (string resource in resources)
            {
               list += resource + IOBus.Gb.Bricks.Cr;
            }
            string s2 = new System.Reflection.AssemblyName(sAssemblyname).Name;
            // list e.g. = "EnumProgs.enumprogslib.dll\r\nEnumProgs.Properties.Resources.resources\r\nEnumProgs.Form1.resources\r\n"
         }

         return resources;
      }


      /// <summary>This method copies a stream to a file.</summary>
      /// <remarks>
      /// id : 20130707°1845 (20130519°1431 20130116°1631 20130702°0531)
      /// todo : Merge here identical methods 20130116°1631, 20130519°1431,
      ///         20130702°0531 QueryPony::Utils.cs (todo 20130519°1432)
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' (20130116°1623)
      /// callers : provideResourceFiles
      /// </remarks>
      /// <param name="input">The input stream</param>
      /// <param name="output">The output stream</param>
      private static void CopyStream(System.IO.Stream input, System.IO.Stream output)
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

         // perform the finally wanted task
         byte[] buffer = new byte[8192];
         int bytesRead;
         while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            output.Write(buffer, 0, bytesRead);
         }
      }


      /// <summary>This method splits a combined URL into the plain URL and the portnumber</summary>
      /// <remarks>id : 20130716°0611</remarks>
      /// <param name="sUrlFull">The given potentially combined URL</param>
      /// <param name="sUrlPlain">The wanted plain URL</param>
      /// <param name="iPortnumber">The wanted portnumber</param>
      /// <returns>Possible error message</returns>
      //////public static string extractPortnumberFromUrl(string sUrlFull, out string sUrlPlain, out uint uiPortnumber)
      public static string extractPortnumberFromUrl(string sUrlFull, out string sUrlPlain, out int iPortnumber) // fix 20180819°0121 for "Warning CS3001: Argument type 'out uint' is not CLS-compliant"
      {
         string sRet = "";
         sUrlPlain = "";
         iPortnumber = 0;


         // process and set port number (sequence 20130709.0941)
         string[] ar = sUrlFull.Split(':');
         if (ar.Length == 1)
         {
            // server is e.g. "127.0.0.1", "localhost", supplement port explicitly (not sure whether this is necessary)
            sUrlPlain = ar[0];
            iPortnumber = 0;                                  // 3306
         }
         else if (ar.Length == 2)
         {
            // port was given with the server url, e.g. "127.0.0.1:3307"
            sUrlPlain = ar[0];
            //////uint ui = 0;
            int ui = 0;
            try
            {
               //////ui = uint.Parse(ar[1]);
               ui = int.Parse(ar[1]);
            }
            catch (System.Exception ex)
            {
               // todo : Supplement fatal error processing. (todo 20130709.0943)
               sRet = ex.Message;
            }
            sUrlPlain = ar[0];
            //////uiPortnumber = ui;
            iPortnumber = ui;
         }
         else
         {
            // todo : Supplement fatal error processing (todo 20130709°0942)
            sRet = "Something is wrong with URL '" + sUrlFull + "'.";
         }

         return sRet;
      }
   }
}
