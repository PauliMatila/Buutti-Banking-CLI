using System;
using System.Collections.Generic;
using System.Text;

namespace Buutti_Banking_CLI
{
    class MainMenu
    {
        DataHandler dataHandler = new DataHandler();
        
        public void OpenMainMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = Menu();
            }
        }
            
        public bool Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Buutti banking CLI!");
            Console.WriteLine("1) Create account");
            Console.WriteLine("2) Check if account exists");
            Console.WriteLine("3) Account balance");
            Console.WriteLine("4) Withdraw funds");
            Console.WriteLine("5) Deposit funds");
            Console.WriteLine("6) Transfer funds");
            Console.WriteLine("7) Help");
            Console.WriteLine("8) Quit");
            Console.Write("\nSelect an option: ");
            dataHandler.AddTestAccounts();
            switch (Console.ReadLine())
            {
                case "1":
                    dataHandler.CreateAccount();
                    return true;
                case "2":
                    dataHandler.CheckIfAccountExists();
                    return true;
                case "3":
                    dataHandler.AccountBalance();
                    return true;
                case "4":
                    dataHandler.WithdrawFunds();
                    return true;
                case "5":
                    dataHandler.DepositFunds();
                    return true;
                case "6":
                    dataHandler.TransferFunds();
                    return true;
                case "7":
                    dataHandler.Help();
                    return true;
                case "8":
                    return false;
                default:
                    return true;
            }
        }
    }
}
