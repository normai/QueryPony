#region Fileinfo
// file        : 20130604°0531 /QueryPony/QueryPonyGui/Gui/MainForm.cs
// summary     : Class 'MainForm' constitutes the Main Form
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyGui.Properties;
using QueryPonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QueryPonyGui
{

   /// <summary>This class constitutes the Main Form</summary>
   /// <remarks>id : 20130604°0532</remarks>
   public partial class MainForm : Form
   {

      /// <summary>This static field provides an instance of the MenuItems class</summary>
      /// <remarks>id : 20130707°1833</remarks>
      internal static MenuItems _MenuItems = new MenuItems();

      /// <summary>This static field represents the MainForm object ... (was introduced as a workaround for the delegate construction)</summary>
      /// <remarks>id : 20130707°1021</remarks>
      internal static string _signPost = "Beta\r\nVersion";

      /// <summary>This static field represents the MainForm object ... (was introduced as a workaround for the delegate construction)</summary>
      /// <remarks>id : 20130707°1022</remarks>
      internal static System.Drawing.Color _signPostColor = System.Drawing.Color.Magenta; // Aquamarine, Magenta, Pink

      /// <summary>This static field stores this MainForm's instance to be available for other classes</summary>
      /// <remarks>id : 20130619°0441</remarks>
      // note : Access level switched 'public', so other assemblies can use assembly 'QueryPonyGui' (20130814°091102).
      public static MainForm _mainform = null;

      /// <summary>This static field stores an array of the opened QueryForms</summary>
      /// <remarks>id : 20130701°1136</remarks>
      internal static List<QueryForm> _queryforms = new List<QueryForm>();

      /// <summary>This property gets/sets the status textbox to be available for other classes</summary>
      /// <remarks>id : 20130701°1137</remarks>
      internal static TextBox TextboxStatus { get; set; }

      /// <summary>This static property gets/sets the active serverlist</summary>
      /// <remarks>
      /// id : 20130622°0901
      /// todo : Convert to the automatic property. Before this can be done, the direct
      ///    accessings of _serverList have to be eliminated [todo 20130725°1711 done]
      /// note : Remember issue 20130725°1611 'connection is missing login name'. The
      ///   issue is only partially solved, property ServerList_ may contain many duplicates.
      /// </remarks>
      public static ServerList ServerList_ { get; set; }

      /// <summary>This static property gets/sets the main treeview</summary>
      /// <remarks>id : 20130701°1112</remarks>
      public static TreeView TreeviewMain { get; set; }

      /// <summary>This field stores ... the EditManager</summary>
      /// <remarks>id : 20130604°0534</remarks>
      private EditManager _editManager1 = EditManager.GetEditManager();

      /// <summary>This constructor forwards to the other constructor</summary>
      /// <remarks>id : ctor 20130604°0535</remarks>
      public MainForm() : this(new string[0])
      {
      }

      /// <summary>This constructor creates the new MainForm</summary>
      /// <remarks>id : ctor 20130604°0536</remarks>
      /// <param name="args">The arguments from the commandline starting this program.</param>
      public MainForm(string[] args)
      {
         string s = "";

         // Determine window appearance [seq 20130604°1111]
         if (Settings.Default.MaximizeMainWindow && Glb.Behavior.StartupWindowAllowMaximize)
         {
            this.WindowState = FormWindowState.Maximized;
         }

         InitializeComponent();

         // Static helper fields for communication with other forms [helpers/workaround/refactor 20130618°0416]
         MainForm._mainform = this;
         MainForm._maintabcontrol = this.tabcontrolMain;
         MainForm.TextboxStatus = this.textboxStatus;

         // Initialisation tasks [seq 20130707°0904]
         // Note : This was outsourced from Program.cs to here to counteract issue 20130706°2221.
         Inits inits = new Inits();
         if (! inits.DoInitialization())
         {
            s = "Program flow error (20130707°0904)";
            MessageBox.Show(s);
            return;
         }

         // Output welcome log message number two [seq 20130711°0911]
         // Note : It looks as if this cannot be done in Program.cs at all, only later
         //    in MainForm.cs. If you do this, it will build fine, but when starting the
         //    standalone executable, it runs into an exception. (20130711°0911)
         string sOut = "[Debug]" + " " + Glb.Resources.AssemblyNameLib
                      + Glb.sBlnk + "was initialized (two) (debug 20130711°0911)."
                       ;
         Utils.outputLine(sOut);

         // Attach the EditManager ...
         AttachEditManager();

         // Initialize ServerList (but this cannot yet fill in the controls on the ConnectForm)
         // note : Remember issue 20130731°0131 finding 20130808°1513 'ServerList settings silently fail'.
         LoadServerList();

         // Load the most-recently-used files list
         LoadMRU();

         // Obey commandline parameters
         if (args.Length > 0)
         {
            ConnSettingsGui conSettings = LoadSettingsFromArgs(args);
            if (conSettings != null)
            {
               IQueryForm qf = DoConnect(conSettings);
               if (qf != null)
               {
                  CommandLineParams cmdLine = new CommandLineParams(args);

                  // Process option "-i [FileName] : Open SQL File"
                  if (cmdLine["i"] != null)
                  {
                     qf.Open(cmdLine["i"]);
                  }
               }
            }
         }

         // Avoid compiler warning "The event '_eventhandlerPastedOrWhat' is never used" [seq 20130828°1432]
         _eventhandlerPastedOrWhat = ClipboardEvent_PastedOrWhat;

         // Mandatory sequence
         EnableControls();
         this.Show();

         // [seq 20130828°1411]
         // Note : Compare seq 20130604°1111 and simplify/merge, possibly shift this above.
         // Note : Setting the location works only behind this.Show(). The size works already before.
         System.Drawing.Point ptMainLocation = Settings.Default.PosMain;
         System.Drawing.Size sizMainSize = Settings.Default.SizeMain;
         if (sizMainSize.Width > 0)
         {
            _mainform.Location = ptMainLocation;
            _mainform.Size = sizMainSize;
            splitContainer1.SplitterDistance = Settings.Default.PosSplitOne;
            splitContainer2.SplitterDistance = Settings.Default.PosSplitTwo;
         }

         // Provide the TreeView to other modules (20130701°1113)
         MainForm.TreeviewMain = treeviewMain;

         // Delete this condition if the new Forms-on-Tabs feature has proofen fine (20130618°040111)
         if (IOBus.Gb.Debag.Shutdown_Forever)
         {
            // Original call
            DoConnect_POSSIBLY_REVIVE();
         }
         else
         {
            FurnishConnectTab(); // (20130618°0401)
         }

         // Connect surrogate feature (20130725°1437)
         _blinkTimer.Tick += new System.EventHandler(dispatcherTimer_Tick);

         // Set e.g. the 'Query Options' menu item availability [line 20130812°1402]
         MainForm._mainform.EnableControlsOthers();

         // Cosmetic experiment [seq 20130814°1012]
         // Ref : 20130814°1011 'keep split container fixed width'
         this.splitContainer1.FixedPanel = FixedPanel.Panel1;

         // Restore connections [seq 20130828°1524]
         // See open issue 20130828°1531 'restoring connections works only partially'.
         foreach (ConnSettingsGui csGui in ServerList_.Items)
         {
            if (csGui.Status != ConnSettingsGui.ConnStatus.Connected)
            {
               continue;
            }

            s = "Connecting " + csGui.Key + " ... ";
            IOBusConsumer.writeHost(s);
            bool b = establishConnection(csGui, out s);
            if (b)
            {
               s = "success.";
            }
            else
            {
               s = "failed: " + s;
            }
            IOBusConsumer.writeHostChar(s);
         }
      }

      /// <summary>This method loads the most-recently-used files list</summary>
      /// <remarks>id : 20130604°0537</remarks>
      private void LoadMRU()
      {
         mruMenuManager1.Filenames = Settings.Default.MRUFiles;
      }

      /// <summary>This method ...</summary>
      /// <remarks>
      /// id : 20130604°0538
      /// issue : Not sure whether this feature works at all. But surely it is
      ///    missing to process most of the server types. (issue 20130612°1331)
      /// </remarks>
      /// <param name="args">The arguments from the commandline starting this program.</param>
      /// <returns>The wanted connections settings from the commansline</returns>
      private ConnSettingsGui LoadSettingsFromArgs(string[] args)
      {
         ConnSettingsGui conSettings = null;
         CommandLineParams cmdLine = new CommandLineParams(args);

         if (cmdLine["cn"] != null)
         {
            if (ServerList_.IndexOf(cmdLine["cn"]) >= 0)
            {
               conSettings = ServerList_.Items[ServerList_.IndexOf(cmdLine["CN"])];
               return conSettings;
            }
         }
         else if (cmdLine["s"] != null)
         {
            conSettings = new ConnSettingsGui();
            conSettings.Type = ConnSettingsLib.ConnectionType.Mssql;
            conSettings.DatabaseServerUrl = cmdLine["s"];
         }
         else if (cmdLine["os"] != null)
         {
            conSettings = new ConnSettingsGui();
            conSettings.Type = ConnSettingsLib.ConnectionType.Oracle;
            conSettings.DatabaseName = cmdLine["os"];
         }
         else
         {
         }

         if (conSettings != null)
         {
            if (cmdLine["e"] != null)
            {
               conSettings.Trusted = true;
            }
            else
            {
               if (cmdLine["u"] != null) conSettings.LoginName = cmdLine["u"];
               if (cmdLine["p"] != null) conSettings.Password = cmdLine["p"];
            }
         }

         return conSettings;
      }

      /// <summary>This method attaches the EditManager ...</summary>
      /// <remarks>
      /// id : 20130604°0539
      /// callers : Only MainForm()
      /// </remarks>
      private void AttachEditManager()
      {
         _editManager1.MenuItemCopy = copyToolStripMenuItem;
         _editManager1.MenuItemCopyWithHeaders = copyWithHeadersToolStripMenuItem;
         _editManager1.MenuItemCut = cutToolStripMenuItem;
         _editManager1.MenuItemEdit = editToolStripMenuItem1;                  // Debug menu items gray [mark 20130810°1602`19]
         _editManager1.MenuItemPaste = pasteToolStripMenuItem;
         _editManager1.MenuItemSelectAll = selectAllToolStripMenuItem;
         _editManager1.MenuItemUndo = undoToolStripMenuItem;
      }

      //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      // Outsourced here about 600 lines to MainMenu.cs [note 20130725°1412]
      // Attempting to separate menu handling, this regions seem appropriate:
      // - #region Menu and Toolbar Button Events
      // - #region Menu and Toolbar Button Implementations
      //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°0624</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void qf_PropertyChanged(object sender, EventArgs e)
      {
         EnableControls();
      }

      /// <summary>This method returns the active MDI QueryForm if one exists</summary>
      /// <remarks>id : 20130604°0625</remarks>
      /// <returns>The wanted IQueryForm or null</returns>
      private IQueryForm GetQueryChild__OBSOLET()
      {
         return (IQueryForm) ActiveMdiChild;
      }

      /// <summary>This method returns the active query form</summary>
      /// <remarks>
      /// id : 20130619°0512 (20130604°0625)
      /// note : Remember bug 20130729°1543 'ArgumentOutOfRangeException when clicking tabs/nodes'.
      /// </remarks>
      /// <returns>The wanted IQueryForm or null</returns>
      internal IQueryForm GetQueryChild()
      {
         // The original line from the former MDI method
         // //return (IQueryForm)ActiveMdiChild;

         IQueryForm qfRet = null;

         // Primitive attempt to replace '(IQueryForm)ActiveMdiChild'
         int iTab = tabcontrolMain.SelectedIndex; // debug 20130729°154302
         TabPage tb = tabcontrolMain.SelectedTab;
         Control.ControlCollection cc = tb.Controls;
         int iCount = cc.Count;

         Control c = null;
         if (iCount > 0)
         {
            c = cc[0];
         }
         else
         {
            // Fatal?
            // Note : E.g. it occurres with the Settings Form on the tab [note 20130809°1523]
            if (Glb.Debag.Execute_No)
            {
               string s = "Probably no QueryForm on the wanted TabPage (bug 20130729°154311)";
               outputStatusLine(s);
            }
            return qfRet;
         }

         try
         {
            qfRet = (IQueryForm) c;
         }
         catch (Exception ex)
         {
            string sMsg = ex.Message; // fatal
         }

         return qfRet;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0626</remarks>
      /// <returns>The wanted flag ...</returns>
      private bool IsChildActive_PRESERVED_OBSOLET()
      {
         return ActiveMdiChild != null;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130619°0513 (20130604°0626)</remarks>
      /// <returns>The wanted flag whether ...</returns>
      private bool IsChildActive()
      {
         // The original line from the former MDI method
         // //return ActiveMdiChild != null;

         bool bRet = false;

         // Primitive attempt to replace '(ActiveMdiChild != null)'
         int iTab = tabcontrolMain.SelectedIndex;
         if (iTab > 0)
         {
            bRet = true;
         }

         return bRet;
      }

      /// <summary>This method loads the ServerList from user.conf into our live object</summary>
      /// <remarks>
      /// id : 20130604°0627
      /// note : Remember issue 20130731°0131'ServerList settings silently fail'.
      ///    Here was a key location for the issue. Here the 'random named assembly'
      ///    from finding 20130808°1513 is loaded. For details see debug sequence
      ///    20130808°1531 in Program.cs::AppDomain.CurrentDomain.AssemblyLoad += ()=>{};
      /// note : Here struck issue 20130808°1552 'settings type with library call fails'
      /// </remarks>
      private void LoadServerList()
      {
         // Experiment [seq 20130808°1551]
         // Ref : Issues 20130731°0131 and 20130808°1552, solution 20130809°1221.
         if (Glb.Debag.Execute_No)
         {
            XmlSerializer ser = new XmlSerializer(typeof(ServerList));
         }

         // Retrieve connection list from Settings as it looked on last program exit
         ServerList_ = Settings.Default.ServerList;

      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0628</remarks>
      private void SaveServerList()
      {
         Settings.Default.ServerList = ServerList_;
         Settings.Default.Save();
      }

      /// <summary>This eventhandler runs when the form is closed, it stores some settings for the next session</summary>
      /// <remarks>id : 20130604°0629</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         // (B) Save settings
         // (B.1) Convenience
         int iSelectedConnectionIndex = ConnectForm.SelectedConnectionIndex;

         // (B.2) Debug
         if (Settings.Default.ShowDebugProgramExit)
         {
            string sMsg = "QueryPony is closing"
                         + IOBus.Gb.Bricks.Cr + "Connection index = " + iSelectedConnectionIndex.ToString()
                         + IOBus.Gb.Bricks.Cr + "Window location = " + _mainform.Location.ToString()
                         + IOBus.Gb.Bricks.Cr + "Window size = " + _mainform.Size.ToString()
                         + IOBus.Gb.Bricks.CrCr + "(Switch off this message on Tab Settings - Debug)"
                         + IOBus.Gb.Bricks.Cr + "[Breakpoint 20130622°1123]"
                          ;
            DialogResult dr = MessageBox.Show(sMsg);
         }

         // (B.3) Collect values
         Settings.Default.LastSelectedConnection = iSelectedConnectionIndex;
         Settings.Default.MaximizeMainWindow = (this.WindowState == FormWindowState.Maximized);

         // The other end of seqence 20130828°1411 [seqence 20130828°1412]
         Settings.Default.PosMain = _mainform.Location;
         Settings.Default.SizeMain = _mainform.Size;
         Settings.Default.PosSplitOne = splitContainer1.SplitterDistance;
         Settings.Default.PosSplitTwo = splitContainer2.SplitterDistance;

         // (B.4) Finally save
         Settings.Default.Save();

         // (C) Close all open QueryForm Tabs [seq 20130709°1211]
         // Note : Compare method 20130704°1251 button_Queryform_Close_Click()
         TabControl.TabPageCollection tpc = tabcontrolMain.TabPages;
         string sDebug = "Form closing, TabPages found:" + IOBus.Gb.Bricks.Cr;
         QueryForm[] arQueryforms = { };
         foreach (TabPage tp in tpc)
         {
            sDebug += IOBus.Gb.Bricks.Cr + " - " + tp.Name;
            Control.ControlCollection cc = tp.Controls;
            foreach (Control c in cc)
            {
               QueryForm qf = c as QueryForm;

               // This is a tabpage with a connection
               //if (c is QueryForm qf) // [line 20190410°0631] refactored by VS2017 from "QueryForm qf = c as QueryForm; if (qf != null) { ...", but SharpDev does not like this refactoring
               if (qf != null)
               {
                  sDebug += qf.DbClient.ConnSettings.Description; // the tabpage name
                  Array.Resize(ref arQueryforms, arQueryforms.Length + 1);
                  arQueryforms[arQueryforms.Length - 1] = qf;
               }
            }
         }
         sDebug += IOBus.Gb.Bricks.CrCr + "(Debug 20130709°1212)";
         if (Globs.Debag.DebugMessage_MainForm_Close)
         {
            MessageBox.Show(sDebug);
         }
         for (int i = 0; i < arQueryforms.Length; i++)
         {
            if (IOBus.Gb.Debag.Execute_No)
            {
               // Try to fight issue 20130828°1531 .. does not help [seq 20130828°1532]
               //  Find index (after line 20130620°1135)
               int settingIndex = ServerList_.IndexOf(arQueryforms[i].DbClient.ConnSettings.Key);
               ConnSettingsGui csGuiHlp = ConnSettingsGui.convertSettingsLibToGui(arQueryforms[i].DbClient.ConnSettings);
               if (settingIndex >= 0)
               {
                  ServerList_.Items[settingIndex].Status = ConnSettingsGui.ConnStatus.Connected;
                  QueryPonyGui.Properties.Settings.Default.ServerList = ServerList_;
                  QueryPonyGui.Properties.Settings.Default.Save();
               }
            }

            // Close QueryForm so it's thread vanishes properly
            // Note : Remember issue 20130619°0551 'Undisposed processes', finished 20130709°1221.
            // Note : Compare method 20130604°0614 MainForm._mainform.DoDisconnect()
            //    to a get an idea what's going on in other closing situations.
            arQueryforms[i].Close(); // this helps against issue 20130619°0551 'orphan processes'
         }
      }

      /// <summary>This eventhandler after(?) the main form has opened and the controls are initialized</summary>
      /// <remarks>id : 20130604°0630</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void MainForm_Load(object sender, EventArgs e)
      {
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°0631</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void MainForm_DragDrop(object sender, DragEventArgs e)
      {
         object data;
         data = e.Data.GetData(DataFormats.FileDrop);
         List<string> fileNames = new List<string>();
         foreach (string filename in (string[])data)
         {
            FileAttributes attribs;
            attribs = System.IO.File.GetAttributes(filename);

            if ((attribs & FileAttributes.Directory) != FileAttributes.Directory)
               fileNames.Add(filename);
         }

         if (fileNames.Count > 0)
         {
            LoadFileNames(fileNames);
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0632</remarks>
      /// <param name="fileNames">...</param>
      private void LoadFileNames(List<string> fileNames)
      {
         foreach(string fileName in fileNames)
         {
            LoadFileName(fileName);
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°0633</remarks>
      /// <param name="fileName">...</param>
      private void LoadFileName(string fileName)
      {
         // [seq 20130622°0932]
         if (Glb.Debag.Execute_Yes)
         {
            System.Diagnostics.Debugger.Break();
         }

         if (! IsChildActive())
         {
            DoConnect_POSSIBLY_REVIVE();
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            IQueryForm newQF = GetQueryChild();

            // could be null if new connection failed
            if (newQF != null)
            {
               newQF.Open(fileName);
               mruMenuManager1.Add(fileName);
            }

            Cursor = oldCursor;
         }
         else
         {
            // Change the cursor to an hourglass while we're doing this,
            //  in case establishing the new connection takes some time
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            IQueryForm newQF = GetQueryChild().Clone();

            // Could be null if new connection failed
            if (newQF != null)
            {
               ((Form)newQF).MdiParent = this;
               newQF.PropertyChanged += new EventHandler<EventArgs>(qf_PropertyChanged);
               newQF.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(qf_MRUFileAdded);
               newQF.Open(fileName);
               ((Form)newQF).Show();
               mruMenuManager1.Add(fileName);
            }
            Cursor = oldCursor;
         }
      }

      /// <summary>This method exports the serverlist as Xml file</summary>
      /// <remarks>id : 20130604°0634</remarks>
      /// <param name="filename">Path to save the file to.</param>
      public void ExportServerlist(string filename)
      {
         XmlSerializer serializer = new XmlSerializer(typeof(ServerList));
         FileStream fileStream = new FileStream(filename, FileMode.Create);
         serializer.Serialize(fileStream, ServerList_);
         fileStream.Close();
      }

      /// <summary>This method imports the serverlist from an Xml file</summary>
      /// <remarks>
      /// id : 20130604°0635
      /// todo : Reconsider the exact mechanism. If the old ServerList is plainly
      ///    replaced by a new one, then as well the ConnectionCombobox should be
      ///    completely replaced. Means: first empty the combobox and as well the
      ///    tabpages, then refill them. [todo 20130810°1142]
      /// </remarks>
      /// <param name="filename">The Path to the file.</param>
      public void ImportServerlist(string filename)
      {
         XmlSerializer serializer = new XmlSerializer(typeof(ServerList));
         FileStream fileStream = new FileStream(filename, FileMode.Open);
         ServerList_ = serializer.Deserialize(fileStream) as ServerList;
         fileStream.Close();

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // Make new ServerList visible in the Connections Combobox [seq 20130810°1131]

         // (.1) Get handle for the Connect TabPage [seq 20130810°1133]
         // Note : Compare sequence 20130725°1423 and possibly merge the two.
         TabPage tabpageConnect = null;
         foreach (TabPage tabpage in MainForm._maintabcontrol.TabPages)
         {
            if (tabpage.Text == "Connect")
            {
               tabpageConnect = tabpage;
               break;
            }
         }

         // (.2) Get handle for the Connect Form
         System.Windows.Forms.Control.ControlCollection coll = tabpageConnect.Controls;
         int iDebug = coll.Count;
         Control c = coll[0];
         Panel panel = c as Panel;
         System.Windows.Forms.Control.ControlCollection coll2 = panel.Controls;
         ConnectForm connectform = coll2[0] as ConnectForm;

         // (.3) Fill the combobox
         // Todo : Possibly merge the Connection Combobox filling sequences to one
         //    place. The most object oriented way to do so were to implement that
         //    functionality into the ServerList class. [todo 20130810°1132]
         for (int i = 0; i < ServerList_.Items.Length; i++)
         {
            connectform.ComboboxConnectionAddItem(ServerList_.Items[i]);
         }
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°0636</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void MainForm_DragEnter(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent("FileDrop"))
         {
            e.Effect = DragDropEffects.Copy;
         }
         else
         {
            e.Effect = DragDropEffects.None;
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°0637</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void mruMenuManager1_MruMenuItemClick(object sender, MRUSampleControlLibrary.MruMenuItemClickEventArgs e)
      {
         LoadFileName(e.Filename);
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°0638</remarks>
      private void mruMenuManager1_MruMenuItemFileMissing(object sender, MRUSampleControlLibrary.MruMenuItemFileMissingEventArgs e)
      {
         e.RemoveFromMru = false;
      }

      /// <summary>This eventhandler processes the 'File - Settings' main menu item selection</summary>
      /// <remarks>id : 20130604°0639</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Gui.SettingsForm form = new Gui.SettingsForm();

         if (Globs.Behavior.OpenSettingsFormInModalDialog)
         {
            form.ShowMyDialog(this); // (compare issue 20130604°132402 in SettingsForm.ShowMyDialog())

            foreach (var mdiChild in MdiChildren)
            {
               ((QueryForm)mdiChild).ResetHighlightColors();
            }
         }
         else
         {
            // Open Settings Form as form-on-tab [seq 20130809°1512]
            FurnishFormOnTab(form);
         }
      }

      /// <summary>This eventhandler processes the 'File - Export ServerList to File' main menu item selection</summary>
      /// <remarks>id : 20130604°0641</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void exportServerListToolStripMenuItem_Click(object sender, EventArgs e)
      {
         SaveFileDialog f = new SaveFileDialog();
         f.FileName = Settings.Default.LastExportPath;
         f.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
         if ((f.ShowDialog(this) != DialogResult.Cancel))
         {
            Settings.Default.LastExportPath = f.FileName;
            ExportServerlist(f.FileName);
         }
      }

      /// <summary>This eventhandler processes the 'File - Import ServerList from File' main menu item selection</summary>
      /// <remarks>id : 20130604°0642</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void importServerListToolStripMenuItem_Click(object sender, EventArgs e)
      {
         OpenFileDialog f = new OpenFileDialog();
         f.FileName = Settings.Default.LastImportPath;
         f.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
         if ((f.ShowDialog(this) != DialogResult.Cancel))
         {
            Settings.Default.LastImportPath = f.FileName;
            ImportServerlist(f.FileName);
         }
      }

      // ------------------------------------------------------
      // Below the newly introduced methods for the Forms-on-Tabs feature (20130618°0423)
      // ------------------------------------------------------

      /// <summary>This method creates a new Connection Form and a new Connection Tab, an puts the form on a tab</summary>
      /// <remarks>
      /// id : 20130618°0401 (20130604°0612)
      /// note : Compare 20130111°0621.
      /// </remarks>
      private void FurnishConnectTab()
      {
         // Create a Connect Form
         ConnectForm cf = new ConnectForm(_furnishNewConnection, _outputToStatusLine);

         // Prepare mandatory form properties
         cf.TopLevel = false;
         cf.Location = new System.Drawing.Point(5, 5);
         cf.FormBorderStyle = FormBorderStyle.None;
         cf.Visible = true;

         // Put ConnectForm on MainTabcontrol
         panelConnect.Controls.Add(cf);

         // [condition 20130618°040102]
         if (Glb.Debag.ShutdownFeatureMdiWindows)
         {
            int settingIndex = ServerList_.IndexOf(cf.DbClient.ConnSettings.Key);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // [seq 20130620°1133]
            ConnSettingsGui csGui = ConnSettingsGui.convertSettingsLibToGui(cf.DbClient.ConnSettings);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            if (settingIndex >= 0)
            {
               ServerList_.Items[settingIndex] = (csGui);
            }
            else
            {
               ServerList_.Add(csGui);
            }

            SaveServerList(); // shifted behind the MDI sequece

            QueryForm qf = new QueryForm(cf.DbClient, false); //, cf.Browser, cf.LowBandwidth);
            qf.MdiParent = this;

            // This is so that we can update the toolbar and menu as the state of the QueryForm changes
            qf.PropertyChanged += new EventHandler<EventArgs>(qf_PropertyChanged);

            qf.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(qf_MRUFileAdded);
            qf.WindowState = FormWindowState.Maximized;
            qf.Show();
            return;
         }

         return;
      }

      /// <summary>This method opens any form on tab</summary>
      /// <remarks>
      /// id : 20130809°1511 (20130618°0401)
      /// note : Partially copied from method 20130618°0412 furnishNewConnectionDelegateImplementation().
      /// callers : So far only ...
      /// </remarks>
      /// <param name="form">The form to place on a tab (so far only the Settings Form).</param>
      private void FurnishFormOnTab(Form fm)
      {
         // Prepare mandatory form properties
         fm.TopLevel = false;
         fm.Location = new System.Drawing.Point(5, 5);
         fm.FormBorderStyle = FormBorderStyle.None;
         fm.Visible = true;

         // Supplement event handlers
         /*
         // Note : This is so that we can update the toolbar and menu as the state of the QueryForm changes
         fm.PropertyChanged += new EventHandler<EventArgs>(_mainform.qf_PropertyChanged);
         fm.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(_mainform.qf_MRUFileAdded);
         */

         // Set optional child properties
         fm.Dock = DockStyle.Fill;

         // Decorate tabpage [line 20130724°0813]
         // Remember issue 20130702°1502 'Best way to tag objects for interconnection'
         TabPage tabpage = new TabPage();
         tabpage.Text = "Settings";                                            // Formerly "client.ConnSettings.LabelTabpageDatabase;"

         // Bring prepared control to live
         tabpage.Controls.Add(fm);
         _maintabcontrol.Controls.Add(tabpage);

         // issue 20130809°1521 ''
         // Description : Below SelectedIndex setting ignits EnableControls(), which wants
         //  the eventhandlers PropertyChanged and MRUFileAdded, which are implemented in
         //  the Query Form. Not sure whether those eventhandlers are useful with the
         //  Settings Form, but the SelectedIndex is. So we write sequence 20130809°1522
         //  into EnableControls() to manage that situation. The eventhandlers PropertyChanged
         //  and MRUFileAdded are prepared anyway but not used yet (20130809°1514/1515).

         // / *
         // Postprocessing - navigate to the new tab
         int iIndex = _maintabcontrol.Controls.Count - 1;
         _maintabcontrol.SelectedIndex = iIndex;
         // * /

         // Breakpoint
         System.Threading.Thread.Sleep(1);

         return;
      }

      /// <summary>This delegate declaration declares the FurnishNewConnection delegate</summary>
      /// <remarks>id : 20130618°0413</remarks>
      /// <param name="dbclient">The database client the new TabPage shall display</param>
      /// <param name="connSettingsLib">The library connection setting the new TabPage shall be tagged with</param>
      private delegate void FurnishNewConnection(DbClient dbclient, ConnSettingsLib connSettingsLib);

      /// <summary>This field instantiates the FurnishNewConnection delegate</summary>
      /// <remarks>id : 20130618°0414</remarks>
      private FurnishNewConnection _furnishNewConnection = new FurnishNewConnection(furnishNewConnectionDelegateImplementation);

      /// <summary>This field declares the OutputToStatusLine delegate</summary>
      /// <remarks>id : 20130716°0632</remarks>
      /// <param name="dbclient">The message to be ouput</param>
      private delegate void OutputToStatusLine(string sText);

      /// <summary>This field declares the OutputToStatusChar delegate</summary>
      /// <remarks>id : 20130821°0932</remarks>
      /// <param name="dbclient">The message to be ouput</param>
      private delegate void OutputToStatusChar(string sText);

      /// <summary>This field instantiates the _outputToStatusChar delegate</summary>
      /// <remarks>id : 20130716°0633</remarks>
      private OutputToStatusChar _outputToStatusChar = new OutputToStatusChar(outputStatusChar);

      /// <summary>This field instantiates the _outputToStatusLine delegate</summary>
      /// <remarks>id : 20130716°0633</remarks>
      private OutputToStatusLine _outputToStatusLine = new OutputToStatusLine(outputStatusLine);

      /// <summary>This static field represents the main tabcontrol, where the Connect Form and the Connection Forms are placed</summary>
      /// <remarks>
      /// id : 20130618°0415
      /// note : This exists as workaround(?) because the delegate implementation seems to must be static,
      ///    and thus there is no direct access to the tabcontrol from there. (workaround 20130618°0416)
      /// </remarks>
      internal static TabControl _maintabcontrol = null;

      /// <summary>This method creates a new tab on the main tabcontrol and places the given connection on it</summary>
      /// <remarks>
      /// id : 20130618°0412
      /// callers : E.g. sequence 20130618°041103 in the ConnectForm's Connect button Click event
      /// </remarks>
      /// <param name="client">The database client the new TabPage shall display</param>
      /// <param name="connSettingsLib">The library connection settings the new TabPage shall be tagged with</param>
      public static void furnishNewConnectionDelegateImplementation(DbClient client, ConnSettingsLib connSettingsLib)
      {
         // the original MDI sequence which is replaced by below new code (20130618°0541)
         // note : Don't throw away this sequence for possible recycling in case of restoring the MDI feature.
         if (Glb.Debag.ShutdownFeatureMdiWindows)
         {
            QueryForm qfDummy = new QueryForm(client, false);                  // , cf.Browser, cf.LowBandwidth);
            qfDummy.MdiParent = _mainform;                                     // '_mainform' was formerly 'this'

            // This is so that we can update the toolbar and menu as the state of the QueryForm changes
            qfDummy.PropertyChanged += new EventHandler<EventArgs>(_mainform.qf_PropertyChanged);
            qfDummy.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(_mainform.qf_MRUFileAdded);
            qfDummy.Show();
         }

         QueryForm qf = new QueryForm(client, true);

         // Set mandatory child properties
         qf.TopLevel = false;
         qf.Location = new System.Drawing.Point(5, 5);
         qf.FormBorderStyle = FormBorderStyle.None;
         qf.Visible = true;

         // Supplement event handlers
         // note : this is so that we can update the toolbar and menu as the state of the QueryForm changes
         qf.PropertyChanged += new EventHandler<EventArgs>(_mainform.qf_PropertyChanged);
         qf.MRUFileAdded += new EventHandler<MRUFileAddedEventArgs>(_mainform.qf_MRUFileAdded);

         // Set optional child properties
         qf.Dock = DockStyle.Fill;

         TabPage tabpage = new TabPage();

         // Tag tabpage (line 20130724°0814)
         // note : See issue 20130702°1502 'Best way to tag objects for interconnection'.
         tabpage.Tag = connSettingsLib;

         // Decorate tabpage (line 20130724°0812)
         tabpage.Text = client.ConnSettings.LabelTabpageDatabase;

         // Bring prepared control to live
         tabpage.Controls.Add(qf);
         _maintabcontrol.Controls.Add(tabpage);

         // Postprocessing — navigate to the new tab
         int iIndex = _maintabcontrol.Controls.Count - 1;
         _maintabcontrol.SelectedIndex = iIndex;

         // Breakpoint
         System.Threading.Thread.Sleep(1);

         return;
      }

      /// <summary>This eventhandler processes the main treeview's BeforeExpand event to add subnodes to the given node</summary>
      /// <remarks>id : 20130701°1131 (20130604°2205)</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void treeviewMain_BeforeExpand(object sender, TreeViewCancelEventArgs e)
      {
         // The origianl treeview's BeforeExpand event lines
         /*
         // If a browser has been installed, see if it has a sub object
         //  hierarchy for us at the point of expansion
         if (Browser == null) { return; }

         // Retrieve the basic nodes for the specific database and node type
         TreeNode[] subtree = Browser.GetSubObjectHierarchy(e.Node);
         if (subtree != null)
         {
            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(subtree);
         }
         */

         // Possibly create sub-nodes (code is outsourced to MainTv.cs)
         MainTv.BeforeExpand(TreeviewMain, (TreeNode)e.Node);

         return;
      }

      /// <summary>This eventhandler processes the main treeview's ItemDrag event</summary>
      /// <remarks>id : 20130701°1132 (20130604°2206)</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void treeviewMain_ItemDrag(object sender, ItemDragEventArgs e)
      {
         // The original treeview's ItemDrag event lines
         /*
         // Allow objects to be dragged from the browser to the query textbox.
         if (e.Button == MouseButtons.Left && e.Item is TreeNode)
         {
            // Ask the browser object for a string applicable to dragging onto the query window.
            string dragText = Browser.GetDragText((TreeNode)e.Item);
            // We'll use a simple string-type DataObject
            if (dragText != "")
            {
               treeView.DoDragDrop(new DataObject(dragText), DragDropEffects.Copy);
            }
         }
         */

         // Allow objects to be dragged from the browser to the query textbox.
         if (e.Button == MouseButtons.Left && e.Item is TreeNode)
         {
            // Code outsourced to MainTv.cs
            MainTv.ItemDrag(TreeviewMain, (TreeNode)e.Item);
         }
      }

      /// <summary>This eventhandler processes the main treeview's MouseDown event</summary>
      /// <remarks>id : 20130701°1133 (20130604°2202)</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void treeviewMain_MouseDown(object sender, MouseEventArgs e)
      {
         // The original treeview's MouseDown event lines
         /*
         // When right-clicking, first select the node under the mouse.
         if (e.Button == MouseButtons.Right)
         {
            TreeNode tn = treeView.GetNodeAt(e.X, e.Y);
            if (tn != null)
            {
               treeView.SelectedNode = tn;
            }
         }
         */

         // When right-clicking, first select the node under the mouse.
         if (e.Button == MouseButtons.Right)
         {
            TreeNode tn = TreeviewMain.GetNodeAt(e.X, e.Y);
            if (tn != null)
            {
               TreeviewMain.SelectedNode = tn;
            }
         }
      }

      /// <summary>This eventhandler processes the main treeview's MouseUp event to present the context menu for the given treeview node</summary>
      /// <remarks>id : 20130701°1134 (20130604°2203)</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void treeviewMain_MouseUp(object sender, MouseEventArgs e)
      {

         // The lines from the original treeview's MouseUp event
         /*
         if (Browser == null) { return; }

         // Display a context menu if the browser has an action list for the selected node
         if (e.Button == MouseButtons.Right && treeView.SelectedNode != null)
         {
            StringCollection actions = Browser.GetActionList(treeView.SelectedNode);
            if (actions != null)
            {
               System.Windows.Forms.ContextMenu cm = new ContextMenu();
               foreach (string action in actions)
               {
                  cm.MenuItems.Add(action, new EventHandler(DoBrowserAction));
               }
               cm.Show(treeView, new Point(e.X, e.Y));
            }
         }
         */

         // Display the context menu (code outsourced to MainTv.css)
         if (e.Button == MouseButtons.Right && TreeviewMain.SelectedNode != null)
         {
            MainTv.MouseUpRight(e, TreeviewMain);
         }
         return;

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // Debug [note 20130702°1531]
         // Problem : The context menu does only work for some databases (the first
         //    created). Below, the correct queryform.Browser must be addressed!
         // Check : Who maintains above _iNdxQueryform? This field might be the culprit.
         // Answer : Nobody does that yet. _iNdxQueryform is always 0!
         // Idea : The method just written to find the TabPage corresponding to the selected
         //    treeview Database node, that might be the right place.
         // Note : And it looks as if the database which has no context menu, also has
         //    no fields in the hierarchy under the table.
         // Proposal : Begin with treeviewMain_BeforeExpand() and rip out _queryforms[].
         //    Replace it with some real time recognition a la searchCorrespondingTabPage().
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      }

      /// <summary>This eventhandler processes the main treeview's AfterSelect event to synchronize the connection tabs etc</summary>
      /// <remarks>id : 20130701°1401</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void treeviewMain_AfterSelect(object sender, TreeViewEventArgs e)
      {
         TreeView tv = sender as TreeView;
         TreeNode tn = tv.SelectedNode;

         // Code outsourced to MainTv.cs
         MainTv.AfterSelect(tv, tn);
      }

      /// <summary>This field stores a helper variable to count the output lines</summary>
      /// <remarks>
      /// id : 20130715°1311
      /// note : The output line number serves just to reduce confusion of the
      ///    user in case identical output messages come one after the other.
      /// </remarks>
      private static int _iOutputLineCounter = 0;

      /// <summary>This method outputs to the status textbox without a newline</summary>
      /// <remarks>id : 20130821°0931</remarks>
      /// <param name="sMsg">The line to output</param>
      public static void outputStatusChar(string sMsg)
      {
         MainForm.TextboxStatus.AppendText(sMsg);
      }

      /// <summary>This method outputs a line to the status textbox</summary>
      /// <remarks>id : 20130713°0931</remarks>
      /// <param name="sMsg">The line to output</param>
      public static void outputStatusLine(string sMsg)
      {
         _iOutputLineCounter++;
         sMsg = _iOutputLineCounter.ToString() + ": " + sMsg;
         if (_iOutputLineCounter > 1)
         {
            sMsg = IOBus.Gb.Bricks.Cr + sMsg;
         }
         MainForm.TextboxStatus.AppendText(sMsg);
      }

      /// <summary>This eventhandler processes the Main TabControls SelectedIndexChanged event</summary>
      /// <remarks>
      /// id : 20130725°1531
      /// note : This cares about adjusting the Main Menu Items, notably, that
      ///    the Close Event is active when a Connection Tab is selected.
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void tabcontrolMain_SelectedIndexChanged(object sender, EventArgs e)
      {
         EnableControls();
      }

      /// <summary>
      /// This method is a dummy to be accessed from a host program
      ///  to make it's reference viable for the compiler.
      /// </summary>
      /// <remarks>
      /// id : 20130814°0913
      /// note : Just accessing this method solves issue 20130814°0911.
      /// </remarks>
      /// <returns>A dummy string</returns>
      public void hello()
      {
         return;
      }

      /// <summary>This method establishes a connection</summary>
      /// <remarks>
      /// id : 20130828°1521
      /// note : This method is introduced to automatically open connections.
      /// note : This code is largely duplicated here from method 20130618°0411
      ///    button_Connect_Click(), possibly to be used from there.
      ///    See • Todo 20130828°1522 and • Todo 20130828°1523
      /// </remarks>
      /// <param name="csGui">The connection settings for the connection to open</param>
      /// <param name="sErr">The possible error message to output or blank</param>
      /// <returns>Success flag</returns>
      public bool establishConnection(ConnSettingsGui csGui, out string sErr)
      {
         sErr = "";
         ConnSettingsGui _conSettings = csGui;                                 // For now mimic fields from 20130618°0411
         DbClient _client = null;                                              // For now mimic fields from 20130618°0411

         // Create the engine connection settings [seq 20130620°1613]
         ConnSettingsLib csLib = ConnSettingsGui.convertSettingsGuiToLib(_conSettings);

         // Paranoia [seq 20130620°1613]
         if (!DbClientFactory.ValidateSettings(csLib))
         {
            sErr = "Connection settings invalid: " + _conSettings.ConnIdString();
            MainForm.outputStatusLine(sErr);
            return false;
         }

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // Create the connection [seq 20130618°0351`01]
         // See todo 20130828°1522 'Combine 4 connect dbClient seqences into one method'

         // Retrieve the client object
         _client = DbClientFactory.GetDbClient(csLib);

         // Waiting phase start
         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;
         SplashConnecting c = new SplashConnecting();
         c.Show(this);
         c.Refresh();

         // Make the connection
         bool bSuccess = _client.Connect();

         // Waiting phase end
         c.Close();
         Cursor = oldCursor;

         // All right?
         if (! bSuccess)
         {
            sErr = "Unable to connect: " + _client.ErrorMessage + " " + "[Error 20130717°1245]";

            // Different icons make different sounds [seq 20130725°0912]
            if (IOBus.Gb.Debag.Shutdown_Alternatively)
            {
               // The error icon makes an annoying bang sound
               MessageBox.Show(sErr, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               // The exclamation icon makes a more graceful bling sound
            }

            _client.Dispose();

            // Experiment [line 20130828°1515]
            _conSettings.Status = ConnSettingsGui.ConnStatus.Failed;

            return false;
         }

         // Experiment [line 20130828°1514]
         _conSettings.Status = ConnSettingsGui.ConnStatus.Connected;
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         // Note : Formerly, the caller cared about opening the connection form. Now, with the
         //    ConnectForm on a tab, we have to care about opening the form independently here
         //    via below called delegate. [note 20130618°041102]
         // //DialogResult = DialogResult.OK; // no more useful with the Connect-Form-on-Tab
         // //DialogResult DialogResultELIMINATE = DialogResult.OK; // no more useful with the Connect-Form-on-Tab

         // Establish a GUI for the new connection [seq 20130618°0411`03] new style
         // Note : The delegate implementation finally being called is method 20130618°0412
         //    MainForm::furnishNewConnectionDelegateImplementation(). (Use 'Find' with the
         //    method id to navigate there, that is much easier than with 'Go To Definition'.)
         object[] aro = { _client, csLib };
         this.Invoke(this._furnishNewConnection, aro);

         return true;
      }
   }
}
