
using AutomatConsole2000.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    /// <summary>
    /// Put this in a page to store all the page's conotrolactions and map them to a console key
    /// </summary>
    internal class Controller
    {
        //this is where the ControlAction get stored as the value and the key is it's ConsoleKey
        public Dictionary<ConsoleKey, ControlAction> Mapped { get; private set; } = new Dictionary<ConsoleKey, ControlAction>();


        /// <summary>
        /// Takes a ConsoleKey, a name, and an action, and adds it to dictionary Mapped as an ControlAction with ConsoleKey as the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        void AddControl(ConsoleKey key, string name, Action action)
        {
            
            var control = InputHandler.CreateControl(key, name, action);


            //if Mapped already contains a value with the same key it gets replaced
            if (Mapped.ContainsKey(control.Key))
            {
                Mapped[control.Key] = control.Value;
            }
            else
            {
                Mapped.Add(control.Key, control.Value);
            }


        }


        /// <summary>
        /// Invokes the Action mapped to given ConsoleKey
        /// </summary>
        /// <param name="key"></param>
        public void TriggerAction(ConsoleKey key)
        {
            if (Mapped.ContainsKey(key))
            {
                Mapped[key].ControlFunction.Invoke();
            }
        }

        /// <summary>
        /// Clears Dictonary Mapped
        /// </summary>
        public void Clear()
        {
            Mapped.Clear();
        }


        /// <summary>
        /// For inserting multiple controlactions with keys into Controller
        /// </summary>
        /// <param name="newControls">Dictionary with mapped controlactions to add to controller</param>
        /// <param name="refresh">Clears Controller if true</param>
        public void SetControls(Dictionary<ConsoleKey, ControlAction> newControls, bool refresh = false)
        {
            if (refresh) Clear();

            foreach (var control in newControls)
            {
                AddControl(control.Key, control.Value.Name, control.Value.ControlFunction);
            }

        }


        /// <summary>
        /// Returns all the currently mapped controls as a readable string to display for user
        /// </summary>
        /// <returns></returns>
        public string GetControlsForDisplay()
        {
            string output = string.Empty;

            //not a pretty solution, but sepparates the value names and keys from mapped into 2 arrayes
            string[] keys = Mapped.Keys.Select(k => k.ToString()).ToArray();
            string[] values = Mapped.Values.Select(v => v.Name + ":").ToArray();


            //sends the value names to EvenSpacesRight in ConsoleHelper to get an even spacegap on the right side of them
            string[] formated = ConsoleHelper.EvenSpacesRight(values, 3);

            //then merges the formated keys and values again 
            for (int i =0; i < Mapped.Count; i++)
            {
                output += $"{formated[i]}[{keys[i]}]\n";
            
            }

            // to returns it for example:
            //"Select:   [Enter]\n"
            //"Exit:     [Escape]\n"
            return output;
        }
    }
}
