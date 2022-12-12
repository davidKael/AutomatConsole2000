using AutomatConsole2000.Control;
using AutomatConsole2000.PageComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AutomatConsole2000.Pages
{
    internal abstract class Page
    {
        public abstract string? Title { get; }


        public List<PageComponent> Content { get; protected set; } = new List<PageComponent>();

        public int FocusIndex { get; set; } = 0;

        protected Page? NextPage;

        public Controller Controller { get; protected set; } = new Controller();

        public bool ShowControls { get; protected set; } = true;


        public virtual Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            return new Dictionary<ConsoleKey, ControlAction>();
        }


        public virtual void Refresh()
        {
            RefreshControls();
        }

        private void RefreshControls()
        {

            Controller.Clear();

            if (Content.Count(x => x is IControllable) > 0) Controller.SetControls(GetContentControls());

            Controller.SetControls(CustomControls());



        }

        public PageComponent GetFocusedComponent()
        {
            return Content[FocusIndex];
        }

        public virtual void FocusOnComponent(PageComponent pc)
        {


            FocusIndex = Content.IndexOf(pc);
        }

        public virtual void AddComponent(PageComponent pc, bool focus = false)
        {
            Content.Add(pc);

            if (focus) FocusOnComponent(pc);


        }

        public virtual Page? GetNextPage()
        {
            return NextPage;
        }


        protected Dictionary<ConsoleKey, ControlAction> GetContentControls()
        {
            Dictionary<ConsoleKey, ControlAction> controls = new Dictionary<ConsoleKey, ControlAction>();


            if (Content != null && Content.Count > FocusIndex && Content[FocusIndex] != null)
            {
                if (Content[FocusIndex] is IControllable)
                {
                    IControllable? controlableComp = Content[FocusIndex] as IControllable;

                    if (controlableComp != null)
                    {
                        controls = controlableComp.GetControls();

                    }
                }
            }

            return controls;
        }

    }
}
