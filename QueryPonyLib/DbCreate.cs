#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/DbApi/DbCreate.cs
// id          : 20130818°1611
// summary     : This file stores class DbCreate to create a new database for the given connection settings.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 by Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        :
// callers     :
#endregion Fileinfo

using System;

namespace QueryPonyLib
{

   /// <summary>This class creates a new database for the given connection settings.</summary>
   /// <remarks>
   /// id : 20130818°1612
   /// note : I made this a static class because a database creation is a singular
   ///    event, not an object you want hold for continued access.
   /// </remarks>
   public static class DbCreate
   {

      /// <summary>This method creates a database after the given connection settings.</summary>
      /// <remarks>id : 20130818°1613</remarks>
      /// <param name="settings">The connection settings for the wanted database</param>
      /// <returns>The newly created database client or null</returns>
      public static DbClient CreateDatabase (ConnSettingsLib cs)
      {
         string s = "";

         // provisory condition
         if (cs.Type != ConnSettingsLib.ConnectionType.Sqlite)
         {
            s = EnumExtensions.Description((QueryPonyLib.ConnSettingsLib.ConnectionType)cs.Type);
            string s2 = EnumExtensions.Description(ConnSettingsLib.ConnectionType.Sqlite);
            s = "Sorry, cloning to database type '" + s + "' is not (yet) supported (" + IOBus.Gb.Bricks.Cr + "only to '" + s2 + ").";
            IOBus.Dialogs.dialogOk(s);
            return null;
         }

         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         // issue 20130820°2123 'IOException while cloning'
         // Below line 'createSqlite(cs)' throws IOException with message "The process cannot
         //  access the file 'C:\Documents and Settings\Frank\Local Settings\Application Data\
         //  www.trilo.de\QueryPony.exe_Url_euqpzyfib35yvw55i13etxckcivppvbm\0.3.2.271\
         //  System.Data.SQLite.dll' because it is being used by another process."
         //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         // the actual database-specific job
         // [seq 20131201.0821] # wrapped line in try loop for debugging
         DbClient dbclient = null;
         try
         {
            dbclient = createSqlite(cs);
         }
         catch (Exception ex)
         {
            s = ex.Message;
            IOBus.Dialogs.dialogErr(s);
         }

         return dbclient;
      }


      /// <summary>This method creates a new SQLite database.</summary>
      /// <remarks>
      /// id : 20130818°1614
      /// ref : 20130818°1553 'Tigran: SQLite'
      /// </remarks>
      /// <param name="cs">The connection settings to create the database</param>
      /// <returns>The wanted newly created DbClient object</returns>
      private static DbClient createSqlite(ConnSettingsLib csLib)
      {
         DbClient dbclient = null;
         string s = "";

         // does the file already exist?
         string sFile = Utils.CombineServerAndDatabaseToFullfilename(csLib.DatabaseServerAddress, csLib.DatabaseName);
         if (System.IO.File.Exists(sFile))
         {
            s = "File does already exist:" + IOBus.Gb.Bricks.CrCr + "   " + sFile + IOBus.Gb.Bricks.CrCr + "Delete file?";
            bool b = IOBus.Dialogs.dialogOkCancel(s);
            if (b)
            {
               System.IO.File.Delete(sFile);
            }
            else
            {
               return null;
            }
         }

         // create the file
         System.Data.SQLite.SQLiteConnection.CreateFile(sFile);

         // paranoia (20130620°1613)
         if (! DbClientFactory.ValidateSettings(csLib))
         {
            s = "Connection settings invalid: " + csLib.Description;
            IOBus.Dialogs.dialogErr(s);
            return null;
         }

         // cretate the DbClient
         // note : Compare sequences 20130618°0351 and sequence 20130713°0922.
         dbclient = DbClientFactory.GetDbClient(csLib);

         // don't forget to complete the connection (seq 20130821°1111)
         dbclient.Connect();

         return dbclient;
      }

   }

}
