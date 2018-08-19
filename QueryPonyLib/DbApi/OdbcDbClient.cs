#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DbApi/OdbcDbClient.cs
// id          : 20130604°0901
// summary     : This file stores class 'OdbcClient' to constitute an implementation of DBClient for ODBC.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Data;
using System.Data.Odbc;

namespace QueryPonyLib
{

   /// <summary>This class constitutes an implementation of DBClient for ODBC.</summary>
   /// <remarks>id : 20130604°0902</remarks>
   class OdbcClient : DbClient
   {

      /// <summary>This constructor...</summary>
      /// <remarks>id : 20130604°0903</remarks>
      public OdbcClient(ConnSettingsLib settings) : base(settings)
      {
      }


      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°0904</remarks>
      public OdbcConnection Connection
      {
         get { return (OdbcConnection) _connection; }
      }


      /// <summary>This method returns an ODBC database connection.</summary>
      /// <remarks>id : 20130604°0905</remarks>
      /// <returns>The wanted ODBC database connection</returns>
      protected override IDbConnection GetDbConnection()
      {
         OdbcConnection con = new OdbcConnection(GenerateConnectionString());
         con.InfoMessage += new OdbcInfoMessageEventHandler(con_InfoMessage);
         return con;
      }


      /// <summary>This eventhandler processes the InfoMessage event of this ODBC connection.</summary>
      /// <remarks>id : 20130604°0906</remarks>
      void con_InfoMessage(object sender, OdbcInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, ""));
      }


      /// <summary>This method builds an ODBC connectionstring from the connection settings of this DbClient.</summary>
      /// <remarks>id : 20130604°0907</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         OdbcConnectionStringBuilder csb = new OdbcConnectionStringBuilder();
         csb.ConnectionString = _connSettings.DatabaseConnectionstring;

         return csb.ConnectionString;                                                  // connectString.ToString();
      }


      /// <summary>This method delivers an ODBC command object for a given command string.</summary>
      /// <remarks>id : 20130604°0908</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         OdbcCommand cmd = ((OdbcConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0909</remarks>
      public override QueryOptions GetDefaultOptions()
      {
         return new ODBCQueryOptions();
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0911</remarks>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new OdbcDataAdapter((OdbcCommand) command);
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0912</remarks>
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
