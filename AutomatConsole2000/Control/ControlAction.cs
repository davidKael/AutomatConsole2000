using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    internal class ControlAction
    {
        public string Name { get; private set; }

        public Action ControlFunction;

        public ControlAction(string name, Action controlFunction)
        {
            Name = name;
            ControlFunction = controlFunction;
        }
    }
}
