using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;

namespace DAL
{
    public class DalImp : IDAL
    {
        /// <summary>
        /// This function adds a guest request to the data's list
        /// </summary>
        /// <param name="gr">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            DataSource.guestRequestsList.Add(gr);
        }

        /// <summary>
        /// This function adds a hosting unit to the data's list
        /// </summary>
        /// <param name="hu">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit hu)
        {
            DataSource.hostingUnitsList.Add(hu);
        }

        /// <summary>
        /// This function addes an order to the data's list
        /// </summary>
        /// <param name="ord">Order to be added to the data collection</param>
        public void AddOrder(Order ord)
        {
            DataSource.ordersList.Add(ord);
        }

        public List<BankAccount> GetBankAccounts()
        {
            //TODO: Hardcode bank account
            List<BankAccount> ret = new List<BankAccount>();
            ret.Add(new BankAccount { });
            ret.Add(new BankAccount { });
            ret.Add(new BankAccount { });
            ret.Add(new BankAccount { });
            ret.Add(new BankAccount { });

            return ret;
        }

        public List<GuestRequest> GetGuests()
        {
            return DataSource.guestRequestsList;
        }

        public List<HostingUnit> GetHostingUnits()
        {
            return DataSource.hostingUnitsList;
        }

        public List<Order> GetOrders()
        {
            return DataSource.ordersList;
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            DataSource.hostingUnitsList.Remove(hu);
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            //TODO: Implement
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            //TODO: Implement
        }

        public void UpdateOrder(Order ord)
        {
            throw new NotImplementedException();
        }
    }
}
