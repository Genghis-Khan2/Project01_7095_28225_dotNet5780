using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using BE;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        private Dal_XML_imp()
        { }

        protected static Dal_XML_imp instance = null;

        /// <summary>
        /// This is the factory method of Dal_XML_imp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        public static IDAL GetDal()
        {
            if (instance == null)
            {
                instance = new Dal_XML_imp();
                return instance;
            }
            return instance;
        }


        public void AddGuestRequest(GuestRequest gr)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order ord)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfBankAccountExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfGuestRequestExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHostExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHostingUnitExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfOrderExists(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Host> GetAllHosts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public BankBranch GetBankBranch(int key)
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequest(int key)
        {
            throw new NotImplementedException();
        }

        public Host GetHost(int key)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int key)
        {
            throw new NotImplementedException();
        }

        public void RemoveHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderStatus(int key, Enums.OrderStatus stat)
        {
            throw new NotImplementedException();
        }
    }
}
