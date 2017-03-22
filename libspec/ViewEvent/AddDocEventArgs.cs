using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libspec.View.Objects;

namespace libspec.View.ViewEvent
{
    public class AddDocEventArgs : System.EventArgs
    {
        public readonly DocObject Doc;
        public readonly GroupObject Group;
        public readonly ProjectObject Project;
        public AddDocEventArgs(DocObject d, GroupObject g, ProjectObject p)
        {
            Doc = d;
            Group = g;
            Project = p;
        }
    }
}
