#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DatabaseApi/MysqlDbClient.cs
// id          : 20130612°0921 (20130604°0721)
// summary     : This file stores class 'MysqlDbClient' to constitute an implementation of DbClient for MySQL.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from MssqlDbClient.cs and modified (20130612°0921)
// note        :
// callers     :
#endregion Fileinfo

#if MYSQL20130619YES
using MySql.Data; // available for .NET 2 and .NET 4, we are on .NET 3.5, thus select v2 (20130611°0911)
#endif

using System.Data;
using System.Text;

namespace QueryPonyLib
{

   /// <summary>This class constitutes an implementation of DbClient for MySQL.</summary>
   /// <remarks>
   /// id : 20130612°0922 (20130604°0722)
   /// note : This class was copied/adjusted from MssqlDbClient. But it looks like we had
   ///        better choosen e.g. OleDb/SqliteDbClient as the starting point. The MS-SQL
   ///        method set and the OleDb/SQLite method set are pretty different! E.g.
   ///        OleDb/SQLite have a 'SQLiteConnection Connection' property, and MSSSQL not.
   ///        [20130614°1412]
   /// </remarks>
   class MysqlDbClient : DbClient
   {

      /// <summary>This constructor...</summary>
      /// <remarks>id : 20130612°0923 (20130604°0723)</remarks>
      /// <param name="settings">...</param>
      public MysqlDbClient(ConnSettingsLib settings) : base(settings)
      {
      }

#if MYSQL20130619YES

      /// <summary>This property gets ...</summary>
      /// <remarks>
      /// id : 20130614°1421 (20130604°1004)
      /// issue : What is the difference/advantage/disadvantage between this property
      ///         borrowed from OledbDbClient and below GetDbConnection() borrowed from
      ///         MssqlDbClient? Can we standardize the mechanism and use only one of
      ///         the two throughout all databases? (issue 20130614°1422)
      /// </remarks>
      public MySql.Data.MySqlClient.MySqlConnection Connection
      {
         get
         {
            return (MySql.Data.MySqlClient.MySqlConnection) _connection;
         }
      }

#endif


      /// <summary>This method returns a MySQL database connection.</summary>
      /// <remarks>
      /// id : 20130612°0924 (20130604°0724)
      /// issue : This method borrowed from MssqlDbClient might not be used at all with
      ///         MysqlClient, since we use above MySqlConnection property. Clear the
      ///         situation and possibly eliminate this method.  (issue 20130614°1423)
      /// </remarks>
      /// <returns>The wanted MySQL database connection</returns>
      protected override IDbConnection GetDbConnection()
      {

#if MYSQL20130619YES

         // note : [20130701°1257] working connection strings are e.g.:
         // - sConn = "Database=contao;Data Source=localhost;User Id=myusername;Password=mypassword"; // the former hardcoded manual value  [20130701°125702]
         // - sConn = "database=contao;server=127.0.0.1;port=3306;User Id=myusername;password=mypassword"; // coming now from GenerateConnectionString()  [20130701°125703]
         string sConn = GenerateConnectionString();

         MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(sConn);

         ////con.InfoMessage += new SqlInfoMessageEventHandler(con_InfoMessage);
         /*
         sConn = GenerateConnectionString();
         System.Data.SqlClient.SqlConnection conDummy = new System.Data.SqlClient.SqlConnection(sConn);
         conDummy.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(con_InfoMessage); // (see issue 20130612°1131)
         */

         return con;

#else
         return null;
#endif

      }


      /// <summary>
      /// This eventhandler processes the InfoMessage event of this MySQL
      ///  connection. But it is nowhere attached (see issue 20130607°1311).
      /// </summary>
      /// <remarks>id : 20130612°0925 (20130604°0725)</remarks>
      /// <remarks>
      /// issue : Class 'SqlInfoMessageEventArgs' seems not available in other databases. Find
      ///          out the exact implications and provide a workaround. (issue 20130612°1131)
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void con_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
      {
         OnInfoMessage(sender, new  InfoMessageEventArgs(e.Message, e.Source));
      }


      /// <summary>This method builds a MySQL connectionstring from the connection settings of this DbClient.</summary>
      /// <remarks>id : 20130612°0926 (20130604°0726)</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected override string GenerateConnectionString()
      {

#if MYSQL20130619YES

         // get empty connectionstring
         MySql.Data.MySqlClient.MySqlConnectionStringBuilder csb = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();


         // set basic information
         csb.Server = _connSettings.DatabaseServerUrl.Trim();
         csb.Database = _connSettings.DatabaseName.Trim();


         // process and set port number (sequence 20130709°0941)
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         /*
         string[] ar = csb.Server.Split(':');
         if (ar.Length == 1)
         {
            // server is e.g. "127.0.0.1", "localhost", supplement port explicitly (not sure whether this is necessary)
            csb.Port = 3306;
         }
         else if (ar.Length == 2)
         {
            // port was given with the server url, e.g. "127.0.0.1:3307"
            csb.Server = ar[0];
            uint ui = 0;
            try
            {
               ui = uint.Parse(ar[1]);
            }
            catch (System.Exception ex)
            {
               string sErr = ex.Message;
               // todo : Supplement fatal error processing. (todo 20130709°0943)
            }
            csb.Server = ar[0];
            csb.Port = ui;
         }
         else
         {
            // todo : Supplement fatal error processing (todo 20130709°0942)
         }
         */
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         string sServer = csb.Server;
         //////uint uiPortnumber = csb.Port;
         int iPortnumber = (int) csb.Port;
         //////string s = IOBus.Utils.extractPortnumberFromUrl(csb.Server, out sServer, out uiPortnumber);
         string s = IOBus.Utils.extractPortnumberFromUrl(csb.Server, out sServer, out iPortnumber); // fix 20180819°0121`03
         if (s != "")
         {
            // fatal
            System.Exception ex = new System.Exception(s);
            throw (ex);
         }
         csb.Server = sServer;
         csb.Port = (uint) ( (iPortnumber < 1) ? ((int) Glb.DbSpecs.MysqlDefaultPortnum) : iPortnumber ); // 3306


         // process and set credentials
         if (_connSettings.Trusted)
         {
            csb.IntegratedSecurity = true;
         }
         else
         {
            csb.UserID = _connSettings.LoginName.Trim();
            csb.Password = _connSettings.Password.Trim();
         }


         // connectionstring ready
         return csb.ConnectionString;

#else
         return "";
#endif

      }


      /// <summary>This method delivers a MySQL command object for a given command string.</summary>
      /// <remarks>id : 20130612°0927 (20130604°0727)</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected override IDbCommand GetDbCommand(string sQuery)
      {

#if MYSQL20130619YES

         MySql.Data.MySqlClient.MySqlCommand cmd = ((MySql.Data.MySqlClient.MySqlConnection)_connection).CreateCommand();
         cmd.CommandText = sQuery;
         return cmd;
#else
         return null;
#endif

      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130612°0928 (20130604°0728)</remarks>
      /// <returns>...</returns>
      public override QueryOptions GetDefaultOptions()
      {
         return new MysqlQueryOptions();
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130612°0929 (20130604°0729)</remarks>
      /// <param name="command">...</param>
      /// <returns>...</returns>
      protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
      {

#if MYSQL20130619YES

         return new MySql.Data.MySqlClient.MySqlDataAdapter( (MySql.Data.MySqlClient.MySqlCommand) command );

#else

         return null;

#endif

      }


      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130612°0930 (20130604°0730)
      /// todo : Continue implementation of ApplyQueryOptions() for all *DbClient (todo 20130619°1321)
      /// </remarks>
      public override void ApplyQueryOptions()
      {
         return;

         // This sequence comes by the cloning from MssqlDbClient, and is not valid for
         //  MySQL, since that's style guide then become SqliteDbClient. Good luck,
         //  we can unpunished shutdown it, that eases refactor 20130619°1311.
         /*
         StringBuilder sb = new StringBuilder();
         MssqlQueryOptions sqo = ((MysqlQueryOptons) queryOptions);
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
