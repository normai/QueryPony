#region Fileinfo
// file        : 20130604°0921 /QueryPony/QueryPonyLib/DbApi/OdbcQOptions.cs
// summary     : This file stores class 'ODBCQueryOptions' to define ODBC-specific query options.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Data;
using System.Windows.Forms; // DialogResult

namespace QueryPonyLib
{
   /// <summary>
   /// This class defines ODBC-specific query options that can be globally
   ///  applied to commands and/or connections. Not yet implemented for ODBC.
   /// </summary>
   /// <remarks>id : 20130604°0922</remarks>
   class ODBCQueryOptions : QueryOptions
   {
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0923</remarks>
      /// <param name="connection"></param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0924</remarks>
      /// <param name="connection"></param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0925</remarks>
      /// <param name="connection"></param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°0926
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override DialogResult ShowForm()
      {
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());
      }

      /// <summary>This method implements ... (experimental)</summary>
      /// <remarks>id : 20130705°1025</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102502)"
                      + Glb.sCrCr + "ODBC shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);
         return;
      }
   }
}
