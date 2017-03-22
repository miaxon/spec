using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.Objects
{
    public class DocObject : BaseObject
    {
        public UInt16 num_kol { get; set; }        
        
        public override string Text
        {
            get { return (num_kol == 1) ? obozn : string.Format("{0} ({1})", obozn, num_kol); }
        }
        public DocObject(object[] values)
            : base(values)
        {
            num_kol = (UInt16)values[5];
            refid = (UInt32)values[6];

        }
        public DocObject()
            : base()
        {
            obozn = DateTime.Now.GetHashCode().ToString();
            naimen = "новый документ";
            descr = "дата создания " + DateTime.Now.ToString();
        }


    }
}
