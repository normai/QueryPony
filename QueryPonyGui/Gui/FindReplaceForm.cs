#region Fileinfo
// file        : 20130604°1551 /QueryPony/QueryPonyGui/Gui/FindReplaceForm.cs
// summary     : Class 'FindReplaceForm' provides ...
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyGui
{
   /// <summary>This class provides .</summary>
   /// <remarks>id : 20130604°1552</remarks>
   public partial class FindReplaceForm : Form
   {
      /// <summary>This constructor .</summary>
      /// <remarks>id : 20130604°1553</remarks>
      public FindReplaceForm()
      {
         InitializeComponent();
      }

      /// <summary>This eventhandler .</summary>
      /// <remarks>id : 20130604°1554</remarks>
      private void cmdCancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      /// <summary>This eventhandler .</summary>
      /// <remarks>id : 20130604°1555</remarks>
      private void cmdFind_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
   }
}
