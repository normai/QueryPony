#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/MostRecentlyUsed/IMruMenuListRenderer.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/MostRecentlyUsed/IMruMenuListRenderer.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1611
// summary     : This file stores Genghis interface 'IMruMenuListRenderer' to provide ...
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
using System.Windows.Forms;

/// <summary>This class ...</summary>
/// <remarks>id : 20130604°1612</remarks>
namespace MRUSampleControlLibrary {

  /// <summary>This interface ...</summary>
  /// <remarks>id : 20130604°1613</remarks>
  internal interface IMruMenuListRenderer {

    /// <summary>This method ...</summary>
    /// <remarks>id : 20130604°1614</remarks>
    void Render(ToolStripMenuItem mruListMenu, MruMenuListItems mruMenuListItems, int textWidth, EventHandler mruMenuItem_Click);

  }
}
