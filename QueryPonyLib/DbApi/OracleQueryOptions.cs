#region Fileinfo
// file        : 20130604°1051 /QueryPony/QueryPonyLib/DbApi/OracleQueryOptions.cs
// summary     : This file stores class 'OracleQueryOptions' define Oracle-specific query options.
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
   /// This class defines Oracle-specific query options that can be globally
   ///  applied to commands and/or connections. Not yet implemented for Oracle.
   /// </summary>
   /// <remarks>id : 20130604°1052</remarks>
   class OracleQueryOptions : QueryOptions
   {
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1053</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1054</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1055</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°1056
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override DialogResult ShowForm()
      {
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());
      }

      /// <summary>This method implements ... (experimental)</summary>
      /// <remarks>id : 20130705°1027</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102702)"
                      + Glb.sCrCr + "Oracle shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }
   }
}
