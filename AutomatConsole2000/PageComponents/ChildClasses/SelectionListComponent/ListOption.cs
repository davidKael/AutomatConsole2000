using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent
{
    internal class ListOption
    {
        public string Text { get; }
        public object? Obj { get; }


        public ListOption(string text, object? obj = null)
        {
            Text = text;
            Obj = obj;

        }

    }
}
