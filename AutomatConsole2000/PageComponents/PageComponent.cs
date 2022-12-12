using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.PageComponents
{
    /// <summary>
    /// Inherit this abstract to class if it needs to be recoginzed as a PageComponent
    /// </summary>
    internal abstract class PageComponent
    {
        /// <summary>
        /// String for displaying it's content
        /// </summary>
        public string DisplayString { get { return GetDisplayData(); } }

        /// <summary>
        /// Use to control how components output should be updated 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDisplayData();
    }
}
