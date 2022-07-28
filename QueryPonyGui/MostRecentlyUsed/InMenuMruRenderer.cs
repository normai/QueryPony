#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/MostRecentlyUsed/IMenuMruRenderer.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/MostRecentlyUsed/IMenuMruRenderer.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1621
// summary     : This file stores Genghis class 'InMenuMruMenuListRender' to provide ...
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
   /// <remarks>id : 20130604°1622</remarks>
   public class InMenuMruMenuListRender : IMruMenuListRenderer {

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1623</remarks>
      public void Render(ToolStripMenuItem mruListMenu, MruMenuListItems mruMenuListItems, int textWidth, EventHandler mruMenuItem_Click)
      {
         // Clear existing menu items, if any
         ToolStripDropDownMenu rootMenu = (ToolStripDropDownMenu)mruListMenu.Owner;

         int mruListMenuIndex = rootMenu.Items.IndexOf(mruListMenu);

         for ( int index = rootMenu.Items.Count - 1; index > mruListMenuIndex; index-- ) {
            if ( rootMenu.Items[index] is MruToolStripMenuItem ) {
               rootMenu.Items.RemoveAt(index);
            }
         }

         mruListMenu.Enabled = false;
         mruListMenu.Visible = true;

         // Fill MRU menu
         if ( mruMenuListItems.Count > 0 ) {
            for ( int index = 0; index < mruMenuListItems.Count; index++ ) {
               string filename = mruMenuListItems[index];
               MruToolStripMenuItem mruMenuItem = new MruToolStripMenuItem(filename, textWidth, index + 1, mruMenuItem_Click);
               rootMenu.Items.Insert(mruListMenuIndex + index + 1, mruMenuItem);
            }
            mruListMenu.Enabled = true;
            mruListMenu.Visible = false;
         }
      }
   }
}
