using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents an invitation (customer-hostingUnit relationship)
    /// </summary>
    public class Order
    {
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public int OrderKey { get; set; }
        public Enums.OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The function returns the host information in a string type
        /// </summary>
        /// <returns>The host information in string type</returns>
        ///<seealso cref="object.ToString()"/>
        public override string ToString()
        {
            string res = "";
            res += "Order Key: " + OrderKey + "\n";
            res += "Hosting Unit Key: " + HostingUnitKey + "\n";
            res += "Guest Request Key: " + GuestRequestKey + "\n";
            res += "Status: " + Status + "\n";
            res += "Create Date: " + CreateDate.ToString("dd.MM.yyyy") + "\n";
            res += "Order Date: " + OrderDate.ToString("dd.MM.yyyy") + "\n";
            return res;
        }
    }
}
