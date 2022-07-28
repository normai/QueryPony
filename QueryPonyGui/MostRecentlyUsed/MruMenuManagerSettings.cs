#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/MostRecentlyUsed/MruMenuMangerSettings.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/MostRecentlyUsed/MruMenuMangerSettings.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1851
// summary     : This file stores Genghis class 'MruMenuManagerSettings' to provide ...
// license     : Custom License
// copyright   : Copyright 2002-2004 The Genghis Group http://genghis.codeplex.com
// authors     : Shawn Wildermuth and others
// status      :
// note        :
// callers     :
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;

namespace MRUSampleControlLibrary {

   /// <summary>This class provides ...</summary>
   /// <remarks>id : 20130604°1852</remarks>
   class MruMenuManagerSettings : ApplicationSettingsBase {

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1853</remarks>
      public MruMenuManagerSettings(IComponent owner, string settingsKey) : base(owner, settingsKey) { }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1854</remarks>
      [UserScopedSetting]
      public ArrayList MruListItems {
         get { return (ArrayList)this["MruListItems"]; }
         set { this["MruListItems"] = value; }
      }
   }
}
