using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueryBuilder
{
    public class FluentObject
    {
        private readonly Dictionary<string, string> _fields; 


        public string Name { get; set; }

        public string this[string key]
        {
            get { return _fields[key]; }
            set { _fields[key] = value; }
        }

        public int Count
        {
            get { return _fields.Count; }
        }

        public FluentObject()
        {
            _fields = new Dictionary<string, string>();
        }

        public FluentObject(string name) : this()
        {
            Name = name;
        }

        public FluentObject(string name, Dictionary<string, string> values) : this(name)
        {
            _fields = values;
        }


        public bool ContainsKey(string key)
        {
            return _fields.ContainsKey(key);
        }

        public void Add(string key, string value)
        {
            _fields.Add(key, value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);

            foreach (var field in _fields)
            {
                builder.AppendFormat("{0}:{1}{2}", field.Key, field.Value, Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
