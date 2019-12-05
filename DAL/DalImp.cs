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
            //TODO: Finish implementation of function
            foreach (GuestRequest i in DataSource.guestRequestsList)
            {

            }
        }

        public List<Guest> GetGuests()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetHostingUnits()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order ord)
        {
            throw new NotImplementedException();
        }
    }
}
