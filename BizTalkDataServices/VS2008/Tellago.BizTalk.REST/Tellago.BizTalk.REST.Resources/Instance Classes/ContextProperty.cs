using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tellago.BizTalk.REST.Resources
{
    public class ContextProperty
    {
        private string _id;
        private string _name;
        private string _namespace;
        private string _value;
        private bool _isPromoted;

        public ContextProperty()
        { }

        public ContextProperty(string name, string ns, string value, bool ispromoted)
        {
            _id = ns + "#" + name;
            _name = name;
            _namespace = ns;

            //if (value is Array)
            //{
            //    _value = ((Array)value).GetValue(0).ToString();
            //    for (int index = 1; index <= ((Array)value).Length - 1; index++)
            //        _value += "," + ((Array)value).GetValue(index).ToString();
            //}
            //else

            _value = value;
            _isPromoted = ispromoted;
        }

        public string ID { get { return _id; } }
        public string Name { get { return _name; } }
        public string Namespace { get { return _namespace; } }
        public string Value { get { return _value; } }
        public bool IsPromoted { get { return _isPromoted; } }

    }
}
