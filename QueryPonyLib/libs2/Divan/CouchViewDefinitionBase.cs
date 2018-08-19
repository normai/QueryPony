﻿// id          : 20130717°0421
// encoding    : UTF-8-with-BOM

namespace Divan
{
    public abstract class CouchViewDefinitionBase : Divan.ICouchViewDefinitionBase
    {
        public CouchDesignDocument Doc { get; set; }
        public string Name { get; set; }

        protected CouchViewDefinitionBase(string name, CouchDesignDocument doc)
        {
            Doc = doc;
            Name = name;
        }

        public ICouchDatabase Db()
        {
            return Doc.Owner;
        }

        public ICouchRequest Request()
        {
            return Db().Request(Path());
        }

        public virtual string Path()
        {
            if (Doc.Id == "_design/")
            {
                return Name;
            }
            return Doc.Id + "/_view/" + Name;
        }
    }
}
