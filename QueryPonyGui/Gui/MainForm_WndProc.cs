#region Fileinfo
// file        : 20130812°0811 /QueryPony/QueryPonyGui/Gui/MainForm_WndProc.cs
// summary     : This file stores partial class 'MainForm' to constitute the Main Form's Windows message pipe hook part.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyGui.Properties;
using QueryPonyLib;
using System;
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This partial class constitutes the Main Form's Windows message pipe hook part</summary>
   /// <remarks>
   /// id : 20130810°1931 (20130604°0532)
   /// note : This partial class was introduced while debugging issue 20130810°1901 'edit menu items fail'.
   /// ref : 20130810°1911 'Thread: Hook Paste event'
   /// ref : 20130810°1912 'Thread: Disable Clipboard features'
   /// ref : 20130810°1913 'MSDN: The WM_COPY message'
   /// </remarks>
   public partial class MainForm
   {

      /// <summary>This field stores the ClipboardEvent eventhandler</summary>
      /// <remarks>id : 20130810°1925</remarks>
      private event EventHandler<ClipboardEventArgs> _eventhandlerPastedOrWhat;

      /// <summary>This field defines System.Windows.Forms.Message bulk messages to be skipped</summary>
      /// <remarks>
      /// id : 20130810°1934
      /// note : This values and names were found empirically in a debug session until
      ///    the program runs all ususal actions without stepping into the breakpoint.
      /// note : The debugger shows the message name (if one exists). Where from does it know
      ///    the names? I found no enum or the like with intellisense or 'Goto Definition'.
      /// note : Only the context menu appearance on the Connect Form did never run into the
      ///    trap. This context menu I wanted catch, to see how it performs it's job, to find
      ///    a hint, what might be missing with issue 20130810°1901 'edit menu items fail'.
      /// note : Legend
      ///             ------     ----  --------  -------------     ---------------------------------------------------------
      ///             hex     decimal  appear    name              parameter example - comment
      ///             ------     ----  --------  -------------     ---------------------------------------------------------
      /// </remarks>
      private int[] iMsgs = {
                      0x0001 //     1  bulk   4  WM_NCCALCSIZE     {}
                    , 0x0003 //     3  bulk  20  WM_MOVE           {hwnd=0x18093a wparam=0x0 lparam=0xaf0072 result=0x0}
                    , 0x0005 //     5  bulk  19  WM_SIZE           {hwnd=0x18093a wparam=0x0 lparam=0x21502f5 result=0x0}
                    , 0x0006 //     6  bulk  18  WM_ACTIVATE       {hwnd=0x18093a wparam=0x1 lparam=0x0 result=0x0}
                    , 0x0007 //     7  bulk  45  WM_SETFOCUS       {hwnd=0x210946 wparam=0x2e0a44 lparam=0x0 result=0x0}
                    , 0x0007 //     8  bulk  46  WM_KILLFOCUS      {hwnd=0x210946 wparam=0x2208ae lparam=0x0 result=0x0}
                  //, 0x000a //    10  bulk 113  WM_ENABLE         {hwnd=0x11090a wparam=0x0 lparam=0x0 result=0x0} - selecting menu item 'About'; closing the 'About' dialogbox
                    , 0x000c //    12  bulk   5  WM_SETTEXT        {}
                    , 0x000d //    13  bulk  11  WM_GETTEXT        {hwnd=0x15090a wparam=0x18 lparam=0x1b06f8 result=0x0}
                    , 0x000e //    14  bulk  10  WM_GETTEXTLENGTH  {hwnd=0x15090a wparam=0x0 lparam=0x0 result=0x0}
                    , 0x000f //    15  bulk  25  WM_PAINT          {hwnd=0x150908 wparam=0x0 lparam=0x0 result=0x0}
                    , 0x0014 //    20  bulk  22  WM_ERASEBKGND     {hwnd=0x18093a wparam=0xffffffff9801186d lparam=0x0 result=0x0}
                    , 0x0018 //    24  bulk  14  WM_SHOWWINDOW     {hwnd=0x15090a wparam=0x1 lparam=0x0 result=0x0}
                    , 0x001a //    26  bulk  41  ?                 {}
                    , 0x001c //    28  bulk  16  WM_ACTIVATEAPP    {hwnd=0x18093a wparam=0x1 lparam=0x0 result=0x0}
                    , 0x001f //    31  bulk  39  WM_CANCELMODE     {hwnd=0x210946 wparam=0x0 lparam=0x0 result=0x0}
                    , 0x0020 //    32  bulk  29  WM_SETCURSOR      {hwnd=0x1c093a wparam=0x150a60 lparam=0x2000001 result=0x0}
                    , 0x0021 //    33  bulk 111  WM_MOUSEACTIVATE  {hwnd=0xc08e8 wparam=0xc08e8 lparam=0x2010002 result=0x0}
                    , 0x0024 //    36  bulk   1  WM_GETMINMAXINFO  {}
                    , 0x0046 //    70  bulk   8  WM_WINDOWPOSCHANGING {hwnd=0x15090a wparam=0x0 lparam=0x12f0e4 result=0x0}
                    , 0x0047 //    71  bulk   9  WM_WINDOWPOSCHANGED {hwnd=0x15090a wparam=0x0 lparam=0x12f0e4 result=0x0}
                  //, 0x007b //   123  bulk 112  WM_CONTEXTMENU    {hwnd=0x12093c wparam=0x1a081e lparam=0x15500c2 result=0x0} - the rightclick
                    , 0x007c //   124  bulk  12  WM_STYLECHANGING  {hwnd=0x15090a wparam=0xfffffffffffffff0 lparam=0x12f0c0 result=0x0}
                    , 0x007d //   125  bulk  13  WM_STYLECHANGED   {hwnd=0x15090a wparam=0xfffffffffffffff0 lparam=0x12f0c0 result=0x0}
                    , 0x007f //   127  bulk   7  WM_GETICON        {hwnd=0x15090a wparam=0x2 lparam=0x0 result=0x0}
                    , 0x0080 //   128  bulk   6  WM_SETICON        {}
                    , 0x0081 //   129  bulk   2  WM_GETMINMAXINFO  {}
                    , 0x0083 //   131  bulk   3  WM_NCCALCSIZE     {}
                    , 0x0084 //   132  bulk  28  WM_NCHITTEST      {hwnd=0x150908 wparam=0x0 lparam=0x13b01e8 result=0x0}
                    , 0x0085 //   133  bulk  21  WM_NCPAINT        {hwnd=0x18093a wparam=0x1 lparam=0x0 result=0x0}
                    , 0x0086 //   134  bulk  17  WM_NCACTIVATE     {hwnd=0x18093a wparam=0x0 lparam=0x0 result=0x0}
                    , 0x0088 //   136  bulk  23  .                 {hwnd=0x18093a wparam=0x4 lparam=0x0 result=0x0}
                    , 0x00a0 //   160  bulk  31  WM_NCMOUSEMOVE    {hwnd=0x1c093a wparam=0xc lparam=0x910101 result=0x0}
                    , 0x00a1 //   161  bulk  33  WM_NCLBUTTONDOWN  {hwnd=0x1b090a wparam=0x2 lparam=0x13602c1 result=0x0} - e.g. before moving window
                    , 0x00ae //   174  bulk  30  .                 {hwnd=0x1c093a wparam=0x1000 lparam=0x0 result=0x0}
                    , 0x0112 //   274  bulk  34  WM_SYSCOMMAND     {hwnd=0x1b090a wparam=0xf012 lparam=0x13602c1 result=0x0}
                    , 0x0127 //   295  bulk  26  .                 {hwnd=0x150908 wparam=0x30001 lparam=0x0 result=0x0}
                    , 0x0128 //   296  bulk  27  .                 {hwnd=0x150908 wparam=0x30001 lparam=0x0 result=0x0}
                    , 0x0202 //   514  bulk  37  WM_LBUTTONUP      {hwnd=0x1b090a wparam=0x0 lparam=0xffffffffffe501aa result=0x0}
                    , 0x0210 //   528  bulk  15  WM_PARENTNOTIFY   {hwnd=0x18093a wparam=0x8e80001 lparam=0x5308e8 (WM_CREATE) result=0x0}
                    , 0x0215 //   533  bulk  35  WM_CAPTURECHANGED {hwnd=0x1b090a wparam=0x0 lparam=0x0 result=0x0}
                    , 0x0216 //   534  bulk 110  WM_MOVING         {hwnd=0xc090a wparam=0x9 lparam=0x12e298 result=0x0}
                    , 0x0219 //   537  bulk  42  WM_DEVICECHANGE   {hwnd=0x210946 wparam=0x7 lparam=0x0 result=0x0}
                    , 0x0231 //   561  bulk  36  WM_ENTERSIZEMOVE  {hwnd=0x1b090a wparam=0x0 lparam=0x0 result=0x0}
                    , 0x0232 //   562  bulk  38  WM_EXITSIZEMOVE   {hwnd=0x5908e8 wparam=0x0 lparam=0x0 result=0x0}
                  //, 0x0281 //   641  bulk  43  WM_IME_SETCONTEXT {hwnd=0x210946 wparam=0x1 lparam=0xffffffffc000000f result=0x0}
                    , 0x0282 //   642  bulk  44  WM_IME_NOTIFY     {hwnd=0x210946 wparam=0x2 lparam=0x0 result=0x0}
                    , 0x02a2 //   674  bulk  32  .                 {hwnd=0x1c093a wparam=0x0 lparam=0x0 result=0x0}
                  //, 0x03e0 //   992  bulk  40  .                 {hwnd=0x210946 wparam=0x28099c lparam=0xffffffffc063c062 result=0x0} - selecting menu 'Open Doc in Browser'
                    , 0xc167 // 49511  bulk  24  .                 {hwnd=0x18093a wparam=0x0 lparam=0x0 result=0x0}
                    };

      /// <summary>This const tells the Windows message number WM_CONTEXTMENU for opening the context menu (empirically found)</summary>
      /// <remarks>id : 20130810°1951</remarks>
      private const int WM_CONTEXTMENU = 0x007b; // 123

      /// <summary>This const tells the Windows message number WM_ENABLE for igniting a menu item selection (empirically found)</summary>
      /// <remarks>id : 20130810°1952</remarks>
      private const int WM_ENABLE = 0x000a; // 113

      /// <summary>This const tells the Windows message number WM_IME_SETCONTEXT for ... (?) (empirically found)</summary>
      /// <remarks>id : 20130810°1953</remarks>
      private const int WM_IME_SETCONTEXT = 0x0281; // 641

      /// <summary>This const tells the Windows message number to execute a specific menu item (empirically found)</summary>
      /// <remarks>
      /// id : 20130810°1954
      /// note : Why has this value no name? What is the official description?
      /// </remarks>
      private const int WM__MenuItem_OpenDocInBrowser = 0x03e0; // 992

      /// <summary>This const tells the Windows message number for ~'Clipboard Clear' (from MSDN)</summary>
      /// <remarks>id : 20130810°1941</remarks>
      private const int WM_CLEAR = 0x0303;

      /// <summary>This const tells the Windows message number for 'Clipboard Copy' (from MSDN)</summary>
      /// <remarks>id : 20130810°1942</remarks>
      private const int WM_COPY = 0x0301;

      /// <summary>This const tells the Windows message number for 'Clipboard Cut' (from MSDN)</summary>
      /// <remarks>id : 20130810°1943</remarks>
      private const int WM_CUT = 0x0300;

      /// <summary>This const tells the Windows message number for 'Clipboard Paste' (from MSDN)</summary>
      /// <remarks>id : 20130810°1944</remarks>
      private const int WM_PASTE = 0x0302;

      /// <summary>This const tells the Windows message number for ~deprecated ~'Clipboard Undo' (from MSDN)</summary>
      /// <remarks>id : 20130810°1945</remarks>
      private const int WM_UNDO = 0; // ?

      /// <summary>This const tells the Windows message number for ~'Clipboard Undo' (from MSDN)</summary>
      /// <remarks>id : 20130810°1946</remarks>
      private const int EM_UNDO = 0; // ?

      /// <summary>This const tells the Windows message number for ~'Clipboard CanUndo' (from MSDN)</summary>
      /// <remarks>id : 20130810°1947</remarks>
      private const int EM_CANUNDO = 0; // ?

      /// <summary>This method hooks into the Windows message pipe (for debug purposes)</summary>
      /// <remarks>
      /// id : 20130810°1932
      /// note : Compare ref 20130810°1911
      /// note : The method attribute is after a MSDN example. But it does
      ///          not help to catch the wanted messages.
      /// </remarks>
      [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
      protected override void WndProc(ref Message msg)
      {
         // Ignore bulk messages [seq 20130810°1935]
         if (Array.IndexOf(iMsgs, msg.Msg) > -1)
         {
            base.WndProc(ref msg);
            return;
         }

         // Just demonstrate the syntax for a message creation [seq 20130810°1936]
         if (Glb.Debag.Execute_No)
         {
            System.IntPtr ptr = (System.IntPtr)1;
            Message message = System.Windows.Forms.Message.Create(ptr, 1, ptr, ptr);
         }

         // Process some empirical examples
         string sMsg = "";
         switch (msg.Msg)
         {
            case WM_ENABLE: sMsg = "WM_ENABLE"; break;
            case WM_CONTEXTMENU: sMsg = "WM_CONTEXTMENU"; break;
            case WM_IME_SETCONTEXT: sMsg = "WM_IME_SETCONTEXT"; break;
            case WM__MenuItem_OpenDocInBrowser: sMsg = "WM__SelectOpenDocInBrowser"; break;
            default: break;
         }
         if (sMsg != "")
         {
            if (Properties.Settings.Default.DeveloperMode)
            {
               string s = "[Debug] Windows message '" + sMsg + "'.";
               MainForm.outputStatusLine(s);
            }
         }

         // Seek for the wanted messages [seq 20130810°1933]
         // Note : Only they never catch
         if (msg.Msg == WM_COPY || msg.Msg == WM_CUT || msg.Msg == WM_PASTE)
         {
            // Ignore input if it was from a keyboard shortcut or a menu command
            // note : To cancle the action, just leave out 'base.WndProc(ref msg);'

            // Want ignore but have some feedback (but that is never seen)
            const string s = "[Debug] Clipboard Copy/Cut/Paste intercepted (20130810°1932).";
            MainForm.outputStatusLine(s);

         }

         // Handle the windows message normally
         base.WndProc(ref msg);
      }

      /// <summary>This eventhandler processes the ClipboardEvent event</summary>
      /// <remarks>
      /// id : 20130828°1431
      /// note : Implemented to avoid compiler warning "The event '_eventhandlerPastedOrWhat' is never used".
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void ClipboardEvent_PastedOrWhat(object sender, ClipboardEventArgs e)
      {
         return;
      }
   }

   /// <summary>This class constitutes ClipboardEvent arguments</summary>
   /// <remarks>id : 20130810°1922</remarks>
   public class ClipboardEventArgs : EventArgs
   {
      /// <summary>This property gets/sets the text in the clipboard</summary>
      /// <remarks>id : 20130810°1923</remarks>
      public string ClipboardText { get; set; }

      /// <summary>This constructor creates a new ClipboardEventArgs object</summary>
      /// <param name="clipboardText"></param>
      /// <remarks>id : 20130810°1924</remarks>
      public ClipboardEventArgs(string clipboardText)
      {
         ClipboardText = clipboardText;
      }
   }
}
