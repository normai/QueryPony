#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/Utils.cs
// id          : 20130625°0911 (20130604°0351)
// summary     : This file stores class 'Utils' to provide file processing and other methods.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Specialized;

namespace QueryPonyLib
{

   /// <summary>This class provides file processing and other methods.</summary>
   /// <remarks>
   /// id : 20130604°0352
   /// todo : Shift or merge most of the methods from here to IOBus::Utils. (todo 20130709°0933)
   /// </remarks>
   public class Utils
   {

      // (property outcommented 20130719.0813 and re-activated)
      //  This property was used only in four methods getConnectStringFromFileContent()
      //  and seemed to make not much sense. But oops, the privat part of it is used in
      //  this class, this makes more sense.
      // / *
      /// <summary>This private static field stores the error message created in a catch block.</summary>
      /// <remarks>id : 20130604°0353</remarks>
      static string error = "";


      /// <summary>This property gets a description of the last error encountered.</summary>
      /// <remarks>id : 20130604°0354</remarks>
      public static string Error
      {
         get { return error; }
      }
      // * /


      /// <summary>This method appends one line to the given textfile.</summary>
      /// <remarks>id : 20130625°0931 (20051109°1802)</remarks>
      /// <param name="sLine">The line to output.</param>
      /// <returns>Success flag</returns>
      public static bool outputLine(string sLine)
      {
         // (sequence 20130711°0912) possibly create output folder
         // note : If we output a line at a too early moment (e.g. sequence 20130711°0912),
         //    the Settings have not yet created their folder, so we have to do it here.
         if (! System.IO.Directory.Exists(InitLib.SettingsDir))
         {
            // pretty courageous, doing creating a folder without feedback about success or not, but
            //  the path being told from the Settings machinery might justify a little confidence
            System.IO.Directory.CreateDirectory(InitLib.SettingsDir);
         }


         // () garantee appropriate filename
         ////string sFileOut = InitLib.SettingsDir + Glb.sDelim_PathBkslsh + "logfile.txt";
         ////string sFileOut = InitLib.SettingsDir + IOBus.Gb.Bricks.PathBkslsh + "logfile.txt";
         string sFileOut = InitLib.PathLogfile;

         // build final output string
         string sWrite = " " + getTimestamp() + "  " + sLine + Glb.sCr;

         // () provide file handle
         System.IO.StreamWriter swFileOut = System.IO.File.AppendText(sFileOut);

         // () write output
         swFileOut.Write(sWrite);

         // () update the underlying file
         swFileOut.Flush();

         // () close the writer and it's last underlying file
         swFileOut.Close();

         return true;
      }


      /// <summary>This method builds and returns a timestamp string.</summary>
      /// <remarks>id : 20130625°0932 (20051110°2201)</remarks>
      /// <returns>The wanted timestamp string</returns>
      public static String getTimestamp()
      {
         // () datetime format
         DateTime dtNow = new DateTime(9999, 12, 31, 23, 59, 59);
         String sRet = "";

         // () build timestamp
         dtNow = DateTime.Now;
         ////sRet = dtNow.ToLongDateString() + " " + dtNow.ToLongTimeString();
         sRet = dtNow.ToString(Glb.sFormat_Timestamp);                                 // e.g. "yyyy-MM-dd,HH:mm:ss"

         return sRet;
      }


      /// <summary>This method copies a stream to a file.</summary>
      /// <remarks>
      /// id : 20130702°0531 (20130116°1631 20130519°1431)
      /// ref : Method after 'Jon Skeet: Write file from assembly to disk' (20130116°1623)
      /// </remarks>
      /// <param name="input">The input stream</param>
      /// <param name="output">The output stream</param>
      public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
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


      /// <summary>This method combines a 'server' part and a 'database' part to a fullfilename (e.g. for SQLite).</summary>
      /// <remarks>id : 20130703°1521</remarks>
      /// <param name="sServer">The 'server' part of the wanted file path</param>
      /// <param name="sDatabase">The 'database' part of the wanted file path</param>
      /// <returns>The wanted fullfilename</returns>
      public static string CombineServerAndDatabaseToFullfilename(string sServer, string sDatabase)
      {
         string sFullFilename = "";

         // paranoia - just avoid exception below, the actual error must be handled by the caller
         if (sServer == null) { sServer = ""; }
         if (sDatabase == null) { sDatabase = ""; }

         // is server just a drive letter?
         // note : Remember issue 20130703°1511 'Path.Combine() fails to recognize drive letter'
         if (sServer.EndsWith(":"))
         {
            sServer += IOBus.Gb.Bricks.PathBkslsh;
         }

         // the actual job
         sFullFilename = System.IO.Path.Combine(sServer, sDatabase);

         // little cosmetics (what Combine() is missing to do)
         sFullFilename.Replace("\\\\","\\");

         return sFullFilename;
      }


      /// <summary>
      /// This method splits a full file path in two components to
      ///  be used for SQLite as 'Server' and 'Database' component.
      /// </summary>
      /// <remarks>
      /// id : 20130702°1421
      /// note : The splitting of a (long) path can be between any path elements. We did
      ///    it behind the drive letter, but that makes very long 'databasename' strings.
      ///    Now, just for fun, we do it one folder below the file. Best were, if in the
      ///    settings, the wanted algorithm is defined by the user. (20130717°1321)
      /// </remarks>
      /// <param name="sFullfilename">The full file path beginning with a drive</param>
      /// <param name="sServer">The wanted part one, by default the drive</param>
      /// <param name="sDatabase">The wanted part two, by default the full path but without the drive</param>
      /// <returns>Success flag proforma</returns>
      public static bool SplitFullfilenamInServerAndDatabase(string sFullfilename, out string sServerAddress, out string sDatabaseName)
      {
         sServerAddress = "";
         sDatabaseName = "";

         // paranoia (justifiably)
         if (String.IsNullOrEmpty(sFullfilename))
         {
            return false;
         }

         int iAlgorithm = 1;
         if (iAlgorithm < 1)
         {
            // old algorithm 'short serveraddress, long databasename'

            string sRoot = System.IO.Path.GetPathRoot(sFullfilename);
            sRoot = sRoot.TrimEnd('\\');
            sRoot = sRoot.TrimEnd('/');
            string sPathAboveRoot = System.IO.Path.GetDirectoryName(sFullfilename);
            sPathAboveRoot = sPathAboveRoot.Substring(sRoot.Length);
            string sFilename = System.IO.Path.GetFileName(sFullfilename);
            string sFilePathWithoutRoot = System.IO.Path.Combine(sPathAboveRoot, sFilename);
            sFilePathWithoutRoot = sFilePathWithoutRoot.TrimStart('\\');
            sServerAddress = sRoot;                                                    // e.g. "C:"
            sDatabaseName = sFilePathWithoutRoot;                                      // e.g. "Documents and Settings\Frank\Local Settings\Application Data\www.trilo.de\QueryPony.vshost.exe_Url_pnyidnkusl2rbapfnj3mkumbsyerzfns\2.1.2.34917\joesgarage.sqlite3"

         }
         else
         {
            // new algorithm (20130717°1331) 'variable serveraddress/databasename lengthes'

            sFullfilename.Replace("/", "\\");
            string[] ar = sFullfilename.Split('\\');
            if (ar.Length < 2)
            {
               sServerAddress = "Error1";
               sDatabaseName = "Error2";
            }
            else if (ar.Length == 2)
            {
               sServerAddress = ar[0];
               sDatabaseName = ar[1];
            }
            else
            {

               // this tells, how many subfolders shall stick before the database name
               int iDelve = 1;

               for (int i = 0; i < ar.Length; i++)
               {
                  if (i < (ar.Length - iDelve - 1))
                  {
                     if (sServerAddress != "")
                     {
                        sServerAddress += "\\";
                     }
                     sServerAddress += ar[i];
                  }
                  else
                  {
                     if (sDatabaseName != "")
                     {
                        sDatabaseName += "\\";
                     }
                     sDatabaseName += ar[i];
                  }
               }

               // No, do not wrap with quotes, later we get exception 'illegal characters
               //  in path', if we do not process the quotes in subsequent places.
               if (Glb.Debag.ExecuteNo)
               {
                  if (sServerAddress.Contains(" ")) { sServerAddress = "\"" + sServerAddress + "\""; }
                  if (sDatabaseName.Contains(" ")) { sDatabaseName = "\"" + sDatabaseName + "\""; }
               }

               // result e.g:
               // sServerAddress = "C:\Documents and Settings\Frank\Local Settings\Application Data\www.trilo.de\QueryPony.exe_Url_euqpzyfib35yvw55i13etxckcivppvbm\0.2.0.4711"
               // sDatabaseName = "C:\Documents and Settings\Frank\Local Settings\Application Data\www.trilo.de\QueryPony.exe_Url_euqpzyfib35yvw55i13etxckcivppvbm\0.2.0.4711"
            }

         }

         return true;
      }


      /// <summary>This method writes a string to a file, returning true if successful.</summary>
      /// <remarks>id : 20130604°0355</remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <param name="data">String data to write</param>
      /// <returns>Success flag</returns>
      public static bool WriteToFile (string fileName, string data)
      {
         bool success = true;
         try
         {
            System.IO.StreamWriter w = System.IO.File.CreateText (fileName);
            try
            {
               w.Write (data);
            }
            catch (Exception e)
            {
               error = "Cannot write to file: " + fileName + "\r\n" + e.Message;
               success = false;
            }
            finally
            {
               w.Close();
            }
         }
         catch (Exception e)
         {
            error = "Cannot create file: " + fileName + "\r\n" + e.Message;
            success = false;
         }
         return success;
      }


      /// <summary>This method writes a string to a file, returning true if successful.</summary>
      /// <remarks>
      /// id : 20130604°0356
      /// note : Called from nowhere so far.
      /// todo : Do not engage own file access methods here, but call the other WriteToFile()
      ///    as basic method, and use that for file access. Formulate this method here as a
      ///    wrapper for that, just breaking the StringCollection into strings. [todo 20130625°0921]
      /// </remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <param name="data">String collection to write</param>
      /// <returns>Success flag</returns>
      public static bool WriteToFile (string fileName, StringCollection data)
      {
         bool success = true;
         try
         {
            System.IO.StreamWriter w = System.IO.File.CreateText (fileName);
            try
            {
               foreach (string s in data)
               {
                  w.WriteLine(s);
               }
            }
            catch (Exception e)
            {
               error = "Cannot write to file: " + fileName + "\r\n" + e.Message;
               success = false;
            }
            finally {w.Close();}
         }
         catch (Exception e)
         {
            error = "Cannot create file: " + fileName + "\r\n" + e.Message;
            success = false;
         }
         return success;
      }


      /// <summary>This method reads the contents of a file into a string, returning true if successful.</summary>
      /// <remarks>id : 20130604°0357</remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <param name="data">Output data</param>
      /// <returns>Success flag</returns>
      public static bool ReadFromFile (string fileName, out string data)
      {
         bool success = true;
         data = "";
         try
         {
            System.IO.StreamReader r = System.IO.File.OpenText (fileName);
            try
            {
               data = r.ReadToEnd();
            }
            catch (Exception e)
            {
               error = "Cannot read from file: " + fileName + "\r\n" + e.Message;
               success = false;
            }
            finally {r.Close();}
         }
         catch (Exception e)
         {
            error = "Cannot open file: " + fileName + "\r\n" + e.Message;
            success = false;
         }
         return success;
      }


      /// <summary>This method reads the contents of a file into a string, returning true if successful.</summary>
      /// <remarks>
      /// id : 20130604°0358
      /// todo : Do not engage own file access methods here, but call the other
      ///    ReadFromFile() as basic method, and only that performs file access.
      ///    Formulate this method here as a wrapper for that, just mounting the
      ///    strings into the StringCollection. [todo 20130625°0922]
      /// </remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <param name="data">Output data</param>
      /// <returns>Success flag</returns>
      public static bool ReadFromFile (string fileName, out StringCollection data)
      {
         bool success = true;
         data = new StringCollection();
         try
         {
            System.IO.StreamReader r = System.IO.File.OpenText (fileName);
            try
            {
               string s;
               do
               {
                  s = r.ReadLine();
                  if (s != null) { data.Add(s); }
               }
               while (s != null);
            }
            catch (Exception e)
            {
               error = "Cannot read from file: " + fileName + "\r\n" + e.Message;
               success = false;
            }
            finally {r.Close();}
         }
         catch (Exception e)
         {
            error = "Cannot open file: " + fileName + "\r\n" + e.Message;
            success = false;
         }
         return success;
      }

   }
}
