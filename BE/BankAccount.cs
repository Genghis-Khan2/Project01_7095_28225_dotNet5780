using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a bank account
    /// </summary>
    public class BankAccount
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public int BankAccountNumber { get; set; }

        /// <summary>
        /// The function returns the BankAccount information in a string type
        /// </summary>
        /// <returns>The BankAccount information in string type</returns>
        public override string ToString()
        {
            string res = "";
            res += "Bank Number: " + BankNumber;
            res += "Bank Name: " + BankName;
            res += "Branch Number: " + BranchNumber;
            res += "Branch Address: " + BranchAddress;
            res += "Branch City: " + BranchCity;
            res += "Bank Account Number: " + BankAccountNumber;
            return res;
        }
    }
}
