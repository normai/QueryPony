#region Fileinfo
// file        : 20130604°1001 (20130605°1731) /QueryPony/QueryPonyLib/DbApi/OledbDbClient.cs
// summary     : This file stores class 'OledbDbClient' to constitute an implementation of DbClient for OleDb.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace QueryPonyLib
{
   /// <summary>This class constitutes an implementation of DbClient for OleDb</summary>
   /// <remarks>id : 20130604°1002 (20130605°1732)</remarks>
   class OledbDbClient : DbClient
   {
      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1003 (20130605°1733)</remarks>
      /// <param name="settings">...</param>
      public OledbDbClient(ConnSettingsLib settings) : base(settings)
      {
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°1004 (20130605°1734)</remarks>
      public OleDbConnection Connection
      {
         get { return (OleDbConnection) _connection; }
      }

      /// <summary>This method returns an OleDb database connection</summary>
      /// <remarks>id : 20130604°1005 (20130605°1735)</remarks>
      /// <returns>The wanted OleDb database connection</returns>
      protected override IDbConnection GetDbConnection()
      {
         OleDbConnection con = new OleDbConnection(GenerateConnectionString());
         con.InfoMessage += new OleDbInfoMessageEventHandler(con_InfoMessage);
         return con;
      }

      /// <summary>This eventhandler processes the InfoMessage event of this OleDb connection</summary>
      /// <remarks>id : 20130604°1006 (20130605°1736)</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void con_InfoMessage(object sender, OleDbInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, ""));
      }

      /// <summary>This method builds a OleDb connectionstring from the connection settings of this DbClient</summary>
      /// <remarks>id : 20130604°1007 (20130605°1737)</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         OleDbConnectionStringBuilder csb = new OleDbConnectionStringBuilder();
         csb.ConnectionString = _connSettings.DatabaseConnectionstring;
         return csb.ConnectionString;
      }

      /// <summary>This method delivers an OleDb command object for a given command string</summary>
      /// <remarks>id : 20130604°1008 (20130605°1738)</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         OleDbCommand cmd = ((OleDbConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }

      /// <summary>This method retrieves the default QueryOptions</summary>
      /// <remarks>id : 20130604°1009 (20130605°1739)</remarks>
      /// <returns>The wanted default QueryOptions</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new OledbQueryOptions();
      }

      /// <summary>This method creates an IDbDataAdapter for the given command</summary>
      /// <remarks>id : 20130604°1017 (20130605°1740)</remarks>
      /// <param name="command">The command for which to create a DbDataAdapter</param>
      /// <returns>The wanted (IDbDataAdapter</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new OleDbDataAdapter((OleDbCommand) command);
      }

      /// <summary>This method applies QueryOptions to this DbClient</summary>
      /// <remarks>id : 20130604°1010 (20130605°1741)</remarks>
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
