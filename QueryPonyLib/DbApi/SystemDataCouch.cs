#region Fileinfo
// file        : 20130719°0901 /QueryPony/QueryPonyLib/DbApi/SystemDataCouch.cs
// summary     : This file homes classes 'CouchDBDataAdapter', 'CouchDBInfoMessageEventArgs',
//               'CouchDBCommand', 'CouchDBConnection' to constitute an experimental
//               implementation of .NET CouchDB classes analogical to the other native
//               or third-party database access classes.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2021 Norbert C. Maier
// authors     : See /QueryPony/QueryPonyGui/docs/authors.txt
// encoding    : UTF-8-with-BOM
// status      : Experimental
// note        :
// callers     :
#endregion Fileinfo

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

//-----------------------------------------------------
// note 20130719°0901 ''
// The namespace name 'System.Data.CouchDB' is pretentious. Never any comprehensive
//  Implementation will be attempted, but little stubs may suffice. With this name,
//  I only follow the SQLite example. The name indicates correctly the purpose of
//  the namespace's classes: having analogical classes for all database types.
//  But perhaps it should rather be renamed more QueryPony related anyway.
//-----------------------------------------------------
namespace System.Data.CouchDB
{
   /// <summary>
   /// This class represents a CouchDB command. It is implemented
   ///  proforma, to satisfy syntax, with no functionality so far.
   /// </summary>
   /// <remarks>
   /// id : 20130716°0941
   /// note : For CouchDB, there is no third party library offering a *DBCommand class,
   ///    as with the other databases. But I want such class, at least proforma, to keep
   ///    the code parallel other implementations. This class is experimental, I have no
   ///    idea, how it will develop. The class definition line with the interfaces,
   ///    as well as the members to suffice syntax in the first attempt, are written
   ///    after 'System.Data.SQLite::SQLiteDataAdapter'.
   /// note : When opening the file containing this class with doubleclick instead 'View Code',
   ///    you receive a designer warning. For comments on this, read issue 20130717°1131.
   /// </remarks>
   public sealed class CouchDBDataAdapter : System.Data.Common.DbDataAdapter
   {
      /// <summary>This constructor creates a new CouchDBDataAdapter object</summary>
      /// <remarks>id : 20130716°0941</remarks>
      /// <param name="cmd">The CouchDBCommand for which the CouchDBDataAdapter is wanted</param>
      public CouchDBDataAdapter(CouchDBCommand cmd)
      {
      }
   }

   /// <summary>
   /// This class provides data for the CouchDBConnection.InfoMessage event.
   ///  This class cannot be inherited.
   /// </summary>
   /// <remarks>
   /// id : 20130716°0951
   /// note :  This class represents a CouchDBInfoMessageEventArgs object. It is
   ///    implemented proforma, to satisfy syntax, with no functionality so far.
   /// note : For CouchDB, there is no third party library offering a *DBCommand class,
   ///    as with the other databases. But I want such class, at least proforma, to keep
   ///    the code parallel other implementations. This class is experimental, I have no
   ///    idea, how it will develop. The class definition line with the interfaces,
   ///    as well as the members to suffice syntax in the first attempt, are written
   ///    after 'System.Data.OleDb::OleDbInfoMessageEventArgs'.
   /// </remarks>
   public sealed class CouchDBInfoMessageEventArgs : EventArgs
   {
      /// <summary>This property gets the full text of the error sent from the data source</summary>
      /// <remarks>
      /// id : 20130716°0952
      /// note : This property implements the inherited abstract member Message.
      /// </remarks>
      /// <return>The full text of the error.</return>
      public string Message { get { return _message; } }

      /// <summary>This field stores the full text of the error sent from the data source</summary>
      /// <remarks>
      /// id : 20130716°0953
      /// note : This field helps to implements the inherited abstract member Message.
      /// </remarks>
      private string _message = "";

   }

   /// <summary>
   /// This class represents a CouchDB command. It is implemented
   ///  proforma, to satisfy syntax, with no functionality so far.
   /// </summary>
   /// <remarks>
   /// id : 20130716°0921
   /// note : For CouchDB, there is no third party library offering a DBCommand class,
   ///    as with the other databases. But I want such class, at least proforma, to keep
   ///    the code parallel other implementations. This class is experimental, I have no
   ///    idea, how it will develop. The class definition line with the interfaces,
   ///    as well as the members to suffice syntax in the first attempt, are written
   ///    after 'System.Data.SQLite::SQLiteCommand'.
   /// </remarks>
   public sealed class CouchDBCommand : System.Data.Common.DbCommand, ICloneable
   {
      /// <summary>This method implements the inherited abstract member Cancel()</summary>
      /// <remarks>id : 20130716°0922</remarks>
      public override void Cancel()
      {
      }

      /// <summary>This method implements the inherited abstract member Clone()</summary>
      /// <remarks>id : 20130716°0923</remarks>
      public object Clone()
      {
         object oRet = null;
         return oRet;
      }

      /// <summary>This method implements the inherited abstract member CreateDbParameter()</summary>
      /// <remarks>id : 20130716°0924</remarks>
      protected override System.Data.Common.DbParameter CreateDbParameter()
      {
         System.Data.Common.DbParameter dbParameter = null;

         return dbParameter;
      }

      /// <summary>This method implements the inherited abstract member ExecuteDbDataReader()</summary>
      /// <remarks>id : 20130716°0925</remarks>
      protected override System.Data.Common.DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
      {
         System.Data.Common.DbDataReader dbDataReader = null;

         return dbDataReader;
      }

      /// <summary>This method implements the inherited abstract member ExecuteNonQuery()</summary>
      /// <remarks>id : 20130716°0926</remarks>
      public override int ExecuteNonQuery()
      {
         int iRet = 0;

         return iRet;
      }

      /// <summary>This method implements the inherited abstract member ExecuteScalar()</summary>
      /// <remarks>id : 20130716°0927</remarks>
      public override object ExecuteScalar()
      {
         object oRet = null;
         return oRet;
      }

      /// <summary>This method implements the inherited abstract member Prepare()</summary>
      /// <remarks>id : 20130716°0928</remarks>
      public override void Prepare()
      {
      }


      /// <summary>This property implements the inherited abstract member CommandText</summary>
      /// <remarks>id : 20130716°0929</remarks>
      public override string CommandText { get; set; }

      /// <summary>This property implements the inherited abstract member CommandTimeout</summary>
      /// <remarks>id : 20130716°0930</remarks>
      public override int CommandTimeout { get; set; }

      /// <summary>This property implements the inherited abstract member CommandType</summary>
      /// <remarks>id : 20130716°0931</remarks>
      public override CommandType CommandType { get; set; }

      /// <summary>This property implements the inherited abstract member DesignTimeVisible</summary>
      /// <remarks>id : 20130716°0932</remarks>
      public override bool DesignTimeVisible { get; set; }

      /// <summary>This property implements the inherited abstract member DbConnection</summary>
      /// <remarks>id : 20130716°0933</remarks>
      protected override System.Data.Common.DbConnection DbConnection { get; set; }

      /// <summary>This property implements the inherited abstract member DbParameterCollection</summary>
      /// <remarks>id : 20130716°0934</remarks>
      protected override System.Data.Common.DbParameterCollection DbParameterCollection { get { return _dbParameterCollection; } }

      /// <summary>This property implements the inherited abstract member DbTransaction</summary>
      /// <remarks>id : 20130716°0935</remarks>
      protected override System.Data.Common.DbTransaction DbTransaction { get; set; }

      /// <summary>This property implements the inherited abstract member UpdatedRowSource</summary>
      /// <remarks>id : 20130716°0936</remarks>
      public override UpdateRowSource UpdatedRowSource { get; set; }

      /// <summary>This field helps to implement the inherited abstract member DbParameterCollection</summary>
      /// <remarks>id : 20130716°0937</remarks>
      private System.Data.Common.DbParameterCollection _dbParameterCollection = null;

   }


   /// <summary>
   /// This class represents a CouchDB connection. It is implemented
   ///  proforma, to satisfy syntax, with no functionality so far.
   /// </summary>
   /// <remarks>
   /// id : 20130716°0901
   /// note : For CouchDB, there is no third party library offering a *Connection class,
   ///    as with the other databases. But I want such class, at least proforma, to keep
   ///    the code parallel other implementations. This class is experimental, I have no
   ///    idea, how it will develop. The class definition line with the interfaces,
   ///    as well as the members to suffice syntax in the first attempt, are written
   ///    after 'System.Data.SQLite::SQLiteConnection'.
   /// </remarks>
   public sealed class CouchDBConnection : System.Data.Common.DbConnection, ICloneable
   {
      /// <summary>This method implements the inherited abstract member BeginDbTransaction()</summary>
      /// <remarks>id : 20130716°0902</remarks>
      /// <returns>The wanted DbTransaction object</returns>
      protected override System.Data.Common.DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
      {
         System.Data.Common.DbTransaction dbtransaction = null;

         return dbtransaction;
      }

      /// <summary>This method implements the inherited abstract member ChangeDatabase()</summary>
      /// <remarks>id : 20130716°0903</remarks>
      /// <param name="sDatabaseName">The database name of the database wanted to be switched to</param>
      public override void ChangeDatabase(string sDatabaseName)
      {
      }

      /// <summary>This method implements the inherited abstract member Clone()</summary>
      /// <remarks>id : 20130716°0904</remarks>
      /// <returns>The wanted new clone of this CouchDBConnection</returns>
      public object Clone()
      {
         object o = new object();
         return o;
      }

      /// <summary>This method implements the inherited abstract member Close()</summary>
      /// <remarks>id : 20130716°0905</remarks>
      public override void Close()
      {
      }

      /// <summary>This method implements the inherited abstract member CreateDbCommand()</summary>
      /// <remarks>id : 20130716°0906</remarks>
      /// <returns>A DbCommand object</returns>
      protected override System.Data.Common.DbCommand CreateDbCommand()
      {
         System.Data.Common.DbCommand dbcommand = null;

         return dbcommand;
      }

      /// <summary>This method implements the inherited abstract member Open()</summary>
      /// <remarks>id : 20130716°0907</remarks>
      public override void Open()
      {
         return;
      }

      /// <summary>This property implements the inherited abstract member ConnectionString.get/set</summary>
      /// <remarks>id : 20130716°0908</remarks>
      public override string ConnectionString { get; set; }

      /// <summary>This property implements the inherited abstract member Database.get()</summary>
      /// <remarks>id : 20130716°0909</remarks>
      public override string Database { get { return _database; } }

      /// <summary>This property implements the inherited abstract member DataSource.get()</summary>
      /// <remarks>id : 20130716°0910</remarks>
      public override string DataSource { get { return _dataSource; } }

      /// <summary>This property implements the inherited abstract member ServerVersion.get()</summary>
      /// <remarks>id : 20130716°0911</remarks>
      public override string ServerVersion { get { return _serverVersion; } }

      /// <summary>This property implements the inherited abstract member State.get()</summary>
      /// <remarks>id : 20130716°0912</remarks>
      public override ConnectionState State { get { return _state; } }

      /*
      Note 20130719°0814 'assignment necessary to avoid compiler warning'
      Text : For some properties which seem not used, compiler warns 'is never assigned to'.
          But they are indeed necessary for the new System.Data.Couch classes.
      Location : SystemDataCouch.cs
      */

      /// <summary>This field helps implementing the inherited abstract member Database</summary>
      /// <remarks>
      /// id : 20130716°0913
      /// see : note 20130719°0814 'assignment necessary to avoid compiler warning'
      /// </remarks>
      private string _database = "";

      /// <summary>This field helps implementing the inherited abstract member DataSource</summary>
      /// <remarks>
      /// id : 20130716°0914
      /// see : note 20130719°0814 'assignment necessary to avoid compiler warning'
      /// </remarks>
      private string _dataSource = "";

      /// <summary>This field helps implementing the inherited abstract member ServerVersion</summary>
      /// <remarks>
      /// id : 20130716°0915
      /// see : note 20130719°0814 'assignment necessary to avoid compiler warning'
      /// </remarks>
      private string _serverVersion = "";

      /// <summary>This field helps implementing the inherited abstract member State</summary>
      /// <remarks>
      /// id : 20130716°0916
      /// see : note 20130719°0814 'assignment necessary to avoid compiler warning'
      /// </remarks>
      private ConnectionState _state = ConnectionState.Closed;
   }
}
