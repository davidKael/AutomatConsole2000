using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    /// <summary>
    /// Necessary for reading a PageConmponents controls
    /// </summary>
    internal interface IControllable
    {
        /// <summary>
        /// For getting all controls on an IControllable Component
        /// </summary>
        /// <returns></returns>
        Dictionary<ConsoleKey, ControlAction> GetControls();


        /// <summary>
        /// For setting the controls on a IControllable Component
        /// </summary>
        void SetControls();
    }
}
