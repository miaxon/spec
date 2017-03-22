using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.View.Objects
{
    [Serializable()]
    public class ProjectObject : BaseObject
    {
        public ProjectObject(object[] values) : base(values) { }
        public ProjectObject()
            : base()
        {
            obozn = DateTime.Now.GetHashCode().ToString();
            naimen = "новый проект";
            descr = "дата создания " + DateTime.Now.ToString();
        }
    }
}
