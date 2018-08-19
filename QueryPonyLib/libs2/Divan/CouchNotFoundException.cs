// id          : 20130717°0301
// encoding    : UTF-8-with-BOM

using System;

namespace Divan
{
    /// <summary>
    /// Represents a HttpStatusCode of 404, document not found.
    /// </summary>
    public class CouchNotFoundException : Exception
    {
        public CouchNotFoundException(string msg, Exception e) : base(msg, e)
        {
        }
    }
}
