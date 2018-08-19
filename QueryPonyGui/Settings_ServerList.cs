#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/Settings_ServerList.cs
// id          : 20130604°1921
// summary     : This file stores one part of partial class 'Settings' to provide the ServerList custom type Setting.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo


namespace QueryPonyGui.Properties
{
   /// <summary>This partial class 'Settings' part provides the ServerList custom type Setting.</summary>
   /// <remarks>id : 20130604°1922</remarks>
   ////internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
   public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase // (20130901°073102) switched internal to public
   {

      // experimentally shutdown (20130828°1611)
      // note : (1) After each edit in the Settings design view, the designer creates
      //  a parallel property in Settings.Designer.cs. (2) This then causes compiler
      //  error "The type 'QueryPonyGui.Properties.Settings' already contains a
      //  definition for 'ServerList'". (3) Now let's experimentally shutdown this
      //  property here, and see, how the automatically created one in Settings.Designer.cs
      //  suits our purpose. (4) The problem with the automatically created one was
      //  the initial creation, when the designer did not offer the wanted type
      //  'ServerList'. (5) But now, after 'ServerList' exists, maybe it is fine
      //  there without this manual file here.
      /*

      /// <summary>This Settings property gets/sets the ServerList custom type setting.</summary>
      /// <remarks>
      /// id : 20130604°1923
      /// note : (20130808°1411) This sequence was refreshed by generating it again
      ///    in Settings.Designer.cs and then shift it here. (This was while debugging
      ///    issue 20130731°0131, and it did not help against that.)
      /// note : The generator put an attribute '[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]'
      ///    in front of this property. MSDN tells about it: 'Identifies a type or member
      ///    that is not part of the user code for an application.'. I consider this partial
      ///    class being part of the user code, thus shutdown the attribute.
      /// </remarks>
      [global::System.Configuration.UserScopedSettingAttribute()]
      //[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
      public global::QueryPonyGui.ServerList ServerList
      {
         get
         {
            return ((global::QueryPonyGui.ServerList)(this["ServerList"]));
         }
         set
         {
            this["ServerList"] = value;
         }
      }
      */

   }
}
