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
        private int HostKey { get; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public int PhoneNumber { get; set; }
        public string MailAddress { get; set; }//TODO:check if the mal is valid
        public BankAccount BankAccuont { get; set; }
        public bool CollectionClearance { get; set; }//TODO: is it list?
        //TODO: cunstructor
        /// <summary>
        /// The function returns the host information in a string type
        /// </summary>
        /// <returns>The host information in string type</returns>
        public override string ToString()
        {
            string res = "";
            res += "Host Key: " + HostKey + "\n";
            res += "Private Name: " + PrivateName + "\n";
            res += "Family Name: " + FamilyName + "\n";
            res += "Phone Number: " + PhoneNumber + "\n";
            res += "Mail Address: " + MailAddress + "\n";
            res += "Bank Accuont: " + BankAccuont + "\n";
            res += "Collection Clearance: " + CollectionClearance + "\n";
            return res;
        }

    }
}
