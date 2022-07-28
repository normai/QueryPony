#region Fileinfo
// file        : 20130604°0731 /QueryPony/QueryPonyLib/DbApi/MssqlQueryOptions.cs
// summary     : This file stores class 'MssqlQueryOptions' to definine MS-SQL-specific query options.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// changes     : File/class renamed from SqlQueryOptions.cs to MssqlQueryOptions.cs (20130606°1343)
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace QueryPonyLib
{
   /// <summary>
   /// This class definines MS-SQL-specific query options that can be
   ///  globally applied to commands and/or connections.
   /// </summary>
   /// <remarks>id : 20130604°0732</remarks>
   internal class MssqlQueryOptions : QueryOptions
   {
      #region Private Fields

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0733</remarks>
      private int _TextSize = 2147483647;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0734</remarks>
      private bool _NoCount;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0735</remarks>
      private bool _NoExec;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0736</remarks>
      private bool _ParseOnly;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0737</remarks>
      private bool _Concat_Null_Yields_Null = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0738</remarks>
      private bool _ArithAbort = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0739</remarks>
      private int _Lock_Timeout = -1;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0741</remarks>
      private int _Query_Governor_Cost_Limit = 0;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0742</remarks>
      private string _Deadlock_Priority = "NORMAL";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0743</remarks>
      private string _Transaction_Isolation_Level = "READ COMMITTED";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0744</remarks>
      private bool _Ansi_Nulls = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0745</remarks>
      private bool _Ansi_Warnings = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0746</remarks>
      private bool _Ansi_Padding = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0747</remarks>
      private bool _Cursor_Close_On_Commit;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0748</remarks>
      private bool _Quoted_Identifier = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0749</remarks>
      private bool _Implicit_Transactions;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0751</remarks>
      private bool _Ansi_Null_Dflt_On = true;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0752</remarks>
      private bool _ShowPlanText = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0753</remarks>
      private bool _StatisticsTime = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°0754</remarks>
      private bool _StatisticsIo = false;

      #endregion Private Fields

      #region Constructor

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°0755</remarks>
      public MssqlQueryOptions()
      {
      }

      #endregion Constructor

      #region Public Properties

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0756</remarks>
      public int TextSize
      {
         get { return _TextSize; }
         set { _TextSize = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0757</remarks>
      public bool NoCount
      {
         get { return _NoCount; }
         set { _NoCount = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0758</remarks>
      public bool NoExec
      {
         get { return _NoExec; }
         set { _NoExec = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0759</remarks>
      public bool ParseOnly
      {
         get { return _ParseOnly; }
         set { _ParseOnly = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0801</remarks>
      public bool Concat_Null_Yields_Null
      {
         get { return _Concat_Null_Yields_Null; }
         set { _Concat_Null_Yields_Null = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0802</remarks>
      public bool ArithAbort
      {
         get { return _ArithAbort; }
         set { _ArithAbort = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0803</remarks>
      public int Lock_Timeout
      {
         get { return _Lock_Timeout; }
         set { _Lock_Timeout = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0804</remarks>
      public int Query_Governor_Cost_Limit
      {
         get { return _Query_Governor_Cost_Limit; }
         set { _Query_Governor_Cost_Limit = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0805</remarks>
      public string Deadlock_Priority
      {
         get { return _Deadlock_Priority; }
         set { _Deadlock_Priority = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0806</remarks>
      public string Transaction_Isolation_Level
      {
         get { return _Transaction_Isolation_Level; }
         set { _Transaction_Isolation_Level = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0807</remarks>
      public bool Ansi_Nulls
      {
         get { return _Ansi_Nulls; }
         set { _Ansi_Nulls = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0808</remarks>
      public bool Ansi_Warnings
      {
         get { return _Ansi_Warnings; }
         set { _Ansi_Warnings = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0809</remarks>
      public bool Ansi_Padding
      {
         get { return _Ansi_Padding; }
         set { _Ansi_Padding = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0811</remarks>
      public bool Cursor_Close_On_Commit
      {
         get { return _Cursor_Close_On_Commit; }
         set { _Cursor_Close_On_Commit = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0812</remarks>
      public bool Quoted_Identifier
      {
         get { return _Quoted_Identifier; }
         set { _Quoted_Identifier = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0813</remarks>
      public bool Implicit_Transactions
      {
         get { return _Implicit_Transactions; }
         set { _Implicit_Transactions = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0814</remarks>
      public bool Ansi_Null_Dflt_On
      {
         get { return _Ansi_Null_Dflt_On; }
         set { _Ansi_Null_Dflt_On = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0815</remarks>
      public bool ShowPlanText
      {
         get { return _ShowPlanText; }
         set { _ShowPlanText = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0816</remarks>
      public bool StatisticsTime
      {
         get { return _StatisticsTime; }
         set { _StatisticsTime = value; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°0817</remarks>
      public bool StatisticsIo
      {
         get { return _StatisticsIo; }
         set { _StatisticsIo = value; }
      }

      #endregion Public Properties

      #region Interface Definition

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0818</remarks>
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

         SqlCommand cmd = new SqlCommand(sb.ToString(), (SqlConnection)connection);
         cmd.ExecuteNonQuery();
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0819</remarks>
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
            SqlCommand cmd = new SqlCommand(sb.ToString(), (SqlConnection)connection);
            cmd.ExecuteNonQuery();
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0821</remarks>
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
            SqlCommand cmd = new SqlCommand(sb.ToString(), (SqlConnection)connection);
            cmd.ExecuteNonQuery();
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°0822
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override System.Windows.Forms.DialogResult ShowForm()
      {
         MysqlQueryOptionsForm_DUMMY f = new MysqlQueryOptionsForm_DUMMY();
         FieldsToForm(f);

         System.Windows.Forms.DialogResult res = ShowForm(f);
         if (res == System.Windows.Forms.DialogResult.OK)
         {
            FormToFields(f);
         }
         return res;
      }

      /// <summary>This method sets this QueryOptions object with the values from MysqlQueryOptionsForm_DUMMY</summary>
      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°0823
      /// note : This method is involved in refactoring 20130619°1311
      /// note : About compiler warnings with method name, compare note 20130623°0931.
      /// </remarks>
      /// <param name="f">...</param>
      private void FormToFields_Mssql(MysqlQueryOptionsForm_DUMMY f)
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

      /// <summary>This method fills in the MysqlQueryOptionsForm_DUMMY from from the QueryOptions object</summary>
      /// <remarks>
      /// id : 20130604°0824
      /// note : This method is involved in refactor 20130619°1311
      /// note : About compiler warnings with method name, compare note 20130623°0931.
      /// </remarks>
      /// <param name="f">...</param>
      private void FieldsToForm_Mssql(MysqlQueryOptionsForm_DUMMY f)
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

      /// <summary>This method implements ... (experimental)</summary>
      /// <remarks>id : 20130705°1023</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102302)"
                      + Glb.sCrCr + "MS-SQL shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }
   }
}
