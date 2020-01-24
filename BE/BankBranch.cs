using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a bank account
    /// </summary>
    public class BankBranch
    {
        //TODO: Fix values
        public int BankNumber { get; set; } = 0;
        public string BankName { get; set; } = "Mizrach";
        public int BranchNumber { get; set; } = 0;
        public string BranchAddress { get; set; } = "212 PoopyDoo";
        public string BranchCity { get; set; } = "Meliathan";
        public int BankAccountNumber { get; set; } = 0;

        /// <summary>
        /// The function returns the BankAccount information in a string type
        /// </summary>
        /// <returns>The BankAccount information in string type</returns>
        ///<seealso cref="Object.ToString()"/>
        public override string ToString()
        {
            string res = "";
            res += "Bank Number: " + BankNumber + "\n";
            res += "Bank Name: " + BankName + "\n";
            res += "Branch Number: " + BranchNumber + "\n";
            res += "Branch Address: " + BranchAddress + "\n";
            res += "Branch City: " + BranchCity + "\n";
            res += "Bank Account Number: " + BankAccountNumber + "\n";
            return res;
        }
    }
}
