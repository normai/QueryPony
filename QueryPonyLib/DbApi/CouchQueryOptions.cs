#region Fileinfo
// file        : 20130616°1651 (20130605°1751) /QueryPony/QueryPonyLib/DbApi/CouchQueryOptions.cs
// summary     : Class 'CouchQOptions' defines CouchDB-specific query options
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File cloned from SqliteQueryOptions.cs and modified (20130616°1651)
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
   /// This class defines CouchDB-specific query options that can be globally
   ///  applied to commands and/or connections. (It is not yet implemented for OleDb.)
   /// </summary>
   /// <remarks>id : 20130616°1652 (20130605°1752)</remarks>
   class CouchQueryOptions : QueryOptions
   {
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1653 (20130605°1753)</remarks>
      /// <param name="connection">...</param>
      public override void ApplyToConnection(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1654 (20130605°1754)</remarks>
      /// <param name="connection">...</param>
      public override void SetupBatch(IDbConnection connection)
      {
         return;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130616°1655 (20130605°1755)</remarks>
      /// <param name="connection">...</param>
      public override void ResetBatch(IDbConnection connection)
      {
         return;
      }

      /*
      issue 20130716°1121 'Library was pulling QueryOptions'
      Title : Library was pulling QueryOptions (and shall now stop doing this)
      Symptom : Still open is the question how to deal with the ShowForm() method inherited
         from QueryOptions. So far exists still the nasty dummy MysqlQueryOptionsForm_DUMMY()
         to keep them syntactically alive. By what shall we replace that? Or has rather the
         entire idea to be withdrawn, to igninit a dialog from the library here?
      Proposal : Let's stall such considerations, until we made some experiments from
         the other side, the GUI. The GUI is opening the QueryOptions dialog(s), and we
         should examine what happens behind them, because finally, they have to push the
         options to the library, not so much the library has to pull.
      Proposal : The GUI holds a data structure with QueryOptions. Those can be pulled from
         the library at any time without user interaction. The user interaction is purely
         optional on the GUI side. The user can set them explicitly or leave default values,
         for the library, this is transparent. (proposal 20130716°1122)
      Location : CouchQueryOptions.cs method 20130616°1656 ShowForm()
      Status : Unsolved
      */

      /// <summary>This method implements inherited abstract member ShowForm()</summary>
      /// <remarks>
      /// id : 20130616°1656 [sibling 20130604°1016`11]
      /// note : This method is involved in refactor 20130619°1311
      /// note : Work off issue 20130716°1121 'Library was pulling QueryOptions'
      /// </remarks>
      /// <returns>The wanted dialog result</returns>
      public override DialogResult ShowForm()
      {
         return ShowForm(new MysqlQueryOptionsForm_DUMMY());                   // See issue 20130716°1121
      }

      /// <summary>
      /// This method implements an experimental stub for one possible gear
      ///  to resolve issue 20130716°1121 'Library was pulling QueryOptions'.
      /// </summary>
      /// <remarks>id : 20130705°1022</remarks>
      public override void LetOptionsPushFromGui()
      {
         string sMsg = "(Debug 20130705°102202)"
                      + Glb.sCrCr + "CouchDb shall get pushed query options ..."
                       + Glb.sCrCr + "This is not implemented yet."
                        ;
         System.Windows.Forms.MessageBox.Show(sMsg);

         return;
      }
   }
}
