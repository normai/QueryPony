#region Fileinfo
// file        : 20130604°0031 /QueryPony/QueryPonyLib/DbApi/DbClientFactory.cs
// summary     : This file stores class 'DbClientFactory' to provide
//                methods to open the connection to a Database.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyLib
{
   /// <summary>This class provides methods to open the connection to a Database</summary>
   /// <remarks>
   /// id : 20130604°0032
   /// note : Access modifier set 'public' to make it available for other projects (20130604°1427)
   /// </remarks>
   public static class DbClientFactory
   {
      /// <summary>This method validates the given connection settings</summary>
      /// <remarks>
      /// id : 20130604°0033
      /// todo : This should not be in the factory since that is against Objcet Oriented
      ///         design. I am not sure where to put it yet. (todo 20130604°003302)
      /// </remarks>
      /// <param name="conSettings">The connection settings to validate</param>
      /// <returns>The wanted flag whether the connections settings are valid or not</returns>
      public static bool ValidateSettings(ConnSettingsLib conSettings)
      {
         bool bRet = false; // as long we have not yet implemented that validation
         string sErr = "";

         switch (conSettings.Type)
         {
            case ConnSettingsLib.ConnectionType.Couch:
               bRet = true;
               if (String.IsNullOrEmpty(conSettings.DatabaseServerUrl))
               {
                  // Todo 20130723°1411 : Possibly implement console output.
                  sErr = "Field 'ServerAddress' must not be empty.";
                  bRet = false;
               }
               if (String.IsNullOrEmpty(conSettings.DatabaseName))
               {
                  // Todo 20130723°1411`02 : Possibly implement console output.
                  sErr = "Field 'DatabaseName' must not be empty.";
                  bRet = false;
               }
               break;

            case ConnSettingsLib.ConnectionType.Mssql:
               bRet = conSettings.DatabaseServerUrl != null && conSettings.DatabaseServerUrl.Length > 0;
               break;

            case ConnSettingsLib.ConnectionType.Mysql:
               bRet = (! (String.IsNullOrEmpty(conSettings.DatabaseServerUrl) || (conSettings.DatabaseName == null)));
               break;

            case ConnSettingsLib.ConnectionType.Odbc:
               bRet = conSettings.DatabaseConnectionstring != null && conSettings.DatabaseConnectionstring.Length > 0;
               break;

            case ConnSettingsLib.ConnectionType.OleDb:
               bRet = conSettings.DatabaseConnectionstring != null && conSettings.DatabaseConnectionstring.Length > 0;
               break;

            case ConnSettingsLib.ConnectionType.Oracle:
               bRet = conSettings.DatabaseName != null && conSettings.DatabaseName.Length > 0; // (20130719°0914)
               break;

            case ConnSettingsLib.ConnectionType.Pgsql:
               bRet = conSettings.DatabaseServerUrl != null && conSettings.DatabaseName.Length > 0;
               break;

            case ConnSettingsLib.ConnectionType.Sqlite:

               // [seq 20130703°1512] See issue 20130703°1511
               string sFileFullname = Utils.CombineServerAndDatabaseToFullfilename(conSettings.DatabaseServerUrl, conSettings.DatabaseName);
               bRet = System.IO.File.Exists(sFileFullname);
               if (! bRet)
               {
                  string sMsg = "File does not exist: " + sFileFullname + Glb.sCr + "(Error 20130703°1513)";
                  System.Windows.Forms.MessageBox.Show(sMsg);
               }
               break;

            case ConnSettingsLib.ConnectionType.NoType:

               // styleguide : Don't use tabs in exception messages, they appear ugly
               //    in the system message box. [styleguide 20130624°1031]
               sErr = Glb.Errors.TheoreticallyNotPossible + ":"
                     + Glb.sCr + "Connection Type missing."
                      + Glb.sCr + "[Error 20130624°1021]"
                       ;
               throw new ArgumentOutOfRangeException(sErr);

            default:
               string sParamName = "conSettings.Type";
               throw new ArgumentOutOfRangeException(sParamName);
         }

         return bRet;
      }

      /// <summary>This method creates and returns a DbClient object for the given connection settings</summary>
      /// <remarks>id : 20130604°0034</remarks>
      /// <param name="conSettings">The connection settings for which a DbClient is wanted</param>
      /// <returns>The wanted DbClient matching the given connection settings</returns>
      public static DbClient GetDbClient(ConnSettingsLib conSettings)
      {
         string sErr = "";
         DbClient client = null;

         switch (conSettings.Type)                                             // Breakpoint [brkpt 20140126°1631]
         {
            case ConnSettingsLib.ConnectionType.Couch:
               client = new CouchDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Mssql:
               client = new MssqlDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Mysql:
               client = new MysqlDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Odbc:
               client = new OdbcClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.OleDb:
               client = new OledbDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Oracle:
               client = new OracleDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Pgsql:
               client = new PgsqlDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.Sqlite:
               client = new SqliteDbClient(conSettings);
               break;

            case ConnSettingsLib.ConnectionType.NoType:
               sErr = Glb.Errors.TheoreticallyNotPossible
                     + Glb.sCr + "Connection Type 'No Type'."
                      + Glb.sBlnk + "(Error 20130624°1112)"
                       ;
               throw new ArgumentOutOfRangeException(sErr);

            default:
               sErr = Glb.Errors.TheoreticallyNotPossible
                     + Glb.sCr + "Connection Type invalid."
                      + Glb.sBlnk + "(Error 20130624°1111)"
                       ;
               throw new ArgumentOutOfRangeException(sErr);
         }

         return client;
      }

      /// <summary>This method retrieves a DbBrowser object matching the given DbClient object</summary>
      /// <remarks>id : 20130604°0035</remarks>
      /// <param name="client">The DbClient object for which the DbBrowser is wanted</param>
      /// <returns>The wanted DbBrowser object matching the given DbClient type.</returns>
      public static IDbBrowser GetBrowser(DbClient client)
      {
         IDbBrowser ibRet = null;

         if ((client as CouchDbClient) != null)
         {
            ibRet = new CouchDbBrowser(client);
         }
         if ((client as MssqlDbClient) != null)
         {
            ibRet = new MssqlDbBrowser(client);
         }
         if ((client as MysqlDbClient) != null)
         {
            ibRet = new MysqlDbBrowser(client);
         }
         if ((client as OdbcClient) != null)
         {
            ibRet = new OdbcDbBrowser(client);
         }
         if ((client as OledbDbClient) != null)
         {
            ibRet = new OleDbBrowser(client);
         }
         if ((client as OracleDbClient) != null)
         {
            ibRet = new OracleDbBrowser(client);
         }
         if ((client as PgsqlDbClient) != null)
         {
            ibRet = new PgsqlDbBrowser(client);
         }
         if ((client as SqliteDbClient) != null)
         {
            ibRet = new SqliteDbBrowser(client);
         }

         if (ibRet == null)
         {
            throw new ApplicationException("Unknown connection type");
         }

         return ibRet;
      }
   }
}
