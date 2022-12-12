using Automat_Console.Pages;
using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using System.ComponentModel.DataAnnotations;

namespace AutomatConsole2000.Pages.ChildClasses
{

    /// <summary>
    /// Initial page
    /// </summary>
    internal class StartPage : Page, ISelecter, IExitable
    {
        public override string? Title { get; } = "Main Menu";

        public TextComponent WelcomeText { get; set; }

        public SelectionListComponent SelectionList { get; set; }




        List<ListOption> _options;


        public StartPage()
        {

            _options = new List<ListOption>()
            {
                
                new ListOption("Go to store", new ChooseCategoryPage(this)),
                new ListOption("Insert Coins Into Machine", new InsertCoinsPage(this)),
                new ListOption("Go to Your Product Bag", new UserProductBagPage(this)),

            };

            WelcomeText = new TextComponent(name: "Welcome", text:"Welcome! What would you like to to?");
            SelectionList = new SelectionListComponent(_options);
            


            AddComponent(WelcomeText);
            AddComponent(SelectionList, true);

        }



        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();

            var focusedComp = GetFocusedComponent();

            if (focusedComp == SelectionList && SelectionList.OptionAtCurrIndex?.Obj is Page)
            {
                var select = InputHandler.CreateControl(ConsoleKey.Enter, "Select", SelectObject);

                output.Add(select.Key, select.Value);
            }

            var exit = InputHandler.CreateControl(ConsoleKey.Escape, "Exit", Exit);

            output.Add(exit.Key, exit.Value);

            return output;
        }

        public override void Refresh()
        {
            NextPage = null;
            SelectionList.SetValues(_options);

            base.Refresh();
        }


        public void SelectObject()
        {
            if (GetFocusedComponent() == SelectionList && SelectionList != null)
            {

                if (SelectionList?.OptionAtCurrIndex?.Obj is Page)
                {
                    NextPage = SelectionList.OptionAtCurrIndex.Obj as Page;

                }



            }
        }

        public void Exit()
        {
            NextPage = new ExitPage(this);
        }




    }

}



