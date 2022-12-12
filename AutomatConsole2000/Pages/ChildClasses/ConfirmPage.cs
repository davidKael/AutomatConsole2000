using AutomatConsole2000.Control;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Pages.ChildClasses
{
    internal class ConfirmPage : Page, IReDirecter
    {
        public override string? Title { get; }
        public SelectionListComponent SelectionList { get; set; }

        Page YesRedirect;
        Page NoRedirect;

        TextComponent TextMessage;

        List<ListOption> _options = new List<ListOption>();

        public ConfirmPage(Page yesRedirect,Page noRedirect, string? title = "", string message = "")
        {
            Title = title;
            YesRedirect= yesRedirect;
            NoRedirect= noRedirect;

            TextMessage = new TextComponent(text:message);

           _options.Add(new ListOption("Ok", YesRedirect));
           _options.Add(new ListOption("No", NoRedirect));

            SelectionList= new SelectionListComponent(_options);

            AddComponent(TextMessage);
            AddComponent(SelectionList, true);


        }

        public void Redirect()
        {
            var page = SelectionList?.OptionAtCurrIndex?.Obj as Page;

            if (page is Page)
            {
                NextPage = page;
            }
        }

        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();

            var focusedComp = GetFocusedComponent();

            if (focusedComp == SelectionList)
            {
                if (SelectionList.OptionAtCurrIndex?.Obj is Page)
                {
                    var goTo = InputHandler.CreateControl(ConsoleKey.Enter, "Select", Redirect);

                    output.Add(goTo.Key, goTo.Value);
                }



            }

            return output;
        }

        public override void Refresh()
        {
            base.Refresh();
        }

    }
}
