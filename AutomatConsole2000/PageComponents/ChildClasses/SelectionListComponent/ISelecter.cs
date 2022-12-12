using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent
{
    internal interface ISelecter
    {
        void SelectObject();

        SelectionListComponent SelectionList { get; set; }
    }
}
