#region Fileinfo
// file        : 20130604°1011 (20130605°1751) /QueryPony/QueryPonyLib/DbApi/OledbQueryOptions.cs
// summary     : This file stores class 'OledbQueryOptions' to define OleDb-specific query options.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyLib
{
   /// <summary>
   /// This class defines OleDb-specific query options that can be globally
   ///  applied to commands and/or connections. Not yet implemented for OleDB.
   /// </summary>
   /// <remarks>id : 20130604°1012 (20130605°1752)</remarks>
   class OledbQueryOptions : QueryOptions
   {
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1013 (20130605°1753)</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1014 (20130605°1754)</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1015 (20130605°1755)</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°1016 [sibling 20130604°1016`13]
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override DialogResult ShowForm()
      {
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());
      }

      /// <summary>This method implements ... (experimental)</summary>
      /// <remarks>id : 20130705°1026</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102602)"
                      + Glb.sCrCr + "OleDb shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }
   }
}
