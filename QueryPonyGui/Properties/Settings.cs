#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Properties/Settings.cs
// id          : 20130604°1311
// summary     : This file stores partial class 'Settings' to process the Settings Events.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

namespace QueryPonyGui.Properties
{

   /// <summary>This partial class processes the Settings Events.</summary>
   /// <remarks>
   /// id : 20130604°1312
   /// explanation : This class allows to handle specific events on the settings class.
   ///    - The SettingChanging event is raised before a setting's value is changed.
   ///    - The PropertyChanged event is raised after a setting's value is changed.
   ///    - The SettingsLoaded event is raised after the setting values are loaded.
   ///    - The SettingsSaving event is raised before the setting values are saved.
   /// </remarks>
   ////internal sealed partial class Settings
   public sealed partial class Settings // (20130901°0731) switched internal to public
   {

      /// <summary>This constructor creates the Settings object.</summary>
      /// <remarks>id : 20130604°1313</remarks>
      public Settings() {

         // // To add event handlers for saving and changing settings, uncomment the lines below:
         //
         this.SettingChanging += this.SettingChangingEventHandler;
         //
         this.SettingsSaving += this.SettingsSavingEventHandler;
         //
      }


      /// <summary>This eventhandler processes the SettingChanging event.</summary>
      /// <remarks>id : 20130604°1314</remarks>
      private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
      {
         // Add code to handle the SettingChangingEvent event here.
         System.Threading.Thread.Sleep(1); // (breakpoint 20130621°0942)
      }


      /// <summary>This eventhandler processes the SettingsSaving event.</summary>
      /// <remarks>id : 20130604°1315</remarks>
      private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
      {
         // Add code to handle the SettingsSaving event here.
         System.Threading.Thread.Sleep(1); // (breakpoint 20130621°0943)
      }
   }
}
