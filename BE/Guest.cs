using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
                //Use regex to check the validation of the mail address
                string reg = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                Regex r = new Regex(reg, RegexOptions.IgnoreCase);
                if (!r.IsMatch(value))
                    throw new FormatException("The format of the email isnt valid");
                mailAddress = value;
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
