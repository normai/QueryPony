#region Fileinfo
// file        : 20130604°0051 /QueryPony/QueryPonyGui/ConnectForm.cs
// summary     : Class 'ConnectForm' constitutes the Connect Form
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyLib;
using System;
using System.Collections.Generic;                                              // List<>
using System.Drawing;                                                          // Graphics (experimental)
using System.IO;
using System.Windows.Forms;

namespace QueryPonyGui
{
   /// <summary>This class constitutes the Connect Form</summary>
   /// <remarks>id : 20130604°0052</remarks>
   public partial class ConnectForm : Form
   {
      /// <summary>This method adds one item to the Connection Combobox (experimental)</summary>
      /// <remarks>
      /// id : 20130810°1134
      /// callers : So far only method 20130604°0635 MainForm.cs::ImportServerlist().
      /// todo : Possibly use this method also from other 'combobox_Connection.Items.Add()' places.
      /// </remarks>
      /// <param name="csGui">The connection settings to add</param>
      /// <returns>Success flag, true if inserted, false if not inserted</returns>
      public bool ComboboxConnectionAddItem(ConnSettingsGui csGui)
      {
         // Don't store duplicate connection settings
         // note : Actually, if the ConnectionsCombobox were strictly synchronized with
         //    the ServerList, such conditions were not necessary, because the ServerList
         //    would handle this.
         foreach (ConnSettingsGui cs in combobox_Connection.Items)
         {
            if (cs.ToString() == csGui.ToString())
            {
               return false;
            }
         }

         // The actual job
         combobox_Connection.Items.Add(csGui);

         // Some cosmetics
         combobox_Connection.SelectedItem = csGui;                             // Does not to work always

         return true;
      }

      /// <summary>This field stores the DbClient ... (for this ConnectForm or for what exactly?)</summary>
      /// <remarks>Ident 20130604°0053</remarks>
      private DbClient _client = null;

      /// <summary>This field stores the actually displayed settings. Those are used if the Connect Button is pressed</summary>
      /// <remarks>id : 20130604°0054</remarks>
      private ConnSettingsGui _conSettings = new ConnSettingsGui();

      /// <summary>This field stores a flag telling the main connection combobox's SelectionChanged event not to adjust the tabcontrol</summary>
      /// <remarks>id : 20130622°1012</remarks>
      private bool _tabpageHasChanged_AndCombobox_IsAlreadyAdjusted = false;

      /// <summary>
      /// This field stores a flag telling the TabPage, the change was initiated
      ///  by the ComboBox, it shall not set the flag for the ComboBox. Without
      ///  this helper flag, every second selection would fail.
      /// </summary>
      /// <remarks>id : 20130623°1021</remarks>
      private bool _tabpageChange_WasInitiated_ByCombobox = false;

      /// <summary>This field stores a flag telling whether the Connection Combobox SelectionChanged event is called the very first time or not</summary>
      /// <remarks>id : 20130623°1553</remarks>
      private bool _connectionCombobox_SelectionChanged_FirstCall_HELPERFLAG = true;

      /// <summary>This field stores the original filling of a tabpage for later use when creating a new connection</summary>
      /// <remarks>id : 20130623°1554</remarks>
      private string _tabpageSelected_OriginalFilling_HELPERMEMO = "";

      /// <summary>This field stores a flag to signal connection deletion as reason for tabpage selection change</summary>
      /// <remarks>id : 20130624°0932</remarks>
      private bool _tabpageSelectionChange_CausedByConnectionDeletion = false;

      /// <summary>This field stores the actually selected tabpage to be still available after the selection has changed</summary>
      /// <remarks>id : 20130622°1012</remarks>
      private TabPage _previouslySelectedTabpage = null;

      /// <summary>This field stores the actually selected connection index to be available for the Settings on progam exit via 'static'</summary>
      /// <remarks>id : 20130622°1131</remarks>
      internal static int SelectedConnectionIndex = -1;

      /// <summary>This delegate stores the method to display a new connection ...</summary>
      /// <remarks>id : 20130618°0422</remarks>
      private Delegate _furnishNewConnection = null;

      /// <summary>This field stores the delegate to output text to the status display</summary>
      /// <remarks>
      /// id : 20130716°0631
      /// note : Created to solve issue 20130716°0622. It enables asynchronous access to the Status TextBox.
      /// </remarks>
      /// <param name="sText">The text to output</param>
      private Delegate _outputStatusLino = null;

      /// <summary>This constructor creates a Connect Form while taking a delegate for opening new connections</summary>
      /// <remarks>id : 20130618°0421</remarks>
      /// <param name="FunishNewConnection">The delegate to establish a connection display after the Connect Button is pressed.</param>
      public ConnectForm(Delegate FunishNewConnection, Delegate OutputStatusLino) : this()
      {
         _furnishNewConnection = FunishNewConnection;
         _outputStatusLino = OutputStatusLino;
      }

      /// <summary>This constructor creates a Connect Form</summary>
      /// <remarks>id : 20130604°0055</remarks>
      private ConnectForm()
      {
         InitializeComponent();

         label_SignPost.Text = MainForm._signPost;
         label_SignPost.ForeColor = MainForm._signPostColor;

         // Legacy sequence
         // Todo : Possibly shift or remove this to a better suited place. [todo 20130623°0832]
         if (QueryPonyGui.Properties.Settings.Default.MssqlServerAuthenticationDefault)
         {
            radiobutton_Mssql_Trusted.Checked = false;
            radiobutton_Mssql_Untrusted.Checked = true;
         }
         else
         {
            radiobutton_Mssql_Trusted.Checked = true;
            radiobutton_Mssql_Untrusted.Checked = false;
         }

         radiobutton_Oracle_Untrusted.Checked = true;

         if (IOBus.Gb.Debag.Shutdown_Temporarily)                              // Permanently? [seq 20130623°1531]
         {
            this.ActiveControl = textbox_Mssql_ServerAddress;                  // Is this a good idea? [todo 20130620°1612]
         }

         // Carry out the initial filling of the controls
         SettingsLoad();

         // Retrieve last used connection
         int iLastUsedConn = QueryPonyGui.Properties.Settings.Default.LastSelectedConnection;

         // Possibly select last used connection
         if (combobox_Connection.Items.Count < 1)
         {
            // ComboBox empty, no index to select
         }
         else if (combobox_Connection.Items.Count <= iLastUsedConn)
         {
            // Last used connection not applicable
            combobox_Connection.SelectedIndex = 0;
         }
         else
         {
            // Normal case
            combobox_Connection.SelectedIndex = iLastUsedConn;
         }

         // Avoid null reference exception on very first TabControl SelectedIndexChanged event [line 20130623°1112]
         _previouslySelectedTabpage = tabcontrol_ServerTypes.SelectedTab;

         // Maintain Connect Form title (20130724°1112)
         maintainFormTitleText();

         // Maintain main treeview (20130701°1114)
         // Todo : Not sure yet whether this is the final location to put the treeview maintenance [todo 20130701°1114`02]
         bool b = maintainTreeviewMain();
      }

      /// <summary>
      /// This property gets the DbClient of the Connect Form.
      ///  The database client object which is used to talk to the database server.
      ///  This should be queried after the form is closed (following a DialogResult.OK).
      /// </summary>
      /// <remarks>
      /// id : 20130604°0056
      /// todo : Is this superfluous? It is used in method 20130618°0411 button_Connect_Click()
      ///    only. Why should it be a class-wide property? It impedes the outsourcing of code
      ///    from button_Connect_Click() to 20130828°1521 establishConnection(). [todo 20130828°1523]
      /// </remarks>
      internal DbClient DbClient
      {
         get { return _client; }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0057</remarks>
      private void radiobutton_Mssql_Trusted_CheckedChanged(object sender, EventArgs e)
      {
         if (radiobutton_Mssql_Trusted.Checked)
         {
            textbox_Mssql_LoginName.Enabled = false;
            textbox_Mssql_Password.Enabled = false;
         }
         else
         {
            textbox_Mssql_LoginName.Enabled = true;
            textbox_Mssql_Password.Enabled = true;
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0058</remarks>
      private void radiobutton_Oracle_Trusted_CheckedChanged(object sender, EventArgs e)
      {
         if (radiobutton_Oracle_Trusted.Checked)
         {
            textbox_Oracle_LoginName.Enabled = false;
            textbox_Oracle_Password.Enabled = false;
         }
         else
         {
            textbox_Oracle_LoginName.Enabled = true;
            textbox_Oracle_Password.Enabled = true;
         }
      }

      /// <summary>This eventhandler processes the Connect Buttons Click event</summary>
      /// <remarks>
      /// id : 20130618°0411 [after 20130604°0059]
      /// note : Here was issue 20130724°0911 'connect wrong database' solved 20130724°0912
      /// </remarks>
      private void button_Connect_Click(object sender, EventArgs e)
      {
         string sErr = "";

         // [seq 20130704°1242]
         if (Glb.Debag.Debug_ConnectForm_ButtonConnect && System.Diagnostics.Debugger.IsAttached)
         {
            System.Diagnostics.Debugger.Break();
         }

         // See if the left tabpage displays a new connection [seq 20130623°1411]
         TabPage tabPageSelected = tabcontrol_ServerTypes.SelectedTab;
         if (! syncConnectionComboboxWith_PreviouslySelectedTabpage(tabPageSelected))
         {
            return;
         }

         // Save the displayed settings
         SettingsSave();

         // Determine selected connection settings
         // Note : ScreenToSettings() has written just all tabpages back, ignorant about which
         //    is the selected one. Here a humble workaround, possibly to be much refactored.
         TabPage tb = tabcontrol_ServerTypes.SelectedTab;

         // (.) Update the active connection settings [seq 20130701°1231]
         // (.1) Identify ServerList entry
         string sConnId = getConnectionIdFromTabpage(tb);
         int iNdx = MainForm.ServerList_.IndexOfById(sConnId);

         // (.2) Paranoia about IndexOutOfRangeException
         if (iNdx < 1)
         {
            sErr = "Program flow error: Something seems wrong with the connection settings. [error 20130725°0913]";
            MessageBox.Show(sErr, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
         }

         // (.3) Finally do the update
         _conSettings = MainForm.ServerList_.Items[iNdx];

         // Experimental [line 20130828°1513]
         _conSettings.Status = ConnSettingsGui.ConnStatus.Unvalidated;

         // Read password [seq 20130717°1251]
         // Note : The password must be read from the live tabpage, not from the ConnList.
         // Todo : Finally the SettingsSave() mechanism has to be refactored to handle
         //    the option 'save password' versus 'do not save password' cleanly.
         if      (tb == tabpage_Couch)  { _conSettings.Password = textbox_Couch_Password.Text  ; }
         else if (tb == tabpage_Mssql)  { _conSettings.Password = textbox_Mssql_Password.Text  ; }
         else if (tb == tabpage_Mysql)  { _conSettings.Password = textbox_Mysql_Password.Text  ; }
         else if (tb == tabpage_Oracle) { _conSettings.Password = textbox_Oracle_Password.Text ; }
         else if (tb == tabpage_Pgsql)  { _conSettings.Password = textbox_Pgsql_Password.Text  ; }
         else if (tb == tabpage_Sqlite) { _conSettings.Password = textbox_Sqlite_Password.Text ; }
         else { }

         // Repair possible connection type 'no type' [seq 20130624°1041]
         // Note : This is an empirical fix for _conSettings being of 'no-type',
         //    appearing in the situation that the combobox is empty, and the user
         //    fills values into a tabpage and presses the Connect Button.
         if (_conSettings.Type == ConnSettingsLib.ConnectionType.NoType)
         {
            _conSettings = combobox_Connection.SelectedItem as ConnSettingsGui;
         }

         /*
         Todo 20130828°1522 'Combine 4 connect dbClient seqences into one method'
         Matter : The code from around here to method end is used for method
                      20130828°1521 establishConnection().
         Do : Try to outsource this sequence here, and use that method instead.
         Findings : This are 4 places marked 'Compare seqence 20130713°0922/20130618°0351'
            • MainForm.cs seq 20130618°0351`01
            • MainForm.cs seq 20130618°0351`02
            • ConnecForm.cs line 20130818°1617 'Cretate the DbClient'
            • ConnecForm.cs 20130713°0922 'Connect dbClient'
         Location : Method 20130618°0411 button_Connect_Click seq 20130620°1613
         Status : Open
         ܀
         */

         // Create the engine connection settings [seq 20130620°1613]
         // See : Todo 20130828°1522 'Combine seqences into method'
         ConnSettingsLib csLib = ConnSettingsGui.convertSettingsGuiToLib(this._conSettings);

         // Paranoia [seq 20130620°1613]
         if (! DbClientFactory.ValidateSettings(csLib))
         {
            sErr = "Connection settings invalid: " + this._conSettings.ConnIdString(); // 'Oracle - XE'
            MainForm.outputStatusLine(sErr);
            return;
         }

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // Create the connection [seq 20130618°0351`02]
         // See todo 20130828°1522 'Combine 4 connect dbClient seqences into one method'

         // Retrieve the client object
         _client = DbClientFactory.GetDbClient(csLib);                         // Breakpoint 20140126°1611`12

         // Waiting phase start
         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;
         SplashConnecting c = new SplashConnecting();
         c.Show(this);
         c.Refresh();

         // Make the connection
         bool bSuccess = _client.Connect();                                    // Breakpoint 20140126°1621

         // Waiting phase end
         c.Close();
         Cursor = oldCursor;

         // All right?
         if (! bSuccess)
         {
            string s = "Unable to connect: " + _client.ErrorMessage + " " + "[Error 20130717°1243]";

            // Different icons make different sounds [seq 20130725°0912]
            if (IOBus.Gb.Debag.Shutdown_Alternatively)
            {
               // The error icon makes an annoying bang sound
               MessageBox.Show(s, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               // The exclamation icon makes a more graceful bling sound
               MessageBox.Show(s, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            _client.Dispose();

            // Experimental [line 20130828°1515]
            _conSettings.Status = ConnSettingsGui.ConnStatus.Failed;

            return;
         }
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         // Preserve connected status for next program start [seq 20130828°1514]
         _conSettings.Status = ConnSettingsGui.ConnStatus.Connected;           // This seems not to propagate into the ServerList ...
         MainForm.ServerList_.Items[iNdx].Status = ConnSettingsGui.ConnStatus.Connected;  // ... and this is also not yet enough ...
         QueryPonyGui.Properties.Settings.Default.ServerList = MainForm.ServerList_;  // ... but brute force helps.
         QueryPonyGui.Properties.Settings.Default.Save();

         /*
         note 20130618°0411`02 'Care about form opening via delegate'
         Text : Formerly, the caller cared about opening the connection form. Now,
            with the ConnectForm on a tab, we have to care about opening the form
            independently here via below called delegate.
            //DialogResult = DialogResult.OK; // no more useful with the Connect-Form-on-Tab
            //DialogResult DialogResultELIMINATE = DialogResult.OK; // no more useful with the Connect-Form-on-Tab
         Location : ConnectForm.cs seq 20130618°0411`03
         Status : ?
         */

         // Establish a GUI for the new connection [seq 20130618°0411`03] New style
         // Note : The delegate implementation finally being called is method 20130618°0412
         //    MainForm::furnishNewConnectionDelegateImplementation(). (Use 'Find' with the
         //    method id to navigate there, that is much easier than with 'Go To Definition'.)
         // See : note 20130618°0411`02 'Care about form opening via delegate'
         object[] aro = { _client, csLib };
         this.Invoke(this._furnishNewConnection, aro);

         return;
      }

      /// <summary>This method fills the tabpages with initial values</summary>
      /// <remarks>id : 20130604°0103</remarks>
      private void ConnectForm_Load(object sender, EventArgs e)
      {
         tabpage_Couch.Tag = ConnSettingsLib.ConnectionType.Couch;
         tabpage_Mssql.Tag = ConnSettingsLib.ConnectionType.Mssql;
         tabpage_Mysql.Tag = ConnSettingsLib.ConnectionType.Mysql;
         tabpage_Odbc.Tag = ConnSettingsLib.ConnectionType.Odbc;
         tabpage_Oledb.Tag = ConnSettingsLib.ConnectionType.OleDb;
         tabpage_Oracle.Tag = ConnSettingsLib.ConnectionType.Oracle;
         tabpage_Pgsql.Tag = ConnSettingsLib.ConnectionType.Pgsql;
         tabpage_Sqlite.Tag = ConnSettingsLib.ConnectionType.Sqlite;
      }

      /// <summary>This eventhandler processes the 'ODBC Load' button click</summary>
      /// <remarks>id : 20130604°0107</remarks>
      private void cmdLoadOdbc_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd;
         ofd = new System.Windows.Forms.OpenFileDialog();
         ofd.DefaultExt = "connectString";
         ofd.FileName = "ODBC";
         ofd.Filter = "Connection String|*.connectString|Text File|*.txt|All Files|*.*";

         if (ofd.ShowDialog() == DialogResult.OK)
         {
            try
            {
               string s;
               StreamReader r = null;
               try
               {
                  r = File.OpenText(ofd.FileName);
                  s =  r.ReadToEnd();
               }
               finally
               {
                  if (r != null)
                  {
                     r.Close();
                  }
               }
               if (s != null)
               {
                  textbox_Odbc_ConnectionString.Text = s;
                  button_Connect.Focus();
               }

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         }
      }

      /// <summary>This eventhandler processes the 'ODBC Save' button click</summary>
      /// <remarks>id : 20130604°0108</remarks>
      private void cmdSaveOdbc_Click(object sender, EventArgs e)
      {
         SaveFileDialog sfd;
         sfd = new System.Windows.Forms.SaveFileDialog();
         sfd.DefaultExt = "connectString";
         sfd.FileName = "ODBC";
         sfd.Filter = "Connection String|*.connectString|Text File|*.txt|All Files|*.*";

         if (sfd.ShowDialog() == DialogResult.OK)
         {
            StreamWriter w = null;
            try
            {
               try
               {
                  w = File.CreateText(sfd.FileName);
                  w.Write(textbox_Odbc_ConnectionString.Text);
                  button_Connect.Focus();
               }
               finally
               {
                  if (w != null)
                  {
                     w.Close();
                  }
               }
            }
            catch
            {
               MessageBox.Show("Unable to save connection string");
            }
         }
      }

      /// <summary>This eventhandler processes the 'OleDb Load' button click</summary>
      /// <remarks>id : 20130604°0109</remarks>
      private void cmdLoadOleDb_Click(object sender, EventArgs e)
      {
         OpenFileDialog openOleDbFileDialog;
         openOleDbFileDialog = new System.Windows.Forms.OpenFileDialog();
         openOleDbFileDialog.DefaultExt = "udl";
         openOleDbFileDialog.FileName = "OleDb";
         openOleDbFileDialog.Filter = "OleDB Connection String"
                                     + " (*.udl;*.connectString)|*.udl;"
                                      + "*.connectString|Text File(*.txt)|*.txt|All Files (*.*)|*.*"
                                       ;

         if (openOleDbFileDialog.ShowDialog() == DialogResult.OK)
         {
            try
            {
               string data = OleDbBrowser.GetConnectString(openOleDbFileDialog.FileName);
               if ((data != null) && (data != string.Empty))
               {
                  textbox_Oledb_ConnectionString.Text = data;
                  button_Connect.Focus();
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
         }
      }

      /// <summary>This eventhandler processes the 'OleDb Save' button click</summary>
      /// <remarks>id : 20130604°0110</remarks>
      private void cmdSaveOleDb_Click(object sender, EventArgs e)
      {
         SaveFileDialog saveOleDbFileDialog = new System.Windows.Forms.SaveFileDialog();

         saveOleDbFileDialog.DefaultExt = "udl";
         saveOleDbFileDialog.FileName = "OleDb1";
         saveOleDbFileDialog.Filter = "Connection String"
                                     + "(*.udl)|*.udl|Connection String(*.connectString)"
                                      + "|*.connectString"
                                       + "|Text File(*.txt)|*.txt|All Files(*.*)|*.*"
                                        ;

         if (saveOleDbFileDialog.ShowDialog() == DialogResult.OK)
         {
            try
            {
               string connect = textbox_Oledb_ConnectionString.Text;
               if (saveOleDbFileDialog.FileName.ToLower().EndsWith(".udl"))
               {
                  connect = "[oledb]\r\n"
                           + "; Everything after this line is an OLE DB initstring\r\n"
                            + connect
                             ;
               }
               else
               {
                  connect = textbox_Oledb_ConnectionString.Text;
               }
               Utils.WriteToFile(saveOleDbFileDialog.FileName, connect);
               button_Connect.Focus();
            }
            catch
            {
               MessageBox.Show("Unable to save connection string");
            }
         }
      }

      /// <summary>This eventhandler processes the 'SQLite Browse' button click</summary>
      /// <remarks>
      /// id : 20130606°1311
      /// note : Code copied/modified from cmdLoadOdbc_Click()
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void button_BrowseSqliteFile_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd;
         ofd = new System.Windows.Forms.OpenFileDialog();

         if (ofd.ShowDialog() == DialogResult.OK)
         {
            /*
            try
            {
               string s;
               StreamReader r = null;
               try
               {
                  r = File.OpenText(ofd.FileName);
                  s = r.ReadToEnd();
               }
               finally
               {
                  if (r != null)
                     r.Close();
               }
               if (s != null)
               {
                  txtOdbcConnectionString.Text = s;
                  cmdConnect.Focus();
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            */
            string sServer = "";
            string sDatabase = "";
            Utils.SplitFullfilenamInServerAndDatabase(ofd.FileName, out sServer, out sDatabase);
            textbox_Sqlite_ServerAddress.Text = sServer;
            textBox_SqliteFile.Text = sDatabase;
         }
      }

      /// <summary>This eventhandler is a gimmick to manipulate the appearance of the tabs title text</summary>
      /// <remarks>
      /// id : 20130616°1703
      /// note : Open issue 20130616°1711 'TabControl DrawItem border missing black line'
      /// note : The DrawMode property of the TabControl must be set from 'Normal' to
      ///    'OwnerDrawFixed', otherwise this event does not fiere. Then, it fires one
      ///    time for each tab.
      /// note : See ref 20130616°1702 'Stacko: How make TabPage's title text bold?'
      /// note : See ref 20130616°1704 'MSDN: TabControl.DrawItem Event'
      /// note : See ref 20130723°1041 'Stacko: A way to color tabs of a tabpage in winforms'
      /// note : See ref 20130723°1042 'Stacko: Set the Base Color of a TabControl'
      /// note : See ref 20130723°1043 'Mick Doherty: Change colours of Tabcontrols Header Item'
      /// note : See ref 20130723°1044 'Oscar Londono: A .NET Flat TabControl'
      /// note : See ref 20130723°1045 'U.N.C.L.E.: Painting Your Own Tabs'
      /// </remarks>
      /// <param name="sender">The object which sent this even</param>
      /// <param name="e">The event object itself</param>
      private void tabcontrol_ServerTypes_DrawItem(object sender, DrawItemEventArgs e)
      {
         // No more wanted now after CustomTabControl is implemented,
         //  but keep the code a while in case (shutdown 20130723°1445)
         if (IOBus.Gb.Debag.Shutdown_Forever)
         {
            Graphics g = e.Graphics;
            Brush brush;

            // Get the item from the collection.
            TabPage tabpage = tabcontrol_ServerTypes.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle rectTabBounds = tabcontrol_ServerTypes.GetTabRect(e.Index);

            // Use own font, just for fun
            // //Font font = new Font(e.Font.FontFamily, (float) 9, FontStyle.Bold, GraphicsUnit.Pixel);
            Font font = tabcontrol_ServerTypes.Font;                           // Borrow font from designer setting

            // Paint selected tab different than the others
            if (e.State == DrawItemState.Selected)
            {
               // Draw a different background color, and don't paint a focus rectangle.
               brush = new System.Drawing.SolidBrush(Color.Black);

               Brush brushRect = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))))); // (experiment 20130716°0801)
               g.FillRectangle(brushRect, e.Bounds);

               if (Glb.Debag.Execute_No)
               {
                  font = new Font(font.FontFamily, font.Size, FontStyle.Bold, GraphicsUnit.Point); // Experiment [line 20130716°0802]
               }
            }
            else
            {
               brush = new System.Drawing.SolidBrush(e.ForeColor);
               // e.DrawBackground();

               // BorderStyle? Empirical test => This is the border of the page area, not the title text area.
               // //tabcontrol_ServerTypes.TabPages[e.Index].BorderStyle = BorderStyle.FixedSingle;
               // //tabcontrol_ServerTypes.TabPages[e.Index].BorderStyle = BorderStyle.None;
               // //tabcontrol_ServerTypes.TabPages[e.Index].BorderStyle = BorderStyle.Fixed3D;

               // Experiment [seq 20130723°1047] This works
               Brush brushRect = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250))))));
               g.FillRectangle(brushRect, e.Bounds);
            }

            // Todo : Retrieve wanted index programmatically [todo 20130616°1731]
            if ((e.Index == 7))                                                // 7 = CouchDB
            {
               font = new Font(font.FontFamily, font.Size, GraphicsUnit.Point);
               brush = new SolidBrush(Color.Teal);
            }

            // Draw string, center the text
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;
            g.DrawString(tabcontrol_ServerTypes.TabPages[e.Index].Text
                          , font
                           , brush
                            , rectTabBounds
                             , new StringFormat(stringformat)
                              );
         }
      }

      /// <summary>This eventhandler serves test purposes only</summary>
      /// <remarks>
      /// id : 20130617°1431
      /// finding : This event fires on about each keystroke.
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void combobox_MssqlServer_TextChanged(object sender, EventArgs e)
      {
         System.Threading.Thread.Sleep(0);                                     // Breakpoint
      }

      /// <summary>This eventhandler serves test purposes only</summary>
      /// <remarks>
      /// id : 20130617°1432
      /// finding : This event fires on about each keystroke.
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void combobox_MssqlServer_TextUpdate(object sender, EventArgs e)
      {
         System.Threading.Thread.Sleep(0);                                     // Breakpoint
      }

      /// <summary>This eventhandler serves test purposes only</summary>
      /// <remarks>
      /// id : 20130617°1433
      /// finding : ?
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void combobox_MssqlServer_SelectedValueChanged(object sender, EventArgs e)
      {
         System.Threading.Thread.Sleep(0);                                     // Breakpoint
      }

      /// <summary>This eventhandler serves test purposes only</summary>
      /// <remarks>
      /// id : 20130617°1434
      /// finding : This fires on typing character keys, but not on the TAB or ENTER key.
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e"></param>
      private void combobox_MssqlServer_KeyPress(object sender, KeyPressEventArgs e)
      {
         char cKey = ' ';
         cKey = e.KeyChar;

         System.Threading.Thread.Sleep(0);                                     // Breakpoint
      }

      /// <summary>This eventhandler processes the SelectedIndexChanged event of all server comboboxes</summary>
      /// <remarks>id : 20130622°1211 (20130617°1621)</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void combobox_Connection_SelectedIndexChanged(object sender, EventArgs e)
      {
         // Service or workaround for storing the selected connection on program exit
         ComboBox combobox = sender as ComboBox;
         SelectedConnectionIndex = combobox.SelectedIndex;

         // Obey flag to avoid vicious circle of adjustment events
         if (_tabpageHasChanged_AndCombobox_IsAlreadyAdjusted == true)
         {
            _tabpageHasChanged_AndCombobox_IsAlreadyAdjusted = false;
            return;
         }
         _tabpageChange_WasInitiated_ByCombobox = true;

         // Confusing workaround ...
         // Todo : Streamline/clear that flag's mechanism. [todo 20130624°0922]
         if (_connectionCombobox_SelectionChanged_FirstCall_HELPERFLAG)
         {
            _connectionCombobox_SelectionChanged_FirstCall_HELPERFLAG = false;
            _tabpageChange_WasInitiated_ByCombobox = false;
         }

         // The 'try' should not be necessary, but during development we had failing casts
         ConnSettingsGui csGui = null;
         try
         {
            csGui = (ConnSettingsGui)combobox.SelectedItem; // (refactor 20130620°0211)

            // Empirical try to prevent crashing when loading demo connection settings [seq 20130624°1231] This helped to some degree
            if (csGui == null)
            {
               return;
            }
         }
         catch (Exception ex)
         {
            // Fatal
            string sMst = ex.Message;
         }

         // Todo : Check when exactly this executed, and whether it is useful at all. (todo 20130709°1323)
         tabpagePaint(csGui);

         // Remember original tabpage filling for later use with creating new connection [20130623°1555]
         _tabpageSelected_OriginalFilling_HELPERMEMO = csGui.ConnIdString();

         // Don't forget this one, otherwise the Connect Button will work wrong [20130620°1623]
         _conSettings = csGui;
      }

      /// <summary>This method paints a TabPage depending on the given connection setting</summary>
      /// <remarks>
      /// id : 20130624°1241
      /// note : Sequence outsourced from method 20130622°1211 combobox_Connection_SelectedIndexChanged()
      ///    to be used also from method 20130624°1121 button_LoadDemoConnections_Click()
      /// </remarks>
      private void tabpagePaint(ConnSettingsGui cs)
      {
         // The actual job, depending on the selected connection type
         ConnSettingsLib.ConnectionType cst = cs.Type;
         if (cst == ConnSettingsLib.ConnectionType.Couch)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Couch;
            textbox_Couch_ServerAddress.Text = cs.DatabaseServerUrl;

            // (empirical shutdown 20130725°1702)
            if (IOBus.Gb.Debag.Shutdown_Anyway)
            {
               combobox_Couch_DatabaseName.Items.Add("cleopatra");
               combobox_Couch_DatabaseName.SelectedIndex = 0;
            }
            else
            {
               combobox_Couch_DatabaseName.Text = cs.DatabaseName;
            }

            textbox_Couch_LoginName.Text = cs.LoginName;
            textbox_Couch_Password.PasswordChar = '*';
            textbox_Couch_Password.Text = cs.Password;
            label_Couch_CapslockIsOn.Visible = Control.IsKeyLocked(Keys.CapsLock);
         }
         else if (cst == ConnSettingsLib.ConnectionType.Mssql)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Mssql;
            textbox_Mssql_ServerAddress.Text = cs.DatabaseServerUrl;
            combobox_Mssql_DatabaseName.Text = cs.DatabaseName;

            radiobutton_Mssql_Trusted.Checked = cs.Trusted;
            radiobutton_Mssql_Untrusted.Checked = (! cs.Trusted);              // Is this necessary? [line 20130623°1533]
            textbox_Mssql_LoginName.Text = cs.LoginName;
            textbox_Mssql_Password.PasswordChar = '*';
            textbox_Mssql_Password.Text = cs.Password;
            label_Mssql_CapslockIsOn.Visible = Control.IsKeyLocked(Keys.CapsLock);
         }
         else if (cst == ConnSettingsLib.ConnectionType.Mysql)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Mysql;
            textbox_Mysql_ServerAddress.Text = cs.DatabaseServerUrl;
            combobox_Mysql_DatabaseName.Text = cs.DatabaseName;

            textbox_Mysql_LoginName.Text = cs.LoginName;
            textbox_Mysql_Password.PasswordChar = '*';
            textbox_Mysql_Password.Text = cs.Password;
            checkbox_Mysql_SavePassword.Checked = cs.SavePassword;
            label_Mysql_CapslockIsOn.Visible = Control.IsKeyLocked(Keys.CapsLock);
         }
         else if (cst == ConnSettingsLib.ConnectionType.Odbc)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Odbc;
            textbox_Odbc_ConnectionString.Text = cs.DatabaseConnectionstring;
         }
         else if (cst == ConnSettingsLib.ConnectionType.OleDb)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Oledb;
            textbox_Oledb_ConnectionString.Text = cs.DatabaseConnectionstring;
         }
         else if (cst == ConnSettingsLib.ConnectionType.Oracle)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Oracle;
            combobox_Oracle_DatabaseName.Text = cs.DatabaseName;

            textbox_Oracle_LoginName.Text = cs.LoginName;
            textbox_Oracle_Password.PasswordChar = '*';
            textbox_Oracle_Password.Text = cs.Password;
            radiobutton_Oracle_Trusted.Checked = cs.Trusted;
            radiobutton_Oracle_Untrusted.Checked = (! cs.Trusted);
            label_Oracle_CapslockIsOn.Visible = Control.IsKeyLocked(Keys.CapsLock);
         }
         else if (cst == ConnSettingsLib.ConnectionType.Pgsql)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Pgsql;
            textbox_Pgsql_ServerAddress.Text = cs.DatabaseServerUrl;
            combobox_Pgsql_DatabaseName.Text = cs.DatabaseName;

            textbox_Pgsql_LoginName.Text = cs.LoginName;
            textbox_Pgsql_Password.PasswordChar = '*';
            textbox_Pgsql_Password.Text = cs.Password;
            label_Pgsql_CapslockIsOn.Visible = Control.IsKeyLocked(Keys.CapsLock);
         }
         else if (cst == ConnSettingsLib.ConnectionType.Sqlite)
         {
            tabcontrol_ServerTypes.SelectedTab = tabpage_Sqlite;
            textbox_Sqlite_ServerAddress.Text = cs.DatabaseServerUrl;
            textBox_SqliteFile.Text = cs.DatabaseName;
         }
         else
         {
            // Fatal
         }
      }

      /// <summary>This method reads all settings from disk and writes them into the controls en bloc</summary>
      /// <remarks>id : 20130622°0711 (20130604°0102)</remarks>
      private void SettingsLoad()
      {
         // Preparation
         MainForm.ServerList_ = Properties.Settings.Default.ServerList;

         // Possible initialisation on program start
         // Todo : The default proposal settings for each database type should be set
         //    by the tab, in the moment the tab is is selected the first time
         //    and it recognizes it has no settings yet. [todo 20130622°0741]
         if (MainForm.ServerList_ == null)
         {
            MainForm.ServerList_ = new ServerList();
         }

         //----------------------------------------------------
         // Note : Below assigned controls should resemble the exact list of all
         //         controls existing on the Connect Form. [note 20130622°0713]
         //----------------------------------------------------

         // No list, no looping
         int iListCount = 0;
         if (MainForm.ServerList_ != null)
         {
            iListCount = MainForm.ServerList_.Items.Length;
         }

         // Loop over the ServerList
         for (int i = 0; i < iListCount; i++)
         {
            ConnSettingsGui csNdx = MainForm.ServerList_.Items[i];             // Just looping comfort

            combobox_Connection.Items.Add(csNdx);

            switch (csNdx.Type)
            {
               case ConnSettingsLib.ConnectionType.Mssql:

                  textbox_Mssql_ServerAddress.Text = csNdx.DatabaseServerUrl;
                  combobox_Mssql_DatabaseName.SelectedText = csNdx.DatabaseName; // Just on suspicion [line 20130717°1221`08]
                  combobox_Mssql_DatabaseName.Text = csNdx.DatabaseName;       // Just on suspicion [line 20130717°1221`09]

                  radiobutton_Mssql_Trusted.Checked = csNdx.Trusted;
                  radiobutton_Mssql_Untrusted.Checked = (! csNdx.Trusted);
                  textbox_Mssql_LoginName.Text = csNdx.LoginName;
                  textbox_Mssql_Password.Text = csNdx.Password;
                  break;

               case ConnSettingsLib.ConnectionType.Mysql:

                  textbox_Mysql_ServerAddress.Text = csNdx.DatabaseServerUrl;
                  combobox_Mysql_DatabaseName.Text = csNdx.DatabaseName;
                  textbox_Mysql_LoginName.Text = csNdx.LoginName;
                  textbox_Mysql_Password.Text = csNdx.Password;
                  checkbox_Mysql_SavePassword.Checked = csNdx.SavePassword;
                  break;

               case ConnSettingsLib.ConnectionType.Odbc:

                  textbox_Odbc_ConnectionString.Text = csNdx.DatabaseConnectionstring;
                  break;

               case ConnSettingsLib.ConnectionType.OleDb:

                  textbox_Oledb_ConnectionString.Text = csNdx.DatabaseConnectionstring;
                  break;

               case ConnSettingsLib.ConnectionType.Oracle:

                  textbox_Oracle_DataSource.Text = csNdx.DatabaseServerUrl;
                  combobox_Oracle_DatabaseName.Text = csNdx.DatabaseName;      // Just on suspicion [line 20130717°1221`07]
                  radiobutton_Oracle_Trusted.Checked = csNdx.Trusted;
                  radiobutton_Oracle_Untrusted.Checked = (! csNdx.Trusted);
                  textbox_Oracle_LoginName.Text = csNdx.LoginName;
                  textbox_Oracle_Password.Text = csNdx.LoginName;
                  break;

               case ConnSettingsLib.ConnectionType.Pgsql:

                  textbox_Pgsql_ServerAddress.Text = csNdx.DatabaseServerUrl;
                  combobox_Pgsql_DatabaseName.Text = csNdx.DatabaseName;       // Abuse
                  textbox_Pgsql_LoginName.Text = csNdx.LoginName;
                  textbox_Pgsql_Password.Text = csNdx.Password;
                  break;

               case ConnSettingsLib.ConnectionType.Sqlite:

                  textbox_Sqlite_ServerAddress.Text = csNdx.DatabaseServerUrl;
                  textBox_SqliteFile.Text = csNdx.DatabaseName;
                  break;

               case ConnSettingsLib.ConnectionType.Couch:

                  textbox_Couch_ServerAddress.Text = csNdx.DatabaseServerUrl;
                  combobox_Couch_DatabaseName.Text = csNdx.DatabaseName;
                  textbox_Couch_LoginName.Text = csNdx.LoginName;
                  textbox_Couch_Password.Text = csNdx.Password;

                  break;

               default:
                  // fatal
                  break;
            }

            // Remember original tabpage filling for later use with creating new connection [20130623°1556]
            _tabpageSelected_OriginalFilling_HELPERMEMO = csNdx.ConnIdString();

         }

         // Dummy flag (compare issue 20130714°1702)
         checkbox_LowBandwidth.Checked = Properties.Settings.Default.LowBandwidth;

      }

      /// <summary>This method saves all available Setting values en bloc</summary>
      /// <remarks>
      /// id : 20130621°1311 (20130604°0101)
      /// todo : More elegant were, not to save all tablpages en bloc, but only
      ///    one single tabpage a time, possibly determined by a parameter passed
      ///    from the caller. [todo 20130809°1501]
      /// callers : button_Connect_Click(), button_SaveSettings_Click_DEBUG()
      /// </remarks>
      private void SettingsSave()
      {
         // Preparation
         List<ConnSettingsGui> liConnSettings = new List<ConnSettingsGui>();

         //====================================================================
         // Resemble the exact list of all controls on the Connect Form [seq 20130621°1312]

         string sComboConnection = combobox_Connection.Text;
         int sTabconSelectedIndex = tabcontrol_ServerTypes.SelectedIndex;

         //----------------------------------------------------
         // (.1) CouchDB
         if ((textbox_Couch_ServerAddress.Text != "") || (combobox_Couch_DatabaseName.Text != ""))
         {
            ConnSettingsGui csCouch = new ConnSettingsGui();
            csCouch.Type = ConnSettingsLib.ConnectionType.Couch;

            csCouch.DatabaseServerUrl = textbox_Couch_ServerAddress.Text;
            csCouch.DatabaseName = combobox_Couch_DatabaseName.Text;

            csCouch.LoginName = textbox_Couch_LoginName.Text;
            csCouch.Password = textbox_Couch_Password.Text;

            liConnSettings.Add(csCouch);
         }

         //----------------------------------------------------
         // (.2) MS-SQL
         if (textbox_Mssql_ServerAddress.Text != "")
         {
            ConnSettingsGui csMssql = new ConnSettingsGui();
            csMssql.Type = ConnSettingsLib.ConnectionType.Mssql;

            csMssql.DatabaseServerUrl = textbox_Mssql_ServerAddress.Text;
            csMssql.DatabaseName = combobox_Mssql_DatabaseName.Text;

            csMssql.Trusted = radiobutton_Mssql_Trusted.Checked;
            csMssql.Trusted = (! radiobutton_Mssql_Untrusted.Checked);         // Haha
            csMssql.LoginName = textbox_Mssql_LoginName.Text;
            csMssql.Password = textbox_Mssql_Password.Text;

            liConnSettings.Add(csMssql);
         }

         //----------------------------------------------------
         // (.3) MySQL
         if ((textbox_Mysql_ServerAddress.Text != ""))
         {
            ConnSettingsGui csMysql = new ConnSettingsGui();
            csMysql.Type = ConnSettingsLib.ConnectionType.Mysql;

            csMysql.DatabaseServerUrl = textbox_Mysql_ServerAddress.Text;
            csMysql.DatabaseName = combobox_Mysql_DatabaseName.Text;

            csMysql.LoginName = textbox_Mysql_LoginName.Text;
            csMysql.Password = textbox_Mysql_Password.Text;
            csMysql.SavePassword = checkbox_Mysql_SavePassword.Checked;

            liConnSettings.Add(csMysql);
         }

         //----------------------------------------------------
         // (.4) ODBC
         if ((textbox_Odbc_ConnectionString.Text != ""))
         {
            ConnSettingsGui csOdbc = new ConnSettingsGui();
            csOdbc.Type = ConnSettingsLib.ConnectionType.Odbc;

            csOdbc.DatabaseConnectionstring = textbox_Odbc_ConnectionString.Text;

            liConnSettings.Add(csOdbc);
         }

         //----------------------------------------------------
         // (.5) OleDb
         if ((textbox_Oledb_ConnectionString.Text != ""))
         {
            ConnSettingsGui csOledb = new ConnSettingsGui();
            csOledb.Type = ConnSettingsLib.ConnectionType.OleDb;

            csOledb.DatabaseConnectionstring = textbox_Oledb_ConnectionString.Text;

            liConnSettings.Add(csOledb);
         }

         //----------------------------------------------------
         // (.6) Oracle
         if ((combobox_Oracle_DatabaseName.Text != ""))
         {
            ConnSettingsGui csOracle = new ConnSettingsGui();
            csOracle.Type = ConnSettingsLib.ConnectionType.Oracle;

            csOracle.DatabaseServerUrl = textbox_Oracle_DataSource.Text;       // Gray, not used
            csOracle.DatabaseName = combobox_Oracle_DatabaseName.Text;

            csOracle.Trusted = radiobutton_Oracle_Trusted.Checked;
            csOracle.Trusted = (! radiobutton_Oracle_Untrusted.Checked);
            csOracle.LoginName = textbox_Oracle_LoginName.Text;
            csOracle.Password = textbox_Oracle_Password.Text;

            liConnSettings.Add(csOracle);
         }

         //----------------------------------------------------
         // (.7) PostgreSQL
         if ((textbox_Pgsql_ServerAddress.Text != ""))
         {
            ConnSettingsGui csPgsql = new ConnSettingsGui();
            csPgsql.Type = ConnSettingsLib.ConnectionType.Pgsql;

            csPgsql.DatabaseServerUrl = textbox_Pgsql_ServerAddress.Text;
            csPgsql.DatabaseName = combobox_Pgsql_DatabaseName.Text;

            csPgsql.LoginName = textbox_Pgsql_LoginName.Text;
            csPgsql.Password = textbox_Pgsql_Password.Text;

            liConnSettings.Add(csPgsql);
         }

         //----------------------------------------------------
         // (.8) SQLite
         if ((textBox_SqliteFile.Text != ""))
         {
            ConnSettingsGui csSqlite = new ConnSettingsGui();
            csSqlite.Type = ConnSettingsLib.ConnectionType.Sqlite;

            // [seq 20130702°1412]
            string sRoot = "";
            string sFilePathWithoutRoot = "";
            bool b = Utils.SplitFullfilenamInServerAndDatabase ( textBox_SqliteFile.Text
                                                                , out sRoot
                                                                 , out sFilePathWithoutRoot
                                                                  );
            csSqlite.DatabaseServerUrl = sRoot;                                // E.g. "C:"
            csSqlite.DatabaseName = sFilePathWithoutRoot;                      // E.g. "Documents and Settings\Frank\Local Settings\Application Data\www.trilo.de\QueryPony.vshost.exe_Url_pnyidnkusl2rbapfnj3mkumbsyerzfns\2.1.2.34917\joesgarage.sqlite3"

            liConnSettings.Add(csSqlite);
         }

         //----------------------------------------------------
         // (.9) Additional
         Properties.Settings.Default.LowBandwidth = checkbox_LowBandwidth.Checked;

         //====================================================================

         // Loop over the accumulated settings and actualize the ServerList [algorithm 20130622°0721]
         foreach (ConnSettingsGui csGui in liConnSettings)
         {
            // Find the respective ServerList entry
            // Todo : implement his algorithm inside ServerList as a service method like
            //   'ConnectionSetting ServerList.findConnection(ConnectionSetting x)' [todo 20130622°0722]
            int iNdxHit = -1;
            for (int i = 0; i < MainForm.ServerList_.Items.Length; i++)
            {
               string sConnFromServerList = MainForm.ServerList_.Items[i].ConnIdString();
               string sConnFromSettings = csGui.ConnIdString();
               if (sConnFromServerList == sConnFromSettings)
               {
                  // Connection found
                  iNdxHit = i;
               }
            }

            // (.) Is the connection setting a not-yet-existing one?
            if (iNdxHit < 0)
            {
               // (.1) The connection has to be added
               MainForm.ServerList_.Add(csGui);                                // Occurrence 20130725°1614`11
            }
            else
            {
               // (.2) The connection has to be updated

               //----------------------------------------------
               // issue 20130701°1256
               // title : Item in ConnectionList cannot be set individually.
               // symptom : The line 'MainForm.ConnectionList.Items[iNdxHit] = cs' just does not work as expected.
               // workaround : Brute force setting sequence 20130701°125602 rebuilds the complete list
               // status : Unsolved. The workaround is a ugly overkill but suffices so far.
               // question : What is missing in the ConnectionList class to update one single item in the List?
               //----------------------------------------------

               // Brute force setting [seq 20130701°1256`02] Completely rebuild the list, this helps
               ServerList sl = new ServerList();
               for (int i = 0; i < MainForm.ServerList_.Items.Length; i++)
               {
                  ConnSettingsGui csHlp = null;
                  if (i == iNdxHit)
                  {
                     csHlp = csGui;
                  }
                  else
                  {
                     csHlp = MainForm.ServerList_.Items[i];
                  }
                  sl.Add(csHlp);
               }
               MainForm.ServerList_ = sl;
            }
         }

         // [seq 20130807°1311]
         QueryPonyGui.Properties.Settings.Default.ServerList = MainForm.ServerList_;
         QueryPonyGui.Properties.Settings.Default.Save();                      // Here stroke issue 20130731°0131, solved 20130809°1221

      }

      /// <summary>This eventhandler processes the SaveSettings Button. This button exists for debug purposes only.</summary>
      /// <remarks>id : 20130621°0921</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_SaveSettings_Click_DEBUG(object sender, EventArgs e)
      {
         SettingsSave();
      }

      /// <summary>
      /// This eventhandler synchronizes the main connection combobox to what
      ///  is now shown on the tabcontrol.
      /// </summary>
      /// <remarks>id : 20130622°1011</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void tabcontrol_ServerTypes_SelectedIndexChanged(object sender, EventArgs e)
      {
         TabControl tcNew = sender as TabControl;

         TabPage tbNew = tcNew.SelectedTab;

         TabPage tbPrev = _previouslySelectedTabpage;
         _previouslySelectedTabpage = tcNew.SelectedTab;

         // Maintain Connect Form title (20130724°1112)
         maintainFormTitleText();

         // If the tabpage change is caused by connection deletion, ignore that values,
         //  otherwise we get a nonsens 'connection edited' dialog. [condition 20130624°0932]
         if (_tabpageSelectionChange_CausedByConnectionDeletion)
         {
            tbPrev = tcNew.SelectedTab;
         }

         // Point the connection combobox to the newly selected tabpage if possible.
         syncConnectionComboboxWith_NewlySelectedTabpage(tbNew);

         // See whether the left tabpage was edited, and possibly store that as new connection.
         bool bEdited = syncConnectionComboboxWith_PreviouslySelectedTabpage(tbPrev);
      }

      /// <summary>
      /// This method examines the newly selected TabPage and
      ///  synchronizes the general connection combobox with it.
      /// </summary>
      /// <remarks>id : 20130623°0911</remarks>
      private void syncConnectionComboboxWith_NewlySelectedTabpage(TabPage tabpageNew)
      {
         string sConnIdFromTab = "";

         // Retrieve connnection ID string
         sConnIdFromTab = getConnectionIdFromTabpage(tabpageNew);

         // Find the index of the wanted item in the connection combobox.
         int iNdxCombo = findConnectionInConnectionCombobox(sConnIdFromTab);

         // Tell the combobox's SelectionChanged eventhandler, that it shall not ignit
         //  a vicious circle of events by trying to adjust the tabcontrol in turn.
         if (! _tabpageChange_WasInitiated_ByCombobox)
         {
            _tabpageHasChanged_AndCombobox_IsAlreadyAdjusted = true;
         }
         _tabpageChange_WasInitiated_ByCombobox = false;

         // Finally select the wanted combobox item
         combobox_Connection.SelectedIndex = iNdxCombo;

         return;
      }

      /// <summary>This method locates the given connection ID in the connection list (or in the Connection ComboBox?)</summary>
      /// <remarks>
      /// id : 20130623°1121
      /// issue : How do we garantee, that the internal connection list ('ServerList') and
      ///          the Items in the Connection ComboBox are always in sync? Shouldn't there
      ///          be some dedicated instance for that? [issue 20130623°1122]
      /// </remarks>
      /// <param name="sConnId">The given connection ID string</param>
      /// <returns>The index of the found connection or -1</returns>
      private int findConnectionInConnectionCombobox(string sConnId)
      {
         // Find the index of the wanted item in the connection combobox.
         int iNdxCombo = -1;
         bool bFound = false;

         foreach (ConnSettingsGui csGui in combobox_Connection.Items)
         {
            iNdxCombo++;
            string sToString = csGui.ConnIdString();
            if (sToString == sConnId)
            {
               bFound = true;
               break;
            }
         }
         if (! bFound)
         {
            iNdxCombo = -1;
         }

         return iNdxCombo;
      }

      /// <summary>This method examines the previously selected TabPage and synchronizes the general connection combobox with it</summary>
      /// <remarks>
      /// id : 20130623°0921
      /// note : This method detects a new connection by comparing the tabpage values
      ///    with those stored in the Connection ComboBox. A more direct algorithm were,
      ///    to compare the actual tabpage values with those values from the tabpage's
      ///    fresh filling. [note 20130623°1551]
      /// </remarks>
      /// <returns>Flag whether the tabpage was edited or not</returns>
      private bool syncConnectionComboboxWith_PreviouslySelectedTabpage(TabPage tbPrev)
      {
         bool bRet = true;

         // Retrieve connnection ID string
         string sConnIdNew = getConnectionIdFromTabpage(tbPrev);

         // If TabPage is just empty, it is considered not edited [20130624°094502]
         if (sConnIdNew == "")
         {
            return true;
         }

         // Examine connection list whether such connection already exists
         int iNdx = findConnectionInConnectionCombobox(sConnIdNew);

         // If connection does not yet exist, create it.
         if (iNdx < 0)
         {
            // Dialogbox 'Connection Edited' confirmation
            DialogResult dr = DialogResult.Yes;
            if (Glb.Behavior.Dialogbox_CreateNewConnection)
            {
               string sConnOld = _tabpageSelected_OriginalFilling_HELPERMEMO;
               if (sConnOld == "")
               {
                  sConnOld = "<n/a>";
               }
               string sMsg = "Connection edited:"
                            + Glb.sCrCr + "Old value:"
                             + Glb.sCr + Glb.sTb + _tabpageSelected_OriginalFilling_HELPERMEMO
                              + Glb.sCrCr + "New value:"
                               + Glb.sCr + Glb.sTb + sConnIdNew
                                + Glb.sCrCr + "Create new connection?"
                                 ;
               string sTitle = "Create New Connection?";
               dr = MessageBox.Show(sMsg, sTitle, MessageBoxButtons.YesNo);
            }
            if (dr == DialogResult.Yes)
            {
               bRet = createNewConnection(tbPrev);
            }
         }

         return bRet;
      }

      /// <summary>This method creates a new connection</summary>
      /// <remarks>id : 20130623°1131</remarks>
      /// <returns>Success flag</returns>
      private bool createNewConnection(TabPage tabpage)
      {
         bool bRet = true;

         ConnSettingsGui cs = new ConnSettingsGui();

         if (tabpage == tabpage_Couch)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Couch;
            cs.DatabaseServerUrl = textbox_Couch_ServerAddress.Text;
            cs.DatabaseName = combobox_Couch_DatabaseName.Text;
            cs.LoginName = textbox_Couch_LoginName.Text;
            cs.Password = textbox_Couch_Password.Text;
         }
         else if (tabpage == tabpage_Mssql)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Mssql;

            cs.DatabaseServerUrl = textbox_Mssql_ServerAddress.Text;
            cs.DatabaseName = combobox_Mssql_DatabaseName.Text;
            cs.LoginName = textbox_Mssql_LoginName.Text;
            cs.Password = textbox_Mssql_Password.Text;
            cs.Trusted = radiobutton_Mssql_Trusted.Checked;                    // Not sure?
         }
         else if (tabpage == tabpage_Mysql)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Mysql;
            cs.DatabaseServerUrl = textbox_Mysql_ServerAddress.Text;
            cs.DatabaseName = combobox_Mysql_DatabaseName.Text;
            cs.LoginName = textbox_Mysql_LoginName.Text;
            cs.Password = textbox_Mysql_Password.Text;
         }
         else if (tabpage == tabpage_Odbc)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Odbc;
            cs.DatabaseConnectionstring = textbox_Odbc_ConnectionString.Text;
         }
         else if (tabpage == tabpage_Oledb)
         {
            cs.Type = ConnSettingsLib.ConnectionType.OleDb;
            cs.DatabaseConnectionstring = textbox_Oledb_ConnectionString.Text;
         }
         else if (tabpage == tabpage_Oracle)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Oracle;
            cs.DatabaseServerUrl = textbox_Oracle_DataSource.Text;             // Just on suspicion [line 20130717°1221`11]
            cs.DatabaseName = combobox_Oracle_DatabaseName.Text;               // Just on suspicion [line 20130717°1221`12]
            cs.LoginName = textbox_Oracle_LoginName.Text;
            cs.Password = textbox_Oracle_Password.Text;
            cs.Trusted = radiobutton_Oracle_Trusted.Checked;                   // Not sure?
         }
         else if (tabpage == tabpage_Pgsql)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Pgsql;
            cs.DatabaseServerUrl = textbox_Pgsql_ServerAddress.Text;
            cs.DatabaseName = combobox_Pgsql_DatabaseName.Text;
            cs.LoginName = textbox_Pgsql_LoginName.Text;
            cs.Password = textbox_Pgsql_Password.Text;
         }
         else if (tabpage == tabpage_Sqlite)
         {
            cs.Type = ConnSettingsLib.ConnectionType.Sqlite;
            cs.DatabaseServerUrl = textbox_Sqlite_ServerAddress.Text;
            cs.DatabaseName = textBox_SqliteFile.Text;
         }
         else
         {
            string sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCr + "(Error 20130623°1132)";
            MessageBox.Show(sErr);
            bRet = false;
         }

         // Maintain Connection ComboBox
         if (bRet)
         {
            //-----------------------------------------------------------------
            // issue 20130623°1142
            // symptoms : In the code, we have much maintaining the ComboBox Items.
            // proposal : Use ConnectionList as the DataSource for the ComboBox.
            // workaround : Leave it as it is.
            // status :
            //-----------------------------------------------------------------

            // Maintain Connection ComboBox
            combobox_Connection.Items.Add(cs);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Empirically added 20130724°0912
            // Note : About .
            //    Line 20130624°1051 causes issue 20130724°0911, it causes the
            //    CouchDB's database combobox value to change from 'fruits' to
            //    '_replicator' unwanted. Why? Probably by igniting the Connection
            //    Combobox's OnSelectionChanged value, which in turn makes various
            //    manipulations.
            //    If this line is shutdown, everything will work, except the Connections
            //    Combobox will not show the actual connection anymore. But there exists
            //    flag 'IsAlreadyAdjusted' to tell the combobox, it shall not work off
            //    it's full program but leave the tabpages alone. This flag has to be used.
            _tabpageHasChanged_AndCombobox_IsAlreadyAdjusted = true; // the solution for issue 20130724°0911
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            // Empirically added 20130624°1051
            combobox_Connection.SelectedItem = cs;                             // The guilty line for issue 20130724°0911

            // Maintain ConnectionList [seq 20130623°1134]
            MainForm.ServerList_.Add(cs);                                      // Cleopatra // Occurrence 20130725°1614`12
            QueryPonyGui.Properties.Settings.Default.ServerList = MainForm.ServerList_;
            QueryPonyGui.Properties.Settings.Default.Save();

            // Maintain TabPages
            tabcontrol_ServerTypes.SelectedTab = tabpage;
         }

         return bRet;
      }

      /// <summary>This method builds the connection ID for the given TabPage</summary>
      /// <remarks>id : 20130623°1011</remarks>
      /// <param name="tb">The TabPage for which to build a connection ID</param>
      /// <returns>The wanted connection ID</returns>
      private string getConnectionIdFromTabpage(TabPage tabpage)
      {
         string sRet = "";

         // Retrieve connnection ID string
         ConnSettingsLib.ConnectionType cTyp = ConnSettingsLib.ConnectionType.NoType;
         string sEle1 = "", sEle2 = "";
         if (tabpage == tabpage_Couch)
         {
            cTyp = ConnSettingsLib.ConnectionType.Couch;
            sEle1 = textbox_Couch_ServerAddress.Text;
            sEle2 = combobox_Couch_DatabaseName.Text;
         }
         else if (tabpage == tabpage_Mssql)
         {
            cTyp = ConnSettingsLib.ConnectionType.Mssql;
            sEle1 = textbox_Mssql_ServerAddress.Text;
            sEle2 = combobox_Mssql_DatabaseName.Text;
         }
         else if (tabpage == tabpage_Mysql)
         {
            cTyp = ConnSettingsLib.ConnectionType.Mysql;
            sEle1 = textbox_Mysql_ServerAddress.Text;
            sEle2 = combobox_Mysql_DatabaseName.Text;
         }
         else if (tabpage == tabpage_Odbc)
         {
            cTyp = ConnSettingsLib.ConnectionType.Odbc;
            sEle1 = textbox_Odbc_ConnectionString.Text;
         }
         else if (tabpage == tabpage_Oledb)
         {
            cTyp = ConnSettingsLib.ConnectionType.OleDb;
            sEle1 = textbox_Oledb_ConnectionString.Text;
         }
         else if (tabpage == tabpage_Oracle)
         {
            cTyp = ConnSettingsLib.ConnectionType.Oracle;
            sEle1 = textbox_Oracle_DataSource.Text;
            sEle2 = combobox_Oracle_DatabaseName.Text;
         }
         else if (tabpage == tabpage_Pgsql)
         {
            cTyp = ConnSettingsLib.ConnectionType.Pgsql;
            sEle1 = textbox_Pgsql_ServerAddress.Text;
            sEle2 = combobox_Pgsql_DatabaseName.Text;
         }
         else if (tabpage == tabpage_Sqlite)
         {
            cTyp = ConnSettingsLib.ConnectionType.Sqlite;
            sEle1 = textBox_SqliteFile.Text;                                           // [obsolet 20130702°1428`51]
            sEle1 = textbox_Sqlite_ServerAddress.Text;
            sEle2 = textBox_SqliteFile.Text;
         }
         else
         {
            string sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCrCr + "(Error 20130623°1002)";
            MessageBox.Show(sErr);
         }

         // Determine IDs only for non-empty TabPage [experiment 20130624°0945]
         if (sEle1 != "" || sEle2 != "")
         {
            sRet = ConnSettingsGui.getConnectionId(cTyp, sEle1, sEle2);
         }

         return sRet;
      }

      /// <summary>This eventhandler processes the Click event to delete the selected connection</summary>
      /// <remarks>id : 20130623°0701</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_DeleteConnection_Click(object sender, EventArgs e)
      {
         string sMsg = "";
         ConnSettingsGui csDel = combobox_Connection.SelectedItem as ConnSettingsGui;
         int iComboSelectedIndex = combobox_Connection.SelectedIndex;

         // Paranoia
         if (combobox_Connection.Items.Count < 1)
         {
            string sErr = "Nothing to delete, connection list is empty.";
            MessageBox.Show(sErr);
            return;
         }

         /*
         if ( (combobox_Connection.Items.Count == 1)
             && (((ConnSettingsGui) combobox_Connection.SelectedItem).Type == ConnSettings.ConnectionType.NoType)
             )
         {
            sMsg = "Connection list is empty." + Glb.sCrCr + "Nothing to delete.";
            MessageBox.Show(sMsg);
            return;
         }
         */

         // User confirmation
         if (csDel != null)                                                    // [added 20130702°1434]
         {
            sMsg = "Connection:" + Glb.sCrCr + Glb.sTb + csDel.ConnIdString()
                  + Glb.sCrCr + "Delete this connection from the connections list?"
                   ;
         }
         else
         {
            sMsg = "No connection selected. Nothing to delete";
         }
         DialogResult dr = MessageBox.Show(sMsg);
         if (dr != DialogResult.OK)
         {
            return;
         }
         if (csDel == null) // (added 20130702°143402)
         {
            return;
         }

         // Maintain TabPage (a place more below would also suit) [seq 20130624°0933]
         clearTabPage(_previouslySelectedTabpage);
         _tabpageSelectionChange_CausedByConnectionDeletion = true;

         // Locate item in connection list
         ServerList connList = MainForm.ServerList_;
         int iNdxFound = -1;
         for (int iNdx = 0; iNdx < connList.Items.Length; iNdx++)
         {
            if (connList.Items[iNdx].ToString() == csDel.ConnIdString())
            {
               iNdxFound = iNdx;
               break;
            }
         }

         // Connection found?
         if (iNdxFound < 0)
         {
            // Error — this should never happen
            string sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCr + "Connection not found.";
            MessageBox.Show(sErr);
            return;
         }

         // Debug issue 20130623°0721
         if (Glb.Debag.ShowDebugMessage_FALSE)
         {
            sMsg = "Connection list indices:" + Glb.sCr ;
            int iCount = -1;
            foreach (ConnSettingsGui cs in combobox_Connection.Items)
            {
               iCount++;
               sMsg += Glb.sCr + iCount.ToString()
                     + Glb.sTb + cs.ToString()
                      ;
            }
            sMsg += Glb.sCrCr + "[Breakpoint 20130623°0722]";

            dr = MessageBox.Show(sMsg);
         }

         // Remove item from global connection list
         if (! connList.Remove(iNdxFound))
         {
            // Error, e.g. the connection list was already empty
            return;
         }

         // Remove item from combobox
         combobox_Connection.Items.RemoveAt(iComboSelectedIndex);

         // Postprocess combobox [seqence 20130623°0734]
         if (combobox_Connection.Items.Count < 1) // now the combobox is empty
         {
            // Experimentally shutdown 20130810°1141 shutdown permanently if possible — Not soo easy
            // The 'N/A' item is puzzling, best we get along without it.
            if (Glb.Debag.Execute_No)
            {
               ConnSettingsGui csPlaceholder = new ConnSettingsGui();
               csPlaceholder.Type = ConnSettingsLib.ConnectionType.NoType;
               combobox_Connection.Items.Add(csPlaceholder);

               // Now we should set _previouslySelected to avoid 'connection edited' dialog [line 20130624°0931]
               combobox_Connection.SelectedIndex = combobox_Connection.Items.Count - 1;
            }
            combobox_Connection.Text = "";                                     // Experiment 20130810°1141`02 seems to be fine
         }
         else if (iComboSelectedIndex == combobox_Connection.Items.Count)      // The highest item was deleted
         {
            iComboSelectedIndex--;

            // Now we should set _previouslySelected to avoid 'connection edited' dialog [line 20130624°0931`02]
            combobox_Connection.SelectedIndex = iComboSelectedIndex;
         }
         else                                                                  // The normal case
         {
            combobox_Connection.SelectedIndex = iComboSelectedIndex;
         }

         // Postprocess TabPage
         TabPage tbDummy2 = _previouslySelectedTabpage; //  null;

         // Maintain settings
         QueryPonyGui.Properties.Settings.Default.ServerList = connList;
         QueryPonyGui.Properties.Settings.Default.Save();

         return;
      }

      /// <summary>This method empties all fields on a TabPage</summary>
      /// <remarks>
      /// id : 20130623°1621
      /// todo : Write the lines to empty all fields on the given TabPage. This is
      ///        not soo easy, because first the caller has to retrieve the TabPage
      ///        to be cleared. This is an additional task. [todo 20130623°1622]
      /// </remarks>
      /// <param name="tb">The TabPage to be cleared</param>
      private void clearTabPage(TabPage tb)
      {
         if (tb == tabpage_Couch)
         {
            textbox_Couch_ServerAddress.Text = "";
            combobox_Couch_DatabaseName.Text = "";
            textbox_Couch_LoginName.Text = "";
            textbox_Couch_Password.Text = "";
         }
         else if (tb == tabpage_Mssql)
         {
            textbox_Mssql_ServerAddress.Text = "";
            combobox_Mssql_DatabaseName.SelectedText = "";
            radiobutton_Mssql_Trusted.Checked = true;                          // The both radiobuttons false? O.k.?
            radiobutton_Mssql_Untrusted.Checked = false;                       // The both radiobuttons false? O.k.?
            textbox_Mssql_LoginName.Text = "";
            textbox_Mssql_Password.Text = "";
         }
         else if (tb == tabpage_Mysql)
         {
            textbox_Mysql_ServerAddress.Text = "";
            combobox_Mysql_DatabaseName.Text = "";
            textbox_Mysql_LoginName.Text = "";
            textbox_Mysql_Password.Text = "";
            checkbox_Mysql_SavePassword.Checked = false;
         }
         else if (tb == tabpage_Odbc)
         {
            textbox_Odbc_ConnectionString.Text = "";
         }
         else if (tb == tabpage_Oledb)
         {
            textbox_Oledb_ConnectionString.Text = "";
         }
         else if (tb == tabpage_Oracle)
         {
            textbox_Oracle_DataSource.Text = "";
            radiobutton_Oracle_Trusted.Checked = false;                        // The both radiobuttons false? O.k.?
            radiobutton_Oracle_Untrusted.Checked = false;                      // The both radiobuttons false? O.k.?
            textbox_Oracle_LoginName.Text = "";
            textbox_Oracle_Password.Text = "";
         }
         else if (tb == tabpage_Pgsql)
         {
            textbox_Pgsql_ServerAddress.Text = "";
            combobox_Pgsql_DatabaseName.Text = "";
            textbox_Pgsql_LoginName.Text = "";
            textbox_Pgsql_Password.Text = "";
         }
         else if (tb == tabpage_Sqlite)
         {
            textBox_SqliteFile.Text = "";
         }
         else
         {
            string sErr = Glb.Errors.TheoreticallyNotPossible + Glb.sCr + "(Error 20130624°0921)";
            MessageBox.Show(sErr);
         }
      }

      /// <summary>This eventhandler processes the LowBandwidth CheckedChanged event and saves the new value to the settings</summary>
      /// <remarks>id : 20130623°0841</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void checkbox_LowBandwidth_CheckedChanged(object sender, EventArgs e)
      {
         //----------------------------------------------------
         // issue 20130714°1702 ''
         // topic : Implement LowBandwidth
         // symptoms : Checkbox LowBandwidth exists but has no functionality yet.
         // todo : Find out what might have been the intention of this flag, then
         //    implement possible different behaviour between high and low bandwith.
         //----------------------------------------------------

         CheckBox checkbox = sender as CheckBox; // checkbox_LowBandwidth
         QueryPonyGui.Properties.Settings.Default.LowBandwidth = checkbox.Checked;
         QueryPonyGui.Properties.Settings.Default.Save();
      }

      /// <summary>This eventhandler ... (experiments for maintaining connection deletion)</summary>
      /// <remarks>id : 20130624°0941</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void tabcontrol_ServerTypes_Enter(object sender, EventArgs e)
      {
         TabControl tc = sender as TabControl;
         TabPage tb = tc.SelectedTab;

         if (Glb.Debag.Execute_No)
         {
            string sMsg = "Debug Enter"
                         + Glb.sCrCr + "TabControl" + Glb.sTb + " : " + tc.Name
                          + Glb.sCr + "TabPage" + Glb.sTb + " : " + tb.Name
                           ;
            MessageBox.Show(sMsg);
         }
         // Finding : This event does not detect the TabPage switching. [finding 20130624°0943]
      }

      /// <summary>This eventhandler ... (experiments for maintaining connection deletion)</summary>
      /// <remarks>id : 20130624°0942</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void tabcontrol_ServerTypes_Leave(object sender, EventArgs e)
      {
         TabControl tc = sender as TabControl;
         TabPage tb = tc.SelectedTab;

         if (Glb.Debag.Execute_No)
         {
            string sMsg = "Debug Leave"
                         + Glb.sCrCr + "TabControl" + Glb.sTb + " : " + tc.Name
                          + Glb.sCr + "TabPage" + Glb.sTb + " : " + tb.Name
                           ;
            MessageBox.Show(sMsg);
         }
         // Finding : This event does not detect the TabPage switching. [finding 20130624°0944]
      }

      /// <summary>This eventhandler processes the button 'Load Demo Connection Settings' click event</summary>
      /// <remarks>id : 20130624°1121</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_LoadDemoConnectionSettings_Click(object sender, EventArgs e)
      {
         DemoConnSettings dcs = new DemoConnSettings();

         // The dialogbox message leading line(s)
         string sMsg       = "Load Demo Connection Settings?";
         sMsg += Glb.sCrCr + "The Demo Connection Settings are examples, what kind of information you"
               + Glb.sCr   + "have to fill into the fields on the connection tabs. They will mostly"
               + Glb.sCr   + "not work 'as are', because the settings for the databases available on"
               + Glb.sCr   + "your machine will be mostly different. Modify the examples to your needs."
               + Glb.sCrCr + "Only the two SQLite connection settings shall work 'as are' indeed,"
               + Glb.sCr   + "because this are demo files stored in your user configuration directory."
               + Glb.sCr   + "(See main menu item 'About', tab 'Machine'.)"
               + Glb.sCrCr + "The following connection settings will be loaded:"
               + Glb.sCr
               ;

         // Build list of demo connections which will be loaded
         for (int iNdx = 0; iNdx < dcs.Items.Count; iNdx++ )
         {
            ConnSettingsGui csGui = dcs.Items[iNdx];
            sMsg += Glb.sCr + "   " + (iNdx + 1).ToString() + "  -  " + csGui.ConnIdString();
         }
         sMsg += Glb.sCrCr + "Load them?";

         DialogResult dr = MessageBox.Show(sMsg, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

         if (dr != DialogResult.OK)
         {
            return;
         }

         //--------------------------------------------------------------------
         // Sequence is possibly to be outsourced to a dedicated 'add' method

         for (int iNdx = 0; iNdx < dcs.Items.Count; iNdx++)
         {
            ConnSettingsGui csGui = dcs.Items[iNdx];

            // Is connection already in the Connection List?
            string sDemoId = csGui.ConnIdString();

            // Is demo connectionsetting already in main connectionsettings list?
            List<string> list = MainForm.ServerList_.Ids;
            if (list.Contains(sDemoId))
            {
               continue;
            }

            // Transfer this DemoConnectionSettings into the program
            MainForm.ServerList_.Add(csGui);                                   // Occurrence 20130725°1614`13
            combobox_Connection.Items.Add(csGui);
            tabpagePaint(csGui);

         }
         combobox_Connection.SelectedIndex = 0;                                // Just to have any selection at all
         //--------------------------------------------------------------------
      }

      // (ref 20130624°1132)
      // title : How to disable editing of elements in combobox for C#?
      // url : http://stackoverflow.com/questions/598447/how-to-disable-editing-of-elements-in-combobox-for-c
      // usage : In method 20130624°1132 combobox_Connection_KeyPress()

      /// <summary>This eventhandler processes the Connection ComboBox KeyPress event to prevent editing it</summary>
      /// <remarks>id : 20130624°1131</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void combobox_Connection_KeyPress(object sender, KeyPressEventArgs e)
      {
         e.Handled = true;
      }

      //-------------------------------------------------------
      // [issue 20130622°1141]
      // issue : The Connect Form's FormClosing and FormClosed events on the ConnectForm just
      //    do not fire. The FormClosed event works on the MainForm, but not with ConnectForm.
      // question : How to intercept the closing of a form-on-a-tab on program exit?
      // workaround : Pass the setting values to be stored in the event via a public
      //    static helper field to method 20130604°0629 MainForm::FormClosed().
      //-------------------------------------------------------

      /// <summary>This method maintains the main treeview</summary>
      /// <remarks>id : 20130701°1115</remarks>
      /// <returns>Success flag</returns>
      private bool maintainTreeviewMain()
      {
         TreeNode tn = new TreeNode();
         tn.Text = "Hello";
         tn.Tag = "hellox";
         MainForm.TreeviewMain.Nodes.Add(tn);

         return true;
      }

      /// <summary>This method retrieves a list of databases from the server of the given connection settings</summary>
      /// <remarks>id : 20130713°0921</remarks>
      /// <param name="arDatabases">The wanted array with database names</param>
      /// <param name="csLib">The connection setting for which to retrieve a database list</param>
      /// <returns>Success flag</returns>
      private bool getDatabaseList(out string[] arDatabases, ConnSettingsLib csLib)
      {
         bool bRet = false;
         arDatabases = null;

         // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
         // Connect dbClient [seq 20130713°0922]
         // See Todo 20130828°1522 'Combine 4 connect dbClient seqences into one method'

         // Get a the client object
         DbClient dbClient = DbClientFactory.GetDbClient(csLib);

         // Compare issue 20130716°0622
         dbClient.Error += new EventHandler<ErrorEventQeArgs>(dbClient_Error);

         // Waiting phase start
         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;

         // Make the connection
         bRet = dbClient.Connect();

         // Waiting phase end
         Cursor = oldCursor;
         // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

         // Connection failed?
         if (bRet)
         {
            // To retrieve the database list, a DbBrowser is wanted
            IDbBrowser dbBrowser = DbClientFactory.GetBrowser(dbClient);

            // Do the actual job
            arDatabases = dbBrowser.GetDatabases();
         }
         else
         {
            // notification
            string sErr = dbClient.ErrorMessage;
            sErr += "Error" + ": ";
            MainForm.outputStatusLine(sErr);
         }

         // Finish this connection - otherwise an orphaned process is left behind (issue 20130619°0551)
         dbClient.Dispose();

         return bRet;
      }

      /// <summary>This eventhandler processes the Error event of a DbClient</summary>
      /// <remarks>
      /// id : 20130716°0625
      /// note : This eventhandler was introduced to solve issue 20130716°0622.
      /// ref : 20130716°0642 'thread: hang after invoking a delegate from other thread'
      /// ref : 20130716°0627 'msdn: Make Thread-Safe Calls to Windows Forms Controls'
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void dbClient_Error(object sender, ErrorEventQeArgs e)
      {
         // Shutdown 20130716°0643
         if (IOBus.Gb.Debag.Shutdown_Archived)
         {
            // This line was automatically created by the VS wizzard when it created this eventhandler.
            throw new NotImplementedException();
         }

         // note : Remember issue 20130716°0626 'Thread-Safe Calls to Windows Forms Controls'
         // note : The not-working attempts:
         //    (1) 'MainForm._mainform.outputStatusLino(e.ErrorMessage);' causes cross-thread exception
         //    (2) 'this.Invoke(this._outputStatusLino, { e.ErrorMessage });' goes to nirwana anyway
         object[] aro = { e.ErrorMessage };
         this.BeginInvoke(this._outputStatusLino, aro);

         return;                                                               // Breakpoint
      }

      /// <summary>This eventhandler processes the DropDown event of the MS-SQL databases combobox</summary>
      /// <remarks>
      /// id : 20130714°1721
      /// note : (1) This method contains a minimalistic server connect and disconnect
      ///    sequence, as opposed to the more complex processes behind the Connect Button.
      ///    Thus it serves as good location to study connecting. (2) Also, if something
      ///    goes wrong, this method is sensitive to the orphane-processes-leave-behind
      ///    issues. Thus it is a good starting point for studying and debugging the
      ///    threading behaviour (note 20130715°1134/1135)
      /// todo : Outsource the functionality to a non-eventhandler method to be available
      ///    for callers in general. Or is getDatabaseList() already enough outsourced?
      ///    (todo 20130715°1136)
      /// note 20130714°1722 : BTW, to see what the corresponding field above
      ///    the old treeview does, inspect methods
      ///    - 20130604°2156 QueryForm.cs::comboboxDatabase_SelectedIndexChanged()
      ///    - 20130604°2157 QueryForm.cs::comboboxDatabase_Enter()
      ///    - 20130604°2139 QueryForm.cs::CheckDatabase()
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void combobox_DatabaseName_DropDown(object sender, EventArgs e)
      {
         string s = "", sMsg = "";

         // Detect database type
         ComboBox cb = sender as ComboBox;
         TabPage tb = cb.Parent as TabPage;
         if (tb == null)
         {
            sMsg = "Error" + ": " + Glb.Errors.TheoreticallyNotPossible + " " + "[error 20130715°1131]";
            MainForm.outputStatusLine(sMsg);
            return;
         }

         // Prepare the controls to use
         // Note 20130715°1133 : This sequence will *much* be simplified, if we eliminate
         //    the tabs at all. This sequence is also a preparation for such planned
         //    refactor, because it tells how to condense the involved controls.
         TextBox textboxServerAddress = null;
         ComboBox comboboxDatabaseName = null;
         TextBox textboxLoginName = null;
         TextBox textboxPassword = null;
         TextBox textboxServerAddress2 = new TextBox();                        // Is this a good idea? [line 20130723°1025`11]
         TextBox textboxServerPortnum = new TextBox();                         // Is this a good idea? [line 20130723°1025`12]
         TextBox textboxServerProtocol = new TextBox();                        // Is this a good idea? [line 20130723°1025`13]
         ConnSettingsLib.ConnectionType connectionType = ConnSettingsLib.ConnectionType.NoType;
         bool bTrusted = false;
         if (tb == tabpage_Couch)
         {
            connectionType = ConnSettingsLib.ConnectionType.Couch;
            textboxServerAddress = textbox_Couch_ServerAddress;
            comboboxDatabaseName = combobox_Couch_DatabaseName;
            textboxServerAddress2.Text = textboxServerAddress.Text;
            textboxServerPortnum.Text = "0";
            textboxServerProtocol.Text = "";
            textboxLoginName = textbox_Couch_LoginName;
            textboxPassword = textbox_Couch_Password;
         }
         else if (tb == tabpage_Mssql)
         {
            connectionType = ConnSettingsLib.ConnectionType.Mssql;
            textboxServerAddress = textbox_Mssql_ServerAddress;
            comboboxDatabaseName = combobox_Mssql_DatabaseName;
            bTrusted = radiobutton_Mssql_Trusted.Checked;
            textboxLoginName = textbox_Mssql_LoginName;
            textboxPassword = textbox_Mssql_Password;
         }
         else if (tb == tabpage_Mysql)
         {
            connectionType = ConnSettingsLib.ConnectionType.Mysql;
            textboxServerAddress = textbox_Mysql_ServerAddress;
            comboboxDatabaseName = combobox_Mysql_DatabaseName;
            bTrusted = checkbox_Mysql_IntegratedSecurity.Checked;
            textboxLoginName = textbox_Mysql_LoginName;
            textboxPassword = textbox_Mysql_Password;
         }
         else if (tb == tabpage_Oracle)
         {
            connectionType = ConnSettingsLib.ConnectionType.Oracle;
            textboxServerAddress = textbox_Oracle_DataSource;
            comboboxDatabaseName = combobox_Oracle_DatabaseName;
            bTrusted = radiobutton_Oracle_Trusted.Checked;
            textboxLoginName = textbox_Oracle_LoginName;
            textboxPassword = textbox_Oracle_Password;
         }
         else if (tb == tabpage_Pgsql)
         {
            connectionType = ConnSettingsLib.ConnectionType.Pgsql;
            textboxServerAddress = textbox_Pgsql_ServerAddress;
            comboboxDatabaseName = combobox_Pgsql_DatabaseName;
            textboxLoginName = textbox_Pgsql_LoginName;
            textboxPassword = textbox_Pgsql_Password;
         }
         else
         {
            sMsg = "Error" + ": " + Glb.Errors.TheoreticallyNotPossible + " " + "[error 20130715°1132]";
            MainForm.outputStatusLine(sMsg);
            return;
         }

         // Define connection settings
         ConnSettingsLib csLib = new ConnSettingsLib();
         csLib.Type = connectionType;
         csLib.Timeout = 3;                                                    // Hm ..
         csLib.DatabaseServerUrl = textboxServerAddress.Text;
         csLib.DatabaseName = "";                                              // Blank shall be interpreted as e.g. "master"
         csLib.DatabaseServerAddress = textboxServerAddress.Text;              // So far only used for CouchDB [new 20130723°1024`01]
         try { csLib.DatabaseServerPortnum = int.Parse(textboxServerPortnum.Text); }  // So far only used for CouchDB [new 20130723°1024`02]
         catch { csLib.DatabaseServerPortnum = 0; }
         csLib.DatabaseServerProtocol = textboxServerProtocol.Text;            // So far only used for CouchDB [new 20130723°1024`03]
         csLib.Trusted = bTrusted;
         if (! bTrusted)
         {
            csLib.LoginName = textboxLoginName.Text;
            csLib.Password = textboxPassword.Text;
         }

         // Switch signal on while retrieving list of databases
         Color colOrginal = textboxServerAddress.BackColor;
         textboxServerAddress.BackColor = Color.Yellow;
         textboxServerAddress.Refresh();
         comboboxDatabaseName.BackColor = Color.Yellow;
         comboboxDatabaseName.Refresh();

         // Retrieve the wanted list
         string[] arDatabases = null;
         bool b = getDatabaseList(out arDatabases, csLib);

         //----------------------------------------------------
         // note 20130715°1152 ''
         // About MySQL connecting. Even with no password set, getDatabaseList()
         //  delivers two databases: (1) 'information_schema' (2) 'test'. But if
         //  I want view one then, the credentials are required.
         //----------------------------------------------------

         // Fill combobox
         // note : If it's null, isn't then something wrong? [note 20130714°1744]
         if (arDatabases != null)
         {
            comboboxDatabaseName.Items.Clear();
            for (int i = 0; i < arDatabases.Length; i++)
            {
               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
               // Paranoia [seq 20130717°1211]
               // Note : This condition is introduced as quick'n'dirty fix for problem
               //    'database entry is null'. Debugging shows, the deeper reason is problem
               //    'cannot switch from CouchDB tab to any other tab'. This reason seems
               //    'cannot by adding lines 20130717°1213/1214.
               // Todo : Make sure above, that a database entry cannot be null. [todo 20130717°1212]
               if (arDatabases[i] == null)
               {
                  s = "Error : Database list entry no " + (i + 1).ToString() + " is null.";
                  MainForm.outputStatusLine(s);
                  continue;
               }
               //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

               comboboxDatabaseName.Items.Add(arDatabases[i]);
            }
            sMsg = "Server found: " + "'" + textboxServerAddress.Text + "'";
         }
         else
         {
            sMsg = "Server not found: " + "'" + textboxServerAddress.Text + "'";
         }
         MainForm.outputStatusLine(sMsg);

         // switch signal off
         textboxServerAddress.BackColor = colOrginal;
         textboxServerAddress.Refresh();
         comboboxDatabaseName.BackColor = colOrginal;
         comboboxDatabaseName.Refresh();

         return;
      }

      /// <summary>This eventhandler processes the checkbox IntegratedSecurity's CheckedChanged event</summary>
      /// <remarks>
      /// id : 20130715°1151
      /// todo : Merge all 'IntegratedSecurity' and related flags eventhandlers to one. [todo 20130715°1301]
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void checkbox_Mysql_IntegratedSecurity_CheckedChanged(object sender, EventArgs e)
      {
         if (checkbox_Mysql_IntegratedSecurity.Checked)
         {
            textbox_Mysql_LoginName.TabStop = false;
            textbox_Mysql_Password.TabStop = false;
            label_Mysql_LoginName.ForeColor = Color.Gray;
            label_Mysql_Password.ForeColor = Color.Gray;
         }
         else
         {
            textbox_Mysql_LoginName.TabStop = true;
            textbox_Mysql_Password.TabStop = true;
            label_Mysql_LoginName.ForeColor = Color.Black;
            label_Mysql_Password.ForeColor = Color.Black;
         }
      }

      /// <summary>This method maintains the Connect Form's title text 'Connect to ...'</summary>
      /// <remarks>id : 20130724°1111</remarks>
      private void maintainFormTitleText()
      {
         if (! IOBus.Gb.Debag.Shutdown_Alternatively)
         {
            // Fade in database type into main form's caption [seq 20130725°0911]
            // Note : It is not sure to be such good feature, it might be more puzzling than helpful.
            TabPage tb = tabcontrol_ServerTypes.SelectedTab;
            string s = "";
            if (tb == tabpage_Couch) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Couch); }
            else if (tb == tabpage_Mssql) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Mssql); }
            else if (tb == tabpage_Mysql) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Mysql); }
            else if (tb == tabpage_Odbc) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Odbc); }
            else if (tb == tabpage_Oledb) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.OleDb); }
            else if (tb == tabpage_Oracle) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Oracle); }
            else if (tb == tabpage_Pgsql) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Pgsql); }
            else if (tb == tabpage_Sqlite) { s = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Sqlite); }
            else { }
            label_ToDatabase.Text = s;
         }
         else
         {
            label_ToDatabase.Text = "...";
         }
      }
   }
}
