using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.ViewEvent
{
    public class ButtonActionEventArgs : EventArgs
    {
        public readonly ButtonAction Action;
        public ButtonActionEventArgs(ButtonAction action, object target = null)
        {
            Action = action;
        }
    }
}
