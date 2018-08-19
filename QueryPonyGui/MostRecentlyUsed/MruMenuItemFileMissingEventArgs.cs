#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/MostRecentlyUsed/MruMenuItemFileMissingEventArgs.cs from
//                "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1731
// summary     : This file stores Genghis class 'MruMenuItemFileMissingEventArgs' to provide ...
// license     : Custom License
// copyright   : Copyright 2002-2004 The Genghis Group http://genghis.codeplex.com
// authors     : Shawn Wildermuth and others
// status      :
// note        :
// callers     :
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace MRUSampleControlLibrary {

   /// <summary>This class provides ...</summary>
   /// <remarks>id : 20130604°1732</remarks>
   public class MruMenuItemFileMissingEventArgs {

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1733</remarks>
      private string filename;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1734</remarks>
      private bool removeFromMru;

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1735</remarks>
      public MruMenuItemFileMissingEventArgs(string filename, bool removeFromMru) {
         this.filename = filename;
         this.removeFromMru = removeFromMru;
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°1736</remarks>
      public string Filename {
         get { return this.filename; }
      }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1737</remarks>
      public bool RemoveFromMru {
         get { return this.removeFromMru; }
         set { this.removeFromMru = value; }
      }

   }
}
