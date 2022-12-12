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

namespace Automat_Console.Pages
{
    internal class InsertCoinsPage : Page, ISelecter, IExitable, IReDirecter
    {

        public override string? Title { get; } = "Insert Coins to Machine";


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
                if (SelectionList.OptionAtCurrIndex?.Obj is Type)
                {
                    var insert = InputHandler.CreateControl(ConsoleKey.Enter, "Insert", SelectObject);

                    output.Add(insert.Key, insert.Value);
                }

                else if (SelectionList.OptionAtCurrIndex?.Obj == ReturnPage)
                {
                    var goBack = InputHandler.CreateControl(ConsoleKey.Enter, "Back", Redirect);

                    output.Add(goBack.Key, goBack.Value);
                }
            }

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

            if(walletContent != null)
            {
                foreach (var coinType in walletContent)
                {
                    string rowText = $"{coinType.Name} x{wallet?.CountCoinTypeInWallet(coinType)}";

                    _options.Add(new ListOption(rowText, coinType));
                }

                
            }

            _options.Add(new ListOption("Back", ReturnPage));

            SelectionList.SetValues(_options);

            base.Refresh();
        }

        public void SelectObject()
        {
            var wallet = UserSession.SessionWallet;
            var store = UserSession.SessionStore;

            Type? coinType = SelectionList?.OptionAtCurrIndex?.Obj as Type;

            if(wallet != null && wallet.UseCoin(coinType, out Coin? coinToUse)){

                if (store!=null && coinToUse != null && store.TryDeposit(coinToUse))
                {
                    //success

                }

                else
                {
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
