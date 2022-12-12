using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatConsole2000.Control;

namespace AutomatConsole2000.Helpers
{


    internal static class InputHandler
    {

        /// <summary>
        /// Helps for taking user input and sending it to the given Controller to trigger an action
        /// </summary>
        public static void HandleInput(Controller controller)
        {
            ConsoleKey input = Console.ReadKey().Key;

            controller.TriggerAction(input);

        }


        /// <summary>
        /// For making it easier to set all the values needed to map a ControlAction with a ConsoleKey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static KeyValuePair<ConsoleKey, ControlAction> CreateControl(ConsoleKey key, string name, Action action)
        {
            return new KeyValuePair<ConsoleKey, ControlAction>(key, new ControlAction(name, action));
        }
    }
}
