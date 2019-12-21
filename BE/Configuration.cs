using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class contains all the static variables required for the software classes
    /// </summary>
    public class Configuration
    {
        public static int GuestRequestKey = 0;///Using by <see cref="GuestRequest"/> and <see cref="Order"/>
        public static int BankNumber = 0;///Using by <see cref="BankAccount"/>
        public static int HostKey = 0;///Using by <see cref="Host"/>
        public static int HostingUnitKey = 0;///Using by <see cref="HostingUnit"/> and <see cref="Order"/>
        public static int OrderKey = 0;///Using by <see cref="Order"/>
        public static float Commission = 0;//TODO: get the real Commission
    }
}
