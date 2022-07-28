#region Fileinfo
// file        : 20130604°2001 /QueryPony/QueryPonyGui/Gui/QueryForm.cs
// summary     : This file stores class 'QueryForm' to constitute the Query Form.
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
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QueryPonyGui
{

   /// <summary>This class constitutes the Query Form</summary>
   /// <remarks>id : 20130604°2002</remarks>
   internal partial class QueryForm : Form, IQueryForm
   {

      #region Private Variables

      /// <summary>This enum defines the Result Tab types</summary>
      /// <remarks>id : 20130604°2003</remarks>
      private enum _resultsTabType
      {
         /// <summary>This enum field indicates a message result-type tab</summary>
         /// <remarks>id : 20130604°1951</remarks>
         Message,

         /// <summary>This enum field indicates a message grid result-type tab</summary>
         /// <remarks>id : 20130604°1952</remarks>
         GridResults,

         /// <summary>This enum field indicates a message info-message result-type tab</summary>
         /// <remarks>id : 20130604°1953</remarks>
         InfoMessage
      }

      /// <summary>This private const "yyyy'-'MM'-'dd HH':'mm':'ss.fff" tells a timestamp format string</summary>
      /// <remarks>id : 20130604°2004</remarks>
      private const string s_DateTimeFormatString = "yyyy'-'MM'-'dd HH':'mm':'ss.fff";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2005</remarks>
      private static readonly int _dateTimeFormatStringLength = s_DateTimeFormatString.Length;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2006</remarks>
      private bool _realFileName = false;

      /// <summary>This field stores the filename for when the query is saved</summary>
      /// <remarks>id : 20130604°2007</remarks>
      private string _fileName;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2008</remarks>
      private bool _resultsInText = (! Properties.Settings.Default.ResultInGridDefault);

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2009</remarks>
      private bool _gridShowNulls = QueryPonyGui.Properties.Settings.Default.ShowNulls;

      /// <summary>This field stores the handle to the rich textbox used to display text results</summary>
      /// <remarks>id : 20130604°2011</remarks>
      private RichTextBox _txtResultsBox;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2012</remarks>
      private bool _errorOccured = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2013</remarks>
      private long _rowCount;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2014</remarks>
      private string _lastDatabase;

      /// <summary>This static field stores the postfix number for default new filenames (Untited-1, Untitled-2, etc)</summary>
      /// <remarks>id : 20130604°2015</remarks>
      static int _untitledCount = 1;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2016</remarks>
      int[] _colWidths;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2017</remarks>
      private StringBuilder _textResults = new StringBuilder();

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2018</remarks>
      private DateTime _lastResults;

      /// <summary>This field stores the client specific browser implementation acquired via DbClientFactory.GetBrowser(client)</summary>
      /// <remarks>id : 20130604°2019</remarks>
      private IDbBrowser _browser;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2021</remarks>
      private bool _hideBrowser = true; // initialize as true (20130704°1305)

      /// <summary>This private field stores ...</summary>
      /// <remarks>id : 20130604°2022</remarks>
      private string _lastFindText;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2023</remarks>
      private bool _matchCase;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2024</remarks>
      private int _lastFindPos;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2025</remarks>
      private int _lastFindRow;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2026</remarks>
      private int _lastFindCol;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2027</remarks>
      private Regex _regexSQLKeywords;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2028</remarks>
      private MatchCollection _regexWordsMatchCollection;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2029</remarks>
      private Regex _regexSQLOperators;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2031</remarks>
      private MatchCollection _regexOperatorsMatchCollection;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2032</remarks>
      private Regex _regexSQLStrings;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2033</remarks>
      private MatchCollection _regexStringsMatchCollection;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2034</remarks>
      private Regex _regexSQLNumbers;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2035</remarks>
      private MatchCollection _regexNumbersMatchCollection;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2036</remarks>
      private int _cursorPos;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2037</remarks>
      private bool _ignoreTextChanged;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2038</remarks>
      private string _rtfColorDictionary;

      #region Properties

      /// <summary>
      /// This property gets/sets the database client object used to
      ///  talk to database server. It is provided in construction.
      ///  </summary>
      /// <remarks>
      /// id : 20130604°2039
      /// todo : Having the identical identifyer for property type and property name is not nice. (todo 20130818°1521)
      /// </remarks>
      public DbClient DbClient { get; set; }

      /// <summary>This property gets the result DataSet of a query on this QueryForm</summary>
      /// <remarks>id : 20130604°2041</remarks>
      DataSet DSResults
      {
         get { return DbClient.DataSet; }
      }

      #endregion Properties

      #endregion Private Variables

      #region Constructors

      /// <summary>This constructor creates a plain QueryForm</summary>
      /// <remarks>id : 20130604°2042</remarks>
      private QueryForm()
      {
         InitializeComponent();

         // (seq 20130704°1303)
         this.checkbox_ShowDedicatedTreeview.Checked = (! this.HideBrowser);
      }

      /// <summary>This constructor creates a new QueryForm with the given database client</summary>
      /// <remarks>id : 20130604°2043</remarks>
      /// <param name="dbClient">The database client to display on the form</param>
      /// <param name="bHideBrowser">Flag whether to hide or show the dedicated/legacy treeview</param>
      public QueryForm(DbClient dbClient, bool bHideBrowser) : this()
      {

         this.DbClient = dbClient;
         dbClient.DataReaderAvailable += new EventHandler<DataReaderAvailableEventArgs>(client_DataReaderAvailable);
         dbClient.TableSchemaAvailable += new EventHandler<TableSchemaAvailableEventArgs>(client_TableSchemaAvailable);
         dbClient.DataRowAvailable += new EventHandler<DataRowAvailableEventArgs>(client_DataRowAvailable);
         dbClient.InfoMessage += new EventHandler<InfoMessageEventArgs>(client_InfoMessage);

         //----------------------------------------------------
         // (debug note 20130716°0624) Such eventhandler is wanted also for any
         //  DbClient not coming inside with a QueryForm. (compare issue 20130716°0622)
         //----------------------------------------------------
         dbClient.Error += new EventHandler<ErrorEventQeArgs>(client_Error);

         dbClient.CommandDone += new EventHandler<CommandDoneEventArgs>(client_CommandDone);
         dbClient.BatchDone += new EventHandler(client_batchDone);
         dbClient.CancelDone += new EventHandler(client_cancelDone);
         this._lastDatabase = dbClient.Database; // this is so we know when the current database changes
         this.FileName = "untitled" + _untitledCount++.ToString() + ".sql";
         this._browser = DbClientFactory.GetBrowser(dbClient);
         this.HideBrowser = bHideBrowser;

         string[] keywordArr = Properties.Settings.Default.SQLKeyWords.Replace("\n", "").Split('\r');

         string keywordRegEx = @"(\b(";
         foreach (string keyword in keywordArr)
         {
            keywordRegEx += string.Format("{0}|", keyword);
         }
         keywordRegEx = keywordRegEx.Substring(0, keywordRegEx.Length - 1);
         keywordRegEx += @")\b)|('([^']+)?'?)";

         // this is a workaround
         // note : Appending |('([^']+)?'?) gives me the thing I'm actually looking for
         //    (keywords, numbers etc) and the strings used in the text (i.e. text enclosed
         //    in ' characters). I've done this because I couldn't for the life of me figure
         //    out how to make a regex that gives me everything except those "string areas",
         //    and I was fed up with this. [note 20130604°204302]
         string operatorRegEx = @"(\||\+|\-|\*|\/|\(|\)|\[|\]|\{|\}|\<|\>|\=|\.|\,)|('([^']+)?'?)";

         this._regexSQLKeywords = new Regex(keywordRegEx, RegexOptions.IgnoreCase);
         this._regexSQLOperators = new Regex(operatorRegEx);
         this._regexSQLStrings = new Regex(@"('([^']+)?'?)");
         this._regexSQLNumbers = new Regex(@"(\b\d+\b)|('([^']+)?'?)");

         this._rtfColorDictionary = "{\\colortbl ;"
                                   + GetColorString(Properties.Settings.Default.ColorKeywords)
                                    + GetColorString(Properties.Settings.Default.ColorStrings)
                                     + GetColorString(Properties.Settings.Default.ColorOperators)
                                      + GetColorString(Properties.Settings.Default.ColorNumbers)
                                       + "}"
                                        ;

         //----------------------------------------------------
         // [seq 20130818°1503]

         // prepare descriptive combobox
         this.combobox_ClonetoDbtype.FormattingEnabled = true;
         this.combobox_ClonetoDbtype.Format += delegate(object sender, ListControlConvertEventArgs e)
         {
            e.Value = EnumExtensions.Description((QueryPonyLib.ConnSettingsLib.ConnectionType)e.Value);
         };
         this.combobox_ClonetoDbtype.Items.Add(QueryPonyLib.ConnSettingsLib.ConnectionType.Mssql);
         this.combobox_ClonetoDbtype.Items.Add(QueryPonyLib.ConnSettingsLib.ConnectionType.Mysql);
         this.combobox_ClonetoDbtype.Items.Add(QueryPonyLib.ConnSettingsLib.ConnectionType.Pgsql);
         this.combobox_ClonetoDbtype.Items.Add(QueryPonyLib.ConnSettingsLib.ConnectionType.Sqlite);

         this.textbox_CloneFrom.Text = dbClient.ConnSettings.Description; // the combobox-dedicated ToString() override exists only for ConnSettingsGui
         this.textbox_ClonetoDatabase.Text = dbClient.ConnSettings.DatabaseName;
         this.textbox_ClonetoServer.Text = "";

         // debug service
         this.combobox_ClonetoDbtype.SelectedItem = this.combobox_ClonetoDbtype.Items[3];
         this.textbox_ClonetoServer.Text = "d:\\tmp\\";
         this.textbox_ClonetoDatabase.Text = "test.sqlite3";

         // (.) visibilities (20130828°1601)
         // (.1) clone panel
         this.panel_DbClone.Visible = false;
         if ((this.DbClient.ConnSettings.Type == ConnSettingsLib.ConnectionType.OleDb)
             || QueryPonyGui.Properties.Settings.Default.ShowDeveloperObjects
              )
         {
            this.panel_DbClone.Visible = true;
         }

         // (.2) options tabpage
         if (!QueryPonyGui.Properties.Settings.Default.ShowDeveloperObjects)
         {
            this.tabcontrol_Queryform.TabPages.Remove(this.tabpage_Options);
         }
         //----------------------------------------------------

      }

      #endregion Constructors

      #region DbClient eventhandlers

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2044</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_DataReaderAvailable(object sender, DataReaderAvailableEventArgs e)
      {
         if (! ResultsInText)
         {
            DisplayGrid(e.dr); // (breakpoint 20130822.2112xx) debugging issue 20130822°2111 'DbClient triggers QueryForm methods'
            e.SkipResults = true;
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2045</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_TableSchemaAvailable(object sender, TableSchemaAvailableEventArgs e)
      {
         _rowCount = 0;
         if (ResultsInText)
         {
            DisplayTextSchema(e.Schema);
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2046</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_DataRowAvailable(object sender, DataRowAvailableEventArgs e)
      {
         _rowCount++;
         if (ResultsInText)
         {
            DisplayTextRow(e.DataFields);
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2047</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_CommandDone(object sender, CommandDoneEventArgs e)
      {
         if (e.RecordsAffected >= 0)
         {
            AppendTextResults(string.Format("\r\n   ({0} row(s) affected)", e.RecordsAffected), true);
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2048</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_batchDone(object sender, EventArgs e)
      {
         // do this asyncroneously, so worker thread can go back to beeing idle, we are done anyways
         BeginInvoke(new MethodInvoker(doBatchDone));
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2049</remarks>
      private void doBatchDone()
      {
         panRunStatus.Text = "Query batch completed" + (_errorOccured ? " with errors" : ".");

         // if there were no results from query, display message to provide feedback to user
         if (! _errorOccured)
         {
            AppendTextResults("\r\nThe command(s) completed successfully.", true);
         }
         if (! ResultsInText)
         {
            tabcontrol_Results.SelectedIndex = _errorOccured ? 0 : 1;
         }
         ShowRowCount();
         ShowExecTime();
         SetRunning(false);
         richtextbox_Query.Focus();
      }

      /// <summary>This eventhandler processes the Error event of this QueryForm</summary>
      /// <remarks>id : 20130604°2051</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_Error(object sender, ErrorEventQeArgs e)
      {
         AppendTextResults("\r\n" + e.ErrorMessage, true);
         IOBusConsumer.writeHost(e.ErrorMessage);
         _errorOccured = true;
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2052</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_InfoMessage(object sender, InfoMessageEventArgs e)
      {
         AppendTextResults("\r\n" + e.Message, true);
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2053</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      void client_cancelDone(object sender, EventArgs e)
      {
         // do this asyncroneously, so worker thread can go back to beeing idle, we are done anyways
         BeginInvoke(new MethodInvoker(doCancelDone));
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2054</remarks>
      private void doCancelDone()
      {
         SetRunning(false);
         panRunStatus.Text = "Query batch was cancelled.";
         ShowExecTime();
      }

      #endregion DbClient eventhandlers

      #region IQueryForm implementation

      /// <summary>This public field stores the eventhandler for this form's PropertyChanged event</summary>
      /// <remarks>id : 20130604°2055</remarks>
      public event EventHandler<EventArgs> PropertyChanged;

      /// <summary>This public field stores the eventhandler for this form's MRUFileAdded event</summary>
      /// <remarks>id : 20130604°2056</remarks>
      public event EventHandler<MRUFileAddedEventArgs> MRUFileAdded;

      /// <summary>This method returns false if the Open was cancelled or if the file I/O failed</summary>
      /// <remarks>id : 20130604°2057</remarks>
      /// <returns>Success flag</returns>
      public bool Open()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.FileName = "*.sql";
         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            string f = openFileDialog.FileName;
            if (Path.GetExtension(f) == "") { f += ".sql"; }
            return Open(f);
         }
         else
         {
            return false;
         }
      }

      /// <summary>This method returns false if the 'Open' was cancelled or if the file I/O failed</summary>
      /// <remarks>id : 20130604°2058</remarks>
      /// <param name="fileName">The file to open</param>
      /// <returns>Succes flag</returns>
      public bool Open(string fileName)
      {
         return OpenFile(fileName);
      }

      /// <summary>This method starts execution of a query</summary>
      /// <remarks>id : 20130604°2059</remarks>
      public void Execute()
      {
         if (RunState != DbClient.RunStates.Idle)
         {
            return;
         }

         if (this.DbClient.DataSet == null)
         {
            this.DbClient.DataSet = new DataSet();
         }
         else
         {
            this.DbClient.DataSet.Tables.Clear();
         }

         _errorOccured = false;
         _rowCount = 0;

         // delete any previously defined tab pages and their child controls
         CreateResultsTextbox();

         // if the user has selected text within the query window, just execute the
         //  selected text, otherwise, execute the contents of the whole textbox
         string query = richtextbox_Query.SelectedText.Length == 0
                       ? richtextbox_Query.Text
                        : richtextbox_Query.SelectedText
                         ;

         if (query.Trim() == "") { return; }

         // use the database client class to execute the query, create delegates
         //  which will be invoked when the query completes or cancels with an error

         if (_resultsInText)
         {
            // results = new MethodInvoker (AddTextResults);
         }
         else
         {
            // results = new MethodInvoker (AddGridResults);
         }

         // possible results (?)
         //  - done = new MethodInvoker (QueryDone);
         //  - failed = new MethodInvoker (QueryFailed);
         //  - infoMessageHandler = new InfoMessageEventHandler(OnInfoMessage);
         // dbClient.Execute runs asynchronously, so control will return immediately to the calling method.

         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;
         panRunStatus.Text = "Executing Query Batch...";
         DbClient.Execute(query); // this does the work
         SetRunning(true);
         Cursor = oldCursor;
      }

      /// <summary>This method creates a Results Textbox</summary>
      /// <remarks>id : 20130604°2101</remarks>
      private void CreateResultsTextbox()
      {
         tabcontrol_Results.TabPages.Clear();

         TabPage tabPage = new TabPage(_resultsInText ? "Results" : "Messages");

         // we'll need a rich textbox because an ordinary textbox has limited capacity
         _txtResultsBox = new RichTextBox();
         _txtResultsBox.AutoSize = false;
         _txtResultsBox.Dock = DockStyle.Fill;
         _txtResultsBox.Multiline = true;
         _txtResultsBox.WordWrap = false;
         _txtResultsBox.Font = new Font("Courier New", 8);
         _txtResultsBox.ScrollBars = RichTextBoxScrollBars.Both;
         _txtResultsBox.MaxLength = 0;
         _txtResultsBox.Text = "";
         tabPage.Tag = _resultsTabType.Message;

         #region Create Context Menu

         _txtResultsBox.ContextMenuStrip = EditManager.GetEditManager().GetContextMenuStrip(this._txtResultsBox);
         _txtResultsBox.ContextMenuStrip.Opening += delegate(object o, System.ComponentModel.CancelEventArgs e)
                                                                                  { this._txtResultsBox.Focus(); };

         #endregion Create Context Menu

         tabcontrol_Results.TabPages.Add(tabPage);
         tabPage.Controls.Add(_txtResultsBox);
      }

      /// <summary>This method cancels the running operation</summary>
      /// <remarks>id : 20130604°2102</remarks>
      public void Cancel()
      {
         panRunStatus.Text = "Cancelling...";
         DbClient.CancelAsync();

         // control will return immediately, and CancelDone will be invoked when the cancel is complete
         NotifyPropertyChanged();
      }

      /// <summary>This method returns false if user cancelled or save failed</summary>
      /// <remarks>id : 20130604°2103</remarks>
      /// <returns>Success flag</returns>
      public bool Save()
      {
         bool bRet = false;
         if (! _realFileName)
         {
            bRet = SaveAs();
         }
         else
         {
            bRet = SaveFile(FileName);
         }
         return bRet;
      }

      /// <summary>This method returns false if user cancelled or save failed</summary>
      /// <remarks>id : 20130604°2104</remarks>
      /// <returns>Success flag</returns>
      public bool SaveAs()
      {
         bool bRet = false;

         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.FileName = FileName;
         if (saveFileDialog.ShowDialog() == DialogResult.OK)
         {
            FileName = saveFileDialog.FileName;
            _realFileName = true;
            bRet = SaveFile(FileName);
         }

         return bRet;
      }

      /// <summary>This method returns a copy of the QueryForm object, with separate connection and browser objects</summary>
      /// <remarks>id : 20130604°2105</remarks>
      /// <returns>...</returns>
      public IQueryForm Clone()
      {
         // make a copy of the QueryForm's DbClient object.
         // note : We can't use the same object object because this would mean sharing
         //    the same connection, preventing concurrent queries. [note 20130604°2105]
         DbClient d = DbClient.Clone();
         if (d.Connect())
         {
            d.Database = DbClient.Database;

            // we have to duplicate the Browser too, since it has a reference to the DbClient object
            // // IBrowser b = null;
            // // if (Browser != null) try { b = Browser.Clone(d); }
            // //catch { }

            QueryForm newQF = new QueryForm(d, _hideBrowser);
            newQF.ResultsInText = ResultsInText;

            // supplemented on suspicion (20130717°1241) not tested
            d.Dispose();

            return newQF;
         }
         else
         {
            string s = "Unable to connect: " + d.ErrorMessage + " " + "[Error 20130717°1242]";
            MessageBox.Show(s, "QueryPony", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
         }
      }

      /// <summary>This property gets/sets true if results are return in the text window</summary>
      /// <remarks>id : 20130604°2106</remarks>
      /// <returns>The wanted flag whether the result will be given as text or as table</returns>
      public bool ResultsInText
      {
         get { return _resultsInText; }
         set
         {
            _resultsInText = value;
            NotifyPropertyChanged();
         }
      }

      /// <summary>This property gets/sets true if null values are displayed in the grid with special formatting</summary>
      /// <remarks>id : 20130604°2107</remarks>
      /// <returns>The wanted flag whether the grid shows nulls or not</returns>
      public bool GridShowNulls
      {
         get { return _gridShowNulls; }
         set
         {
            _gridShowNulls = value;
            NotifyPropertyChanged();
         }
      }

      /// <summary>This property gets/sets true to hide the object browser</summary>
      /// <remarks>id : 20130604°2108</remarks>
      /// <returns>If getting, then flag whether the dedicated/legacy treeview is hidden or visible, if setting then nothing</returns>
      public bool HideBrowser
      {
         get { return _hideBrowser; }

         set
         {
            _hideBrowser = value;
            if ((_browser == null) && (! value))                               // can't show browser if not available
            {
               // empirical fix while separating the two PopulateBrowser*() methods (20130704°1304) DOES NOT HELP TO SHOW PRIMARY BROWSER
               if (Glb.Debag.Execute_No)
               {
                  return;
               }
            }

            _hideBrowser = value;
            splitBrowser.Panel1.Visible = (! value);                                   // show/hide the browser panel containing the treeview
            splitBrowser.Panel1Collapsed = value;
            //splBrowser.Visible = !value;                                             // show/hide the splitter
            if (! value)
            {
               PopulateBrowser();
            }
            PopulateBrowser2();                                                        // is this necessary/wanted here?

            richtextbox_Query.Focus();
            NotifyPropertyChanged();
         }
      }

      /// <summary>This property gets the current status of the running query</summary>
      /// <remarks>id : 20130604°2109</remarks>
      public DbClient.RunStates RunState
      {
         get { return DbClient.RunState; }
      }

      /// <summary>This method, implementing the IQueryForm interface, displays the Query Options window</summary>
      /// <remarks>id : 20130705°1011</remarks>
      public void ShowQueryOptions()
      {
         // don't show dialog if situation is inappropriate
         if (ClientBusy || DbClient.QueryOptions == null)
         {
            return;
         }

         // (shutdown 20130705°1013)
         if (Glb.Debag.Execute_No) // want get rid of the form retrieved from the library
         {
            if (DbClient.QueryOptions.ShowForm() == DialogResult.OK)
            {
               DbClient.ApplyQueryOptions();
            }
         }
         else // replace original by experimental sequence
         {

            QueryOptionsForm f = null;

            // (experiment 20130705°1012) discriminate clients, quick'n'dirty violating OOP
            // note : I want some way to show the specific QueryOptions form, but without retrieving
            //    it from the engine/library (as was planned in QueryExPlus). Because in the library,
            //    the Form classes shall vanish. Not the library shall pull the values, but the GUI
            //    shall push them to the library. This is necessary, so the calling direction goes
            //    only from GUI to library, never from library to GUI. [note 20130705°1014]
            if (DbClient.ConnSettings.Type == ConnSettingsLib.ConnectionType.Mssql)
            {
               f = new QueryOptionsFormMssql();
            }
            else if (DbClient.ConnSettings.Type == ConnSettingsLib.ConnectionType.Mysql)
            {
               f = new QueryOptionsFormMysql();
            }
            else
            {
               f = new QueryOptionsForm();
            }

            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
               DbClient.QueryOptions.LetOptionsPushFromGui();
            }
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2112</remarks>
      public void ShowFind()
      {
         FindReplaceForm ff = new FindReplaceForm();

         DialogResult res = ff.ShowDialog();
         if (res == DialogResult.Cancel)
         {
            return;
         }

         _lastFindText = ff.txtFind.Text;
         _lastFindPos = -1;
         _lastFindRow = -1;
         _lastFindCol = -1;

         _matchCase = ff.chkMatchCase.Checked;
         FindNext();
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2113</remarks>
      public void FindNext()
      {
         if (_lastFindText == null || _lastFindText.Length == 0)
         {
            return;
         }

         Control ctrl = GetActiveControl(this);
         if (ctrl is TextBoxBase)
         {
            FindNextInTextBox((TextBoxBase)ctrl);
         }
         else if (ctrl is DataGridView)
         {
            FindNextInDataGrid((DataGridView)ctrl);
         }
         else
         {
         }
      }

      /// <summary>This method finds the next search value in the given grid</summary>
      /// <remarks>id : 20130604°2114</remarks>
      /// <param name="grid">The grid in which to search</param>
      private void FindNextInDataGrid(DataGridView grid)
      {
         StringComparison compareType;
         compareType = _matchCase
                      ? StringComparison.CurrentCulture
                       : StringComparison.CurrentCultureIgnoreCase;
         if (grid.Rows.Count == 0)
         {
            return;
         }
         DataGridViewCell cell;
         if (grid.SelectedCells.Count == 0)
         {
            cell = grid.Rows[0].Cells[0];
         }
         else
         {
            // for some reason they are in reverse order, LeftTop selected item is last in the list
            cell = grid.SelectedCells[grid.SelectedCells.Count - 1];
         }

         int _lastRow;
         int _lastCol;
         if (grid.SelectedCells.Count == 1 && grid.SelectedCells[0].RowIndex == _lastFindRow && grid.SelectedCells[0].ColumnIndex == _lastFindCol)
         {
            _lastRow = _lastFindRow;
            _lastCol = _lastFindCol;
         }
         else if (grid.SelectedCells.Count == 0)
         {
            _lastRow = 0;
            _lastCol = -1;
         }
         else
         {
            _lastRow = grid.SelectedCells[0].RowIndex;
            _lastCol = grid.SelectedCells[0].ColumnIndex - 1;
         }

         for (int row = _lastRow; row < grid.Rows.Count; row++)
         {
            for (int col = _lastCol + 1; col < grid.Columns.Count; col++)
            {
               if (grid.Rows[row].Cells[col].FormattedValue.ToString().IndexOf(_lastFindText, 0, compareType) >= 0)
               {
                  //grid.SelectedCells.Clear();
                  //grid.SelectedCells.Insert(0, grid.Rows[row].Cells[col]);
                  grid.ClearSelection();
                  grid.CurrentCell = grid.Rows[row].Cells[col];
                  grid.Rows[row].Cells[col].Selected = true;
                  _lastFindRow = row;
                  _lastFindCol = col;
                  return;
               }
            }
            _lastCol = -1;
         }
      }

      /// <summary>This method finds the next search value in the given TextBox</summary>
      /// <remarks>id : 20130604°2115</remarks>
      /// <param name="txtBox">The textbox in which to search for a value</param>
      private void FindNextInTextBox(TextBoxBase txtBox)
      {
         StringComparison compareType;
         compareType = _matchCase
                      ? StringComparison.CurrentCulture
                       : StringComparison.CurrentCultureIgnoreCase
                        ;

         int _lastFindPosition = _lastFindPos == txtBox.SelectionStart
                                && txtBox.SelectedText.Equals(_lastFindText, compareType)
                                 ? _lastFindPos + 1
                                  : txtBox.SelectionStart
                                   ;

         int idx = txtBox.Text.IndexOf(_lastFindText, _lastFindPosition, compareType);
         if (idx == -1)
         {
            idx = txtBox.Text.IndexOf(_lastFindText, 0, compareType);
         }
         if (idx >= 0)
         {
            txtBox.SelectionStart = idx;
            txtBox.SelectionLength = _lastFindText.Length;
            txtBox.ScrollToCaret();
            _lastFindPos = idx;
         }
      }

      /// <summary>This property gets the connection specific Browser object</summary>
      /// <remarks>id : 20130604°2116</remarks>
      public IDbBrowser Browser
      {
         get { return _browser; }
      }

      #endregion IQueryForm implementation

      #region Grid Output

      /// <summary>This field stores the delegate used to call when displaying data in the grid</summary>
      /// <remarks>id : 20130604°2117</remarks>
      /// <param name="dt">The table to display</param>
      private delegate void DisplayGridDelegate(DataTable dt);

      /// <summary>This method displays a datatable on the grid. Should be called only from UI grid</summary>
      /// <remarks>id : 20130604°2118</remarks>
      /// <remarks>emonk72 - 24-Nov-2011 - Set default display for null values.</remarks>
      /// <param name="dt">The table to display</param>
      private void DisplayGrid(DataTable dt)
      {
         DataGridView dataGrid = new DataGridView();

         // note : Due to a bug in the grid control, we must add the grid to the tabpage
         //    before assigning a datasource. This bug was introduced in Beta 1, was fixed
         //    for Beta 2, then reared its ugly head again in RTM. [note 20130604°211802]
         TabPage tabPage = new TabPage("Result Set " + (tabcontrol_Results.TabCount).ToString());
         tabPage.Tag = _resultsTabType.GridResults;
         tabPage.Controls.Add(dataGrid);
         tabcontrol_Results.TabPages.Add(tabPage);

         dataGrid.Dock = DockStyle.Fill;

         dataGrid.ReadOnly = true;
         dataGrid.AllowUserToAddRows = false;
         if (GridShowNulls)
         {
            dataGrid.CellPainting += new DataGridViewCellPaintingEventHandler(dataGrid_CellPainting);
         }
         dataGrid.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGrid_RowPostPaint);
         dataGrid.DataError += new DataGridViewDataErrorEventHandler(dataGrid_DataError);
         dataGrid.DataSource = dt;
         _rowCount = dt.Rows.Count;

         #region Create Context Menu

         dataGrid.ContextMenuStrip = EditManager.GetEditManager().GetContextMenuStrip(dataGrid);
         dataGrid.ContextMenuStrip.Opening += delegate(object o, System.ComponentModel.CancelEventArgs e) { dataGrid.Focus(); };

         #endregion Create Context Menu

         // note : The auto sizing feature is supported but slooowwww.
         //    // dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
         //    Instead we'll have to size each column manually. A graphics object is required
         //    to measure text so we can size the grid columns correctly. [note 20130604°211803]
         Graphics g = CreateGraphics();

         // note : For each column, determine the largest visible text string,
         //    and use that to size the column. We'll be measuring text for each
         //    row that's visible in the grid. [note 20130604°211804]
         int maxRows = Math.Min(dataGrid.DisplayedRowCount(true), dt.Rows.Count);
         // GridColumnStylesCollection cols = ts.GridColumnStyles;
         const int margin = 6; // allow 6 pixels per column, for grid lines and some white space
         int colNum = 0;
         if (dataGrid.Columns.Count == 1)
         {
            dataGrid.Columns[0].Width = dataGrid.Width;
         }
         else
         {
            foreach (DataGridViewColumn col in dataGrid.Columns)
            {
               int maxWidth = (int)g.MeasureString(col.HeaderText, dataGrid.Font).Width + margin;
               for (int row = 0; row < maxRows; row++)
               {
                  string s = dt.Rows[row][colNum, DataRowVersion.Current].ToString();
                  if (GridShowNulls && dt.Rows[row][colNum, DataRowVersion.Current] == DBNull.Value)
                  {
                     s = "(null)";
                  }
                  int length = (int)g.MeasureString(s, dataGrid.Font).Width + margin;
                  maxWidth = Math.Max(maxWidth, length);
               }

               // assign length of longest string to the column width,
               //  but don't exceed width of actual grid
               col.Width = Math.Min(dataGrid.Width, maxWidth);
               colNum++;
               if (GridShowNulls)
               {
                  col.DefaultCellStyle.NullValue = "(null)";
               }
            }
         }
         if (Properties.Settings.Default.ExpandRowNumber)
         {
            int headerWidth = (int)g.MeasureString(dataGrid.RowCount.ToString(), dataGrid.Font).Width + 3 * margin;
            dataGrid.RowHeadersWidth = headerWidth;
         }
         g.Dispose();

         // set datetime columns to show the time as well as the date
         DataGridViewCellStyle dateCellStyle;
         dateCellStyle = new DataGridViewCellStyle();
         dateCellStyle.Format = s_DateTimeFormatString;
         if (GridShowNulls)
         {
            dateCellStyle.NullValue = "(null)";
         }
         foreach (DataGridViewColumn col in dataGrid.Columns)
         {
            if (dt.Columns[col.Name].DataType == typeof(DateTime))
            {
               col.DefaultCellStyle = dateCellStyle;
            }
         }
      }

      /// <summary>This eventhandler processes the dataGrid_DataError event</summary>
      /// <remarks>id : 20130604°2119</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
      {
         // throw new Exception("The method or operation is not implemented."); // 20130604°2119
         return;
      }

      /// <summary>This method displays the data to the grid</summary>
      /// <remarks>id : 20130604°2121</remarks>
      /// <param name="dr">The DataReader for which to display the grid</param>
      private void DisplayGrid(IDataReader dr)
      {
         DataTable dt = new DataTable();
         this.DbClient.DataSet.Tables.Add(dt); // (breakpoint 20130822.2112xx) debugging issue 20130822°2111 'DbClient triggers QueryForm methods'

         // assign unique name to each table, so subsequent queries don't override new tables
         dt.TableName = "QueryResult-" + this.DbClient.DataSet.Tables.Count;
         dt.BeginLoadData();
         try
         {
            // [line 20130604°2311]
            // note : Remember issue 20131201°0811 'SQLite String not recognized as DateTime'.
            dt.Load(dr);
         }
         catch (ConstraintException /* ex */ )
         {
            dt.Constraints.Clear();
            DataRow[] errorRows = dt.GetErrors();
            foreach (DataRow row in errorRows)
            {
               row.ClearErrors();
            }
         }
         dt.EndLoadData();

         // do not display grid for commands that do not return a valid recordset (example: "use master")
         if (dt.Columns.Count == 0)
         {
            return;
         }

         if (InvokeRequired)
         {
            BeginInvoke(new DisplayGridDelegate(DisplayGrid), new object[] { dt } );
         }
         else
         {
            DisplayGrid(dt);
         }
      }

      /// <summary>This eventhandler sets the color for cells with null values to visually distinguish from valid cell values</summary>
      /// <remarks>id : 20130604°2122</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      void dataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
      {
         if (e.Value == DBNull.Value && e.CellStyle.ForeColor != Color.Gray)
         {
            e.CellStyle.ForeColor = Color.Gray;
         }
      }

      /// <summary>
      /// This eventhandler paints the row number on the row header.
      ///  The using statement automatically disposes the brush.
      /// </summary>
      /// <remarks>emonk72 - 24-Nov-2011 - Added clipping to prevent overflow into data cells.</remarks>
      /// <remarks>id : 20130604°2123</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void dataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
      {
         string t = (e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture);

         Region clp = null;
         if (! e.Graphics.IsClipEmpty)
            clp = e.Graphics.Clip;

         e.Graphics.SetClip(new Rectangle(e.RowBounds.Left, e.RowBounds.Top, ((DataGridView)sender).RowHeadersWidth - 1, e.RowBounds.Height), System.Drawing.Drawing2D.CombineMode.Replace);

         using (SolidBrush b = new SolidBrush(((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor))
         {
            e.Graphics.DrawString ( t
                                   , e.InheritedRowStyle.Font
                                    , b
                                     , e.RowBounds.Location.X + 16
                                      , e.RowBounds.Location.Y + 4
                                       );
         }

         if (clp == null)
         {
            e.Graphics.ResetClip();
         }
         else
         {
            e.Graphics.Clip = clp;
         }
      }

      #endregion Grid Output

      #region Text Output

      /// <summary>This method creates text headers for the table schema</summary>
      /// <remarks>id : 20130604°2124</remarks>
      /// <param name="schema">The DataTable from which the schema can be read</param>
      private void DisplayTextSchema(DataTable schema)
      {
         _lastResults = DateTime.Now;
         _colWidths = new int[schema.Rows.Count];
         string separator = "";
         AppendTextResults("\r\n");
         for (int col = 0; col < schema.Rows.Count; col++)
         {
            string colName = schema.Rows[col][0].ToString();
            int colSize = (int)schema.Rows[col]["ColumnSize"];
            Type dataType = (Type)schema.Rows[col]["DataType"];
            if (dataType == typeof(Guid))
            {
               colSize = Guid.NewGuid().ToString().Length;
            }
            if (dataType == typeof(DateTime))
            {
               colSize = _dateTimeFormatStringLength;
            }

            // the column should be big enough also to accommodate the size of the column header
            _colWidths[col] = Math.Min(DbClient.QueryOptions.maxTextWidth, Math.Max(colName.Length, colSize));
            if (colName.Length > DbClient.QueryOptions.maxTextWidth)
            {
               colName = colName.Substring(0, DbClient.QueryOptions.maxTextWidth);
            }
            AppendTextResults(colName.PadRight(_colWidths[col]) + " ");
            separator = separator + "".PadRight(_colWidths[col], '-') + " ";
         }
         AppendTextResults("\r\n" + separator + "\r\n");
      }

      /// <summary>This method outputs a row of data in text format</summary>
      /// <remarks>id : 20130604°2125</remarks>
      /// <param name="DataFields">The array with the field values to be output</param>
      private void DisplayTextRow(object[] DataFields)
      {
         string cell = "";
         for (int col = 0; col < DataFields.Length; col++)
         {
            object data = DataFields[col];

            // use a fixed format for dates, so we can predict its length
            cell = data is DateTime
                  ? ((DateTime)data).ToString(s_DateTimeFormatString)
                   : data.ToString()
                    ;

            // if on last field, don't truncate or pad
            if (col == DataFields.Length - 1)
            {
               AppendTextResults(cell);
            }
            else
            {
               _textResults.Append(cell.PadRight(_colWidths[col]).Substring(0, _colWidths[col]));
               _textResults.Append(" ");
            }
         }
         if (! cell.EndsWith("\r\n")) { AppendTextResults("\r\n"); }
      }

      /// <summary>This method appends a string to the text output. Flush after 5 seconds</summary>
      /// <remarks>id : 20130604°2126</remarks>
      /// <param name="text">The text to be appended to the output</param>
      private void AppendTextResults(string sText)
      {
         AppendTextResults(sText, false);
      }

      /// <summary>This method appends a string to the text output. Flush after 5 seconds of on demand</summary>
      /// <remarks>id : 20130604°2127</remarks>
      /// <param name="text">The text to be appended to the output</param>
      /// <param name="flush">The flag telling whether to flush or not</param>
      private void AppendTextResults(string sText, bool bFlush)
      {
         _textResults.Append(sText);

         // feed results to host every 5 seconds
         if (_lastResults.AddSeconds(5) < DateTime.Now)
         {
            bFlush = true;
         }

         if (bFlush)
         {
            AppendToTextBoxDelegate del = AppendToTextBox;
            Invoke(del, new object[] { _textResults.ToString() });
            _textResults = new StringBuilder();
            _lastResults = DateTime.Now;
         }
      }

      /// <summary>This field stores the delegate to append to the textbox on the UI thread</summary>
      /// <remarks>id : 20130604°2128</remarks>
      /// <param name="s">...</param>
      delegate void AppendToTextBoxDelegate(string s);

      /// <summary>This method appends a string to the textbox. Should be called only on the UI thread</summary>
      /// <remarks>id : 20130604°2129</remarks>
      /// <param name="s">...</param>
      private void AppendToTextBox(string s)
      {
         if (_txtResultsBox == null)
         {
            CreateResultsTextbox();
         }
         _txtResultsBox.AppendText(s);
      }

      #endregion Text Output

      #region Private Functions

      /// <summary>This method returns false if user cancelled or open failed</summary>
      /// <remarks>id : 20130604°2131</remarks>
      /// <param name="sFilename">The filename of the file to open</param>
      /// <returns>Success flag. False if user cancelled or file open failed.</returns>
      private bool OpenFile(string sFilename)
      {
         try
         {
            string s;
            s = ReadFromFile(sFilename);
            if (s != null && CloseQuery())
            {
               richtextbox_Query.Text = s;
               richtextbox_Query.Modified = false;
               this.FileName = sFilename;
               _realFileName = true;
               if (MRUFileAdded != null)
               {
                  MRUFileAdded.Invoke(this, new MRUFileAddedEventArgs(sFilename));
               }
               return true;
            }
            else
            {
               return false;
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show ( ex.Message
                             , "Error opening file"
                              , MessageBoxButtons.OK
                               , MessageBoxIcon.Exclamation
                                );
            return false;
         }
      }

      /// <summary>This method saves the query to the file</summary>
      /// <remarks>id : 20130604°2132</remarks>
      /// <param name="fileName">...</param>
      /// <returns>...</returns>
      private bool SaveFile(string fileName)
      {
         try
         {
            WriteToFile(fileName, richtextbox_Query.Text);
            richtextbox_Query.Modified = false;
            return true;
         }
         catch (Exception ex)
         {
            MessageBox.Show ( ex.Message
                             , "Error saving file"
                              , MessageBoxButtons.OK
                               , MessageBoxIcon.Exclamation
                                );
            return false;
         }
      }

      /// <summary>This method closes the query window</summary>
      /// <remarks>id : 20130604°2133</remarks>
      /// <returns>...</returns>
      internal bool CloseQuery()
      {
         // check to see if a query is running, and warn user that the query will be cancelled
         if (RunState != DbClient.RunStates.Idle)
         {
            string sMsg = FileName + " is currently executing.\nWould you like to cancel the query?";
            if ( MessageBox.Show ( sMsg
                                  , "QueryPony"
                                   , MessageBoxButtons.YesNo
                                    , MessageBoxIcon.Question
                                     ) != DialogResult.Yes
                                      )
            {
               // the Dispose method in DbClient will actually do the Cancel
               return false;
            }
         }

         // If the query text has been modified, give option of saving changes,
         //  don't nag the user in the case of simple queries of less than 30 characters
         if (richtextbox_Query.Modified && richtextbox_Query.Text.Length > 30)
         {
            string sMsg = "Save changes to " + FileName + "?";
            DialogResult dr = MessageBox.Show ( sMsg
                                               , Text
                                                , MessageBoxButtons.YesNoCancel
                                                 );
            if (dr == DialogResult.Yes)
            {
               if (! Save())
               {
                  return false;
               }
            }
            else if (dr == DialogResult.Cancel)
            {
               return false;
            }
            else
            {
            }
         }
         return true;
      }

      /// <summary>This property gets/sets the filename of the query displayed in the window</summary>
      /// <remarks>id : 20130604°2134</remarks>
      private string FileName
      {
         get { return _fileName; }
         set
         {
            _fileName = value;
            UpdateFormText();
            NotifyPropertyChanged();
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2135</remarks>
      /// <param name="ContainerControl">...</param>
      /// <returns>...</returns>
      protected Control GetActiveControl(Control ContainerControl)
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

      /// <summary>This method writes a string to a file, returning true if successful</summary>
      /// <remarks>id : 20130604°2136</remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <param name="data">String data to write</param>
      private void WriteToFile(string fileName, string data)
      {
         StreamWriter w = null;
         try
         {
            w = File.CreateText(fileName);
            w.Write(data);
         }
         finally
         {
            if (w != null)
            {
               w.Close();
            }
         }
      }

      /// <summary>This method reads the contents of a file into a string, returning true if successful</summary>
      /// <remarks>id : 20130604°2137</remarks>
      /// <param name="fileName">Qualified filename</param>
      /// <returns>...</returns>
      private string ReadFromFile(string fileName)
      {
         StreamReader r = null;
         try
         {
            r = File.OpenText(fileName);
            return r.ReadToEnd();
         }
         finally
         {
            if (r != null)
            {
               r.Close();
            }
         }
      }

      /// <summary>This method should be called whenever a query is started or stopped</summary>
      /// <remarks>id : 20130604°2138</remarks>
      /// <param name="running">...</param>
      private void SetRunning(bool running)
      {
         if (! running)
         {
            CheckDatabase();
         }

         tmrExecTime.Enabled = running;
         NotifyPropertyChanged();
      }

      /// <summary>This method checks the current database - if it has changed, update controls accordingly</summary>
      /// <remarks>id : 20130604°2139</remarks>
      private void CheckDatabase()
      {
         if (_lastDatabase != DbClient.Database)
         {
            _lastDatabase = DbClient.Database;
            UpdateFormText();
            PopulateBrowser();
            PopulateBrowser2();
         }
      }

      /// <summary>This method displays the rowcount in the status bar</summary>
      /// <remarks>id : 20130604°2141</remarks>
      private void ShowRowCount()
      {
         panRows.Text = _rowCount == 0
                       ? ""
                        : _rowCount.ToString() + " row" + (_rowCount == 1 ? "" : "s")
                         ;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2142</remarks>
      private void ShowExecTime()
      {
         if (DbClient.RunState == DbClient.RunStates.Running)
         {
            panExecTime.Text = DateTime.Now.Subtract(DbClient.ExecStartTime).ToString();
         }
         else
         {
            panExecTime.Text = DbClient.ExecDuration.ToString();
         }
      }

      /// <summary>This method updates the form's caption to show the connection & selected database</summary>
      /// <remarks>id : 20130604°2143</remarks>
      private void UpdateFormText()
      {
         this.Text = DbClient.ConnSettings.Description + " - " + DbClient.Database + " - " + _fileName;
      }

      /// <summary>This method notifys the parent form of any property changes</summary>
      /// <remarks>id : 20130604°2144</remarks>
      private void NotifyPropertyChanged()
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, EventArgs.Empty);
         }
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°2145</remarks>
      private bool ClientBusy
      {
         get { return RunState != DbClient.RunStates.Idle; }
      }

      /// <summary>This method maintains the (old) dedicated treeview on the QueryForm</summary>
      /// <remarks>id : 20130604°2146</remarks>
      private void PopulateBrowser()
      {
         // can we populate?
         if ((this._browser == null) || this.HideBrowser || this.ClientBusy)
         {
            return;
         }

         // populate
         try
         {
            this.treeView.Nodes.Clear();
            TreeNode[] tn = this._browser.GetObjectHierarchy();
            if (tn == null)
            {
               this.HideBrowser = true;
            }
            else
            {
               this.treeView.Nodes.AddRange(tn);

               // expand the top level of hierarchy
               this.treeView.Nodes[0].Expand();

               this.comboboxDatabase.Items.Clear();
               this.comboboxDatabase.Items.Add("<refresh list ...>");
               string[] dbs = _browser.GetDatabases();
               this.comboboxDatabase.Items.AddRange(dbs);
               try
               {
                  this.comboboxDatabase.Text = this.DbClient.Database;
               }
               catch
               {
               }
            }
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
         }
      }

      /// <summary>This method maintains the (new) central treeview on the MainForm</summary>
      /// <remarks>id : 20130701°1121 (20130604°2146)</remarks>
      private void PopulateBrowser2()
      {

         // can we populate?
         if ((this._browser == null) || this.ClientBusy)
         {
            return;
         }

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // Maintain query forms list and main treeview, and if the QueryForms
         //  list does not contain this QueryForm, add it [seq 20130701°1141]
         if (! MainForm._queryforms.Contains(this))
         {
            MainForm._queryforms.Add(this);
         }
         TreeNode tnDatabase = this.maintainMainTreeview();
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         tnDatabase.Nodes.Clear();
         TreeNode[] tn = this._browser.GetObjectHierarchy();
         if (tn == null)
         {
            if (IOBus.Gb.Debag.Shutdown_Temporarily)
            {
               this.HideBrowser = true;
            }
         }
         else
         {
            tnDatabase.Nodes.AddRange(tn);

            // expand the top level of hierarchy
            try
            {
               tnDatabase.Nodes[0].Expand();                                   // May throw exception 'null reference'
            }
            catch (Exception ex)
            {
               string sErr = ex.Message + Glb.sCr + "[Error 20130701°1143]";
               MainForm.outputStatusLine(sErr);
               // Continue
            }

            // Maintain databases combobox
            // [shutdown 20130713°0902] The new treeview shall not care about the
            //    comboboxDatabase on the QueryForm, this does the old treeview.
            if (IOBus.Gb.Debag.Shutdown_Temporarily)
            {
               this.comboboxDatabase.Items.Clear();
               this.comboboxDatabase.Items.Add("<refresh list ...>");
               string[] dbs = _browser.GetDatabases();
               this.comboboxDatabase.Items.AddRange(dbs);
               try
               {
                  this.comboboxDatabase.Text = DbClient.Database;
               }
               catch
               {
                  // ?
               }
            }
         }

         try
         {
         }
         catch (Exception ex)
         {
            string sMsg = ex.ToString() + Glb.sBlnk + "[Error 20130701°1122]";
            System.Diagnostics.Debug.WriteLine(sMsg);
         }
      }

      /// <summary>This method maintains the QueryForms List and the main treeview</summary>
      /// <remarks>id : 20130701°1151</remarks>
      private TreeNode maintainMainTreeview()
      {
         // If the main treeview does not contain this connection, then add it,
         //  first process the server node, then the database node ...

         // (1) Guarantee server node
         // (1.1) Retrieve server id
         ConnSettingsLib csLib = DbClient.ConnSettings;

         // (1.2) Retrieve the tree node label text
         string sServerNodeText = csLib.DatabaseServerUrl;                     // Provisory — Does not work for all connection types ..
         sServerNodeText = csLib.LabelTreenodeServer;                          // This may be better [line 20130723°1444]

         ConnSettingsGui.Server sServerNodeTag = new ConnSettingsGui.Server(sServerNodeText); // [experiment 20130701°1411`02]

         // (1.3) Maintain treeview
         TreeView tv = MainForm.TreeviewMain;                                  // Comfort variable
         TreeNode tnServer = searchServerNode(tv, sServerNodeTag);

         if (tnServer == null)
         {
            tnServer = new TreeNode();
            tnServer.Text = sServerNodeText;
            tnServer.Tag = sServerNodeTag;
            tv.Nodes.Add(tnServer);
         }
         else
         {
            sServerNodeTag = tnServer.Tag as ConnSettingsGui.Server;           // [experiment 20130701°141102]
         }

         // (2) Guarantee database/connection node
         // (2.1) Retrieve connection

         // () Get tree node label [line 20130723°1443]
         string sDatabaseTreenodeText = csLib.LabelTreenodeDatabase;

         // (2.2) Maintain treeview
         TreeNode tnConn = searchServerNode(tv, csLib);
         if (tnConn == null)
         {
            tnConn = new TreeNode();
            tnConn.Text = sDatabaseTreenodeText;
            tnConn.Tag = csLib;
            tnServer.Nodes.Add(tnConn);
         }

         return tnConn;
      }

      //-------------------------------------------------------
      // reference 20130701°1213
      // title : Thread 'TreeView search'
      // url : http://stackoverflow.com/questions/11530643/treeview-search
      // usage : Method 20130701°1211 SearchNodes()
      //-------------------------------------------------------

      /// <summary>This field stores the hitlist of the treeview search method</summary>
      /// <remarks>id : 20130701°1212</remarks>
      System.Collections.Generic.List<TreeNode> CurrentNodeMatches = new System.Collections.Generic.List<TreeNode>();

      /// <summary>This method searches a server node</summary>
      /// <remarks>id 20130701°1221</remarks>
      /// <returns></returns>
      private TreeNode searchServerNode(TreeView tv, object tag)
      {
         TreeNode tnRet = null;
         CurrentNodeMatches.Clear();

         SearchNodesByTag(tag, tv.Nodes[0]);

         if (CurrentNodeMatches.Count > 0)
         {
            // just pick the first treenode
            tnRet = CurrentNodeMatches[0];
         }
         return tnRet;
      }

      /// <summary>This method is a helper method to search ... treenode ...</summary>
      /// <remarks>id : 20130701°1211</remarks>
      /// <param name="tv">The treeview to search</param>
      /// <returns>The found treenode or null</returns>
      private void SearchNodesByTag(object oSearch, TreeNode tnStart)
      {

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // issue 20130701°1251
         // topic : condition 'tnStart.Tag != null' in method 20130701°1211 SearchNodesByTag
         // note : The offending node for which we introduced this condition is a 'Table' node.
         //    This condition is a quick workaround.
         //    (1) advantage of this condition : the many subnodes of one database are skipped
         //    (2) disadvantage : it will break if we introduce tags on the subnodes (what we plan to do indeed)
         // note : The reason for introducing this condition is a null reference exception below
         //    in the comparison '( tnStart.Tag.ToString() == ((string) oSearch))'
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         while ((tnStart != null) && (tnStart.Tag != null)) // see issue 20130701°1251!
         {

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // ref 20130701°1432
            // title : Stackoverflow thread 'Comparing Two Objects'
            // url : http://stackoverflow.com/questions/5183929/comparing-two-objects
            // usage : Method 20130701°1211 SearchNodesByTag(),
            //          method 20130701°1431 ConnectionSettings.Server.Equals()
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // ref 20130701°1433
            // title : MSDN article 'IEquatable<T>.Equals-Methode'
            // url : http://msdn.microsoft.com/de-de/library/vstudio/ms131190%28v=vs.90%29.aspx
            // usage : Method 20130701°1211 SearchNodesByTag(),
            //          method 20130701°1431 ConnectionSettings.Server.Equals()
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            bool bFound = false;
            if (tnStart.Tag.Equals(oSearch)) // need the IEquatable interface for ConnectionSettings.Server
            {
               bFound = true;
            }

            // act on finding
            if (bFound)
            {
               CurrentNodeMatches.Add(tnStart);
            }

            if (tnStart.Nodes.Count > 0)
            {
               SearchNodesByTag(oSearch, tnStart.Nodes[0]);                    // Recursive search
            }

            tnStart = tnStart.NextNode;
         }
      }

      #region Methods for Saving Results

      /// <summary>This method present a Save Dialog for query results and save to CSV or XML format</summary>
      /// <remarks>id : 20130604°2147</remarks>
      public void SaveResults()
      {
         if (! ResultsInText && (DSResults.Tables.Count == 0) || ClientBusy)
         {
            return;
         }

         SaveFileDialog saveResultsDialog;
         saveResultsDialog = new System.Windows.Forms.SaveFileDialog();
         if (ClientBusy || (! ResultsInText && ! tabcontrol_Results.SelectedTab.Tag.Equals(_resultsTabType.GridResults)))
         {
            return;
         }

         saveResultsDialog.Filter = ResultsInText
                                   ? "Text Format|*.txt"
                                    : "CSV Format|*.csv|XML|*.xml|XSD Schema|*.xsd" + "|All files|*.*"
                                     ;

         if (saveResultsDialog.ShowDialog() != DialogResult.OK) { return; }

         if (ResultsInText)
         {
            SaveResultsText(saveResultsDialog.FileName);
         }
         else if (System.IO.Path.GetExtension(saveResultsDialog.FileName).ToUpper() == ".XML")
         {
            SaveResultsXml(saveResultsDialog.FileName);
         }
         else if (System.IO.Path.GetExtension(saveResultsDialog.FileName).ToUpper() == ".XSD")
         {
            SaveResultsXsd(saveResultsDialog.FileName);
         }
         else
         {
            SaveResultsCsv(saveResultsDialog.FileName);
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2148</remarks>
      /// <param name="fileName">...</param>
      void SaveResultsText(string fileName)
      {
         WriteToFile(fileName, _txtResultsBox.Text);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2149</remarks>
      /// <param name="fileName">...</param>
      void SaveResultsXml(string fileName)
      {
         DSResults.WriteXml(fileName);
      }

      /// <summary>This method ... (dl3bak)</summary>
      /// <remarks>id : 20130604°2151</remarks>
      /// <param name="fileName">...</param>
      void SaveResultsXsd(string fileName)
      {
         DSResults.WriteXmlSchema(fileName);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2152</remarks>
      /// <param name="fileName">...</param>
      void SaveResultsCsv(string fileName)
      {
         // save the currently selected table only
         string DL = QueryPonyGui.Properties.Settings.Default.Delimiter;
         char TDL = QueryPonyGui.Properties.Settings.Default.TextDelimiter;
         DataTable table = (DataTable)((DataGridView)tabcontrol_Results.SelectedTab.Controls[0]).DataSource;
         System.IO.StreamWriter w;
         try { w = System.IO.File.CreateText(fileName); }
         catch (Exception e)
         {
            string sMsg = "Could not create file: " + fileName + "\n" + e.Message;
            MessageBox.Show ( sMsg
                             , "QueryPony"
                              , MessageBoxButtons.OK
                               , MessageBoxIcon.Exclamation
                                );
            return;
         }
         using (w)
         {
            // write a header consisting of a list of columns
            string colList = "";
            foreach (DataColumn column in table.Columns)
            {
               if (colList.Length > 0)
               {
                  colList += DL;
               }
               colList += column.ColumnName;
            }
            w.WriteLine(colList);
            foreach (DataRow row in table.Rows)
            {
               string line = "";
               foreach (object cell in row.ItemArray)
               {
                  if (line.Length > 0)
                  {
                     line += DL;
                  }

                  // string types may contain embedded commas, so wrap in quotes
                  if (cell is string)
                  {
                     line += TDL + cell.ToString() + TDL;
                  }
                  else
                  {
                     line += cell.ToString();
                  }
               }
               w.WriteLine(line);
            }
         }
      }

      #endregion Methods for Saving Results

      #endregion Private Functions

      #region Form Events

      /// <summary>This eventhandler processes the this QueryForm's FormClosing event</summary>
      /// <remarks>id : 20130604°2153</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void QueryForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (! CloseQuery())
         {
            e.Cancel = true;
         }
      }

      /// <summary>This eventhandler processes the this QueryForm's FormClosed event</summary>
      /// <remarks>id : 20130604°2154</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void QueryForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         DbClient.Dispose();
      }

      /// <summary>This eventhandler processes this QueryForm's Activated event</summary>
      /// <remarks>id : 20130604°2155</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void QueryForm_Activated(object sender, EventArgs e)
      {
         NotifyPropertyChanged();
      }

      /// <summary>This eventhandler processes the database combobox Enter event</summary>
      /// <remarks>id : 20130604°2157</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void comboboxDatabase_Enter(object sender, EventArgs e)
      {
         if (ClientBusy)
         {
            richtextbox_Query.Focus();
         }
      }

      /// <summary>This eventhandler processes the database combobox SelectedIndexChanged event</summary>
      /// <remarks>id : 20130604°2156</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void comboboxDatabase_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (comboboxDatabase.SelectedIndex == 0)
         {
            PopulateBrowser();
            PopulateBrowser2();
         }
         else
         {
            DbClient.Database = comboboxDatabase.Text;
         }

         CheckDatabase();
      }

      #region Drag/Drop

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2158</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void txtQuery_DragOver(object sender, DragEventArgs e)
      {
         // if (e.Effect == DragDropEffects.Copy)
         // {
         //    txtQuery.SelectionStart = txtQuery.GetCharIndexFromPosition(txtQuery.PointToClient(new Point(e.X, e.Y)));
         //    txtQuery.SelectionLength = 1;
         // }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2159</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void txtQuery_DragEnter(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(typeof(string)))
         {
            e.Effect = DragDropEffects.Copy;
         }
         else if (e.Data.GetDataPresent(DataFormats.FileDrop))
         {
            e.Effect = DragDropEffects.Copy;
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2201</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void txtQuery_DragDrop(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(typeof(string)))
         {
            richtextbox_Query.SelectionStart = richtextbox_Query.GetCharIndexFromPosition(richtextbox_Query.PointToClient(new Point(e.X, e.Y)));
            string s = (string)e.Data.GetData(typeof(string));

            // Have the newly inserted text highlighted
            int start = richtextbox_Query.SelectionStart;
            richtextbox_Query.SelectedText = s;
            richtextbox_Query.SelectionStart = start;
            richtextbox_Query.SelectionLength = s.Length;
            richtextbox_Query.Modified = true;
            richtextbox_Query.Focus();
         }
         else if (e.Data.GetDataPresent(DataFormats.FileDrop))
         {
            Open(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
         }
         else
         {
         }
      }

      #endregion Drag/Drop

      /// <summary>This eventhandler processes the treeview's MouseDown event</summary>
      /// <remarks>id : 20130604°2202</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void treeView_MouseDown(object sender, MouseEventArgs e)
      {
         // when right-clicking, first select the node under the mouse.
         if (e.Button == MouseButtons.Right)
         {
            TreeNode tn = treeView.GetNodeAt(e.X, e.Y);
            if (tn != null)
            {
               treeView.SelectedNode = tn;
            }
         }
      }

      /// <summary>This eventhandler presents the context menu for a treeview node</summary>
      /// <remarks>id : 20130604°2203</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void treeView_MouseUp(object sender, MouseEventArgs e)
      {
         if (Browser == null) { return; }

         // display a context menu if the browser has an action list for the selected node
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
      }

      /// <summary>This eventhandler processes a treeview item context menu selection</summary>
      /// <remarks>
      /// id : 20130604°2204
      /// note : Remember issue 20130619°0531 'F5 key does not work'
      /// note : Remember reference 20130619°0532 'thread: programatically fire event handler'
      /// </remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      public void DoBrowserAction(object sender, EventArgs e) // (public to be accessible from method 20130701°1134 treeviewMain_MouseUp())
      {
         // this is called from the context menu activated by the TreeView's right-click event handler
         //  treeView_MouseUp() and appends text to the query textbox applicable to the selected menu item
         MenuItem mi = (MenuItem)sender;

         // possible breakpoint (20130619°0451)
         if (Glb.Debag.Debug_MainMenu_DoBrowserAction && System.Diagnostics.Debugger.IsAttached)
         {
            System.Diagnostics.Debugger.Break();
         }

         // ask the browser for the text to append, applicable to the selected node and menu item text
         string s1 = Browser.GetActionText(treeView.SelectedNode, mi.Text);
         string s2 = Browser.GetActionText(MainForm.TreeviewMain.SelectedNode, mi.Text);
         string s = s2;                                                                // provisory (20130701°114403)
         if (s == null)                                                                //
         {                                                                             //
            s = s1;                                                                    //
         }                                                                             //

         // nothing to do?
         // note : This should never happen, isn't it? (20130615°1322 ncm)
         if (s == null)
         {
            return;
         }

         // if (s.Length > 200) HideResults = true;
         if (richtextbox_Query.Text != "") { richtextbox_Query.AppendText("\r\n\r\n"); }
         int start = richtextbox_Query.SelectionStart;
         richtextbox_Query.AppendText(s);
         richtextbox_Query.SelectionStart = start;
         richtextbox_Query.SelectionLength = s.Length;
         richtextbox_Query.Modified = true;
         richtextbox_Query.Focus();

         // [seq 20130619°0533]
         // todo : This sequence seems no more wanted here. But it contains interesting
         //    lines to generate debug output when dealing with events. Provide those
         //    debug lines for general availability. (todo 20130709°0932)
         if (Glb.Debag.Execute_No)
         {

            Type tDbg = this.GetType();

            // test - this works but returns an array of zero elements
            System.Reflection.EventInfo[] areiDbg = this.GetType(
                       ).GetEvents(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                        );

            //--------------------
            // test - this retrieves 558 items, e.g. 'NotifyPropertyChanged'
            System.Reflection.MethodInfo[] armDbg = this.GetType(
                       ).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                        );
            // above result is too big with objects to inspect, make it easier
            System.Collections.Generic.List<string> liDbg = new System.Collections.Generic.List<string>();
            foreach (System.Reflection.MethodInfo mi2 in armDbg)
            {
               string sDbg = mi2.Name;
               liDbg.Add(sDbg);
            }
            liDbg.Sort();
            // the list does not contain 'PropertyChanged' nor 'OnPropertyChanged',
            // but a 'NotifyPropertyChanged'
            // note : When inspecting the QueryForm in the designer, I cannot find any of the events
            //   'PropertyChanged', 'OnPropertyChanged' or 'NotifyPropertyChanged'. How this?
            //--------------------

            // This is one proposed methods from stackoverflow 20130619°0532, but it does not work here.
            // This gets null with "PropertyChanged", obviously the wanted event was not in the list. But
            // just for fun, let's try any some other events. "TextChanges" returns also null. Even if we
            // set binding flags parameter to System.Reflection.BindingFlags.Default, still null will come.
            System.Reflection.MethodInfo methodinfo = this.GetType().GetMethod
                                                     ( "TextChanged" // "PropertyChanged"
                                                       , System.Reflection.BindingFlags.Default // specifiey 'no binding flags'
                                                        );
            methodinfo.Invoke(this, new object[] { new EventArgs() });
         }

         // let's try this [seq 20130619°0541] Heureka, that's it!
         // Now the F5 key will be activated immediately after the
         //  first entry from the context menu to the command textbox.
         NotifyPropertyChanged();

      }

      /// <summary>This eventhandler adds subnodes to the given node</summary>
      /// <remarks>id : 20130604°2205</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
      {
         // if a browser has been installed, see if it has a sub object
         //  hierarchy for us at the point of expansion
         if (Browser == null) { return; }

         // retrieve the basic nodes for the specific database and node type
         TreeNode[] subtree = Browser.GetSubObjectHierarchy(e.Node);
         if (subtree != null)
         {
            e.Node.Nodes.Clear();
            e.Node.Nodes.AddRange(subtree);
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2206</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
      {
         // allow objects to be dragged from the browser to the query textbox.
         if (e.Button == MouseButtons.Left && e.Item is TreeNode)
         {
            // ask the browser object for a string applicable to dragging onto the query window.
            string dragText = Browser.GetDragText((TreeNode)e.Item);

            // we'll use a simple string-type DataObject
            if (dragText != "")
            {
               treeView.DoDragDrop(new DataObject(dragText), DragDropEffects.Copy);
            }
         }
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2207</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void label1_VisibleChanged(object sender, EventArgs e)
      {
         richtextbox_Query.Focus();
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2208</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void QueryForm_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Alt && e.KeyCode == Keys.X)
         {
            this.Execute();
            e.Handled = true;
         }

         // check for Alt+Break combination (alternative shortcut for cancelling a query)
         if (e.Alt && e.KeyCode == Keys.Pause && RunState == DbClient.RunStates.Running)
         {
            Cancel();
            e.Handled = true;
         }
      }

      #endregion Form Events

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2209</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void tmrExecTime_Tick(object sender, EventArgs e)
      {
         ShowExecTime();
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2211</remarks>
      /// <param name="hWnd">...</param>
      /// <param name="msg">...</param>
      /// <param name="wParam">...</param>
      /// <param name="lParam">...</param>
      /// <returns>...</returns>
      [System.Runtime.InteropServices.DllImport("User32")]
      private static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°2218</remarks>
      int WM_SETREDRAW = 0xB;

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2212</remarks>
      private void FreezeDraw()
      {
         SendMessage(richtextbox_Query.Handle, WM_SETREDRAW, 0, 0);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°2213</remarks>
      private void UnFreezeDraw()
      {
         SendMessage(richtextbox_Query.Handle, WM_SETREDRAW, 1, 0);
         richtextbox_Query.Invalidate(true);
      }

      /// <summary>This method returns a RTF-usable RGB notation for the color</summary>
      /// <remarks>id : 20130604°2214</remarks>
      /// <param name="c">...</param>
      /// <returns>...</returns>
      private string GetColorString(Color c)
      {
         return string.Format("\\red{0}\\green{1}\\blue{2};", c.R, c.G, c.B);
      }

      /// <summary>
      /// This method rebuilds the color dictionary with the currently selected
      ///  colors from the settings menu and then applies it to the text once.
      /// </summary>
      /// <remarks>id : 20130604°2215</remarks>
      public void ResetHighlightColors()
      {
         if (Properties.Settings.Default.SyntaxHighlighting)
         {
            _rtfColorDictionary = "{\\colortbl ;"
                                 + GetColorString(Properties.Settings.Default.ColorKeywords)
                                 + GetColorString(Properties.Settings.Default.ColorStrings)
                                 + GetColorString(Properties.Settings.Default.ColorOperators)
                                 + GetColorString(Properties.Settings.Default.ColorNumbers)
                                 + "}"
                                  ;
         }
         else
         {
            _rtfColorDictionary = "{\\colortbl ;"
                                 + GetColorString(Color.Black)
                                 + GetColorString(Color.Black)
                                 + GetColorString(Color.Black)
                                 + GetColorString(Color.Black)
                                 + "}"
                                  ;
         }
         HighlightSyntax();
      }

      /// <summary>This method performs the syntax highlighting in the ... pane</summary>
      /// <remarks>id : 20130604°2216</remarks>
      private void HighlightSyntax()
      {
         // Here it happens
         // Note : I'm quite aware that this is a kind of clumsy approach, but I
         //    didn't know of any better way without having to do a major rewrite.
         //    Feel free to suggest improvements (or to improve it yourself, or write
         //    to your head of state, whatever you like). [note 20130604°221603]

         // Used to save the position of the cursor, so that when the RTF is changed
         //  the cursor does not jump to the end of the text if it wasn't already there
         _cursorPos = richtextbox_Query.SelectionStart;

         // A stringbuilder used to put together the formatted text, it starts
         //  with the plain text and replaces line breaks
         StringBuilder sb = new StringBuilder(richtextbox_Query.Text.Replace("\\", "\\\\").Replace("\n", "\\par\r\n"));

         // Get the strings and colour them first (i.e. text contained in ' characters)
         // note : For each match, set the corresponding number from the color table before
         //    the match and standard color after the match. Iterating through the matches in
         //    reverse order prevents the other matche-indices from being affected by the
         //    changing length of the text. [note 20130604°221604]
         _regexStringsMatchCollection = _regexSQLStrings.Matches(sb.ToString());
         for (int i = _regexStringsMatchCollection.Count - 1; i >= 0; i--)
         {
            sb.Insert(_regexStringsMatchCollection[i].Index + _regexStringsMatchCollection[i].Length, "\\cf0 ");
            sb.Insert(_regexStringsMatchCollection[i].Index, "\\cf2 ");
         }

         // Same for the keywords
         // Note : But due to our workaround above, the MatchCollection also
         //    contains the strings, so we only handle the matches that don't
         //    start with the ' character. [note 20130604°221605]
         _regexWordsMatchCollection = _regexSQLKeywords.Matches(sb.ToString());
         for (int i = _regexWordsMatchCollection.Count - 1; i >= 0; i--)
         {
            if (sb[_regexWordsMatchCollection[i].Index] != '\'')
            {
               sb.Insert(_regexWordsMatchCollection[i].Index + _regexWordsMatchCollection[i].Length, "\\cf0 ");
               sb.Insert(_regexWordsMatchCollection[i].Index, "\\cf1 ");
            }
         }

         // Now find operators, brackets etc. and mark them, same procedure as above
         _regexOperatorsMatchCollection = _regexSQLOperators.Matches(sb.ToString());
         for (int i = _regexOperatorsMatchCollection.Count - 1; i >= 0; i--)
         {
            if (sb[_regexOperatorsMatchCollection[i].Index] != '\'')
            {
               sb.Insert(_regexOperatorsMatchCollection[i].Index + _regexOperatorsMatchCollection[i].Length, "\\cf0 ");
               sb.Insert(_regexOperatorsMatchCollection[i].Index, "\\cf3 ");
            }
         }

         // Now the same for the numbers
         _regexNumbersMatchCollection = _regexSQLNumbers.Matches(sb.ToString());
         for (int i = _regexNumbersMatchCollection.Count - 1; i >= 0; i--)
         {
            if (sb[_regexNumbersMatchCollection[i].Index] != '\'')
            {
               sb.Insert(_regexNumbersMatchCollection[i].Index + _regexNumbersMatchCollection[i].Length, "\\cf0 ");
               sb.Insert(_regexNumbersMatchCollection[i].Index, "\\cf4 ");
            }
         }

         // Build a new RTF string, the colortbl is created from the colours
         //  we want to use, and the newly formatted text inserted
         string newRtf = "{\\rtf1\\ansi\\deff0{\\fonttbl{\\f0\fnil\\fcharset0 Verdana;}}"
                        + _rtfColorDictionary
                         + "\\viewkind4\\uc1\\pard\\lang1031\\f0\\fs20 " + sb + "\\par}"
                          ;

         // This is to prevent an infinite loop when setting the Rtf Property of txtQuery below.
         _ignoreTextChanged = true;

         // Don't draw this control to keep it from flickering
         FreezeDraw();

         richtextbox_Query.Rtf = newRtf;

         // So that future TextChanged events won't be ignored
         _ignoreTextChanged = false;

         // Set the cursor to its last position. NOTE: this should put the cursor
         //  in the right position, but might cause the screen to scroll there.
         //  I have not yet found a way to change the RichtTextBox scroll position.
         richtextbox_Query.Select(_cursorPos, 0);

         // Resume drawing the control
         UnFreezeDraw();

         // A similar logic could be used to set fonts, bold, italic and other
         // properties as well. Main problem is performance. With longer queries,
         // there might be lags while typing if too much is done in the TextChanged
         // event. Apparently, every way to improve on this would require rewriting
         // large parts.
      }

      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°2217</remarks>
      /// <param name="sender">The object which sent this event</param>
      /// <param name="e">The event object itself</param>
      private void txtQuery_TextChanged(object sender, EventArgs e)
      {
         if (! _ignoreTextChanged && Properties.Settings.Default.SyntaxHighlighting)
         {
            HighlightSyntax();
         }
      }

      //----------------------------------------------------
      // Issue 20130619°0513
      // Note : See method 20130619°0512 GetQueryChild() what about the hardcoded
      //    index '0' into a controls array. Should this be somehow adapted?
      // Location : Method 20130704°1251 button_Queryform_Close_Click()
      //----------------------------------------------------

      /// <summary>This eventhandler processes the Queryform Close Button's Click event</summary>
      /// <remarks>id : 20130704°1251</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_Queryform_Close_Click(object sender, EventArgs e)
      {

         // Finish DbClient thread
         MainForm._mainform.DoDisconnect();

      }

      /// <summary>This eventhandler processes the ShowDedicatedTreeview checkbox's CheckedChanged event</summary>
      /// <remarks>id : 20130704°1301</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void checkbox_ShowDedicatedTreeview_CheckedChanged(object sender, EventArgs e)
      {
         CheckBox cb = sender as CheckBox;

         if (cb.Checked)
         {
            this.HideBrowser = false;
         }
         else
         {
            this.HideBrowser = true;
         }
      }

      /// <summary>This eventhandler processes the 'Clone DB' button click event</summary>
      /// <remarks>id : 20130818°1501</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_CloneDb_Click(object sender, EventArgs e)
      {
         bool b = cloneDatabase();

         if (! b)
         {
            string s = "Clone database failed";
            MainForm.outputStatusLine(s);
         }
      }

      /// <summary>This method clones a database</summary>
      /// <remarks>id : 20130818°1502</remarks>
      /// <returns>Success flag</returns>
      private bool cloneDatabase()
      {
         bool bRet = false;

         // Read out target settings
         ConnSettingsLib csLib = new ConnSettingsLib();
         csLib.Type = (ConnSettingsLib.ConnectionType) this.combobox_ClonetoDbtype.SelectedItem;
         csLib.DatabaseServerUrl = this.textbox_ClonetoServer.Text;
         csLib.DatabaseServerAddress = this.textbox_ClonetoServer.Text;
         csLib.DatabaseName = this.textbox_ClonetoDatabase.Text;

         // Prepare the DbClone object
         Clone clone = new Clone();

         // Do the cloning
         bRet = clone.CloneDb(this.DbClient, csLib);

         // [todo 20130821°1134]
         // Now it were nice, we could immediately show the new database.
         //  Only that seems not yet soo easy. So far, all connection
         //  displaying is done from the ConnectForm, we have no immediate
         //  access to that ... and there is also not yet any intermediate
         //  command method to perform such task ... isn't it?

         return bRet;
      }

      /// <summary>This method switches the visibility of the developer objects</summary>
      /// <remarks>id : 20130828°1616</remarks>
      /// <param name="bVisible">The flag telling whether the developer objects shall be visible or not</param>
      public void switchDeveloperObjectsVisibility(bool bVisible)
      {
         // Clone dialog fields
         if (this.DbClient.ConnSettings.Type != ConnSettingsLib.ConnectionType.OleDb)
         {
            this.panel_DbClone.Visible = bVisible;
         }

         // Tabpage, has no Visibility property but must added/removed
         if (bVisible)
         {
            this.tabcontrol_Queryform.TabPages.Add(this.tabpage_Options);
         }
         else
         {
            this.tabcontrol_Queryform.TabPages.Remove(this.tabpage_Options);
         }
      }

      /// <summary>Thie eventhandler processes the Button1 click event</summary>
      /// <remarks>id : 20130902°0601</remarks>
      /// <param name="sender">The object which sent the event</param>
      /// <param name="e">The event object</param>
      private void button1_Click(object sender, EventArgs e)
      {
      }
   }
}
