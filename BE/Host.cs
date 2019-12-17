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
        public string PhoneNumber { get; set; }
        public string MailAddress
        {
            get
            {
                return this.MailAddress;
            }
            set
            {
                try
                {
                    new System.Net.Mail.MailAddress(value);
                    this.MailAddress = value;
                }
                catch
                {
                    this.MailAddress = "plony@almony.com";
                }
            }
        }
        public BankAccount BankAccount { get; set; }
        public bool CollectionClearance { get; set; }//TODO: is it list?

        /// <summary>
        /// Default constructor
        /// </summary>
        public Host()
        {
            this.HostKey = Configuration.HostKey++;
            this.PrivateName = "";
            this.FamilyName = "";
            this.PhoneNumber = "000-0000000";
            this.MailAddress = "plony@almony.com";
            this.BankAccount = new BankAccount();
            this.CollectionClearance = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privateName">Host first name</param>
        /// <param name="familyName">The host's last name</param>
        /// <param name="phoneNumber">The host's phone number</param>
        /// <param name="mailAddress">Host Email Address</param>
        /// <param name="bankAccuont">Host Bank Account Details</param>
        /// <param name="collectionClearance">Certificate of collection from the bank account</param>
        public Host(string privateName, string familyName, string phoneNumber, string mailAddress, BankAccount bankAccuont, bool collectionClearance)
        {
            this.HostKey = Configuration.HostKey++;
            this.PrivateName = privateName;
            this.FamilyName = familyName;
            this.MailAddress = mailAddress;
            this.BankAccount = bankAccuont;
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
            res += "Bank Accuont: " + BankAccount + "\n";
            res += "Collection Clearance: " + CollectionClearance + "\n";
            return res;
        }

    }
}
