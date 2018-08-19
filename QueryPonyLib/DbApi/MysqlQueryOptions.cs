#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DatabaseApi/MysqlQueryOptions.cs
// id          : 20130612°0931 (20130604°0731)
// summary     : This file stores class 'MysqlQueryOptoins' to define the MySQL-specific query options.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from MssqlQueryOptions.cs and modified (20130612°0931)
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QueryPonyLib
{

   /// <summary>This class serves as a helper class while refactoring.</summary>
   /// <remarks>id : 20130619°1458</remarks>
   public class QueryOptionsForm_DUMMY : System.Windows.Forms.Form
   {
   }


   /// <summary>
   /// This class defines the MySQL-specific query options that can
   ///  be globally applied to commands and/or connections.
   /// </summary>
   /// <remarks>id : 20130612°0932 (20130604°0732)</remarks>
   internal class MysqlQueryOptions : QueryOptions
   {

      #region Private Fields

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0933 (20130604°0733)</remarks>
      private int _TextSize = 2147483647;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0934 (20130604°0734)</remarks>
      private bool _NoCount;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0935 (20130604°0735)</remarks>
      private bool _NoExec;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0936 (20130604°0736)</remarks>
      private bool _ParseOnly;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0937 (20130604°0737)</remarks>
      private bool _Concat_Null_Yields_Null = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0938 (20130604°0738)</remarks>
      private bool _ArithAbort = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0939 (20130604°0739)</remarks>
      private int _Lock_Timeout = -1;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0941 (20130604°0741)</remarks>
      private int _Query_Governor_Cost_Limit = 0;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0942 (20130604°0742)</remarks>
      private string _Deadlock_Priority = "NORMAL";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0943 (20130604°0743)</remarks>
      private string _Transaction_Isolation_Level = "READ COMMITTED";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0944 (20130604°0744)</remarks>
      private bool _Ansi_Nulls = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0945 (20130604°0745)</remarks>
      private bool _Ansi_Warnings = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0946 (20130604°0746)</remarks>
      private bool _Ansi_Padding = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0947 (20130604°0747)</remarks>
      private bool _Cursor_Close_On_Commit;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0948 (20130604°0748)</remarks>
      private bool _Quoted_Identifier = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0949 (20130604°0749)</remarks>
      private bool _Implicit_Transactions;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0951 (20130604°0751)</remarks>
      private bool _Ansi_Null_Dflt_On = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0952 (20130604°0752)</remarks>
      private bool _ShowPlanText = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0953 (20130604°0753)</remarks>
      private bool _StatisticsTime = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130612°0954 (20130604°0754)</remarks>
      private bool _StatisticsIo = false;

      #endregion Private Fields

      #region Constructor

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130612°0955 (20130604°0755)</remarks>
      public MysqlQueryOptions()
      {
      }

      #endregion Constructor

      #region Public Properties

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°0956 (20130604°0756)</remarks>
      public int TextSize
      {
         get { return _TextSize; }
         set { _TextSize = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°0957 (20130604°0757)</remarks>
      public bool NoCount
      {
         get { return _NoCount; }
         set { _NoCount = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°0958 (20130604°0758)</remarks>
      public bool NoExec
      {
         get { return _NoExec; }
         set { _NoExec = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°0959 (20130604°0759)</remarks>
      public bool ParseOnly
      {
         get { return _ParseOnly; }
         set { _ParseOnly = value; }
      }

      /// <summary>This property ...</summary>
      /// <remarks>id : 20130612°1001 (20130604°0801)</remarks>
      public bool Concat_Null_Yields_Null
      {
         get { return _Concat_Null_Yields_Null; }
         set { _Concat_Null_Yields_Null = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1002 (20130604°0802)</remarks>
      public bool ArithAbort
      {
         get { return _ArithAbort; }
         set { _ArithAbort = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1003 (20130604°0803)</remarks>
      public int Lock_Timeout
      {
         get { return _Lock_Timeout; }
         set { _Lock_Timeout = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1004 (20130604°0804)</remarks>
      public int Query_Governor_Cost_Limit
      {
         get { return _Query_Governor_Cost_Limit; }
         set { _Query_Governor_Cost_Limit = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1005 (20130604°0805)</remarks>
      public string Deadlock_Priority
      {
         get { return _Deadlock_Priority; }
         set { _Deadlock_Priority = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1006 (20130604°0806)</remarks>
      public string Transaction_Isolation_Level
      {
         get { return _Transaction_Isolation_Level; }
         set { _Transaction_Isolation_Level = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1007 (20130604°0807)</remarks>
      public bool Ansi_Nulls
      {
         get { return _Ansi_Nulls; }
         set { _Ansi_Nulls = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1008 (20130604°0808)</remarks>
      public bool Ansi_Warnings
      {
         get { return _Ansi_Warnings; }
         set { _Ansi_Warnings = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1009 (20130604°0809)</remarks>
      public bool Ansi_Padding
      {
         get { return _Ansi_Padding; }
         set { _Ansi_Padding = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1011 (20130604°0811)</remarks>
      public bool Cursor_Close_On_Commit
      {
         get { return _Cursor_Close_On_Commit; }
         set { _Cursor_Close_On_Commit = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1012 (20130604°0812)</remarks>
      public bool Quoted_Identifier
      {
         get { return _Quoted_Identifier; }
         set { _Quoted_Identifier = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1013 (20130604°0813)</remarks>
      public bool Implicit_Transactions
      {
         get { return _Implicit_Transactions; }
         set { _Implicit_Transactions = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1014 (20130604°0814)</remarks>
      public bool Ansi_Null_Dflt_On
      {
         get { return _Ansi_Null_Dflt_On; }
         set { _Ansi_Null_Dflt_On = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1015 (20130604°0815)</remarks>
      public bool ShowPlanText
      {
         get { return _ShowPlanText; }
         set { _ShowPlanText = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1016 (20130604°0816)</remarks>
      public bool StatisticsTime
      {
         get { return _StatisticsTime; }
         set { _StatisticsTime = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130612°1017 (20130604°0817)</remarks>
      public bool StatisticsIo
      {
         get { return _StatisticsIo; }
         set { _StatisticsIo = value; }
      }

      #endregion Public Properties

      #region Interface Definition

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130612°1018 (20130604°0818)</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(string.Format(" SET ROWCOUNT {0}", RowCount));
         sb.Append(string.Format(" SET TEXTSIZE {0}", _TextSize));
         sb.Append(string.Format(" SET NOCOUNT {0}", _NoCount ? "ON" : "OFF"));
         sb.Append(string.Format(" SET CONCAT_NULL_YIELDS_NULL {0}", _Concat_Null_Yields_Null ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ARITHABORT {0}", _ArithAbort ? "ON" : "OFF"));
         sb.Append(string.Format(" SET LOCK_TIMEOUT {0}", _Lock_Timeout));
         sb.Append(string.Format(" SET QUERY_GOVERNOR_COST_LIMIT {0}", _Query_Governor_Cost_Limit));
         sb.Append(string.Format(" SET DEADLOCK_PRIORITY {0}", _Deadlock_Priority));
         sb.Append(string.Format(" SET TRANSACTION ISOLATION LEVEL {0}", _Transaction_Isolation_Level));
         sb.Append(string.Format(" SET ANSI_NULLS {0}", _Ansi_Nulls ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_NULL_DFLT_ON {0}", _Ansi_Null_Dflt_On ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_PADDING {0}", _Ansi_Padding ? "ON" : "OFF"));
         sb.Append(string.Format(" SET ANSI_WARNINGS {0}", _Ansi_Warnings ? "ON" : "OFF"));
         sb.Append(string.Format(" SET CURSOR_CLOSE_ON_COMMIT {0}", _Cursor_Close_On_Commit ? "ON" : "OFF"));
         sb.Append(string.Format(" SET IMPLICIT_TRANSACTIONS {0}", _Implicit_Transactions ? "ON" : "OFF"));
         sb.Append(string.Format(" SET QUOTED_IDENTIFIER {0}", _Quoted_Identifier ? "ON" : "OFF"));

#if MYSQL20130619YES

         MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand
                                                  ( sb.ToString()
                                                   , (MySql.Data.MySqlClient.MySqlConnection) connection
                                                    );
         cmd.ExecuteNonQuery();

#else
         return;
#endif

      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130612°1019 (20130604°0819)</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         StringBuilder sb = new StringBuilder();
         if (_NoExec)          { sb.Append(" SET NOEXEC ON"); }
         if (_ParseOnly)       { sb.Append(" SET PARSEONLY ON"); }
         if (_StatisticsIo)    { sb.Append(" SET STATISTICS IO ON"); }
         if (_StatisticsTime)  { sb.Append(" SET STATISTICS TIME ON"); }
         if (_ShowPlanText)    { sb.Append(" SET SHOWPLAN_TEXT ON"); }

         if (sb.Length > 0)
         {

#if MYSQL20130619YES

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand
                                                     ( sb.ToString()
                                                      , (MySql.Data.MySqlClient.MySqlConnection) connection
                                                       );

            cmd.ExecuteNonQuery();
#else
#endif

         }
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130612°1021 (20130604°0821)</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         StringBuilder sb = new StringBuilder();
         if (_NoExec)          { sb.Append(" SET NOEXEC OFF"); }
         if (_ParseOnly)       { sb.Append(" SET PARSEONLY OFF"); }
         if (_StatisticsIo)    { sb.Append(" SET STATISTICS IO OFF"); }
         if (_StatisticsTime)  { sb.Append(" SET STATISTICS TIME OFF"); }
         if (_ShowPlanText)    { sb.Append(" SET SHOWPLAN_TEXT OFF"); }

         if (sb.Length>0)
         {

#if MYSQL20130619YES

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand
                                                     ( sb.ToString()
                                                      , (MySql.Data.MySqlClient.MySqlConnection)connection
                                                       );

            cmd.ExecuteNonQuery();

#else
#endif

         }
      }


      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130612°1022 (20130604°0822)
      /// note : This method is involved in refactor 20130619°1311
      /// note : Compare issue 20130623°0933.
      /// </remarks>
      /// <returns>...</returns>
      public override System.Windows.Forms.DialogResult ShowForm()
      {
         ////SqlQueryOptionsForm f = new SqlQueryOptionsForm(); // original line
         MysqlQueryOptionsForm_DUMMY f = new MysqlQueryOptionsForm_DUMMY();
         FieldsToForm_Mysql(f);  //// debug 20130705°093103

         System.Windows.Forms.DialogResult res = ShowForm(f);
         if (res == System.Windows.Forms.DialogResult.OK)
         {
            FormToFields_Mysql(f);
         }
         return res;
      }


      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130612°1023 (20130604°0823)
      /// note : This method is involved in refactor 20130619°1311. Good luck it
      ///    seems not (yet) used at all, so we can temporarily shutdown it with impunity.
      /// note : When method name was 'FormToFields' we (sometimes) receive
      ///        compiler warning "'QueryPonyLib.MssqlQueryOptions.FormToFields(QueryPonyLib.MssqlQueryOptionsForm_DUMMY)'
      ///        hides inherited member 'QueryPonyLib.QueryOptions.FormToFields(QueryPonyLib.MssqlQueryOptionsForm_DUMMY)'.
      ///        Use the new keyword if hiding was intended."
      ///        The warning was gone after renaming the method. [20130623°0931]
      /// </remarks>
      /// <param name="f">...</param>
      ////private void FormToFields(SqlQueryOptionsForm f)
      ///private void FormToFields(MssqlQueryOptionsForm_DUMMY f)
      private void FormToFields_Mysql(MysqlQueryOptionsForm_DUMMY f) // rename
      {
         this.TextSize = (int)(f.txtTextSize.Value);

         this.NoCount = f.chkNoCount.Checked;
         this.NoExec = f.chkNoExec.Checked;
         this.ParseOnly = f.chkParseOnly.Checked;
         this.Concat_Null_Yields_Null = f.chkConcatNullYieldsNull.Checked;
         this.ArithAbort = f.chkArithAbort.Checked;
         this.ShowPlanText = f.chkShowPlanText.Checked;
         this.StatisticsTime = f.chkStatisticsTime.Checked;
         this.StatisticsIo = f.chkStatisticsIo.Checked;

         this.Transaction_Isolation_Level = f.cboTransactionIsolation.Text;
         this.Deadlock_Priority = f.cboDeadlockPriority.Text;
         this.Lock_Timeout = (int)f.txtLockTimeout.Value;
         this.Query_Governor_Cost_Limit = (int)f.txtQueryGovernorCostLimit.Value;

         this.Quoted_Identifier = f.chkQuotedIdentifier.Checked;
         this.Ansi_Null_Dflt_On = f.chkAnsiNullDflt.Checked;
         this.Implicit_Transactions = f.chkImplicitTransactions.Checked;
         this.Cursor_Close_On_Commit = f.chkCursorCloseOnCommit.Checked;
         this.Ansi_Padding = f.chkAnsiPadding.Checked;
         this.Ansi_Warnings = f.chkAnsiWarnings.Checked;
         this.Ansi_Nulls = f.chkAnsiNulls.Checked;
      }


      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130612°1024 (20130604°0824)
      /// note : This method is involved in refactor 20130619°1311
      /// note : About compiler warnings with method name, compare note 20130623°0931.
      /// </remarks>
      /// <param name="f">...</param>
      ////private void FieldsToForm(SqlQueryOptionsForm f)
      ///private void FieldsToForm(MssqlQueryOptionsForm_DUMMY f)
      private void FieldsToForm_Mysql(MysqlQueryOptionsForm_DUMMY f)
      {
         f.txtTextSize.Value = this.TextSize;

         f.chkNoCount.Checked = this.NoCount;
         f.chkNoExec.Checked = this.NoExec;
         f.chkParseOnly.Checked = this.ParseOnly;
         f.chkConcatNullYieldsNull.Checked = this.Concat_Null_Yields_Null;
         f.chkArithAbort.Checked = this.ArithAbort;
         f.chkShowPlanText.Checked = this.ShowPlanText;
         f.chkStatisticsTime.Checked = this.StatisticsTime;
         f.chkStatisticsIo.Checked = this.StatisticsIo;

         f.cboTransactionIsolation.Text = this.Transaction_Isolation_Level;
         f.cboDeadlockPriority.Text = this.Deadlock_Priority;
         f.txtLockTimeout.Value = this.Lock_Timeout;
         f.txtQueryGovernorCostLimit.Value = this.Query_Governor_Cost_Limit;

         f.chkQuotedIdentifier.Checked = this.Quoted_Identifier;
         f.chkAnsiNullDflt.Checked = this.Ansi_Null_Dflt_On;
         f.chkImplicitTransactions.Checked = this.Implicit_Transactions;
         f.chkCursorCloseOnCommit.Checked = this.Cursor_Close_On_Commit;
         f.chkAnsiPadding.Checked = this.Ansi_Padding;
         f.chkAnsiWarnings.Checked = this.Ansi_Warnings;
         f.chkAnsiNulls.Checked = this.Ansi_Nulls;
      }

      #endregion Interface Definition


      /// <summary>This method implements ... (experimental).</summary>
      /// <remarks>id : 20130705°1024</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102402)"
                      + Glb.sCrCr + "MySQL shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }

   }


   /// <summary>This class serves as (dummy) mediator between the library and the GUI.</summary>
   /// <remarks>
   /// id : 20130619°1331
   /// note : This class was introduced as a helper refactor 20130619°1311, so we
   ///    need not destroy existing structures, but just calm them down.
   ///    As well, when resuming the QueryOptions feature somewhen, this class may
   ///    serve as the wanted mediator between the library and the GUI.
   /// </remarks>
   ////internal class MssqlQueryOptionsForm_DUMMY
   ////protected class MssqlQueryOptionsForm_DUMMY
   ////public class MssqlQueryOptionsForm_DUMMY : QueryOptionsForm_DUMMY
   public class MysqlQueryOptionsForm_DUMMY : QueryOptionsForm_DUMMY //// QueryOptions //// debug 20130705°093104
   {

      /// <summary>This subclass ... serves as a helper (during refactor 20130619°1311).</summary>
      /// <remarks>id : 20130619°1411</remarks>
      public class txtBatchSeparatorHelper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°141102</remarks>
         public string Text;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1412</remarks>
      public txtBatchSeparatorHelper txtBatchSeparator = new txtBatchSeparatorHelper();


      /// <summary>This subclass ... serves as a helper (during refactor 20130619°1311).</summary>
      /// <remarks>id : 20130619°1413</remarks>
      public class txtExecutionTimeoutHelper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°141302</remarks>
         public int Value;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1414</remarks>
      public txtExecutionTimeoutHelper txtExecutionTimeout = new txtExecutionTimeoutHelper();


      /// <summary>This subclass ... serves as a helper (during refactor 20130619°1311).</summary>
      /// <remarks>id : 20130619°1415</remarks>
      public class txtRowCountHelper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°141502</remarks>
         public int Value;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1416</remarks>
      public txtRowCountHelper txtRowcount = new txtRowCountHelper();


      //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      // the easy sequence - no, not really ... (archived sequence 20130619°1409)
      /*
      public int txtTextSize;                          // .Value;              // = this.TextSize;

      public bool chkNoCount;                          // .Checked;            // = this.NoCount;
      public bool chkNoExec;                           // .Checked;            // = this.NoExec;
      public bool chkParseOnly;                        // .Checked;            // = this.ParseOnly;
      public bool chkConcatNullYieldsNull;             // .Checked;            // = this.Concat_Null_Yields_Null;
      public bool chkArithAbort;                       // .Checked;            // = this.ArithAbort;
      public bool chkShowPlanText;                     // .Checked;            // = this.ShowPlanText;
      public bool chkStatisticsTime;                   // .Checked;            // = this.StatisticsTime;
      public bool chkStatisticsIo;                     // .Checked;            // = this.StatisticsIo;

      public string cboTransactionIsolation;           // .Text;               // = this.Transaction_Isolation_Level;
      public string cboDeadlockPriority;               // .Text;               // = this.Deadlock_Priority;
      public int txtLockTimeout;                       // .Value;              // = this.Lock_Timeout;
      public int txtQueryGovernorCostLimit;            // .Value;              // = this.Query_Governor_Cost_Limit;

      public bool chkQuotedIdentifier;                 // .Checked;            // = this.Quoted_Identifier;
      public bool chkAnsiNullDflt;                     // .Checked;            // = this.Ansi_Null_Dflt_On;
      public bool chkImplicitTransactions;             // .Checked;            // = this.Implicit_Transactions;
      public bool chkCursorCloseOnCommit;              // .Checked;            // = this.Cursor_Close_On_Commit;
      public bool chkAnsiPadding;                      // .Checked;            // = this.Ansi_Padding;
      public bool chkAnsiWarnings;                     // .Checked;            // = this.Ansi_Warnings;
      public bool chkAnsiNulls;                        // .Checked;            // = this.Ansi_Nulls;
      */
      //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1417</remarks>
      public class txtTextSize_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°1417 (20130705°1001)</remarks>
         public txtTextSize_Helper()
         {
            this.Value = 123;  //// (debug 20130705°093113)
         }

         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°141703 (20130705°1002)</remarks>
         public int Value;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1418</remarks>
      public txtTextSize_Helper txtTextSize = new txtTextSize_Helper(); //// (debug 20130705°093115)


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1419</remarks>
      public class chkNoCount_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°141902</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1420</remarks>
      public chkNoCount_Helper chkNoCount = new chkNoCount_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1421</remarks>
      public class chkNoExec_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°142102</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1422</remarks>
      public chkNoExec_Helper chkNoExec = new chkNoExec_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1423</remarks>
      public class chkParseOnly_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°142302</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1424</remarks>
      public chkParseOnly_Helper chkParseOnly = new chkParseOnly_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1425</remarks>
      public class chkConcatNullYieldsNull_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°142502</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1426</remarks>
      public chkConcatNullYieldsNull_Helper chkConcatNullYieldsNull = new chkConcatNullYieldsNull_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1427</remarks>
      public class chkArithAbort_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°142702</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1428</remarks>
      public chkArithAbort_Helper chkArithAbort = new chkArithAbort_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1429</remarks>
      public class chkShowPlanText_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°142902</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1430</remarks>
      public chkShowPlanText_Helper chkShowPlanText = new chkShowPlanText_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1431</remarks>
      public class chkStatisticsTime_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°143102</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1432</remarks>
      public chkStatisticsTime_Helper chkStatisticsTime = new chkStatisticsTime_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1433</remarks>
      public class chkStatisticsIo_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°143302</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1434</remarks>
      public chkStatisticsIo_Helper chkStatisticsIo = new chkStatisticsIo_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1435</remarks>
      public class cboTransactionIsolation_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°143502</remarks>
         public string Text;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1436</remarks>
      public cboTransactionIsolation_Helper cboTransactionIsolation = new cboTransactionIsolation_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1437</remarks>
      public class cboDeadlockPriority_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°143702</remarks>
         public string Text;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1438</remarks>
      public cboDeadlockPriority_Helper cboDeadlockPriority = new cboDeadlockPriority_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1439</remarks>
      public class txtLockTimeout_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°143902</remarks>
         public int Value;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1440</remarks>
      public txtLockTimeout_Helper txtLockTimeout = new txtLockTimeout_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1441</remarks>
      public class txtQueryGovernorCostLimit_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°144102</remarks>
         public int Value;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1442</remarks>
      public txtQueryGovernorCostLimit_Helper txtQueryGovernorCostLimit = new txtQueryGovernorCostLimit_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1443</remarks>
      public class chkQuotedIdentifier_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°144302</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1444</remarks>
      public chkQuotedIdentifier_Helper chkQuotedIdentifier = new chkQuotedIdentifier_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1445</remarks>
      public class chkAnsiNullDflt_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°144502</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1446</remarks>
      public chkAnsiNullDflt_Helper chkAnsiNullDflt = new chkAnsiNullDflt_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1447</remarks>
      public class chkImplicitTransactions_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°144702</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1448</remarks>
      public chkImplicitTransactions_Helper chkImplicitTransactions = new chkImplicitTransactions_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1449</remarks>
      public class chkCursorCloseOnCommit_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°144902</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1450</remarks>
      public chkCursorCloseOnCommit_Helper chkCursorCloseOnCommit = new chkCursorCloseOnCommit_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1451</remarks>
      public class chkAnsiPadding_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°145102</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1452</remarks>
      public chkAnsiPadding_Helper chkAnsiPadding = new chkAnsiPadding_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1453</remarks>
      public class chkAnsiWarnings_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°145302</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1454</remarks>
      public chkAnsiWarnings_Helper chkAnsiWarnings = new chkAnsiWarnings_Helper();


      /// <summary>This subclass ... serves as a helper (while refactoring).</summary>
      /// <remarks>id : 20130619°1455</remarks>
      public class chkAnsiNulls_Helper
      {
         /// <summary>This field ...</summary>
         /// <remarks>id : 20130619°145502</remarks>
         public bool Checked;
      }


      /// <summary>This field ... (serves as dummy while refactoring).</summary>
      /// <remarks>id : 20130619°1456</remarks>
      public chkAnsiNulls_Helper chkAnsiNulls = new chkAnsiNulls_Helper();


      /// <summary>This method ... serves as a helper (while refactoring).</summary>
      /// <remarks>
      /// id : 20130619°1457
      /// note : When method name was 'ShowDialog', sometimes appeared compiler warning
      ///        "'QueryPonyLib.MssqlQueryOptionsForm_DUMMY.ShowDialog()' hides inherited member
      ///        'System.Windows.Forms.Form.ShowDialog()'. Use the new keyword if hiding was intended.".
      ///        The warning was gone after renaming the method. [note 20130623°0932]
      /// issue : Calling a WinForms dialogbox should never be done in the library. Possibly
      ///         replace this by a delegate passed from the client to the library on init.
      ///         [issue 20130623°0933]
      /// </remarks>
      ////DialogResult res = optionsForm.ShowDialog();
      ////public DialogResult ShowDialog()
      public System.Windows.Forms.DialogResult ShowDialog_Mysql()
      {
         System.Windows.Forms.DialogResult dr = new System.Windows.Forms.DialogResult();
         return dr;
      }

   }

}
