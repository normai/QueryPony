#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/IOBus/IOBus.cs
// id          : 20130819°0851 (20130102°1712 20121215°2101 20111105°1801)
// summary     : This file stores class 'IOBus' to constitute glue
//                for I/O delegate mechanism between the engine and the UI.
// license     : GNU AGPL v3
// copyright   : © 2011 - 2018 by Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      :
// note        : IOBus serves/connects two types of project:
//               - the callers who have concrete I/O facilities
//               - the calleés who want some I/O channel
// note        :
// callers     :
#endregion

using System;
using System.Windows.Threading; // for DispatcherObject

namespace IOBus
{

   /// <summary>This definition defines the cross class delegate for writing one Line to output device.</summary>
   /// <remarks>
   /// id : 20130819°0852 (20110921°1422)
   /// note : The 'cross class trick' is just to place this definition(s) outside of any class.
   /// </remarks>
   /// <param name="sMsg">The string to be output</param>
   /// <returns>Delegate void</returns>
   public delegate void IOBus_OutputLine(string sMsg);


   /// <summary>This definition defines the cross class delegate definition for reading a keystroke from input device.</summary>
   /// <remarks>id : 20130819°0853 (20110921°1423)</remarks>
   /// <param name="bWait">Wait flag</param>
   /// <returns>Delegate void</returns>
   public delegate string IOBus_InputKeypress(bool bWait);


   /// <summary>This definition defines the cross class delegate definition for reading a line.</summary>
   /// <remarks>id : 20130819°0854 (20111109°1021)</remarks>
   /// <returns>Delegate string</returns>
   public delegate string IOBus_InputLine();


   /// <summary>This definition defines the delegate which signals the end of a Backgroundworker.</summary>
   /// <remarks>id : 20130819°0856 (20111004°1051)</remarks>
   /// <returns>Delegate void</returns>
   public delegate void IOBus_Workfinished();


   /// <summary>This enum defines the meaning of signals sent from client to host</summary>
   /// <remarks>id : 20130921°2011</remarks>
   public enum SignalToHost
   {
      /// <summary>This enum value tells the host it shall refresh it's display</summary>
      /// <remarks>id : 20130921°2012</remarks>
      RefreshDisplay,

      /// <summary>This enum value tells that the splashscreen shall be opened</summary>
      /// <remarks>id : 20130921°2013</remarks>
      SplashOpen,

      /// <summary>This enum value tells that the splashscreen shall be closed</summary>
      /// <remarks>id : 20130921°2014</remarks>
      SplashClose
   }


   /// <summary>This definition defines the delegate to send some signal from client module to it's host.</summary>
   /// <remarks>id : 20130907°0531</remarks>
   /// <returns>Delegate void</returns>
   ////public delegate void IOBus_SendSignalToHost(string sSignal);
   ////public delegate void IOBus_SendSignalToHost(SignalToHost sig, string sSignal);
   public delegate bool IOBus_SendSignalToHost(SignalToHost sig, string sSignal);


   /// <summary>This class provides an invoke extension in all DispatcherObject controls.</summary>
   /// <remarks>
   /// id : 20130819°0857 (20111001°1951)
   /// ref : 'Reiff: Access control from other thread' (20111001°1941)
   /// todo : Explain why exactly this is useful.
   /// </remarks>
   public static class IOBus_ISynchronizeInvokeExtensions
   {

      /// <summary>This method attaches the InvokeEx() extension into all DispatcherObject controls.</summary>
      /// <remarks>
      /// id : 20130819°0858 (2011100°1952)
      /// note : Parameter 'xThis' was originally named '@this', but I disliked the '@' in an identifyer.
      /// </remarks>
      /// <param name="xThis">Formal parameter to mark method as extension method</param>
      /// <param name="action">The real parameter, a lambda expression</param>
      public static void InvokeEx<T>(this T xThis, Action<T> action)
           where T : DispatcherObject
      {
         xThis.Dispatcher.Invoke(action, new object[] { xThis } );
      }

   }
}
