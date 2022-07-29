#region Fileinfo
// file        : 20130604°1041 /QueryPony/QueryPonyLib/DbApi/OracleDbClient.cs
// summary     : Class 'OracleDbClient' constitutes an implementation of DbClient for Oracle
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Data;
using OMDAC = Oracle.ManagedDataAccess.Client;                                 // Replaces former System.Data.OracleClient [evt 20200522°0911]

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of DbClient for Oracle</summary>
   /// <remarks>id : 20130604°1042</remarks>
   class OracleDbClient: DbClient
   {
      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1043</remarks>
      public OracleDbClient(ConnSettingsLib settings) : base(settings)
      {
      }

      /// <summary>This method returns an Oracle database connection</summary>
      /// <remarks>id : 20130604°1044</remarks>
      /// <returns>The wanted Oracle database connection</returns>
      protected override IDbConnection GetDbConnection()
      {
         OMDAC.OracleConnection con = new OMDAC.OracleConnection(GenerateConnectionString());
         con.InfoMessage += new OMDAC.OracleInfoMessageEventHandler(con_InfoMessage);
         return con;
      }

      /// <summary>This eventhandler processes the InfoMessage event of this Oracle connection</summary>
      /// <remarks>id 20130604°1045</remarks>
      void con_InfoMessage(object sender, OMDAC.OracleInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, e.Source));
      }

      /// <summary>This method builds an Oracle connectionstring from the connection settings of this DbClient</summary>
      /// <remarks>
      /// id : 20130604°1046
      /// note : Oracle connection strings are e.g. 'Data Source=XE;User ID=joe;Password=joe' [note 20130719°0916]
      /// see : issue 20130719°0912 'Oracle Server/Port properties'
      /// remember : issue 20130719°0912 'Oracle Server/Port properties'
      /// remember : ref 20130719°0913 'devart → OracleConnectionStringBuilder Class'
      /// </remarks>

      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         OMDAC.OracleConnectionStringBuilder csb = new OMDAC.OracleConnectionStringBuilder();

         csb.DataSource = _connSettings.DatabaseName.Trim();                   // [line 20130719°0911]

         // Sequence replaced. See issue 20200522°0921 'IntegratedSecurity missing with Oracle'.
         // // ---------------------------
         // if (_connSettings.Trusted)
         // {
         //    csb.IntegratedSecurity = true;
         // }
         // else
         // {
         //    csb.UserID = _connSettings.LoginName.Trim();
         //    csb.Password = _connSettings.Password.Trim();
         // }
         // // ---------------------------
         csb.UserID = _connSettings.LoginName.Trim();
         csb.Password = _connSettings.Password.Trim();
         // // ---------------------------

         return csb.ConnectionString;
      }

      /// <summary>This method delivers an Oracle command object for a given command string</summary>
      /// <remarks>id : 20130604°1047</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         OMDAC.OracleCommand cmd = ((OMDAC.OracleConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1048</remarks>
      public override QueryOptions GetDefaultOptions()
      {
         return new OracleQueryOptions();
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1049</remarks>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new OMDAC.OracleDataAdapter((OMDAC.OracleCommand)command);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1050</remarks>
      public override void ApplyQueryOptions()
      {
         return;

         /*
         StringBuilder sb = new StringBuilder();
         MssqlQueryOptions sqo = ((SqlQueryOptons)queryOptions);
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
