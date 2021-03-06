﻿// id          : 20130717°0341
// encoding    : UTF-8-with-BOM

using Newtonsoft.Json.Linq;

namespace Divan
{
    public class CouchRecord<T> where T: ICanJson, new()
    {
        private readonly JObject record;

        public CouchRecord(JObject source)
        {
            record = source;

            Id = record.Value<string>("id");
            Key = record["key"];
            Value = record["value"];
        }

        public string Id { get; private set; }
        public JToken Key { get; private set; }
        public JToken Value { get; private set; }

        public T Document
        {
            get
            {
                JToken val;
                if (!record.TryGetValue("doc", out val))
                {
                    return default(T);
                }

                var doc = val as JObject;
                if (doc == null)
                {
                    return default(T);
                }

                var ret = new T();
                ret.ReadJson(doc);
                return ret;
            }
        }
    }
}
