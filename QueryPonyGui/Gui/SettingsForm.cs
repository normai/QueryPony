#region Fileinfo
// file        : 20130604°1321 /QueryPony/QueryPonyGui/Gui/SettingsForm.cs
// summary     : This file stores class 'SettingsForm' to constitute the Settings Form.
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryPonyGui.Gui
{

   /// <summary>This class constitutes the Settings Form</summary>
   /// <remarks>id : 20130604°1322</remarks>
   public partial class SettingsForm : Form
   {

      /// <summary>This constructor creates a new Settings Form</summary>
      /// <remarks>id : 20130604°1323</remarks>
      public SettingsForm()
      {
         InitializeComponent();
         LoadSettings();

         _flagValueChangeByUser = true;

         // [seq 20130828°1442]
         _PropertyChanged = SettingsForm_PropertyChanged;

         // [seq 20130828°1452]
         _MRUFileAdded = SettingsForm_MRUFileAdded;
      }

      /// <summary>This field stores a flag to prevent recursion during the initial settings loading</summary>
      /// <remarks>id : 20130812°0921</remarks>
      private bool _flagValueChangeByUser = false;

      /// <summary>This field(?) stores the PropertyChanged eventhandler</summary>
      /// <remarks>
      /// id : 20130809°1514 (20130604°0507)
      /// note : Not sure whether this eventhandler makes sense inside the Settings
      ///    Form. It is copied here from IQueryForm.cs for purely formal reasons while
      ///    implementing the Settings Form to be opened as form-on-tab. In IQueryForm.cs,
      ///    this eventhandler(s) are wanted in method 20130809°1511 FurnishFormOnTab.
      ///    If it is missing here, we get exceptions in EnableControls().
      /// todo : Check, what is the exact matter with EventHandler PropertyChanged in
      ///    SettingsForm.cs. Clean it up or activate it. [todo 20130809°151402 priority low]
      /// </remarks>
      private event EventHandler<EventArgs> _PropertyChanged;

      /// <summary>This field(?) stores the MRUFileAdded eventhandler</summary>
      /// <remarks>
      /// id : 20130809°1515 (20130604°0508)
      /// note : See note in the PropertyChanged eventhandler 20130809°1514 above.
      /// todo : Check, what is the exact matter with EventHandler MRUFileAdded in SettingsForm.cs.
      ///    Either clean it up or make it active. [todo 20130809°151502 priority low]
      /// </remarks>
      private event EventHandler<MRUFileAddedEventArgs> _MRUFileAdded;

      /// <summary>This method presents this form as a modal dialog. It is called from a menu event in MainForm</summary>
      /// <remarks>
      /// id : 20130604°1324
      /// note : Remember issue 20130604°132402 'Warning: IWin32Windowhides inherited member'
      /// </remarks>
      public DialogResult ShowMyDialog(IWin32Window owner)
      {
         this.Icon = ((Form) owner).Icon;
         return base.ShowDialog(owner);
      }

      /// <summary>This method loads the actual settings</summary>
      /// <remarks>id : 20130604°1325</remarks>
      private void LoadSettings()
      {
         checkbox_ExpandRowNumber.Checked = Properties.Settings.Default.ExpandRowNumber;
         checkbox_GridDefault.Checked = Properties.Settings.Default.ResultInGridDefault;
         checkbox_DeveloperMode.Checked = Properties.Settings.Default.DeveloperMode;
         checkbox_ShowNulls.Checked = Properties.Settings.Default.ShowNulls;
         checkbox_SqlAuthDefault.Checked = Properties.Settings.Default.MssqlServerAuthenticationDefault;
         checkbox_SyntaxHighlight.Checked = Properties.Settings.Default.SyntaxHighlighting;

         textbox_Delimiter.Text = Properties.Settings.Default.Delimiter;
         textbox_TextDelimiter.Text = Properties.Settings.Default.TextDelimiter.ToString();

         picturebox_ColorKeywords.BackColor = Properties.Settings.Default.ColorKeywords;
         picturebox_ColorNumbers.BackColor = Properties.Settings.Default.ColorNumbers;
         picturebox_ColorOperators.BackColor = Properties.Settings.Default.ColorOperators;
         picturebox_ColorStrings.BackColor = Properties.Settings.Default.ColorStrings;

         checkbox_ShowDebugProgramExit.Checked = Properties.Settings.Default.ShowDebugProgramExit;
         checkbox_ShowDeveloperObjects.Checked = Properties.Settings.Default.ShowDeveloperObjects;

      }

      /// <summary>This method saves the given settings</summary>
      /// <remarks>id : 20130604°1326</remarks>
      private void SaveSettings()
      {
         Properties.Settings.Default.ExpandRowNumber = checkbox_ExpandRowNumber.Checked;
         Properties.Settings.Default.MssqlServerAuthenticationDefault = checkbox_SqlAuthDefault.Checked;
         Properties.Settings.Default.DeveloperMode = checkbox_DeveloperMode.Checked;
         Properties.Settings.Default.ResultInGridDefault = checkbox_GridDefault.Checked;
         Properties.Settings.Default.ShowNulls = checkbox_ShowNulls.Checked;
         Properties.Settings.Default.SyntaxHighlighting = checkbox_SyntaxHighlight.Checked;

         Properties.Settings.Default.ColorKeywords = picturebox_ColorKeywords.BackColor;
         Properties.Settings.Default.ColorNumbers = picturebox_ColorNumbers.BackColor;
         Properties.Settings.Default.ColorOperators = picturebox_ColorOperators.BackColor;
         Properties.Settings.Default.ColorStrings = picturebox_ColorStrings.BackColor;

         if (textbox_Delimiter.Text != "")
         {
            Properties.Settings.Default.Delimiter = textbox_Delimiter.Text;
         }
         else
         {
            Properties.Settings.Default.Delimiter = ",";
         }

         if (textbox_TextDelimiter.Text.Length == 1 && textbox_TextDelimiter.Text != " ")
         {
            char c = char.Parse(textbox_TextDelimiter.Text);
            Properties.Settings.Default.TextDelimiter = c;
         }
         else
         {
            Properties.Settings.Default.TextDelimiter = '"';
         }

         // Immediately save to file
         Properties.Settings.Default.Save();

         // Possibly change the 'Query Options'menu item availability [line 20130812°1403]
         MainForm._mainform.EnableControlsOthers();

         Properties.Settings.Default.ShowDebugProgramExit = checkbox_ShowDebugProgramExit.Checked;
         Properties.Settings.Default.ShowDeveloperObjects = checkbox_ShowDeveloperObjects.Checked;
      }

      /// <summary>This eventhandler processes the ... click</summary>
      /// <remarks>id : 20130604°1329</remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void picturebox_Color_Click(object sender, EventArgs e)
      {
         colorDialog.Color = ((PictureBox)sender).BackColor;
         colorDialog.ShowDialog();
         ((PictureBox)sender).BackColor = colorDialog.Color;
      }

      /// <summary>This eventhandler processes the button 'Cancel' event</summary>
      /// <remarks>
      /// id : 20130604°1328
      /// todo : Implement a value-restore feature. [todo 20130809°1543]
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_Cancel_Click(object sender, EventArgs e)
      {
         if (Globs.Debag.Execute_No)
         {
            MainForm._mainform.DoDisconnect();                                 // Only for form-on-tab, not for modal dialog! [line 20130809°1524]

            // Original sequence for the form as a modal dialog
            DialogResult = DialogResult.Cancel;
            Close();
         }
      }

      /// <summary>This eventhandler processes the button 'Close' click</summary>
      /// <remarks>
      /// id : 20130809°1541
      /// todo : Adjust for the differences between possible opening modes 'modal dialog'
      ///    and 'form-on-tab'. [todo 20130809°1542]
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_Close_Click(object sender, EventArgs e)
      {
         MainForm._mainform.DoDisconnect();                                    // Quick try only for form-on-tab, not for modal dialog! [line 20130809°1524]

         // Original sequence for the form as a modal dialog
         DialogResult = DialogResult.Cancel;
         Close();
      }

      /// <summary>This eventhandler processes the button 'Save' event</summary>
      /// <remarks>
      /// id : 20130604°1327
      /// todo : Adjust for the differences between possible opening modes 'modal dialog'
      ///    and 'form-on-tab'. [todo 20130809°1544 priority low]
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void button_Save_Click(object sender, EventArgs e)
      {
         SaveSettings();

         if (Globs.Debag.Execute_No)
         {
            DialogResult = DialogResult.OK;
            Close();
         }
      }

      /// <summary>This eventhandler processes all CheckBoxes CheckedChanged events</summary>
      /// <remarks>id : 20130812°0911</remarks>
      /// <param name="sender">The CheckBox which sent the event</param>
      /// <param name="e">The event object</param>
      private void checkbox_AllCheckboxes_CheckedChanged(object sender, EventArgs e)
      {
         valueChanged(sender, e);
      }

      /// <summary>This eventhandler processes all TextBoxes TextChanged events</summary>
      /// <remarks>id : 20130812°0912</remarks>
      /// <param name="sender">The TextBox which sent the event</param>
      /// <param name="e">The event object</param>
      private void textbox_AllTextBoxes_TextChanged(object sender, EventArgs e)
      {
         valueChanged(sender, e);
      }

      /// <summary>This eventhandler processes all PictureBoxes SystemColorsChanged events</summary>
      /// <remarks>id : 20130812°0913</remarks>
      /// <param name="sender">The PictureBox which sent the event</param>
      /// <param name="e">The event object</param>
      private void picturebox_AllPictureBoxes_BackColorChanged(object sender, EventArgs e)
      {
         valueChanged(sender, e);
      }

      /// <summary>This method processes all CheckBox, TexBox and PictureBox value changed events</summary>
      /// <remarks>id : 20130812°0914</remarks>
      /// <param name="sender">The CheckBox, TexBox or PictureBox which sent the event</param>
      /// <param name="e">The event object</param>
      private void valueChanged(object sender, EventArgs e)
      {
         if (_flagValueChangeByUser)
         {
            SaveSettings();
         }
      }

      /// <summary>This eventhandler processes the  event</summary>
      /// <remarks>
      /// id : 20130828°1441
      /// note : Implemented to avoid compiler warning "The event 'PropertyChanged' is never used" [field 20130809°1514].
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void SettingsForm_PropertyChanged(object sender, EventArgs e)
      {
         return;
      }

      /// <summary>This eventhandler processes the  event</summary>
      /// <remarks>
      /// id : 20130828°1451
      /// note : Implemented to avoid compiler warning "The event 'PropertyChanged' is never used" [field 20130809°1514].
      /// </remarks>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The event object</param>
      private void SettingsForm_MRUFileAdded(object sender, MRUFileAddedEventArgs e)
      {
         return;
      }

      /// <summary>This eventhandler processes the checkbox 'ShowDevelop' CheckedChanged event</summary>
      /// <remarks>id : 20130828°1612</remarks>
      /// <param name="sender">The object which sent the event</param>
      /// <param name="e">The event object</param>
      private void checkbox_ShowDevelop_CheckedChanged(object sender, EventArgs e)
      {
         valueChanged(sender, e);
         switchDeveloperView();
      }

      /// <summary>This eventhandler processes the checkbox 'DebugProgramExit' CheckedChanged event</summary>
      /// <remarks>id : 20130828°1613</remarks>
      /// <param name="sender">The object which sent the event</param>
      /// <param name="e">The event object</param>
      private void checkbox_DebugProgramExit_CheckedChanged(object sender, EventArgs e)
      {
         valueChanged(sender, e);
      }

      /// <summary>This method toggles the visibility of the developer objects</summary>
      /// <remarks>id : 20130828°1614</remarks>
      private void switchDeveloperView()
      {

         // find all QueryForms
         // (compare sequence 20130709°1211)
         TabControl.TabPageCollection tpc = MainForm._maintabcontrol.TabPages;
         foreach (TabPage tp in tpc)
         {
            Control.ControlCollection cc = tp.Controls;
            foreach (Control c in cc)
            {
               QueryForm qf = c as QueryForm;

               // this is a tabpage with a connection
               if (qf != null)
               {
                  qf.switchDeveloperObjectsVisibility(checkbox_ShowDeveloperObjects.Checked);
               }
            }
         }
      }
   }
}
