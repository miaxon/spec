using libspec.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedDataGridView;

namespace libspec.ViewEvent
{
    public class ButtonActionEventArgs : EventArgs
    {
        public readonly ButtonAction Action;
        public readonly TreeGridNode Target;
        public ButtonActionEventArgs(ButtonAction action, TreeGridNode target = null)
        {
            Action = action;
            Target = target;
        }
    }
}
