#region Fileinfo
// file        : 20130604°2221 /QueryPony/QueryPonyLib/DbApi/QueryOptions.cs
// summary     : This file stores class 'QueryOptions' to provide query options
//                that can be globally applied to commands and/or connections.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Data;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>
   /// This abstract class defines query options that can
   ///  be globally applied to commands and/or connections.
   /// </summary>
   /// <remarks>
   /// id : 20130604°2222
   /// note : Access modifier changed from 'internal' to 'public' to
   ///    make it accessible from other projects (20130604°1425)
   /// </remarks>
   public abstract class QueryOptions
   {
      /// <summary>This field stores the number of rows ...</summary>
      /// <remarks>id : 20130604°2223</remarks>
      private int _RowCount = 0;

      /// <summary>This field stores the display max width of text fields in pixels</summary>
      /// <remarks>id : 20130604°2224</remarks>
      private int _maxTextWidth = 60;

      /// <summary>This field stores the batch separator</summary>
      /// <remarks>id : 20130604°2225</remarks>
      private string _BatchSeparator = "GO";

      /// <summary>This field stores the timeout in seconds(?)</summary>
      /// <remarks>id : 20130604°2226</remarks>
      private int _ExecutionTimeout = 0;

      /// <summary>This property gets/sets the batch separator</summary>
      /// <remarks>id : 20130604°2227</remarks>
      public virtual string BatchSeparator
      {
         get { return _BatchSeparator; }
         set { _BatchSeparator = value; }
      }

      /// <summary>This property gets/sets the display max width of text fields in pixels</summary>
      /// <remarks>id : 20130604°2228</remarks>
      public virtual int maxTextWidth
      {
         get { return _maxTextWidth; }
         set { _maxTextWidth = value; }
      }

      /// <summary>This property gets/sets the number of rows ...</summary>
      /// <remarks>id : 20130604°2229</remarks>
      public virtual int RowCount
      {
         get { return _RowCount; }
         set { _RowCount = value; }
      }

      /// <summary>This property gets/sets the query timeout in seconds(?), zero means no timeout(?)</summary>
      /// <remarks>id : 20130604°2231</remarks>
      public virtual int ExecutionTimeout
      {
         get { return _ExecutionTimeout; }
         set { _ExecutionTimeout = value; }
      }

      /// <summary>This abstract method applies this query options to the given connection</summary>
      /// <remarks>id : 20130604°2232</remarks>
      public abstract void ApplyToConnection(IDbConnection connection);

      /// <summary>This abstract method sets up the batch ...</summary>
      /// <remarks>id : 20130604°2233</remarks>
      public abstract void SetupBatch(IDbConnection connection);

      /// <summary>This abstract method resets the batch ...</summary>
      /// <remarks>id : 20130604°2234</remarks>
      public abstract void ResetBatch(IDbConnection connection);

      /// <summary>This abstract method displays the (modal) QueryOptions dialog form</summary>
      /// <remarks>id : 20130604°2235</remarks>
      public abstract DialogResult ShowForm();

      /// <summary>This abstract method ... experimental ...</summary>
      /// <remarks>id : 20130705°1021</remarks>
      public abstract void LetOptionsPushFromGui();

      /// <summary>This method retrieves Query Options from the user through a modal form</summary>
      /// <remarks>
      /// id : 20130604°2236
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      protected DialogResult ShowForm(MysqlQueryOptionsForm_DUMMY optionsForm)
      {
         FieldsToForm(optionsForm);

         DialogResult res = optionsForm.ShowDialog_Mysql();
         if (res == DialogResult.OK)
         {
            FormToFields(optionsForm);
         }
         return res;

      }

      /// <summary>This method reads the values from the form into the properties</summary>
      /// <remarks>
      /// id : 20130604°2237
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      protected void FormToFields(MysqlQueryOptionsForm_DUMMY f)
      {
         this.BatchSeparator = f.txtBatchSeparator.Text;
         this.ExecutionTimeout = (int)(f.txtExecutionTimeout.Value);
         this.RowCount = (int)(f.txtRowcount.Value);
      }

      /// <summary>This method displays the properties on the form</summary>
      /// <remarks>
      /// id : 20130604°2238
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      protected void FieldsToForm(MysqlQueryOptionsForm_DUMMY f)
      {
         string sDbg = this.BatchSeparator;

         f.txtBatchSeparator.Text = this.BatchSeparator;
         f.txtExecutionTimeout.Value = this.ExecutionTimeout;
         f.txtRowcount.Value = this.RowCount;
      }
   }
}
