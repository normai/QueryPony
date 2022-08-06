#region Fileinfo
// file        : 20130604°1541 github.com/normai/QueryPony/blob/main/QueryPonyGui/MostRecentlyUsed/MruToolStripMenuItem.cs
// origin      : "Genghis v0.8.zip" at http://genghis.codeplex.com/releases/view/4954
// summary     : This file stores Genghis class 'MruToolStripMenuItem' to provide ...
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
   /// <remarks>id : 20130604°1542</remarks>
   public class MruToolStripMenuItem : ToolStripMenuItem {

      /// <summary>This field stores ...</summary>
      /// <remarks>id : 20130604°1543</remarks>
      private string filename;

      /// <summary>This constructor ...</summary>
      /// <remarks>id : 20130604°1544</remarks>
      public MruToolStripMenuItem(string filename, int textWidth, int index, System.EventHandler click) {
         this.filename = filename;
         this.Click += click;
         this.Text = GetMruMenuItemText(filename, textWidth, index);
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130604°1545</remarks>
      public string Filename {
         get { return filename; }
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1546</remarks>
      private string GetMruMenuItemText(string filename, int textWidth, int index) {
         string formattedAccessKey = GetFormattedAccessKey(index);
         string formattedFilename = GetFormattedFilename(filename, textWidth);
         return string.Format("{0} {1}", formattedAccessKey, formattedFilename);
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1547</remarks>
      private string GetFormattedAccessKey(int index) {
         // Build numeric access key
         if ( index <= 9 ) return string.Format("&{0}", index);
         else if ( index == 10 ) return "1&0";
         else return index.ToString();
      }

      // Derived from original post by James Berry from Chris Sells's win_tech_off_topic list
      /// <summary>This method ...</summary>
      /// <remarks>id : 20130604°1548</remarks>
      private string GetFormattedFilename(string filename, int textWidth) {
         // We will begin by taking the string and splitting it apart into an array
         // Check if we are within the max length then return the whole string
         if ( filename.Length <= textWidth ) return filename;

         // Split the string into an array using the \ as a delimiter
         char[] seperator = { '\\' };
         string[] pathBits;
         pathBits = filename.Split(seperator);

         // The first value of the array is taken in case we need to create the string
         StringBuilder sb = new StringBuilder();
         int length = sb.Length;
         int beginLength = pathBits[0].Length + 3;
         bool addHeader = false;
         string pathItem;
         int pathItemLength;

         // Now we loop backwards through the string
         for ( int pathItemIndex = pathBits.Length - 1; pathItemIndex > 0; pathItemIndex-- ) {
            pathItem = '\\' + pathBits[pathItemIndex];
            pathItemLength = pathItem.Length;

            // Check if adding the current item does not increase the length of the
            // max string
            if ( length + pathItemLength <= textWidth ) {
               // In this case we can afford to add the item
               sb.Insert(0, pathItem);
               length += pathItemLength;
            }
            else break;

            // Check if there is room to add the header and if so then reserve it by
            // incrementing the length
            if ( (addHeader == false) && (length + beginLength <= textWidth) ) {
               addHeader = true;
               length += beginLength;
            }
         }

         // It is possible that the last value in the array itself was long
         // In such case simply use the substring of the last value
         if ( sb.Length == 0 ) return pathBits[pathBits.Length - 1].Substring(0, textWidth);

         // Add the header if the bool is true
         if ( addHeader == true ) sb.Insert(0, pathBits[0] + "\\...");
         return sb.ToString();
      }
   }
}
