using Automat_Console;
using AutomatConsole2000.Control;
using AutomatConsole2000.Pages.ChildClasses;


namespace AutomatConsole2000.Pages
{
    internal static class PageHandler
    {
        static public void Run()
        {
            UserSession.SetUpSession();

            Page currPage = new StartPage();

           


            bool isExiting = false;

            while (!isExiting)
            {

                //updates and sets all values in page
                currPage.Refresh();

                //builds and render page
                PageBuilder.BuildPage(currPage);
               
                //reads input and executes command
                InputHandler.HandleInput(currPage.Controller.Mapped);

                //processes unpdated values if needed and return new page if any
                var nextPage = currPage.GetNextPage();

                //if response is returns page, a new page is choosen
                if (nextPage != null)
                {
                    currPage = nextPage;


                }


                if (currPage is ExitPage)
                {
                    isExiting = ((ExitPage)currPage).IsExiting;
                }



            }
            Console.Clear();

            Console.WriteLine("Closing...");
            Thread.Sleep(1000);

        }
    }


}
