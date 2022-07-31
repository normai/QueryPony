#region Fileinfo
// file        : 20130604°0121 /QueryPony/QueryPonyGui/ConnectionSettingsGui.cs
// summary     : Classes 'ConnectionSettingsGui', 'ServerList'/'ConnectionSettingsList' and 'DemoConnectionSettings' maintain the settings.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo

using QueryPonyLib;                                                            // [refactor 20130620°1011]
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;                                              // List
using System.Configuration;                                                    // For [SettingsSerializeAs(SettingsSerializeAs.Xml)] [line 20130807°1701]
using System.Xml.Schema;
using System.Xml.Serialization;

namespace QueryPonyGui
{
   /// <summary>This class provides the XML-file-persisted settings for a connection</summary>
   /// <remarks>
   /// id : class 20130604°0127
   /// note : This class is involved in refactor 20130620°0211
   /// </remarks>
   public class ConnSettingsGui : IComparable
   {
      //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      // Experimental

      /// <summary>
      /// This subclass provides the definition of a server. It is introduced
      ///  experimentally just to characterize treeview nodes by their tag type.
      /// </summary>
      /// <remarks>id : class 20130701°1411</remarks>
      public class Server : IEquatable<Server>
      {
         /// <summary>This constructor creates a new Server object</summary>
         /// <remarks>id : ctor 20130701°1412</remarks>
         public Server(string sServer)
         {
            this.Name = sServer;
         }

         /// <summary>This property gets/sets the server name</summary>
         /// <remarks>id : method 20130701°1413</remarks>
         public string Name { get; set; }

         /// <summary>This method ... is wanted for interface IEquatable<Server> ... for treenode comparison</summary>
         /// <remarks>id : method 20130701°1431</remarks>
         /// <param name="other">The object to be compared</param>
         public bool Equals(Server other)
         {
            // Reference equality is still a simple and necessary check
            if(Object.ReferenceEquals(this, other)) { return true; }

            // Another simple check, the two objects should be the same type
            if(this.GetType() != other.GetType()) { return false; }

            // Now compare members of the object that you want to use to determine 'semantic'
            //  equality. These members, if also reference types, must also be 'semantically' equal.
            if (this.Name == other.Name)
            {
               return true;
            }

            return false;
         }

         /// <summary>This method ... is wanted for interface IEquatable[Server] ... for treenode comparison</summary>
         /// <remarks>id : method 20130701°1434</remarks>
         /// <param name="other">The object to be compared</param>
         public override bool Equals(object other)
         {
            if (other.GetType() == this.GetType())
            {
               return this.Equals(((Server) other));
            }
            return false;
         }

         /// <summary>This method ... is wanted for interface IEquatable[Server]</summary>
         /// <remarks>id : method 20130701°1435</remarks>
         /// <returns>The wanted hash code</returns>
         public override int GetHashCode()
         {
            return this.Name.GetHashCode();
         }
      }

      /// <summary>This constructor creates a new ConnectionSetting</summary>
      /// <remarks>id : ctor 20130622°0941</remarks>
      public ConnSettingsGui()
      {
         // [seq 20130622°0942]
         // Note : This may be a service for later, when they may be empty but must
         //    not be null, e.g. in the MySQL getConnection(). This should actually
         //    not be necessary, because those who set up the connection should do it.
         //    [note 20130622°094202]
         this.LoginName = "";
         this.Password = "";
      }

      /// <summary>This enum defines some connection states</summary>
      /// <remarks>
      /// id : enum 20130828°1511
      /// note : This gradation is definitely too finegrained for the usage now.
      ///    Only 'Active' is used so far. Maybe somewhen such states can become
      ///    useful, e.g. to paint database nodes in different colors.
      /// </remarks>
      public enum ConnStatus
      {
         Unvalidated,                                                          // User has written something into the connection settings
         Invalid,                                                              // The connection settings is defintiely wrong
         Valid,                                                                // The connection settings seem fine
         Failed,                                                               // Settings is fine, but the server is not reachable or down
         Connected                                                             // Successfully connected (so far the only significant status)
      }

      #region Private Variables

      /// <summary>This field stores the value of the live property 'Type'</summary>
      /// <remarks>id : var 20130809°1223</remarks>
      private ConnSettingsLib.ConnectionType _Type = ConnSettingsLib.ConnectionType.NoType;

      /// <summary>
      /// This field stores the value of the Settings property 'sType' (wanted
      ///  for the 'self-synchronizing property pair' for solution 20130809°1221).
      /// </summary>
      /// <remarks>id : var 20130809°1224</remarks>
      private string _sType = "";

      #endregion Private Variables

      #region Public Properties

      /// <summary>This property gets/sets the connection's connectionstring</summary>
      /// <remarks>
      /// id : prop 20130617°1412
      /// note : This property is one of the newly introduced generic
      ///    properties to replace the provider-specific ones.
      /// </remarks>
      public string DatabaseConnectionstring { get; set; }

      /// <summary>This property gets/sets the wanted database name on a given server</summary>
      /// <remarks>id : prop 20130623°0702</remarks>
      public string DatabaseName { get; set; }

      /// <summary>This property gets/sets the connection's server URL</summary>
      /// <remarks>
      /// id : prop 20130617°1416
      /// note : This property is one of the newly introduced generic
      ///    properties to replace the provider-specific ones.
      /// </remarks>
      public string DatabaseServerUrl { get; set; }

      // Experimentally shutdown [chg 20130808°1554] This property seems not be referenced anymore
      /*
      /// <summary>This property gets ...</summary>
      /// <remarks>id : prop 20130604°0146</remarks>
      [XmlIgnore]
      public string Description
      {
         get
         {
            string sRet = "";

            switch (this.Type)
            {
               case ConnSettings.ConnectionType.Couch:
                  sRet =  "CouchDB" + Glb.sBlnk + this.DatabaseServerUrl;
                  break;

               case ConnSettings.ConnectionType.Mssql:
                  sRet = this.DatabaseServerUrl + Glb.sBlnk + "(" + (Trusted ? "Trusted" : LoginName.Trim()) + ")";
                  break;

               case ConnSettings.ConnectionType.Mysql:
                  sRet = "MySQL" + Glb.sBlnk + this.DatabaseServerUrl; ;
                  break;

               case ConnSettings.ConnectionType.Oracle:
                  sRet = this.DatabaseName + Glb.sBlnk + "(" + (this.Trusted ? "Trusted" : this.LoginName.Trim()) + ")";
                  break;

               case ConnSettings.ConnectionType.Odbc:
                  sRet = "Odbc" + Glb.sBlnk + this.DatabaseConnectionstring;
                  break;

               case ConnSettings.ConnectionType.OleDb:
                  sRet = "OleDb" + Glb.sBlnk + this.DatabaseConnectionstring;
                  break;

               case ConnSettings.ConnectionType.Pgsql:
                  sRet = "SQLite" + Glb.sBlnk + this.DatabaseServerUrl + Glb.sBlnk + this.DatabaseName;
                  break;

               case ConnSettings.ConnectionType.Sqlite:
                  sRet = "SQLite" + Glb.sBlnk + this.DatabaseServerUrl + Glb.sBlnk + this.DatabaseName;
                  break;

               default:
                  string sParamName = "ConnectionType";
                  string sMessage = "Invalid Connection Type";
                  throw new ArgumentOutOfRangeException(sParamName, sMessage);
            }

            return sRet;
         }
      }
      */

      /// <summary>This property gets the connection's Key</summary>
      /// <remarks>
      /// id : property 20130604°0145
      /// todo : Use here the new property 20130818°1511 ConnTypeString [todo 20130818°151204]
      /// </remarks>
      [XmlIgnore]
      public string Key
      {
         get
         {
            string sRet = "";
            switch (this.Type)
            {
               case ConnSettingsLib.ConnectionType.Couch:
                  sRet = "CouchDB" + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnSettingsLib.ConnectionType.Mssql:
                  sRet = "MS-SQL" + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnSettingsLib.ConnectionType.Mysql:
                  sRet = "MySQL" + Glb.sUlin + this.DatabaseServerUrl;
                  break;

               case ConnSettingsLib.ConnectionType.Odbc:
                  sRet = "ODBC" + Glb.sUlin + this.DatabaseConnectionstring;
                  break;

               case ConnSettingsLib.ConnectionType.OleDb:
                  sRet = "OleDb" + Glb.sUlin + this.DatabaseConnectionstring;
                  break;

               case ConnSettingsLib.ConnectionType.Oracle:
                  sRet = "ORACLE" + Glb.sUlin + this.DatabaseServerUrl;        // Just on suspicion [line 20130717°1221`03]
                  break;

               case ConnSettingsLib.ConnectionType.Pgsql:
                  sRet = "PostgreSQL" + Glb.sUlin + this.DatabaseServerUrl;    // Just on suspicion [line 20130717°1221`02]
                  break;

               case ConnSettingsLib.ConnectionType.Sqlite:
                  sRet = "SQLite" + Glb.sUlin + this.DatabaseName;
                  break;

               default:
                  throw new ArgumentOutOfRangeException("ConnectionType", "Invalid Connection Type");
            }
            return sRet;
         }
      }

      /// <summary>This property gets/sets the connection's login name</summary>
      /// <remarks>id : property 20130604°0143</remarks>
      public string LoginName { get; set; }

      /// <summary>This property gets/sets the connection's password</summary>
      /// <remarks>id : property 20130604°0144</remarks>
      [XmlIgnore]
      public string Password { get; set; }

      /// <summary>
      /// This property gets/sets the connection type as string, as opposed
      ///  to 'ConnSettings.ConnectionType Type'. It must be translated
      ///  to/from 'ConnSettings.ConnectionType Type' on each access.
      /// </summary>
      /// <remarks>
      /// id : property 20130809°1222
      /// note : This is key component for solution 20130809°1221 against
      ///    issue 20130731°0131 'Settings with reference to resourced library silently fail'.
      /// </remarks>
      public string sType
      {
         get
         {
            return this._sType;
         }
         set
         {
            switch (value)
            {
               case "Couch"  : this._Type = ConnSettingsLib.ConnectionType.Couch  ; break;
               case "Mssql"  : this._Type = ConnSettingsLib.ConnectionType.Mssql  ; break;
               case "Mysql"  : this._Type = ConnSettingsLib.ConnectionType.Mysql  ; break;
               case "Odbc"   : this._Type = ConnSettingsLib.ConnectionType.Odbc   ; break;
               case "OleDb"  : this._Type = ConnSettingsLib.ConnectionType.OleDb  ; break;
               case "Oracle" : this._Type = ConnSettingsLib.ConnectionType.Oracle ; break;
               case "Pgsql"  : this._Type = ConnSettingsLib.ConnectionType.Pgsql  ; break;
               case "Sqlite" : this._Type = ConnSettingsLib.ConnectionType.Sqlite ; break;
               default       : this._Type = ConnSettingsLib.ConnectionType.NoType ; break;
            }

            this._sType = value;
         }
      }

      /// <summary>This property gets/sets the connection's Type</summary>
      /// <remarks>
      /// id : property 20130604°0139
      /// note : This property was created during refactor 20130620°0211 'Split GUI from Lib Settings'.
      /// note : (finding 20130808°1555 about issue 20130731°0131) If this property
      ///    is set '[XmlIgnore]', the issue 20130731°0131 is gone. But also, it is not
      ///    saved to the Settings anymore. So we need an additional workaround mechanism.
      /// </remarks>
      [XmlIgnore]
      internal ConnSettingsLib.ConnectionType Type
      {
         get
         {
            return this._Type;
         }
         set
         {
            switch (value)
            {
               case ConnSettingsLib.ConnectionType.Couch  : this._sType = "Couch"  ; break;
               case ConnSettingsLib.ConnectionType.Mssql  : this._sType = "Mssql"  ; break;
               case ConnSettingsLib.ConnectionType.Mysql  : this._sType = "Mysql"  ; break;
               case ConnSettingsLib.ConnectionType.Odbc   : this._sType = "Odbc"   ; break;
               case ConnSettingsLib.ConnectionType.OleDb  : this._sType = "OleDb"  ; break;
               case ConnSettingsLib.ConnectionType.Oracle : this._sType = "Oracle" ; break;
               case ConnSettingsLib.ConnectionType.Pgsql  : this._sType = "Pgsql"  ; break;
               case ConnSettingsLib.ConnectionType.Sqlite : this._sType = "Sqlite" ; break;
               default                                    : this._sType = "NoType" ; break;
            }

            this._Type = value;
         }
      }

      /// <summary>This property gets/sets the setting flag 'Save Password' whether to store the password in the settings file or not</summary>
      /// <remarks>
      /// id : property 20130620°1641
      /// todo : Find out about an encryption mechanism offered by .NET to store the password in
      ///        the file, so that only this user can decode it. [todo 20130620°1641]
      /// </remarks>
      public bool SavePassword { get; set; }

      /// <summary>This property gets/sets the connection status</summary>
      /// <remarks>id : property 20130828°1512</remarks>
      public ConnStatus Status { get; set; }

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : property 20130604°0142</remarks>
      public bool Trusted { get; set; }

      #endregion Public Properties

      #region Public Methods

      /// <summary>This method serves the IComparable interface. It provides the ability of the objects to be sorted</summary>
      /// <remarks>
      /// id : method 20130623°1511
      /// note : About IComparable see references 20130623°1522 'interface IComparable'
      ///    and 20130623°1523 'method IComparable.CompareTo'.
      /// </remarks>
      /// <param name="other"></param>
      /// <returns>Comparison result</returns>
      public int CompareTo(object obj)
      {
         int iRet = 1;
         if (obj == null)
         {
            return iRet;
         }

         ConnSettingsGui csOther = obj as ConnSettingsGui;
         string sThis = this.ConnIdString();
         string sOther = csOther.ConnIdString();
         iRet = String.Compare(sThis, sOther);

         return iRet;
      }

      /// <summary>
      /// This method provides the captions for displaying
      /// connections in the Connections Combobox.
      /// </summary>
      /// <remarks>
      /// id : method 20130724°0924
      /// note : The reason for the existence of this override method
      ///    is described in issue 20130724°0923 in ConnIdString().
      /// </remarks>
      /// <returns>The string by which this connection shall be represented (in the Connection Combobox)</returns>
      public override string ToString()
      {
         string sRet = this.ConnIdString();
         return sRet;
      }

      /// <summary>This method returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see></summary>
      /// <remarks>
      /// id : method 20130604°0150
      /// note : This method is wanted to build the names appearing in the
      ///          Server/Connection comboboxes.
      /// qanda : Why can a switch get away without the mandatory 'break' statements?
      ///    This is possible if the 'case' ends in a 'return' or 'throw'. With such
      ///    logic, why is it necessary to place a 'break' then behind the 'default'
      ///    if not a return? (qanda 20130617°1401)
      /// attention : [20130622°101501]
      ///         The string building must go strictly analogous in the following places:
      ///         - ConnectionSettings.cs::ConnectionSettings.ToString()
      ///         - ConnectForm.cs::tabcontrol_ServerTypes_SelectedIndexChanged()
      /// todo : Make a string building method which can also be used from the TabPages
      ///         on the ConnectForm. [todo 20130622°1016]
      /// change : Having this functionality originally implemented as 'override
      ///    string ToString()' was more of a gimmick, than for a reason, I think.
      ///    More unambiguous, it were rather implmemeted as a regular property.
      ///    [change 20130724°0921]
      /// issue : Which advantages might have a ToString() override? Here is the
      ///    killer-reason: A combobox my be filled with items of any type. But the
      ///    caption it shows for each item, is just that objects ToString() method.
      ///    So for seeing the connections correctly described in the Connections
      ///    Combobox, we need to provide the ToString() method accordingly.
      ///    [issue 20130724°0923]
      /// todo : Provide the connection ID string not from the GUI's ConnSettings,
      ///    but from the Lib's ConnSettings. This goes toghether with the overall
      ///    topic of mostly eliminating the GUI's ConnSettings at all. Compare
      ///    issue 20130724°0927. [todo 20130724°0925]
      /// </remarks>
      /// <filterpriority>2</filterpriority>
      /// <returns>A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.</returns>
      public string ConnIdString()
      {
         string sEle1 = "", sEle2 = "", sRet = "";

         // prepare parameters
         switch (this.Type)
         {
            case ConnSettingsLib.ConnectionType.Couch:
               sEle1 = this.DatabaseServerUrl;
               sEle2 = this.DatabaseName;
               break;

            case ConnSettingsLib.ConnectionType.Mssql:
               sEle1 = this.DatabaseServerUrl;
               sEle2 = this.DatabaseName;
               break;

            case ConnSettingsLib.ConnectionType.Mysql:
               sEle1 = this.DatabaseServerUrl;
               sEle2 = this.DatabaseName;
               break;

            case ConnSettingsLib.ConnectionType.Odbc:
               sEle1 = this.DatabaseConnectionstring;
               break;

            case ConnSettingsLib.ConnectionType.OleDb:
               sEle1 = this.DatabaseConnectionstring;
               break;

            case ConnSettingsLib.ConnectionType.Oracle:
               sEle1 = this.DatabaseServerUrl;                                         // Just on suspicion (20130717°122104)
               sEle2 = this.DatabaseName;                                              // Just on suspicion (20130717°122405)
               break;

            case ConnSettingsLib.ConnectionType.Pgsql:
               sEle1 = this.DatabaseServerUrl;
               sEle2 = this.DatabaseName;
               break;

            case ConnSettingsLib.ConnectionType.Sqlite:
               sEle1 = this.DatabaseServerUrl;                                         // [var 20130702°1423]
               sEle2 = this.DatabaseName;                                              // [var 20130702°1423`02]
               break;

            // [seq 20130623°0731]
            case ConnSettingsLib.ConnectionType.NoType:
               break;

            // this.Type = null
            default:
               break;
         }

         // retrieve connnection ID string
         sRet = getConnectionId(this.Type, sEle1, sEle2);

         return sRet;
      }

      /// <summary>This method creates an connection ID string</summary>
      /// <remarks>
      /// id : method 20130623°1001
      /// todo : Use here the new property 20130818°1511 ConnTypeString [todo 20130818°151207]
      /// </remarks>
      /// <returns>The wanted ID string</returns>
      public static string getConnectionId(ConnSettingsLib.ConnectionType ct, string sEle1, string sEle2)
      {
         string sRet = "";
         switch (ct)
         {
            case ConnSettingsLib.ConnectionType.Couch:
               sRet = "CouchDb" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2;
               break;

            case ConnSettingsLib.ConnectionType.Mssql:
               sRet = "MS-SQL" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2;
               break;

            case ConnSettingsLib.ConnectionType.Mysql:
               sRet = "MySQL" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2;
               break;

            case ConnSettingsLib.ConnectionType.Odbc:
               sRet = "ODBC" + Glb.sBlHyBl + sEle1;
               break;

            case ConnSettingsLib.ConnectionType.OleDb:

               //----------------------------------------------
               // [issue 20130623°1123]
               // topic : Different 'connection strings' (ID and description)
               // symptom : In the Connection ComboBox, for connections with long filenames,
               //    one cannot see the (significant) tail anymore. We need a way to provide
               //    the user with a shorter (significant) description in the combobox.
               // workaround : Stay cool. It is just an annoyance, not a killer bug.
               // note : Since we use ConnectionSettings.ToString() as idenifyer for connections,
               //    we must no more cut significant information (as did before). For purely
               //    descriptive purposes use another thing (e.g. property 'Description'?).
               // proposal : E.g. a long filename in an ID string looks curious. For purely
               //    identification purposes, e.g. a hash value were better suited. Such could
               //    be provided e.g. by a dedicated property, e.g. 'ConnectionSettings.ID'.
               // status : Open
               //----------------------------------------------

               sRet = "OleDb" + Glb.sBlHyBl + sEle1;
               break;

            case ConnSettingsLib.ConnectionType.Oracle:
               sRet = "Oracle" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2;            // Added sEle2 just on suspicion (20130717°122106)
               break;

            case ConnSettingsLib.ConnectionType.Pgsql:
               sRet = "PostgreSQL" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2;
               break;

            case ConnSettingsLib.ConnectionType.Sqlite:

               sRet = "SQLite" + Glb.sBlHyBl + sEle1 + Glb.sBlHyBl + sEle2; ;
               break;

            // (20130623°073102)
            case ConnSettingsLib.ConnectionType.NoType:
               sRet = "<N/A>";
               break;

            default:
               string sParamName = "ConnectionType";
               string sMessage = "Invalid Connection Type (Error 20130623°1031)";
               throw new ArgumentOutOfRangeException(sParamName, sMessage);
         }
         return sRet;
      }

      /// <summary>This method clones the ConnectionSettings object itself</summary>
      /// <remarks>
      /// id : method 20130604°0151
      /// note : Since this method was never tested, it might be incomplete.
      /// </remarks>
      /// <returns>The newly created clone</returns>
      public ConnSettingsGui Clone()
      {
         ConnSettingsGui csNew;
         csNew = new ConnSettingsGui();

         csNew.DatabaseConnectionstring = this.DatabaseConnectionstring;
         csNew.DatabaseName = this.DatabaseName;
         csNew.DatabaseServerUrl = this.DatabaseServerUrl;
         csNew.LoginName = this.LoginName;
         csNew.Password = this.Password;
         csNew.Password = this.Password;
         csNew.SavePassword = this.SavePassword;
         csNew.Trusted = this.Trusted;
         csNew.Type = this.Type;

         return csNew;
      }

      /// <summary>This method converts Engine-ConnectionSettings to GUI-ConnectionSettings</summary>
      /// <remarks>
      /// id : method 20130620°1131
      /// note : Need some kind of translator from 'ConnectionSettings' to 'ConnectionSettings_DUMMY'.
      ///    This seems a key element while refactor 20130620°0211 'Split Settings GUI and Lib'.
      ///    [note 20130620°1121 from method 20130618°0401 FurnishConnectTab()]
      /// </remarks>
      /// <param name="csLib">The Engine-ConnectionSettings to be converted</param>
      /// <returns>The wanted GUI-ConnectionSettings</returns>
      public static ConnSettingsGui convertSettingsLibToGui(ConnSettingsLib csLib)
      {
         ConnSettingsGui csGui = new ConnSettingsGui();

         csGui.DatabaseConnectionstring = csLib.DatabaseConnectionstring;
         csGui.DatabaseName = csLib.DatabaseName;
         csGui.DatabaseServerUrl = csLib.DatabaseServerUrl;
         csGui.LoginName = csLib.LoginName;
         csGui.Password = csLib.Password;
         csGui.Trusted = csLib.Trusted;
         csGui.Type = csLib.Type;

         return csGui;
      }

      /// <summary>This method converts GUI ConnectionSettings to Engine ConnectionSettings</summary>
      /// <remarks>
      /// id : method 20130620°1621
      /// note : Here we see the first time a systematic/alphabetic list of the properties of the
      ///     librarie's ConnectionSettings. This is the list of all properies, which made it from
      ///     the GUI to the Lib during refactor 20130620°0211 'Split. [note 20130620°1622]
      /// </remarks>
      /// <param name="csLib">The GUI ConnectionSettings to be converted</param>
      /// <returns>The wanted Engine ConnectionSettings</returns>
      public static ConnSettingsLib convertSettingsGuiToLib(ConnSettingsGui csGui)
      {
         ConnSettingsLib csLib = new ConnSettingsLib();

         csLib.DatabaseConnectionstring = csGui.DatabaseConnectionstring;
         csLib.DatabaseName = csGui.DatabaseName;
         csLib.DatabaseServerUrl = csGui.DatabaseServerUrl;
         csLib.LoginName = csGui.LoginName;
         csLib.Password = csGui.Password;
         csLib.Trusted = csGui.Trusted;
         csLib.Type = csGui.Type;

         return csLib;
      }

      #endregion Public Functions
   }

   /*
   todo 20130624°1143 'Rename class ServerList to ConnectionSettingsList'
   Mmatter : Rename class 'ServerList' to 'ConnectionSettingsList'. Only this is sensitive
      because it involves the Settings. When attempting to refactor the name from the usual
      editor, we get a dialog [20130624°1142] "The file 'Properties\Settings.Designer.cs'.
      could not be refactored. The current object is auto-generated and only supports renaming
      throught the Settings Designer. Do you wish to continue ...?"
   Location : class 20130604°0122 ServerList
   Status : ?
   */

   /// <summary>This class constitutes a list of configurations of type GUI-ConnectionSettings</summary>
   /// <remarks>
   /// id : class 20130604°0122
   /// note : Remember todo 20130624°1143 'Rename class ServerList to ConnectionSettingsList'
   /// </remarks>
   public class ServerList
   {
      /// <summary>This constructor creates a empty ServerList object. It is needed to make the Settings work</summary>
      /// <remarks>
      /// id : constructor 20130621°1121
      /// note : Without constructor, the ServerList will mysteriously be written
      ///    only empty to user.config. This is not obvious and difficult to debug.
      ///    So never forget: For complex Setting types, you need the constructor!
      /// ref : 20130621°0934/093402 'Thread: Settings with complex types'
      /// todo : Possibly rename 'ServerList' to 'ConnectionList' [todo 20130622°0902]
      /// </remarks>
      public ServerList()
      {
      }

      /// <summary>This field stores the ArrayList with the connection settings</summary>
      /// <remarks>id : field 20130604°0123</remarks>
      private ArrayList _alConnSettings = new ArrayList();

      /// <summary>This property gets/sets the items of the Connection List</summary>
      /// <remarks>id : property 20130604°0124</remarks>
      [XmlElement("Server", Form = XmlSchemaForm.Unqualified)]
      public ConnSettingsGui[] Items
      {
         get
         {
            int iNumOfItems = _alConnSettings.Count;
            ConnSettingsGui[] arcs = new ConnSettingsGui[iNumOfItems];

            // Fine — That's what we had before debugging [line 20130620°1154]
            arcs = (ConnSettingsGui[])this._alConnSettings.ToArray(typeof(ConnSettingsGui));

            return arcs;
         }
         set
         {
            this._alConnSettings.Clear();
            this._alConnSettings.AddRange(value);
         }
      }

      /// <summary>This method adds one ConnectionSettings item to the ServerList</summary>
      /// <remarks>id : method 20130604°0125</remarks>
      /// <param name="conSettings">The ConnectionSettings to be added to this ConnectionList object.</param>
      /// <returns>The ArrayList index at which the value has been added to the ConnectionList (useless because it's the index before sorting).</returns>
      public int Add(ConnSettingsGui conSettings)
      {
         int iRet = -1;
         iRet = _alConnSettings.Add(conSettings);

         // This works after IComparable was implemented [note 20130623°1513]
         _alConnSettings.Sort();

         return iRet;
      }

      /// <summary>This method removes one ConnectionSettings item from the ServerList</summary>
      /// <remarks>id : method 20130623°0711</remarks>
      /// <param name="conSettings">The index for the item to remove</param>
      /// <returns>Success flag</returns>
      public bool Remove(int iNdx)
      {
         bool bRet = false;
         if (_alConnSettings.Count > -1)
         {
            _alConnSettings.RemoveAt(iNdx);
            bRet = true;
         }
         return bRet;
      }

      /// <summary>This method retrieves the array index of one Connection by the given key</summary>
      /// <remarks>id : method 20130604°0126</remarks>
      /// <param name="key">The key for which to retrieve the index.</param>
      /// <returns>The ConnectionSettings array index associated with the given key.</returns>
      public int IndexOf(string sKey)
      {
         for (int i = 0; i < _alConnSettings.Count; i++)
         {
            ConnSettingsGui conSetting = (ConnSettingsGui) _alConnSettings[i];
            if (conSetting.Key == sKey)
            {
               return i;
            }
         }
         return -1;
      }

      /// <summary>This method retrieves the array index of one Connection by the given connection ID</summary>
      /// <remarks>
      /// id : method 20130701°1241 (after 20130604°0126)
      /// note : This method is introduced only for method 20130618°0411 button_Connect_Click()
      ///         With a cleaner design of the treenode handling, it were probably superfluous
      /// todo : One of the methods IndexOf() and IndexOfById is probably superfluous. Analyse
      ///         and refactor the usages of this.Id and this.ToString() [todo 20130701°1242]
      /// </remarks>
      /// <param name="key">The ID for which to retrieve the index.</param>
      /// <returns>The ConnectionSettings array index associated with the given ID.</returns>
      public int IndexOfById(string sConnId)
      {
         for (int i = 0; i < _alConnSettings.Count; i++)
         {
            ConnSettingsGui conSetting = (ConnSettingsGui)_alConnSettings[i];
            if (conSetting.ConnIdString() == sConnId)
            {
               return i;
            }
         }
         return -1;
      }

      /// <summary>This property gets a List of the ConnectionList ID strings</summary>
      /// <remarks>id : property 20130604°0128</remarks>
      [XmlIgnore] // Do not store this property in the Settings [added 20130810°1111]
      public List<string> Ids
      {
         get
         {
            List<string> list = new List<string>();
            for (int i = 0; i < this.Items.Length; i++)
            {
               list.Add(this.Items[i].ConnIdString());
            }
            return list;
         }
      }
   }

   /// <summary>This class constitutes a list of hardcoded demo connections (experimental)</summary>
   /// <remarks>id : class 20130624°1211 (20130604°0122)</remarks>
   public class DemoConnSettings
   {
      /// <summary>This constructor creates a empty DemoConnSettings object</summary>
      /// <remarks>id : ctor 20130624°1212</remarks>
      public DemoConnSettings()
      {
         fillTheList();
      }

      /// <summary>This field stores ...</summary>
      /// <remarks>id : field 20130624°1213</remarks>
      private ArrayList _arraylist = new ArrayList();

      /// <summary>This property gets/sets ...</summary>
      /// <remarks>id : property 20130624°1214 (after 20130604°0124)</remarks>
      public List<ConnSettingsGui> Items
      {
         get
         {
            List<ConnSettingsGui> csl = new List<ConnSettingsGui>();
            for (int i = 0; i < _arraylist.Count; i++)
            {
               ConnSettingsGui cs = _arraylist[i] as ConnSettingsGui;
               csl.Add(cs);
            }
            return csl;
         }
      }

      /// <summary>This method creates a new ServerList either empty or with default values</summary>
      /// <remarks>id : method 20130624°1215 (after 20130617°1511)</remarks>
      private void fillTheList()
      {
         ConnSettingsGui cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Couch;
         cs.DatabaseServerUrl = "127.0.0.1" + Glb.DbSpecs.sSepaUrlPort + Glb.DbSpecs.CouchDefaultPortnum.ToString(); // '127.0.0.1:5984'
         cs.DatabaseName = "Aloha";
         this.Add(cs);

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Mssql;
         cs.DatabaseServerUrl = "localhost\\SQLEXPRESS";
         cs.DatabaseName = "microbrewery";
         cs.Trusted = true;
         this.Add(cs);

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Mssql;
         cs.DatabaseServerUrl = "localhost\\SQLEXPRESS";
         cs.DatabaseName = "Northwind";
         cs.Trusted = true;
         this.Add(cs);

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Mysql;
         cs.DatabaseServerUrl = "127.0.0.1" + Glb.DbSpecs.sSepaUrlPort + Glb.DbSpecs.MysqlDefaultPortnum.ToString(); // '127.0.0.1:3306'
         cs.DatabaseName = "contao";
         this.Add(cs);

         // See ref 20130624°1001 'Article: Connection strings for Paradox'
         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Odbc;
         cs.DatabaseConnectionstring = "DSN=Joesgarage";
         this.Add(cs);

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.OleDb;
         cs.DatabaseConnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Paradox 7.x;Data Source=C:\\NetDir\\Joesgarage\\Firmendaten";
         this.Add(cs);

         // The Oracle connection makes problems with 'Add Demo Connections' [note 20130624°1242]
         if (Glb.Debag.Execute_Yes)
         {
            cs = new ConnSettingsGui();
            cs.Type = ConnSettingsLib.ConnectionType.Oracle;
            cs.DatabaseName = "XE";
            this.Add(cs);
         }

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Pgsql;
         cs.DatabaseServerUrl = "127.0.0.1" + Glb.DbSpecs.sSepaUrlPort + Glb.DbSpecs.PgsqlDefaultPortnum.ToString(); // '127.0.0.1:5432'
         cs.DatabaseServerUrl = "localhost";                                   // Port 5432 seems to be supplied automatically
         cs.DatabaseName = "pauline1";
         cs.LoginName = "postgres";
         this.Add(cs);

         //----------------------------------------------------
         // Guarantee SQLite demo database files [seq 20130709°1351]
         // Note : This is the first time using of provideResourceFiles() with a mixed-assembly-list
         System.Reflection.Assembly asmSource1 = System.Reflection.Assembly.GetExecutingAssembly();
         System.Reflection.Assembly asmSource2 = System.Reflection.Assembly.Load(Glb.Resources.AssemblyNameLib);
         string sAsmResourceName1 = Glb.Resources.JoesgarageSqliteResourcename;  // "QueryPonyGui.docs.joesgarage.sqlite3"
////     string sAsmResourceName2 = Glb.Resources.JoespostboxSqliteResourcename;  // "QueryPonyLib.docs.joespostbox.201307031243.sqlite3"
         string sTargetFolder = Program.PathConfigDirUser + "\\" + "docs";
         string sTargetFilename1 = Glb.Resources.JoesgarageSqliteFilename;     // "joesgarage.sqlite3"
////     string sTargetFilename2 = Glb.Resources.JoespostboxSqliteFilename;    // "joespostbox.201307031243.sqlite3";
         string sFullfilename1 = System.IO.Path.Combine(sTargetFolder, sTargetFilename1);
////     string sFullfilename2 = System.IO.Path.Combine(sTargetFolder, sTargetFilename2);

         // Prepare extraction list
         IOBus.Utils.Resofile[] resos = { new IOBus.Utils.Resofile(asmSource1, sAsmResourceName1, sTargetFolder, sTargetFilename1)
                                        //// , new IOBus.Utils.Resofile(asmSource2, sAsmResourceName2, sTargetFolder, sTargetFilename2)
                                        };

         // Determine assembly from which to extract files
         System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly(); // First occasion I use this method [line 20130709°1352]

         // Perform the extraction
         IOBus.Utils.provideResourceFiles(resos);

         // Paranoia
         if (! System.IO.File.Exists(sFullfilename1))
         {
            // Fatal
            // Todo : Provide error handling [issue 20130709°1354]
         }
         ////if (! System.IO.File.Exists(sFullfilename2))
         ////{
         ////   // Fatal
         ////   // Todo : Provide error handling [issue 20130709°1355]
         ////}
         Program.SqliteDemoJoesgarage = sFullfilename1;
         ////Program.SqliteDemoJoespostbox = sFullfilename2;
         //----------------------------------------------------

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Sqlite;
         string sRoot = "";
         string sFilePathWithoutRoot = "";
         bool bProforma = Utils.SplitFullfilenamInServerAndDatabase(Program.SqliteDemoJoesgarage, out sRoot, out sFilePathWithoutRoot);
         cs.DatabaseServerUrl = sRoot;                                         // Experiment [line 20130702°1413`01]
         cs.DatabaseName = sFilePathWithoutRoot;                               // Experiment [line 20130702°1413`02]
         this.Add(cs);

         cs = new ConnSettingsGui();
         cs.Type = ConnSettingsLib.ConnectionType.Sqlite;
         sRoot = "";
         sFilePathWithoutRoot = "";
         bProforma = Utils.SplitFullfilenamInServerAndDatabase(Program.SqliteDemoJoespostbox, out sRoot, out sFilePathWithoutRoot);
         cs.DatabaseServerUrl = sRoot;
         cs.DatabaseName = sFilePathWithoutRoot;
         this.Add(cs);

         return;
      }

      /// <summary>This method adds one ConnectionSettings item to this DemoConnectionSettings object</summary>
      /// <remarks>id : method 20130624°1215 (20130604°0125)</remarks>
      /// <param name="conSettings">The ConnectionSettings to be added to this object.</param>
      /// <returns>The ArrayList index at which the value has been added to the list.</returns>
      public int Add(ConnSettingsGui conSettings)
      {
         int iRet = -1;
         iRet = _arraylist.Add(conSettings);
         return iRet;
      }
   }
}
