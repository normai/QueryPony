#region Fileinfo
// file        : 20130725°1411 (20130604°0531) /QueryPony/QueryPonyGui/Gui/MainForm_Menu.cs
// summary     : This file stores a parts of class 'MainForm' to constitute the Main Form.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// history     : (20130725°1411) This about 600 lines outsourced from MainForm.cs.
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyLib;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QueryPonyGui
{

   /// <summary>This class constitutes the Main Form</summary>
   /// <remarks>id : 20130604°0532</remarks>
   public partial class MainForm
   {

      /// <summary>This method enables/disables some non-edit toolbar or menu items</summary>
      /// <remarks>
      /// id : 20130812°1401
      /// note : The original EnableControls() does it's job only if a QueryForm is focussed.
      /// </remarks>
      public void EnableControlsOthers()
      {
         queryOptionsToolStripMenuItem.Enabled = Properties.Settings.Default.DeveloperMode;
      }

      /// <summary>This method enables/disables the toolbar and menu items</summary>
      /// <remarks>id : 20130604°0643</remarks>
      private void EnableControls()                                            // [mark 20130810°1602`11 'debug menu items gray']
      {
         // retrieve QueryForm handle or leave it null
         IQueryForm qf = null;
         bool active = IsChildActive();
         if (active)
         {
            qf = GetQueryChild();
         }

         // Empirical try against issue 20130809°1521 with Settings Form [seq 20130809°1522]
         // Note : If q is null, then it is not a QueryForm.
         if (qf == null)
         {
            if (Glb.Debag.Execute_No)
            {
               string s = "Tab selection issue 20130809°1521 (o.k.)";
               outputStatusLine(s);
            }
            return;
         }

         // Try envelope against bug 20130729°1543
         try
         {
            openToolStripMenuItem.Enabled = openToolStripButton.Enabled
                                           = saveResultsAsToolStripMenuItem.Enabled
                                            = (active && (qf.RunState == DbClient.RunStates.Idle)) // explicitly indicate precedence [line 20130809°1531]
                                             ;
         }
         catch (Exception ex)
         {
            string s = "POSSIBLY ORPHANED PROCESS LEFT BEHIND (bug 20130729°154312 with tabs)"
                      + " " + "Exception = \"" + ex.Message + "\""
                       ;
            outputStatusLine(s);
            return;
         }

         settingsToolStripMenuItem.Enabled = newToolStripButton.Enabled
                                            = saveToolStripMenuItem.Enabled
                                             = saveToolStripButton.Enabled
                                              = saveAsToolStripMenuItem.Enabled
                                               = active
                                                ;

         settingsToolStripMenuItem.Enabled = true;

         // [seq 20130725°1511] experiment with the Close Menu Item .. to be continued
         // original lines:
         // //disconnectToolStripMenuItem.Enabled = (active && (q.RunState != DbClient.RunStates.Cancelling));
         // new experimental lines:
         // enable it if a Connection Tab is selected
         // The search for a certain tab occurres also e.g. in sequence 20130725°1423
         disconnectToolStripMenuItem.Enabled = false;
         if (this.IsChildActive())
         {
            disconnectToolStripMenuItem.Enabled = true; // (active && (q.RunState != DbClient.RunStates.Cancelling));
         }

         //miQueryOptions.Enabled = (active && q.RunState == DbClient.RunStates.Idle);

         executeToolStripMenuItem.Enabled = (active && (qf.RunState == DbClient.RunStates.Idle));

         cancelExecutingQueryToolStripMenuItem.Enabled = (active && (qf.RunState == DbClient.RunStates.Running));

         resultsInTextToolStripMenuItem.Enabled = resultsInGridToolStripMenuItem.Enabled
                                                 = active
                                                  ;

         resultsInTextToolStripMenuItem.Checked = (active && qf.ResultsInText); // = tbResultsText.Pushed
         resultsInGridToolStripMenuItem.Checked = (active && !qf.ResultsInText);

         showNullValuesToolStripMenuItem.Enabled = active;
         showNullValuesToolStripMenuItem.Checked = (active && qf.GridShowNulls);

         hideNullValuesToolStripMenuItem.Enabled = active;
         hideNullValuesToolStripMenuItem.Checked = (active && !qf.GridShowNulls);

         //miNextPane.Enabled = miPrevPane.Enabled = active;

         //miHideResults.Enabled = tbHideResults.Enabled = active;
         hideBrowserToolStripMenuItem.Enabled = (active
                                                 && (qf.Browser != null)
                                                  && (qf.RunState == DbClient.RunStates.Idle)
                                                   ); // = tbHideBrowser.Enabled =

         //miHideResults.Checked = tbHideResults.Pushed = (active && q.HideResults);
         hideBrowserToolStripMenuItem.Checked = (active && qf.HideBrowser); // = tbHideBrowser.Pushed =

      }

      #region Menu and Toolbar Button Events

      /// <summary>This eventhandler processes the Main Menu 'File/Connect' Item click</summary>
      /// <remarks>id : 20130604°0540</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void connectToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoConnect_POSSIBLY_REVIVE();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/Close' Item click</summary>
      /// <remarks>id : 20130604°0541</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoDisconnect();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/Execute' Item (F5) click</summary>
      /// <remarks>id : 20130604°0542</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void executeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoExecuteQuery();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'Execute' Icon (F5) click</summary>
      /// <remarks>id : 20130604°0543</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void ExecuteToolStripButton_Click(object sender, EventArgs e)
      {
         DoExecuteQuery();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/CancelExecuting' Item click</summary>
      /// <remarks>id : 20130604°0544</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void cancelExecutingQueryToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoCancel();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'CancelExecuting' Icon click</summary>
      /// <remarks>id : 20130604°0545</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void cancelExecutingQueryToolStripButton_Click(object sender, EventArgs e)
      {
         DoCancel();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'New' (Ctrl+N) Icon click</summary>
      /// <remarks>id : 20130604°0546</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void newToolStripButton_Click(object sender, EventArgs e)
      {
         DoNew();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/New' (Ctrl+N) Item click</summary>
      /// <remarks>id : 20130604°0547</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void newToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoNew();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/ResultsInText' Item click</summary>
      /// <remarks>id : 20130604°0548</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void resultsInTextToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoResultsInText();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'ResultsInText' Icon click</summary>
      /// <remarks>id : 20130604°0549</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void resultsInTextToolStripButton_Click(object sender, EventArgs e)
      {
         DoResultsInText();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/ResultsInGrid' Item click</summary>
      /// <remarks>id : 20130604°0550</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void resultsInGridToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoResultsInGrid();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'ResultsInGrid' Icon click</summary>
      /// <remarks>id : 20130604°0551</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void resultsInGridtoolStripButton_Click(object sender, EventArgs e)
      {
         DoResultsInGrid();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/ShowNullValues' Item click</summary>
      /// <remarks>id : 20130604°0552</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void showNullValuesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoShowNullValues();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/HideNullValues' Item click</summary>
      /// <remarks>id : 20130604°0553</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void hideNullValuesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoHideNullValues();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/Exit' Item click</summary>
      /// <remarks>id : 20130604°0554</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Close();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/Open' Item click</summary>
      /// <remarks>id : 20130604°0555</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void openToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoOpen();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'Open' Icon click</summary>
      /// <remarks>id : 20130604°0556</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void openToolStripButton_Click(object sender, EventArgs e)
      {
         DoOpen();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/Save' (Ctrl+S) Item click</summary>
      /// <remarks>id : 20130604°0557</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void saveToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoSave();
      }

      /// <summary>This eventhandler processes the Main Toolbar 'Save' Icon click</summary>
      /// <remarks>id : 20130604°0558</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void saveToolStripButton_Click(object sender, EventArgs e)
      {
         DoSave();
      }

      /// <summary>This eventhandler processes the Main Menu 'Window/HideObjectBrowser' Item click</summary>
      /// <remarks>id : 20130604°0559</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void hideBrowserToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoHideShowBrowser();
      }

      /// <summary>This eventhandler processes the Main Menu 'Query/QueryOptions' Item click</summary>
      /// <remarks>id : 20130604°0602</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void queryOptionsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (IsChildActive())
         {
            IQueryForm iqf = GetQueryChild();
            iqf.ShowQueryOptions();                                            // key location with issue 20130716°1121
         }
      }

      /// <summary>This eventhandler processes the Main Menu 'File/SaveAs' Item click</summary>
      /// <remarks>id : 20130604°0603</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DoSaveAs();
      }

      /// <summary>This eventhandler processes the Main Menu 'File/SaveResultsAs' Item click</summary>
      /// <remarks>id : 20130604°0604</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void saveResultsAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (IsChildActive()) { GetQueryChild().SaveResults(); }
      }

      /// <summary>This eventhandler processes the Main Menu 'Edit/Find' (Ctrl-F) Item click</summary>
      /// <remarks>id : 20130604°0605</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void findToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (IsChildActive()) { GetQueryChild().ShowFind(); }
      }

      /// <summary>This eventhandler processes the Main Menu 'Edit/FindNext' (F3) Item click</summary>
      /// <remarks>id : 20130604°0606</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (IsChildActive()) { GetQueryChild().FindNext(); }
      }

      /// <summary>This eventhandler processes the Main Menu 'Help/About' Item click</summary>
      /// <remarks>id : 20130604°0607</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         new AboutForm().ShowDialog();
      }

      /// <summary>This eventhandler processes the Menu Menu 'Help/ViewUserManualInBrowser' Item Click event</summary>
      /// <remarks>id : 20130707°1811</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void viewHelpInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
      {
         _MenuItems.main_help_viewDocInBrowser();
      }

      #endregion Menu and Toolbar Button Events

      #region Menu and Toolbar Button Implementations

      /// <summary>This method executes the Main Menu 'Query/Execute' Item job</summary>
      /// <remarks>id : 20130604°0608</remarks>
      private void DoExecuteQuery()
      {
         // possible breakpoint (20130619°0451)
         if (Glb.Debag.Debug_MainMenu_DoExecuteQuery && System.Diagnostics.Debugger.IsAttached)
         {
            System.Diagnostics.Debugger.Break();
         }

         // run the given SQL command
         if (IsChildActive())
         {
            GetQueryChild().Execute();
         }
      }

      /// <summary>This method executes the Main Menu 'File/New' Item job</summary>
      /// <remarks>id : 20130604°0609</remarks>
      private void DoNew()
      {
         if (IsChildActive())
         {
            // change the cursor to an hourglass while we're doing this,
            //  in case establishing the new connection takes some time
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            IQueryForm newQF = GetQueryChild().Clone();

            // could be null if new connection failed
            if (newQF != null)
            {
               ((Form)newQF).MdiParent = this;
               newQF.PropertyChanged += new EventHandler<EventArgs>(qf_PropertyChanged);
               newQF.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(qf_MRUFileAdded);
               ((Form)newQF).Show();
            }
            Cursor = oldCursor;
         }
         else
         {
            DoConnect_POSSIBLY_REVIVE();
         }
      }

      //-------------------------------------------------------
      // the methods below may are not soo typical for file MainMenu.cs (20130725°1413)
      //-------------------------------------------------------

      /// <summary>This eventhandler ... (seems nowhere attached)</summary>
      /// <remarks>id : 20130604°0610</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void qf_(object sender, MRUFileAddedEventArgs e)
      {
         string s = "The method or operation is not implemented." + " " + "[20130604°0610]";
         throw new Exception(s);
      }

      /// <summary>
      /// This method performs a parameterized auto-connect on program
      ///  start (it seems only be called from the MainForm constructor).
      ///  </summary>
      /// <remarks>
      /// id : 20130604°0611
      /// note : This method seems to be called only if the program is started with arguments,
      ///    but not if a connection is initiated via the 'Connect' button. [note 20130617°1542]
      /// </remarks>
      /// <param name="conSettings">The ConnSettings for which to create a QueryForm</param>
      /// <returns>The newly created QueryForm</returns>
      private IQueryForm DoConnect(ConnSettingsGui conSettings)
      {
         // fix single-file-deployment collission [seq 20130706°1054]
         ConnSettingsLib csLib1 = null;
         csLib1 = ConnSettingsGui.convertSettingsGuiToLib(conSettings);

         // collission with single-file-deployment [line 20130706°1054`03]
         DbClient client = DbClientFactory.GetDbClient(csLib1);

         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;

         SplashConnecting c = new SplashConnecting();
         c.Show();
         c.Refresh();

         bool success = client.Connect();
         c.Close();
         Cursor = oldCursor;

         // user notification
         if (! success)
         {
            string s = "Unable to connect: " + client.ErrorMessage + " " + "[Error 20130717°1244]";
            MessageBox.Show(s, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Error);
            client.Dispose();
            return null;
         }

         // find index (line 20130620°1135) looks like a good line with issue 20130828°1531
         int settingIndex = ServerList_.IndexOf(client.ConnSettings.Key);

         // [seqence 20130620°1134 (20130620°1121)]
         // Need some kind of translator from 'ConnectionSettings' to 'ConnectionSettings_DUMMY'
         // Seems a key element while refactor 20130620°0211 'Split Settings GUI and Lib'.
         ConnSettingsGui csGui = ConnSettingsGui.convertSettingsLibToGui(client.ConnSettings);
         ConnSettingsLib csLib = client.ConnSettings; // ?

         // put item on the global connection list
         if (settingIndex >= 0)
         {
            ServerList_.Items[settingIndex] = csGui;
         }
         else
         {
            ServerList_.Add(csGui);
         }

         SaveServerList();

         QueryForm qf = new QueryForm(client, true);
         qf.MdiParent = this;
         // This is so that we can update the toolbar and menu as the state of the QueryForm changes.
         qf.PropertyChanged += new EventHandler<EventArgs>(qf_PropertyChanged);
         qf.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(qf_MRUFileAdded);
         qf.Show();

         return qf;
      }

      /// <summary>This method opens a Connect Form as modal dialog</summary>
      /// <remarks>
      /// id : 20130604°0612
      /// issue : (1) Since we have the Connect Form on a tabpage now, this method
      ///    is no more really necessary. But it were a nice gimmick to keep this
      ///    alternative available and working. (2) If this alternative is working,
      ///    it were more intersting as a non-modal form, than as a modal one.
      ///    (3) For the QueryForm, such alternative were much more interesting
      ///    indeed. (4) Befor the Connect Form can be revived in Dialog-Form style,
      ///    it's MDI features had to be adjusted. [issue 20130725°1421]
      /// </remarks>
      /// <returns>The opened Connect Form object (but that is evaluated by none of the callers) or null</returns>
      private IQueryForm DoConnect_POSSIBLY_REVIVE()
      {
         // [condition 20130725°1422'02]
         if (IOBus.Gb.Debag.Shutdown_Archived)
         {
            // almost original code, possibly to be revived

            ConnectForm cf = new ConnectForm(_furnishNewConnection, _outputToStatusLine);

            if (cf.ShowDialog(this) == DialogResult.OK)
            {
               int settingIndex = ServerList_.IndexOf(cf.DbClient.ConnSettings.Key);

               // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
               // [seqence 20130620°1132 (20130620°1121)]
               ConnSettingsGui csGui = ConnSettingsGui.convertSettingsLibToGui(cf.DbClient.ConnSettings);
               // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

               if (settingIndex >= 0)
               {
                  ServerList_.Items[settingIndex] = (csGui);
               }
               else
               {
                  ServerList_.Add(csGui);
               }

               SaveServerList();

               QueryForm qf = new QueryForm(cf.DbClient, true);
               qf.MdiParent = this;
               // This is so that we can update the toolbar and menu as the state of the QueryForm changes.
               qf.PropertyChanged += new EventHandler<EventArgs>(qf_PropertyChanged);
               qf.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(qf_MRUFileAdded);
               qf.WindowState = FormWindowState.Maximized;
               qf.Show();
               return qf;
            }

            return null;

         }
         else
         {
            //=================================================
            // alternative 'connect-surrogate' gimmick feature (20130725°1422)
            //=================================================

            // just go to the Connect Tab [seq 20130725°1423]
            // note : This algorithm presumes the Connect Tab exists, otherwise it will crash. [note 20130725°1424]
            TabPage tbBefore = MainForm._maintabcontrol.SelectedTab;
            foreach (TabPage tabpage in MainForm._maintabcontrol.TabPages)
            {
               if (tabpage.Text == "Connect")
               {
                  _tabpageFound = tabpage;
                  break;
               }
            }
            _maintabcontrol.SelectedTab = _tabpageFound;

            // blink only if the Connect Tab was already selected (just to signal some
            //  effect of the menu item clicking, since otherwise nothing happens)
            if (_tabpageFound == tbBefore)
            {
               // connect-surrogate feature [seq 20130725°1438]
               // note : The eventhandler attachment line
               //  '_blinkTimer.Tick += new System.EventHandler(dispatcherTimer_Tick);'
               //  must not be here but at a place where it is called only once.
               _colorFound = _tabpageFound.BackColor;
               _blinkTimer.Interval = new System.TimeSpan(0, 0, 0, 0, 123); // 234);
               _blinkTimer.Start();
            }

            return null;
         }
      }

      //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
      // The 'connect surrogate gimmick' feature (20130725°1431)
      // note : Actually, it's not just for fun, but for quick'n'dirty performing
      //    some reaction from the Main Menu 'Connect' Item, without disturbing
      //    the other code. And as well for the user to provide some correlation
      //    between the menu item and the Connect TabPage.

      /// <summary>This field stores a helper variable for the Connect Surrogate gimmick feature</summary>
      /// <remarks>id : 20130725°1434</remarks>
      private static TabPage _tabpageFound = null;

      /// <summary>This field stores a helper variable for the Connect Surrogate gimmick feature</summary>
      /// <remarks>id : 20130725°1435</remarks>
      private static System.Drawing.Color _colorFound;

      /// <summary>This field stores a helper variable for the Connect Surrogate gimmick feature</summary>
      /// <remarks>id : 20130725°1436</remarks>
      private static int _iBlinkCount = 0;

      /// <summary>This private property stores a timer</summary>
      /// <remarks>
      /// id : 20130725°1432 (20130609°1545)
      /// note : This requires a reference to WindowsBase.
      /// </remarks>
      private System.Windows.Threading.DispatcherTimer _blinkTimer = new System.Windows.Threading.DispatcherTimer();

      /// <summary>This eventhandler ... processes the timer ...</summary>
      /// <remarks>
      /// id : 20130725°1433 (20130609°1541)
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void dispatcherTimer_Tick(object sender, System.EventArgs e)
      {
         _iBlinkCount++;

         switch (_iBlinkCount)
         {
            case 1: _tabpageFound.BackColor = System.Drawing.Color.LimeGreen; break;
            case 2: _tabpageFound.BackColor = _colorFound; break;
            case 3: _tabpageFound.BackColor = System.Drawing.Color.LimeGreen; break;
            default :
               _tabpageFound.BackColor = _colorFound;
               _iBlinkCount = 0;
               _blinkTimer.Stop();
               break;
         }
      }
      //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

      /// <summary>This eventhandler processes the MRUFileAddedEvent event of this form</summary>
      /// <remarks>id : 20130604°0613</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void qf_MRUFileAdded(object sender, MRUFileAddedEventArgs e)
      {
         mruMenuManager1.Add(e.Filename);
         QueryPonyGui.Properties.Settings.Default.MRUFiles = mruMenuManager1.Filenames;
         QueryPonyGui.Properties.Settings.Default.Save();
      }

      /// <summary>This method closes one QueryForm</summary>
      /// <remarks>
      /// id : 20130604°0614
      /// note : Since the Settings Form is also using this method to remove itself, the
      ///    name 'DoDisconnect' is no more really correct, because only the Query Forms
      ///    use the disconnect sequence, the Settings Form uses only the tabpage-remove
      ///    sequence. [note 20130809°1526]
      /// callers : The QueryForms and Settings Form.
      /// </remarks>
      internal void DoDisconnect()
      {
         if (this.IsChildActive())
         {
            // make a handle for the QueryForm to close
            IQueryForm iqf = GetQueryChild();

            // retrieve the connection id of the QueryForm to close to find
            //  the corresponding treeview node [seq 20130729°1511]
            QueryForm qf = iqf as QueryForm;

            // make this method suited for the Settings Form as well (20130809°1525)
            if (qf != null)
            {
               ConnSettingsLib csTarget = qf.DbClient.ConnSettings;

               // close the QueryForm [seq 20130729°1544]
               // note : Remember bug 20130729°1543 'ArgumentOutOfRangeException when
               //    clicking tabs/nodes'. This line being before the treeview maintenance
               //    instead of behind it fixes it. Now only message 'QueryForm not closed
               //    (quirk 20130729°154313).' appears in the status textbox, but that seems
               //    no problem. This should be cleaned up after a while of observation.
               iqf.Close();

               //-----------------------------------------
               // remove the corresponding treeview database node and
               //  possibly also the server node [seq 20130729°1512]
               TreeNode tnTarget = MainTv.searchTreenodeByConnSettings(csTarget);
               TreeNode tnServer = tnTarget.Parent;

               // remove the database node
               tnTarget.Remove();

               // possibly remove the server node
               if (tnServer.Nodes.Count < 1)
               {
                  tnServer.Remove();
               }
               //-----------------------------------------

            }

            // remove the TabPage [seq 20130709°1421]
            // note : Sequence shifted here from method 20130704°1251
            //    QueryForm.cs::button_Queryform_Close_Click(). [note 20130725°1521]
            MainForm._maintabcontrol.SelectedTab.Dispose();

            // seq finally outcommented [log 20130818°1541]
            //  it seems to be from a time with a differend order of commands
            /*
            int iTabBefore = tabcontrolMain.SelectedIndex; // debug 20130729°154303

            // debug 20130729°154304 curiously, this index is different from above.
            // finding : Inside 'tnTarget.Remove();' somehow getQueryChild() is called,
            //    and there seems to happen some kind of unwanted manipulation.
            if (tabcontrolMain.SelectedIndex != iTabBefore)
            {
               string s = "QueryForm not closed (quirk 20130729°154313).";
               outputStatusLine(s);
               return;
            }
            */
         }
      }

      /// <summary>This method performs the Cancel action for the active QueryForm</summary>
      /// <remarks>id : 20130604°0615</remarks>
      private void DoCancel()
      {
         if (IsChildActive())
         {
            GetQueryChild().Cancel();
         }
      }

      /// <summary>This method switches the ResultsInText flag of the active QueryForm to 'true'</summary>
      /// <remarks>id : 20130604°0616</remarks>
      private void DoResultsInText()
      {
         // Changing the value of this property will automatically invoke the QueryForm's
         //  PropertyChanged event, which we've wired to EnableControls().
         if (IsChildActive())
         {
            GetQueryChild().ResultsInText = true;
         }
      }

      /// <summary>This method switches the ResultsInText flag of the active QueryForm to 'false'</summary>
      /// <remarks>id : 20130604°0617</remarks>
      private void DoResultsInGrid()
      {
         // Changing the value of this property will automatically invoke the QueryForm's
         //    PropertyChanged event, which we've wired to EnableControls().
         if (IsChildActive())
         {
            GetQueryChild().ResultsInText = false;
         }
      }

      /// <summary>This method switches the GridShowNulls flag of the active QueryForm to 'true'</summary>
      /// <remarks>id : 20130604°0618</remarks>
      private void DoShowNullValues()
      {
         if (IsChildActive())
         {
            GetQueryChild().GridShowNulls = true;
         }
      }

      /// <summary>This method switches the GridShowNulls flag of the active QueryForm to 'false'</summary>
      /// <remarks>id : 20130604°0619</remarks>
      private void DoHideNullValues()
      {
         if (IsChildActive())
         {
            GetQueryChild().GridShowNulls = false;
         }
      }

      /// <summary>This method opens the ... file for the active QueryForm</summary>
      /// <remarks>id : 20130604°0620</remarks>
      private void DoOpen()
      {
         if (IsChildActive())
         {
            GetQueryChild().Open();
         }
      }

      /// <summary>This method saves the ... file for the active QueryForm</summary>
      /// <remarks>id : 20130604°0621</remarks>
      private void DoSave()
      {
         if (IsChildActive())
         {
            GetQueryChild().Save();
         }
      }

      /// <summary>This method saves the ... file for the active QueryForm via the Save-As dialog</summary>
      /// <remarks>id : 20130604°0622</remarks>
      private void DoSaveAs()
      {
         if (IsChildActive())
         {
            GetQueryChild().SaveAs();
         }
      }

      /// <summary>This method toggles the visibility of the dedicated database browser treeview</summary>
      /// <remarks>id : 20130604°0623</remarks>
      private void DoHideShowBrowser()
      {
         if (IsChildActive())
         {
            GetQueryChild().HideBrowser = !GetQueryChild().HideBrowser;
         }
      }

      #endregion Menu and Toolbar Button Implementations
   }
}
