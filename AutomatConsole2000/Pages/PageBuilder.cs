using AutomatConsole2000;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.PageComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Pages
{
    internal static class PageBuilder
    {


        public static void BuildPage(Page page)
        {


            Console.Clear();

            StringBuilder output = new StringBuilder();


            output.Append(Header());

            if (page != null)
            {
                output.Append(PageHeader(page.Title));

                output.Append(Body(page.Content.ToArray()));

                if (page.ShowControls)
                {
                    output.Append(Footer(page.Controller.GetControlsForDisplay()));
                }


            }

            else
            {
                output.Append("Something went wrong ..");
            }

            Console.Write(output.ToString());
        }



        static string Header()
        {
            return
              ConsoleHelper.Line()
            + $"\t\t\t\tConsole Automat 2000\n"
            + ConsoleHelper.Line();
        }

        static string PageHeader(string? pageTitle)
        {
            if (pageTitle != null && pageTitle.Length > 0)
            {
                return pageTitle + "\n" + ConsoleHelper.Line() + "\n\n";
            }

            return "";

        }

        static string Body(PageComponent[] content)
        {
            string allContent = "";

            foreach (PageComponent item in content)
            {

                allContent += item.DisplayString + ConsoleHelper.Multiply("\n",3);
            }

            if (allContent.Length > 0)
            {
                return allContent + ConsoleHelper.Multiply("\n", 2);
            }
            else
            {
                return "[No Content]" + ConsoleHelper.Multiply("\n", 3);
            }

        }

        static string Footer(string str)
        {
            string output = "\n" + ConsoleHelper.Line() + str;

            return output;

        }




    }
}
