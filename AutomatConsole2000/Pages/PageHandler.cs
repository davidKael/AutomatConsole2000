using Automat_Console;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.Pages.ChildClasses;


namespace AutomatConsole2000.Pages
{

    /// <summary>
    /// Use to run pages
    /// </summary>
    internal static class PageHandler
    {

        /// <summary>
        /// To Initiate PageHandler
        /// </summary>
        static public void Run()
        {
            UserSession.SetUpSession();

            //sets start page to StartPage
            Page currPage = new StartPage();

           

            
            bool isExiting = false;


            //when isExiting true, the handler will leave the loop and end the program
            while (!isExiting)
            {

                //updates and sets all values in page
                currPage.Refresh();

                //builds and render page
                PageBuilder.BuildPage(currPage);
               
                //reads input and triggers command
                InputHandler.HandleInput(currPage.Controller);

                //processes unpdated values if needed and return new page if any
                var nextPage = currPage.GetNextPage();

                //if response is returns page, a next page is choosen
                if (nextPage != null)
                {
                    currPage = nextPage;


                }

                //if the current page is an Exit page, it check if the Exitpage is Exiting
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
