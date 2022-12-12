using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    internal interface IControllable
    {
        Dictionary<ConsoleKey, ControlAction> GetControls();

        void SetControls();
    }
}
