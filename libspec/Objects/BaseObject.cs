using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.Objects
{
    public enum Closed { N, Y };
    public class BaseObject
    {
        private UInt32 m_id;
        public UInt32 refid { get; set; } // uid field in _did record
        public string obozn { get; set; }
        public string naimen { get; set; }
        public string descr { get; set; }
        public Closed status { get; set; }
        public UInt32 id { get { return m_id; } }
        public virtual string Text { get { return obozn; } }
        public BaseObject(object[] values)
        {
            m_id = (UInt32)values[0];
            obozn = (string)values[1];
            naimen = (string)values[2];
            descr = (string)values[3];
            status = (Closed)Enum.Parse(typeof(Closed), (string)values[4]);

        }
        public BaseObject() { }
    }
}
