using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Guest
    {
        public int GuestKey { get; set; }

        public string PrivateName { get; set; }

        public string FamilyName { get; set; }

        public string MailAddress
        {
            get
            {
                return mailAddress;
            }
            set
            {
                //Input integrity checking by using the email address integrity check in the System.Net.Mail.MailAddress function
                try
                {
                    new System.Net.Mail.MailAddress(value);
                    mailAddress = value;
                    //if the email address valid
                }
                catch
                {
                    //if the email address is invalid
                    mailAddress = "plony@almony.com";
                    throw new FormatException("The format of the email isnt valid");
                }
            }
        }

        private string mailAddress;

        public List<int> GuestRequests { get; set; } = null;

        public override string ToString()
        {
            return string.Format("GuestKey: {0}\nPrivate Name: {1}\nFamily Name: {2}\n Mail Address: {3}\n",
                GuestKey, PrivateName, FamilyName, MailAddress);
        }
    }
}
