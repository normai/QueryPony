#region Fileinfo
// file        : 20130819°0811 (20130114°1531 20111105°1731) /QueryPony/QueryPonyGui/IOBus/IOBusProvider.cs
// summary     : This file stores partial class 'MainForm' with methods to provide basic I/O for external modules.
// license     : GNU AGPL v3
// copyright   : © 2011 - 2021 Norbert C. Maier
// authors     :
// encoding    : UTF-8-with-BOM
// status      :
// callers     :
// note        : To understand the I/O delegate mechanism, remember the engine calling in Window1.xaml.cs by
//               - WebreaderFire.DoBackgroundWork(webriOutputDelegate, webriInputDelegate, webriWorkfinishedDelegate);
//                 Before this is done, the delegate functions here are instantiated with
//               - DelegateDefinitionOutput webriOutputDelegate = new DelegateDefinitionOutput(webreaderguiOutputDelegateImplementation);
//               - DelegateDefinitionKeypress webriInputDelegate = new DelegateDefinitionKeypress(webreaderguiInputDelegateImplementation);
//               - DelegateDefinitionWorkfinished webriWorkfinishedDelegate = new DelegateDefinitionWorkfinished(webreaderguiWorkfinishedDelegateImplementation);
// note        :
#endregion

namespace QueryPonyGui // adjust this to the namespace of the local form (20130819°0802)
{

   /// <summary>This partial class provides basic I/O for external modules via delegates</summary>
   /// <remarks>
   /// id : 20130819°0812 (20130114°1532 20111105°1732)
   /// note : Nice were a class name like 'static IO', but this seems much more
   ///    complicated, since we need to access the GUI controls of the window.
   /// </remarks>
   public partial class MainForm : System.Windows.Forms.Form // adjust this to a partial class of the local form (20130819°0801)
   {
      /// <summary>This field stores an internal helper variable</summary>
      /// <remarks>id : 20130819°0813 (20130114°1533)</remarks>
      private static string _sKeyboardReadHelper = ""; // volatile?

      /// <summary>This method provides the local character output facility</summary>
      /// <remarks>id : 20130821°0935</remarks>
      /// <param name="sMsg">The text to be output</param>
      public void writeChar(string sMsg)
      {
         webreaderguiWriteCharDelegateImplementation(sMsg);
         return;
      }

      /// <summary>This method provides the local output facility</summary>
      /// <remarks>id : 20130819°0814 (20130114°1534 20110920°1722)</remarks>
      /// <param name="sMsg">The text to be output</param>
      public void writeLine(string sMsg)
      {
         webreaderguiWriteLineDelegateImplementation(sMsg);
         return;
      }

      /// <summary>This method provides the I/O-delegate implementation for character output (without newline)</summary>
      /// <remarks>id : 20130821°0934</remarks>
      /// <param name="sMsg">The text to be output</param>
      public void webreaderguiWriteCharDelegateImplementation(string sMsg)
      {
         // handle different requirements (compare note 20130821°0914)
         try
         {
            object[] aro = { sMsg };
            this.BeginInvoke(_outputToStatusChar, aro);
         }
         catch
         {
            outputStatusChar(sMsg);
         }

         return;
      }

      /// <summary>This method provides the I/O-delegate implementation for line output</summary>
      /// <remarks>
      /// id : 20130819°0815 (20130114°1535 20111002°1221)
      /// todo : Explain why exactly we need or need not the InvokeEx method here. (todo 20130114°153502)
      /// note : Here a try for an explanation. Since this method is given as a delegate
      ///    to any classes. Those classes cannot directly access this form's controls,
      ///    but only via Invoke(). [note 20130819°0811 about todo 20130114°153502]
      /// </remarks>
      /// <param name="sMsg">The text to be output</param>
      public void webreaderguiWriteLineDelegateImplementation(string sMsg)
      {
         /*
         this.InvokeEx(f => f.textboxStatus.AppendText(Gb.sCr + sMsg));
         this.InvokeEx(f => f.textboxStatus.ScrollToEnd()); // ScrollToEnd()does not exist for a WinForms TextBox
         */

         // Original line, not accepted due to issue 20130819°0822
         /*
         this.InvokeEx(f => f.textboxStatus.AppendText(Gb.sCr + sMsg));
         */

         // Handle requirements for different situations [seq 20121215°2112]
         // Note : This sequence replaces the original line
         //    'this.InvokeEx(f => f.textboxStatus.AppendText("\r\n" + sMsg));
         //    This replacement may have to do with the fact, that we want use this
         //    class from WinForms projects and from WPF projects in the same way.
         // Note : Compare issue 20130716°0626.
         // Note : Though practical, it is not nice to solve the different calling situations
         //    by a try envelope. Better were to detect the differrent situations and react
         //    with the appropriate flavour without guessing. [note 20130821°0814]
         // Note : Remember issue 20130819°0821 'ScrollToEnd() in IOBusProvider for WinForms'
         // Note : Consider issue 20130819°0822 'InvokeEx() in IOBusProvider for WinForms'
         try
         {
            // Probably during program start, this throws InvalidOperationException
            //  "Invoke or BeginInvoke cannot be called on a control until the window
            //  handle has been created." (exception 20130821°0813)
            object[] aro = { sMsg };
            this.BeginInvoke(_outputToStatusLine, aro);
         }
         catch
         {
            try
            {
               // If called from DbClone, this throws InvalidOperationException "Cross-thread
               //  operation not valid: Control 'textboxStatus' accessed from a thread other
               //  than the thread it was created on" (exceptions 20130821°0812).
               outputStatusLine(sMsg);
            }
            catch
            {
               // Never seen so far. In case this happens ... we have to dig still deeper.
               string s = "Program flow error [20130821°0815]."
                         + Globs.sCr + "This message should have gone to the console:"
                          + Globs.sCr + Globs.sCr
                           ;
               System.Windows.Forms.MessageBox.Show(s);
            }
         }

         return;
      }

      /// <summary>
      /// This method provides the I/O-delegate implementation to retrieve a keypress from
      ///  the WPF window. It depends on two helper event handlers setting sKeyboardReadHelper.
      /// </summary>
      /// <remarks>
      /// id : 20130819°0816 (20130114°1536 20111002°1222)
      /// note : This is a workaround for the here missing Console.ReadKey() method.
      ///    It uses the help of sKeyboardReadHelper, which is set by two event handlers
      ///    WebreaderGui_KeyDown() and WebreaderGui_KeyUp() to represent the keyboard state.
      /// </remarks>
      /// <param name="bWait">Flag whether to echo or not</param>
      /// <returns>The wanted key string</returns>
      public string readKey(bool bEcho)
      {
         string s = _sKeyboardReadHelper;
         string sKey = _sKeyboardReadHelper;
         while (sKey == s)
         {
            System.Threading.Thread.Sleep(99);
            sKey = _sKeyboardReadHelper;
         }

         // some key tokens want be convert
         switch (sKey)
         {
            case "D0" : sKey = "0"; break;
            case "D1" : sKey = "1"; break;
            case "D2" : sKey = "2"; break;
            case "D3" : sKey = "3"; break;
            case "D4" : sKey = "4"; break;
            case "D5" : sKey = "5"; break;
            case "D6" : sKey = "6"; break;
            case "D7" : sKey = "7"; break;
            case "D8" : sKey = "8"; break;
            case "D9" : sKey = "9"; break;
            default : break;
         }

         return sKey;
      }

      /// <summary>This method is helper method 1 for readKey(), setting sKeyboardReadHelper</summary>
      /// <remarks>
      /// id : 20130819°0817 (20130114°1537 20111001°2011)
      /// note : Using System.Windows.Input needs a reference to assembly PresentationCore.
      /// note : There exists also a System.Windows.Forms.KeyEventArgs but that has no Key property.
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void WebreaderGui_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
      {
         _sKeyboardReadHelper = e.Key.ToString();
         return;
      }

      /// <summary>This method is helper method 2 for readKey() unsetting sKeyboardReadHelper</summary>
      /// <remarks>
      /// id : 20130819°0818 (20130114°1538 20111001°2012)
      /// note :
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void WebreaderGui_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
      {
         _sKeyboardReadHelper = "";
         return;
      }

      /// <summary>This method provides the I/O-delegate implementation to read one line</summary>
      /// <remarks>
      /// id : 20130819°0819 (20130114°1539 20111109°1024)
      /// status : works
      /// ref : See 20111109°1025 'CodeProject, InputBox in C#'
      /// </remarks>
      /// <returns>The wanted string</returns>
      public string readLine()
      {
         string sLine = "";

         string sPrompt = "Prompt" + IOBus.Gb.Bricks.Cr + "prompt ...";
         string sTitle = "Input box title";
         string sDefaultResponse = "*";
         int iXPos = 100;
         int iYPos = 100;

         // the project must reference to Microsoft.VisualBasic.dll
         sLine = Microsoft.VisualBasic.Interaction.InputBox
               ( sPrompt
                , sTitle
                 , sDefaultResponse
                  , iXPos
                   , iYPos
                    );

         return sLine;
      }
   }
}
