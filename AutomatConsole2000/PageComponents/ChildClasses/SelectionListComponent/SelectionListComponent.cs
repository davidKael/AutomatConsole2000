using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;


namespace AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent
{
    /// <summary>
    /// A page component to display a list of options the user can interact with
    /// </summary>
    internal class SelectionListComponent : PageComponent, IControllable
    {

        //here is the current options available
        private List<ListOption> _options = new List<ListOption>();

        
        //Which index currently is hovering on
        public int CurrIndex { get; private set; } = 0;

        //A direct way of getting the Option at the current index
        public ListOption? OptionAtCurrIndex { get { return _options.Count > CurrIndex ? _options[CurrIndex] : null; } }


        //Specific controls for SelectionLisComponent
        private Dictionary<ConsoleKey, ControlAction> _controls = new Dictionary<ConsoleKey, ControlAction>();

        


        // Attributes to tweak settings
        int _leftOffset = 1;
        int rowGap = 2;
        char _selectorChar = '>';
        string _optionsName;

        //ctor when no initial options are given
        public SelectionListComponent(string optionsName = "options")
        {

            SetControls();
            _optionsName = optionsName;

        }

        //ctor when multiple options are given
        public SelectionListComponent(List<ListOption> options, string optionsName = "options")
        {
            SetValues(options);
            SetControls();
            _optionsName = optionsName;

        }



        /// <summary>
        /// Updates the options in list
        /// </summary>
        /// <param name="options"></param>
        public void SetValues(List<ListOption> options)
        {
            _options = options;

            if (CurrIndex > _options.Count)
            {
                CurrIndex = 0;
            }
        }

        /// <summary>
        /// Returns result of component as a string
        /// </summary>
        /// <returns></returns>
        protected override string GetDisplayData()
        {
            string output = string.Empty;

            if (CurrIndex >= _options?.Count) CurrIndex = 0;

            if (_options?.Count > 0)
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    //adds selector character if selected
                    output += (CurrIndex == i ? _selectorChar : " ") + ConsoleHelper.Multiply(" ", _leftOffset);

                    //adds option text with row number
                    output += $"{i + 1}. {_options[i].Text}{ConsoleHelper.Multiply("\n", rowGap)}";

                }
            }
            else
            {
                //if no options available
                output = $"[No {_optionsName}]";
            }

            return output;
        }

        /// <summary>
        /// Build in function to navigate up in list
        /// </summary>
        void MoveUp()
        {
            //if index is zero or less than 0 it moves to end of list
            if (CurrIndex <= 0) CurrIndex = _options != null ? _options.Count - 1 : 0;
            else CurrIndex--;
        }

        /// <summary>
        /// Build in function to navigate down in list
        /// </summary>
        void MoveDown()
        {
            //if index is out of range it moves to 0
            if (CurrIndex >= _options?.Count - 1) CurrIndex = 0;
            else CurrIndex++;
        }



        /// <summary>
        /// Sets controls specific to selectionlist
        /// </summary>
        public void SetControls()
        {
            _controls.Clear();

            var up = InputHandler.CreateControl(ConsoleKey.UpArrow, "Up", this.MoveUp);
            var down = InputHandler.CreateControl(ConsoleKey.DownArrow, "Down", this.MoveDown);


            _controls.Add(up.Key, up.Value);
            _controls.Add(down.Key, down.Value);
        }


        /// <summary>
        /// For getting all controls in ListComponent
        /// </summary>
        /// <returns></returns>
        public Dictionary<ConsoleKey, ControlAction> GetControls()
        {
            return _controls;
        }
    }
}
