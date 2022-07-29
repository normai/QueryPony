#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/MostRecentlyUsed/InSubMenuMruRenderer.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/MostRecentlyUsed/InSubMenuMruRenderer.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1631
// summary     : This file stores Genghis class 'InSubMenuMruMenuListRender' to provide ...
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

namespace MRUSampleControlLibrary {

   /// <summary>This class provides ...</summary>
   /// <remarks>id : 20130604°1632</remarks>
   public class InSubMenuMruMenuListRender : IMruMenuListRenderer {

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1633</remarks>
      public void Render(ToolStripMenuItem mruListMenu, MruMenuListItems mruMenuListItems, int textWidth, EventHandler mruMenuItem_Click) {

         // Clear existing sub-menu list items, if any
         mruListMenu.DropDownItems.Clear();
         mruListMenu.Enabled = false;

         // Fill mru menu
         if ( mruMenuListItems.Count > 0 ) {
            for ( int index = 0; index < mruMenuListItems.Count; index++ ) {
               string filename = mruMenuListItems[index];
               MruToolStripMenuItem mruMenuItem = new MruToolStripMenuItem(filename, textWidth, index + 1, mruMenuItem_Click);
               mruListMenu.DropDownItems.Add(mruMenuItem);
            }
            mruListMenu.Enabled = true;
         }
      }
   }
}
