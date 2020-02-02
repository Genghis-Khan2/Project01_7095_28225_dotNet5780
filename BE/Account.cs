using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents an account
    /// </summary>
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Enums.AccountType Type { get; set; }
        public int Key { get; set; }

        /// <summary>
        /// The function returns the Account information in a string type
        /// </summary>
        /// <returns>The account information in string type</returns>
        ///<seealso cref="Object.ToString()"/>
        public override string ToString()
        {
            string res = "";
            res += "User Name: " + UserName + "\n";
            res += "Password: " + Password + "\n";
            res += "Type: " + Type + "\n";
            res += "Key: " + Key + "\n";
            return res;
        }
    }
}
