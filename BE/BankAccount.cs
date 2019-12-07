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
        //TODO: write comment
        private int BankNumber { get; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public int BankAccountNumber { get; set; }
        public BankAccount()
        {
            this.BankNumber = Configuration.BankNumber++;
            this.BankName = "";
            this.BranchNumber = 0;
            this.BranchAddress = "";
            this.BranchCity = "";
            this.BankAccountNumber = 0;
        }
        public BankAccount(string bankName, int branchNumber, string branchAddress, string branchCity, int bankAccountNumber)
        {
            this.BankNumber = Configuration.BankNumber++;
            this.BankName = bankName;
            this.BranchNumber = branchNumber;
            this.BranchAddress = branchAddress;
            this.BranchCity = branchCity;
            this.BankAccountNumber = bankAccountNumber;
        }
        /// <summary>
        /// The function returns the BankAccount information in a string type
        /// </summary>
        /// <returns>The BankAccount information in string type</returns>
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
