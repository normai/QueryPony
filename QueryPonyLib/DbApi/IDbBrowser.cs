#region Fileinfo
// file        : 20130604°0441 /QueryPony/QueryPonyLib/DbApi/IDbBrowser.cs
// summary     : This file stores interface 'IBrowser' to defines DbBrowser
//                classes (for an Explorer-like tree view of a database).
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
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyLib
{

   #region DbBrowser Interface

   /// <summary>This interface defines Browser classes (an Explorer-like tree view of a database)</summary>
   /// <remarks>
   /// id : 20130604°0442
   /// note : Access modifier switched from 'internal' to 'public' to
   ///    make it accessible from other projects. [note 20130604°1422]
   /// </remarks>
   public interface IDbBrowser
   {
      /// <summary>
      /// This interface method creates and returns a new browser object, using
      ///  the supplied database client object.
      /// </summary>
      /// <remarks>
      /// id : 20130604°0450
      /// note : What is this method good for? It seems never be called at all, and it
      ///    seems not necessary to satisfy any interface implementation. [note 20130720°1221]
      /// </remarks>
      /// <param name="newDbClient">The other DbClient for which the clone is wanted</param>
      /// <returns>The wanted newly cloned DbBrowser object</returns>
      IDbBrowser Clone(DbClient newDbClient);

      /// <summary>This property gets the active DbClient object given in the constructor</summary>
      /// <remarks>id : 20130604°0443</remarks>
      DbClient DbClient { get; }

      /// <summary>
      /// This interface method returns a list of actions applicable to a node
      ///  (suitable for a context menu), or null if no actions are applicable.
      /// </summary>
      /// <remarks>id : 20130604°0447</remarks>
      /// <param name="node">The treenode for which the context menu shall be created</param>
      /// <returns>The created context menu items for the given treenode</returns>
      StringCollection GetActionList(TreeNode node);

      /// <summary>
      /// This interface method returns text suitable for pasting into a query
      ///  window, given a particular node and action. GetActionList() should be
      ///  called first to obtain a list of applicable actions.
      /// </summary>
      /// <remarks>id : 20130604°0448</remarks>
      /// <!-- param name="actionIndex">One of the action text strings returned by GetActionList()</param -->
      /// <param name="node">The table node</param>
      /// <param name="action">The menu item's text</param>
      /// <returns>The wanted command string</returns>
      string GetActionText(TreeNode node, string sAction);

      /// <summary>This interface retrieves the list of available databases on this server</summary>
      /// <remarks>id : 20130604°0449</remarks>
      /// <returns>The wanted list of databases</returns>
      string[] GetDatabases();

      /// <summary>
      /// This interface method returns text suitable for dropping
      ///  into a query window, for a given node.
      /// </summary>
      /// <remarks>id : 20130604°0446</remarks>
      /// <param name="node">The treenode from which the drag text is wanted</param>
      /// <returns>The wanted drag text</returns>
      string GetDragText(TreeNode node);

      /// <summary>
      /// This interface method returns an array of TreeNodes representing the
      ///  object hierarchy for the 'Explorer' view. This can return either the
      ///  entire hierarchy, or for efficiency, just the higher level(s).
      /// </summary>
      /// <remarks>id : 20130604°0444</remarks>
      /// <returns>The wanted array of treenodes</returns>
      TreeNode[] GetObjectHierarchy();

      /// <summary>
      /// This interface method returns an array of TreeNodes representing the
      ///  object hierarchy below a given node. This should return null if there
      ///  is no hierarchy below the given node, or if the hierarchy is already
      ///  present. This method is called whenever the user expands a node.
      /// </summary>
      /// <remarks>
      /// id : 20130604°0445
      /// todo : Possibly rename this method to e.g. 'GetActionTextHierarchy()',
      ///    this would indicate that this method is called always and only from
      ///    GetActionText(). (todo 20130723°1413)
      /// </remarks>
      /// <param name="node">The node for which to retrieve the subhierarchy</param>
      /// <returns>The wanted array of treenodes</returns>
      TreeNode[] GetSubObjectHierarchy(TreeNode node);

      /// <summary>This interface method attaches field an index subnodes to the given table node</summary>
      /// <remarks>id : 20130819°1501 (20130604°0445)</remarks>
      /// <param name="node">The node to be supplemented with a subhierarchy</param>
      /// <returns>Success flag</returns>
      bool GetSubObjectHierarchy2(Nodes.Table node);

      /// <summary>This interface method retrieves the collections available for this data provider</summary>
      /// <remarks>id : 20130826°1211</remarks>
      /// <returns>The wanted array of collections</returns>
      string[] SchemaGetCollections();

      /// <summary>This interface method retrieves the array of index nodes for the given table</summary>
      /// <remarks>id : 20130825°1311</remarks>
      /// <returns>The wanted array of Index Nodes</returns>
      Nodes.Indices[] SchemaGetIndices(Nodes.Table ndTable);

      /// <summary>This interface method retrieves an experimental schema object for debug purposes</summary>
      /// <remarks>id : 20130819°0921</remarks>
      /// <returns>The wanted schema object, e.g. an DataTable or a XML table</returns>
      object SchemaGetSchema();

      /// <summary>This interface method retrieves the list of tables in this IDbBrowser's DbClient</summary>
      /// <remarks>id : 20130819°0701</remarks>
      /// <returns>The wanted list of tables</returns>
      string[] SchemaGetTables();
   }
   #endregion DbBrowser Interface
}
