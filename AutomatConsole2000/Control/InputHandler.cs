using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    internal static class InputHandler
    {
        public static void HandleInput(Dictionary<ConsoleKey, ControlAction> controls)
        {
            ConsoleKey input = Console.ReadKey().Key;

            if (controls.ContainsKey(input))
            {
                controls[input].ControlFunction.Invoke();
            }

        }

        public static KeyValuePair<ConsoleKey, ControlAction> CreateControl(ConsoleKey key, string name, Action action)
        {
            return new KeyValuePair<ConsoleKey, ControlAction>(key, new ControlAction(name, action));
        }
    }
}
