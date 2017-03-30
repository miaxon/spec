using libspec.View.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedDataGridView;

namespace libspec.View.ViewEvent
{
    public class ButtonActionEventArgs : EventArgs
    {
        public readonly ButtonAction Action;
        public readonly object Target;
        public readonly object Data;
        public ButtonActionEventArgs(ButtonAction action, object target = null, object data = null)
        {
            Action = action;
            Target = target;
            Data = data;
        }
    }
}
