namespace QueryPonyGui.Gui
{
	partial class SettingsForm
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
         this.button_Save = new System.Windows.Forms.Button();
         this.button_Cancel = new System.Windows.Forms.Button();
         this.checkbox_SqlAuthDefault = new System.Windows.Forms.CheckBox();
         this.textbox_TextDelimiter = new System.Windows.Forms.TextBox();
         this.textbox_Delimiter = new System.Windows.Forms.TextBox();
         this.label_TextDelimiter = new System.Windows.Forms.Label();
         this.label_Delimiter = new System.Windows.Forms.Label();
         this.colorDialog = new System.Windows.Forms.ColorDialog();
         this.label_ColorKeywords = new System.Windows.Forms.Label();
         this.picturebox_ColorNumbers = new System.Windows.Forms.PictureBox();
         this.picturebox_ColorStrings = new System.Windows.Forms.PictureBox();
         this.picturebox_ColorOperators = new System.Windows.Forms.PictureBox();
         this.picturebox_ColorKeywords = new System.Windows.Forms.PictureBox();
         this.label_ColorNumbers = new System.Windows.Forms.Label();
         this.label_ColorStrings = new System.Windows.Forms.Label();
         this.label_ColorOperators = new System.Windows.Forms.Label();
         this.button_Close = new System.Windows.Forms.Button();
         this.label_Authentication = new System.Windows.Forms.Label();
         this.checkbox_ShowNulls = new System.Windows.Forms.CheckBox();
         this.checkbox_GridDefault = new System.Windows.Forms.CheckBox();
         this.checkbox_SyntaxHighlight = new System.Windows.Forms.CheckBox();
         this.checkbox_ExpandRowNumber = new System.Windows.Forms.CheckBox();
         this.label_General = new System.Windows.Forms.Label();
         this.label_CSV = new System.Windows.Forms.Label();
         this.label_Highlighting = new System.Windows.Forms.Label();
         this.checkbox_DeveloperMode = new System.Windows.Forms.CheckBox();
         this.label_QueryForms = new System.Windows.Forms.Label();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabpage_Main = new System.Windows.Forms.TabPage();
         this.tabpage_Debug = new System.Windows.Forms.TabPage();
         this.checkbox_ShowDebugProgramExit = new System.Windows.Forms.CheckBox();
         this.checkbox_ShowDeveloperObjects = new System.Windows.Forms.CheckBox();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorNumbers)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorStrings)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorOperators)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorKeywords)).BeginInit();
         this.tabControl1.SuspendLayout();
         this.tabpage_Main.SuspendLayout();
         this.tabpage_Debug.SuspendLayout();
         this.SuspendLayout();
         //
         // button_Save
         //
         this.button_Save.Location = new System.Drawing.Point(345, 3);
         this.button_Save.Name = "button_Save";
         this.button_Save.Size = new System.Drawing.Size(74, 23);
         this.button_Save.TabIndex = 0;
         this.button_Save.Text = "Save";
         this.button_Save.UseVisualStyleBackColor = true;
         this.button_Save.Visible = false;
         this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
         //
         // button_Cancel
         //
         this.button_Cancel.CausesValidation = false;
         this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button_Cancel.ForeColor = System.Drawing.Color.LightGray;
         this.button_Cancel.Location = new System.Drawing.Point(253, 3);
         this.button_Cancel.Name = "button_Cancel";
         this.button_Cancel.Size = new System.Drawing.Size(74, 23);
         this.button_Cancel.TabIndex = 1;
         this.button_Cancel.Text = "Cancel";
         this.button_Cancel.UseVisualStyleBackColor = true;
         this.button_Cancel.Visible = false;
         this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
         //
         // checkbox_SqlAuthDefault
         //
         this.checkbox_SqlAuthDefault.AutoSize = true;
         this.checkbox_SqlAuthDefault.Location = new System.Drawing.Point(55, 253);
         this.checkbox_SqlAuthDefault.Name = "checkbox_SqlAuthDefault";
         this.checkbox_SqlAuthDefault.Size = new System.Drawing.Size(189, 17);
         this.checkbox_SqlAuthDefault.TabIndex = 0;
         this.checkbox_SqlAuthDefault.Text = "Use SQL Authentication as default";
         this.checkbox_SqlAuthDefault.UseVisualStyleBackColor = true;
         this.checkbox_SqlAuthDefault.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // textbox_TextDelimiter
         //
         this.textbox_TextDelimiter.Location = new System.Drawing.Point(395, 66);
         this.textbox_TextDelimiter.Name = "textbox_TextDelimiter";
         this.textbox_TextDelimiter.Size = new System.Drawing.Size(32, 20);
         this.textbox_TextDelimiter.TabIndex = 3;
         this.textbox_TextDelimiter.TextChanged += new System.EventHandler(this.textbox_AllTextBoxes_TextChanged);
         //
         // textbox_Delimiter
         //
         this.textbox_Delimiter.Location = new System.Drawing.Point(395, 40);
         this.textbox_Delimiter.Name = "textbox_Delimiter";
         this.textbox_Delimiter.Size = new System.Drawing.Size(32, 20);
         this.textbox_Delimiter.TabIndex = 2;
         this.textbox_Delimiter.TextChanged += new System.EventHandler(this.textbox_AllTextBoxes_TextChanged);
         //
         // label_TextDelimiter
         //
         this.label_TextDelimiter.AutoSize = true;
         this.label_TextDelimiter.Location = new System.Drawing.Point(320, 70);
         this.label_TextDelimiter.Name = "label_TextDelimiter";
         this.label_TextDelimiter.Size = new System.Drawing.Size(74, 13);
         this.label_TextDelimiter.TabIndex = 1;
         this.label_TextDelimiter.Text = "Text Delimiter:";
         //
         // label_Delimiter
         //
         this.label_Delimiter.AutoSize = true;
         this.label_Delimiter.Location = new System.Drawing.Point(321, 43);
         this.label_Delimiter.Name = "label_Delimiter";
         this.label_Delimiter.Size = new System.Drawing.Size(50, 13);
         this.label_Delimiter.TabIndex = 0;
         this.label_Delimiter.Text = "Delimiter:";
         //
         // label_ColorKeywords
         //
         this.label_ColorKeywords.AutoSize = true;
         this.label_ColorKeywords.Location = new System.Drawing.Point(321, 179);
         this.label_ColorKeywords.Name = "label_ColorKeywords";
         this.label_ColorKeywords.Size = new System.Drawing.Size(80, 13);
         this.label_ColorKeywords.TabIndex = 4;
         this.label_ColorKeywords.Text = "SQL Keywords:";
         //
         // picturebox_ColorNumbers
         //
         this.picturebox_ColorNumbers.BackColor = System.Drawing.SystemColors.MenuText;
         this.picturebox_ColorNumbers.Location = new System.Drawing.Point(404, 251);
         this.picturebox_ColorNumbers.Name = "picturebox_ColorNumbers";
         this.picturebox_ColorNumbers.Size = new System.Drawing.Size(47, 17);
         this.picturebox_ColorNumbers.TabIndex = 11;
         this.picturebox_ColorNumbers.TabStop = false;
         this.picturebox_ColorNumbers.BackColorChanged += new System.EventHandler(this.picturebox_AllPictureBoxes_BackColorChanged);
         this.picturebox_ColorNumbers.Click += new System.EventHandler(this.picturebox_Color_Click);
         //
         // picturebox_ColorStrings
         //
         this.picturebox_ColorStrings.BackColor = System.Drawing.SystemColors.MenuText;
         this.picturebox_ColorStrings.Location = new System.Drawing.Point(404, 228);
         this.picturebox_ColorStrings.Name = "picturebox_ColorStrings";
         this.picturebox_ColorStrings.Size = new System.Drawing.Size(47, 17);
         this.picturebox_ColorStrings.TabIndex = 10;
         this.picturebox_ColorStrings.TabStop = false;
         this.picturebox_ColorStrings.BackColorChanged += new System.EventHandler(this.picturebox_AllPictureBoxes_BackColorChanged);
         this.picturebox_ColorStrings.Click += new System.EventHandler(this.picturebox_Color_Click);
         //
         // picturebox_ColorOperators
         //
         this.picturebox_ColorOperators.BackColor = System.Drawing.SystemColors.MenuText;
         this.picturebox_ColorOperators.Location = new System.Drawing.Point(404, 202);
         this.picturebox_ColorOperators.Name = "picturebox_ColorOperators";
         this.picturebox_ColorOperators.Size = new System.Drawing.Size(47, 17);
         this.picturebox_ColorOperators.TabIndex = 9;
         this.picturebox_ColorOperators.TabStop = false;
         this.picturebox_ColorOperators.BackColorChanged += new System.EventHandler(this.picturebox_AllPictureBoxes_BackColorChanged);
         this.picturebox_ColorOperators.Click += new System.EventHandler(this.picturebox_Color_Click);
         //
         // picturebox_ColorKeywords
         //
         this.picturebox_ColorKeywords.BackColor = System.Drawing.SystemColors.MenuText;
         this.picturebox_ColorKeywords.Location = new System.Drawing.Point(404, 177);
         this.picturebox_ColorKeywords.Name = "picturebox_ColorKeywords";
         this.picturebox_ColorKeywords.Size = new System.Drawing.Size(47, 17);
         this.picturebox_ColorKeywords.TabIndex = 8;
         this.picturebox_ColorKeywords.TabStop = false;
         this.picturebox_ColorKeywords.BackColorChanged += new System.EventHandler(this.picturebox_AllPictureBoxes_BackColorChanged);
         this.picturebox_ColorKeywords.Click += new System.EventHandler(this.picturebox_Color_Click);
         //
         // label_ColorNumbers
         //
         this.label_ColorNumbers.AutoSize = true;
         this.label_ColorNumbers.Location = new System.Drawing.Point(321, 253);
         this.label_ColorNumbers.Name = "label_ColorNumbers";
         this.label_ColorNumbers.Size = new System.Drawing.Size(52, 13);
         this.label_ColorNumbers.TabIndex = 7;
         this.label_ColorNumbers.Text = "Numbers:";
         //
         // label_ColorStrings
         //
         this.label_ColorStrings.AutoSize = true;
         this.label_ColorStrings.Location = new System.Drawing.Point(321, 230);
         this.label_ColorStrings.Name = "label_ColorStrings";
         this.label_ColorStrings.Size = new System.Drawing.Size(42, 13);
         this.label_ColorStrings.TabIndex = 6;
         this.label_ColorStrings.Text = "Strings:";
         //
         // label_ColorOperators
         //
         this.label_ColorOperators.AutoSize = true;
         this.label_ColorOperators.Location = new System.Drawing.Point(321, 204);
         this.label_ColorOperators.Name = "label_ColorOperators";
         this.label_ColorOperators.Size = new System.Drawing.Size(56, 13);
         this.label_ColorOperators.TabIndex = 5;
         this.label_ColorOperators.Text = "Operators:";
         //
         // button_Close
         //
         this.button_Close.CausesValidation = false;
         this.button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.button_Close.Location = new System.Drawing.Point(437, 3);
         this.button_Close.Name = "button_Close";
         this.button_Close.Size = new System.Drawing.Size(74, 23);
         this.button_Close.TabIndex = 6;
         this.button_Close.Text = "Close";
         this.button_Close.UseVisualStyleBackColor = true;
         this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
         //
         // label_Authentication
         //
         this.label_Authentication.AutoSize = true;
         this.label_Authentication.ForeColor = System.Drawing.Color.Blue;
         this.label_Authentication.Location = new System.Drawing.Point(39, 230);
         this.label_Authentication.Name = "label_Authentication";
         this.label_Authentication.Size = new System.Drawing.Size(78, 13);
         this.label_Authentication.TabIndex = 7;
         this.label_Authentication.Text = "Authentication:";
         //
         // checkbox_ShowNulls
         //
         this.checkbox_ShowNulls.AutoSize = true;
         this.checkbox_ShowNulls.Location = new System.Drawing.Point(55, 91);
         this.checkbox_ShowNulls.Name = "checkbox_ShowNulls";
         this.checkbox_ShowNulls.Size = new System.Drawing.Size(106, 17);
         this.checkbox_ShowNulls.TabIndex = 1;
         this.checkbox_ShowNulls.Text = "Show null values";
         this.checkbox_ShowNulls.UseVisualStyleBackColor = true;
         this.checkbox_ShowNulls.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // checkbox_GridDefault
         //
         this.checkbox_GridDefault.AutoSize = true;
         this.checkbox_GridDefault.Location = new System.Drawing.Point(55, 44);
         this.checkbox_GridDefault.Name = "checkbox_GridDefault";
         this.checkbox_GridDefault.Size = new System.Drawing.Size(166, 17);
         this.checkbox_GridDefault.TabIndex = 0;
         this.checkbox_GridDefault.Text = "Show results in grid as default";
         this.checkbox_GridDefault.UseVisualStyleBackColor = true;
         this.checkbox_GridDefault.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // checkbox_SyntaxHighlight
         //
         this.checkbox_SyntaxHighlight.AutoSize = true;
         this.checkbox_SyntaxHighlight.Location = new System.Drawing.Point(55, 68);
         this.checkbox_SyntaxHighlight.Name = "checkbox_SyntaxHighlight";
         this.checkbox_SyntaxHighlight.Size = new System.Drawing.Size(138, 17);
         this.checkbox_SyntaxHighlight.TabIndex = 1;
         this.checkbox_SyntaxHighlight.Text = "Use Syntax Highlighting";
         this.checkbox_SyntaxHighlight.UseVisualStyleBackColor = true;
         this.checkbox_SyntaxHighlight.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // checkbox_ExpandRowNumber
         //
         this.checkbox_ExpandRowNumber.AutoSize = true;
         this.checkbox_ExpandRowNumber.Location = new System.Drawing.Point(55, 115);
         this.checkbox_ExpandRowNumber.Name = "checkbox_ExpandRowNumber";
         this.checkbox_ExpandRowNumber.Size = new System.Drawing.Size(165, 17);
         this.checkbox_ExpandRowNumber.TabIndex = 2;
         this.checkbox_ExpandRowNumber.Text = "Expand Row Number Column";
         this.checkbox_ExpandRowNumber.UseVisualStyleBackColor = true;
         this.checkbox_ExpandRowNumber.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // label_General
         //
         this.label_General.AutoSize = true;
         this.label_General.ForeColor = System.Drawing.Color.Blue;
         this.label_General.Location = new System.Drawing.Point(39, 159);
         this.label_General.Name = "label_General";
         this.label_General.Size = new System.Drawing.Size(42, 13);
         this.label_General.TabIndex = 8;
         this.label_General.Text = "Debug:";
         //
         // label_CSV
         //
         this.label_CSV.AutoSize = true;
         this.label_CSV.ForeColor = System.Drawing.Color.Blue;
         this.label_CSV.Location = new System.Drawing.Point(307, 16);
         this.label_CSV.Name = "label_CSV";
         this.label_CSV.Size = new System.Drawing.Size(31, 13);
         this.label_CSV.TabIndex = 9;
         this.label_CSV.Text = "CSV:";
         //
         // label_Highlighting
         //
         this.label_Highlighting.AutoSize = true;
         this.label_Highlighting.ForeColor = System.Drawing.Color.Blue;
         this.label_Highlighting.Location = new System.Drawing.Point(307, 152);
         this.label_Highlighting.Name = "label_Highlighting";
         this.label_Highlighting.Size = new System.Drawing.Size(132, 13);
         this.label_Highlighting.TabIndex = 12;
         this.label_Highlighting.Text = "Syntax Highlighting Colors:";
         //
         // checkbox_DeveloperMode
         //
         this.checkbox_DeveloperMode.AutoSize = true;
         this.checkbox_DeveloperMode.Location = new System.Drawing.Point(55, 182);
         this.checkbox_DeveloperMode.Name = "checkbox_DeveloperMode";
         this.checkbox_DeveloperMode.Size = new System.Drawing.Size(105, 17);
         this.checkbox_DeveloperMode.TabIndex = 13;
         this.checkbox_DeveloperMode.Text = "Developer Mode";
         this.checkbox_DeveloperMode.UseVisualStyleBackColor = true;
         this.checkbox_DeveloperMode.CheckedChanged += new System.EventHandler(this.checkbox_AllCheckboxes_CheckedChanged);
         //
         // label_QueryForms
         //
         this.label_QueryForms.AutoSize = true;
         this.label_QueryForms.ForeColor = System.Drawing.Color.Blue;
         this.label_QueryForms.Location = new System.Drawing.Point(39, 16);
         this.label_QueryForms.Name = "label_QueryForms";
         this.label_QueryForms.Size = new System.Drawing.Size(69, 13);
         this.label_QueryForms.TabIndex = 14;
         this.label_QueryForms.Text = "Query Forms:";
         //
         // tabControl1
         //
         this.tabControl1.Controls.Add(this.tabpage_Main);
         this.tabControl1.Controls.Add(this.tabpage_Debug);
         this.tabControl1.Location = new System.Drawing.Point(11, 29);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(513, 330);
         this.tabControl1.TabIndex = 15;
         //
         // tabpage_Main
         //
         this.tabpage_Main.Controls.Add(this.label_CSV);
         this.tabpage_Main.Controls.Add(this.label_QueryForms);
         this.tabpage_Main.Controls.Add(this.checkbox_ShowNulls);
         this.tabpage_Main.Controls.Add(this.checkbox_DeveloperMode);
         this.tabpage_Main.Controls.Add(this.checkbox_GridDefault);
         this.tabpage_Main.Controls.Add(this.label_Highlighting);
         this.tabpage_Main.Controls.Add(this.checkbox_SqlAuthDefault);
         this.tabpage_Main.Controls.Add(this.picturebox_ColorNumbers);
         this.tabpage_Main.Controls.Add(this.checkbox_SyntaxHighlight);
         this.tabpage_Main.Controls.Add(this.picturebox_ColorStrings);
         this.tabpage_Main.Controls.Add(this.label_Authentication);
         this.tabpage_Main.Controls.Add(this.label_Delimiter);
         this.tabpage_Main.Controls.Add(this.picturebox_ColorOperators);
         this.tabpage_Main.Controls.Add(this.label_ColorKeywords);
         this.tabpage_Main.Controls.Add(this.textbox_TextDelimiter);
         this.tabpage_Main.Controls.Add(this.label_TextDelimiter);
         this.tabpage_Main.Controls.Add(this.picturebox_ColorKeywords);
         this.tabpage_Main.Controls.Add(this.label_ColorOperators);
         this.tabpage_Main.Controls.Add(this.label_General);
         this.tabpage_Main.Controls.Add(this.checkbox_ExpandRowNumber);
         this.tabpage_Main.Controls.Add(this.label_ColorNumbers);
         this.tabpage_Main.Controls.Add(this.label_ColorStrings);
         this.tabpage_Main.Controls.Add(this.textbox_Delimiter);
         this.tabpage_Main.Location = new System.Drawing.Point(4, 22);
         this.tabpage_Main.Name = "tabpage_Main";
         this.tabpage_Main.Padding = new System.Windows.Forms.Padding(3);
         this.tabpage_Main.Size = new System.Drawing.Size(505, 304);
         this.tabpage_Main.TabIndex = 0;
         this.tabpage_Main.Text = "   Main   ";
         this.tabpage_Main.UseVisualStyleBackColor = true;
         //
         // tabpage_Debug
         //
         this.tabpage_Debug.Controls.Add(this.checkbox_ShowDebugProgramExit);
         this.tabpage_Debug.Controls.Add(this.checkbox_ShowDeveloperObjects);
         this.tabpage_Debug.Location = new System.Drawing.Point(4, 22);
         this.tabpage_Debug.Name = "tabpage_Debug";
         this.tabpage_Debug.Padding = new System.Windows.Forms.Padding(3);
         this.tabpage_Debug.Size = new System.Drawing.Size(505, 304);
         this.tabpage_Debug.TabIndex = 1;
         this.tabpage_Debug.Text = "Debug";
         this.tabpage_Debug.UseVisualStyleBackColor = true;
         //
         // checkbox_ShowDebugProgramExit
         //
         this.checkbox_ShowDebugProgramExit.AutoSize = true;
         this.checkbox_ShowDebugProgramExit.Location = new System.Drawing.Point(58, 110);
         this.checkbox_ShowDebugProgramExit.Name = "checkbox_ShowDebugProgramExit";
         this.checkbox_ShowDebugProgramExit.Size = new System.Drawing.Size(168, 17);
         this.checkbox_ShowDebugProgramExit.TabIndex = 1;
         this.checkbox_ShowDebugProgramExit.Text = "Debug Dialog on Program Exit";
         this.checkbox_ShowDebugProgramExit.UseVisualStyleBackColor = true;
         this.checkbox_ShowDebugProgramExit.CheckedChanged += new System.EventHandler(this.checkbox_DebugProgramExit_CheckedChanged);
         //
         // checkbox_ShowDeveloperObjects
         //
         this.checkbox_ShowDeveloperObjects.AutoSize = true;
         this.checkbox_ShowDeveloperObjects.Location = new System.Drawing.Point(58, 60);
         this.checkbox_ShowDeveloperObjects.Name = "checkbox_ShowDeveloperObjects";
         this.checkbox_ShowDeveloperObjects.Size = new System.Drawing.Size(101, 17);
         this.checkbox_ShowDeveloperObjects.TabIndex = 0;
         this.checkbox_ShowDeveloperObjects.Text = "Developer View";
         this.checkbox_ShowDeveloperObjects.UseVisualStyleBackColor = true;
         this.checkbox_ShowDeveloperObjects.CheckedChanged += new System.EventHandler(this.checkbox_ShowDevelop_CheckedChanged);
         //
         // SettingsForm
         //
         this.AcceptButton = this.button_Save;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.WhiteSmoke;
         this.CancelButton = this.button_Cancel;
         this.ClientSize = new System.Drawing.Size(536, 374);
         this.Controls.Add(this.tabControl1);
         this.Controls.Add(this.button_Close);
         this.Controls.Add(this.button_Cancel);
         this.Controls.Add(this.button_Save);
         this.Name = "SettingsForm";
         this.Text = "Settings";
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorNumbers)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorStrings)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorOperators)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.picturebox_ColorKeywords)).EndInit();
         this.tabControl1.ResumeLayout(false);
         this.tabpage_Main.ResumeLayout(false);
         this.tabpage_Main.PerformLayout();
         this.tabpage_Debug.ResumeLayout(false);
         this.tabpage_Debug.PerformLayout();
         this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_Save;
      private System.Windows.Forms.Button button_Cancel;
      private System.Windows.Forms.CheckBox checkbox_SqlAuthDefault;
		private System.Windows.Forms.TextBox textbox_TextDelimiter;
      private System.Windows.Forms.TextBox textbox_Delimiter;
		private System.Windows.Forms.Label label_TextDelimiter;
      private System.Windows.Forms.Label label_Delimiter;
		private System.Windows.Forms.ColorDialog colorDialog;
      private System.Windows.Forms.Label label_ColorKeywords;
		private System.Windows.Forms.PictureBox picturebox_ColorNumbers;
		private System.Windows.Forms.PictureBox picturebox_ColorStrings;
		private System.Windows.Forms.PictureBox picturebox_ColorOperators;
		private System.Windows.Forms.PictureBox picturebox_ColorKeywords;
		private System.Windows.Forms.Label label_ColorNumbers;
		private System.Windows.Forms.Label label_ColorStrings;
      private System.Windows.Forms.Label label_ColorOperators;
      private System.Windows.Forms.Button button_Close;
      private System.Windows.Forms.Label label_Authentication;
      private System.Windows.Forms.CheckBox checkbox_ShowNulls;
      private System.Windows.Forms.CheckBox checkbox_GridDefault;
      private System.Windows.Forms.CheckBox checkbox_SyntaxHighlight;
      private System.Windows.Forms.CheckBox checkbox_ExpandRowNumber;
      private System.Windows.Forms.Label label_General;
      private System.Windows.Forms.Label label_CSV;
      private System.Windows.Forms.Label label_Highlighting;
      private System.Windows.Forms.CheckBox checkbox_DeveloperMode;
      private System.Windows.Forms.Label label_QueryForms;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabpage_Main;
      private System.Windows.Forms.TabPage tabpage_Debug;
      private System.Windows.Forms.CheckBox checkbox_ShowDebugProgramExit;
      private System.Windows.Forms.CheckBox checkbox_ShowDeveloperObjects;

	}
}
