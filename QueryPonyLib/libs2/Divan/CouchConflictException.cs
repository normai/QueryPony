// id          : 20130717°0141
// encoding    : UTF-8-with-BOM

using System;

namespace Divan
{
    /// <summary>
    /// Represents a CouchDB HTTP 409 conflict.
    /// </summary>
    public class CouchConflictException : Exception
    {
        public CouchConflictException(string msg, Exception e) : base(msg, e)
        {
        }
    }
}
