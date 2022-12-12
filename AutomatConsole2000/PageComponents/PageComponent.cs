using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents
{
    internal abstract class PageComponent
    {
        public string DisplayString { get { return GetDisplayData(); } }

        /// <summary>
        /// Use when components output should be updated 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDisplayData();
    }
}
