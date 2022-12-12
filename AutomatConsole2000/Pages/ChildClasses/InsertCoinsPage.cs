using AutomatConsole2000.Control;
using AutomatConsole2000.PageComponents.ChildClasses.SelectionListComponent;
using AutomatConsole2000.PageComponents.ChildClasses;
using AutomatConsole2000.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatConsole2000.Pages.ChildClasses;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using AutomatConsole2000.Helpers;

namespace Automat_Console.Pages
{
    /// <summary>
    /// Page to let the user insert coins to the machine 
    /// </summary>
    internal class InsertCoinsPage : Page, ISelecter, IExitable, IReDirecter
    {

        public override string? Title { get; } = "Insert Coins to Machine";

        //text component to show user the current balance in the machine
        public TextComponent MachineBalance = new MachineBalanceComp(UserSession.SessionStore, name: "MachineBalance");



       

        public SelectionListComponent SelectionList { get; set; } = new SelectionListComponent("Coins");



        public Page? ReturnPage { get; private set; }
        
        List<ListOption> _options = new List<ListOption>();

        public InsertCoinsPage(Page? returnPage = null)
        {
            ReturnPage = returnPage;


            AddComponent(MachineBalance);


            AddComponent(SelectionList, true);

            Refresh();
        }


        public override Dictionary<ConsoleKey, ControlAction> CustomControls()
        {
            var output = new Dictionary<ConsoleKey, ControlAction>();
   
            var focusedComp = GetFocusedComponent();

            if (focusedComp == SelectionList)
            {
                //checks if curremt object is a Type
                if (SelectionList.OptionAtCurrIndex?.Obj is Type)
                {
                    //Adds the control to insert a coin
                    var insert = InputHandler.CreateControl(ConsoleKey.Enter, "Insert", SelectObject);

                    output.Add(insert.Key, insert.Value);
                }
                //checks if curremt object is the return page
                else if (SelectionList.OptionAtCurrIndex?.Obj == ReturnPage)
                {
                    //adds control to go back
                    var goBack = InputHandler.CreateControl(ConsoleKey.Enter, "Back", Redirect);

                    output.Add(goBack.Key, goBack.Value);
                }
            }
            //adds control to exit application
            var exit = InputHandler.CreateControl(ConsoleKey.Escape, "Exit", Exit);

            output.Add(exit.Key, exit.Value);

            return output;
        }


        public override void Refresh()
        {
            var wallet = UserSession.SessionWallet;

            NextPage = null;

            

            Type[]? walletContent = wallet?.GetCoinTypesFoundInWallet();

            

            _options.Clear();


            //adds all the wallet coinTypes to the options
            if(walletContent != null)
            {
                foreach (var coinType in walletContent)
                {
                    string rowText = $"{coinType.Name} x{wallet?.CountCoinTypeInWallet(coinType)}";

                    _options.Add(new ListOption(rowText, coinType));
                }


            }

            //adds option to go back
            _options.Add(new ListOption("Back", ReturnPage));

            SelectionList.SetValues(_options);

            base.Refresh();
        }


        /// <summary>
        /// Selects object and in this case tries to insert a coin of selected type
        /// </summary>
        public void SelectObject()
        {
            var wallet = UserSession.SessionWallet;
            var store = UserSession.SessionStore;

            Type? coinType = SelectionList?.OptionAtCurrIndex?.Obj as Type;


            //tries to use coin
            if(wallet != null && wallet.UseCoin(coinType, out Coin? coinToUse)){

                //tries to insert coin
                if (store!=null && coinToUse != null && store.TryDeposit(coinToUse))
                {
                    //success

                }

                else
                {

                    //fail...
                    //if coin still exists it returns to wallet
                    if (coinToUse != null)
                    {
                        wallet.AddCoin(coinToUse);
                    }
                        
                }

            }

           
        }

        public void Exit()
        {
            NextPage = new ExitPage();
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
