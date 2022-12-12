using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents.ChildClasses
{
    internal class TextComponent : PageComponent
    {
        public virtual string Text { get; set; }
        public string Name { get; protected set; }

        public TextComponent(string name = "", string text = "")
        {
            Name = name;
            Text = text;
        }

        protected override string GetDisplayData()
        {
            return Text;
        }
    }
}
