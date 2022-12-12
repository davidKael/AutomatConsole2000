using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Helpers
{
    internal static class ConsoleHelper
    {
        public static string Multiply(string str, int times)
        {
            return string.Join(string.Empty, Enumerable.Repeat(str, times).ToArray());
        }

        public static string Line()
        {
            return Multiply("-", Console.WindowWidth) + "\n";
        }

        public static string[] EvenSpacesRight(string[] strs, int gap) 
        {
            int longestStr = 0;

            foreach(string s in strs)
            {
                if (longestStr < s.Length) longestStr = s.Length;
            }

            int totalLength = longestStr + gap;

            for(int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i] + Multiply(" ", totalLength - strs[i].Length);
            }

            return strs;
        }



    }
}
