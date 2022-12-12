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
    /// <summary>
    /// Inherit a class from page to organize and set up specific pages
    /// </summary>
    internal abstract class Page
    {
        public abstract string? Title { get; }

        /// <summary>
        /// Lists all PageComponents on page
        /// </summary>
        public List<PageComponent> Content { get; protected set; } = new List<PageComponent>();

        /// <summary>
        /// Index of current Component thats in focus
        /// </summary>
        public int FocusIndex { get; set; } = 0;


        /// <summary>
        /// Set NextPage to leave current page and go to that NextPage instead
        /// </summary>
        protected Page? NextPage;


        /// <summary>
        /// Stores all the pages controls
        /// </summary>
        public Controller Controller { get; protected set; } = new Controller();


        /// <summary>
        /// Puts the controlmapping in footer if true
        /// </summary>
        public bool ShowControls { get; protected set; } = true;

        /// <summary>
        /// Page specific controls can used to update controls dynamicly
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            return new Dictionary<ConsoleKey, ControlAction>();
        }

        /// <summary>
        /// Refresh to update values etc. depending on page
        /// </summary>
        public virtual void Refresh()
        {
            RefreshControls();
        }


        /// <summary>
        /// Refreshes all controls on page controls
        /// </summary>
        private void RefreshControls()
        {

            Controller.Clear();

            //Adds all controls from focused Component in page
            if (Content.Count(x => x is IControllable) > 0) Controller.SetControls(GetContentControls());

            //adds controls from custom controls (depending on page)
            Controller.SetControls(CustomControls());



        }

        /// <summary>
        /// return The currently focused Component on page
        /// </summary>
        /// <returns></returns>
        public PageComponent GetFocusedComponent()
        {
            return Content[FocusIndex];
        }

        /// <summary>
        /// Sets a given component as focused 
        /// </summary>
        /// <param name="pc"></param>
        public virtual void FocusOnComponent(PageComponent pc)
        {


            FocusIndex = Content.IndexOf(pc);
        }
        

        /// <summary>
        /// Adds component to page Content, and recognizes it as an active component that should be rendered
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="focus"></param>
        public virtual void AddComponent(PageComponent pc, bool focus = false)
        {
            Content.Add(pc);

            if (focus) FocusOnComponent(pc);


        }

        /// <summary>
        /// Gets next Page to redirect to
        /// </summary>
        /// <returns></returns>
        public virtual Page? GetNextPage()
        {
            return NextPage;
        }

        /// <summary>
        /// Gets all controls in focused Component
        /// </summary>
        /// <returns></returns>
        protected Dictionary<ConsoleKey, ControlAction> GetContentControls()
        {
            Dictionary<ConsoleKey, ControlAction> controls = new Dictionary<ConsoleKey, ControlAction>();

            //if there is any content and if the focused Component exists
            if (Content != null && Content.Count > FocusIndex && Content[FocusIndex] != null)
            {
                //if It is a "IControllable" means it is fitted to control
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
