
using AutomatConsole2000.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Control
{
    internal class Controller
    {
        public Dictionary<ConsoleKey, ControlAction> Mapped { get; set; } = new Dictionary<ConsoleKey, ControlAction>();


        void AddControl(ConsoleKey key, string name, Action action)
        {

            var control = InputHandler.CreateControl(key, name, action);

            if (Mapped.ContainsKey(control.Key))
            {
                Mapped[control.Key] = control.Value;
            }
            else
            {
                Mapped.Add(control.Key, control.Value);
            }


        }

        public void TriggerAction(ConsoleKey key)
        {
            if (Mapped.ContainsKey(key))
            {

                Mapped[key].ControlFunction.Invoke();
            }
        }


        public void Clear()
        {
            Mapped.Clear();
        }


        /// <summary>
        /// For inserting controlactions with keys into Controller
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

        public string GetControlsForDisplay()
        {
            string output = string.Empty;


            string[] keys = Mapped.Keys.Select(k => k.ToString() +":").ToArray();
            string[] values = Mapped.Values.Select(v => v.Name).ToArray();

            string[] formated = ConsoleHelper.EvenSpacesRight(keys, 3);

            for (int i =0; i < Mapped.Count; i++)
            {
                output += $"{formated[i]}[{values[i]}]\n";
            }

            return output;
        }
    }
}
