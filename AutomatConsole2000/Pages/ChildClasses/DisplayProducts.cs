using Automat_Console.Items;
using AutomatConsole2000.Control;
using AutomatConsole2000.Helpers;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using AutomatConsole2000.Pages;
using AutomatConsole2000.Pages.ChildClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Automat_Console.Pages
{

    /// <summary>
    /// Page for displaying prodruct in store dependingon given category
    /// </summary>
    internal class DisplayProducts : Page, IReDirecter, IExitable
    {


        public override string Title { get; } = "Store";

        public TextComponent ListText { get; set; } = new TextComponent(text: "Products:");

        //uses MachineBalanceComp to display current balance on machine
        public TextComponent MachineBalance = new MachineBalanceComp(UserSession.SessionStore, name: "MachineBalance");

        public SelectionListComponent SelectionList { get; set; } 

        Page? ReturnPage;

        
        private string _currCategory;

        List<ListOption> _options = new List<ListOption>();

        //Sets if more details is desired on a choosen Product
        Product? DetailedProduct = null;

        public DisplayProducts(string category, Page? returnPage = null)
        {
            _currCategory = category;
            ReturnPage = returnPage;


            SelectionList  = new SelectionListComponent(category + " Products:");

            AddComponent(MachineBalance);
            AddComponent(ListText);
            AddComponent(SelectionList, true);

            Refresh();
        }

  

        public void Exit()
        {
            NextPage = new ExitPage(this);
        }




        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();

            var focusedComp = GetFocusedComponent();

            if (focusedComp == SelectionList)
            {
                var obj = SelectionList.OptionAtCurrIndex?.Obj;


                //if current option is the ReturnPage
                if (obj == ReturnPage)
                {
                    //adds The control "back" to redirect to last page
                    var back = InputHandler.CreateControl(ConsoleKey.Enter, "Back", Redirect);

                    output.Add(back.Key, back.Value);
                }

                //if current option is a InsertCoinsPage
                else if (SelectionList.OptionAtCurrIndex?.Obj is InsertCoinsPage)
                {

                    //adds The control "Insert Coins" to redirect to Insert Coins page
                    var page = InputHandler.CreateControl(ConsoleKey.Enter, "Insert Coins", Redirect);

                    output.Add(page.Key, page.Value);
                }

                //if current option is a Product
                else if (SelectionList.OptionAtCurrIndex?.Obj is Product)
                {
                    //adds The control "Buy" to try to buy product
                    var buy = InputHandler.CreateControl(ConsoleKey.Enter, "Buy", BuyProduct);

                    //adds The control "Details" to show details on product
                    var showDetails = InputHandler.CreateControl(ConsoleKey.Spacebar, "Details", ShowProductDetails);

                    output.Add(buy.Key, buy.Value);
                    output.Add(showDetails.Key, showDetails.Value);
                }

                obj = SelectionList.GetControls();
            }

            var exit = InputHandler.CreateControl(ConsoleKey.Escape, "Exit", Exit);

            output.Add(exit.Key, exit.Value);

            return output;
        }

        /// <summary>
        /// To buy selected product in list
        /// </summary>
        public void BuyProduct()
        {
            var store = UserSession.SessionStore;
            var wallet = UserSession.SessionWallet;

            var product = SelectionList?.OptionAtCurrIndex?.Obj as Product;

            if(product != null)
            {
                //if Purchase is successfull 
                if(store.MakePurchase(wallet, product, out Product? boughtProduct, out string message))
                {

                    UserSession.AddProductToUserBag(boughtProduct);

                    string str = message + "\n\n Do yoo want to go to your products bag?";

                    //redirects to a confirm page
                    NextPage = new ConfirmPage(new UserProductBagPage(this), this, "Purchase Successfull!!", str);
              
                }
                else
                {
                    //redirects to a confirm page
                    NextPage = new ConfirmPage(new InsertCoinsPage(this), this, "Purchase Failed!!", message);
                }
                
            }

            
        }

        /// <summary>
        /// Show details of selected product
        /// </summary>
        void ShowProductDetails()
        {
            var p = SelectionList?.OptionAtCurrIndex?.Obj as Product;

            if(p != null)
            {


                if(DetailedProduct != p)
                {
                    DetailedProduct = p;
                }
                else
                {
                    //if Product allready is detailed id resets
                    DetailedProduct = null;
                }
                
            }
            
        }

        public override void Refresh()
        {
            _options.Clear();
            NextPage = null;

            Store store = UserSession.SessionStore;

            var CurrentSelected = DetailedProduct;

            //gets all the product by category
            var products = store.GetProductsRows(_currCategory).ToList();

            //formats all the product data to "nicelooking" text
            List<string> formatedProducts = Product.GetPrintableData(products, highlighted: DetailedProduct);
            
            //adds all the products wanted as options 
            for(int i =0; i< products.Count; i++)
            {
                 _options.Add(new ListOption(formatedProducts[i], products[i]));
                
      
            };

            //also adds options to insert coins or go back
            _options.Add(new ListOption("Insert Coins", new InsertCoinsPage(this)));
            _options.Add(new ListOption("Back", ReturnPage));


            SelectionList?.SetValues(_options);

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
