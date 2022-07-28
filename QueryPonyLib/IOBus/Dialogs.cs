#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/IOBus/Dialogs.cs
// id          : 20130216°1121
// summary     : This file stores class 'Dialogs' to provide some standard dialogboxes.
// license     : GNU AGPL v3
// copyright   : © 2011 - 2018 by Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion

using System;

namespace IOBus
{
   /// <summary>This class provides some standard dialogboxes.</summary>
   /// <remarks>id : 20130216°1122</remarks>
   public static class Dialogs
   {

      /// <summary>This method provides a dialogbox for immediate messages.</summary>
      /// <remarks>
      /// id : 20130215°0131
      /// note : This should never be used from a pure library. Use it
      ///         there only as a provisory quick-shot during development.
      /// </remarks>
      /// <param name="sMsg">The message to be displayed to the user</param>
      /// <param name="sTitle">The string for the title line of the dialogbox</param>
      public static void dialogbox(string sMsg, string sTitle)
      {
         System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
         System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Information;
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show
                                               ( sMsg
                                                , sTitle                               // e.g. "Warnung"
                                                 , button                              // e.g. .YesNo
                                                  , icon                               // e.g. .Warning
                                                   );
         return;
      }


      /// <summary>This method executes the dialogbox. </summary>
      /// <remarks>
      /// id : 20130203°1421
      /// note : This method is not part of the IOBus system, just it
      ///         fits nice here since it provides an output facility.
      /// </remarks>
      /// <param name="sMsg">The message to display to the user</param>
      /// <param name="sTitle">The title string for the dialogbox</param>
      /// <param name="button">The button set to be displayed</param>
      /// <param name="icon">The icon to decorate the dialogbox</param>
      public static System.Windows.Forms.DialogResult dialogbox ( string sMsg
                                                                 , string sTitle
                                                                  , System.Windows.Forms.MessageBoxButtons button
                                                                   , System.Windows.Forms.MessageBoxIcon icon
                                                                    )
      {

         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show
                                               ( sMsg
                                                , sTitle                               // e.g. "Warnung"
                                                 , button                              // e.g. .YesNo
                                                  , icon                               // e.g. .Warning
                                                   );

         // just a template sequence
         switch (dr)
         {
            case System.Windows.Forms.DialogResult.Abort  : break;
            case System.Windows.Forms.DialogResult.Cancel : break;
            case System.Windows.Forms.DialogResult.Ignore : break;
            case System.Windows.Forms.DialogResult.No     : break;
            case System.Windows.Forms.DialogResult.None   : break;
            case System.Windows.Forms.DialogResult.OK     : break;
            case System.Windows.Forms.DialogResult.Retry  : break;
            case System.Windows.Forms.DialogResult.Yes    : break;
            default: break;
         }

         return dr;
      }


      /// <summary>This method provides a dialogbox with the title 'Error'. </summary>
      /// <remarks>id : 20130218°1401</remarks>
      /// <param name="sMsg">The message to display to the user</param>
      public static void dialogErr(string sMsg)
      {
         string sTitle = "Error";
         dialogbox(sMsg, sTitle);
         return;
      }


      /// <summary>This method provides a dialogbox for immediate messages. </summary>
      /// <remarks>
      /// id : 20130203°1422
      /// note : This method should never be used from a library. Use it only as provisory quick-shot during development. 
      /// </remarks>
      /// <param name="sMsg">The message to display to the user</param>
      public static void dialogOk(string sMsg)
      {
         string sTitle = "Notification";
         dialogbox(sMsg, sTitle);
         return;
      }


      /// <summary>This method provides a standard yes/no-question dialogbox and returns true for 'OK' and false for 'Cancel'.</summary>
      /// <remarks>id : 20130216°1123</remarks>
      /// <param name="sMsg">The message to display to the user</param>
      /// <returns>Success flag</returns>
      public static bool dialogOkCancel(string sMsg)
      {
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show
                               ( sMsg
                                , "Question"
                                 , System.Windows.Forms.MessageBoxButtons.OKCancel
                                  , System.Windows.Forms.MessageBoxIcon.Question
                                   );
         if (dr != System.Windows.Forms.DialogResult.OK)
         {
            return false;
         }

         return true;
      }


      /// <summary>This method provides a standard yes/no-question dialogbox and returns true for 'Yes' and false for 'No'.</summary>
      /// <remarks>id : 20130216°1124</remarks>
      /// <param name="sMsg">The message to display to the user</param>
      /// <returns>Success flag</returns>
      public static bool dialogYesNo(string sMsg)
      {
         bool bAnswer = dialogYesNo("Question", sMsg);
         return bAnswer;
      }


      /// <summary>This method provides a standard yes/no-question dialogbox and returns true for 'Yes' and false for 'No'.</summary>
      /// <remarks>id : 20131121°0901 (20130216°1124)</remarks>
      /// <param name="sCaption">The caption string to show in the title bar of the dialog box</param>
      /// <param name="sMsg">The message to display to the user in the dialog box</param>
      /// <returns>Success flag</returns>
      public static bool dialogYesNo(string sCaption, string sMsg)
      {
         System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show
                               (sMsg
                                , sCaption // "Question"
                                 , System.Windows.Forms.MessageBoxButtons.YesNo
                                  , System.Windows.Forms.MessageBoxIcon.Question
                                   );
         if (dr != System.Windows.Forms.DialogResult.Yes)
         {
            return false;
         }

         return true;
      }
   }
}
