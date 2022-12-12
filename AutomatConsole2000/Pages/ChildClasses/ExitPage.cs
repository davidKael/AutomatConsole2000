using AutomatConsole2000.Control;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;


namespace AutomatConsole2000.Pages.ChildClasses
{
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

            _options = new List<ListOption>()
            {
                new ListOption("Yes", null),
                new ListOption("No", ReturnPage),
            };


            MessageComponent = new TextComponent("Exit Message", "Do you really want to quit?");
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


            var select = InputHandler.CreateControl(ConsoleKey.Enter, "Select", SelectObject);

            output.Add(select.Key, select.Value);


            return output;
        }


        public void SelectObject()
        {
            if (SelectionList?.OptionAtCurrIndex?.Obj is Page)
            {
                NextPage = SelectionList.OptionAtCurrIndex.Obj as Page;
            }
            else
            {
                IsExiting = true;
                NextPage = null;
            }
        }
    }
}
