#region Fileinfo
// file        : 20130604°0721 /QueryPony/QueryPonyLib/DbApi/MssqlDbClient.cs
// summary     : This file stores class 'MssqlDbClient' to constitute an implementation of DbClient for MS-SQL.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// changes     : File/class renamed from SqlDBClient.cs to MssqlDbClient.cs (20130606°1342)
// note        :
// callers     :
#endregion Fileinfo

using System.Data;
using SyDaSq = System.Data.SqlClient;
using System.Text;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of DbClient for MS-SQL</summary>
   /// <remarks>id : 20130604°0722</remarks>
   class MssqlDbClient : DbClient
   {
      /// <summary>This constructor creates a new MS-SQL DbClient object</summary>
      /// <remarks>id : 20130604°0723</remarks>
      /// <param name="settings">The ConnectionSettings to create the new DbClient with</param>
      public MssqlDbClient(ConnSettingsLib settings) : base(settings)
      {
      }

      /// <summary>This method returns a MS-SQL database connection</summary>
      /// <remarks>id : 20130604°0724</remarks>
      /// <returns>The wanted MS-SQL database connection</returns>
      protected override IDbConnection GetDbConnection()
      {
         string s = GenerateConnectionString();
         SyDaSq.SqlConnection con = new SyDaSq.SqlConnection(s);
         con.InfoMessage += new SyDaSq.SqlInfoMessageEventHandler(con_InfoMessage);
         return con;
      }

      /// <summary>This eventhandler processes the InfoMessage event of this MS-SQL connection</summary>
      /// <remarks>id : 20130604°0725</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void con_InfoMessage(object sender, SyDaSq.SqlInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new  InfoMessageEventArgs(e.Message, e.Source));
      }

      /// <summary>This method builds a MS-SQL connectionstring from the connection settings of this DbClient</summary>
      /// <remarks>
      /// id : 20130604°0726
      /// remember : question 20130714°1743 'how to select a database from the server'
      /// </remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         SyDaSq.SqlConnectionStringBuilder csb = new SyDaSq.SqlConnectionStringBuilder();
         csb.ApplicationName = Glb.Resources.AssemblyNameLib;
         csb.DataSource = _connSettings.DatabaseServerUrl.Trim();

         if (_connSettings.Trusted)
         {
            csb.IntegratedSecurity = true;
         }
         else
         {
            csb.UserID = _connSettings.LoginName.Trim();
            csb.Password = _connSettings.Password.Trim();
         }

         // Set the timeout [seq 20130713°0934]
         // Note : compare note 20130713°0933
         // Ref : MSDN article 'SqlConnection.ConnectionTimeout-Eigenschaft' on (20130713°0943)
         //    msdn.microsoft.com/de-de/library/system.data.sqlclient.sqlconnection.connectiontimeout%28v=vs.90%29.aspx
         if (_connSettings.Timeout > 0)
         {
            // set value other than default (default is 15 seconds)
            csb.ConnectTimeout = _connSettings.Timeout;
         }

         return csb.ConnectionString;                                          // connectString.ToString();
      }

      /// <summary>This method delivers a MS-SQL command object for a given command string</summary>
      /// <remarks>id : 20130604°0727</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         SyDaSq.SqlCommand cmd = ((SyDaSq.SqlConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0728</remarks>
      /// <returns>...</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new MssqlQueryOptions();
      }

      /// <summary>This method executes the SQL command and delivers the result</summary>
      /// <remarks>id : 20130604°0729</remarks>
      /// <param name="command">The IDbCommand object to execute</param>
      /// <returns>The wanted IDbDataAdapter object</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new SyDaSq.SqlDataAdapter((SyDaSq.SqlCommand)command);
      }

      /// <summary>This method builds a query options command and applies it to the connection via ExecuteOnWorker()</summary>
      /// <remarks>
      /// id : 20130604°0730
      /// callers : Only • method 20130604°0336 DbClient::Connect() after the connection has been made
      ///     • method 20130705°1011 QueryForm::ShowQueryOptions() after the QueryOptions modal dialog form was committed
      /// note : After having built the options string, it is given to ExecuteOnWorker(), which applies them to the connection.
      /// </remarks>
      public override void ApplyQueryOptions()
      {
         StringBuilder sb = new StringBuilder();
         MssqlQueryOptions sqo = ((MssqlQueryOptions) queryOptions);
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
      }
   }
}
