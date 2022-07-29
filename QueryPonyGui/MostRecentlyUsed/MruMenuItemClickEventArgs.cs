#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/MostRecentlyUsed/MruMenuItemClickEventArgs.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/MostRecentlyUsed/MruMenuItemClickEventArgs.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1711
// summary     : This file stores Genghis class 'MruMenuItemClickEventArgs' to provide ...
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
   /// <remarks>id : 20130604°1712</remarks>
   public class MruMenuItemClickEventArgs {

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1713</remarks>
      private string filename;

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1714</remarks>
      public MruMenuItemClickEventArgs(string filename) {
         this.filename = filename;
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°1715</remarks>
      public string Filename {
         get { return this.filename; }
      }
   }
}
