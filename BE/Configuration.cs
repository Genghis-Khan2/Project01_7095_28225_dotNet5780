using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class contains all the static variables required for the software classes
    /// </summary>
    class Configuration
    {
        public static int GuestRequestKey = 0;//using by GuestRequest and Order
        public static int BankNumber = 0;//using by BankAccount
        public static int HostKey = 0;//using by Host
        public static int HostingUnitKey = 0;//using by HostingUnit and Order
        public static int OrderKey = 0;//using by Order
        //TODO: מספר עמלה?
    }
}
