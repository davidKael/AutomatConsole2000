using AutomatConsole2000.Control;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using AutomatConsole2000.Pages;
using AutomatConsole2000.Pages.ChildClasses;


namespace Automat_Console.Pages
{
    internal class ChooseCategoryPage : Page, IReDirecter, IExitable, ISelecter
    {
        public override string Title { get; } = "Store";

      

        public TextComponent ListText { get; set; } = new TextComponent(text:"Choose Category:");

        public SelectionListComponent SelectionList { get; set; }


        Page? ReturnPage;

        List<ListOption> _options = new List<ListOption>();

        public ChooseCategoryPage(Page? returnPage = null)
        {
            ReturnPage= returnPage;

            

            
            SelectionList = new SelectionListComponent(_options, "Categories");

            AddComponent(ListText);
            AddComponent(SelectionList, true);
            Refresh();
        }


        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();

            var focusedComp = GetFocusedComponent();

            if (focusedComp == SelectionList)
            {
                if (SelectionList.OptionAtCurrIndex?.Obj == ReturnPage)
                {
                    var goTo = InputHandler.CreateControl(ConsoleKey.Enter, "Back", Redirect);

                    output.Add(goTo.Key, goTo.Value);
                }



                else if (SelectionList?.OptionAtCurrIndex?.Text != "")
                {
                    var category = InputHandler.CreateControl(ConsoleKey.Enter, "Select", SelectObject);

                    output.Add(category.Key, category.Value);
                }

            }

            var exit = InputHandler.CreateControl(ConsoleKey.Escape, "Exit", Exit);

            output.Add(exit.Key, exit.Value);

            return output;
        }

        public void Return()
        {
            NextPage = ReturnPage;
        }

        public void SelectObject()
        {
            var category = SelectionList?.OptionAtCurrIndex?.Text;

            if(category != null)
            {
                NextPage = new DisplayProducts(category, this);
            }
           


        }

        public void Exit()
        {
            NextPage = new ExitPage(this);
        }

        public override void Refresh()
        {
            _options.Clear();
            NextPage = null;

        
            List<String> Categoires = UserSession.SessionStore.GetProductCategoryNames().ToList();

            Categoires.ForEach(option => { _options.Add(new ListOption(option)); });

            _options.Add(new ListOption("Back", ReturnPage));

            SelectionList.SetValues(_options);

            base.Refresh();
        }

        public void Redirect()
        {
            var page = SelectionList?.OptionAtCurrIndex?.Obj as Page;

            if (page is Page)
            {
                NextPage = page;
            }
        }
    }


    
}
