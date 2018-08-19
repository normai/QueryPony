#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/IOBusConsumer.cs
// id          : 20130819°0831 (20130115°0911 20111105°1751)
// summary     : This file stores class 'IOConsumer' to provide I/O facilities as the calleé, means as the delegates consumer.
// license     : GNU AGPL v3
// copyright   : © 2011 - 2018 by Norbert C. Maier
// authors     :
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion

using IOBus;
using System;
using System.Threading;

namespace QueryPonyLib // adjust this to your local namespace
{

   /// <summary>This class provides I/O facilities as the calleé, means as the delegates consumer.</summary>
   /// <remarks>id : 20130819°0832 (20130115°0912 20111105°1712)</remarks>
   public static class IOBusConsumer
   {

      /// <summary>
      /// This delegate proxy field executes the character output. It
      ///  must be set to a value by the first user of this class.
      ///  </summary>
      /// <remarks>
      /// id : 20130821°0936
      /// note : This delegate (and it's sibling delegates) must be static. No? See
      ///         possible problem 20111111°1431 with callers from different UIs.
      /// </remarks>
      internal static IOBus_OutputLine _writeHostChar;


      /// <summary>
      /// This delegate proxy field executes the line output. It
      ///  must be set to a value by the first user of this class.
      ///  </summary>
      /// <remarks>
      /// id : 20130819°0833 (20130115°0913 20111105°1713)
      /// note : This delegate (and it's sibling delegates) must be static. No? See
      ///         possible problem 20111111°1431 with callers from different UIs.
      /// </remarks>
      ////internal static IOBus_OutputLine writeHosti;
      internal static IOBus_OutputLine _writeHostLine;


      /// <summary>
      /// This delegate proxy retrieves one keypress from input. It
      ///  must be set to a value by the first user of this class.
      /// </summary>
      /// <remarks>id : 20130819°0834 (20130115°0914 20111105°1714)</remarks>
      internal static IOBus_InputKeypress dlgtKeypressi;


      /// <summary>
      /// This delegate proxy retrieves one line from input. It
      ///  must be set to a value by the first user of this class.
      /// </summary>
      /// <remarks>id : 20130819°0835 (20130115°0915 20111105°1715)</remarks>
      internal static IOBus_InputLine dlgtInputLinei;


      /// <summary>
      /// This field tells whether the write-output shall additinally
      ///  be written to a transcript file or not (provisory dummy).
      /// </summary>
      /// <remarks>id : 20130819°0836 (20130115°0916)</remarks>
      private static bool bWriteTranscriptFile = false;


      /// <summary>This field tells the filename of the transcript file (provisory dummy).</summary>
      /// <remarks>id : 20130819°0837 (20130115°0917)</remarks>
      ////private static string sTranscripfilename = "";
      private static string sTranscripfilename = null;


      /// <summary>This method is a helper method to output a character.</summary>
      /// <remarks>id : 20130821°0941</remarks>
      /// <param name="sMsg">String to be output</param>
      /// <returns>Nothing</returns>
      public static void writeHostChar(string sMsg)
      {
         Thread.Sleep(1);

         if (_writeHostChar == null)
         {
            // Fatal, below line will throw null reference exception.
            // todo : Replace this provisory escape by something more solid.
            //   Compare emergency output 20130902°1341 [todo 20130106°1101]
            return;
         }
         _writeHostChar(sMsg);

         // todo : Possible transcript file output still missing (todo 20130821°0942)
      }


      /// <summary>This method is a helper method to output a message.</summary>
      /// <remarks>id : 20130819°0838 (20130115°0918 20110921°1416)</remarks>
      /// <param name="sMsg">String to be output</param>
      /// <returns>Nothing</returns>
      public static void writeHost(string sMsg)
      {
         Thread.Sleep(1);

         // condition to avoid below line to throw exception "Object reference not set to an instance of an object."
         if (_writeHostLine == null)
         {
            // emergency output (20130902°1341)
            string s = "[Message for a not-available console:]" + IOBus.Gb.Bricks.CrCr + sMsg;
            IOBus.Dialogs.dialogOk(s);
         }
         else
         {
            _writeHostLine(sMsg);
         }

         if (bWriteTranscriptFile)
         {
            if (sTranscripfilename != null)
            {
               using (System.IO.StreamWriter file = new System.IO.StreamWriter(sTranscripfilename, true))
               {
                  file.WriteLine(sMsg);
               }
            }
            else
            {
               // emergency output (20130902°1342)
               string s = "[Entry for a not-available logfile:]" + IOBus.Gb.Bricks.CrCr + sMsg;
               IOBus.Dialogs.dialogOk(s);
            }
         }
      }


      /// <summary>This method is a helper method to output a message conditionally.</summary>
      /// <remarks>id : 20130819°0839 (20130115°0919 20110921°1417)</remarks>
      /// <param name="bOutput">Flag whether to perform the output or not.</param>
      /// <param name="sMsg">String to be output conditionally</param>
      /// <returns>Nothing</returns>
      public static void writeHost(bool bOutput, string sMsg)
      {
         if (! bOutput)
         {
            return;
         }

         writeHost(sMsg);
         return;
      }


      /// <summary>This method is a helper method to read a keystroke from a user input source.</summary>
      /// <remarks>id : 20130819°0840 (20130115°0920 20111001°2014)</remarks>
      /// <param name="bEcho">Flag to tell whether to echo the keystroke or not</param>
      /// <returns>The wanted keystroke</returns>
      internal static string readKie(bool bEcho)
      {
         string sKey = dlgtKeypressi(bEcho);
         return sKey;
      }


      /// <summary>This method is the delegate implementation for reading a line from the input device.</summary>
      /// <remarks>
      /// id : 20130819°0841 (20130115°0921 20111109°1026)
      /// note : Is perhaps a wait flag parameter wanted?
      /// </remarks>
      /// <returns>The wanted line</returns>
      internal static string readLine()
      {
         string sLine = dlgtInputLinei();
         return sLine;
      }

   }
}
