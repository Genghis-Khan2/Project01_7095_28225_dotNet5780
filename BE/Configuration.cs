using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class contains all the static variables required for the software classes
    /// </summary>
    /// 
    /// <remarks>
    /// For each of the fields here, we added a variable that is used as a flag and indicates whether the variable has already been initialized or not,
    /// which was required because they required both the initialization and update of the fields to be from the Configuration class,
    /// and because the C # language does not allow both attribute initialization and the creation of a custom GETTER function.
    /// So we had to do it this way even though it is a bit cumbersome and unwise
    /// </remarks>
    public class Configuration
    {
        #region GuestRequestKey

        /// <summary>
        /// This variable Using by <see cref="GuestRequest"/> and <see cref="Order"/>,
        /// see the comments up
        /// </summary>
        private static bool IsInitializedGuestRequestKey = false;
        public static int GuestRequestKey
        {
            get
            {
                if (!IsInitializedGuestRequestKey)
                {
                    IsInitializedGuestRequestKey = true;
                    guestRequestKey = 1;
                }
                return guestRequestKey++;
            }

            private set { guestRequestKey = value; }
        }

        private static int guestRequestKey;

        #endregion

        #region BankNumber

        /// <summary>
        /// This variable Using by <see cref="BankAccount"/>,
        /// see the comments up
        /// </summary>
        private static bool IsInitializedBankNumber = false;
        public static int BankNumber
        {
            get
            {
                if (!IsInitializedBankNumber)
                {
                    IsInitializedBankNumber = true;
                    bankNumber = 1;
                }
                return bankNumber++;
            }
            private set { bankNumber = value; }
        }

        private static int bankNumber;

        #endregion

        #region HostKey

        /// <summary>
        /// This variable Using by <see cref="Host"/>,
        /// see the comments up
        /// </summary>
        private static bool IsInitializedHostKey = false;
        public static int HostKey
        {
            get
            {
                if (!IsInitializedHostKey)
                {
                    IsInitializedHostKey = true;
                    hostKey = 1;
                }
                return hostKey++;
            }
            private set { hostKey = value; }
        }

        private static int hostKey;

        #endregion

        #region HostingUnitKey

        /// <summary>
        /// This variable Using by <see cref="HostingUnit"/> and <see cref="Order"/>,
        /// see the comments up
        /// </summary>
        private static bool IsInitializedHostingUnitKey = false;
        public static int HostingUnitKey
        {
            get
            {
                if (!IsInitializedHostingUnitKey)
                {
                    IsInitializedHostingUnitKey = true;
                    hostingUnitKey = 1;
                }
                return hostingUnitKey++;
            }
            private set { hostingUnitKey = value; }
        }

        private static int hostingUnitKey;

        #endregion

        #region OrderKey

        /// <summary>
        /// This variable Using by <see cref="Order"/>,
        /// see the comments up
        /// </summary>
        private static bool IsInitializedOrderKey = false;
        public static int OrderKey
        {
            get
            {
                if (!IsInitializedOrderKey)
                {
                    IsInitializedOrderKey = true;
                    orderKey = 1;
                }
                return orderKey++;
            }
            private set { orderKey = value; }
        }

        private static int orderKey;

        #endregion

        #region Commission

        /// <summary>
        /// This variable Using by <see cref="Order"/>,
        /// This variable is not a "runner" and therefore does not need what we defined in the comment above
        /// </summary>
        /// <remarks>We set it to 10 NIS according to the Targil</remarks>
        public static float Commission { get; set; } = 10;

        #endregion


    }
}
