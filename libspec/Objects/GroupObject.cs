using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.Objects
{
    public class GroupObject : BaseObject
    {
        public GroupObject(object[] values) : base(values) {}
        public GroupObject()
        {
            obozn = DateTime.Now.GetHashCode().ToString();
            naimen = "новая группа";
            descr = "дата создания " + DateTime.Now.ToString();
        }
    }
}
