using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DAL;

namespace BL
{
    class BLImp : IBL
    {
        private BLImp() { }
        //TODO: check all the Exeption DAL level throw and check they treated
        protected static BLImp instance = null;
        public void AddGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
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
            return DalImp.getDal().GetAllBankAccounts();
        }

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return DalImp.getDal().GetAllGuestRequests();
        }

        /// <summary>
        /// The Function return all the Hosting unit
        /// </summary>
        /// <returns>IEnumerator to </returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return DalImp.getDal().GetAllHostingUnits();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return DalImp.getDal().GetAllOrders();
        }

        /// <summary>
        /// This function return the BLImp Object according to singelton
        /// </summary>
        /// <returns>The BLImp Object</returns>
        public IBL getBL()
        {
            if (instance == null)
            {
                instance = new BLImp();
                return instance;
            }
            return instance;
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
