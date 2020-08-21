using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Buutti_Banking_CLI
{
    class DataHandler
    {
        public List<User> allUsers = new List<User>();

        public void AddTestAccounts()
        {
            allUsers.Add(new User("Teppo Testaaja", "asd", 1111, 50));
            allUsers.Add(new User("Hessu Hopo", "qwe", 2222, 70));
            allUsers.Add(new User("Tero Tester", "zxc", 3333, 30));
        }


        #region helper methods
        private User FindUserById(int id)
        {
            foreach (User user in allUsers)
            {
                if (user.id == id)
                {
                    return user;
                }
            }
            return null;
        }

        private bool UserExists(int id)
        {
            foreach (User element in allUsers)
            {
                if (element.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        private User CheckForIdAndPassword()
        {
            bool searchingID = true;
            while (searchingID)
            {
                Console.Write("What is your account ID?: ");
                int correctId = int.Parse(Console.ReadLine());
                if (UserExists(correctId))
                {
                    Console.Clear();
                    Console.Write("Account found! ");

                    searchingID = false;
                    User user = FindUserById(correctId);
                    if (CheckIfCorrectPassword(user))
                    {
                        Console.Clear();
                        Console.WriteLine($"Correct password. We validated you as {user.name}.");
                        return user;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("An account with that ID does not exist.");
                    Console.Write("Press enter to try again");
                    searchingID = ContinueLoopKey(Console.ReadKey().Key);
                }
            }
            return null;
        }

        private bool CheckIfCorrectPassword(User user)
        {
            bool searchingPassword = true;
            while (searchingPassword)
            {
                Console.Write("Insert your password: ");
                string correctPassword = Console.ReadLine();
                if (user.password.Equals(correctPassword))
                {
                    Console.WriteLine($"Correct password. We validated you as {user.name}.");
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong password, Press enter to try again.");
                    searchingPassword = ContinueLoopKey(Console.ReadKey().Key);
                }
            }
            return false;
        }

        private bool ContinueLoopKey(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReturningToMenu()
        {
            Console.Clear();
            Console.Write("Returning to menu");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
        }
        #endregion

        #region Main methods.
        public void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("Creating a new user account!\n");
            Console.Write("Insert your name: ");
            string userName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Hello, {userName}! It’s great to have you as a client.\n");
            Console.Write("How much is your initial deposit? (The minimum is 10$): ");
            int userBalance = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine($"Great, {userName}! You now have an account with a balance of {userBalance}$.");
            Console.WriteLine("We’re happy to have you as a customer, and we want to ensure that your money is safe with us.\n");
            Console.Write("Give us a password, which gives only you the access to your account: ");
            string userPassword = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Your account is now created.\n");
            Random rand = new Random();
            int number;
            number = rand.Next(1000, 9999);
            foreach (User item in allUsers)
            {
                if (item.id.Equals(number))
                {
                    number = rand.Next(1000, 9999);
                }
            }
            int userId = number;
            Console.WriteLine($"Account id: {userId}");
            Console.WriteLine("Store your account ID in a safe place.");
            Console.ReadKey();
            User user = new User(userName, userPassword, userId, userBalance);
            allUsers.Add(user);
        }

        public void CheckIfAccountExists()
        {
            Console.Clear();
            if (allUsers.Count == 0)
            {
                Console.WriteLine("There is no accounts.\n");
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
            }
            else
            {
                bool continueSearch = true;

                while (continueSearch)
                {
                    Console.Clear();
                    Console.WriteLine("Checking if an account exists!");
                    Console.Write("Enter the account ID whose existence you want to verify: ");
                    int correctId = int.Parse(Console.ReadLine());
                    if (UserExists(correctId))
                    {
                        continueSearch = false;
                        Console.Clear();
                        Console.WriteLine("This account exists.\n");
                        Console.WriteLine("Press any key to go back.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("An account with that ID does not exist.");
                        Console.Write("Press Enter to try again.");
                        continueSearch = ContinueLoopKey(Console.ReadKey().Key);
                    }
                }
            }
        }

        public void AccountBalance()
        {
            Console.Clear();
            Console.WriteLine("Checking your account balance!\n");
            User user = CheckForIdAndPassword();
            if (user != null)
            {
                Console.WriteLine($"Your account balance is {user.balance}$.\n");
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
            }
            else
            {
                ReturningToMenu();
            }
        }

        public void WithdrawFunds()
        {
            Console.Clear();
            Console.WriteLine("Withdrawing cash!\n");
            User user = CheckForIdAndPassword();
            if (user != null)
            {
                Console.WriteLine($"How much money do you want to withdraw? (Current balance: {user.balance}$): ");
                bool loop = true;
                while (loop)
                {
                    int withdraw = int.Parse(Console.ReadLine());
                    if (user.balance < withdraw)
                    {
                        Console.WriteLine("Unfortunately you don’t have the balance for that. Try a smaller amount: ");
                    }
                    else
                    {
                        loop = false;
                        user.balance -= withdraw;
                        Console.WriteLine($"Withdrawing a cash sum of {withdraw}$. Your account balance is now {user.balance}$.");
                    }
                }
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
            }
            else
            {
                ReturningToMenu();
            }
        }

        public void DepositFunds()
        {
            Console.Clear();
            Console.WriteLine("Depositing cash!\n");
            User user = CheckForIdAndPassword();
            if (user != null)
            {
                Console.WriteLine($"How much money do you want to deposit? (Current balance: {user.balance}$): ");               
                int depositing = int.Parse(Console.ReadLine()); 
                user.balance += depositing;
                Console.WriteLine($"Depositing {depositing}$. Your account balance is now {user.balance}$.");
                Console.WriteLine("Press any key to go back.");
                Console.ReadLine();
            }
            else
            {
                ReturningToMenu();
            }
        }

        public void TransferFunds()
        {
            Console.Clear();
            Console.WriteLine("Transferring cash!\n");
            User user = CheckForIdAndPassword();
            if (user != null)
            {
                Console.WriteLine($"How much money do you want to transfer? (Current balance: {user.balance}$): ");
                int transfer = int.Parse(Console.ReadLine());
                user.balance -= transfer;
                Console.Clear();
                Console.WriteLine("Which account ID do you want to transfer these funds to?: ");
                bool continueSearch = true;
                while (continueSearch)
                {
                    int correctId = int.Parse(Console.ReadLine());
                    if (UserExists(correctId))
                    {
                        User user2 = FindUserById(correctId);
                        continueSearch = false;
                        Console.Clear();
                        user2.balance += transfer;
                        Console.WriteLine($"Sending {transfer}$ from account ID {user.id} to account ID {user2.id}.\n");
                        Console.WriteLine("Press any key to go back.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("An account with that ID does not exist.");
                        Console.Write("Press Enter to try again.");
                        continueSearch = ContinueLoopKey(Console.ReadKey().Key);
                    }
                }
            }
            else
            {
                ReturningToMenu();
            }
        }

        public void Help()
        {
            Console.Clear();
            Console.WriteLine("Here’s a list of commands you can use!\n");
            Console.WriteLine("Help                     Opens this dialog.");
            Console.WriteLine("Quit                     Quits the program.\n");
            Console.WriteLine("Account actions");
            Console.WriteLine("Create account           Opens a dialog for creating an account.");
            Console.WriteLine("Check if account exists  Opens a dialog for checking if an account exists.");
            Console.WriteLine("Account balance          Opens a dialog for logging in and prints the account balance.\n");
            Console.WriteLine("Fund actions");
            Console.WriteLine("Withdraw funds           Opens a dialog for withdrawing funds.");
            Console.WriteLine("Deposit funds            Opens a dialog for depositing funds.");
            Console.WriteLine("Transfer funds           Opens a dialog for transferring funds to another account.\n");
            Console.WriteLine("Press any key to go back.");
            Console.ReadKey();            
        }
        #endregion
    }
}

