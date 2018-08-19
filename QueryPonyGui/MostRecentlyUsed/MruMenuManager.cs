#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/MostRecentlyUsed/MruMenuManager.cs from
//                "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1811
// summary     : This file stores Genghis class 'MruMenuManager' to provide ...
// license     : Custom License
// copyright   : Copyright 2002-2004 The Genghis Group http://genghis.codeplex.com
// authors     : Shawn Wildermuth and others
// status      :
// note        :
// callers     :
#endregion

using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MRUSampleControlLibrary {

   /// <summary>This class provides ...</summary>
   /// <remarks>id : 20130604°1812</remarks>
   [DefaultEventAttribute("MruMenuItemClick")]
   public class MruMenuManager : Component, IPersistComponentSettings {

      // MRU menu list items buffer
      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1813</remarks>
      MruMenuListItems mruMenuListItems;

      // Property fields
      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1814</remarks>
      ToolStripMenuItem mruListMenu = null;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1842</remarks>
      MruListDisplayStyle displayMode = MruListDisplayStyle.InMenu;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1843</remarks>
      int textWidth = 40;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1844</remarks>
      int maximumItems = 10;

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1815</remarks>
      public MruMenuManager() {
         mruMenuListItems = new MruMenuListItems(MaximumItems);
      }

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1816</remarks>
      public MruMenuManager(IContainer container)
         : this() {
         container.Add(this);
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1817</remarks>
      [CategoryAttribute("Appearance")]
      [DescriptionAttribute("Gets or sets the menu item that will display the Mru list.")]
      [DefaultValueAttribute(null)]
      public ToolStripMenuItem MruListMenu {
         get { return mruListMenu; }
         set {
            mruListMenu = value;
            // Register Opening event
            if ( (!this.DesignMode) && (mruListMenu != null) ) {
               ToolStripDropDownMenu rootMenu = (ToolStripDropDownMenu)mruListMenu.Owner;
               rootMenu.Opening += RootMenu_Opening;
            }
         }
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1818</remarks>
      [CategoryAttribute("Appearance")]
      [DescriptionAttribute("Gets or sets the Mru menu's display mode; in the same menu or in a sub-menu.")]
      [DefaultValueAttribute(MruListDisplayStyle.InMenu)]
      public MruListDisplayStyle DisplayStyle {
         get { return displayMode; }
         set { displayMode = value; }
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1819</remarks>
      [CategoryAttribute("Appearance")]
      [DescriptionAttribute("Gets or sets the maximum number of characters displayed per MRU menu item.")]
      [DefaultValueAttribute(40)]
      public int TextWidth {
         get { return textWidth; }
         set {
            // TextWidth must be > 0
            if ( value < 0 ) {
               throw new ArgumentOutOfRangeException("TextWidth cannot be less than zero");
            }
            textWidth = value;
         }
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1821</remarks>
      [CategoryAttribute("Appearance")]
      [DescriptionAttribute("Gets or sets the maximum number of MRU menu items to be displayed.")]
      [DefaultValueAttribute(10)]
      public int MaximumItems {
         get { return maximumItems; }
         set {
            // MaximumItems must be > 0
            if ( value < 0 ) {
               throw new ArgumentOutOfRangeException("MaximumItems cannot be less than zero");
            }
            maximumItems = value;

            // Update MRU menu list items buffer with new
            // maximum items size
            if ( !this.DesignMode ) {
               mruMenuListItems.MaximumItems = maximumItems;
            }
         }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1822</remarks>
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public ArrayList Filenames {
         get {
            return new ArrayList(this.mruMenuListItems.ToArray());
         }
         set {
            if ( value == null ) return;
            this.mruMenuListItems.Clear();
            this.mruMenuListItems.AddRange((string[])value.ToArray(typeof(string)));
         }
      }

      // Add a filename to the mru menu list
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1823</remarks>
      public void Add(string filename) {
         mruMenuListItems.Add(filename);
      }


      /// <summary>This public field stores ...</summary>
      /// <remarks>id : 20130604°1824</remarks>
      [CategoryAttribute("Action")]
      [DescriptionAttribute("Occurs when a MRU menu item is clicked.")]
      public event MruMenuItemClickEventHandler MruMenuItemClick;


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1845</remarks>
      protected virtual void OnMruMenuItemClick(MruMenuItemClickEventArgs e) {
         if ( MruMenuItemClick != null ) {
            MruMenuItemClick(this, e);
            // Move file to front of MRU
            this.Add(e.Filename);
         }
      }


      /// <summary>This public field stores ...</summary>
      /// <remarks>id : 20130604°1825</remarks>
      [CategoryAttribute("Action")]
      [DescriptionAttribute("Occurs when a file in the MRU Menu is missing.")]
      public event MruMenuItemFileMissingEventHandler MruMenuItemFileMissing;


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1846</remarks>
      protected virtual void OnMruMenuItemFileMissing(MruMenuItemFileMissingEventArgs e) {
         if ( MruMenuItemFileMissing != null ) {
            MruMenuItemFileMissing(this, e);
         }
      }


      // Render mru menu list with Mru items
      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°1826</remarks>
      private void RootMenu_Opening(object sender, CancelEventArgs e) {

         // Bail if an mru list menu item hasn't been set
         if ( mruListMenu == null ) return;

         // Render mru list menu items
         if ( displayMode == MruListDisplayStyle.InMenu ) {
            RenderInMenu();
         }
         else {
            RenderInSubMenu();
         }
      }


      /// <summary>This eventhandler ...</summary>
      /// <remarks>id : 20130604°1827</remarks>
      private void MruMenuItem_Click(object sender, System.EventArgs e) {
         // Get clicked MruToolStripMenuItem
         MruToolStripMenuItem mruMenuItem = (MruToolStripMenuItem)sender;
         string filename = mruMenuItem.Filename;

         // Check if file is missing and, if so, ask if it needs to
         // be deleted. If nobody's registered with the MruMenuItemFileMissing
         // event, the file won't be deleted, MruMenuItemClick will be
         // called and an exception will be raised if the file is missing.
         MruMenuItemFileMissingEventArgs args = new MruMenuItemFileMissingEventArgs(filename, false);
         if ( !File.Exists(filename) ) {
            OnMruMenuItemFileMissing(args);
            if ( args.RemoveFromMru ) {
               this.mruMenuListItems.Remove(filename);
               return;
            }
         }

         OnMruMenuItemClick(new MruMenuItemClickEventArgs(filename));
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1828</remarks>
      private void RenderInMenu() {


         // Clear existing menu items, if any
         ToolStripDropDownMenu rootMenu = (ToolStripDropDownMenu)mruListMenu.Owner;
         int mruListMenuIndex = rootMenu.Items.IndexOf(mruListMenu);
         for ( int index = rootMenu.Items.Count - 1; index > mruListMenuIndex; index-- ) {
            if ( rootMenu.Items[index] is MruToolStripMenuItem ) {
               rootMenu.Items.RemoveAt(index);
            }
         }

         // Hide mru list menu
         mruListMenu.Enabled = false;
         mruListMenu.Visible = true;

         // Render mru menu
         if ( mruMenuListItems.Count > 0 ) {
            for ( int index = 0; index < mruMenuListItems.Count; index++ ) {
               string filename = mruMenuListItems[index];
               MruToolStripMenuItem mruMenuItem = new MruToolStripMenuItem(filename, textWidth, index + 1, MruMenuItem_Click);
               rootMenu.Items.Insert(mruListMenuIndex + index + 1, mruMenuItem);
            }
            // Show mru list menu
            mruListMenu.Enabled = true;
            mruListMenu.Visible = false;
         }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1829</remarks>
      private void RenderInSubMenu() {

         // Clear existing sub-menu list items, if any
         mruListMenu.DropDownItems.Clear();
         mruListMenu.Enabled = false;

         // Render mru menu
         if ( mruMenuListItems.Count > 0 ) {
            for ( int index = 0; index < mruMenuListItems.Count; index++ ) {
               string filename = mruMenuListItems[index];
               MruToolStripMenuItem mruMenuItem = new MruToolStripMenuItem(filename, textWidth, index + 1, MruMenuItem_Click);
               mruListMenu.DropDownItems.Add(mruMenuItem);
            }
            mruListMenu.Enabled = true;
         }
      }

      // Clean up any resources being used.
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1831</remarks>
      protected override void Dispose(bool disposing) {
         if ( !disposing ) return;

         // Save component settings if specified
         if ( saveSettings ) this.SaveComponentSettings();
         base.Dispose(disposing);
      }


      #region IPersistComponentSettings Members

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1832</remarks>
      private bool saveSettings = false;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1833</remarks>
      private string settingsKey = "MruMenuManager.SettingsKey";

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1834</remarks>
      private MruMenuManagerSettings settings;


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1835</remarks>
      [Category("Behavior")]
      [Description("Specifies whether the MruMenuManager should persist user settings via IPersistComponentSettings.")]
      [DefaultValue(false)]
      public bool SaveSettings {
         get { return saveSettings; }
         set { saveSettings = value; }
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1836</remarks>
      [Category("Behavior")]
      [Description("Specifies the unique group under which MruMenuManager's settings are persisted.")]
      [DefaultValue("MruMenuManager.SettingsKey")]
      public string SettingsKey {
         get { return settingsKey; }
         set { settingsKey = value; }
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1837</remarks>
      public void LoadComponentSettings() {
         if ( this.DesignMode ) return;
         this.mruMenuListItems.Clear();
         if ( this.Settings.MruListItems == null ) return;
         this.mruMenuListItems.AddRange((string[])this.Settings.MruListItems.ToArray(typeof(string)));
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1838</remarks>
      public void SaveComponentSettings() {
         if ( this.DesignMode ) return;
         this.Settings.MruListItems = new ArrayList(this.mruMenuListItems.ToArray());
         this.Settings.Save();
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1839</remarks>
      public void ResetComponentSettings() {
         if ( this.DesignMode ) return;
         this.Settings.Reset();
         this.LoadComponentSettings();
      }


      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°1841</remarks>
      private MruMenuManagerSettings Settings {
        get {
          if ( settings == null ) {
            settings = new MruMenuManagerSettings(this, settingsKey);
          }
          return settings;
        }
      }

      #endregion
   }
}
