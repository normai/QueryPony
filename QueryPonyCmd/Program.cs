#region Fileinfo
// id          : 20190410°0611 QueryPony/QueryPonyLib/Program.cs
// summary     : Class 'Program' constitutes the program entry point
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// callers     : Only • Some external commandline
#endregion Fileinfo

using System;
using System.Collections.Generic;                                              // List
using QuPoLi = QueryPonyLib;

namespace QueryPonyCmd
{
   /// <summary>This static class serves holding the program entry point</summary>
   /// <remarks>id : class 20190410°0621</remarks>
   public static class Program
   {
      /// <summary>
      /// Delegate to be passed into QueryPonyLib initialization
      /// </summary>
      /// <remarks>id: delegate 20210522°1111</remarks>
      private static QuPoLi.IOBus_OutputChars WritChars = QueryPonyCmd.Program.OutputChars;

      /// <summary>
      /// Delegate to be passed into QueryPonyLib initialization
      /// </summary>
      /// <remarks>id: delegate 20210522°1121</remarks>
      private static QuPoLi.IOBus_OutputLine WritLin = QueryPonyCmd.Program.OutputLine;

      /// <summary>Main entry point for the application</summary>
      /// <remarks>id : method 20190410°0631</remarks>
      /// <param name="args">The arguments coming from the commanline that started this program</param>
      [STAThread]
      private static int Main(string[] args)
      {
         // Notification
         string sVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
         Console.WriteLine("*** Welcome to QueryPonyCmd.exe " + sVersion + "***");

         // Process commandline parameters, just printing a hint
         if (args.Length > 0)
         {
            Console.WriteLine(" - Para 1 = " + args[1]);
         }

         // Initialize QueryPony library [seq 20210522°1011]
         var quPoLib = new QuPoLi.InitLib ( null                               // Probably useless
                                           , WritChars                         // Delegate for character writing facility
                                            , WritLin                          // Delegate for line writing facility
                                             );
         Console.WriteLine("UserDir = \"" + IOBus.Utils.Strings.Ram(QuPoLi.InitLib.Settings2Dir, 88) + "\"");

         // Provide optional autostart commands
         Queue<string> cmds = new Queue<string>();
         //cmds.Enqueue("a");
         cmds.Enqueue("test1");
         cmds.Enqueue("test2");

         // Execute main loop [seq 20200522°0153]
         int iErrCode = Dispatcher.Loop(cmds);

         // Finish
         System.Threading.Thread.Sleep(999);                                   // Milliseconds to read the exit message
         return iErrCode;
      }

      /// <summary>
      ///  This method provides the character output device
      /// </summary>
      /// <remarks>
      /// id : method 20210522°1113
      /// </remarks>
      /// <param name="sMsg">The characters to be written</param>
      public static void OutputChars(string sMsg)
      {
         System.Console.Write(sMsg);
      }

      /// <summary>
      ///  This method provides the line output device
      /// </summary>
      /// <remarks>
      /// id : method 20210522°1123
      /// </remarks>
      /// <param name="sMsg">The line to be written</param>
      public static void OutputLine(string sLine)
      {
         System.Console.WriteLine(sLine);
      }
   }
}
