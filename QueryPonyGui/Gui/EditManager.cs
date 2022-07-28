#region Fileinfo
// file        : 20130604°0401 /QueryPony/QueryPonyGui/Gui/EditManager.cs
// summary     : This file stores class 'EditManager' to provide a mediator for managing Edit menu commands (copy, cut, paste, etc).
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QueryPonyGui
{
   /// <summary>This class provides a mediator for managing Edit menu commands (copy, cut, paste, etc)</summary>
   /// <remarks>id : 20130604°0402</remarks>
   public class EditManager
   {
      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Copy'</summary>
      /// <remarks>id : 20130604°0345</remarks>
      ToolStripMenuItem miCopy;

      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Copy With Headers'</summary>
      /// <remarks>id : 20130604°0346</remarks>
      ToolStripMenuItem miCopyWithHeaders;

      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Cut'</summary>
      /// <remarks>id : 20130604°0347</remarks>
      ToolStripMenuItem miCut;

      /// <summary>This field stores ToolStripMenuItem 'Edit' to which to connect</summary>
      /// <remarks>id : 20130604°0403</remarks>
      ToolStripMenuItem miEdit;

      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Paste'</summary>
      /// <remarks>id : 20130604°0348</remarks>
      ToolStripMenuItem miPaste;

      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Select All'</summary>
      /// <remarks>id : 20130604°0349</remarks>
      ToolStripMenuItem miSelectAll;

      /// <summary>This field stores ToolStripMenuItem 'Edit' submenu item 'Undo'</summary>
      /// <remarks>id : 20130604°0344</remarks>
      ToolStripMenuItem miUndo;

      /// <summary>This field stores the (singleton) instance of this class</summary>
      /// <remarks>id : 20130604°0404</remarks>
      private static EditManager _EditManagerInstance = new EditManager();

      /// <summary>This method creates a new EditManager object</summary>
      /// <remarks>id : 20130604°0405</remarks>
      private EditManager()
      {
         MenuItemCopy = null;
         MenuItemCopyWithHeaders = null;
         MenuItemCut = null;
         MenuItemEdit = null;
         MenuItemPaste = null;
         MenuItemSelectAll = null;
         MenuItemUndo = null;
      }

      /// <summary>This method delivers a EditManager (singleton) instance</summary>
      /// <remarks>id : 20130604°0406</remarks>
      /// <returns>The wanted EditManager (singleton) instance</returns>
      public static EditManager GetEditManager()
      {
         return _EditManagerInstance;
      }

      /// <summary>
      /// This property gets/sets the the ToolStripMenuItem 'Edit', implementing the
      ///  Edit submenu. It attaches/detaches an event handler to popup event so we can
      ///  enable/disable sub-items when menu is activated.
      /// </summary>
      /// <remarks>id : 20130604°0407</remarks>
      public ToolStripMenuItem MenuItemEdit
      {
         get { return miEdit; }
         set
         {
            if (miEdit != null)
            {
               //miEdit.Click -= new EventHandler(miEdit_Popup);
               miEdit.DropDownOpening -= new EventHandler(miEdit_Popup);
            }
            miEdit = value;
            if (miEdit != null)
            {
               //miEdit.Click += new EventHandler(miEdit_Popup);
               miEdit.DropDownOpening += new EventHandler(miEdit_Popup);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Undo'</summary>
      /// <remarks>id : 20130604°0408</remarks>
      public ToolStripMenuItem MenuItemUndo
      {
         get { return miUndo; }
         set
         {
            if (miUndo != null)
            {
               miUndo.Click -= new EventHandler(miUndo_Click);
            }
            miUndo = value;
            if (miUndo != null)
            {
               miUndo.Click += new EventHandler(miUndo_Click);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Copy'</summary>
      /// <remarks>id : 20130604°0409</remarks>
      public ToolStripMenuItem MenuItemCopy
      {
         get { return miCopy; }
         set
         {
            if (miCopy != null)
            {
               miCopy.Click -= new EventHandler(miCopy_Click);
            }
            miCopy = value;
            if (miCopy != null)
            {
               miCopy.Click += new EventHandler(miCopy_Click);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Copy With Headers'</summary>
      /// <remarks>id : 20130604°0410</remarks>
      public ToolStripMenuItem MenuItemCopyWithHeaders
      {
         get { return miCopyWithHeaders; }
         set
         {
            if (miCopyWithHeaders != null)
            {
               miCopyWithHeaders.Click -= new EventHandler(miCopyWithHeaders_Click);
            }
            miCopyWithHeaders = value;
            if (miCopyWithHeaders != null)
            {
               miCopyWithHeaders.Click += new EventHandler(miCopyWithHeaders_Click);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Cut'</summary>
      /// <remarks>id : 20130604°0411</remarks>
      public ToolStripMenuItem MenuItemCut
      {
         get { return miCut; }
         set
         {
            if (miCut != null)
            {
               miCut.Click -= new EventHandler(miCut_Click);
            }
            miCut = value;
            if (miCut != null)
            {
               miCut.Click += new EventHandler(miCut_Click);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Paste'</summary>
      /// <remarks>id : 20130604°0412</remarks>
      public ToolStripMenuItem MenuItemPaste
      {
         get { return miPaste; }
         set
         {
            if (miPaste != null)
            {
               miPaste.Click -= new EventHandler(miPaste_Click);
            }
            miPaste = value;
            if (miPaste != null)
            {
               miPaste.Click += new EventHandler(miPaste_Click);
            }
         }
      }

      /// <summary>This property gets/sets the ToolStripMenuItem 'Select All'</summary>
      /// <remarks>id : 20130604°0413</remarks>
      public ToolStripMenuItem MenuItemSelectAll
      {
         get { return miSelectAll; }
         set
         {
            if (miSelectAll != null)
            {
               miSelectAll.Click -= new EventHandler(miSelectAll_Click);
            }
            miSelectAll = value;
            if (miSelectAll != null)
            {
               miSelectAll.Click += new EventHandler(miSelectAll_Click);
            }
         }
      }

      /// <summary>This method provides a context menu for a control</summary>
      /// <remarks>id : 20130604°0414</remarks>
      /// <param name="parent">The control for which this context menu shall be shown.</param>
      /// <returns>The wanted context menu</returns>
      public ContextMenuStrip GetContextMenuStrip(Control parent)              // Ddebug menu items gray  [mark 20130810°1602`12 ]
      {
         ContextMenuStrip cm = new ContextMenuStrip();
         //cm.Parent = parent;
         cm.Opened += new EventHandler(miEdit_Popup);
         cm.Items.AddRange ( new ToolStripItem[]
                           { new ToolStripMenuItem ( miCopy.Text
                                                    , miCopy.Image
                                                     , new EventHandler(miCopy_Click)
                                                      )
                             , new ToolStripMenuItem ( miCopyWithHeaders.Text
                                                      , miCopyWithHeaders.Image
                                                       , new EventHandler(miCopyWithHeaders_Click)
                                                        )
                             , new ToolStripMenuItem ( miSelectAll.Text
                                                      , miSelectAll.Image
                                                       , new EventHandler(miSelectAll_Click)
                                                        )
                           });
         if (! (parent is DataGridView))
         {
            cm.Items[1].Enabled = false;
         }
         return cm;
      }

      /// <summary>This method delivers the context menu</summary>
      /// <remarks>id : 20130604°0415</remarks>
      /// <returns>The wanted context menu</returns>
      public ContextMenu GetContextMenu()
      {
         MenuItem[] mi = new MenuItem[] { new MenuItem(miCopy.Text, new EventHandler(miCopy_Click)) };
         ContextMenu cm = new ContextMenu();
         cm.MenuItems.AddRange (   new MenuItem[]
                                {  new MenuItem ( miCopy.Text, new EventHandler(miCopy_Click) )
                                 , new MenuItem ( miCopyWithHeaders.Text, new EventHandler(miCopyWithHeaders_Click) )
                                  });
         return cm;
      }

      /// <summary>This method processes the context menu 'Copy' action</summary>
      /// <remarks>id : 20130604°0417</remarks>
      public void Copy()                                                       // Debug menu items gray [mark 20130810°1602`13]
      {
         Control ctrl = GetActiveControl();

         if (ctrl is TextBoxBase)
         {
            TextBoxBase tbb = (TextBoxBase) ctrl;
            tbb.Copy();
         }

         if (ctrl is DataGridView)
         {
            DataGridView dgv = (DataGridView) ctrl;

            // Add selection to clipboard
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            Clipboard.SetDataObject(dgv.GetClipboardContent());
         }

         if (ctrl is ComboBox)
         {
            ComboBox cb = (ComboBox) ctrl;
            // What about this? It has no Copy() method. Possibly look how the context menu does it.
         }
      }

      /// <summary>This method processes the context menu 'Copy With Headers' action</summary>
      /// <remarks>id : 20130604°0418</remarks>
      public void CopyWithHeaders()
      {
         Control ctrl = GetActiveControl();

         if (ctrl is DataGridView)
         {
            DataGridView dgv = (DataGridView) ctrl;

            // Add selection to clipboard
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            Clipboard.SetDataObject(dgv.GetClipboardContent());
         }
      }

      /// <summary>This method processes the context menu 'Cut' action</summary>
      /// <remarks>id : 20130604°0419</remarks>
      public void Cut()
      {
         Control ctrl = GetActiveControl();

         if (ctrl is TextBoxBase)
         {
            TextBoxBase tbb = (TextBoxBase) ctrl;
            tbb.Cut();
         }

         if (ctrl is ComboBox)
         {
            ComboBox cb = (ComboBox) ctrl;
            // What about this? It has no Cut() method. Possibly look how the context menu does it.
         }
      }

      /// <summary>This method processes the context menu 'Paste' action</summary>
      /// <remarks>
      /// id : 20130604°0420
      /// note : Not available for ComboBox
      /// </remarks>
      public void Paste()
      {
         Control ctrl = GetActiveControl();

         if (ctrl is TextBoxBase)
         {
            TextBoxBase tbb = (TextBoxBase) ctrl;
            tbb.Paste();
         }

         if (ctrl is ComboBox)
         {
            ComboBox cb = (ComboBox) ctrl;
            // What about this? It has no Copy() method. Possibly look how the context menu does it.
         }
      }

      /// <summary>This method processes the context menu 'Select All' action</summary>
      /// <remarks>id : 20130604°0421</remarks>
      public void SelectAll()
      {
         Control ctrl = GetActiveControl();
         if (ctrl is TextBoxBase)
         {
            TextBoxBase tbb = (TextBoxBase) ctrl;
            tbb.SelectAll();
         }
         if (ctrl is DataGridView)
         {
            DataGridView dgv = (DataGridView) ctrl;
            dgv.SelectAll();
         }

         // [seq 20130810°1624]
         if (ctrl is ComboBox)                                                 // Debug menu items gray [mark 20130810°1602`14]
         {
            ComboBox cb = (ComboBox) ctrl;
            cb.SelectAll();
         }
      }

      /// <summary>This method processes the context menu 'Undo' action</summary>
      /// <remarks>
      /// id : 20130604°0416
      /// note : Not available for ComboBox
      /// </remarks>
      public void Undo()
      {
         Control ctrl = GetActiveControl();

         if (ctrl is TextBoxBase)
         {
            TextBoxBase tb = (TextBoxBase) ctrl;
            tb.Undo();
         }

         if (ctrl is ComboBox)
         {
            ComboBox cb = (ComboBox) ctrl;
            // What about this? It has no Undo() method. Possibly look how the context menu does it.
         }
      }

      /// <summary>This method ... (seemingly) ... returns the currently active control. (Formerly MDI, now tabbed.)</summary>
      /// <remarks>
      /// id : 20130604°0422
      /// note : This seems one of the methods we have to touch after switching from MDI to tabbed. [note 20130619°0454]
      /// </remarks>
      /// <returns>The wanted control</returns>
      protected Control GetActiveControl_OBSOLET()
      {
         Form form = Form.ActiveForm;
         if (form != null && form.IsMdiContainer)
         {
            form = form.ActiveMdiChild;
         }
         return GetActiveControl(form);
      }

      /// <summary>This method  returns the currently active control. (Formerly MDI, now tabbed.)</summary>
      /// <remarks>
      /// id : 20130619°0511 (20130604°0422)
      /// note : This seems one of the methods we have to touch after switching from MDI to tabbed. (20130619°0454)
      /// </remarks>
      /// <returns>The wanted control</returns>
      protected Control GetActiveControl()                                     // Debug menu items gray [mark 20130810°1602`15]
      {
         // the original lines from the former MDI method
         if (QueryPonyLib.Glb.Debag.ShutdownFeatureMdiWindows)
         {
            Form form = Form.ActiveForm;
            if (form != null && form.IsMdiContainer)
            {
               form = form.ActiveMdiChild;
            }
            return GetActiveControl(form);
         }

         //----------------------------------------------------
         // Against issue 20130624°1011 'shortcut key fail' [seq 20130810°1622]
         // Note : Compare reference 20130810°1611 'find focused control'
         Control ctrl = MainForm._mainform;
         ContainerControl container = ctrl as ContainerControl;
         while (container != null)
         {
            ctrl = container.ActiveControl;
            container = ctrl as ContainerControl;
         }
         //----------------------------------------------------

         return ctrl;
      }

      /// <summary>This method .</summary>
      /// <remarks>id : 20130604°0423</remarks>
      /// <param name="ContainerControl">...</param>
      /// <returns>The wanted control</returns>"
      protected Control GetActiveControl_OBSOLET(Control ContainerControl)
      {
         if (ContainerControl == null || ContainerControl as ContainerControl == null)
         {
            return ContainerControl;
         }
         else
         {
            return GetActiveControl(((ContainerControl)ContainerControl).ActiveControl);
         }
      }

      /// <summary>
      /// This method returns the given control if it is not a container,
      ///  and if it is a container, it returns the active control inside that.
      /// </summary>
      /// <remarks>
      /// id : 20130619°0514 (20130604°0423)
      /// note : This method should be superfluous after we switched from MDI to Forms-on-Tabs.
      ///         But we leave it here anyway, it might becomes useful again somewhen/somewhy.
      /// </remarks>
      /// <param name="containerControl">...</param>
      /// <returns>The wanted control</returns>
      protected Control GetActiveControl(Control containerControl)             // [mark 20130810°1602`15 'debug menu items gray']
      {
         object oDbg1 = containerControl;
         object oDbg2 = containerControl as ContainerControl;

         Control ctrlRet = containerControl;

         // See whether the given control is a container, if yes pick the active control inside it
         if ((containerControl != null) && ((containerControl as ContainerControl) != null))
         {
            return GetActiveControl(((ContainerControl)containerControl).ActiveControl);
         }

         return ctrlRet;
      }

      /// <summary>This eventhandler processes the ToolStripMenuItems 'Edit' item click event</summary>
      /// <remarks>id : 20130604°0424</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miEdit_Popup(object sender, EventArgs e)
      {
         EnableSubMenus();
      }

      /// <summary>This method maintains the availablity of the ToolStripMenuItems 'Edit' item submenu items</summary>
      /// <remarks>
      /// id : 20130604°0425
      /// note : The complete list of the 'Edit' submenu ToolStripMenuItems is:
      ///         miCopy, miCopyWithHeaders, miCut, miPaste, miSelectAll, miUndo.
      /// </remarks>
      private void EnableSubMenus()
      {
         bool canCopy = false, canCopyWithHeaders = false, canCut = false, canPaste = false, canUndo = false;

         System.Diagnostics.Debug.WriteLine("Edit submenu popup");

         Control ctrl = GetActiveControl();                                    // Debug menu items gray [mark 20130810°1602`17]
         if (ctrl != null)
         {
            CanEdit ( ctrl
                     , ref canCopy
                      , ref canCopyWithHeaders
                       , ref canCut
                        , ref canPaste
                         , ref canUndo
                          );
         }

         if (miCopy != null)
         {
            miCopy.Enabled = canCopy;
         }

         if (miCopyWithHeaders != null)
         {
            miCopyWithHeaders.Enabled = canCopyWithHeaders;
         }

         if (miCut != null)
         {
            miCut.Enabled = canCut;
         }

         if (miPaste != null)
         {
            miPaste.Enabled = canPaste;
         }

         if (miUndo != null)
         {
            miUndo.Enabled = canUndo;
         }

         // [added 20130810°1631]
         // Note : Now the menu item is active but still does not work.
         if (miSelectAll != null)
         {
            miSelectAll.Enabled = canCopy; // quick'n'dirty
         }
      }

      /// <summary>This method delivers the 'can' flags for the given control</summary>
      /// <remarks>id : 20130604°0426</remarks>
      /// <param name="c">The control for which to retriev the 'can' flag</param>
      /// <param name="canUndo">The wanted 'can Undo' flag</param>
      /// <param name="canCopy">The wanted 'can Copy' flag</param>
      /// <param name="canCopyWithHeaders">The wanted 'can CopyWithHeaders' flag</param>
      /// <param name="canCut">The wanted 'can Cut' flag</param>
      /// <param name="canPaste">The wanted 'can Paste' flag</param>
      protected void CanEdit ( Control c                                       // Debug menu items gray [mark 20130810°1602`18]
                              , ref bool canCopy
                               , ref bool canCopyWithHeaders
                                , ref bool canCut
                                 , ref bool canPaste
                                  , ref bool canUndo
                                   )
      {
         if (c is TextBoxBase)
         {
            TextBoxBase t = (TextBoxBase) c;
            canCopy = t.SelectionLength > 0;
            canCopyWithHeaders = false;
            canCut = t.SelectionLength > 0 && !t.ReadOnly;
            IDataObject iData = Clipboard.GetDataObject();
            canPaste = !t.ReadOnly && iData.GetDataPresent(DataFormats.Text); ;
            canUndo = t.CanUndo;
         }
         else if (c is DataGridView)
         {
            DataGridView dgv = (DataGridView) c;
            canCopy = dgv.RowCount > 0;
            canCopyWithHeaders = canCopy;
            canCut = false;
            canPaste = false;
            canUndo = false;
         }

         // [seq 20130810°1623] against issue 20130624°1011 'shortcut key fail'
         else if (c is ComboBox)
         {
            ComboBox cb = (ComboBox) c;
            canCopy = true;
            canCopyWithHeaders = false;
            canCut = true;
            canPaste = true;
            canUndo = true;
         }

         // proforma
         else { }
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Copy'</summary>
      /// <remarks>id : 20130604°0428</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miCopy_Click(object sender, EventArgs e)
      {
         Copy();
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Copy With Headers'</summary>
      /// <remarks>id : 20130604°0429</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miCopyWithHeaders_Click(object sender, EventArgs e)
      {
         CopyWithHeaders();
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Cut'</summary>
      /// <remarks>id : 20130604°0430</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miCut_Click(object sender, EventArgs e)
      {
         Cut();
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Paste'</summary>
      /// <remarks>id : 20130604°0431</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miPaste_Click(object sender, EventArgs e)
      {
         Paste();
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Select All'</summary>
      /// <remarks>id : 20130604°0432</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miSelectAll_Click(object sender, EventArgs e)
      {
         SelectAll();
      }

      /// <summary>This eventhandler processes the ToolStripMenuItem 'Undo'</summary>
      /// <remarks>id : 20130604°0427</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      protected void miUndo_Click(object sender, EventArgs e)
      {
         Undo();
      }

      // Experimentally outcommented 20130810°1121
      /*
      #region Component Designer generated code

      /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor</summary>
      /// <remarks>
      /// id : 20130604°0433
      /// note : This method seems called from nowhere. Is it a relic? Can it be deleted? (20130619°0453)
      /// </remarks>
      private void InitializeComponent()
      {
      }

      #endregion Component Designer generated code
      */

   }
}
