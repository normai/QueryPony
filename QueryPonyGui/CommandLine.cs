#region Fileinfo
// file        : 20130604°0041 /QueryPony/QueryPonyGui/CommandLine.cs
// summary     : Class 'CommandLineParams' processes commandline parameters
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace QueryPonyGui
{
   /// <summary>This class processes commandline parameters</summary>
   /// <remarks>id : class 20130604°0042</remarks>
   internal class CommandLineParams : IEnumerable
   {
      /// <summary>This field stores ...</summary>
      /// <remarks>id : field 20130604°0043</remarks>
      private readonly StringDictionary Parameters = new StringDictionary ();

      /// <summary>This constructor ...</summary>
      /// <remarks>id : ctor 20130604°0044</remarks>
      /// <param name="Args">The commandline arguments array</param>
      public CommandLineParams (string[] Args)
      {
         Regex Spliter = new Regex (@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
         Regex Remover = new Regex (@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
         string Parameter = null;
         string[] Parts;

         // Note : Valid parameter forms are:
         //    - {-,/,--}param{ ,=,:}((",')value(",'))
         // examples :
         //    -param1 value1
         //    --param2 /param3:"Test-:-work"
         //    /param4=happy -param5
         //    '--=nice=--'
         foreach (string Txt in Args)
         {
            // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
            Parts = Spliter.Split (Txt, 3);

            switch (Parts.Length)
            {
               case 1:

                  // Found a value (for the last parameter found (space separator))
                  if (Parameter != null)
                  {
                     if (! Parameters.ContainsKey (Parameter))
                     {
                        Parts[0] = Remover.Replace (Parts[0], "$1");
                        Parameters.Add (Parameter, Parts[0]);
                     }
                     Parameter = null;
                  }
                  break;

               // else Error: no parameter waiting for a value (skipped)

               case 2:

                  // Found just a parameter, the last parameter is
                  //  still waiting, with no value, set it to true
                  if (Parameter != null)
                  {
                     if (! Parameters.ContainsKey(Parameter))
                     {
                        Parameters.Add(Parameter, "true");
                     }
                  }

                  Parameter = Parts[1];
                  break;

               case 3:

                  // Parameter with enclosed value, the last parameter is
                  //  still waiting, with no value, set it to true
                  if (Parameter != null)
                  {
                     if (! Parameters.ContainsKey(Parameter))
                     {
                        Parameters.Add(Parameter, "true");
                     }
                  }

                  Parameter = Parts[1];

                  // Remove possible enclosing characters (",')
                  if (! Parameters.ContainsKey (Parameter))
                  {
                     Parts[2] = Remover.Replace (Parts[2], "$1");
                     Parameters.Add (Parameter, Parts[2]);
                  }

                  Parameter = null;
                  break;
            }
         }

         // In case a parameter is still waiting
         if (Parameter != null)
         {
            if (! Parameters.ContainsKey(Parameter))
            {
               Parameters.Add(Parameter, "true");
            }
         }
      }

      /// <summary>This method provides the parameter enumerator. This exists to satisfy the IEnumerable interface</summary>
      /// <remarks>id : method 20130604°0045</remarks>
      public IEnumerator GetEnumerator ()
      {
         return Parameters.GetEnumerator ();
      }

      /// <summary>This property provides a read-only indexer on the commandline parameters</summary>
      /// <remarks>id : property 20130604°0046</remarks>
      public string this [string key]
      {
         get { return Parameters[key]; }
      }

      /// <summary>This method tells whether a given parameter key exists or not</summary>
      /// <remarks>id : method 20130604°0047</remarks>
      public bool ContainsKey(string value)
      {
          return Parameters.ContainsKey(value);
      }
   }
}
