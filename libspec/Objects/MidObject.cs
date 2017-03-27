using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.Objects
{
    public class MidObject
    {
        private UInt32 m_id;
        private Int32 m_num_kod;
        public UInt32 refid { get; set; } // uid field in _did record
        public string obozn { get; set; }
        public string naimen { get; set; }
        public string descr { get; set; }
        public UInt32 id { get { return m_id; } }
        public Int32 num_kod { get { return m_num_kod; } }
        public virtual string Text { get { return obozn; } }
        public MidObject(object[] values, int kod)
        {
            m_id = (UInt32)values[0];
            obozn = (string)values[1];
            naimen = (string)values[2];
            descr = (string)values[3];
            m_num_kod = kod;
        }
        public MidObject() { }
    }
}
