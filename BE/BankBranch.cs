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
