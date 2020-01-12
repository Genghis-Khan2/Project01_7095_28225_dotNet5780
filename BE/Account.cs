using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Enums.AccountType Type { get; set; }
        public int key { get; set; }
    }
}
