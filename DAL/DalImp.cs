using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;

namespace DAL
{
    public class DalImp : IDAL
    {
        private DalImp() { }

        protected static DalImp instance;

        public IDAL getDal()
        {
            return new DalImp();
        }

        /// <summary>
        /// This function adds a guest request to the data's list
        /// </summary>
        /// <param name="gr">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            if (!DataSource.guestRequestsList.Exists(x => x.GuestRequestKey == gr.GuestRequestKey))
            {
                gr.GuestRequestKey = ++Configuration.Number;
                DataSource.guestRequestsList.Add(gr.Clone());
            }
        }

        /// <summary>
        /// This function adds a hosting unit to the data's list
        /// </summary>
        /// <param name="hu">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit hu)
        {
            hu.HostingUnitKey = ++Configuration.Number;
            DataSource.hostingUnitsList.Add(hu.Clone());
        }

        /// <summary>
        /// This function addes an order to the data's list
        /// </summary>
        /// <param name="ord">Order to be added to the data collection</param>
        public void AddOrder(Order ord)
        {
            DataSource.ordersList.Add(ord.Clone());
        }

        public List<BankAccount> GetAllBankAccounts()
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

        public List<GuestRequest> GetAllGuestRequests()
        {
            return DataSource.guestRequestsList.Clone();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return DataSource.hostingUnitsList.Clone();
        }

        public List<Order> GetAllOrders()
        {
            return DataSource.ordersList.Clone();
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            DataSource.hostingUnitsList.Remove(hu);
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            int index = DataSource.guestRequestsList.FindIndex(new Predicate<GuestRequest>(x => x.GuestRequestKey == gr.GuestRequestKey));
            DataSource.guestRequestsList[index] = gr.Clone();
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            int index = DataSource.hostingUnitsList.FindIndex(new Predicate<HostingUnit>(x => x.HostingUnitKey == hu.HostingUnitKey));
            DataSource.hostingUnitsList[index] = hu.Clone();
        }

        public void UpdateOrder(Order ord)
        {
            throw new NotImplementedException();//TODO: do it ;)
        }
    }
}
