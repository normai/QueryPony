#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Gui/QueryOptionsForm.cs
// id          : 20130604°2241
// summary     : This file stores class 'QueryOptionsForm' to constitute the QueryOptions Dialog Form.
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

   /// <summary>This class constitutes the QueryOptions Dialog Form.</summary>
   /// <remarks>
   /// id : 20130604°2242
   /// note : Access modifier set 'public' to make it accessible from other projects (20130604°1426)
   /// </remarks>
   public partial class QueryOptionsForm : Form
   {

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°2243</remarks>
      public QueryOptionsForm()
      {
         InitializeComponent();
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2244</remarks>
      private void cmdResetToDefault_General_Click(object sender, EventArgs e)
      {
         txtRowcount.Value = 0;
         txtTextSize.Value = 2147483647;
         txtExecutionTimeout.Value = 0;
         txtBatchSeparator.Text = "GO";
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2245</remarks>
      internal void cmdOk_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.OK;
         Close();
      }

   }
}
