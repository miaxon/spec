using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libspec.ViewEvent
{
    public class SearchEventArgs : System.EventArgs
    {
        public readonly string search_string;
        public readonly int num_kod;
        public readonly string search_field;
        public SearchEventArgs(string searchField, string searchString, int numKod)
        {
            num_kod = numKod;
            search_string = searchString;
            search_field = searchField;
        }
    }
}
