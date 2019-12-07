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
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }//TODO:check if the mal is valid
        public BankAccount BankAccuont { get; set; }
        public bool CollectionClearance { get; set; }//TODO: is it list?
        //TODO: comments

        public Host()
        {
            this.HostKey = Configuration.HostKey++;
            this.PrivateName = "";
            this.FamilyName = "";
            this.PhoneNumber = "000-0000000";
            this.MailAddress = "plony@almony.com";
            this.BankAccuont = new BankAccount();
            this.CollectionClearance = false;
        }

        public Host(string privateName,string familyName, string phoneNumber, string mailAddress,BankAccount bankAccuont, bool collectionClearance)
        {
            this.HostKey = Configuration.HostKey++;
            this.PrivateName = privateName;
            this.FamilyName = familyName;
            try
            {
                new System.Net.Mail.MailAddress(mailAddress);
                this.MailAddress = mailAddress;
            }
            catch
            {
                this.MailAddress = "plony@almony.com";
            }
            this.BankAccuont = bankAccuont;
            this.CollectionClearance = collectionClearance;
        }
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
