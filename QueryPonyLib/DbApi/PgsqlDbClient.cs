#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DbApi/PgsqlDbClient.cs
// id          : 20130616°1531 (20130605°1731)
// summary     : This file stores class 'PgsqlDbClient' to constitute an
//                implementation of DbClient for PostgreSQL.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from SqliteDbClient.cs and modified (20130616°1531)
// note        :
// callers     :
#endregion Fileinfo

////using Npgsql;
using System.Data;

namespace QueryPonyLib
{

   /// <summary>This class constitutes an implementation of DbClient for PostgreSQL.</summary>
   /// <remarks>
   /// id : 20130616°1532 (20130605°1732)
   /// ref : Article 'Npgsql: User's Manual' on [ws 20130616°1721]
   ///        http://npgsql.projects.pgfoundry.org/docs/manual/UserManual.html
   /// ref : Npgsql API Documentation Help File 'Npgsql.chm' from (20130611°1911)
   ///        http://pgfoundry.org/frs/?group_id=1000140/Npgsql2.0.12-apidocs-html.zip
   /// </remarks>
   class PgsqlDbClient : DbClient
   {

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130616°1533 (20130605°1733)</remarks>
      /// <param name="settings">...</param>
      public PgsqlDbClient(ConnSettingsLib settings) : base(settings)
      {
      }


      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130616°1534 (20130605°1734)</remarks>
      public Npgsql.NpgsqlConnection Connection
      {
         get { return (Npgsql.NpgsqlConnection)_connection; }
      }


      /// <summary>This method returns a PostgreSQL database connection.</summary>
      /// <remarks>id : 20130616°1535 (20130605°1735)</remarks>
      /// <returns>The wanted PostgreSQL database connection.</returns>
      protected override IDbConnection GetDbConnection()
      {

         // (sequence 20130606°132202)
         string sCon = GenerateConnectionString();
         Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection();
         con.ConnectionString = sCon;

         return con;
      }


      /// <summary>
      /// This eventhandler processes the InfoMessage event of this PostgreSQL
      ///  connection. But it is nowhere attached (see issue 20130607°1311).
      /// </summary>
      /// <remarks>
      /// id : 20130616°1536 (20130605°1736)
      /// todo : Not sure how this eventhandler will work. Check this and make it work. (20130720°1231)
      /// </remarks>
      /// <param name="e">The event object itself</param>
      /// <param name="sender">The object which sent this event</param>
      ////void con_InfoMessage(object sender, OleDbInfoMessageEventArgs e)
      ////void con_InfoMessage(object sender, Npgsql.NpgsqlNoticeEventArgs e)
      void con_InfoMessage(object sender, Npgsql.NpgsqlNotificationEventArgs e)
      {
         ////OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, ""));
         OnInfoMessage(sender, new InfoMessageEventArgs(e.AdditionalInformation, ""));
      }


      /// <summary>This method builds a PostgreSQL connectionstring from the connection settings of this DbClient.</summary>
      /// <remarks>id : 20130616°1537 (20130605°1737)</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
         ////csb.ConnectionString = "Data Source=" ;//+ conSettings.;

         // sequence (20130720°1201)
         csb.Host = this.ConnSettings.DatabaseServerUrl;
         csb.Database = this.ConnSettings.DatabaseName;
         csb.UserName = this.ConnSettings.LoginName;
         csb.Password = this.ConnSettings.Password;
         //csb.Timeout =

         return csb.ConnectionString; // e.g. "HOST=localhost;PORT=5432;PROTOCOL=3;DATABASE=joetest4;USER ID=postgres;PASSWORD=postgres;SSL=False;SSLMODE=Disable;TIMEOUT=15;SEARCHPATH=;POOLING=True;CONNECTIONLIFETIME=15;MINPOOLSIZE=1;MAXPOOLSIZE=20;SYNCNOTIFICATION=False;COMMANDTIMEOUT=20;ENLIST=False;PRELOADREADER=False;USEEXTENDEDTYPES=False;INTEGRATED SECURITY=False;COMPATIBLE=2.0.12.0;APPLICATIONNAME="
      }


      /// <summary>This method delivers a PostgresSQL command object for a given command string.</summary>
      /// <remarks>id : 20130616°1538 (20130605°1738)</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         ////SQLiteCommand cmd = ((SQLiteConnection)_connection).CreateCommand();
         Npgsql.NpgsqlCommand cmd = ((Npgsql.NpgsqlConnection) _connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1539 (20130605°1739)</remarks>
      /// <returns>...</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new SqliteQueryOptions();
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1540 (20130605°1740)</remarks>
      /// <param name="command">...</param>
      /// <returns>...</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         ////return new SQLiteDataAdapter((SQLiteCommand)command);
         return new Npgsql.NpgsqlDataAdapter((Npgsql.NpgsqlCommand)command);
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1541 (20130605°1741)</remarks>
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
