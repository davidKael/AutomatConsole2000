using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;
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
    /// <summary>
    /// Page to give a "ok" or "no" Question to user, and redirect accordingly
    /// </summary>
    internal class ConfirmPage : Page, IReDirecter
    {
        public override string? Title { get; }
        public SelectionListComponent SelectionList { get; set; }

        //where to redirect if a ok is given by user
        Page YesRedirect;

        //where to redirect if a no is given by user
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

            //sets the selected page as the redirecting page
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
                    //creates a new control
                    var goTo = InputHandler.CreateControl(ConsoleKey.Enter, "Select", Redirect);


                    //add the control to the output of CustomControls
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
