#region Fileinfo
// file        : 20200522°0211 /QueryPonyLib/Selftest.cs
// summary     : Provide some selftest facility
// license     : GNU AGPL v3
// copyright   : © 2020 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Under construction
// callers     : Only • Main()
#endregion Fileinfo

using System;
using SyIo = System.IO;
using QuPoLib = QueryPonyLib;                                                  // E.g. DbClient
using SyDaSqli = System.Data.SQLite;

namespace QueryPonyCmd
{
   /// <summary>
   /// Provide some selftest facilities
   /// </summary>
   /// <remarks>id 20200522°0221</remarks>
   public static class Selftest
   {
      /// <summary>This field stores the DbClient ... (for this ConnectForm or for what exactly?)</summary>
      /// <remarks>Ident 20200522°0331 (after 20130604°0053)</remarks>
      private static QuPoLib.DbClient _client = null;

      /// <summary>
      /// Execute initial self test — Establish connection to the on-board SQLite demo database
      /// </summary>
      /// <remarks>id 20200522°0231</remarks>
      public static int SelfTestOne()
      {

         // Set flags [seq 20210524°1321]
         QuPoLib.Glb.Debag.Tell_AssemblyLoad_Event = false;                    // Use this when in doubt with libraries, e.g. System.Data.SQLite

         // Welcome
         Console.WriteLine("SelfTestOne is performing:");

         // Tell path of executable [seq 20200522°0311]
         // Compare external file 20190925°1521 project 20190925°1511
         string sThisDir = SyIo.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
         string sThisExe = SyIo.Path.Combine(sThisDir, System.AppDomain.CurrentDomain.FriendlyName);
         Console.WriteLine(" - This is file \"" + sThisExe + "\"");

         // Quick'n'dirty [line 20210522°1441]
         string sSqliteFulfilnam = SyIo.Path.Combine(QuPoLib.InitLib.Settings2Dir, "joespostbox.201307031243.sqlite3");

         // Is some database available? [seq 20200522°0315]
         if (! SyIo.File.Exists(sSqliteFulfilnam))
         {
            Console.WriteLine(" - Wanted file not found.");
            return 123;
         }

         // Create the engine connection settings [seq 20200522°0341 (after 20130620°1613)]
         // See todo 20130828°1522 'Combine seqences into method'
         QuPoLib.ConnSettingsLib csLib = new QuPoLib.ConnSettingsLib();        // ConnSettingsGui.convertSettingsGuiToLib(this._conSettings);

         // What can be done with the SQLite library without connection? [seq 20210523°1531]
         // Issue 20210523°1533 SQLiteLog.LogMessage() executes, but where can the messages be read?!
         SyDaSqli.SQLiteLog.Initialize();
         SyDaSqli.SQLiteLog.LogMessage("Hello SQLite?");

         // Configure connection [seq 20200522°0411]
         csLib.Type = QuPoLib.ConnSettingsLib.ConnectionType.Sqlite;
         csLib.DatabaseServerUrl = QuPoLib.InitLib.Settings2Dir;
         csLib.DatabaseName = "joespostbox.20130703o1243.sqlite3";
         csLib.DatabaseConnectionstring = "";

         //
         SyDaSqli.SQLiteConnectionStringBuilder csb = new SyDaSqli.SQLiteConnectionStringBuilder();
         csb.DataSource = SyIo.Path.Combine(csLib.DatabaseServerUrl, csLib.DatabaseName);
         csb.Version = 3;
         csLib.DatabaseConnectionstring = csb.ConnectionString;

         Console.WriteLine(" - DataSource = " + IOBus.Utils.Strings.Ram(csb.DataSource, 84));

         // Just try simple way [seq 20200522°0421]
         string s23 = $"Data Source={sSqliteFulfilnam};Version=3;";
         SyDaSqli.SQLiteConnection conn = new SyDaSqli.SQLiteConnection(csLib.DatabaseConnectionstring);
         conn.Open();

         // Paranoia [seq 20210523°1011]
         // E.g. • conn.ConnectionString = "Data Source=C:\Users\Joe\AppData\Local\www.trekta.biz\QueryPonyCmd.exe_Url_it5gbzlvr3ib2bxw4yg3rgqejejzev0a\0.3.4.25984\joespostbox.201307031243.sqlite3; Version = 3;"
         //  • conn.FileName "threw an exception of type 'System.InvalidOperationException'"
         if (conn.DataSource == null) {
            string s34 = " - Connection attempt returned with DataSource is null. Test run aborted.";
            Program.OutputLine(s34);
            return 123;
         }

         // Retrieve the client object [seq 20200522°0351]
         _client = QuPoLib.DbClientFactory.GetDbClient(csLib);                 // Breakpoint 20140126°1611`11

         // Make the connection [seq 20200522°0321]
         // Compare eventhandler 20130618°0411 button_Connect_Click()
         // Remember dialog 'Error 20130703°1515 — File does not exist "G:\work\downtown\queryponydev\trunk\QueryPony\QueryPonyCmd\docs\joespostbox.201307031234.sqlite3"
         bool bSuccess = _client.Connect();                                    // Breakpoint 20140126°1621`11

         Console.WriteLine(" - Connected = " + bSuccess);

         // Finish connection
         _client.Dispose();

         return 0;
      }

      /// <summary>
      /// Perform more tests — Do something with the on-board SQLite demo database
      /// </summary>
      /// <remarks>
      /// Ident 20210524°1211
      /// </remarks>
      /// <returns></returns>
      public static int SelfTestTwo()
      {
         Console.WriteLine("SelfTestTwo is performing:");





         return 0;
      }

   }
}
