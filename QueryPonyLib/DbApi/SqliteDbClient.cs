#region Fileinfo
// file        : 20130605°1731 (20130604°1001) /QueryPony/QueryPonyLib/DbApi/SqliteDbClient.cs
// summary     : Class 'SqliteDbClient' constitutes an implementation of DbClient for SQLite
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from OledbDbClient.cs and modified (20130605°1731)
// callers     :
#endregion Fileinfo

using System;
using System.Data;
using SyDaSqli = System.Data.SQLite;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of DbClient for SQLite</summary>
   /// <remarks>
   /// id : 20130605°1732 (after 20130604°1002)
   /// ref : 20130605°1611 'Thomas Belser, kleine SQLite Einführung'
   /// </remarks>
   class SqliteDbClient : DbClient
   {
      /// <summary>This constructor creates a new SQLite DbClient after the given connection settings</summary>
      /// <remarks>
      /// id : 20130605°1733 (20130604°1003)
      /// note : This method is involved in refactor 20130620°0211
      /// </remarks>
      /// <param name="settings">The connection settings for which to create this client</param>
      public SqliteDbClient(ConnSettingsLib settings) : base(settings)
      {
      }

      /// <summary>This property gets this DbClient's SQLiteConnection</summary>
      /// <remarks>id : 20130605°1734 (20130604°1004)</remarks>
      public SyDaSqli.SQLiteConnection Connection
      {
         get {
            return (SyDaSqli.SQLiteConnection)_connection;
         }
      }

      /// <summary>This method returns a SQLite database connection</summary>
      /// <remarks>id : 20130605°1735 (20130604°1005)</remarks>
      /// <returns>The wanted SQLite database connection</returns>
      protected override IDbConnection GetDbConnection()
      {
         // [seq 20130606°1322]
         string sCon = GenerateConnectionString();

         // Wrap in try envelop for experimenting with single-file-deployment [issue 20130706°1031]
         SyDaSqli.SQLiteConnection con = null;
         try
         {
            con = new SyDaSqli.SQLiteConnection();
         }
         catch (Exception ex)
         {
            string sMsg = ex.Message;
         }

         con.ConnectionString = sCon;                                          // "Data Source=" + dataSource;

         // Here solve issue 20130607°1311 'The connection InfoMessage event availability'
         // //con.InfoMessage += new OleDbInfoMessageEventHandler(con_InfoMessage);

         return con;
      }

      /// <summary>
      /// This eventhandler processes the InfoMessage event of this SQLite connection. This
      ///  is a syntax dummy, nowhere attached for now. Check, what can be the SQLite equivalent
      ///  for System.Data.OleDb.OleDbInfoMessageEventArgs (see issue 20130607°1311).
      /// </summary>
      /// <remarks>id : 20130605°1736 (20130604°1006)</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      void con_InfoMessage(object sender, System.Data.OleDb.OleDbInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, ""));
      }

      /// <summary>This method builds a SQLite connectionstring from the connection settings of this DbClient</summary>
      /// <remarks>id : 20130605°1737 (20130604°1007)</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         SyDaSqli.SQLiteConnectionStringBuilder csb = new SyDaSqli.SQLiteConnectionStringBuilder();

         // See issue 20130703°1511 [seq 20130703°1514]
         string sFileFullname = Utils.CombineServerAndDatabaseToFullfilename(_connSettings.DatabaseServerUrl, _connSettings.DatabaseName);
         if (! System.IO.File.Exists(sFileFullname))
         {
            string sMsg = "File does not exist: " + sFileFullname + Glb.sCr + "[Error 20130703°1515]";
            System.Windows.Forms.MessageBox.Show(sMsg);
         }
         csb.ConnectionString = "Data Source=" + sFileFullname;

         return csb.ConnectionString;
      }

      /// <summary>This method delivers a SQLite command object for a given command string</summary>
      /// <remarks>id : 20130605°1738 (20130604°1008)</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         SyDaSqli.SQLiteCommand cmd = ((SyDaSqli.SQLiteConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }

      /// <summary>This method retrieves the default SQLite query options</summary>
      /// <remarks>id : 20130605°1739 (20130604°1009)</remarks>
      /// <returns>The wanted default SQLite query options.</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new SqliteQueryOptions();
      }

      /// <summary>This method retrieves the SQLite IDbDataAdapter</summary>
      /// <remarks>id : 20130605°1740 (20130604°1017)</remarks>
      /// <param name="command">The command for which to retrieve the data adapter</param>
      /// <returns>The wanted SQLite IDbDataAdapter</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new SyDaSqli.SQLiteDataAdapter((SyDaSqli.SQLiteCommand)command);
      }

      /// <summary>This method applies query options (not yet implemented)</summary>
      /// <remarks>id : 20130605°1741 (20130604°1010)</remarks>
      public override void ApplyQueryOptions()
      {
         return;

         /*
         StringBuilder sb = new StringBuilder();
         MssqlQueryOptions sqo = ((SqlQueryOptons) queryOptions);
         sb.Append(string.Format(" SET ROWCOUNT {0}", sqo.RowCount));
         sb.Append(string.Format(" SET TEXTSIZE {0}", sqo.TextSize));
         sb.Append(string.Format(" SET NOCOUNT {0}", sqo.NoCount ? "ON" : "OFF"));
         sb.Append(string.Format(" SET CONCAT_NULL_YIELDS_NULL {0}", sqo.Concat_Null_Yields_Null ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ARITHABORT {0}", sqo.ArithAbort ? "ON" : "OFF"));
         sb.Append(string.Format(" SET LOCK_TIMEOUT {0}", sqo.Lock_Timeout));
         sb.Append(string.Format(" SET QUERY_GOVERNOR_COST_LIMIT {0}", sqo.Query_Governor_Cost_Limit));
         sb.Append(string.Format(" SET DEADLOCK_PRIORITY {0}", sqo.Deadlock_Priority));
         sb.Append(string.Format(" SET TRANSACTION ISOLATION LEVEL {0}", sqo.Transaction_Isolation_Level));
         sb.Append(string.Format(" SET ANSI_NULLS {0}", sqo.Ansi_Nulls ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_NULL_DFLT_ON {0}", sqo.Ansi_Null_Dflt_On ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_PADDING {0}", sqo.Ansi_Padding ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_WARNINGS {0}", sqo.Ansi_Warnings ? "ON" : "OFF"));
         sb.Append(string.Format(" SET CURSOR_CLOSE_ON_COMMIT {0}", sqo.Cursor_Close_On_Commit ? "ON" : "OFF"));
         sb.Append(string.Format(" SET IMPLICIT_TRANSACTIONS {0}", sqo.Implicit_Transactions ? "ON" : "OFF"));
         sb.Append(string.Format(" SET QUOTED_IDENTIFIER {0}", sqo.Quoted_Identifier ? "ON" : "OFF"));

         ExecuteOnWorker(sb.ToString(), 5);
         */
      }
   }
}
