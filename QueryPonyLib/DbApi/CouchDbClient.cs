#region Fileinfo
// file        : 20130616°1631 (20130605°1731) /QueryPony/QueryPonyLib/DbApi/CouchDbClient.cs
// summary     : This file homes class 'CouchDbClient' to constitute an
//                experimental implementation of DbClient for CouchDB
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from SqliteDbClient.cs and modified (20130616°1631)
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QueryPonyLib
{

   /// <summary>This class constitutes an implementation of DbClient for CouchDB</summary>
   /// <remarks>id : 20130616°1632 (20130605°1732)</remarks>
   class CouchDbClient : DbClient
   {

      /// <summary>This constructor creates a CouchDB DbClient according the given connection settings</summary>
      /// <remarks>id : 20130616°1633 (20130605°1733)</remarks>
      /// <param name="settings">The event object itself</param>
      public CouchDbClient(ConnSettingsLib connSettings) : base(connSettings)
      {
      }

      /// <summary>This property gets a database connection</summary>
      /// <remarks>id : 20130616°1634 (20130605°1734)</remarks>
      public System.Data.CouchDB.CouchDBConnection Connection
      {
         get { return (System.Data.CouchDB.CouchDBConnection)_connection; }
      }

      /// <summary>This method builds a CouchDB connectionstring from the connection settings of this DbClient</summary>
      /// <remarks>
      /// id : 20130616°1635 (20130605°1735)
      /// note : Just for fun, here how the IDbConnection interface looks like:
      ///        ----------------------------------------------
      ///        namespace System.Data
      ///        {
      ///           // Summary:
      ///           //     Represents an open connection to a data source, and is implemented
      ///           //     by .NET Framework data providers that access relational databases.
      ///           public interface IDbConnection : IDisposable
      ///           {
      ///              string ConnectionString { get; set; }
      ///              int ConnectionTimeout { get; }
      ///              string Database { get; }
      ///              ConnectionState State { get; }
      ///              IDbTransaction BeginTransaction();
      ///              IDbTransaction BeginTransaction(IsolationLevel il);
      ///              void ChangeDatabase(string databaseName);
      ///              void Close();
      ///              IDbCommand CreateCommand();
      ///              void Open();
      ///           }
      ///        }
      ///        ----------------------------------------------
      /// </remarks>
      /// <returns>The wanted CouchDB database connection</returns>
      protected override IDbConnection GetDbConnection()
      {

         // [seq 20130606°1322`03]
         string sCon = GenerateConnectionString();
         System.Data.CouchDB.CouchDBConnection con = new System.Data.CouchDB.CouchDBConnection();
         con.ConnectionString = sCon;

         return (System.Data.CouchDB.CouchDBConnection)con;
      }

      /// <summary>
      /// This eventhandler processes the InfoMessage event of this CouchDB
      ///  connection. But it is nowhere attached (see issue 20130607°1311).
      /// </summary>
      /// <remarks>id : 20130616°1636 (20130605°1736)</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void con_InfoMessage(object sender, System.Data.CouchDB.CouchDBInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new InfoMessageEventArgs(e.Message, ""));
      }

      /// <summary>This method builds a connectionstring</summary>
      /// <remarks>id : 20130616°1637 (20130605°1737)</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {
         string sRet = "";

         /*
         SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder();
         csb.ConnectionString = "Data Source=" + _conSettings.SqliteFile;
         return csb.ConnectionString;
         */

         // Experimental [seq 20130723°0921]
         string sUrlPlain = "";
         int iPortnumber = 0;
         string sErr = IOBus.Utils.extractPortnumberFromUrl(_connSettings.DatabaseServerUrl, out sUrlPlain, out iPortnumber); // fix 20180819°0121`02
         if (sErr != "")
         {
            // Fatal
            // Todo 20130723°0932 : Provide error processing.
         }
         iPortnumber = (iPortnumber < 1) ? ((int) Glb.DbSpecs.CouchDefaultPortnum) : iPortnumber;
         _connSettings.DatabaseServerPortnum = iPortnumber;

         sRet = sUrlPlain + Glb.DbSpecs.sSepaUrlPort + iPortnumber.ToString();

         return sRet;
      }

      /// <summary>This method delivers a CouchDB command object for a given command string</summary>
      /// <remarks>id : 20130616°1638 (20130605°1738)</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {
         /*
         SQLiteCommand cmd = ((SQLiteConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
         */

         System.Data.CouchDB.CouchDBCommand cmd = (System.Data.CouchDB.CouchDBCommand)((System.Data.CouchDB.CouchDBConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1639 (20130605°1739)</remarks>
      /// <returns>...</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new CouchQueryOptions();
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1640 (20130605°1740)</remarks>
      /// <param name="command">...</param>
      /// <returns>...</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {
         return new System.Data.CouchDB.CouchDBDataAdapter((System.Data.CouchDB.CouchDBCommand)command);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1641 (20130605°1741)</remarks>
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
