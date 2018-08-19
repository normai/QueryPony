#region Fileinfo
// file        : http://downtown.trilo.de/svn/queryponydev/trunk/querypony/QueryPonyLib/ConnSettings.cs
// id          : 20130620°0311 (20130604°0121)
// summary     : This file stores class 'ConnSettings' to store the connection
//                settings as seen from the library (as opposed to the GUI
//                connection settings, which shall rather be eliminated).
// license     : GNU AGPL v3
// copyright   : © 2013 - 2018 Norbert C. Maier
// authors     : See /querypony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// version     : (20130620°0311) File cloned from QueryPonyGui/ConnectionSettings.cs ... while refactor 20130620°0211
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace QueryPonyLib
{

   /// <summary>This class homes the extension method to show the enum 'Description' attribute in comboboxes.</summary>
   /// <remarks>id : 20130818°1601</remarks>
   public static class EnumExtensions
   {
      /// <summary>This extension method returns the enum's 'Description' attributes.</summary>
      /// <remarks>
      /// id : 20130818°1602
      /// note : After reference 20130818°1552 'enum description'
      /// </remarks>
      public static string Description(this Enum enumValue)
      {
         var enumType = enumValue.GetType();
         var field = enumType.GetField(enumValue.ToString());
         var attributes = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
         string sRet = (attributes.Length == 0)
                      ? enumValue.ToString()
                       : ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description
                        ;
         return sRet;
      }
   }


   /// <summary>This class constitutes the connection settings on the engine side.</summary>
   /// <remarks>
   /// id : 20130620°0221
   /// note : This class was created while refactor 20130620°0211. The original
   ///    class ConnectionSettings had to be split into ConnectionSettingsGui and
   ///    ConnectionSettingsLib. After the split, ConnectionSettingsLib served as
   ///    helper dummy until syntax was fine and solution built again, then it was
   ///    functionally restored. (note 20130713°0903)
   /// </remarks>
   public class ConnSettingsLib
   {

      /// <summary>This const (16) tells the maximum lenght of the label text of the QueryForm's tabpage.</summary>
      /// <remarks>id : 20130724°1101</remarks>
      private const int i_MaxLengthLabelTabpageConn = 24;

      /// <summary>This const (16) tells the maximum lenght of the label text of a database treenode.</summary>
      /// <remarks>id : 20130724°1102</remarks>
      private const int i_MaxLengthLabelTreenodeDatabase = 24;

      /// <summary>This const (16) tells the maximum lenght of the label text of a server treenode.</summary>
      /// <remarks>id : 20130724°1104</remarks>
      private const int i_MaxLengthLabelTreenodeServer = 24;


      #region Enum definitions

      // (type eliminated 20130818°1801)
      /*
      /// <summary>This enum defines available address types (URL, filename, connectionstring).</summary>
      /// <remarks>
      /// id : 20130620°1521
      /// note : This is just a proposal, not implemented and not mirrored to the
      ///    GUI-ConnectionSettings. The idea is, that one generic field would suffice
      ///    for all database types (whereas now, we have all separate). The question
      ///    is what about a connectionstring: That comprises database type, URL,
      ///    filename and all things. But this question already exists, it does not
      ///    dependend on a possible new 'AddresssType' enum. [note 20130620°1521]
      /// </remarks>
      [Serializable]
      public enum AddressTyype
      {
         /// <summary>This enum value indicates that the given value is a connectionstring (ODBC, OleDb).</summary>
         /// <remarks>id : 20130620°1531</remarks>
         Connstring,

         /// <summary>This enum value indicates that the given value is a 'DataSource' (Oracle).</summary>
         /// <remarks>id : 20130620°1532</remarks>
         DataSource,

         /// <summary>This enum value indicates that the given value is a filename (SQLite).</summary>
         /// <remarks>id : 20130620°1533</remarks>
         Filename,

         /// <summary>This enum value indicates that the given value is a folder (BDE).</summary>
         /// <remarks>id : 20130620°1534</remarks>
         Folder,

         /// <summary>This enum value indicates that the given value is a URL (CouchDB, MySQL, PostgreSQL).</summary>
         /// <remarks>id : 20130620°1535</remarks>
         URL
      }
      */


      /// <summary>This enum defines the available database types.</summary>
      /// <remarks>id : 20130620°0330 (20130604°0128)</remarks>
      [Serializable]
      public enum ConnectionType
      {
         /// <summary>This enum value indicates the connection to a SQLite server.</summary>
         /// <remarks>id : 20130620°0332</remarks>
         [System.ComponentModel.Description("CouchDB")]
         Couch,

         /// <summary>This enum value indicates the connection to a MS-SQL server.</summary>
         /// <remarks>id : 20130620°0333</remarks>
         [System.ComponentModel.Description("MS-SQL")]
         Mssql,

         /// <summary>This enum value indicates the connection to a MySQL server.</summary>
         /// <remarks>id : 20130620°0334</remarks>
         [System.ComponentModel.Description("MySQL")]
         Mysql,

         /// <summary>This enum value indicates the connection to a ODBC interface.</summary>
         /// <remarks>id : 20130620°0335</remarks>
         [System.ComponentModel.Description("ODBC")]
         Odbc,

         /// <summary>This enum value indicates the connection to a Oracle DB server.</summary>
         /// <remarks>id : 20130620°0336</remarks>
         [System.ComponentModel.Description("Oracle")]
         Oracle,

         /// <summary>This enum value indicates the connection to a OleDb interface.</summary>
         /// <remarks>id : 20130620°0337</remarks>
         [System.ComponentModel.Description("OleDb")]
         OleDb,

         /// <summary>This enum value indicates the connection to a SQLite server.</summary>
         /// <remarks>id : 20130620°0338</remarks>
         [System.ComponentModel.Description("PostgreSQL")]
         Pgsql,

         /// <summary>This enum value indicates the connection to a SQLite server.</summary>
         /// <remarks>id : 20130620°0339</remarks>
         [System.ComponentModel.Description("SQLite")]
         Sqlite,

         /// <summary>This enum value indicates that no ConnectionType has been selected so far.</summary>
         /// <remarks>id : 20130620°0331</remarks>
         [System.ComponentModel.Description("NoType")]
         NoType
      }

      /*
      public string ConnStr()
      {
         return "x";
      }

      ////public string ConnectionType.ToString()
      ///string ConnectionType.ToString()
      public static string ConnTypeString()
      {
         return "x";
      }
      */


      #endregion Enum definitions

      #region Properties


      /// <summary>This property gets the connection type as string.</summary>
      /// <remarks>
      /// # id : 20130818°1511
      /// todo : This string is wanted at serveral places all over the program, but
      ///    possibly generated there by local sequences. Replace those redundant
      ///    occurrences by using this centralized method. [todo 20130818°151201]
      ///    E.g. below method 20130723°1441 LabelTreenodeServer().
      ///    And - This method is possibly not yet like finally wanted. I want a
      ///    static method or an additional static method to deliver the DB name
      ///    string also not dependend on having a ConnType instance ...
      /// </remarks>
      public string ConnTypeString
      {
         get
         {
            string sVal = "";

            // read basic database type component
            switch (this.Type)
            {
               case ConnectionType.Couch  : sVal = "CouchDB" ; break;
               case ConnectionType.Mssql  : sVal = "MS-SQL"  ; break;
               case ConnectionType.Mysql  : sVal = "MySQL"   ; break;
               case ConnectionType.NoType : sVal = "N/A"     ; break;
               case ConnectionType.Odbc   : sVal = "ODBC"    ; break;
               case ConnectionType.OleDb  : sVal = "OleDb"   ; break;
               case ConnectionType.Oracle : sVal = "Oracle"  ; break;
               case ConnectionType.Pgsql  : sVal = "PgSQL"   ; break;
               case ConnectionType.Sqlite : sVal = "SQLite"  ; break;
               default                    : sVal = "ERROR"   ; break;
            }

            return sVal;
         }
      }


      /// <summary>This property gets/sets this connection settings' connectionstring.</summary>
      /// <remarks>id : 20130623°1041</remarks>
      public string DatabaseConnectionstring { get; set; }


      /// <summary>This property gets/sets this connection settings' database name.</summary>
      /// <remarks>id : 20130622°0911</remarks>
      public string DatabaseName { get; set; }


      /// <summary>This property gets/sets the server address, e.g. 'localhost'.</summary>
      /// <remarks>
      /// id : 20130723°1021
      /// note : The three new DatabaseServer* properties are tightly related to
      ///    the established property 20130620°0346 'AllDatabaseUrl'. They are
      ///    newly introduced for CouchDB, because to use Divan unchanged, we want
      ///    the portnumber as a dedicated property. And on the occasion to introduce
      ///    a portnumber, I choose to introduce the other components of a complete
      ///    url as dedicated properties as well. The formula shall be:
      ///    'AllDatabaseUrl = ServerProtocol + ":\\" + ServerAddress + ":" + ServerPort'.
      /// </remarks>
      public string DatabaseServerAddress { get; set; }


      /// <summary>This property gets/sets the server portnumber, e.g. 3306</summary>
      /// <remarks>
      /// id : 20130723°1022
      /// note : See issue 20130723°1011 'portnumber casting uint to int'.
      /// </remarks>
      public int DatabaseServerPortnum { get; set; }


      /// <summary>This property gets/sets the server protoclo, e.g. 'http'.</summary>
      /// <remarks>id : 20130723°1023</remarks>
      public string DatabaseServerProtocol { get; set; }


      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : 20130620°0346 (20130617°1416)</remarks>
      public string DatabaseServerUrl { get; set; }


      /// <summary>This property gets a string to be used as ...</summary>
      /// <remarks>
      /// id : 20130620°0344 (20130604°0146)
      /// note : This is not used much. Where exactly is this used?
      ///    - in QueryForm.cs::UpdateFormText() as ... (as what?)
      ///    - in MainForm_FormClosed() just as a debug string
      ///    - in ... as the Server treenode labeling (20130720°1211)
      ///    Is this useful as is? Somehow, something is to be streamlined here.
      /// todo : Use here the new property 20130818°1511 ConnTypeString [todo 20130818°151206]
      /// </remarks>
      [XmlIgnore]
      public string Description
      {
         get
         {
            string sRet = "";

            switch (this.Type)
            {
               case ConnectionType.Couch:
                  sRet = "CouchDB" + Glb.sBlnk + this.DatabaseServerUrl;
                  break;

               case ConnectionType.Mssql:
                  sRet = this.DatabaseServerUrl + Glb.sBlnk + "(" + (this.Trusted ? "Trusted" : this.LoginName.Trim()) + ")";
                  break;

               case ConnectionType.Mysql:
                  sRet = "MySQL" + Glb.sBlnk + this.DatabaseServerUrl;
                  break;

               case ConnectionType.Oracle:
                  sRet = this.DatabaseName + Glb.sBlnk + "(" + (this.Trusted ? "Trusted" : this.LoginName.Trim()) + ")";
                  break;

               case ConnectionType.Odbc:
                  sRet = "Odbc" + Glb.sBlnk + this.DatabaseConnectionstring;
                  break;

               case ConnectionType.OleDb:
                  sRet = "OleDb" + Glb.sBlnk + this.DatabaseConnectionstring;
                  break;

               case ConnectionType.Pgsql:
                  sRet = "PostgreSQL" + Glb.sBlnk + this.DatabaseServerUrl;
                  break;

               case ConnectionType.Sqlite:
                  sRet = "SQLite" + Glb.sBlnk + this.DatabaseName;
                  break;

               default:
                  string sParamName = "ConnectionType";
                  string sMessage = "Invalid Connection Type";
                  throw new ArgumentOutOfRangeException(sParamName, sMessage);
            }

            return sRet;
         }
      }


      /// <summary>This property gets a key for this connection setting.</summary>
      /// <remarks>
      /// id : 20130620°0921 (20130604°0145)
      /// note : What for is this used exactly? In user.config 'Key' it does not show up.
      /// </remarks>
      [XmlIgnore]
      public string Key
      {
         get
         {
            string s = "", sType = "", sRet = "";

            switch (this.Type)
            {
               case ConnectionType.Couch:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin + this.DatabaseServerUrl + Glb.sUlin + this.DatabaseName;
                  break;

               case ConnectionType.Mssql:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnectionType.Mysql:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnectionType.NoType:
                  sRet = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  break;

               case ConnectionType.Odbc:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin;
                  break;

               case ConnectionType.OleDb:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin;
                  break;

               case ConnectionType.Oracle:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin + this.DatabaseName;
                  break;

               case ConnectionType.Pgsql:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  sRet = sType + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnectionType.Sqlite:
                  sType = EnumExtensions.Description((ConnSettingsLib.ConnectionType)this.Type);
                  s = Utils.CombineServerAndDatabaseToFullfilename(this.DatabaseServerAddress, this.DatabaseName);
                  sRet = sType + Glb.sUlin + s; // e.g "SQLite_c:\dbfile.sqlite3"
                  break;

               default:
                  string sParam = "ConnectionType";
                  string sMsg = "Invalid Connection Type";
                  throw new ArgumentOutOfRangeException(sParam, sMsg);
            }

            return sRet;
         }
      }


      /// <summary>
      /// This property gets the label text for this connection's QueryForm's
      ///  TabPage (like the treenode label, but even shorter).
      /// </summary>
      /// <remarks>id : 20130724°0811</remarks>
      public string LabelTabpageDatabase
      {
         get
         {
            // read basic value
            string sVal = this.DatabaseName;

            // process exceptions
            switch (this.Type)
            {
               case ConnectionType.Odbc:
                  sVal = this.DatabaseConnectionstring;
                  if (sVal.StartsWith("DSN=")) { sVal = sVal.Substring(4); }
                  break;
               case ConnectionType.OleDb: sVal = this.DatabaseConnectionstring; break;
               default: break;
            }

            // adjust
            sVal = IOBus.Utils.Strings.ShortenDisplayString(sVal, i_MaxLengthLabelTabpageConn); // e.g. 16

            return sVal;
         }
      }


      /// <summary>This property gets the label text for this connection's database treenode.</summary>
      /// <remarks>id : 20130723°1442</remarks>
      public string LabelTreenodeDatabase
      {
         get
         {
            // read basic value
            string sVal = this.DatabaseName;

            // process exceptions
            switch (this.Type)
            {
               case ConnectionType.Odbc:
                  sVal = this.DatabaseConnectionstring;
                  if (sVal.StartsWith("DSN=")) { sVal = sVal.Substring(4); }
                  break;
               case ConnectionType.OleDb: sVal = this.DatabaseConnectionstring; break;
               default: break;
            }

            // adjust
            sVal = IOBus.Utils.Strings.ShortenDisplayString(sVal, i_MaxLengthLabelTreenodeDatabase); // e.g. 32

            return sVal;
         }
      }


      /// <summary>This property gets the label text for this connection's server treenode.</summary>
      /// <remarks>
      /// id : 20130723°1441
      /// note : For how the label worked before, see e.g. outcommented workaround 20130719°0922 in QueryForm.cs
      /// </remarks>
      public string LabelTreenodeServer
      {
         get
         {
            // initial values
            string sVal = "";

            // read basic server name component
            string sDbUrl = this.DatabaseServerUrl;

            // read basic database type component
            // todo : Use here the new property 20130818°1511 ConnTypeString [todo 20130818°151202]
            switch (this.Type)
            {
               case ConnectionType.Couch  : sVal = "CouchDB" ; break;
               case ConnectionType.Mssql  : sVal = "MS-SQL"  ; break;
               case ConnectionType.Mysql  : sVal = "MySQL"   ; break;
               case ConnectionType.NoType : sVal = "N/A"     ; break;
               case ConnectionType.Odbc   : sVal = "ODBC"    ; sDbUrl = "" ; break;
               case ConnectionType.OleDb  : sVal = "OleDb"   ; sDbUrl = "" ; break;
               case ConnectionType.Oracle : sVal = "Oracle"  ; break;
               case ConnectionType.Pgsql  : sVal = "PgSQL"   ; break;
               case ConnectionType.Sqlite : sVal = "SQLite"  ; break;
               default                    : sVal = "ERROR"   ; break;
            }

            sVal += " " + sDbUrl;

            // adjust
            sVal = IOBus.Utils.Strings.ShortenDisplayString(sVal, i_MaxLengthLabelTreenodeServer); // e.g. 24

            return sVal;
         }
      }


      /// <summary>This property gets/sets the login name for this connection.</summary>
      /// <remarks>id : 20130620°0321 (20130604°0143)</remarks>
      public string LoginName { get; set; }


      /// <summary>This property gets/sets the connection settings password.</summary>
      /// <remarks>id : 20130620°0322 (20130604°0144)</remarks>
      [XmlIgnore]
      public string Password { get; set; }


      /// <summary>This property gets/sets the timeout in seconds to use for this connection.</summary>
      /// <remarks>id : 20130713°0941</remarks>
      public int Timeout { get; set; }


      /// <summary>This property gets/sets the connection settings 'Trusted' flag.</summary>
      /// <remarks>id : 20130620°0319 (20130604°0142)</remarks>
      public bool Trusted { get; set; }


      /// <summary>This property gets/sets the connection settings ConnectionType.</summary>
      /// <remarks>id : 20130620°0341 (20130604°0139)</remarks>
      public ConnectionType Type { get; set; }

      #endregion Properties


      #region Methods

      /// <summary>This method clones this ConnSettings.</summary>
      /// <remarks>
      /// id : 20130620°0313
      /// todo : It looks like some properties are missing to be
      ///    cloned. Complete the method. (todo 20130724°0841)
      /// </remarks>
      public ConnSettingsLib Clone()
      {
         ConnSettingsLib csNew = new ConnSettingsLib();

         // (type eliminated 20130818°180104)
         /*
         csNew.AddressType_NOTYETUSED = this.AddressType_NOTYETUSED;
         */

         csNew.DatabaseConnectionstring = this.DatabaseConnectionstring;
         ////csNew.DatabaseFilename_ELIM = this.DatabaseFilename_ELIM; //// (eliminated 20130818.180203)
         csNew.DatabaseName = this.DatabaseName;
         csNew.DatabaseServerUrl = this.DatabaseServerUrl;
         csNew.LoginName = this.LoginName;
         csNew.Password = this.Password;
         csNew.Trusted = this.Trusted;
         csNew.Type = this.Type;

         return csNew;
      }

      #endregion Methods

   }
}
