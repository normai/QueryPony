#region Fileinfo
// file        : 20130604°0501 /QueryPony/QueryPonyGui/Gui/IQueryForm.cs
// summary     : This file stores interface 'IQueryForm' and class 'MRUFileAddedEventArgs' to provide ...
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryPonyGui
{

   /// <summary>This class provides the MRUFileAddedEventArgs event parameters</summary>
   /// <remarks>id : 20130604°0502</remarks>
   public class MRUFileAddedEventArgs : EventArgs
   {

      /// <summary>This field stores the filename of this event</summary>
      /// <remarks>id : 20130604°0503</remarks>
      private string filename;

      /// <summary>This constructor creates a new MRUFileAddedEventArgs for the given file</summary>
      /// <remarks>id : 20130604°0504</remarks>
      /// <param name="filename">...</param>
      public MRUFileAddedEventArgs(string filename)
      {
         this.filename = filename;
      }

      /// <summary>This property gets the filename of this MRUFileAddedEventArgs</summary>
      /// <remarks>id : 20130604°0505</remarks>
      public string Filename
      {
         get
         {
            return this.filename;
         }
      }
   }

   /// <summary>This interface defines the requirements for a QueryForm</summary>
   /// <remarks>
   /// id : 20130604°0506
   /// note : Access modifier set 'public' to make it accessible from other projects [note 20130604°1421]
   /// </remarks>
   public interface IQueryForm
   {

      /// <summary>This field stores the PropertyChanged eventhandler</summary>
      /// <remarks>id : 20130604°0507</remarks>
      event EventHandler<EventArgs> PropertyChanged;

      /// <summary>This field stores the MRUFileAddedEventArgs eventhandler</summary>
      /// <remarks>id : 20130604°0508</remarks>
      event EventHandler<MRUFileAddedEventArgs> MRUFileAdded;

      /// <summary>This method opens a ... file via the file browser dialog</summary>
      /// <remarks>id : 20130604°0509</remarks>
      /// <returns>Success flag</returns>
      bool Open();

      /// <summary>This method opens the ... file with the given filename</summary>
      /// <remarks>id : 20130604°0510</remarks>
      /// <param name="fileName">The file to open</param>
      /// <returns>Success flag</returns>
      bool Open(string fileName);

      /// <summary>This method executes a query command</summary>
      /// <remarks>id : 20130604°0511</remarks>
      void Execute();

      /// <summary>This method saves the ... file</summary>
      /// <remarks>id : 20130604°0512</remarks>
      /// <returns>Success flag</returns>
      bool Save();

      /// <summary>This method saves the ... file via the save-as dialog</summary>
      /// <remarks>id : 20130604°0513</remarks>
      /// <returns>Success flag</returns>
      bool SaveAs();

      /// <summary>This method saves the query results</summary>
      /// <remarks>id : 20130604°0514</remarks>
      void SaveResults();

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0515</remarks>
      void Cancel();

      /// <summary>This method closes ...</summary>
      /// <remarks>id : 20130604°0516</remarks>
      void Close();

      /// <summary>This method shows the QueryOptions dialog</summary>
      /// <remarks>id : 20130604°0517</remarks>
      void ShowQueryOptions();

      /// <summary>This property gets/sets the flag whether to show the query result as text or as table</summary>
      /// <remarks>id : 20130604°0518</remarks>
      bool ResultsInText { get; set; }

      /// <summary>This property gets/sets the flag whether to show nulls in the table grid or not</summary>
      /// <remarks>id : 20130604°0519</remarks>
      bool GridShowNulls { get; set; }

      /// <summary>This property gets/sets the visibility of the dedicated/legacy treeview</summary>
      /// <remarks>id : 20130604°0520</remarks>
      bool HideBrowser { get; set; }

      /// <summary>This property gets/sets the DbBrowser object for this QueryForm</summary>
      /// <remarks>id : 20130604°0521</remarks>
      IDbBrowser Browser { get; }

      /// <summary>This method shows the find dialogbox</summary>
      /// <remarks>id : 20130604°0522</remarks>
      void ShowFind();

      /// <summary>This method shows the find-next dialogbox</summary>
      /// <remarks>id : 20130604°0523</remarks>
      void FindNext();

      /// <summary>This property gets the RunState of this QueryForm</summary>
      /// <remarks>id : 20130604°0524</remarks>
      DbClient.RunStates RunState { get; }

      /// <summary>This method clones this query form (what?)</summary>
      /// <remarks>id : 20130604°0525</remarks>
      IQueryForm Clone();

      /// <summary>This method switches the visibility of the developer objects</summary>
      /// <remarks>id : 20130828°1615</remarks>
      void switchDeveloperObjectsVisibility(bool bVisible);
   }
}
