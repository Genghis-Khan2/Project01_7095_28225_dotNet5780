using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace BL
{
    class BLImp : IBL
    {
        //TODO: check all the Exeption DAL level throw and check they treated
        protected static BLImp instance = null;
        public void AddGuestRequest(GuestRequest gr)
        {

        }

        public void AddHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
        }

        public void AddOrder(Order ord)
        {
            //TODO: write the function
        }

        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            //TODO: write the function
        }

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            //TODO: write the function
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            //TODO: write the function
        }

        public IEnumerable<Order> GetAllOrders()
        {
            //TODO: write the function
        }

        public IBL getDal()
        {
            if (instance==null)
            {

            }
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
        }

        public void UpdateOrder(Order ord)
        {
            //TODO: write the function
        }
    }
}
