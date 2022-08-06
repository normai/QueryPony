#region Fileinfo
// file        : 20130604°0201 /QueryPony/QueryPonyLib/DbApi/DbClient.cs
// summary     : This file stores the DataClient-Event-Argument-classes 'DataReaderAvailableEventArgs',
//                'TableSchemaAvailableEventArgs', 'DataRowAvailableEventArgs', 'CommandDoneEventArgs',
//                'InfoMessageEventArgs', 'ErrorEventQeArgs' and the class 'DbClient' to represent
//                the connection to a database.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace QueryPonyLib
{

   #region DataClient Event Argument Classes

   /// <summary>This class constitutes a DataReaderAvailableEventArgs object</summary>
   /// <remarks>id : 20130604°0202</remarks>
   public class DataReaderAvailableEventArgs : EventArgs
   {
      /// <summary>This field stores the IDataReader of this DataReaderAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0203</remarks>
      private readonly IDataReader _dr;                        // Add readonly keyword [chg 20220731°1131`01]

      /// <summary>This field stores the SkipResults flag of this DataReaderAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0204</remarks>
      private bool _skipResults = false;

      /// <summary>This property gets the IDataReader of this DataReaderAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0205</remarks>
      public IDataReader dr
      {
         get { return _dr; }
      }

      /// <summary>This property gets/sets the SkipResults flag of this DataReaderAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0206</remarks>
      public bool SkipResults
      {
         get { return _skipResults; }
         set { _skipResults = value; }
      }

      /// <summary>This constructor creates a new DataReaderAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0207</remarks>
      /// <param name="_dr">The IDataReader object</param>
      public DataReaderAvailableEventArgs(IDataReader _dr)
      {
         this._dr = _dr;
      }
   }

   /// <summary>This class constitutes a TableSchemaAvailableEventArgs object</summary>
   /// <remarks>id : 20130604°0208</remarks>
   public class TableSchemaAvailableEventArgs : EventArgs
   {
      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0209</remarks>
      private DataTable _schema;

      /// <summary>This property gets/sets the schema of this TableSchemaAvailableEventArgs' object</summary>
      /// <remarks>id : 20130604°0210</remarks>
      public DataTable Schema
      {
         get { return _schema; }
         set { _schema = value; }
      }

      /// <summary>This constructor creates a TableSchemaAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0211</remarks>
      public TableSchemaAvailableEventArgs(DataTable dtSchema)
      {
         this._schema = dtSchema;
      }
   }

   /// <summary>This class constitutes a DataRowAvailableEventArgs object</summary>
   /// <remarks>id : 20130604°0212</remarks>
   public class DataRowAvailableEventArgs : EventArgs
   {
      /// <summary>This public field stores the data fields of this DataRowAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0213</remarks>
      public object[] DataFields;

      /// <summary>This constructor creates a DataRowAvailableEventArgs object</summary>
      /// <remarks>id : 20130604°0214</remarks>
      /// <param name="dataFields">The data fields to be represented</param>
      public DataRowAvailableEventArgs(object[] dataFields)
      {
         DataFields = dataFields;
      }
   }

   /// <summary>This class constitutes an CommandDoneEventArgs object</summary>
   /// <remarks>id : 20130604°0215</remarks>
   public class CommandDoneEventArgs : EventArgs
   {
      /// <summary>This field stores the number of affected rows of the executed SQL command</summary>
      /// <remarks>id : 20130604°0216</remarks>
      private readonly int _recordsAffected;                   // Add readonly keyword [chg 20220731°1131`02]

      /// <summary>This property gets the number of affected rows of the executed SQL command</summary>
      /// <remarks>id : 20130604°0217</remarks>
      public int RecordsAffected
      {
         get { return _recordsAffected; }
      }

      /// <summary>This constructor creates a new CommandDoneEventArgs object</summary>
      /// <remarks>id : 20130604°0218</remarks>
      /// <param name="_RecordAffected">The number of affected records</param>
      public CommandDoneEventArgs(int iRecordAffected)
      {
         this._recordsAffected = iRecordAffected;
      }
   }

   /// <summary>This class constitutes an InfoMessageEventArgs object</summary>
   /// <remarks>id : 20130604°0219</remarks>
   public class InfoMessageEventArgs : EventArgs
   {
      /// <summary>This field stores the value of the Message property</summary>
      /// <remarks>id : 20130604°0220</remarks>
      private readonly string _message;                        // Add readonly keyword [chg 20220731°1131`03]

      /// <summary>This field stores the value of the Source property</summary>
      /// <remarks>id : 20130604°0221</remarks>
      private readonly string _source;                         // Add readonly keyword [chg 20220731°1131`04]

      /// <summary>This property gets this InfoMessageEventArgs' message</summary>
      /// <remarks>id : 20130604°0222</remarks>
      public string Message
      {
         get { return _message; }
      }

      /// <summary>This property gets this InfoMessageEventArgs' source</summary>
      /// <remarks>id : 20130604°0223</remarks>
      public string Source
      {
         get { return _source; }
      }

      /// <summary>This constructor creates a new InfoMessageEventArgs object</summary>
      /// <remarks>id : 20130604°0224</remarks>
      /// <param name="Message">The event message</param>
      /// <param name="Source">The event source</param>
      public InfoMessageEventArgs(string Message, string Source)
      {
         _message = Message;
         _source = Source;
      }
   }

   /// <summary>This class constitutes an ErrorEventQeArgs object</summary>
   /// <remarks>
   /// id : 20130604°0225
   /// </remarks>
   public class ErrorEventQeArgs : EventArgs
   {
      /// <summary>This field stores the value of the ErrorMessage property</summary>
      /// <remarks>id : 20130604°0226</remarks>
      private readonly string _errorMessage;                   // Add readonly keyword [chg 20220731°1131`05]

      /// <summary>This field stores the value of the Ex propery</summary>
      /// <remarks>id : 20130604°0227</remarks>
      private readonly Exception _ex;                          // Add readonly keyword [chg 20220731°1131`06]

      /// <summary>This field stores the value of the Cancel property</summary>
      /// <remarks>id : 20130604°0228</remarks>
      private bool _cancel = true;

      /// <summary>This property gets the error message of this ErrorEventQeArgs object</summary>
      /// <remarks>id : 20130604°0229</remarks>
      public string ErrorMessage
      {
         get { return _errorMessage; }
      }

      /// <summary>This property gets the exception of this ErrorEventQeArgs object</summary>
      /// <remarks>
      /// id : 20130604°0230
      /// note : This seems used from nowhere.
      /// </remarks>
      public Exception Ex
      {
         get { return _ex; }
      }

      /// <summary>This property.gets/sets the 'Cancel' flag of this ErrorEventQeArgs object</summary>
      /// <remarks>id : 20130604°0231</remarks>
      public bool Cancel
      {
         get { return _cancel; }
         set { _cancel = value; }
      }

      /// <summary>This constructor creates a custom exception</summary>
      /// <remarks>id : 20130604°0232</remarks>
      /// <param name="errorMessage">The error message</param>
      /// <param name="ex">The thrown exception</param>
      public ErrorEventQeArgs(string errorMessage, Exception ex)
      {
         this._errorMessage = errorMessage;
         this._ex = ex;
      }
   }

   #endregion DataClient Event Argument Classes

   /// <summary>This abstract class represents the common abstraction of a database connection</summary>
   /// <remarks>
   /// id : 20130604°0233
   /// note : Access modifier set from 'internal' to 'public' to make class available for other projects (20130604°1423)
   /// note : (20130604°023302) There are different implementations for
   ///    ODBC (<see cref="ODBCClient"/>), OLEDB (<see cref="OledbClient"/>),
   ///    Oracle (<see cref="OracleDbClient"/>), MsSqlServer (<see cref="SqlDbClient"/>).
   /// </remarks>
   public abstract class DbClient
   {
      /// <summary>This public enum defines the possible RunStates of a DbClient</summary>
      /// <remarks>id : 20130604°0234</remarks>
      public enum RunStates
      {
         /// <summary>This enum value indicates that this DbClient is idle</summary>
         /// <remarks>id : 20130604°0114</remarks>
         Idle,

         /// <summary>This enum value indicates that this DbClient is running</summary>
         /// <remarks>id : 20130604°0115</remarks>
         Running,

         /// <summary>This enum value indicates that this DbClient is cancelling</summary>
         /// <remarks>id : 20130604°0116</remarks>
         Cancelling
      };

      #region Events

      /// <summary>This public field stores the DataReaderAvailableEventArgs eventhandler</summary>
      /// <remarks>
      /// id : 20130604°0235
      /// note : This event seems involved in issue 20130822°2111 'DbClient events trigger QueryForm methods'
      /// </remarks>
      public event EventHandler<DataReaderAvailableEventArgs> DataReaderAvailable;

      /// <summary>This public field stores the TableSchemaAvailableEventArgs eventhandler</summary>
      /// <remarks>id : 20130604°0236</remarks>
      public event EventHandler<TableSchemaAvailableEventArgs> TableSchemaAvailable;

      /// <summary>This public field stores the DataRowAvailableEventArgs eventhandler</summary>
      /// <remarks>id : 20130604°0237</remarks>
      public event EventHandler<DataRowAvailableEventArgs> DataRowAvailable;

      /// <summary>This public field stores the InfoMessageEventArgs eventhandler</summary>
      /// <remarks>id : field 20130604°0238</remarks>
      public event EventHandler<InfoMessageEventArgs> InfoMessage;

      /// <summary>This public field stores the eventhandler processing any exceptions</summary>
      /// <remarks>id : field 20130604°0239</remarks>
      public event EventHandler<ErrorEventQeArgs> Error;

      /// <summary>
      /// This public field stores the CancelDone eventhandler, an
      ///  event to inform a caller when cancel action has completed.
      /// </summary>
      /// <remarks>id : 20130604°0240</remarks>
      public event EventHandler CancelDone;

      /// <summary>This public field stores the CommandDoneEventArgs eventhandler</summary>
      /// <remarks>id : 20130604°0241</remarks>
      public event EventHandler<CommandDoneEventArgs> CommandDone;

      /// <summary>This public field stores the BatchDone eventhandler</summary>
      /// <remarks>id : 20130604°0242</remarks>
      public event EventHandler BatchDone;

      #endregion Events

      #region Internal Variables

      /// <summary>This protected field stores the connection settings for this DbClient</summary>
      /// <remarks>id : 20130604°0243</remarks>
      protected ConnSettingsLib _connSettings;

      /// <summary>This protected field stores the actual connection for this DbClient</summary>
      /// <remarks>id : 20130604°0244</remarks>
      protected IDbConnection _connection;

      /// <summary>This protected field stores the command to execute by this DbClient</summary>
      /// <remarks>id : 20130604°0245</remarks>
      protected IDbCommand _selectCommand;

      /// <summary>This public field stores the QueryOptions of this DbClient</summary>
      /// <remarks>
      /// id : 20130604°0246
      /// note : Access modifier set 'public' to make it accessible from other projects (20130604°1424)
      /// </remarks>
      public QueryOptions queryOptions;

      /// <summary>This protected field stores the 'Connected' flag of this DbClient</summary>
      /// <remarks>id : 20130604°0247</remarks>
      protected bool _connected = false;

      /// <summary>This protected field stores the '_task' delegate of this DbClient</summary>
      /// <remarks>id : 20130604°0248</remarks>
      private MethodInvoker _task;

      /// <summary>This protected field stores the background thread for running queries</summary>
      /// <remarks>id : 20130604°0249</remarks>
      protected Thread _workerThread;

      /// <summary>This protected field stores the run state of this DbClient</summary>
      /// <remarks>id : 20130604°0250</remarks>
      protected RunStates _runState = RunStates.Idle;

      /// <summary>This protected field stores the DataSet we're going to fill by executing a query</summary>
      /// <remarks>id : 20130604°0251</remarks>
      private DataSet _syncDataSet = new DataSet();

      /// <summary>This protected field stores the SQL command string which will be executed by DoExecuteSync()</summary>
      /// <remarks>id : 20130604°0252</remarks>
      private string _syncQuery;

      /// <summary>
      /// This protected field stores the seconds(?) timeout which shall
      ///  be used if the SQL command is executed by DoExecuteSync().
      ///  </summary>
      /// <remarks>id : 20130604°0253</remarks>
      private int _syncTimeOut;

      /// <summary>This protected field stores ...</summary>
      /// <remarks>id : 20130604°0254</remarks>
      private ArrayList _queries;

      /// <summary>This protected field stores the last error message</summary>
      /// <remarks>
      /// id : 20130604°0255
      /// note : Convert this to an automatic property (20130821°1121).
      ///    No, do not convert it, because I want it for debugging.
      /// </remarks>
      private string _errorMessage;

      /// <summary>This protected field stores the start time of an asyncronous batch run</summary>
      /// <remarks>id : 20130604°0256</remarks>
      private DateTime _execStartTime;

      /// <summary>This protected field stores the start duration of an asyncronous batch run</summary>
      /// <remarks>id : 20130604°0257</remarks>
      private TimeSpan _execDuration = TimeSpan.Zero;

      #endregion Internal Variables

      #region Constructor

      /// <summary>This constructor creates a DbClient object for the given connection settings</summary>
      /// <remarks>id : 20130604°0258</remarks>
      /// <param name="settings">The connection settings for the client to create</param>
      public DbClient(ConnSettingsLib settings)
      {
         _connSettings = settings;

         queryOptions = GetDefaultOptions();

         // Note 20130604°0258`02
         //  Given that the ODBC classes appear to be apartment threaded, we can't just
         //  spawn worker threads to execute background queries as required, since the
         //  connection will have been created on a different thread. The easiest way
         //  around this is to start a worker thread now, keeping it alive for the duration
         //  of the DbClient object, and have that thread process all database commands
         //  like connections, disconnections, queries, etc.

         // Provide worker thread for the later wanted actions on this DbClient (read 20130604°025802)
         this._workerThread = new Thread(new ThreadStart(StartWorker));

         // Todo : Possibly add the database type to the worker thread name (todo 20130620°1631)
         _workerThread.Name = "DbClient Worker Thread";

         _workerThread.Start();
      }

      #endregion Constructor

      #region Abstract Functions

      /// <summary>This abstract function returns a database connection</summary>
      /// <remarks>id : 20130604°0259</remarks>
      /// <returns>The wanted database connection</returns>
      protected abstract IDbConnection GetDbConnection();

      /// <summary>This abstract function builds a connectionstring from the connection settings of the DbClient</summary>
      /// <remarks>id : 20130604°0302</remarks>
      /// <returns>The wanted connectionstring</returns>
      protected abstract string GenerateConnectionString();

      /// <summary>This abstract function delivers a command object for a given command string</summary>
      /// <remarks>id : 20130604°0303</remarks>
      /// <param name="sQuery">The command string for which to get a command object</param>
      /// <returns>The wanted command object</returns>
      protected abstract IDbCommand GetDbCommand(string sQuery);

      /// <summary>This abstract function retrieves an IDbDataAdapter</summary>
      /// <remarks>id : 20130604°0304</remarks>
      /// <returns>The wanted IDbDataAdapter</returns>
      protected abstract IDbDataAdapter GetDataAdapter(IDbCommand command);

      /// <summary>This abstract function applies the QueryOptions</summary>
      /// <remarks>id : 20130604°0434</remarks>
      public abstract void ApplyQueryOptions();

      #endregion Abstract Functions

      #region Protected Functions

      /// <summary>This method starts the background thread event loop</summary>
      /// <remarks>id : 20130604°0305</remarks>
      protected void StartWorker()
      {
         do
         {
            // Wait for the host thread to wake us up
            // note : We have to use Sleep() rather than Suspend() because Suspend()
            //    sometimes hogs the CPU on NT4 (bug in beta 2?). [note 20130604°030502]
            //Thread.CurrentThread.Suspend();
            try
            {
               Thread.Sleep(Timeout.Infinite);
            }
            catch (System.Threading.ThreadInterruptedException)
            {
               // The wakeup call, i.e. Interrupt() will throw an exception.
               // If we've been given nothing to do, it's time to exit (the form's being closed)
            }
            catch (Exception ex)
            {
               System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            if (this._task == null)
            {
               break;
            }

            // Otherwise, execute the given task
            this._task();
            this._task = null;
         }
         while (true);
      }

      /// <summary>This method finishes the worker thread of the database connection</summary>
      /// <remarks>id : 20130604°0306</remarks>
      protected void StopWorker()
      {
         WaitForWorker();

         // End the thread cleanly
         _workerThread.Interrupt(); // interrupt the thread without a task - this will end it
         _workerThread.Join(); // wait for it to end
      }

      /// <summary>
      /// This method is a wrapper for RunOnWorker with the run-synchronous flag,
      ///  calling that asynchronously.
      /// </summary>
      /// <remarks>id : 20130604°0307</remarks>
      /// <param name="method">The method to run (DoExecuteBatchesAsync)</param>
      protected void RunOnWorker(MethodInvoker method)
      {
         RunOnWorker(method, false);
      }

      /// <summary>This method runs the given method on the worker thread either synchronously or not</summary>
      /// <remarks>id : 20130604°0308</remarks>
      /// <param name="method">The method the worker shall run</param>
      /// <param name="synchronous">Flag whether to run the method synchronously or not</param>
      protected void RunOnWorker(MethodInvoker method, bool bSynchronous)
      {
         // Already doing something?
         if (_task != null)
         {
            // Give it 100 ms to finish ...
            Thread.Sleep(100);

            if (_task != null)
            {
               // Still not finished — Cannot run new task
               return;
            }
         }

         WaitForWorker();
         _task = method;
         _workerThread.Interrupt();

         if (bSynchronous)
         {
            WaitForWorker();
         }
      }

      /// <summary>This method waits for worker thread to become available</summary>
      /// <remarks>id : 20130604°0309</remarks>
      protected void WaitForWorker()
      {
         while (_workerThread.ThreadState != ThreadState.WaitSleepJoin || _task != null)
         {
            Thread.Sleep(20);

            if (_workerThread.ThreadState == ThreadState.Stopped)
            {
               break;
            }
         }
      }

      /// <summary>This method applies QueryOptions to this connection</summary>
      /// <remarks>
      /// id : 20130604°0310
      /// todo : Find out, who exactly shall call this method and try to call it [todo 20130714°1811]
      /// </remarks>
      protected void DoApplyOptionsToConnection_NOTYETCALLED()
      {
         queryOptions.ApplyToConnection(_connection);
      }

      /// <summary>This method completes the connection</summary>
      /// <remarks>id : 20130604°0311</remarks>
      protected void DoConnect()
      {
         if (_connected)
         {
            return;
         }

         try
         {
            _connection = GetDbConnection();

            /*
            note 20130713°0933 ''
            Text : If the connection is not available, below Open() takes 15 secondes
               until it returns unsuccessful. I would like to give it a timeout of
               e.g. one seconds only for just a ping, as e.g. in method 20130713°0921
               ConnectForm.getDatabaseList(). The connection object has a ConnectionTimeout
               property, but only with a getter. How can we *set* it? Solution see
               sequence 20130713°0934 in MssqlDbClient::GenerateConnectionString().
            */

            int iTimoutDebug = _connection.ConnectionTimeout;                  // This are seconds, it's a getter only, no setter, it is e.g. 15
            _connection.Open();
            _connected = true;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Set the database [seq 20130714°1745]
            // Note : This seems the answer for question 20130714°1743 'how to set
            //    the databasee'. I wonder why e.g. MySQL did work correctly so far
            //    without this sequence. The sequence was invented when testing MS-SQL.
            // Note : Findings on how this sequence affects the various databases:
            //    - MS-SQL : ok (tested 20130714°1746)
            //    - MySQL  : ok (tested 20130714°1747)
            //    - Oracle : Exception "Oracle does not support changing databases.",
            //       but then it continues and shows the database given in the
            //       connectionstring. (20130719°0915)
            //    - SQLite : Was fine for usual connection, but needs be skipped for
            //       DbClone. SQLite Database is always 'main'. (issue 20130821°1131)
            // Note : When renaming class ConnectionSettgingsLib to ConnSettings, the VS
            //    refactor function told a warning like 'references whose reference will
            //    no longer be the same', for the line marked with 20130719°0917. But
            //    I could not see anything wrong here. [note 20130723°1431]
            if (! System.String.IsNullOrEmpty(_connSettings.DatabaseName))
            {
               if ((_connSettings.Type != ConnSettingsLib.ConnectionType.Oracle)  // [line 20130719°0917]
                   && (_connSettings.Type != ConnSettingsLib.ConnectionType.Sqlite)  // this.Database is always 'main' [line 20130821°1132]
                    )
               {
                  this.Database = _connSettings.DatabaseName;                  // Here stroke issue 20130821°1131
               }
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         }
         catch (Exception e)
         {
            // Debug [seq 20130716°0623] this.Error seems null but should be something
            EventHandler<ErrorEventQeArgs> ehDebug = this.Error;

            /*
            Todo 20130713°0932 'Make exception message appear on surface'
            Title : Somehow show exception message.
            Symptom : The exception silently fails, the message does not propagate to the surface.
            Todo : Make the exception appear on the surface.
            Location : DbClient.cs method 20130604°0311 DoConnect
            Status : Open
            */

            /*
            Note 20130614°0723 'About setting the database'
            Text : Open throws exceptions e.g.:
               • "MySql.Data.MySqlClient.MySqlException: Authentication to host '' for user
                   'root' using method 'mysql_native_password' failed with message: Unknown
                   database 'localhost:3306' [20130614°0721]
               • "Object reference not set to an instance of an object."
                    (if MySQL is not supported but selected) [20130614°0722]
               • "Unverifiable code failed policy check. (Exception from HRESULT: 0x80131402)"
                    after having the SQLite library as single-file-deployment,
                    StackTrace tells coming from _task [see issue 20130706°1031 in Init.cs]
               • "The given assembly name or codebase was invalid. (Exception from HRESULT:
                    0x80131047) after trying workaround for issue 20130706°1031
               • "Unable to load DLL 'System.Data.SQLite.dll': The specified module could not
                    be found. (Exception from HRESULT: 0x8007007E)" while experimenting with
                    workaround for issue 20130706°1031
            Location : DbClient.cs method 20130604°0311 DoConnect
            Status : ?
            */

            /*
            Todo 20130720°1203 'Fix Postgres loading by referencing 'Mono.Security.dll'
            Matter : When introducing the PostgreSQL connection we see exception
                "Could not load file or assembly 'Mono.Security, Version=2.0.0.0,
                 Culture=neutral, PublicKeyToken=0738eb9f132ed756' or one of its dependencies.
                 Invalid pointer (Exception from HRESULT: 0x80004003 (E_POINTER))".
            Do : Fixing this by referencing 'Mono.Security.dll'. Plus provisory copying
                 Mono.Security.dll to QueryPonyGui\bin\x86\Debug. This must be settled
                 a la single-file-delivery, if the connection finally works.
            Location : DbClient.cs method 20130604°0311 DoConnect
            Status : Open. Possibly finished but must be tested
            */

            // Process error message
            // See todo 20130713°0932 'Make exception message appear on surface'
            ErrorMessage = e.Message;
            OnError(this, new ErrorEventQeArgs(e.Message, e));
         }

      }

      /// <summary>This property gets/sets the latest error message string for this DbClient</summary>
      /// <remarks>id : 20130604°0312</remarks>
      public string ErrorMessage
      {
         get { return _errorMessage; }
         set
         {
            _errorMessage = value;                                             // Breakpoint [line 20130821°1122]
         }
      }

      /// <summary>This virtual method cancels a running worker thread</summary>
      /// <remarks>id : 20130604°0313</remarks>
      protected virtual void DoCancel()
      {
         if (_runState == RunStates.Running)
         {
            _runState = RunStates.Cancelling;

            // We have to cancel on a new thread - separate to the main worker
            //  thread (because the worker thread will be busy) and separate to the
            //  main thread (as this is locked into the main UI apartment, and its
            //  use could cause subsequent corruption to an ODBC connection)
            Thread cancelThread = new Thread(new ThreadStart(_selectCommand.Cancel));
            cancelThread.Name = "DbClient Cancel Thread";
            cancelThread.Start();

            // Wait for the command to finish: this won't take long. However the
            //  main worker thread that is executing the actual command make take
            //  a while to register the cancel request and tidy up.
            cancelThread.Join();
         }
      }

      /// <summary>This virtual method fires the CancelDone event</summary>
      /// <remarks>id : 20130604°0314</remarks>
      protected virtual void InformCancelDone()
      {
         WaitForWorker();
         _runState = RunStates.Idle;
         if (CancelDone != null)
         {
            //if (host != null)
            //   host.Invoke(CancelDone, new object[] {this, EventArgs.Empty});
            //else
            CancelDone(this, EventArgs.Empty);
         }
      }

      /// <summary>This method executes a SQL statement possibly containing several batch jobs</summary>
      /// <remarks>id : 20130604°0315</remarks>
      private void DoExecuteBatchesAsync()
      {
         _execStartTime = DateTime.Now;
         SetupBatch();
         foreach (string sQuery in _queries)
         {
            IDataReader dr = null;
            try
            {
               _selectCommand = GetDbCommand(sQuery);
               _selectCommand.CommandTimeout = queryOptions.ExecutionTimeout;
               dr = _selectCommand.ExecuteReader();
               ReturnResults(dr); // Breakpoint [line 20130822.2112`xx] Debugging issue 20130822°2111 'DbClient triggers QueryForm methods'
               dr.Close();
            }
            catch (Exception ex)
            {
               string sDebug = ex.Message;
               ErrorEventQeArgs args = new ErrorEventQeArgs(ex.Message, ex);
               OnError(this, args);
               if ((ex as DbException) != null)
               {
                  // Do not cancel if conneciton is still open
                  if (_connection.State == ConnectionState.Open)
                  {
                     args.Cancel = false;
                  }
               }
               if (args.Cancel)
               {
                   break;
               }
            }
            finally
            {
               if (! (dr == null) && !dr.IsClosed)
               {
                  try { dr.Close(); }
                  catch (Exception) { }
               }
            }
         }
         ResetBatch();
         _execDuration = DateTime.Now.Subtract(_execStartTime);
         if (_runState == RunStates.Cancelling)
         {
            return;
         }
         _runState = RunStates.Idle;
         OnBatchDone(this, null);
      }

      /// <summary>This method executes a query synchronously and fills the DataSet</summary>
      /// <remarks>id : 20130604°0316</remarks>
      private void DoExecuteSync()
      {
         // Called indirectly by Execute()
         _selectCommand = GetDbCommand(_syncQuery);
         _selectCommand.CommandTimeout = _syncTimeOut;

         IDbDataAdapter da = GetDataAdapter(_selectCommand);
         try
         {
            da.Fill(_syncDataSet);
         }
         catch (Exception e)
         {
            // Exceptions seen e.g.
            // - 'Index not found.' (20130823°1232)
            ErrorMessage = e.Message;

            _syncDataSet = null;
         }
      }

      /// <summary>This virtual eventhandler processes the OnDataReaderAvailable event</summary>
      /// <remarks>id : 20130604°0317</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnDataReaderAvailable(object sender, DataReaderAvailableEventArgs e)
      {
         if (DataReaderAvailable != null) // Breakpoint [line 20130822.2112`xx] Debugging issue 20130822°2111 'DbClient triggers QueryForm methods'
         {
            DataReaderAvailable(this, e);
         }
      }

      /// <summary>This virtual eventhandler processes the OnTableSchemaAvailable event</summary>
      /// <remarks>id : 20130604°0318</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnTableSchemaAvailable(object sender, TableSchemaAvailableEventArgs e)
      {
         if (TableSchemaAvailable != null)
         {
            TableSchemaAvailable(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnDataRowAvailable event</summary>
      /// <remarks>id : 20130604°0319</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnDataRowAvailable(object sender, DataRowAvailableEventArgs e)
      {
         if (DataRowAvailable != null)
         {
            DataRowAvailable(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnInfoMessage event</summary>
      /// <remarks>id : 20130604°0320</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnInfoMessage(object sender, InfoMessageEventArgs e)
      {
         if (InfoMessage != null)
         {
            InfoMessage(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnError event</summary>
      /// <remarks>id : 20130604°0321</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnError(object sender, ErrorEventQeArgs e)
      {
         //----------------------------------------------------
         // Issue 20130716°0622
         // Error is null, even if exception was thrown, e.g. with malformed URL for MySQL.
         //  This is because we use the DbClient from the Connect for getting the databases.
         //  And the error handler is (so far) only installed in the QueryForm constructor.
         //  Means, the whole event mechanism seems only working in connection with a QueryForm.
         //----------------------------------------------------

         if (Error != null)
         {
            Error(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnCancelDone event</summary>
      /// <remarks>id : 20130604°0322</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnCancelDone(object sender, EventArgs e)
      {
         if (CancelDone != null)
         {
            CancelDone(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnCommandDone event</summary>
      /// <remarks>id : 20130604°0323</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnCommandDone(object sender, CommandDoneEventArgs e)
      {
         if (CommandDone != null)
         {
            CommandDone(sender, e);
         }
      }

      /// <summary>This eventhandler processes the OnBatchDone event</summary>
      /// <remarks>id : 20130604°0324</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected virtual void OnBatchDone(object sender, EventArgs e)
      {
         // Sequence simplified by VS 2017 'Delegate invocation can be simplified.' [log 20190410°0755]
         if (BatchDone != null)
         {
            BatchDone(sender, e);
         }
         //BatchDone?.Invoke(sender, e);                                       // After simplification [line 20190410°0756]
      }
      #endregion Protected Functions

      #region Public Properties

      /// <summary>This property gets the RunState of this DbClient</summary>
      /// <remarks>id : 20130604°0325</remarks>
      public RunStates RunState
      {
         get { return _runState; }
      }

      /// <summary>This property gets the connection settings for this DbClient</summary>
      /// <remarks>
      /// id : 20130604°0326
      /// todo : Having the identical identifyer for property type and property name is not nice. (todo 20130818°152102)
      /// </remarks>
      public ConnSettingsLib ConnSettings
      {
         get { return _connSettings; }
      }

      /// <summary>This property gets the QueryOptions for this DbClient</summary>
      /// <remarks>
      /// id : 20130604°0327
      /// todo : Having the identical identifyer for property type and property name is not nice. [todo 20130818°1521`03]
      /// </remarks>
      public QueryOptions QueryOptions
      {
         get { return queryOptions; }
      }

      /// <summary>This property gets/sets the database for this DbClient</summary>
      /// <remarks>id : 20130604°0328</remarks>
      public string Database
      {
         get { return _connection.Database; }
         set
         {
            if (_connection.Database != value)
            {
               _connection.ChangeDatabase(value);
            }
         }
      }

      /// <summary>This property gets the execution start time of an asyncronous batch run</summary>
      /// <remarks>id : 20130604°0329</remarks>
      public DateTime ExecStartTime
      {
         get { return _execStartTime; }
      }

      /// <summary>This property gets the execution duration of an asyncronous batch run</summary>
      /// <remarks>id : 20130604°0330</remarks>
      public TimeSpan ExecDuration
      {
         get { return _execDuration; }
      }

      /// <summary>This property gets/sets the dataset to which results are assigned (unless in Text mode)</summary>
      /// <remarks>id : 20130604°0331</remarks>
      public virtual DataSet DataSet { get; set; }

      #endregion Public Properties

      #region Public Functions

      /// <summary>This virtual method releases the SQL connection. This is called automatically from Dispose()</summary>
      /// <remarks>id : 20130604°0332</remarks>
      public virtual void Disconnect()
      {
         if (_runState == RunStates.Running) { Cancel(); }

         if (_connected)
         {
            RunOnWorker(new MethodInvoker(_connection.Close), true);
         }
      }

      /// <summary>This virtual method cancels any running queries and disconnects</summary>
      /// <remarks>id : 20130604°0333</remarks>
      public virtual void Dispose()
      {
         if (_connected)
         {
            Disconnect();
         }
         StopWorker();
      }

      /// <summary>This virtual method cancels a running query and informs us when it has done cancelling</summary>
      /// <remarks>id : 20130604°0334</remarks>
      public virtual void CancelAsync()
      {
         if (_runState == RunStates.Running)
         {
            DoCancel();

            // Start the thread that will inform us when the cancel has completed.
            //  This could take some time if a rollback is required.
            Thread informCancelDone = new Thread(new ThreadStart(InformCancelDone));
            informCancelDone.Name = "DbClient Inform Cancel";
            informCancelDone.Start();
         }
         else
         {
            CancelDone(this, EventArgs.Empty);
         }
      }

      /// <summary>
      /// This method cancels a running query synchronously (ie wait for it
      ///  to cancel). This method is called when closing an executing query.
      /// </summary>
      /// <remarks>id : 20130604°0335</remarks>
      public virtual void Cancel()
      {
         if (_runState == RunStates.Running)
         {
            DoCancel();
            WaitForWorker();
            _runState = RunStates.Idle;
         }
      }

      /// <summary>This method completes the connection for a newly created DbClient (yes?)</summary>
      /// <remarks>id : 20130604°0336</remarks>
      /// <returns>Success flag</returns>
      public bool Connect()
      {
         if (_connected)
         {
            return true;
         }

         // Even though we're connecting synchronously, we have to marshal the call
         //  onto the worker thread, otherwise the connection object will be locked
         //  into the main thread's apartment
         RunOnWorker(new MethodInvoker(DoConnect), true);

         if (_connected)
         {
            if (queryOptions != null)
            {
               ApplyQueryOptions();
            }
         }
         return _connected;
      }

      /// <summary>This method executes one SQL statement, possibly including several batched statements</summary>
      /// <remarks>id : 20130604°0337</remarks>
      /// <param name="sQuery">The query string to be executed</param>
      public void Execute(string sQuery)
      {
         // Note : If the 'GO' keyword is present, separate each subquery, so they can be run
         //  separately. Use Regex class, as we need a case insensitive match. [note 20130604°0337`02]

         string sSeparator = queryOptions == null ? "GO" : queryOptions.BatchSeparator;
         Regex r = new Regex(string.Format(@"^\s*{0}\s*$", sSeparator), RegexOptions.IgnoreCase | RegexOptions.Multiline);
         MatchCollection mc = r.Matches(sQuery);

         _queries = new ArrayList();
         int iPos = 0;
         foreach (Match m in mc)
         {
            string sub = sQuery.Substring(iPos, m.Index - iPos).Trim();
            if (sub.Length > 0)
            {
               _queries.Add(sub);
            }
            iPos = m.Index + m.Length + 1;
         }

         if (iPos < sQuery.Length)
         {
            string finalQuery = sQuery.Substring(iPos).Trim();
            if (finalQuery.Length > 0)
            {
               _queries.Add(finalQuery);
            }
         }

         _runState = RunStates.Running;

         RunOnWorker(new MethodInvoker(DoExecuteBatchesAsync));

         return;
      }

      /// <summary>This method executes the given SQL command synchronously</summary>
      /// <remarks>id : 20130604°0338</remarks>
      /// <param name="sQuery">The SQL command</param>
      /// <param name="iTimeout">The timeout in milliseconds</param>
      /// <returns>The wanted DataSet</returns>
      public DataSet ExecuteOnWorker(string sQuery, int iTimeout)
      {
         // Even though we just want to run a simple synchronous query, we have to marshal
         //  to the worker thread, since this is the thread that created the connection.
         _syncDataSet = new DataSet();
         this._syncQuery = sQuery;
         this._syncTimeOut = iTimeout;

         RunOnWorker(new MethodInvoker(DoExecuteSync), true);                  // true = synchronous

         if (_syncDataSet == null)
         {
            return null;
         }
         else
         {
            return _syncDataSet;
         }
      }

      /// <summary>This method clones this DbClient object</summary>
      /// <remarks>
      /// id : 20130604°0339
      /// note : What is this good for? Who uses it? Is it wanted at all?
      /// </remarks>
      /// <returns>The wanted cloned DbClient object.</returns>
      public DbClient Clone()
      {
         return DbClientFactory.GetDbClient(_connSettings.Clone());
      }

      /// <summary>This abstract function retrieves the default QueryOptions</summary>
      /// <remarks>id : 20130604°0340</remarks>
      /// <returns>The wanted QueryOptions</returns>
      public abstract QueryOptions GetDefaultOptions();

      #endregion Public Functions

      #region Private Functions

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°0341
      /// callers : DoExecuteBatchesAsync()
      /// </remarks>
      /// <param name="dr">The IDataReader ...</param>
      private void ReturnResults(IDataReader dr)
      {
         do
         {
            DataReaderAvailableEventArgs result;
            result = new DataReaderAvailableEventArgs(dr);                     // Breakpoint [line 20130822.2112`01] Debugging issue 20130822°2111 'DbClient triggers QueryForm methods'
            OnDataReaderAvailable(this, result);
            if (! result.SkipResults)
            {
               DataTable schema = dr.GetSchemaTable();                         // For list of column names & sizes}
               if (schema != null)
               {
                  OnTableSchemaAvailable(this, new TableSchemaAvailableEventArgs(schema));

                  // read through rows in result set
                  while (dr.Read())
                  {
                     if (_runState == RunStates.Cancelling) { return; }

                     object[] values = new object[dr.FieldCount];
                     dr.GetValues(values);
                     OnDataRowAvailable(this, new DataRowAvailableEventArgs(values));
                  }
               }
               OnCommandDone(this, new CommandDoneEventArgs(dr.RecordsAffected));
               if (! dr.NextResult())
               {
                  dr.Close();
               }
            }
            else
            {
               OnCommandDone(this, new CommandDoneEventArgs(dr.RecordsAffected));
            }

         } while (! dr.IsClosed);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0342</remarks>
      private void ResetBatch()
      {
         if (queryOptions != null)
         {
            try
            {
               queryOptions.ResetBatch(_connection);
            }
            catch
            {
            }
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0343</remarks>
      private void SetupBatch()
      {
         if ( queryOptions != null )
         {
            try
            {
               queryOptions.SetupBatch( _connection );
            }
            catch
            {
            }
         }
      }

      #endregion Private Functions
   }
}
