using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.Objects
{
    public class KidObject
    {
        private UInt32 m_id;
        public string obozn { get; set; }
        public string naimen { get; set; }
        public UInt32 id { get { return m_id; } }
        public KidObject(object[] values)
        {
            m_id = (UInt32)values[0];
            obozn = (string)values[1];
            naimen = (string)values[2];
        }
        public KidObject()
        {
            obozn = DateTime.Now.GetHashCode().ToString();
            naimen = "?????";
        }
    }
}
