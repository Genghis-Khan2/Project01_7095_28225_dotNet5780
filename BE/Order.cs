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
        public int OrderKey { get; private set; }
        public Enums.OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Order()
        {
            this.OrderKey = Configuration.OrderKey++;
            this.Status = Enums.OrderStatus.UnTreated;
            this.HostingUnitKey = 0;
            this.GuestRequestKey = 0;
            this.CreateDate = new DateTime(2020, 1, 1);
            this.OrderDate = new DateTime(2020, 1, 1);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hostingUnitKey">Hosting Unit ID Number</param>
        /// <param name="guestRequestKey">Guest request ID Number</param>
        /// <param name="createDate">Date of order creation</param>
        /// <param name="orderDate">Email Delivery Date to Customer</param>
        public Order(int hostingUnitKey, int guestRequestKey, DateTime createDate, DateTime orderDate)
        {
            this.OrderKey = Configuration.OrderKey++;
            this.Status = Enums.OrderStatus.UnTreated;
            this.HostingUnitKey = hostingUnitKey;
            this.GuestRequestKey = guestRequestKey;
            this.CreateDate = createDate;
            this.OrderDate = orderDate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hostingUnitKey">Hosting Unit ID Number</param>
        /// <param name="guestRequestKey">Guest request ID Number</param>
        /// <param name="status">Status of the order</param>
        /// <param name="createDate">Date of order creation</param>
        /// <param name="orderDate">Email Delivery Date to Customer</param>
        public Order(int hostingUnitKey, int guestRequestKey, Enums.OrderStatus status, DateTime createDate, DateTime orderDate)
        {
            this.OrderKey = Configuration.OrderKey++;
            this.HostingUnitKey = hostingUnitKey;
            this.GuestRequestKey = guestRequestKey;
            this.Status = status;
            this.CreateDate = createDate;
            this.OrderDate = orderDate;
        }

        /// <summary>
        /// The function returns the host information in a string type
        /// </summary>
        /// <returns>The host information in string type</returns>
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
