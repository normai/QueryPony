using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryPonyCmd
{
   /// <summary>
   /// This class hosts the dispatching loop
   /// </summary>
   /// <remarks>
   /// id : class 20210523°1411
   /// </remarks>
   class Dispatcher
   {
      /// <summary>
      /// This static method performs the dispatcher loop
      /// </summary>
      /// <remarks>id : method 20200522°0153</remarks>
      /// <param name="sCmdLin">Optional autostart commandline</param>
      /// <returns>Errorcode, 0 is success, >0 is error, <0 is avoided</returns>
      internal static int Loop(Queue<string> lCmdLins = null)
      {
         string sCmdLin = "";
         bool bContinue = true;
         while (bContinue)
         {
            // Process autostart list
            if (lCmdLins.Count > 0)
            {
               sCmdLin = lCmdLins.Dequeue();
            }

            // Display help line
            ////Console.WriteLine("Commands: a/asmbls = List loaded assemblies, t/test = Test, q/quit = Quit;");
            Console.WriteLine("Commands: h = Help, q = Quit;");

            if (String.IsNullOrWhiteSpace(sCmdLin))
            {
               sCmdLin = Console.ReadLine();
            }
            List<string> tokens = new List<string>(sCmdLin.Split(' '));
            string sCmd = tokens[0];

            switch (sCmd)
            {
               case "a":
               case "asms":
                  Console.WriteLine("All currently loaded assemblies:");
                  int i23 = 1;
                  foreach (string s in QueryPonyLib.Utils.listLoadedAssemblies())
                  {
                     Console.WriteLine(" - " + i23.ToString() + " : " + IOBus.Utils.Strings.Ram(s,88));
                     i23++;
                  }
                  break;
               case "h":
               case "help":
                  string s27 = "Available commands:"
                             + "\n" + " - a/asms   : List loade assemblies"
                             + "\n" + " - h/help   : Help (this list)"
                             + "\n" + " - t1/test1 : Self test one"
                             + "\n" + " - t2/test2 : Self test two"
                             + "\n" + " - q/quit   : Quit"
                              ;
                  Console.WriteLine(s27);
                  break;
               case "t1":
               case "test1":
                  int i24 = Selftest.SelfTestOne();
                  Console.WriteLine("SelfTestOne result = " + (i24 == 0 ? "Success" : "Fail"));
                  break;
               case "t2":
               case "test2":
                  int i25 = Selftest.SelfTestTwo();
                  Console.WriteLine("SelfTestTwo result = " + (i25 == 0 ? "Success" : "Fail"));
                  break;
               case "q":
               case "quit":
                  Console.WriteLine("*** QueryPonyCmd finished. ***");
                  bContinue = false;
                  break;
               default:
                  Console.WriteLine("Unknown command \"" + sCmd + "\"");
                  break;
            }
            sCmdLin = "";
         }

         return 0;
      }
   }
}
