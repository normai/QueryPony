namespace QueryPonyGui
{
    partial class QueryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           this.components = new System.ComponentModel.Container();
           ////System.Windows.Forms.TabPage tabpage_Options;
           ////this.tabpage_Options = new System.Windows.Forms.TabPage();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryForm));
           this.button1 = new System.Windows.Forms.Button();
           this.splitBrowser = new System.Windows.Forms.SplitContainer();
           this.comboboxDatabase = new System.Windows.Forms.ComboBox();
           this.label1 = new System.Windows.Forms.Label();
           this.treeView = new System.Windows.Forms.TreeView();
           this.splitContainer_RightSide = new System.Windows.Forms.SplitContainer();
           this.button_Queryform_Close = new System.Windows.Forms.Button();
           this.tabcontrol_Queryform = new System.Windows.Forms.TabControl();
           this.tabpage_Statements = new System.Windows.Forms.TabPage();
           this.splitContainer2 = new System.Windows.Forms.SplitContainer();
           this.richtextbox_Query = new System.Windows.Forms.RichTextBox();
           this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
           this.statusStrip1 = new System.Windows.Forms.StatusStrip();
           this.panRunStatus = new System.Windows.Forms.ToolStripStatusLabel();
           this.panExecTime = new System.Windows.Forms.ToolStripStatusLabel();
           this.panRows = new System.Windows.Forms.ToolStripStatusLabel();
           this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
           this.tabcontrol_Results = new System.Windows.Forms.TabControl();
           this.tabpage_More = new System.Windows.Forms.TabPage();
           this.panel_DbClone = new System.Windows.Forms.Panel();
           this.textbox_ClonetoDatabase = new System.Windows.Forms.TextBox();
           this.label_CloneToDatabase = new System.Windows.Forms.Label();
           this.button_CloneDb = new System.Windows.Forms.Button();
           this.label_CloneToServer = new System.Windows.Forms.Label();
           this.label_CloneFrom = new System.Windows.Forms.Label();
           this.label_CloneToDbtype = new System.Windows.Forms.Label();
           this.label_CloneTo = new System.Windows.Forms.Label();
           this.combobox_ClonetoDbtype = new System.Windows.Forms.ComboBox();
           this.textbox_CloneFrom = new System.Windows.Forms.TextBox();
           this.textbox_ClonetoServer = new System.Windows.Forms.TextBox();
           this.checkbox_ShowDedicatedTreeview = new System.Windows.Forms.CheckBox();
           this.tmrExecTime = new System.Windows.Forms.Timer(this.components);
           this.tabpage_Options = new System.Windows.Forms.TabPage();
           this.tabpage_Options.SuspendLayout();
           this.splitBrowser.Panel1.SuspendLayout();
           this.splitBrowser.Panel2.SuspendLayout();
           this.splitBrowser.SuspendLayout();
           this.splitContainer_RightSide.Panel1.SuspendLayout();
           this.splitContainer_RightSide.Panel2.SuspendLayout();
           this.splitContainer_RightSide.SuspendLayout();
           this.tabcontrol_Queryform.SuspendLayout();
           this.tabpage_Statements.SuspendLayout();
           this.splitContainer2.Panel1.SuspendLayout();
           this.splitContainer2.Panel2.SuspendLayout();
           this.splitContainer2.SuspendLayout();
           this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
           this.toolStripContainer1.ContentPanel.SuspendLayout();
           this.toolStripContainer1.SuspendLayout();
           this.statusStrip1.SuspendLayout();
           this.tabpage_More.SuspendLayout();
           this.panel_DbClone.SuspendLayout();
           this.SuspendLayout();
           //
           // tabpage_Options
           //
           this.tabpage_Options.Controls.Add(this.button1);
           this.tabpage_Options.Location = new System.Drawing.Point(4, 22);
           this.tabpage_Options.Name = "tabpage_Options";
           this.tabpage_Options.Size = new System.Drawing.Size(533, 430);
           this.tabpage_Options.TabIndex = 2;
           this.tabpage_Options.Text = "Options";
           this.tabpage_Options.UseVisualStyleBackColor = true;
           //
           // button1
           //
           this.button1.Location = new System.Drawing.Point(329, 151);
           this.button1.Name = "button1";
           this.button1.Size = new System.Drawing.Size(75, 23);
           this.button1.TabIndex = 0;
           this.button1.Text = "button1";
           this.button1.UseVisualStyleBackColor = true;
           this.button1.Click += new System.EventHandler(this.button1_Click);
           //
           // splitBrowser
           //
           this.splitBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
           this.splitBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitBrowser.Location = new System.Drawing.Point(0, 0);
           this.splitBrowser.Name = "splitBrowser";
           //
           // splitBrowser.Panel1
           //
           this.splitBrowser.Panel1.Controls.Add(this.comboboxDatabase);
           this.splitBrowser.Panel1.Controls.Add(this.label1);
           this.splitBrowser.Panel1.Controls.Add(this.treeView);
           //
           // splitBrowser.Panel2
           //
           this.splitBrowser.Panel2.Controls.Add(this.splitContainer_RightSide);
           this.splitBrowser.Size = new System.Drawing.Size(652, 486);
           this.splitBrowser.SplitterDistance = 103;
           this.splitBrowser.TabIndex = 0;
           this.splitBrowser.TabStop = false;
           //
           // comboboxDatabase
           //
           this.comboboxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.comboboxDatabase.FormattingEnabled = true;
           this.comboboxDatabase.Location = new System.Drawing.Point(3, 25);
           this.comboboxDatabase.Name = "comboboxDatabase";
           this.comboboxDatabase.Size = new System.Drawing.Size(93, 21);
           this.comboboxDatabase.TabIndex = 1;
           this.comboboxDatabase.TabStop = false;
           this.comboboxDatabase.SelectedIndexChanged += new System.EventHandler(this.comboboxDatabase_SelectedIndexChanged);
           this.comboboxDatabase.Enter += new System.EventHandler(this.comboboxDatabase_Enter);
           //
           // label1
           //
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(3, 9);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(53, 13);
           this.label1.TabIndex = 0;
           this.label1.Text = "Database";
           this.label1.VisibleChanged += new System.EventHandler(this.label1_VisibleChanged);
           //
           // treeView
           //
           this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.treeView.Location = new System.Drawing.Point(3, 52);
           this.treeView.Name = "treeView";
           this.treeView.Size = new System.Drawing.Size(93, 418);
           this.treeView.TabIndex = 2;
           this.treeView.TabStop = false;
           this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
           this.treeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseUp);
           this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
           this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
           //
           // splitContainer_RightSide
           //
           this.splitContainer_RightSide.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitContainer_RightSide.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
           this.splitContainer_RightSide.IsSplitterFixed = true;
           this.splitContainer_RightSide.Location = new System.Drawing.Point(0, 0);
           this.splitContainer_RightSide.Name = "splitContainer_RightSide";
           this.splitContainer_RightSide.Orientation = System.Windows.Forms.Orientation.Horizontal;
           //
           // splitContainer_RightSide.Panel1
           //
           this.splitContainer_RightSide.Panel1.Controls.Add(this.button_Queryform_Close);
           //
           // splitContainer_RightSide.Panel2
           //
           this.splitContainer_RightSide.Panel2.Controls.Add(this.tabcontrol_Queryform);
           this.splitContainer_RightSide.Size = new System.Drawing.Size(541, 482);
           this.splitContainer_RightSide.SplitterDistance = 25;
           this.splitContainer_RightSide.SplitterWidth = 1;
           this.splitContainer_RightSide.TabIndex = 1;
           //
           // button_Queryform_Close
           //
           this.button_Queryform_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.button_Queryform_Close.Location = new System.Drawing.Point(430, 1);
           this.button_Queryform_Close.Name = "button_Queryform_Close";
           this.button_Queryform_Close.Size = new System.Drawing.Size(101, 23);
           this.button_Queryform_Close.TabIndex = 0;
           this.button_Queryform_Close.Text = "Close connection";
           this.button_Queryform_Close.UseVisualStyleBackColor = true;
           this.button_Queryform_Close.Click += new System.EventHandler(this.button_Queryform_Close_Click);
           //
           // tabcontrol_Queryform
           //
           this.tabcontrol_Queryform.Controls.Add(this.tabpage_Statements);
           this.tabcontrol_Queryform.Controls.Add(this.tabpage_Options);
           this.tabcontrol_Queryform.Controls.Add(this.tabpage_More);
           this.tabcontrol_Queryform.Dock = System.Windows.Forms.DockStyle.Fill;
           this.tabcontrol_Queryform.Location = new System.Drawing.Point(0, 0);
           this.tabcontrol_Queryform.Name = "tabcontrol_Queryform";
           this.tabcontrol_Queryform.SelectedIndex = 0;
           this.tabcontrol_Queryform.Size = new System.Drawing.Size(541, 456);
           this.tabcontrol_Queryform.TabIndex = 1;
           //
           // tabpage_Statements
           //
           this.tabpage_Statements.Controls.Add(this.splitContainer2);
           this.tabpage_Statements.Location = new System.Drawing.Point(4, 22);
           this.tabpage_Statements.Name = "tabpage_Statements";
           this.tabpage_Statements.Padding = new System.Windows.Forms.Padding(3);
           this.tabpage_Statements.Size = new System.Drawing.Size(533, 430);
           this.tabpage_Statements.TabIndex = 0;
           this.tabpage_Statements.Text = "Statements";
           this.tabpage_Statements.UseVisualStyleBackColor = true;
           //
           // splitContainer2
           //
           this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
           this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitContainer2.Location = new System.Drawing.Point(3, 3);
           this.splitContainer2.Name = "splitContainer2";
           this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
           //
           // splitContainer2.Panel1
           //
           this.splitContainer2.Panel1.Controls.Add(this.richtextbox_Query);
           //
           // splitContainer2.Panel2
           //
           this.splitContainer2.Panel2.Controls.Add(this.toolStripContainer1);
           this.splitContainer2.Size = new System.Drawing.Size(527, 424);
           this.splitContainer2.SplitterDistance = 156;
           this.splitContainer2.SplitterWidth = 6;
           this.splitContainer2.TabIndex = 0;
           this.splitContainer2.TabStop = false;
           //
           // richtextbox_Query
           //
           this.richtextbox_Query.AcceptsTab = true;
           this.richtextbox_Query.AllowDrop = true;
           this.richtextbox_Query.BorderStyle = System.Windows.Forms.BorderStyle.None;
           this.richtextbox_Query.Dock = System.Windows.Forms.DockStyle.Fill;
           this.richtextbox_Query.Font = new System.Drawing.Font("Verdana", 10F);
           this.richtextbox_Query.Location = new System.Drawing.Point(0, 0);
           this.richtextbox_Query.Name = "richtextbox_Query";
           this.richtextbox_Query.Size = new System.Drawing.Size(523, 152);
           this.richtextbox_Query.TabIndex = 2;
           this.richtextbox_Query.Text = "";
           //
           // toolStripContainer1
           //
           //
           // toolStripContainer1.BottomToolStripPanel
           //
           this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
           //
           // toolStripContainer1.ContentPanel
           //
           this.toolStripContainer1.ContentPanel.Controls.Add(this.tabcontrol_Results);
           this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(523, 236);
           this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.toolStripContainer1.LeftToolStripPanelVisible = false;
           this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
           this.toolStripContainer1.Name = "toolStripContainer1";
           this.toolStripContainer1.RightToolStripPanelVisible = false;
           this.toolStripContainer1.Size = new System.Drawing.Size(523, 258);
           this.toolStripContainer1.TabIndex = 7;
           this.toolStripContainer1.Text = "toolStripContainer1";
           this.toolStripContainer1.TopToolStripPanelVisible = false;
           //
           // statusStrip1
           //
           this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
           this.statusStrip1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panRunStatus,
            this.panExecTime,
            this.panRows,
            this.toolStripStatusLabel1});
           this.statusStrip1.Location = new System.Drawing.Point(0, 0);
           this.statusStrip1.Name = "statusStrip1";
           this.statusStrip1.Size = new System.Drawing.Size(523, 22);
           this.statusStrip1.TabIndex = 6;
           this.statusStrip1.Text = "statusStrip1";
           //
           // panRunStatus
           //
           this.panRunStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
           this.panRunStatus.Name = "panRunStatus";
           this.panRunStatus.Size = new System.Drawing.Size(372, 17);
           this.panRunStatus.Spring = true;
           this.panRunStatus.Text = "Ready";
           //
           // panExecTime
           //
           this.panExecTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
           this.panExecTime.Name = "panExecTime";
           this.panExecTime.Size = new System.Drawing.Size(4, 17);
           //
           // panRows
           //
           this.panRows.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                       | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
           this.panRows.Name = "panRows";
           this.panRows.Size = new System.Drawing.Size(4, 17);
           //
           // toolStripStatusLabel1
           //
           this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
           this.toolStripStatusLabel1.Size = new System.Drawing.Size(128, 17);
           this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
           //
           // tabcontrol_Results
           //
           this.tabcontrol_Results.Alignment = System.Windows.Forms.TabAlignment.Bottom;
           this.tabcontrol_Results.Dock = System.Windows.Forms.DockStyle.Fill;
           this.tabcontrol_Results.Location = new System.Drawing.Point(0, 0);
           this.tabcontrol_Results.Name = "tabcontrol_Results";
           this.tabcontrol_Results.SelectedIndex = 0;
           this.tabcontrol_Results.Size = new System.Drawing.Size(523, 236);
           this.tabcontrol_Results.TabIndex = 0;
           //
           // tabpage_More
           //
           this.tabpage_More.Controls.Add(this.panel_DbClone);
           this.tabpage_More.Controls.Add(this.checkbox_ShowDedicatedTreeview);
           this.tabpage_More.Location = new System.Drawing.Point(4, 22);
           this.tabpage_More.Name = "tabpage_More";
           this.tabpage_More.Padding = new System.Windows.Forms.Padding(3);
           this.tabpage_More.Size = new System.Drawing.Size(533, 430);
           this.tabpage_More.TabIndex = 1;
           this.tabpage_More.Text = "More";
           this.tabpage_More.UseVisualStyleBackColor = true;
           //
           // panel_DbClone
           //
           this.panel_DbClone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
           this.panel_DbClone.Controls.Add(this.textbox_ClonetoDatabase);
           this.panel_DbClone.Controls.Add(this.label_CloneToDatabase);
           this.panel_DbClone.Controls.Add(this.button_CloneDb);
           this.panel_DbClone.Controls.Add(this.label_CloneToServer);
           this.panel_DbClone.Controls.Add(this.label_CloneFrom);
           this.panel_DbClone.Controls.Add(this.label_CloneToDbtype);
           this.panel_DbClone.Controls.Add(this.label_CloneTo);
           this.panel_DbClone.Controls.Add(this.combobox_ClonetoDbtype);
           this.panel_DbClone.Controls.Add(this.textbox_CloneFrom);
           this.panel_DbClone.Controls.Add(this.textbox_ClonetoServer);
           this.panel_DbClone.Location = new System.Drawing.Point(17, 62);
           this.panel_DbClone.Name = "panel_DbClone";
           this.panel_DbClone.Size = new System.Drawing.Size(484, 157);
           this.panel_DbClone.TabIndex = 23;
           //
           // textbox_ClonetoDatabase
           //
           this.textbox_ClonetoDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textbox_ClonetoDatabase.Location = new System.Drawing.Point(149, 104);
           this.textbox_ClonetoDatabase.Name = "textbox_ClonetoDatabase";
           this.textbox_ClonetoDatabase.Size = new System.Drawing.Size(287, 20);
           this.textbox_ClonetoDatabase.TabIndex = 18;
           //
           // label_CloneToDatabase
           //
           this.label_CloneToDatabase.AutoSize = true;
           this.label_CloneToDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_CloneToDatabase.Location = new System.Drawing.Point(85, 107);
           this.label_CloneToDatabase.Name = "label_CloneToDatabase";
           this.label_CloneToDatabase.Size = new System.Drawing.Size(64, 15);
           this.label_CloneToDatabase.TabIndex = 22;
           this.label_CloneToDatabase.Text = "Database:";
           //
           // button_CloneDb
           //
           this.button_CloneDb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.button_CloneDb.Location = new System.Drawing.Point(28, 26);
           this.button_CloneDb.Name = "button_CloneDb";
           this.button_CloneDb.Size = new System.Drawing.Size(73, 23);
           this.button_CloneDb.TabIndex = 13;
           this.button_CloneDb.Text = "Clone DB";
           this.button_CloneDb.UseVisualStyleBackColor = true;
           this.button_CloneDb.Click += new System.EventHandler(this.button_CloneDb_Click);
           //
           // label_CloneToServer
           //
           this.label_CloneToServer.AutoSize = true;
           this.label_CloneToServer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_CloneToServer.Location = new System.Drawing.Point(85, 81);
           this.label_CloneToServer.Name = "label_CloneToServer";
           this.label_CloneToServer.Size = new System.Drawing.Size(45, 15);
           this.label_CloneToServer.TabIndex = 21;
           this.label_CloneToServer.Text = "Server:";
           //
           // label_CloneFrom
           //
           this.label_CloneFrom.AutoSize = true;
           this.label_CloneFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_CloneFrom.Location = new System.Drawing.Point(108, 31);
           this.label_CloneFrom.Name = "label_CloneFrom";
           this.label_CloneFrom.Size = new System.Drawing.Size(32, 15);
           this.label_CloneFrom.TabIndex = 14;
           this.label_CloneFrom.Text = "from";
           //
           // label_CloneToDbtype
           //
           this.label_CloneToDbtype.AutoSize = true;
           this.label_CloneToDbtype.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_CloneToDbtype.Location = new System.Drawing.Point(85, 56);
           this.label_CloneToDbtype.Name = "label_CloneToDbtype";
           this.label_CloneToDbtype.Size = new System.Drawing.Size(56, 15);
           this.label_CloneToDbtype.TabIndex = 20;
           this.label_CloneToDbtype.Text = "DB Type:";
           //
           // label_CloneTo
           //
           this.label_CloneTo.AutoSize = true;
           this.label_CloneTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_CloneTo.Location = new System.Drawing.Point(47, 55);
           this.label_CloneTo.Name = "label_CloneTo";
           this.label_CloneTo.Size = new System.Drawing.Size(17, 15);
           this.label_CloneTo.TabIndex = 15;
           this.label_CloneTo.Text = "to";
           //
           // combobox_ClonetoDbtype
           //
           this.combobox_ClonetoDbtype.FormattingEnabled = true;
           this.combobox_ClonetoDbtype.Location = new System.Drawing.Point(149, 53);
           this.combobox_ClonetoDbtype.Name = "combobox_ClonetoDbtype";
           this.combobox_ClonetoDbtype.Size = new System.Drawing.Size(166, 21);
           this.combobox_ClonetoDbtype.TabIndex = 19;
           //
           // textbox_CloneFrom
           //
           this.textbox_CloneFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textbox_CloneFrom.Location = new System.Drawing.Point(149, 29);
           this.textbox_CloneFrom.Name = "textbox_CloneFrom";
           this.textbox_CloneFrom.Size = new System.Drawing.Size(287, 20);
           this.textbox_CloneFrom.TabIndex = 16;
           //
           // textbox_ClonetoServer
           //
           this.textbox_ClonetoServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textbox_ClonetoServer.Location = new System.Drawing.Point(149, 79);
           this.textbox_ClonetoServer.Name = "textbox_ClonetoServer";
           this.textbox_ClonetoServer.Size = new System.Drawing.Size(287, 20);
           this.textbox_ClonetoServer.TabIndex = 17;
           //
           // checkbox_ShowDedicatedTreeview
           //
           this.checkbox_ShowDedicatedTreeview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.checkbox_ShowDedicatedTreeview.AutoSize = true;
           this.checkbox_ShowDedicatedTreeview.Location = new System.Drawing.Point(220, 24);
           this.checkbox_ShowDedicatedTreeview.Name = "checkbox_ShowDedicatedTreeview";
           this.checkbox_ShowDedicatedTreeview.Size = new System.Drawing.Size(146, 17);
           this.checkbox_ShowDedicatedTreeview.TabIndex = 12;
           this.checkbox_ShowDedicatedTreeview.Text = "Show dedicated treeview";
           this.checkbox_ShowDedicatedTreeview.UseVisualStyleBackColor = true;
           this.checkbox_ShowDedicatedTreeview.CheckedChanged += new System.EventHandler(this.checkbox_ShowDedicatedTreeview_CheckedChanged);
           //
           // tmrExecTime
           //
           this.tmrExecTime.Interval = 1000;
           this.tmrExecTime.Tick += new System.EventHandler(this.tmrExecTime_Tick);
           //
           // QueryForm
           //
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.BackColor = System.Drawing.Color.WhiteSmoke;
           this.ClientSize = new System.Drawing.Size(652, 486);
           this.Controls.Add(this.splitBrowser);
           this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           this.KeyPreview = true;
           this.Name = "QueryForm";
           this.Text = "QueryForm";
           this.Activated += new System.EventHandler(this.QueryForm_Activated);
           this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryForm_FormClosed);
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryForm_FormClosing);
           this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryForm_KeyDown);
           this.tabpage_Options.ResumeLayout(false);
           this.splitBrowser.Panel1.ResumeLayout(false);
           this.splitBrowser.Panel1.PerformLayout();
           this.splitBrowser.Panel2.ResumeLayout(false);
           this.splitBrowser.ResumeLayout(false);
           this.splitContainer_RightSide.Panel1.ResumeLayout(false);
           this.splitContainer_RightSide.Panel2.ResumeLayout(false);
           this.splitContainer_RightSide.ResumeLayout(false);
           this.tabcontrol_Queryform.ResumeLayout(false);
           this.tabpage_Statements.ResumeLayout(false);
           this.splitContainer2.Panel1.ResumeLayout(false);
           this.splitContainer2.Panel2.ResumeLayout(false);
           this.splitContainer2.ResumeLayout(false);
           this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
           this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
           this.toolStripContainer1.ContentPanel.ResumeLayout(false);
           this.toolStripContainer1.ResumeLayout(false);
           this.toolStripContainer1.PerformLayout();
           this.statusStrip1.ResumeLayout(false);
           this.statusStrip1.PerformLayout();
           this.tabpage_More.ResumeLayout(false);
           this.tabpage_More.PerformLayout();
           this.panel_DbClone.ResumeLayout(false);
           this.panel_DbClone.PerformLayout();
           this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitBrowser;
        private System.Windows.Forms.ComboBox comboboxDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabcontrol_Results;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel panRunStatus;
        private System.Windows.Forms.ToolStripStatusLabel panExecTime;
        private System.Windows.Forms.ToolStripStatusLabel panRows;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Timer tmrExecTime;
        private System.Windows.Forms.Button button_Queryform_Close;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer_RightSide;
        private System.Windows.Forms.TabControl tabcontrol_Queryform;
        private System.Windows.Forms.TabPage tabpage_Statements;
        private System.Windows.Forms.TabPage tabpage_More;
        private System.Windows.Forms.TabPage tabpage_Options;
        private System.Windows.Forms.Label label_CloneToDatabase;
        private System.Windows.Forms.Label label_CloneToServer;
        private System.Windows.Forms.Label label_CloneToDbtype;
        private System.Windows.Forms.ComboBox combobox_ClonetoDbtype;
        private System.Windows.Forms.TextBox textbox_ClonetoDatabase;
        private System.Windows.Forms.TextBox textbox_ClonetoServer;
        private System.Windows.Forms.TextBox textbox_CloneFrom;
        private System.Windows.Forms.Label label_CloneTo;
        private System.Windows.Forms.Label label_CloneFrom;
        private System.Windows.Forms.Button button_CloneDb;
        private System.Windows.Forms.CheckBox checkbox_ShowDedicatedTreeview;
        private System.Windows.Forms.RichTextBox richtextbox_Query;
        private System.Windows.Forms.Panel panel_DbClone;
        private System.Windows.Forms.Button button1;

    }
}