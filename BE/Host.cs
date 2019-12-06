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
        private int HostKey { get; set; }
        private string PrivateName { get; set; }
        private string FamilyName { get; set; }
        private int PhoneNumber { get; set; }
        private string MailAddress { get; set; }
        private BankAccount BankAccuont { get; set; }
        private bool CollectionClearance { get; set; }//TODO: is it list?

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
            res += "Bank Accuont: " + BankAccuont;
            res += "Collection Clearance: " + CollectionClearance;
            return res;
        }

    }
}
