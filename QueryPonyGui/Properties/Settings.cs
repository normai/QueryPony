#region Fileinfo
// file        : 20130604°1311 /QueryPony/QueryPonyGui/Properties/Settings.cs
// summary     : This file stores partial class 'Settings' to process the Settings Events.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

namespace QueryPonyGui.Properties
{

   /// <summary>This partial class processes the Settings Events</summary>
   /// <remarks>
   /// id : 20130604°1312
   /// explanation : This class allows to handle specific events on the settings class.
   ///    - The SettingChanging event is raised before a setting's value is changed.
   ///    - The PropertyChanged event is raised after a setting's value is changed.
   ///    - The SettingsLoaded event is raised after the setting values are loaded.
   ///    - The SettingsSaving event is raised before the setting values are saved.
   /// </remarks>
   public sealed partial class Settings                                        // Switched internal to public — Must be done at all 3 partial locations [chg 20130901°0731]
   {

      /// <summary>This constructor creates the Settings object</summary>
      /// <remarks>id : 20130604°1313</remarks>
      public Settings() {

         // // To add event handlers for saving and changing settings, uncomment the lines below:
         //
         this.SettingChanging += this.SettingChangingEventHandler;
         //
         this.SettingsSaving += this.SettingsSavingEventHandler;
         //
      }

      /// <summary>This eventhandler processes the SettingChanging event</summary>
      /// <remarks>id : 20130604°1314</remarks>
      private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
      {
         // Add code to handle the SettingChangingEvent event here.
         System.Threading.Thread.Sleep(1); // (breakpoint 20130621°0942)
      }

      /// <summary>This eventhandler processes the SettingsSaving event</summary>
      /// <remarks>id : 20130604°1315</remarks>
      private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // Add code to handle the SettingsSaving event here.
         System.Threading.Thread.Sleep(1); // (breakpoint 20130621°0943)
      }
   }
}
