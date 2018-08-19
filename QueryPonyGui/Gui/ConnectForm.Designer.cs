namespace QueryPonyGui
{
    partial class ConnectForm
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
           this.textBox1 = new System.Windows.Forms.TextBox();
           this.label5 = new System.Windows.Forms.Label();
           this.textBox2 = new System.Windows.Forms.TextBox();
           this.label6 = new System.Windows.Forms.Label();
           this.radioButton1 = new System.Windows.Forms.RadioButton();
           this.radioButton2 = new System.Windows.Forms.RadioButton();
           this.splitContainer = new System.Windows.Forms.SplitContainer();
           this.label_ToDatabase = new System.Windows.Forms.Label();
           this.pictureBox1 = new System.Windows.Forms.PictureBox();
           this.label_SignPost = new System.Windows.Forms.Label();
           this.labelTitle = new System.Windows.Forms.Label();
           this.button_LoadDemoConnectionSettings = new System.Windows.Forms.Button();
           this.button_DeleteConnection = new System.Windows.Forms.Button();
           this.button_SaveSettings = new System.Windows.Forms.Button();
           this.combobox_Connection = new System.Windows.Forms.ComboBox();
           this.label31 = new System.Windows.Forms.Label();
           this.button_Connect = new System.Windows.Forms.Button();
           this.button_Cancel = new System.Windows.Forms.Button();
           this.checkbox_LowBandwidth = new System.Windows.Forms.CheckBox();
           this.tabcontrol_ServerTypes = new CSharpCustomTabControl.CustomTabControl();
           this.tabpage_Mssql = new System.Windows.Forms.TabPage();
           this.label35 = new System.Windows.Forms.Label();
           this.label_Mssql_CapslockIsOn = new System.Windows.Forms.Label();
           this.textbox_Mssql_Password = new System.Windows.Forms.TextBox();
           this.label3 = new System.Windows.Forms.Label();
           this.textbox_Mssql_LoginName = new System.Windows.Forms.TextBox();
           this.label2 = new System.Windows.Forms.Label();
           this.radiobutton_Mssql_Untrusted = new System.Windows.Forms.RadioButton();
           this.radiobutton_Mssql_Trusted = new System.Windows.Forms.RadioButton();
           this.textbox_Mssql_ServerAddress = new System.Windows.Forms.TextBox();
           this.label18 = new System.Windows.Forms.Label();
           this.combobox_Mssql_DatabaseName = new System.Windows.Forms.ComboBox();
           this.label1 = new System.Windows.Forms.Label();
           this.tabpage_Mysql = new System.Windows.Forms.TabPage();
           this.checkbox_Mysql_IntegratedSecurity = new System.Windows.Forms.CheckBox();
           this.combobox_Mysql_DatabaseName = new System.Windows.Forms.ComboBox();
           this.checkbox_Mysql_SavePassword = new System.Windows.Forms.CheckBox();
           this.label_Mysql_CapslockIsOn = new System.Windows.Forms.Label();
           this.textbox_Mysql_Password = new System.Windows.Forms.TextBox();
           this.label_Mysql_Password = new System.Windows.Forms.Label();
           this.textbox_Mysql_LoginName = new System.Windows.Forms.TextBox();
           this.label_Mysql_LoginName = new System.Windows.Forms.Label();
           this.textbox_Mysql_ServerAddress = new System.Windows.Forms.TextBox();
           this.label19 = new System.Windows.Forms.Label();
           this.label_tabMysql_Server = new System.Windows.Forms.Label();
           this.tabpage_Odbc = new System.Windows.Forms.TabPage();
           this.textBox5 = new System.Windows.Forms.TextBox();
           this.label11 = new System.Windows.Forms.Label();
           this.button_Odbc_Save = new System.Windows.Forms.Button();
           this.button_Odbc_Load = new System.Windows.Forms.Button();
           this.label4 = new System.Windows.Forms.Label();
           this.textbox_Odbc_ConnectionString = new System.Windows.Forms.TextBox();
           this.tabpage_Oledb = new System.Windows.Forms.TabPage();
           this.textBox6 = new System.Windows.Forms.TextBox();
           this.label12 = new System.Windows.Forms.Label();
           this.button_Oledb_Save = new System.Windows.Forms.Button();
           this.button_Oledb_Load = new System.Windows.Forms.Button();
           this.label10 = new System.Windows.Forms.Label();
           this.textbox_Oledb_ConnectionString = new System.Windows.Forms.TextBox();
           this.tabpage_Oracle = new System.Windows.Forms.TabPage();
           this.combobox_Oracle_DatabaseName = new System.Windows.Forms.ComboBox();
           this.label37 = new System.Windows.Forms.Label();
           this.label36 = new System.Windows.Forms.Label();
           this.label_Oracle_CapslockIsOn = new System.Windows.Forms.Label();
           this.textbox_Oracle_Password = new System.Windows.Forms.TextBox();
           this.label7 = new System.Windows.Forms.Label();
           this.textbox_Oracle_LoginName = new System.Windows.Forms.TextBox();
           this.label8 = new System.Windows.Forms.Label();
           this.radiobutton_Oracle_Untrusted = new System.Windows.Forms.RadioButton();
           this.radiobutton_Oracle_Trusted = new System.Windows.Forms.RadioButton();
           this.textbox_Oracle_DataSource = new System.Windows.Forms.TextBox();
           this.label9 = new System.Windows.Forms.Label();
           this.tabpage_Pgsql = new System.Windows.Forms.TabPage();
           this.label_Pgsql_CapslockIsOn = new System.Windows.Forms.Label();
           this.textbox_Pgsql_Password = new System.Windows.Forms.TextBox();
           this.label15 = new System.Windows.Forms.Label();
           this.textbox_Pgsql_LoginName = new System.Windows.Forms.TextBox();
           this.label16 = new System.Windows.Forms.Label();
           this.textbox_Pgsql_ServerAddress = new System.Windows.Forms.TextBox();
           this.label20 = new System.Windows.Forms.Label();
           this.combobox_Pgsql_DatabaseName = new System.Windows.Forms.ComboBox();
           this.label17 = new System.Windows.Forms.Label();
           this.tabpage_Sqlite = new System.Windows.Forms.TabPage();
           this.checkbox_Sqlite_SavePassword = new System.Windows.Forms.CheckBox();
           this.label_Sqlite_CapslockIsOn = new System.Windows.Forms.Label();
           this.textbox_Sqlite_Password = new System.Windows.Forms.TextBox();
           this.label40 = new System.Windows.Forms.Label();
           this.textbox_Sqlite_LoginName = new System.Windows.Forms.TextBox();
           this.label41 = new System.Windows.Forms.Label();
           this.textbox_Sqlite_ServerAddress = new System.Windows.Forms.TextBox();
           this.label38 = new System.Windows.Forms.Label();
           this.button_BrowseSqliteFile = new System.Windows.Forms.Button();
           this.textBox_SqliteFile = new System.Windows.Forms.TextBox();
           this.label_SqliteFile = new System.Windows.Forms.Label();
           this.tabpage_Couch = new System.Windows.Forms.TabPage();
           this.label25 = new System.Windows.Forms.Label();
           this.textbox_Couch_Password = new System.Windows.Forms.TextBox();
           this.label_Couch_CapslockIsOn = new System.Windows.Forms.Label();
           this.label22 = new System.Windows.Forms.Label();
           this.textbox_Couch_LoginName = new System.Windows.Forms.TextBox();
           this.label23 = new System.Windows.Forms.Label();
           this.textbox_Couch_ServerAddress = new System.Windows.Forms.TextBox();
           this.label21 = new System.Windows.Forms.Label();
           this.combobox_Couch_DatabaseName = new System.Windows.Forms.ComboBox();
           this.label24 = new System.Windows.Forms.Label();
           this.textBox3 = new System.Windows.Forms.TextBox();
           this.label13 = new System.Windows.Forms.Label();
           this.textBox4 = new System.Windows.Forms.TextBox();
           this.label14 = new System.Windows.Forms.Label();
           this.splitContainer.Panel1.SuspendLayout();
           this.splitContainer.Panel2.SuspendLayout();
           this.splitContainer.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
           this.tabcontrol_ServerTypes.SuspendLayout();
           this.tabpage_Mssql.SuspendLayout();
           this.tabpage_Mysql.SuspendLayout();
           this.tabpage_Odbc.SuspendLayout();
           this.tabpage_Oledb.SuspendLayout();
           this.tabpage_Oracle.SuspendLayout();
           this.tabpage_Pgsql.SuspendLayout();
           this.tabpage_Sqlite.SuspendLayout();
           this.tabpage_Couch.SuspendLayout();
           this.SuspendLayout();
           //
           // textBox1
           //
           this.textBox1.Location = new System.Drawing.Point(109, 91);
           this.textBox1.Name = "textBox1";
           this.textBox1.Size = new System.Drawing.Size(133, 20);
           this.textBox1.TabIndex = 5;
           //
           // label5
           //
           this.label5.AutoSize = true;
           this.label5.Location = new System.Drawing.Point(38, 98);
           this.label5.Name = "label5";
           this.label5.Size = new System.Drawing.Size(56, 13);
           this.label5.TabIndex = 4;
           this.label5.Text = "&Password:";
           //
           // textBox2
           //
           this.textBox2.Location = new System.Drawing.Point(109, 65);
           this.textBox2.Name = "textBox2";
           this.textBox2.Size = new System.Drawing.Size(133, 20);
           this.textBox2.TabIndex = 3;
           //
           // label6
           //
           this.label6.AutoSize = true;
           this.label6.Location = new System.Drawing.Point(38, 72);
           this.label6.Name = "label6";
           this.label6.Size = new System.Drawing.Size(65, 13);
           this.label6.TabIndex = 2;
           this.label6.Text = "&Login name:";
           //
           // radioButton1
           //
           this.radioButton1.AutoSize = true;
           this.radioButton1.Location = new System.Drawing.Point(20, 42);
           this.radioButton1.Name = "radioButton1";
           this.radioButton1.Size = new System.Drawing.Size(151, 17);
           this.radioButton1.TabIndex = 1;
           this.radioButton1.Text = "S&QL Server Authentication";
           this.radioButton1.UseVisualStyleBackColor = true;
           //
           // radioButton2
           //
           this.radioButton2.AutoSize = true;
           this.radioButton2.Location = new System.Drawing.Point(20, 19);
           this.radioButton2.Name = "radioButton2";
           this.radioButton2.Size = new System.Drawing.Size(140, 17);
           this.radioButton2.TabIndex = 0;
           this.radioButton2.Text = "&Windows Authentication";
           this.radioButton2.UseVisualStyleBackColor = true;
           //
           // splitContainer
           //
           this.splitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.splitContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
           this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitContainer.IsSplitterFixed = true;
           this.splitContainer.Location = new System.Drawing.Point(0, 0);
           this.splitContainer.Name = "splitContainer";
           this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
           //
           // splitContainer.Panel1
           //
           this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.splitContainer.Panel1.Controls.Add(this.label_ToDatabase);
           this.splitContainer.Panel1.Controls.Add(this.pictureBox1);
           this.splitContainer.Panel1.Controls.Add(this.label_SignPost);
           this.splitContainer.Panel1.Controls.Add(this.labelTitle);
           this.splitContainer.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
           //
           // splitContainer.Panel2
           //
           this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.splitContainer.Panel2.Controls.Add(this.button_LoadDemoConnectionSettings);
           this.splitContainer.Panel2.Controls.Add(this.button_DeleteConnection);
           this.splitContainer.Panel2.Controls.Add(this.button_SaveSettings);
           this.splitContainer.Panel2.Controls.Add(this.combobox_Connection);
           this.splitContainer.Panel2.Controls.Add(this.label31);
           this.splitContainer.Panel2.Controls.Add(this.button_Connect);
           this.splitContainer.Panel2.Controls.Add(this.button_Cancel);
           this.splitContainer.Panel2.Controls.Add(this.checkbox_LowBandwidth);
           this.splitContainer.Panel2.Controls.Add(this.tabcontrol_ServerTypes);
           this.splitContainer.Size = new System.Drawing.Size(538, 376);
           this.splitContainer.SplitterDistance = 64;
           this.splitContainer.SplitterWidth = 1;
           this.splitContainer.TabIndex = 0;
           //
           // label_ToDatabase
           //
           this.label_ToDatabase.AutoSize = true;
           this.label_ToDatabase.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_ToDatabase.ForeColor = System.Drawing.Color.Blue;
           this.label_ToDatabase.Location = new System.Drawing.Point(113, 13);
           this.label_ToDatabase.Name = "label_ToDatabase";
           this.label_ToDatabase.Size = new System.Drawing.Size(114, 19);
           this.label_ToDatabase.TabIndex = 73;
           this.label_ToDatabase.Text = "Xxxxxxxxxxxxx";
           //
           // pictureBox1
           //
           this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
           this.pictureBox1.Location = new System.Drawing.Point(473, 6);
           this.pictureBox1.Name = "pictureBox1";
           this.pictureBox1.Size = new System.Drawing.Size(52, 52);
           this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
           this.pictureBox1.TabIndex = 72;
           this.pictureBox1.TabStop = false;
           //
           // label_SignPost
           //
           this.label_SignPost.AutoSize = true;
           this.label_SignPost.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_SignPost.ForeColor = System.Drawing.Color.Aquamarine;
           this.label_SignPost.Location = new System.Drawing.Point(300, 5);
           this.label_SignPost.Name = "label_SignPost";
           this.label_SignPost.Size = new System.Drawing.Size(101, 54);
           this.label_SignPost.TabIndex = 3;
           this.label_SignPost.Text = "Prototype\r\nVersion";
           this.label_SignPost.TextAlign = System.Drawing.ContentAlignment.TopCenter;
           //
           // labelTitle
           //
           this.labelTitle.AutoSize = true;
           this.labelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
           this.labelTitle.Location = new System.Drawing.Point(17, 13);
           this.labelTitle.Name = "labelTitle";
           this.labelTitle.Size = new System.Drawing.Size(97, 19);
           this.labelTitle.TabIndex = 0;
           this.labelTitle.Text = "Connect to";
           //
           // button_LoadDemoConnectionSettings
           //
           this.button_LoadDemoConnectionSettings.BackColor = System.Drawing.SystemColors.Control;
           this.button_LoadDemoConnectionSettings.Font = new System.Drawing.Font("Segoe Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.button_LoadDemoConnectionSettings.ForeColor = System.Drawing.Color.Black;
           this.button_LoadDemoConnectionSettings.Location = new System.Drawing.Point(15, 274);
           this.button_LoadDemoConnectionSettings.Name = "button_LoadDemoConnectionSettings";
           this.button_LoadDemoConnectionSettings.Size = new System.Drawing.Size(145, 23);
           this.button_LoadDemoConnectionSettings.TabIndex = 40;
           this.button_LoadDemoConnectionSettings.Text = "Load Demo Connection Settings";
           this.button_LoadDemoConnectionSettings.UseVisualStyleBackColor = false;
           this.button_LoadDemoConnectionSettings.Click += new System.EventHandler(this.button_LoadDemoConnectionSettings_Click);
           //
           // button_DeleteConnection
           //
           this.button_DeleteConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.button_DeleteConnection.BackColor = System.Drawing.SystemColors.Control;
           this.button_DeleteConnection.Location = new System.Drawing.Point(452, 8);
           this.button_DeleteConnection.Name = "button_DeleteConnection";
           this.button_DeleteConnection.Size = new System.Drawing.Size(48, 23);
           this.button_DeleteConnection.TabIndex = 20;
           this.button_DeleteConnection.Text = "&Delete";
           this.button_DeleteConnection.UseVisualStyleBackColor = false;
           this.button_DeleteConnection.Click += new System.EventHandler(this.button_DeleteConnection_Click);
           //
           // button_SaveSettings
           //
           this.button_SaveSettings.Font = new System.Drawing.Font("Tahoma", 7.2F);
           this.button_SaveSettings.ForeColor = System.Drawing.Color.Turquoise;
           this.button_SaveSettings.Location = new System.Drawing.Point(170, 274);
           this.button_SaveSettings.Name = "button_SaveSettings";
           this.button_SaveSettings.Size = new System.Drawing.Size(76, 23);
           this.button_SaveSettings.TabIndex = 70;
           this.button_SaveSettings.TabStop = false;
           this.button_SaveSettings.Text = "Save Settings";
           this.button_SaveSettings.UseVisualStyleBackColor = true;
           this.button_SaveSettings.Visible = false;
           this.button_SaveSettings.Click += new System.EventHandler(this.button_SaveSettings_Click_DEBUG);
           //
           // combobox_Connection
           //
           this.combobox_Connection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Connection.DropDownHeight = 199;
           this.combobox_Connection.ForeColor = System.Drawing.Color.Black;
           this.combobox_Connection.FormattingEnabled = true;
           this.combobox_Connection.IntegralHeight = false;
           this.combobox_Connection.Location = new System.Drawing.Point(114, 9);
           this.combobox_Connection.MaxDropDownItems = 24;
           this.combobox_Connection.Name = "combobox_Connection";
           this.combobox_Connection.Size = new System.Drawing.Size(333, 21);
           this.combobox_Connection.Sorted = true;
           this.combobox_Connection.TabIndex = 10;
           this.combobox_Connection.SelectedIndexChanged += new System.EventHandler(this.combobox_Connection_SelectedIndexChanged);
           this.combobox_Connection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combobox_Connection_KeyPress);
           //
           // label31
           //
           this.label31.AutoSize = true;
           this.label31.ForeColor = System.Drawing.Color.Black;
           this.label31.Location = new System.Drawing.Point(17, 12);
           this.label31.Name = "label31";
           this.label31.Size = new System.Drawing.Size(64, 13);
           this.label31.TabIndex = 11;
           this.label31.Text = "&Connection:";
           //
           // button_Connect
           //
           this.button_Connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.button_Connect.BackColor = System.Drawing.SystemColors.Control;
           this.button_Connect.Location = new System.Drawing.Point(370, 274);
           this.button_Connect.Name = "button_Connect";
           this.button_Connect.Size = new System.Drawing.Size(75, 23);
           this.button_Connect.TabIndex = 50;
           this.button_Connect.Text = "&Connect";
           this.button_Connect.UseVisualStyleBackColor = false;
           this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
           //
           // button_Cancel
           //
           this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.button_Cancel.BackColor = System.Drawing.SystemColors.Control;
           this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.button_Cancel.Location = new System.Drawing.Point(451, 274);
           this.button_Cancel.Name = "button_Cancel";
           this.button_Cancel.Size = new System.Drawing.Size(75, 23);
           this.button_Cancel.TabIndex = 60;
           this.button_Cancel.Text = "Cancel";
           this.button_Cancel.UseVisualStyleBackColor = false;
           //
           // checkbox_LowBandwidth
           //
           this.checkbox_LowBandwidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.checkbox_LowBandwidth.AutoSize = true;
           this.checkbox_LowBandwidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.checkbox_LowBandwidth.ForeColor = System.Drawing.Color.DimGray;
           this.checkbox_LowBandwidth.Location = new System.Drawing.Point(262, 266);
           this.checkbox_LowBandwidth.Name = "checkbox_LowBandwidth";
           this.checkbox_LowBandwidth.Size = new System.Drawing.Size(98, 17);
           this.checkbox_LowBandwidth.TabIndex = 30;
           this.checkbox_LowBandwidth.Text = "Low bandwidth";
           this.checkbox_LowBandwidth.UseVisualStyleBackColor = true;
           this.checkbox_LowBandwidth.Visible = false;
           this.checkbox_LowBandwidth.CheckedChanged += new System.EventHandler(this.checkbox_LowBandwidth_CheckedChanged);
           //
           // tabcontrol_ServerTypes
           //
           this.tabcontrol_ServerTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Mssql);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Mysql);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Odbc);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Oledb);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Oracle);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Pgsql);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Sqlite);
           this.tabcontrol_ServerTypes.Controls.Add(this.tabpage_Couch);
           this.tabcontrol_ServerTypes.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
           this.tabcontrol_ServerTypes.ItemSize = new System.Drawing.Size(0, 15);
           this.tabcontrol_ServerTypes.Location = new System.Drawing.Point(10, 37);
           this.tabcontrol_ServerTypes.Name = "tabcontrol_ServerTypes";
           this.tabcontrol_ServerTypes.Padding = new System.Drawing.Point(9, 0);
           this.tabcontrol_ServerTypes.SelectedIndex = 0;
           this.tabcontrol_ServerTypes.Size = new System.Drawing.Size(516, 224);
           this.tabcontrol_ServerTypes.TabIndex = 8;
           this.tabcontrol_ServerTypes.Leave += new System.EventHandler(this.tabcontrol_ServerTypes_Leave);
           this.tabcontrol_ServerTypes.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabcontrol_ServerTypes_DrawItem);
           this.tabcontrol_ServerTypes.Enter += new System.EventHandler(this.tabcontrol_ServerTypes_Enter);
           this.tabcontrol_ServerTypes.SelectedIndexChanged += new System.EventHandler(this.tabcontrol_ServerTypes_SelectedIndexChanged);
           //
           // tabpage_Mssql
           //
           this.tabpage_Mssql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Mssql.Controls.Add(this.label35);
           this.tabpage_Mssql.Controls.Add(this.label_Mssql_CapslockIsOn);
           this.tabpage_Mssql.Controls.Add(this.textbox_Mssql_Password);
           this.tabpage_Mssql.Controls.Add(this.label3);
           this.tabpage_Mssql.Controls.Add(this.textbox_Mssql_LoginName);
           this.tabpage_Mssql.Controls.Add(this.label2);
           this.tabpage_Mssql.Controls.Add(this.radiobutton_Mssql_Untrusted);
           this.tabpage_Mssql.Controls.Add(this.radiobutton_Mssql_Trusted);
           this.tabpage_Mssql.Controls.Add(this.textbox_Mssql_ServerAddress);
           this.tabpage_Mssql.Controls.Add(this.label18);
           this.tabpage_Mssql.Controls.Add(this.combobox_Mssql_DatabaseName);
           this.tabpage_Mssql.Controls.Add(this.label1);
           this.tabpage_Mssql.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Mssql.Name = "tabpage_Mssql";
           this.tabpage_Mssql.Padding = new System.Windows.Forms.Padding(3);
           this.tabpage_Mssql.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Mssql.TabIndex = 0;
           this.tabpage_Mssql.Tag = "";
           this.tabpage_Mssql.Text = "MS-SQL  ";
           this.tabpage_Mssql.UseVisualStyleBackColor = true;
           //
           // label35
           //
           this.label35.AutoSize = true;
           this.label35.BackColor = System.Drawing.Color.Transparent;
           this.label35.Location = new System.Drawing.Point(6, 102);
           this.label35.Name = "label35";
           this.label35.Size = new System.Drawing.Size(37, 13);
           this.label35.TabIndex = 22;
           this.label35.Text = "&Using:";
           //
           // label_Mssql_CapslockIsOn
           //
           this.label_Mssql_CapslockIsOn.AutoSize = true;
           this.label_Mssql_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Mssql_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Mssql_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Mssql_CapslockIsOn.Location = new System.Drawing.Point(240, 148);
           this.label_Mssql_CapslockIsOn.Name = "label_Mssql_CapslockIsOn";
           this.label_Mssql_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Mssql_CapslockIsOn.TabIndex = 21;
           this.label_Mssql_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Mssql_CapslockIsOn.Visible = false;
           //
           // textbox_Mssql_Password
           //
           this.textbox_Mssql_Password.AcceptsReturn = true;
           this.textbox_Mssql_Password.Location = new System.Drawing.Point(101, 148);
           this.textbox_Mssql_Password.Name = "textbox_Mssql_Password";
           this.textbox_Mssql_Password.PasswordChar = '*';
           this.textbox_Mssql_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Mssql_Password.TabIndex = 170;
           //
           // label3
           //
           this.label3.AutoSize = true;
           this.label3.BackColor = System.Drawing.Color.Transparent;
           this.label3.Location = new System.Drawing.Point(6, 151);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(56, 13);
           this.label3.TabIndex = 19;
           this.label3.Text = "&Password:";
           //
           // textbox_Mssql_LoginName
           //
           this.textbox_Mssql_LoginName.Location = new System.Drawing.Point(101, 122);
           this.textbox_Mssql_LoginName.Name = "textbox_Mssql_LoginName";
           this.textbox_Mssql_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Mssql_LoginName.TabIndex = 160;
           //
           // label2
           //
           this.label2.AutoSize = true;
           this.label2.BackColor = System.Drawing.Color.Transparent;
           this.label2.Location = new System.Drawing.Point(6, 125);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(65, 13);
           this.label2.TabIndex = 17;
           this.label2.Text = "&Login name:";
           //
           // radiobutton_Mssql_Untrusted
           //
           this.radiobutton_Mssql_Untrusted.AutoSize = true;
           this.radiobutton_Mssql_Untrusted.BackColor = System.Drawing.Color.Transparent;
           this.radiobutton_Mssql_Untrusted.Location = new System.Drawing.Point(253, 99);
           this.radiobutton_Mssql_Untrusted.Name = "radiobutton_Mssql_Untrusted";
           this.radiobutton_Mssql_Untrusted.Size = new System.Drawing.Size(151, 17);
           this.radiobutton_Mssql_Untrusted.TabIndex = 152;
           this.radiobutton_Mssql_Untrusted.Text = "S&QL Server Authentication";
           this.radiobutton_Mssql_Untrusted.UseVisualStyleBackColor = false;
           //
           // radiobutton_Mssql_Trusted
           //
           this.radiobutton_Mssql_Trusted.AutoSize = true;
           this.radiobutton_Mssql_Trusted.BackColor = System.Drawing.Color.Transparent;
           this.radiobutton_Mssql_Trusted.Checked = true;
           this.radiobutton_Mssql_Trusted.Location = new System.Drawing.Point(106, 99);
           this.radiobutton_Mssql_Trusted.Name = "radiobutton_Mssql_Trusted";
           this.radiobutton_Mssql_Trusted.Size = new System.Drawing.Size(140, 17);
           this.radiobutton_Mssql_Trusted.TabIndex = 150;
           this.radiobutton_Mssql_Trusted.TabStop = true;
           this.radiobutton_Mssql_Trusted.Text = "&Windows Authentication";
           this.radiobutton_Mssql_Trusted.UseVisualStyleBackColor = false;
           //
           // textbox_Mssql_ServerAddress
           //
           this.textbox_Mssql_ServerAddress.BackColor = System.Drawing.SystemColors.Window;
           this.textbox_Mssql_ServerAddress.Location = new System.Drawing.Point(101, 15);
           this.textbox_Mssql_ServerAddress.Name = "textbox_Mssql_ServerAddress";
           this.textbox_Mssql_ServerAddress.Size = new System.Drawing.Size(333, 20);
           this.textbox_Mssql_ServerAddress.TabIndex = 120;
           //
           // label18
           //
           this.label18.AutoSize = true;
           this.label18.BackColor = System.Drawing.Color.Transparent;
           this.label18.ForeColor = System.Drawing.Color.Black;
           this.label18.Location = new System.Drawing.Point(6, 45);
           this.label18.Name = "label18";
           this.label18.Size = new System.Drawing.Size(87, 13);
           this.label18.TabIndex = 4;
           this.label18.Text = "&Database Name:";
           //
           // combobox_Mssql_DatabaseName
           //
           this.combobox_Mssql_DatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Mssql_DatabaseName.FormattingEnabled = true;
           this.combobox_Mssql_DatabaseName.Location = new System.Drawing.Point(101, 42);
           this.combobox_Mssql_DatabaseName.Name = "combobox_Mssql_DatabaseName";
           this.combobox_Mssql_DatabaseName.Size = new System.Drawing.Size(333, 21);
           this.combobox_Mssql_DatabaseName.TabIndex = 140;
           this.combobox_Mssql_DatabaseName.DropDown += new System.EventHandler(this.combobox_DatabaseName_DropDown);
           //
           // label1
           //
           this.label1.AutoSize = true;
           this.label1.BackColor = System.Drawing.Color.Transparent;
           this.label1.ForeColor = System.Drawing.Color.Black;
           this.label1.Location = new System.Drawing.Point(6, 18);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(82, 13);
           this.label1.TabIndex = 0;
           this.label1.Text = "&Server Address:";
           //
           // tabpage_Mysql
           //
           this.tabpage_Mysql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Mysql.Controls.Add(this.checkbox_Mysql_IntegratedSecurity);
           this.tabpage_Mysql.Controls.Add(this.combobox_Mysql_DatabaseName);
           this.tabpage_Mysql.Controls.Add(this.checkbox_Mysql_SavePassword);
           this.tabpage_Mysql.Controls.Add(this.label_Mysql_CapslockIsOn);
           this.tabpage_Mysql.Controls.Add(this.textbox_Mysql_Password);
           this.tabpage_Mysql.Controls.Add(this.label_Mysql_Password);
           this.tabpage_Mysql.Controls.Add(this.textbox_Mysql_LoginName);
           this.tabpage_Mysql.Controls.Add(this.label_Mysql_LoginName);
           this.tabpage_Mysql.Controls.Add(this.textbox_Mysql_ServerAddress);
           this.tabpage_Mysql.Controls.Add(this.label19);
           this.tabpage_Mysql.Controls.Add(this.label_tabMysql_Server);
           this.tabpage_Mysql.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Mysql.Name = "tabpage_Mysql";
           this.tabpage_Mysql.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Mysql.TabIndex = 5;
           this.tabpage_Mysql.Text = "MySQL";
           this.tabpage_Mysql.UseVisualStyleBackColor = true;
           //
           // checkbox_Mysql_IntegratedSecurity
           //
           this.checkbox_Mysql_IntegratedSecurity.AutoSize = true;
           this.checkbox_Mysql_IntegratedSecurity.BackColor = System.Drawing.Color.Transparent;
           this.checkbox_Mysql_IntegratedSecurity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.checkbox_Mysql_IntegratedSecurity.ForeColor = System.Drawing.Color.LightGray;
           this.checkbox_Mysql_IntegratedSecurity.Location = new System.Drawing.Point(360, 124);
           this.checkbox_Mysql_IntegratedSecurity.Name = "checkbox_Mysql_IntegratedSecurity";
           this.checkbox_Mysql_IntegratedSecurity.Size = new System.Drawing.Size(115, 17);
           this.checkbox_Mysql_IntegratedSecurity.TabIndex = 162;
           this.checkbox_Mysql_IntegratedSecurity.Text = "Integrated Security";
           this.checkbox_Mysql_IntegratedSecurity.UseVisualStyleBackColor = false;
           this.checkbox_Mysql_IntegratedSecurity.Visible = false;
           this.checkbox_Mysql_IntegratedSecurity.CheckedChanged += new System.EventHandler(this.checkbox_Mysql_IntegratedSecurity_CheckedChanged);
           //
           // combobox_Mysql_DatabaseName
           //
           this.combobox_Mysql_DatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Mysql_DatabaseName.FormattingEnabled = true;
           this.combobox_Mysql_DatabaseName.Location = new System.Drawing.Point(101, 42);
           this.combobox_Mysql_DatabaseName.Name = "combobox_Mysql_DatabaseName";
           this.combobox_Mysql_DatabaseName.Size = new System.Drawing.Size(333, 21);
           this.combobox_Mysql_DatabaseName.TabIndex = 130;
           this.combobox_Mysql_DatabaseName.DropDown += new System.EventHandler(this.combobox_DatabaseName_DropDown);
           //
           // checkbox_Mysql_SavePassword
           //
           this.checkbox_Mysql_SavePassword.AutoSize = true;
           this.checkbox_Mysql_SavePassword.BackColor = System.Drawing.Color.Transparent;
           this.checkbox_Mysql_SavePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.checkbox_Mysql_SavePassword.ForeColor = System.Drawing.Color.Silver;
           this.checkbox_Mysql_SavePassword.Location = new System.Drawing.Point(360, 151);
           this.checkbox_Mysql_SavePassword.Name = "checkbox_Mysql_SavePassword";
           this.checkbox_Mysql_SavePassword.Size = new System.Drawing.Size(99, 17);
           this.checkbox_Mysql_SavePassword.TabIndex = 160;
           this.checkbox_Mysql_SavePassword.Text = "Save password";
           this.checkbox_Mysql_SavePassword.UseVisualStyleBackColor = false;
           this.checkbox_Mysql_SavePassword.Visible = false;
           //
           // label_Mysql_CapslockIsOn
           //
           this.label_Mysql_CapslockIsOn.AutoSize = true;
           this.label_Mysql_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Mysql_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Mysql_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Mysql_CapslockIsOn.Location = new System.Drawing.Point(240, 148);
           this.label_Mysql_CapslockIsOn.Name = "label_Mysql_CapslockIsOn";
           this.label_Mysql_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Mysql_CapslockIsOn.TabIndex = 26;
           this.label_Mysql_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Mysql_CapslockIsOn.Visible = false;
           //
           // textbox_Mysql_Password
           //
           this.textbox_Mysql_Password.Location = new System.Drawing.Point(101, 148);
           this.textbox_Mysql_Password.Name = "textbox_Mysql_Password";
           this.textbox_Mysql_Password.PasswordChar = '*';
           this.textbox_Mysql_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Mysql_Password.TabIndex = 150;
           //
           // label_Mysql_Password
           //
           this.label_Mysql_Password.AutoSize = true;
           this.label_Mysql_Password.BackColor = System.Drawing.Color.Transparent;
           this.label_Mysql_Password.Location = new System.Drawing.Point(6, 151);
           this.label_Mysql_Password.Name = "label_Mysql_Password";
           this.label_Mysql_Password.Size = new System.Drawing.Size(56, 13);
           this.label_Mysql_Password.TabIndex = 24;
           this.label_Mysql_Password.Text = "&Password:";
           //
           // textbox_Mysql_LoginName
           //
           this.textbox_Mysql_LoginName.Location = new System.Drawing.Point(101, 122);
           this.textbox_Mysql_LoginName.Name = "textbox_Mysql_LoginName";
           this.textbox_Mysql_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Mysql_LoginName.TabIndex = 140;
           //
           // label_Mysql_LoginName
           //
           this.label_Mysql_LoginName.AutoSize = true;
           this.label_Mysql_LoginName.BackColor = System.Drawing.Color.Transparent;
           this.label_Mysql_LoginName.Location = new System.Drawing.Point(6, 125);
           this.label_Mysql_LoginName.Name = "label_Mysql_LoginName";
           this.label_Mysql_LoginName.Size = new System.Drawing.Size(65, 13);
           this.label_Mysql_LoginName.TabIndex = 22;
           this.label_Mysql_LoginName.Text = "&Login name:";
           //
           // textbox_Mysql_ServerAddress
           //
           this.textbox_Mysql_ServerAddress.Location = new System.Drawing.Point(101, 15);
           this.textbox_Mysql_ServerAddress.Name = "textbox_Mysql_ServerAddress";
           this.textbox_Mysql_ServerAddress.Size = new System.Drawing.Size(333, 20);
           this.textbox_Mysql_ServerAddress.TabIndex = 120;
           //
           // label19
           //
           this.label19.AutoSize = true;
           this.label19.BackColor = System.Drawing.Color.Transparent;
           this.label19.ForeColor = System.Drawing.Color.Black;
           this.label19.Location = new System.Drawing.Point(6, 45);
           this.label19.Name = "label19";
           this.label19.Size = new System.Drawing.Size(87, 13);
           this.label19.TabIndex = 7;
           this.label19.Text = "&Database Name:";
           //
           // label_tabMysql_Server
           //
           this.label_tabMysql_Server.AutoSize = true;
           this.label_tabMysql_Server.BackColor = System.Drawing.Color.Transparent;
           this.label_tabMysql_Server.ForeColor = System.Drawing.Color.Black;
           this.label_tabMysql_Server.Location = new System.Drawing.Point(6, 18);
           this.label_tabMysql_Server.Name = "label_tabMysql_Server";
           this.label_tabMysql_Server.Size = new System.Drawing.Size(82, 13);
           this.label_tabMysql_Server.TabIndex = 3;
           this.label_tabMysql_Server.Text = "&Server Address:";
           //
           // tabpage_Odbc
           //
           this.tabpage_Odbc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Odbc.Controls.Add(this.textBox5);
           this.tabpage_Odbc.Controls.Add(this.label11);
           this.tabpage_Odbc.Controls.Add(this.button_Odbc_Save);
           this.tabpage_Odbc.Controls.Add(this.button_Odbc_Load);
           this.tabpage_Odbc.Controls.Add(this.label4);
           this.tabpage_Odbc.Controls.Add(this.textbox_Odbc_ConnectionString);
           this.tabpage_Odbc.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Odbc.Name = "tabpage_Odbc";
           this.tabpage_Odbc.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Odbc.TabIndex = 1;
           this.tabpage_Odbc.Text = "ODBC";
           this.tabpage_Odbc.UseVisualStyleBackColor = true;
           //
           // textBox5
           //
           this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textBox5.BackColor = System.Drawing.Color.FloralWhite;
           this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
           this.textBox5.ForeColor = System.Drawing.Color.Goldenrod;
           this.textBox5.Location = new System.Drawing.Point(101, 139);
           this.textBox5.Multiline = true;
           this.textBox5.Name = "textBox5";
           this.textBox5.Size = new System.Drawing.Size(333, 47);
           this.textBox5.TabIndex = 143;
           this.textBox5.TabStop = false;
           this.textBox5.Text = "DSN=Joesgarage";
           //
           // label11
           //
           this.label11.AutoSize = true;
           this.label11.BackColor = System.Drawing.Color.Transparent;
           this.label11.ForeColor = System.Drawing.Color.Goldenrod;
           this.label11.Location = new System.Drawing.Point(6, 140);
           this.label11.Name = "label11";
           this.label11.Size = new System.Drawing.Size(50, 13);
           this.label11.TabIndex = 141;
           this.label11.Text = "Example:";
           //
           // button_Odbc_Save
           //
           this.button_Odbc_Save.Location = new System.Drawing.Point(369, 84);
           this.button_Odbc_Save.Name = "button_Odbc_Save";
           this.button_Odbc_Save.Size = new System.Drawing.Size(64, 23);
           this.button_Odbc_Save.TabIndex = 140;
           this.button_Odbc_Save.Text = "Save";
           this.button_Odbc_Save.UseVisualStyleBackColor = true;
           this.button_Odbc_Save.Click += new System.EventHandler(this.cmdSaveOdbc_Click);
           //
           // button_Odbc_Load
           //
           this.button_Odbc_Load.Location = new System.Drawing.Point(292, 84);
           this.button_Odbc_Load.Name = "button_Odbc_Load";
           this.button_Odbc_Load.Size = new System.Drawing.Size(64, 23);
           this.button_Odbc_Load.TabIndex = 130;
           this.button_Odbc_Load.Text = "Load";
           this.button_Odbc_Load.UseVisualStyleBackColor = true;
           this.button_Odbc_Load.Click += new System.EventHandler(this.cmdLoadOdbc_Click);
           //
           // label4
           //
           this.label4.AutoSize = true;
           this.label4.BackColor = System.Drawing.Color.Transparent;
           this.label4.Location = new System.Drawing.Point(6, 18);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(94, 13);
           this.label4.TabIndex = 1;
           this.label4.Text = "Connection String:";
           //
           // textbox_Odbc_ConnectionString
           //
           this.textbox_Odbc_ConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textbox_Odbc_ConnectionString.Location = new System.Drawing.Point(101, 15);
           this.textbox_Odbc_ConnectionString.Multiline = true;
           this.textbox_Odbc_ConnectionString.Name = "textbox_Odbc_ConnectionString";
           this.textbox_Odbc_ConnectionString.Size = new System.Drawing.Size(333, 64);
           this.textbox_Odbc_ConnectionString.TabIndex = 120;
           //
           // tabpage_Oledb
           //
           this.tabpage_Oledb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Oledb.Controls.Add(this.textBox6);
           this.tabpage_Oledb.Controls.Add(this.label12);
           this.tabpage_Oledb.Controls.Add(this.button_Oledb_Save);
           this.tabpage_Oledb.Controls.Add(this.button_Oledb_Load);
           this.tabpage_Oledb.Controls.Add(this.label10);
           this.tabpage_Oledb.Controls.Add(this.textbox_Oledb_ConnectionString);
           this.tabpage_Oledb.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Oledb.Name = "tabpage_Oledb";
           this.tabpage_Oledb.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Oledb.TabIndex = 3;
           this.tabpage_Oledb.Text = "OleDb";
           this.tabpage_Oledb.UseVisualStyleBackColor = true;
           //
           // textBox6
           //
           this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textBox6.BackColor = System.Drawing.Color.FloralWhite;
           this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
           this.textBox6.ForeColor = System.Drawing.Color.Goldenrod;
           this.textBox6.Location = new System.Drawing.Point(101, 139);
           this.textBox6.Multiline = true;
           this.textBox6.Name = "textBox6";
           this.textBox6.Size = new System.Drawing.Size(333, 54);
           this.textBox6.TabIndex = 145;
           this.textBox6.TabStop = false;
           this.textBox6.Text = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Paradox 7.x;Data Source=C:\\N" +
               "etDir\\Joesgarage\\Firmendaten";
           //
           // label12
           //
           this.label12.AutoSize = true;
           this.label12.BackColor = System.Drawing.Color.Transparent;
           this.label12.ForeColor = System.Drawing.Color.Goldenrod;
           this.label12.Location = new System.Drawing.Point(6, 140);
           this.label12.Name = "label12";
           this.label12.Size = new System.Drawing.Size(50, 13);
           this.label12.TabIndex = 144;
           this.label12.Text = "Example:";
           //
           // button_Oledb_Save
           //
           this.button_Oledb_Save.Location = new System.Drawing.Point(369, 84);
           this.button_Oledb_Save.Name = "button_Oledb_Save";
           this.button_Oledb_Save.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
           this.button_Oledb_Save.Size = new System.Drawing.Size(64, 23);
           this.button_Oledb_Save.TabIndex = 140;
           this.button_Oledb_Save.Text = "Save";
           this.button_Oledb_Save.UseVisualStyleBackColor = true;
           this.button_Oledb_Save.Click += new System.EventHandler(this.cmdSaveOleDb_Click);
           //
           // button_Oledb_Load
           //
           this.button_Oledb_Load.Location = new System.Drawing.Point(292, 84);
           this.button_Oledb_Load.Name = "button_Oledb_Load";
           this.button_Oledb_Load.Size = new System.Drawing.Size(64, 23);
           this.button_Oledb_Load.TabIndex = 130;
           this.button_Oledb_Load.Text = "Load";
           this.button_Oledb_Load.UseVisualStyleBackColor = true;
           this.button_Oledb_Load.Click += new System.EventHandler(this.cmdLoadOleDb_Click);
           //
           // label10
           //
           this.label10.AutoSize = true;
           this.label10.BackColor = System.Drawing.Color.Transparent;
           this.label10.Location = new System.Drawing.Point(6, 18);
           this.label10.Name = "label10";
           this.label10.Size = new System.Drawing.Size(94, 13);
           this.label10.TabIndex = 5;
           this.label10.Text = "Connection String:";
           //
           // textbox_Oledb_ConnectionString
           //
           this.textbox_Oledb_ConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.textbox_Oledb_ConnectionString.Location = new System.Drawing.Point(101, 15);
           this.textbox_Oledb_ConnectionString.Multiline = true;
           this.textbox_Oledb_ConnectionString.Name = "textbox_Oledb_ConnectionString";
           this.textbox_Oledb_ConnectionString.Size = new System.Drawing.Size(333, 64);
           this.textbox_Oledb_ConnectionString.TabIndex = 120;
           //
           // tabpage_Oracle
           //
           this.tabpage_Oracle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Oracle.Controls.Add(this.combobox_Oracle_DatabaseName);
           this.tabpage_Oracle.Controls.Add(this.label37);
           this.tabpage_Oracle.Controls.Add(this.label36);
           this.tabpage_Oracle.Controls.Add(this.label_Oracle_CapslockIsOn);
           this.tabpage_Oracle.Controls.Add(this.textbox_Oracle_Password);
           this.tabpage_Oracle.Controls.Add(this.label7);
           this.tabpage_Oracle.Controls.Add(this.textbox_Oracle_LoginName);
           this.tabpage_Oracle.Controls.Add(this.label8);
           this.tabpage_Oracle.Controls.Add(this.radiobutton_Oracle_Untrusted);
           this.tabpage_Oracle.Controls.Add(this.radiobutton_Oracle_Trusted);
           this.tabpage_Oracle.Controls.Add(this.textbox_Oracle_DataSource);
           this.tabpage_Oracle.Controls.Add(this.label9);
           this.tabpage_Oracle.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Oracle.Name = "tabpage_Oracle";
           this.tabpage_Oracle.Padding = new System.Windows.Forms.Padding(3);
           this.tabpage_Oracle.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Oracle.TabIndex = 2;
           this.tabpage_Oracle.Text = "Oracle";
           this.tabpage_Oracle.UseVisualStyleBackColor = true;
           //
           // combobox_Oracle_DatabaseName
           //
           this.combobox_Oracle_DatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Oracle_DatabaseName.FormattingEnabled = true;
           this.combobox_Oracle_DatabaseName.Location = new System.Drawing.Point(101, 42);
           this.combobox_Oracle_DatabaseName.Name = "combobox_Oracle_DatabaseName";
           this.combobox_Oracle_DatabaseName.Size = new System.Drawing.Size(333, 21);
           this.combobox_Oracle_DatabaseName.TabIndex = 135;
           this.combobox_Oracle_DatabaseName.DropDown += new System.EventHandler(this.combobox_DatabaseName_DropDown);
           //
           // label37
           //
           this.label37.AutoSize = true;
           this.label37.BackColor = System.Drawing.Color.Transparent;
           this.label37.ForeColor = System.Drawing.Color.Black;
           this.label37.Location = new System.Drawing.Point(6, 45);
           this.label37.Name = "label37";
           this.label37.Size = new System.Drawing.Size(87, 13);
           this.label37.TabIndex = 26;
           this.label37.Text = "&Database Name:";
           //
           // label36
           //
           this.label36.AutoSize = true;
           this.label36.BackColor = System.Drawing.Color.Transparent;
           this.label36.Location = new System.Drawing.Point(6, 97);
           this.label36.Name = "label36";
           this.label36.Size = new System.Drawing.Size(37, 13);
           this.label36.TabIndex = 24;
           this.label36.Text = "&Using:";
           //
           // label_Oracle_CapslockIsOn
           //
           this.label_Oracle_CapslockIsOn.AutoSize = true;
           this.label_Oracle_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Oracle_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Oracle_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Oracle_CapslockIsOn.Location = new System.Drawing.Point(240, 148);
           this.label_Oracle_CapslockIsOn.Name = "label_Oracle_CapslockIsOn";
           this.label_Oracle_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Oracle_CapslockIsOn.TabIndex = 23;
           this.label_Oracle_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Oracle_CapslockIsOn.Visible = false;
           //
           // textbox_Oracle_Password
           //
           this.textbox_Oracle_Password.Location = new System.Drawing.Point(101, 148);
           this.textbox_Oracle_Password.Name = "textbox_Oracle_Password";
           this.textbox_Oracle_Password.PasswordChar = '*';
           this.textbox_Oracle_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Oracle_Password.TabIndex = 160;
           //
           // label7
           //
           this.label7.AutoSize = true;
           this.label7.BackColor = System.Drawing.Color.Transparent;
           this.label7.Location = new System.Drawing.Point(6, 151);
           this.label7.Name = "label7";
           this.label7.Size = new System.Drawing.Size(56, 13);
           this.label7.TabIndex = 21;
           this.label7.Text = "&Password:";
           //
           // textbox_Oracle_LoginName
           //
           this.textbox_Oracle_LoginName.Location = new System.Drawing.Point(101, 122);
           this.textbox_Oracle_LoginName.Name = "textbox_Oracle_LoginName";
           this.textbox_Oracle_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Oracle_LoginName.TabIndex = 150;
           //
           // label8
           //
           this.label8.AutoSize = true;
           this.label8.BackColor = System.Drawing.Color.Transparent;
           this.label8.Location = new System.Drawing.Point(6, 125);
           this.label8.Name = "label8";
           this.label8.Size = new System.Drawing.Size(65, 13);
           this.label8.TabIndex = 19;
           this.label8.Text = "&Login name:";
           //
           // radiobutton_Oracle_Untrusted
           //
           this.radiobutton_Oracle_Untrusted.AutoSize = true;
           this.radiobutton_Oracle_Untrusted.BackColor = System.Drawing.Color.Transparent;
           this.radiobutton_Oracle_Untrusted.Location = new System.Drawing.Point(222, 95);
           this.radiobutton_Oracle_Untrusted.Name = "radiobutton_Oracle_Untrusted";
           this.radiobutton_Oracle_Untrusted.Size = new System.Drawing.Size(127, 17);
           this.radiobutton_Oracle_Untrusted.TabIndex = 142;
           this.radiobutton_Oracle_Untrusted.Text = "Oracle Authentication";
           this.radiobutton_Oracle_Untrusted.UseVisualStyleBackColor = false;
           //
           // radiobutton_Oracle_Trusted
           //
           this.radiobutton_Oracle_Trusted.AutoSize = true;
           this.radiobutton_Oracle_Trusted.BackColor = System.Drawing.Color.Transparent;
           this.radiobutton_Oracle_Trusted.Location = new System.Drawing.Point(105, 95);
           this.radiobutton_Oracle_Trusted.Name = "radiobutton_Oracle_Trusted";
           this.radiobutton_Oracle_Trusted.Size = new System.Drawing.Size(114, 17);
           this.radiobutton_Oracle_Trusted.TabIndex = 140;
           this.radiobutton_Oracle_Trusted.Text = "&Integrated Security";
           this.radiobutton_Oracle_Trusted.UseVisualStyleBackColor = false;
           //
           // textbox_Oracle_DataSource
           //
           this.textbox_Oracle_DataSource.BackColor = System.Drawing.Color.FloralWhite;
           this.textbox_Oracle_DataSource.ForeColor = System.Drawing.Color.LightGray;
           this.textbox_Oracle_DataSource.Location = new System.Drawing.Point(101, 15);
           this.textbox_Oracle_DataSource.Name = "textbox_Oracle_DataSource";
           this.textbox_Oracle_DataSource.Size = new System.Drawing.Size(333, 20);
           this.textbox_Oracle_DataSource.TabIndex = 120;
           this.textbox_Oracle_DataSource.TabStop = false;
           //
           // label9
           //
           this.label9.AutoSize = true;
           this.label9.BackColor = System.Drawing.Color.Transparent;
           this.label9.ForeColor = System.Drawing.Color.LightGray;
           this.label9.Location = new System.Drawing.Point(6, 18);
           this.label9.Name = "label9";
           this.label9.Size = new System.Drawing.Size(70, 13);
           this.label9.TabIndex = 3;
           this.label9.Text = "&Data Source:";
           //
           // tabpage_Pgsql
           //
           this.tabpage_Pgsql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Pgsql.Controls.Add(this.label_Pgsql_CapslockIsOn);
           this.tabpage_Pgsql.Controls.Add(this.textbox_Pgsql_Password);
           this.tabpage_Pgsql.Controls.Add(this.label15);
           this.tabpage_Pgsql.Controls.Add(this.textbox_Pgsql_LoginName);
           this.tabpage_Pgsql.Controls.Add(this.label16);
           this.tabpage_Pgsql.Controls.Add(this.textbox_Pgsql_ServerAddress);
           this.tabpage_Pgsql.Controls.Add(this.label20);
           this.tabpage_Pgsql.Controls.Add(this.combobox_Pgsql_DatabaseName);
           this.tabpage_Pgsql.Controls.Add(this.label17);
           this.tabpage_Pgsql.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Pgsql.Name = "tabpage_Pgsql";
           this.tabpage_Pgsql.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Pgsql.TabIndex = 6;
           this.tabpage_Pgsql.Text = "PostgreSQL";
           this.tabpage_Pgsql.UseVisualStyleBackColor = true;
           //
           // label_Pgsql_CapslockIsOn
           //
           this.label_Pgsql_CapslockIsOn.AutoSize = true;
           this.label_Pgsql_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Pgsql_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Pgsql_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Pgsql_CapslockIsOn.Location = new System.Drawing.Point(240, 148);
           this.label_Pgsql_CapslockIsOn.Name = "label_Pgsql_CapslockIsOn";
           this.label_Pgsql_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Pgsql_CapslockIsOn.TabIndex = 27;
           this.label_Pgsql_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Pgsql_CapslockIsOn.Visible = false;
           //
           // textbox_Pgsql_Password
           //
           this.textbox_Pgsql_Password.Location = new System.Drawing.Point(101, 148);
           this.textbox_Pgsql_Password.Name = "textbox_Pgsql_Password";
           this.textbox_Pgsql_Password.PasswordChar = '*';
           this.textbox_Pgsql_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Pgsql_Password.TabIndex = 160;
           //
           // label15
           //
           this.label15.AutoSize = true;
           this.label15.BackColor = System.Drawing.Color.Transparent;
           this.label15.Location = new System.Drawing.Point(6, 151);
           this.label15.Name = "label15";
           this.label15.Size = new System.Drawing.Size(56, 13);
           this.label15.TabIndex = 25;
           this.label15.Text = "&Password:";
           //
           // textbox_Pgsql_LoginName
           //
           this.textbox_Pgsql_LoginName.Location = new System.Drawing.Point(101, 122);
           this.textbox_Pgsql_LoginName.Name = "textbox_Pgsql_LoginName";
           this.textbox_Pgsql_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Pgsql_LoginName.TabIndex = 150;
           //
           // label16
           //
           this.label16.AutoSize = true;
           this.label16.BackColor = System.Drawing.Color.Transparent;
           this.label16.Location = new System.Drawing.Point(6, 125);
           this.label16.Name = "label16";
           this.label16.Size = new System.Drawing.Size(65, 13);
           this.label16.TabIndex = 23;
           this.label16.Text = "&Login name:";
           //
           // textbox_Pgsql_ServerAddress
           //
           this.textbox_Pgsql_ServerAddress.Location = new System.Drawing.Point(101, 15);
           this.textbox_Pgsql_ServerAddress.Name = "textbox_Pgsql_ServerAddress";
           this.textbox_Pgsql_ServerAddress.Size = new System.Drawing.Size(333, 20);
           this.textbox_Pgsql_ServerAddress.TabIndex = 120;
           //
           // label20
           //
           this.label20.AutoSize = true;
           this.label20.BackColor = System.Drawing.Color.Transparent;
           this.label20.ForeColor = System.Drawing.Color.Black;
           this.label20.Location = new System.Drawing.Point(6, 45);
           this.label20.Name = "label20";
           this.label20.Size = new System.Drawing.Size(87, 13);
           this.label20.TabIndex = 10;
           this.label20.Text = "&Database Name:";
           //
           // combobox_Pgsql_DatabaseName
           //
           this.combobox_Pgsql_DatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Pgsql_DatabaseName.FormattingEnabled = true;
           this.combobox_Pgsql_DatabaseName.Location = new System.Drawing.Point(101, 42);
           this.combobox_Pgsql_DatabaseName.Name = "combobox_Pgsql_DatabaseName";
           this.combobox_Pgsql_DatabaseName.Size = new System.Drawing.Size(333, 21);
           this.combobox_Pgsql_DatabaseName.TabIndex = 140;
           this.combobox_Pgsql_DatabaseName.DropDown += new System.EventHandler(this.combobox_DatabaseName_DropDown);
           //
           // label17
           //
           this.label17.AutoSize = true;
           this.label17.BackColor = System.Drawing.Color.Transparent;
           this.label17.ForeColor = System.Drawing.Color.Black;
           this.label17.Location = new System.Drawing.Point(6, 18);
           this.label17.Name = "label17";
           this.label17.Size = new System.Drawing.Size(82, 13);
           this.label17.TabIndex = 6;
           this.label17.Text = "&Server Address:";
           //
           // tabpage_Sqlite
           //
           this.tabpage_Sqlite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Sqlite.Controls.Add(this.checkbox_Sqlite_SavePassword);
           this.tabpage_Sqlite.Controls.Add(this.label_Sqlite_CapslockIsOn);
           this.tabpage_Sqlite.Controls.Add(this.textbox_Sqlite_Password);
           this.tabpage_Sqlite.Controls.Add(this.label40);
           this.tabpage_Sqlite.Controls.Add(this.textbox_Sqlite_LoginName);
           this.tabpage_Sqlite.Controls.Add(this.label41);
           this.tabpage_Sqlite.Controls.Add(this.textbox_Sqlite_ServerAddress);
           this.tabpage_Sqlite.Controls.Add(this.label38);
           this.tabpage_Sqlite.Controls.Add(this.button_BrowseSqliteFile);
           this.tabpage_Sqlite.Controls.Add(this.textBox_SqliteFile);
           this.tabpage_Sqlite.Controls.Add(this.label_SqliteFile);
           this.tabpage_Sqlite.ForeColor = System.Drawing.Color.LightGray;
           this.tabpage_Sqlite.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Sqlite.Name = "tabpage_Sqlite";
           this.tabpage_Sqlite.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Sqlite.TabIndex = 4;
           this.tabpage_Sqlite.Text = "SQLite";
           this.tabpage_Sqlite.UseVisualStyleBackColor = true;
           //
           // checkbox_Sqlite_SavePassword
           //
           this.checkbox_Sqlite_SavePassword.AutoSize = true;
           this.checkbox_Sqlite_SavePassword.BackColor = System.Drawing.Color.Transparent;
           this.checkbox_Sqlite_SavePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.checkbox_Sqlite_SavePassword.ForeColor = System.Drawing.Color.LightGray;
           this.checkbox_Sqlite_SavePassword.Location = new System.Drawing.Point(360, 169);
           this.checkbox_Sqlite_SavePassword.Name = "checkbox_Sqlite_SavePassword";
           this.checkbox_Sqlite_SavePassword.Size = new System.Drawing.Size(99, 17);
           this.checkbox_Sqlite_SavePassword.TabIndex = 166;
           this.checkbox_Sqlite_SavePassword.Text = "Save password";
           this.checkbox_Sqlite_SavePassword.UseVisualStyleBackColor = false;
           this.checkbox_Sqlite_SavePassword.Visible = false;
           //
           // label_Sqlite_CapslockIsOn
           //
           this.label_Sqlite_CapslockIsOn.AutoSize = true;
           this.label_Sqlite_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Sqlite_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Sqlite_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Sqlite_CapslockIsOn.Location = new System.Drawing.Point(240, 166);
           this.label_Sqlite_CapslockIsOn.Name = "label_Sqlite_CapslockIsOn";
           this.label_Sqlite_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Sqlite_CapslockIsOn.TabIndex = 163;
           this.label_Sqlite_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Sqlite_CapslockIsOn.Visible = false;
           //
           // textbox_Sqlite_Password
           //
           this.textbox_Sqlite_Password.Location = new System.Drawing.Point(101, 166);
           this.textbox_Sqlite_Password.Name = "textbox_Sqlite_Password";
           this.textbox_Sqlite_Password.PasswordChar = '*';
           this.textbox_Sqlite_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Sqlite_Password.TabIndex = 165;
           //
           // label40
           //
           this.label40.AutoSize = true;
           this.label40.BackColor = System.Drawing.Color.Transparent;
           this.label40.Location = new System.Drawing.Point(6, 169);
           this.label40.Name = "label40";
           this.label40.Size = new System.Drawing.Size(56, 13);
           this.label40.TabIndex = 162;
           this.label40.Text = "&Password:";
           //
           // textbox_Sqlite_LoginName
           //
           this.textbox_Sqlite_LoginName.Location = new System.Drawing.Point(101, 140);
           this.textbox_Sqlite_LoginName.Name = "textbox_Sqlite_LoginName";
           this.textbox_Sqlite_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Sqlite_LoginName.TabIndex = 164;
           //
           // label41
           //
           this.label41.AutoSize = true;
           this.label41.BackColor = System.Drawing.Color.Transparent;
           this.label41.Location = new System.Drawing.Point(6, 143);
           this.label41.Name = "label41";
           this.label41.Size = new System.Drawing.Size(65, 13);
           this.label41.TabIndex = 161;
           this.label41.Text = "&Login name:";
           //
           // textbox_Sqlite_ServerAddress
           //
           this.textbox_Sqlite_ServerAddress.Location = new System.Drawing.Point(101, 15);
           this.textbox_Sqlite_ServerAddress.Multiline = true;
           this.textbox_Sqlite_ServerAddress.Name = "textbox_Sqlite_ServerAddress";
           this.textbox_Sqlite_ServerAddress.Size = new System.Drawing.Size(333, 56);
           this.textbox_Sqlite_ServerAddress.TabIndex = 132;
           //
           // label38
           //
           this.label38.AutoSize = true;
           this.label38.BackColor = System.Drawing.Color.Transparent;
           this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
           this.label38.Location = new System.Drawing.Point(6, 18);
           this.label38.Name = "label38";
           this.label38.Size = new System.Drawing.Size(88, 13);
           this.label38.TabIndex = 131;
           this.label38.Text = "Database F&older:";
           //
           // button_BrowseSqliteFile
           //
           this.button_BrowseSqliteFile.ForeColor = System.Drawing.SystemColors.ControlText;
           this.button_BrowseSqliteFile.Location = new System.Drawing.Point(437, 78);
           this.button_BrowseSqliteFile.Name = "button_BrowseSqliteFile";
           this.button_BrowseSqliteFile.Size = new System.Drawing.Size(64, 23);
           this.button_BrowseSqliteFile.TabIndex = 130;
           this.button_BrowseSqliteFile.Text = "Browse";
           this.button_BrowseSqliteFile.UseVisualStyleBackColor = true;
           this.button_BrowseSqliteFile.Click += new System.EventHandler(this.button_BrowseSqliteFile_Click);
           //
           // textBox_SqliteFile
           //
           this.textBox_SqliteFile.Location = new System.Drawing.Point(101, 78);
           this.textBox_SqliteFile.Multiline = true;
           this.textBox_SqliteFile.Name = "textBox_SqliteFile";
           this.textBox_SqliteFile.Size = new System.Drawing.Size(333, 56);
           this.textBox_SqliteFile.TabIndex = 120;
           //
           // label_SqliteFile
           //
           this.label_SqliteFile.AutoSize = true;
           this.label_SqliteFile.BackColor = System.Drawing.Color.Transparent;
           this.label_SqliteFile.ForeColor = System.Drawing.SystemColors.ControlText;
           this.label_SqliteFile.Location = new System.Drawing.Point(6, 80);
           this.label_SqliteFile.Name = "label_SqliteFile";
           this.label_SqliteFile.Size = new System.Drawing.Size(75, 13);
           this.label_SqliteFile.TabIndex = 4;
           this.label_SqliteFile.Text = "Database &File:";
           //
           // tabpage_Couch
           //
           this.tabpage_Couch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.tabpage_Couch.Controls.Add(this.label25);
           this.tabpage_Couch.Controls.Add(this.textbox_Couch_Password);
           this.tabpage_Couch.Controls.Add(this.label_Couch_CapslockIsOn);
           this.tabpage_Couch.Controls.Add(this.label22);
           this.tabpage_Couch.Controls.Add(this.textbox_Couch_LoginName);
           this.tabpage_Couch.Controls.Add(this.label23);
           this.tabpage_Couch.Controls.Add(this.textbox_Couch_ServerAddress);
           this.tabpage_Couch.Controls.Add(this.label21);
           this.tabpage_Couch.Controls.Add(this.combobox_Couch_DatabaseName);
           this.tabpage_Couch.Controls.Add(this.label24);
           this.tabpage_Couch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.tabpage_Couch.ForeColor = System.Drawing.Color.LightGray;
           this.tabpage_Couch.Location = new System.Drawing.Point(4, 19);
           this.tabpage_Couch.Name = "tabpage_Couch";
           this.tabpage_Couch.Size = new System.Drawing.Size(508, 201);
           this.tabpage_Couch.TabIndex = 7;
           this.tabpage_Couch.Text = "CouchDb";
           this.tabpage_Couch.UseVisualStyleBackColor = true;
           //
           // label25
           //
           this.label25.AutoSize = true;
           this.label25.BackColor = System.Drawing.Color.Transparent;
           this.label25.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label25.ForeColor = System.Drawing.Color.Tomato;
           this.label25.Location = new System.Drawing.Point(268, 76);
           this.label25.Name = "label25";
           this.label25.Size = new System.Drawing.Size(216, 64);
           this.label25.TabIndex = 161;
           this.label25.Text = "For CouchDB, the Database Name\r\nlisting shall work. But if you connect,\r\nthen vie" +
               "wing documents and issuing\r\ncommands will not yet work.";
           this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
           //
           // textbox_Couch_Password
           //
           this.textbox_Couch_Password.Location = new System.Drawing.Point(101, 148);
           this.textbox_Couch_Password.Name = "textbox_Couch_Password";
           this.textbox_Couch_Password.PasswordChar = '*';
           this.textbox_Couch_Password.Size = new System.Drawing.Size(133, 20);
           this.textbox_Couch_Password.TabIndex = 160;
           //
           // label_Couch_CapslockIsOn
           //
           this.label_Couch_CapslockIsOn.AutoSize = true;
           this.label_Couch_CapslockIsOn.BackColor = System.Drawing.Color.Transparent;
           this.label_Couch_CapslockIsOn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label_Couch_CapslockIsOn.ForeColor = System.Drawing.Color.Red;
           this.label_Couch_CapslockIsOn.Location = new System.Drawing.Point(240, 148);
           this.label_Couch_CapslockIsOn.Name = "label_Couch_CapslockIsOn";
           this.label_Couch_CapslockIsOn.Size = new System.Drawing.Size(119, 19);
           this.label_Couch_CapslockIsOn.TabIndex = 13;
           this.label_Couch_CapslockIsOn.Text = "CapsLock is ON";
           this.label_Couch_CapslockIsOn.Visible = false;
           //
           // label22
           //
           this.label22.AutoSize = true;
           this.label22.BackColor = System.Drawing.Color.Transparent;
           this.label22.Location = new System.Drawing.Point(6, 151);
           this.label22.Name = "label22";
           this.label22.Size = new System.Drawing.Size(56, 13);
           this.label22.TabIndex = 4;
           this.label22.Text = "&Password:";
           //
           // textbox_Couch_LoginName
           //
           this.textbox_Couch_LoginName.Location = new System.Drawing.Point(101, 122);
           this.textbox_Couch_LoginName.Name = "textbox_Couch_LoginName";
           this.textbox_Couch_LoginName.Size = new System.Drawing.Size(133, 20);
           this.textbox_Couch_LoginName.TabIndex = 150;
           //
           // label23
           //
           this.label23.AutoSize = true;
           this.label23.BackColor = System.Drawing.Color.Transparent;
           this.label23.Location = new System.Drawing.Point(6, 125);
           this.label23.Name = "label23";
           this.label23.Size = new System.Drawing.Size(65, 13);
           this.label23.TabIndex = 2;
           this.label23.Text = "&Login name:";
           //
           // textbox_Couch_ServerAddress
           //
           this.textbox_Couch_ServerAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.textbox_Couch_ServerAddress.Location = new System.Drawing.Point(101, 15);
           this.textbox_Couch_ServerAddress.Name = "textbox_Couch_ServerAddress";
           this.textbox_Couch_ServerAddress.Size = new System.Drawing.Size(333, 20);
           this.textbox_Couch_ServerAddress.TabIndex = 120;
           this.textbox_Couch_ServerAddress.Text = "localhost";
           //
           // label21
           //
           this.label21.AutoSize = true;
           this.label21.BackColor = System.Drawing.Color.Transparent;
           this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label21.ForeColor = System.Drawing.Color.Black;
           this.label21.Location = new System.Drawing.Point(6, 45);
           this.label21.Name = "label21";
           this.label21.Size = new System.Drawing.Size(87, 13);
           this.label21.TabIndex = 15;
           this.label21.Text = "&Database Name:";
           //
           // combobox_Couch_DatabaseName
           //
           this.combobox_Couch_DatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.combobox_Couch_DatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.combobox_Couch_DatabaseName.FormattingEnabled = true;
           this.combobox_Couch_DatabaseName.Location = new System.Drawing.Point(101, 42);
           this.combobox_Couch_DatabaseName.Name = "combobox_Couch_DatabaseName";
           this.combobox_Couch_DatabaseName.Size = new System.Drawing.Size(333, 21);
           this.combobox_Couch_DatabaseName.TabIndex = 140;
           this.combobox_Couch_DatabaseName.DropDown += new System.EventHandler(this.combobox_DatabaseName_DropDown);
           //
           // label24
           //
           this.label24.AutoSize = true;
           this.label24.BackColor = System.Drawing.Color.Transparent;
           this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.label24.ForeColor = System.Drawing.Color.Black;
           this.label24.Location = new System.Drawing.Point(6, 18);
           this.label24.Name = "label24";
           this.label24.Size = new System.Drawing.Size(82, 13);
           this.label24.TabIndex = 11;
           this.label24.Text = "&Server Address:";
           //
           // textBox3
           //
           this.textBox3.Location = new System.Drawing.Point(109, 91);
           this.textBox3.Name = "textBox3";
           this.textBox3.PasswordChar = '*';
           this.textBox3.Size = new System.Drawing.Size(133, 20);
           this.textBox3.TabIndex = 5;
           //
           // label13
           //
           this.label13.AutoSize = true;
           this.label13.Location = new System.Drawing.Point(38, 98);
           this.label13.Name = "label13";
           this.label13.Size = new System.Drawing.Size(56, 13);
           this.label13.TabIndex = 4;
           this.label13.Text = "&Password:";
           //
           // textBox4
           //
           this.textBox4.Location = new System.Drawing.Point(109, 65);
           this.textBox4.Name = "textBox4";
           this.textBox4.Size = new System.Drawing.Size(133, 20);
           this.textBox4.TabIndex = 3;
           //
           // label14
           //
           this.label14.AutoSize = true;
           this.label14.Location = new System.Drawing.Point(38, 72);
           this.label14.Name = "label14";
           this.label14.Size = new System.Drawing.Size(65, 13);
           this.label14.TabIndex = 2;
           this.label14.Text = "&Login name:";
           //
           // ConnectForm
           //
           this.AcceptButton = this.button_Connect;
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
           this.CancelButton = this.button_Cancel;
           this.ClientSize = new System.Drawing.Size(538, 376);
           this.Controls.Add(this.splitContainer);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "ConnectForm";
           this.ShowIcon = false;
           this.ShowInTaskbar = false;
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
           this.Text = "Connect to Server";
           this.Load += new System.EventHandler(this.ConnectForm_Load);
           this.splitContainer.Panel1.ResumeLayout(false);
           this.splitContainer.Panel1.PerformLayout();
           this.splitContainer.Panel2.ResumeLayout(false);
           this.splitContainer.Panel2.PerformLayout();
           this.splitContainer.ResumeLayout(false);
           ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
           this.tabcontrol_ServerTypes.ResumeLayout(false);
           this.tabpage_Mssql.ResumeLayout(false);
           this.tabpage_Mssql.PerformLayout();
           this.tabpage_Mysql.ResumeLayout(false);
           this.tabpage_Mysql.PerformLayout();
           this.tabpage_Odbc.ResumeLayout(false);
           this.tabpage_Odbc.PerformLayout();
           this.tabpage_Oledb.ResumeLayout(false);
           this.tabpage_Oledb.PerformLayout();
           this.tabpage_Oracle.ResumeLayout(false);
           this.tabpage_Oracle.PerformLayout();
           this.tabpage_Pgsql.ResumeLayout(false);
           this.tabpage_Pgsql.PerformLayout();
           this.tabpage_Sqlite.ResumeLayout(false);
           this.tabpage_Sqlite.PerformLayout();
           this.tabpage_Couch.ResumeLayout(false);
           this.tabpage_Couch.PerformLayout();
           this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
      private System.Windows.Forms.RadioButton radioButton2;
      private System.Windows.Forms.SplitContainer splitContainer;
      private System.Windows.Forms.Button button_Connect;
      private System.Windows.Forms.Button button_Cancel;
      private System.Windows.Forms.CheckBox checkbox_LowBandwidth;
      private System.Windows.Forms.TabPage tabpage_Mssql;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TabPage tabpage_Odbc;
      private System.Windows.Forms.Button button_Odbc_Save;
      private System.Windows.Forms.Button button_Odbc_Load;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox textbox_Odbc_ConnectionString;
      private System.Windows.Forms.TabPage tabpage_Oracle;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TabPage tabpage_Oledb;
        private System.Windows.Forms.Button button_Oledb_Save;
        private System.Windows.Forms.Button button_Oledb_Load;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textbox_Oledb_ConnectionString;
        private System.Windows.Forms.TabPage tabpage_Sqlite;
        private System.Windows.Forms.TextBox textBox_SqliteFile;
        private System.Windows.Forms.Label label_SqliteFile;
        private System.Windows.Forms.Button button_BrowseSqliteFile;
        private System.Windows.Forms.TabPage tabpage_Mysql;
        private System.Windows.Forms.Label label_tabMysql_Server;
        private System.Windows.Forms.TabPage tabpage_Pgsql;
        private System.Windows.Forms.TabPage tabpage_Couch;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox combobox_Mssql_DatabaseName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox combobox_Pgsql_DatabaseName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox combobox_Couch_DatabaseName;
        private System.Windows.Forms.TextBox textbox_Couch_Password;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textbox_Couch_LoginName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textbox_Couch_ServerAddress;
        private System.Windows.Forms.TextBox textbox_Mysql_ServerAddress;
        private System.Windows.Forms.TextBox textbox_Pgsql_ServerAddress;
        private System.Windows.Forms.ComboBox combobox_Connection;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label_SignPost;
        private System.Windows.Forms.Label label_Couch_CapslockIsOn;
        private System.Windows.Forms.Button button_SaveSettings;
        private System.Windows.Forms.TextBox textbox_Mssql_ServerAddress;
        private System.Windows.Forms.TextBox textbox_Oracle_DataSource;
        private System.Windows.Forms.Label label_Mssql_CapslockIsOn;
        private System.Windows.Forms.TextBox textbox_Mssql_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textbox_Mssql_LoginName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radiobutton_Mssql_Untrusted;
        private System.Windows.Forms.RadioButton radiobutton_Mssql_Trusted;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox checkbox_Mysql_SavePassword;
        private System.Windows.Forms.Label label_Mysql_CapslockIsOn;
        private System.Windows.Forms.TextBox textbox_Mysql_Password;
        private System.Windows.Forms.Label label_Mysql_Password;
        private System.Windows.Forms.TextBox textbox_Mysql_LoginName;
        private System.Windows.Forms.Label label_Mysql_LoginName;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label_Oracle_CapslockIsOn;
        private System.Windows.Forms.TextBox textbox_Oracle_Password;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textbox_Oracle_LoginName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radiobutton_Oracle_Untrusted;
        private System.Windows.Forms.RadioButton radiobutton_Oracle_Trusted;
        private System.Windows.Forms.Label label_Pgsql_CapslockIsOn;
        private System.Windows.Forms.TextBox textbox_Pgsql_Password;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textbox_Pgsql_LoginName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_DeleteConnection;
        private System.Windows.Forms.Button button_LoadDemoConnectionSettings;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.CheckBox checkbox_Sqlite_SavePassword;
        private System.Windows.Forms.Label label_Sqlite_CapslockIsOn;
        private System.Windows.Forms.TextBox textbox_Sqlite_Password;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox textbox_Sqlite_LoginName;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textbox_Sqlite_ServerAddress;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox combobox_Mysql_DatabaseName;
        private System.Windows.Forms.CheckBox checkbox_Mysql_IntegratedSecurity;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox combobox_Oracle_DatabaseName;
        private System.Windows.Forms.TextBox textBox6;
        private CSharpCustomTabControl.CustomTabControl tabcontrol_ServerTypes;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label_ToDatabase;
    }
}
