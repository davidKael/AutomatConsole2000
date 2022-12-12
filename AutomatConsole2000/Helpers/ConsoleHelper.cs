using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Helpers
{

    /// <summary>
    /// Helps with common problems in the console
    /// </summary>
    internal static class ConsoleHelper
    {

        /// <summary>
        /// For multiplying a tring for a given number of times
        /// </summary>
        /// <param name="str"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string Multiply(string str, int times)
        {
            return string.Join(string.Empty, Enumerable.Repeat(str, times).ToArray());
        }

        /// <summary>
        /// Returns a line 
        /// </summary>
        /// <returns></returns>
        public static string Line()
        {
            return Multiply("-", Console.WindowWidth) + "\n";
        }


        /// <summary>
        /// Takes an array of strings and adds spaces accordingly to all the length, so that they all get the same length
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="gap">Number of spaces added to the longest string</param>
        /// <returns></returns>
        public static string[] EvenSpacesRight(string[] strs, int gap) 
        {

            int longestStr = 0;

            //finds out the length of the longest string in array
            foreach(string s in strs)
            {
                if (longestStr < s.Length) longestStr = s.Length;
            }

            //adds the gap number to it
            int totalLength = longestStr + gap;


            //goes through all the strings and adds space according to it's own length
            for(int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i] + Multiply(" ", totalLength - strs[i].Length);
            }

            return strs;
        }



    }
}
