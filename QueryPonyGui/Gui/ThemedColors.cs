#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/Gui/ThemedColors.cs
//                + https://github.com/normai/QueryPony/blob/master/QueryPonyGui/Gui/ThemedColors.cs
// id          : 20130723°1231
// summary     : This file stores class 'ThemedColors' to supplement the CustomTabControl class.
// license     : The Code Project Open License (CPOL) 1.02
// copyright   :
// authors     : Mark Jackson (The Man from U.N.C.L.E.)
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        : File copied/modified from article (ref 20130723°1045)
//               'Painting Your Own Tabs' by The Man from U.N.C.L.E., 3 Jul 2010,
//               http://www.codeproject.com/Articles/13305/Painting-Your-Own-Tabs
// note        :
#endregion Fileinfo

using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

// id : 20130723°1202
namespace CSharpCustomTabControl
{
   /// <summary>This class ...</summary>
   /// <remarks>id : 20130723°1232</remarks>
   public class ThemedColors
   {
      #region "Variables and Constants"

      /// <summary>This const "NormalColor" tells a style name.</summary>
      /// <remarks>id : 20130723°1233</remarks>
      private const string NormalColor = "NormalColor";

      /// <summary>This const "HomeStead" tells a style name.</summary>
      /// <remarks>id : 20130723°1234</remarks>
      private const string HomeStead = "HomeStead";

      /// <summary>This const 'Metallic' tells a style name.</summary>
      /// <remarks>id : 20130723°1235</remarks>
      private const string Metallic = "Metallic";

      /// <summary>This const "NoTheme" tells a style name.</summary>
      /// <remarks>id : 20130723°1236</remarks>
      private const string NoTheme = "NoTheme";

      /// <summary>This static field stores ...</summary>
      /// <remarks>id : 20130723°1237</remarks>
      private static Color[] _toolBorder;

      #endregion "Variables and Constants"

      #region "Properties"

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130723°1238</remarks>
      public static int CurrentThemeIndex
      {
         get {
            return ThemedColors.GetCurrentThemeIndex();
         }
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130723°1239</remarks>
      public static string CurrentThemeName
      {
         get {
            return ThemedColors.GetCurrentThemeName();
         }
      }

      /// <summary>This property gets ...</summary>
      /// <remarks>id : 20130723°1240</remarks>
      public static Color ToolBorder
      {
         get {
            return ThemedColors._toolBorder[ThemedColors.CurrentThemeIndex];
         }
      }

      #endregion "Properties"

      #region "Constructors"

      /// <summary>This constructor creates a new ThemedColors object.</summary>
      /// <remarks>id : 20130723°1241</remarks>
      private ThemedColors()
      {
      }

      /// <summary>This constructor creates a new ThemedColors (singleton?) object.</summary>
      /// <remarks>id : 20130723°1242</remarks>
      static ThemedColors()
      {
         Color[] colorArray1;
         colorArray1 = new Color[] {Color.FromArgb(127, 157, 185), Color.FromArgb(164, 185, 127), Color.FromArgb(165, 172, 178), Color.FromArgb(132, 130, 132)};
         ThemedColors._toolBorder = colorArray1;
      }

      #endregion "Constructors"

      /// <summary>This metho ...</summary>
      /// <remarks>id : 20130723°1243</remarks>
      private static int GetCurrentThemeIndex()
      {
         int theme = (int)ColorScheme.NoTheme;
         if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser && Application.RenderWithVisualStyles)
         {
            switch (VisualStyleInformation.ColorScheme) {
               case NormalColor:
                  theme = (int)ColorScheme.NormalColor;
                  break;
               case HomeStead:
                  theme = (int)ColorScheme.HomeStead;
                  break;
               case Metallic:
                  theme = (int)ColorScheme.Metallic;
                  break;
               default:
                  theme = (int)ColorScheme.NoTheme;
                  break;
            }
         }
         return theme;
      }

      /// <summary>This method ...</summary>
      /// <remarks>id : 20130723°1244</remarks>
      private static string GetCurrentThemeName()
      {
         string theme = NoTheme;
         if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser && Application.RenderWithVisualStyles)
         {
            theme = VisualStyleInformation.ColorScheme;
         }
         return theme;
      }

      /// <summary>This enum tells the available ThemedColors styles.</summary>
      /// <remarks>id : 201307°1245</remarks>
      public enum ColorScheme
      {
         NormalColor = 0,
         HomeStead = 1,
         Metallic = 2,
         NoTheme = 3
      }
   }
}
