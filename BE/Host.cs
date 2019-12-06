using System;
using System.Collections.Generic;
using System.Text;

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
        public int PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankAccount BankAccount { get; set; }
        public bool CollectionClearance { get; set; }//TODO: is it list?

        /// <summary>
        /// The function returns the host information in a string type
        /// </summary>
        /// <returns>The host information in string type</returns>
        public override string ToString()
        {
            string res = "";
            res += "Host Key: " + HostKey;
            res += "Private Name: " + PrivateName;
            res += "Family Name: " + FamilyName;
            res += "Phone Number: " + PhoneNumber;
            res += "Mail Address: " + MailAddress;
            res += "Bank Accuont: " + BankAccount;
            res += "Collection Clearance: " + CollectionClearance;
            return res;
        }

    }
}
