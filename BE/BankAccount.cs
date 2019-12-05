using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a bank account
    /// </summary>
    class BankAccount
    {
        private int BankNumber { get; set; }
        private string BankName { get; set; }
        private int BranchNumber { get; set; }
        private string BranchAddress { get; set; }
        private string BranchCity { get; set; }
        private int BankAccountNumber { get; set; }
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
