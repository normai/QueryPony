#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Gui/QueryOptionsFormMysql.cs
// id          : 20130705°0911 (20130604°1331)
// summary     : This file stores class 'QueryOptionsFormMysql' to constitute the MySQL Query Options Dialog Form.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This class constitutes the MySQL Query Options Dialog Form.</summary>
   /// <remarks>id : 20130705°0912 (20130604°1332)</remarks>
   internal partial class QueryOptionsFormMysql : QueryOptionsForm
   {

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130705°0913 (20130604°1333)</remarks>
      public QueryOptionsFormMysql()
      {
         InitializeComponent();
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0914 (20130604°1334)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void cmdResetToDefault_Advanced_Click(object sender, EventArgs e)
      {
         chkNoCount.Checked = false;
         chkNoExec.Checked = false;
         chkParseOnly.Checked = false;
         chkConcatNullYieldsNull.Checked = true;
         chkArithAbort.Checked = true;
         chkStatisticsTime.Checked = false;
         chkStatisticsIo.Checked = false;
         cboTransactionIsolation.Text = "READ COMMITTED";
         cboDeadlockPriority.Text = "Normal";
         txtLockTimeout.Value = -1;
         txtQueryGovernorCostLimit.Value = 0;
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0915 (20130604°1335)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void chkParseOnly_CheckedChanged(object sender, EventArgs e)
      {
         if (chkParseOnly.Checked)
         {
            chkNoCount.Checked = false;
            chkNoCount.Enabled = false;
            chkNoExec.Checked = false;
            chkNoExec.Enabled = false;
            chkArithAbort.Checked = false;
            chkArithAbort.Enabled = false;
            chkShowPlanText.Checked = false;
            chkShowPlanText.Enabled = false;
            chkStatisticsIo.Checked = false;
            chkStatisticsIo.Enabled = false;
            chkStatisticsTime.Checked = false;
            chkStatisticsTime.Enabled = false;
         }
         else
         {
            chkNoCount.Enabled = true;
            chkNoExec.Enabled = true;
            chkArithAbort.Enabled = true;
            chkShowPlanText.Enabled = true;
            chkStatisticsIo.Enabled = true;
            chkStatisticsTime.Enabled = true;
         }

      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0916 (20130604°1336)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void chkNoExec_CheckedChanged(object sender, EventArgs e)
      {
         if (chkNoExec.Checked)
         {
            chkNoCount.Checked = false;
            chkNoCount.Enabled = false;
            chkParseOnly.Checked = false;
            chkParseOnly.Enabled = false;
            chkArithAbort.Checked = false;
            chkArithAbort.Enabled = false;
            chkShowPlanText.Checked = false;
            chkShowPlanText.Enabled = false;
            chkStatisticsIo.Checked = false;
            chkStatisticsIo.Enabled = false;
            chkStatisticsTime.Checked = false;
            chkStatisticsTime.Enabled = false;
         }
         else
         {
            chkNoCount.Enabled = true;
            chkParseOnly.Enabled = true;
            chkArithAbort.Enabled = true;
            chkShowPlanText.Enabled = true;
            chkStatisticsIo.Enabled = true;
            chkStatisticsTime.Enabled = true;
         }
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0917 (20130604°1337)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void chkShowPlanText_CheckedChanged(object sender, EventArgs e)
      {
         if (chkShowPlanText.Checked)
         {
            chkNoCount.Checked = false;
            chkNoCount.Enabled = false;
            chkNoExec.Checked = false;
            chkNoExec.Enabled = false;
            chkParseOnly.Checked = false;
            chkParseOnly.Enabled = false;
            chkArithAbort.Checked = false;
            chkArithAbort.Enabled = false;
            chkStatisticsIo.Checked = false;
            chkStatisticsIo.Enabled = false;
            chkStatisticsTime.Checked = false;
            chkStatisticsTime.Enabled = false;
         }
         else
         {
            chkNoCount.Enabled = true;
            chkNoExec.Enabled = true;
            chkParseOnly.Enabled = true;
            chkArithAbort.Enabled = true;
            chkStatisticsIo.Enabled = true;
            chkStatisticsTime.Enabled = true;
         }
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0918 (20130604°1338)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void ChkANSI_CheckedChanged(object sender, EventArgs e)
      {
         if ((chkQuotedIdentifier.Checked == chkAnsiNullDflt.Checked) &&
            (chkQuotedIdentifier.Checked == chkImplicitTransactions.Checked) &&
            (chkQuotedIdentifier.Checked == chkCursorCloseOnCommit.Checked) &&
            (chkQuotedIdentifier.Checked == chkAnsiPadding.Checked) &&
            (chkQuotedIdentifier.Checked == chkAnsiWarnings.Checked) &&
            (chkQuotedIdentifier.Checked == chkAnsiNulls.Checked))
         {
            if (((CheckBox)sender).Checked)
            {
               chkAnsiDefaults.CheckState = CheckState.Checked;
            }
            else
            {
               chkAnsiDefaults.CheckState = CheckState.Unchecked;
            }
         }
         else
         {
            if (chkAnsiDefaults.CheckState != CheckState.Indeterminate)
            {
               chkAnsiDefaults.CheckState = CheckState.Indeterminate;
            }
         }
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0919 (20130604°1339)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void chkAnsiDefaults_CheckStateChanged(object sender, EventArgs e)
      {
         if (   chkAnsiDefaults.CheckState == CheckState.Checked
             || chkAnsiDefaults.CheckState == CheckState.Unchecked
              )
         {
            bool chk = chkAnsiDefaults.CheckState == CheckState.Checked;
            chkQuotedIdentifier.Checked = chk;
            chkAnsiNullDflt.Checked = chk;
            chkImplicitTransactions.Checked = chk;
            chkCursorCloseOnCommit.Checked = chk;
            chkAnsiPadding.Checked = chk;
            chkAnsiWarnings.Checked = chk;
            chkAnsiNulls.Checked = chk;
         }
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130705°0921 (20130604°1341)</remarks>
      /// <param name="e">The object which sent the event</param>
      /// <param name="sender">The event object itself</param>
      private void cmdResetToDefault_ANSI_Click(object sender, EventArgs e)
      {
         chkQuotedIdentifier.Checked = true;
         chkAnsiNullDflt.Checked = true;
         chkImplicitTransactions.Checked = false;
         chkCursorCloseOnCommit.Checked = false;
         chkAnsiPadding.Checked = true;
         chkAnsiWarnings.Checked = true;
         chkAnsiNulls.Checked = true;
      }

   }
}
