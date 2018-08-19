#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Gui/MainTv.cs
// id          : 20130703°0911
// summary     : This file stores class 'MainTv' to provide the gears for the main treeview.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      :
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyGui.Properties;
using QueryPonyLib;
using System.Windows.Forms; // e.g. TabPage

namespace QueryPonyGui
{

   /// <summary>This class provides the gears for the main treeview.</summary>
   /// <remarks>
   /// id : 20130703°0912
   /// note : Declaring this class 'static' is the quickest way to get the methods
   ///    outsourced from MainForm.cs running. But I am not sure if it is the best
   ///    choice on the long term as well. (note 20130703°091202)
   /// </remarks>
   public static class MainTv
   {

      /// <summary>This method processes the main treeview's BeforeExpand eventhandler to create possible sub-nodes.</summary>
      /// <remarks>id : 20130703°0951 ()</remarks>
      internal static void BeforeExpand(TreeView tvMain, TreeNode tnTarget)
      {

         // detect the correct QueryForm from the given treenode (see debug note 20130702°1531).
         QueryForm queryform = searchCorrespondingQueryform(tvMain, tnTarget);

         // paranoia (queryform being null happens regularly)
         if (queryform == null)
         {
            return;
         }

         // if a browser has been installed, see if it has a sub-object
         //  hierarchy for us at the point of expansion
         if (queryform.Browser == null)
         {
            return;
         }

         // retrieve the basic nodes for the specific database and node type
         TreeNode[] subtree = queryform.Browser.GetSubObjectHierarchy(tnTarget);
         if (subtree != null)
         {
            tnTarget.Nodes.Clear();
            tnTarget.Nodes.AddRange(subtree);
         }

         return;
      }


      /// <summary>This method processes the main treeview's ItemDrag eventhandler to ... serve drag'n'drop.</summary>
      /// <remarks>id : 20130703°0941 (20130701°1132 20130604°2206)</remarks>
      internal static void ItemDrag(TreeView tvMain, TreeNode tnTarget)
      {
         // detect the correct QueryForm from the given treenode (see debug note 20130702°1531).
         QueryForm queryform = searchCorrespondingQueryform(tvMain, tnTarget); // adding 'tnTarget', it is not yet proofen that this is correct indeed (20130717°1236)

         // ask the browser object for a string applicable to dragging onto the query window.
         string dragText = queryform.Browser.GetDragText(tnTarget);

         // we'll use a simple string-type DataObject
         if (dragText != "")
         {
            tvMain.DoDragDrop(new DataObject(dragText), DragDropEffects.Copy);
         }

         return;
      }


      /// <summary>This method processes the main treeview's MouseUp eventhandler to ignit a context menu.</summary>
      /// <remarks>id : 20130703°0932 (20130701°1134 20130604°2203)</remarks>
      internal static void MouseUpRight(MouseEventArgs e, TreeView tvMain)
      {

         // somehow we have to detect the correct QueryForm from the given treenode (see debug note 20130702°1531).
         QueryForm queryform = null;
         queryform = searchCorrespondingQueryform(tvMain, tvMain.SelectedNode); // adding tvMain.SelectedNode, it is not yet sure whether SelectedNode is what we need (20130717°1233)

         // avoid excepion when e.g. rightclicking the 'Hello' node (sequence 20130711°0921)
         if (queryform == null)
         {
            // experimental sequence to probe non-QueryForm nodes (sequence 20130711°0922)
            System.Windows.Forms.ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Hello One", new System.EventHandler(MainTv.DoDummyAction));
            cm.MenuItems.Add("Hello Two", new System.EventHandler(MainTv.DoDummyAction));
            cm.Show(tvMain, new System.Drawing.Point(e.X, e.Y));

            return;
         }

         System.Collections.Specialized.StringCollection actions = queryform.Browser.GetActionList(tvMain.SelectedNode);
         if (actions != null)
         {
            System.Windows.Forms.ContextMenu cm = new ContextMenu();
            foreach (string action in actions)
            {
               cm.MenuItems.Add(action, new System.EventHandler(queryform.DoBrowserAction));
            }
            cm.Show(tvMain, new System.Drawing.Point(e.X, e.Y));
         }

         return;
      }


      /// <summary>This eventhandler processes a treeview item context menu selection.</summary>
      /// <remarks>id : 20130711°0923 (20130604°2204)</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private static void DoDummyAction(object sender, System.EventArgs e)
      {
         // this is called from the context menu activated by the TreeView's right-click event handler
         //  treeView_MouseUp() and appends text to the query textbox applicable to the selected menu item
         MenuItem mi = (MenuItem)sender;
         string s = mi.Text;
         string sMsg = "You selected '" + s + "'";
         MessageBox.Show(sMsg, "Notification");

         return;
      }


      /// <summary>
      /// This method processes the main treeview's AfterSelect
      ///  eventhandler to synchronize the connection tabs etc.
      /// </summary>
      /// <remarks>id : 20130703°0931 (20130701°1401)</remarks>
      /// <param name="tv">The main treeview</param>
      /// <param name="tn">The selected treenode</param>
      internal static void AfterSelect(TreeView tvMain, TreeNode tn)
      {

         // detect the correct QueryForm from the given treenode (line 20130703°0952)
         // ref : Compare debug note 20130702°1531
         // note : Possibly this inserted line makes large parts of below sequence superfluous (note 20130703°095202)
         QueryForm queryform = searchCorrespondingQueryform(tvMain, tn); // adding 'tn', it is not yet proofen that tn is what we need indeed (20130717°1234)

         // need to know which tab is corresponding
         object oTag = tn.Tag;
         string sTagType2NOTUSED = "N/A";
         if (oTag != null)
         {
            // note : Fullname is e.g. (Name were the last component of it):
            // - "QueryPonyGui.ConnectionSettings+Server"
            // - "QueryPonyGui.ConnectionSettings+Database"
            sTagType2NOTUSED = oTag.GetType().FullName;
         }

         // syntax experiment
         string sTagType = "";
         if (oTag == null)
         {
            sTagType = "N/A";
         }
         else if (oTag.GetType() == typeof(ConnSettingsGui.Server))
         {
            ConnSettingsGui.Server oSrv = oTag as ConnSettingsGui.Server;
            sTagType = "Server" + "/" + oSrv.Name;
         }
         else if (oTag.GetType() == typeof(ConnSettingsLib))
         {
            //-------------------------------------------------
            // (issue 20130724°0927)
            // title : About the (Lib's) ConnSettings ToString() method.
            // note : A database always holds a ConnSettings object. The use of below
            //    'oDb.ConnSettingsLib.ToString();' makes not much sense as long as
            //    the ToString() method of that class is not overridden meaningfully.
            //    But it does not hurt either so far.
            // note : We will come back to this topic when attempt to refacture the GUI's
            //    ConnSettings class away. See issue 20130724°0923 and todo 20130724°0925.
            //-------------------------------------------------

            ConnSettingsLib oDb = oTag as ConnSettingsLib;
            sTagType = oDb.ToString(); // is always "QueryPonyLib.ConnSettings" //// so what? [new 20130724°0926] [not new 20130724°092115]
            TabPage tabpage = MainTv.searchCorrespondingTabPage(oDb);
         }
         else
         {
            // (20130810°1101)
            if (Properties.Settings.Default.DeveloperMode)
            {
               sTagType = "ProgramFlowFuzzy";
            }
         }

         // debug message
         if (Properties.Settings.Default.DeveloperMode)
         {
            string sMsg = "[Debug] Node selected: Text = '" + tn.Text + "'," + Glb.sBlnk + "Tpye = " + sTagType;
            MainForm.outputStatusLine(sMsg);
         }
      }


      //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
      // below are search methods a la issue 20130729°1521
      //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


      //-------------------------------------------------------
      // issue 20130729°1521
      // title : 'Centralize connection search methods' or 'Synchronizing
      //    various controls across the system'
      // story : To synchronize various controls, we often need to search
      //    a specific control in a control collection, e.g.:
      //    - On changing a treenode, search the corresponding tabpage.
      //    - On selecting a Connect TabPage, search the corresponding connection
      //       combobox entry to adjust the combobox appearance.
      //    - On selecting a Connection ComboBox entry, search the corresponding
      //       Connect TabPage to adjust the tabcontrol's appearance.
      //    - On closing a QueryForm, search the corresponding treenode.
      //    The situation now is, that there exist already some methods for such kind
      //    of tasks, but they are dispersed, and possibly redundant. The means to
      //    identify the control for a certain connection are not matured yet.
      // todo : Collect those methods at one place, try to bring some simple system
      //    into them, try to remove redundancies, standardize the control recognition.
      // note : Methods for such kind of tasks are so far:
      //    - 20130702°1511 TabPage MainTv.searchCorrespondingTabPage(ConnSettings csLib)
      //    - 20130703°0933 QueryForm MainTv.searchCorrespondingQueryform(TreeNode tnTarget)
      //    - 20130623°1121 int ConnectForm.findConnectionInConnectionCombobox()
      //    - 20130623°1011 string ConnectForm.getConnectionIdFromTabpage()
      //    - 20130701°1221 TreeNode QueryForm.searchServerNode(TreeView tv, object tag)
      //    - 20130701°1211 void QueryForm.SearchNodesByTag(object oSearch, TreeNode tnStart)
      //    -
      //    -
      // proposal : Perhaps it were reasonable to introduce some dedicated 'state
      //    objects' to maintain those synchronisations. E.g.
      //    - one state object to sync the Connect tabs and the Connection combobox
      //    - one state object to sync the main treeview and the Connection tabs
      //-------------------------------------------------------


      /// <summary>
      /// This method helps the main treeview's AfterSelect
      ///  event to find it's corresponding TabPage.
      /// </summary>
      /// <remarks>id : 20130702°1511</remarks>
      /// <param name="csLib">The library connection settings by which the wanted tabpage can be identified</param>
      /// <returns>The wanted TabPage (possibly proforma only)</returns>
      public static TabPage searchCorrespondingTabPage(ConnSettingsLib csLib)
      {
         TabPage tabpage = null;

         // see whether for the given connection setting a correspondend tabpage exists
         int iFound = -1;
         for (int i = 0; i < MainForm._maintabcontrol.Controls.Count; i++)
         {
            ConnSettingsLib csLibCompare = MainForm._maintabcontrol.Controls[i].Tag as ConnSettingsLib;
            if (csLibCompare == null)
            {
               continue;
            }
            if (csLib == csLibCompare)
            {
               iFound = i;
               break;
            }
         }

         // a corresponding tabpage was found, and we can act on it
         if (iFound >= 0)
         {
            tabpage = MainForm._maintabcontrol.Controls[iFound] as TabPage;

            if (tabpage == null)
            {
               string sMsg = Glb.Errors.TheoreticallyNotPossible + Glb.sBlnk + "(Error 20130702°1512)";
               MessageBox.Show(sMsg);
            }

            // do the finally wanted service right here, make the found tab the shown one
            MainForm._maintabcontrol.SelectedTab = tabpage;
         }

         // return the possibly found tabpage, so the caller can do even more with it
         return tabpage;
      }


      /// <summary>
      /// This method searches the QueryForm corresponding to the given TreeNode.
      ///  It seems to be the key method for the treeview/tabcontrol intercommunication.
      /// </summary>
      /// <remarks>
      /// id : 20130703°0933
      /// todo : By it's intention, this method is very similar to method
      ///    20130702°1511 searchCorrespondingTabPage(). Possibly the two can be
      ///    merged or otherwise be simplified/refactored. (todo 20130703°093302)
      /// </remarks>
      /// <param name="tvMain">The treeview with it's selected node for which to search the corresponding QueryForm</param>
      /// <returns>The wanted QueryForm</returns>
      private static QueryForm searchCorrespondingQueryform(TreeView tvMain_SUPERFLUOUS, TreeNode tnTarget)
      {
         QueryForm qfRet = null;
         ConnSettingsLib csd = null;

         // paranoia (no node being selected happens regularly)
         if (tnTarget == null)
         {
            return qfRet;
         }

         // search the nodes hierarchy up to find a node tagged as type Database
         TreeNode tn = tnTarget;
         csd = tn.Tag as ConnSettingsLib;
         while ((csd == null) && (tn != null))
         {
            tn = tn.Parent;
            if (tn != null)
            {
               csd = tn.Tag as ConnSettingsLib;
            }
         }

         // no Database treenode found?
         if (csd == null)
         {
            return qfRet;
         }

         // for the found database treenode, search the corresponding tabpage (sequence 20130703°0934)
         // note : This is the sequence redundant in 20130702°1511 searchCorrespondingTabPage().
         ConnSettingsLib csLib = csd;
         TabPage tabpage = null;
         int iFound = -1;
         for (int i = 0; i < MainForm._maintabcontrol.Controls.Count; i++)
         {
            ConnSettingsLib csLibCompare = MainForm._maintabcontrol.Controls[i].Tag as ConnSettingsLib;
            if (csLibCompare == null)
            {
               continue;
            }
            if (csLib == csLibCompare)
            {
               iFound = i;
               break;
            }
         }

         // a tabpage was found
         if (iFound >= 0)
         {
            // is it a tabpage indeed?
            tabpage = MainForm._maintabcontrol.Controls[iFound] as TabPage;

            // paranoia
            if (tabpage == null)
            {
               string sMsg = Glb.Errors.TheoreticallyNotPossible + Glb.sBlnk + "(Error 20130703°0935)";
               MessageBox.Show(sMsg);
            }

            // we assume, that one QueryForm tabpage contains only one control, and that is the wanted QueryForm
            qfRet = tabpage.Controls[0] as QueryForm;


            // do the finally wanted service right here (not sure whether this is the right place to do)
            MainForm._maintabcontrol.SelectedTab = tabpage;
         }

         return qfRet;
      }


      /// <summary>This method searches a database TreeNode by it's ConnSettings.</summary>
      /// <remarks>id : 20130729°1531</remarks>
      /// <param name="csTarget">The connection settings for which the corresponding treenode is wanted</param>
      /// <returns>The wanted treenode or null</returns>
      public static TreeNode searchTreenodeByConnSettings(ConnSettingsLib csTarget)
      {
         TreeNode tnRet = null;

         // iterate over all database nodes
         // issue : The traversing algorithm can be that simple, because given hierarchy
         //    structure is simply 'server nodes on top and under them database nodes'.
         //    But remember: The algorithm must be adjusted, becoming more complicated,
         //    if we depart from that simple schema. [issue 20130729°1532]
         foreach (TreeNode tnTop in MainForm.TreeviewMain.Nodes)
         {
            foreach (TreeNode tnDb in tnTop.Nodes)
            {
               ConnSettingsLib csNode = tnDb.Tag as ConnSettingsLib;
               if (csNode == csTarget)
               {
                  tnRet = tnDb;
               }
            }
         }

         return tnRet;
      }
   }
}
