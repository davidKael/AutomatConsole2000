using Automat_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents.ChildClasses
{
    /// <summary>
    /// A textComponent that specificly displays MachineBalance 
    /// </summary>
    internal class MachineBalanceComp : TextComponent
    {
        public override string Text { get { return GetDisplayData(); } }
        Store? _store;

        public MachineBalanceComp(Store? store, string name = "")
        {
            Name = name;
            _store= store;
        }

        protected override string GetDisplayData()
        {
            string text;

            //if no store is set it returns null, otherwise it returns the current machine balance
            if (_store == null)
            {
                text = "0";
            }
            else
            {
                text = $"Machine Balance: {_store.MachineBalance}";
            }
      
            return text;
        }
    }
}
