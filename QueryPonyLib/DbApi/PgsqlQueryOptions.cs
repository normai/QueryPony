#region Fileinfo
// file        : 20130616°1551 (20130605°1751) /QueryPony/QueryPonyLib/DbApi/PgsqlQueryOptions.cs
// summary     : Class 'PgsqlQueryOptions' defines PostgreSQL-specific query options
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from SqliteQueryOptions.cs and modified (20130616°1551)
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
   /// This class defines PostgreSQL-specific query options to be
   ///  connection-globally applied. (Possibly nothing yet implemented.)
   /// </summary>
   /// <remarks>id : 20130616°1552 (20130605°1752)</remarks>
   class PgsqlQueryOptions : QueryOptions
   {
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1553 (20130605°1753)</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1554 (20130605°1754)</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1555 (20130605°1755)</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130616°1556 [sibling 20130604°1016`14]
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override DialogResult ShowForm()
      {
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());
      }

      /// <summary>This method implements ... (experimental)</summary>
      /// <remarks>id : 20130705°1028</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102802)"
                      + Glb.sCrCr + "PostgreSQL shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }
   }
}
