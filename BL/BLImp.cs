using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DAL;

namespace BL
{
    class BLImp : IBL
    {

        public delegate bool isMeetTheDefinition(HostingUnit hostingUnit);
        private BLImp() { }
        //TODO: check all the Exeption DAL level throw and check they treated
        protected static BLImp instance = null;

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

        public void AddGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void AddOrder(Order ord)
        {
            //TODO: write the function
            throw new NotImplementedException();
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

        public void RemoveHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order ord)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }
        public IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }
        public int getNumberOfDateInRange(DateTime startDay)//check 2 param
        {
            //remember to reset the year of end Day to 2020 - maibe dont need because we dont use it?
            //TODO: write the function
            throw new NotImplementedException();
        }
        public IEnumerable<HostingUnit> getAllGuestRequestWhere(isMeetTheDefinition func)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }
        public int getAmountOfOrderToGuest(GuestRequest guestRequest)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

    }
}
