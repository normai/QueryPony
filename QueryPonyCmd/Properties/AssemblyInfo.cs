#region Fileinfo
// file        : 20130619°1212 (20130604°0021) /QueryPony/QueryPonyLib/Properties/AssemblyInfo.cs
// summary     : This file stores the project attribute definitions.
// license     : GNU AGPL v3
// copyright   : © 2013 - 2022 Norbert C. Maier
// authors     : ncm
// encoding    : UTF-8-with-BOM
// status      : Applicable
// note        :
// callers     :
#endregion Fileinfo
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("QueryPonyCmd")]
[assembly: AssemblyDescription("QueryPonyCmd is the textual interface for QueryPonyLib")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("www.trekta.biz")]
[assembly: AssemblyProduct("QueryPonyCmd")]
[assembly: AssemblyCopyright("© 2013 - 2022 Norbert C. Maier")]
[assembly: AssemblyTrademark ("Trekta®")]
[assembly: AssemblyCulture ("")]
// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible (false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid ("5dbf1ca7-5fce-4cd8-a216-226861e51587")]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
//[assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("1.0.0.0")]
//[assembly: AssemblyVersion("2.1.1.*")]
//...
//[assembly: AssemblyVersion("0.3.4.*")]
[assembly: AssemblyVersion("0.4.1.*")]

// Manually set versions
//[assembly: AssemblyFileVersion("1.0.0.0")]   // New VS project
//[assembly: AssemblyFileVersion("2.1.1")]     // Tag 20130624°1331
//[assembly: AssemblyFileVersion("2.1.1.1")]   // Intermediate
//[assembly: AssemblyFileVersion("2.1.2")]     // Tag 20130630°2000
//[assembly: AssemblyFileVersion("2.1.2.1")]   // Intermediate
//[assembly: AssemblyFileVersion("0.1.0")]     // Version 20130707°1051 (single-file-delivery proof-of-concept)
//[assembly: AssemblyFileVersion("0.2.0")]     // Version 0.2.0 Crashtest Release (20130710°0123)
//[assembly: AssemblyFileVersion("0.2.1.0")]   // Intermediate
//[assembly: AssemblyFileVersion("0.3.0")]     // Version 0.3.0.40616 Alpha Release (20130726°1551)
//[assembly: AssemblyFileVersion("0.3.1.0")]   // Intermediate
//[assembly: AssemblyFileVersion("0.3.1.0")]   // Version 0.3.1.42329 Alpha Release (20130810°1956)
//[assembly: AssemblyFileVersion("0.3.2.1")]   // Intermediate (20130912°2121)
//[assembly: AssemblyFileVersion("0.3.4.0")]   // Last before refactoring to NET4.8/x64
//[assembly: AssemblyFileVersion("0.4.0.0")]   // version 20220805°1511 Restored
[assembly: AssemblyFileVersion("0.4.1.0")]     // version 20220806°0931 On-the-road-again

// custom attributes
[assembly: GlobalCustomAttributes.AssemblyPluginTestAttribute (GlobalCustomAttributes.AssemblyPluginTestType.Library)]
[assembly: GlobalCustomAttributes.CustomAuthorsAttribute (Authors = "Joseph Albahari, Timor Fanshteyn, Christian S., dl3bak, klaus3b, Norbert C. Maier")]
