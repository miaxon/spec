using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedDataGridView;

namespace libspec.View.ViewEvent
{
    public class NodeEditEventArgs : System.EventArgs
    {
        public readonly object Object;
        public readonly string Field;
        public readonly object Value;
        public readonly object OldValue;
        public NodeEditEventArgs(object o, string field, object value, object old_value)
        {
            Object = o;
            Field = field;
            Value = value;
            OldValue = old_value;
        }
    }
}
