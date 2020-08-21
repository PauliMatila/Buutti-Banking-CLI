using System;
using System.Collections.Generic;
using System.Text;

namespace Buutti_Banking_CLI
{
    class User
    {
        public string name;
        public string password;
        public int id;
        public int balance;

        public User(string userName, string userPassword, int userId, int userBalance)
        {
            name = userName;
            password = userPassword;
            id = userId;
            balance = userBalance;
        }
    }
}
