using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BE
{
    /// <summary>
    /// The class represents a host
    /// </summary>
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress
        {
            get
            {
                return mailAddress;
            }
            set
            {
                try
                {
                    //Use regex to check the validation of the mail address
                    string reg = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                    Regex r = new Regex(reg, RegexOptions.IgnoreCase);
                    if (!r.IsMatch(value))
                        throw new FormatException("The format of the email isnt valid");
                    mailAddress = value;
                }
                catch
                {
                    mailAddress = "plony@almony.com";
                }
            }
        }

        private string mailAddress;
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }

        /// <summary>
        /// The function returns the host information in a string type
        /// </summary>
        /// <returns>The host information in string type</returns>
        ///<seealso cref="object.ToString()"/>
        public override string ToString()
        {
            string res = "";
            res += "Host Key: " + HostKey + "\n";
            res += "Private Name: " + PrivateName + "\n";
            res += "Family Name: " + FamilyName + "\n";
            res += "Phone Number: " + PhoneNumber + "\n";
            res += "Mail Address: " + MailAddress + "\n";
            res += "Bank branch details: " + BankBranchDetails + "\n";
            res += "Bank account number: " + BankAccountNumber + "\n";
            res += "Collection Clearance: " + CollectionClearance + "\n";
            return res;
        }
    }
}
