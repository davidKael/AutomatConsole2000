using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;


namespace AutomatConsole2000.Pages.ChildClasses
{
    /// <summary>
    /// Page to use when user whant to exit the application
    /// </summary>
    internal class ExitPage : Page, ISelecter
    {
        public override string? Title => "Quit Application";

        public TextComponent MessageComponent { get; set; }
        public SelectionListComponent SelectionList { get; set; }

        Page? ReturnPage;
        List<ListOption> _options;

        public bool IsExiting { get; private set; } = false;

        public ExitPage(Page? returnPage = null)
        {
            ReturnPage = returnPage;

            //the options given are yes or no, yes don't have an object attached, and no has the return page
            _options = new List<ListOption>()
            {
                new ListOption("Yes", null),
                new ListOption("No", ReturnPage),
            };

            //text component to make sure the user understand what is happening
            MessageComponent = new TextComponent("Exit Message", "Do you really want to quit?");

            //selection list to store all the options the user have
            SelectionList = new SelectionListComponent(_options);

            AddComponent(MessageComponent);
            AddComponent(SelectionList, true);
        }

        public override void Refresh()
        {
            NextPage = null;
            SelectionList.SetValues(_options);

            base.Refresh();
        }

        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();

            //add the control to select an object in list
            var select = InputHandler.CreateControl(ConsoleKey.Enter, "Select", SelectObject);

            output.Add(select.Key, select.Value);


            return output;
        }

        /// <summary>
        /// When selecting an option in list
        /// </summary>
        public void SelectObject()
        {
            //if selection is another page it means the user wants to go back
            if (SelectionList?.OptionAtCurrIndex?.Obj is Page)
            {
                NextPage = SelectionList.OptionAtCurrIndex.Obj as Page;
            }
            //otherwise the last page has been reached and the next page is null
            else
            {
                IsExiting = true;
                NextPage = null;
            }
        }
    }
}
