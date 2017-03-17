using libspec.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.ViewEvent
{
    public class ButtonActionEventArgs : EventArgs
    {
        public readonly ButtonAction Action;
        public readonly BaseObject Target;
        public ButtonActionEventArgs(ButtonAction action, BaseObject target = null)
        {
            Action = action;
            Target = target;
        }
    }
}
