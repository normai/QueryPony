﻿#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DbApi/SqliteQueryOptions.cs
// id          : 20130605°1751 (20130604°1012)
// summary     : This file stores class 'SqliteQueryOptions' to define
//                SQLite-specific query options.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// versions    :
//             : -
// status      : Experimental
// note        : File cloned from OledbQueryOptions.cs and modified (20130605°1751)
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
   /// This class defines SQLite-specific query options that can be globally
   ///  applied to commands and/or connections. (It is not yet implemented for SQLite.)
   /// </summary>
   /// <remarks>id : 20130605°1752 (20130604°1012)</remarks>
   class SqliteQueryOptions : QueryOptions
   {

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130605°1753 (20130604°1013)</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130605°1754 (20130604°1014)</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130605°1755 (20130604°1015)</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }


      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130605°1756 (20130604°1016)
      /// note : This method is involved in refactor 20130619°1311
      /// </remarks>
      /// <returns>...</returns>
      public override DialogResult ShowForm()
      {
         ////return ShowForm(new QueryOptionsForm()); // original line
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());
      }


      /// <summary>This method implements ... (experimental).</summary>
      /// <remarks>id : 20130705°1029</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102902)"
                      + Glb.sCrCr + "SQLite shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);


         return;
      }

   }
}
