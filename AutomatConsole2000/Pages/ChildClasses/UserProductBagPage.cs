using Automat_Console.Items;
using Automat_Console.Products;
using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using AutomatConsole2000.Pages;
using AutomatConsole2000.Pages.ChildClasses;

namespace Automat_Console.Pages
{

    /// <summary>
    /// Page to let user see and interact with all the products the user has collected
    /// </summary>
    internal class UserProductBagPage : Page, IExitable, ISelecter, IReDirecter
    {
        public override string Title => "User Bag";


        /// <summary>
        /// To show a text when using a product
        /// </summary>
        public TextComponent UseText { get; set; } = new TextComponent();


        public TextComponent ListText { get; set; } = new TextComponent(text: "Products:");

        public SelectionListComponent SelectionList { get; set; }


        Page? ReturnPage;

        //Product currently detailed
        Product? DetailedProduct;

        List<ListOption> _options = new List<ListOption>();

        public UserProductBagPage(Page? returnPage = null)
        {
            ReturnPage = returnPage;

            SelectionList= new SelectionListComponent("products");

            AddComponent(UseText);
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
                    var back = InputHandler.CreateControl(ConsoleKey.Enter, "Back", Redirect);

                    output.Add(back.Key, back.Value);
                }



                else if (SelectionList?.OptionAtCurrIndex?.Obj is Product)
                {
                    var use = InputHandler.CreateControl(ConsoleKey.Enter, "Use", SelectObject);
                    var inspect = InputHandler.CreateControl(ConsoleKey.Spacebar, "Inpect", ShowProductDetails);

                    output.Add(use.Key, use.Value);
                    output.Add(inspect.Key, inspect.Value);
                }

            }

            var exit = InputHandler.CreateControl(ConsoleKey.Escape, "Exit", Exit);

            output.Add(exit.Key, exit.Value);

            return output;
        }

        



        public void Exit()
        {
            NextPage = new ExitPage(this);
        }


        /// <summary>
        /// Use Product when selecting it
        /// </summary>
        public void SelectObject()
        {
            if (SelectionList?.OptionAtCurrIndex?.Obj is Product)
            {
                Product p = (Product)SelectionList.OptionAtCurrIndex.Obj;

                UseText.Text = $"{p.Name}: {p.Use()}";


                //when user has used product it disappears from the bag
                UserSession.UserProductBag.Remove(p);
            }
        }

        /// <summary>
        /// To show product details
        /// </summary>
        void ShowProductDetails()
        {
            var p = SelectionList?.OptionAtCurrIndex?.Obj as Product;

            if (p != null)
            {
                if (DetailedProduct != p)
                {
                    DetailedProduct = p;
                }
                else
                {
                    DetailedProduct = null;
                }

            }

        }

        public void Redirect()
        {
            if(SelectionList?.OptionAtCurrIndex?.Obj is Page)
            {
                NextPage = SelectionList.OptionAtCurrIndex.Obj as Page;
            }
        }



        public override void Refresh()
        {
            _options.Clear();
            NextPage = null;

            List<Product> bag = UserSession.UserProductBag;

            List<string> formatedProducts = Product.GetPrintableData(bag, highlighted: DetailedProduct, false);


            for (int i = 0; i < bag.Count; i++)
            {
                _options.Add(new ListOption(formatedProducts[i], bag[i]));


            };

            _options.Add(new ListOption("Back", ReturnPage));


            SelectionList?.SetValues(_options);

            base.Refresh();
        }
    }
}
