#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyGui/MostRecentlyUsed/MruMenuListItems.cs from
//                "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// id          : 20130604°1751
// summary     : This file stores Genghis class 'MruMenuListItems' to provide ...
// license     : Custom License
// copyright   : Copyright 2002-2004 The Genghis Group http://genghis.codeplex.com
// authors     : Shawn Wildermuth and others
// status      :
// note        :
// callers     :
#endregion

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace MRUSampleControlLibrary {

   /// <summary>This class provides ...</summary>
   /// <remarks>id : 20130604°1752</remarks>
   public class MruMenuListItems : List<string> {

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1753</remarks>
      private int maximumItems;


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1754</remarks>
      public MruMenuListItems(int maximumItems) {
         this.maximumItems = maximumItems;
      }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130604°1755</remarks>
      public int MaximumItems {
         get { return maximumItems; }
         set {
            maximumItems = value;

           // Remove extra if more than capacity
           TrimToMaximumItems();
        }
      }


      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1756</remarks>
      public new int Add(string value) {
         // Get the index of this value
         int valueIndex = this.IndexOf(value);

         // Don't do anything if this file is the first item
         if ( valueIndex == 0 ) return -1;

         // Move file to top if already in the list, unless it's the first item
         if ( valueIndex > 0 ) this.RemoveAt(valueIndex);
         this.Insert(0, value);

         // Remove extra if more than capacity
         TrimToMaximumItems();

         return 0;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1757</remarks>
      private void TrimToMaximumItems() {
         if ( this.Count > maximumItems ) {
            for ( int i = this.Count - 1; i >= maximumItems; i-- ) {
               this.RemoveAt(maximumItems);
            }
         }
      }

  }
}
