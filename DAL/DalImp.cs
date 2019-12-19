using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BE;
using DS;

namespace DAL
{
    public class DalImp : IDAL
    {
        #region Singletory These parts are what make the class a singletory
        private DalImp() { }

        protected static DalImp instance = null;

        /// <summary>
        /// This is the factory method of DalImp
        /// </summary>
        /// <returns>The instance of the singleton factory (singletory)</returns>
        public static IDAL getDal()
        {
            if (instance == null)
            {
                instance = new DalImp();
                return instance;
            }
            return instance;
        }

        #endregion

        #region GuestRequest These functions perform actions on GuestRequests

        #region AddGuestRequest This function adds a guest request

        /// <summary>
        /// This function adds a guest request to the data's list
        /// </summary>
        /// <param name="gr">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            var linq = from item in DataSource.guestRequestsList
                       where item.GuestRequestKey == gr.GuestRequestKey
                       select new { Num = item.GuestRequestKey };
            if (linq.Count() == 0)
            {
                gr.GuestRequestKey = Configuration.GuestRequestKey++;
                DataSource.guestRequestsList.Add(gr.Clone());
            }
        }

        #endregion

        #region GetAllGuestRequests This function returns the guest requests

        /// <summary>
        /// This function returns the guest requests in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return from item in DataSource.guestRequestsList.Clone()
                   select item;
        }

        #endregion

        #region UpdateGuestRequest This function updates a guest request

        /// <summary>
        /// This function updates a guest request
        /// </summary>
        /// <param name="gr">Guest request to update to</param>
        /// <param name="key">Key of guest request to update</param>
        public void UpdateGuestRequest(GuestRequest gr, int key)
        {
            int i = DataSource.guestRequestsList.FindIndex(t => t.GuestRequestKey == key);
            DataSource.guestRequestsList[i] = gr;
        }

        #endregion

        #endregion

        #region HostingUnit These functions perform actions on HostingUnits

        #region AddHostingUnit This function adds a hosting unit

        /// <summary>
        /// This function adds a hosting unit to the data's list
        /// </summary>
        /// <param name="hu">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit hu)
        {
            var linq = from item in DataSource.hostingUnitsList
                       where item.HostingUnitKey == hu.HostingUnitKey
                       select new { Num = item.HostingUnitKey };
            if (linq.Count() == 0)
            {
                hu.HostingUnitKey = Configuration.HostingUnitKey++;
                DataSource.hostingUnitsList.Add(hu.Clone());
            }
        }

        #endregion

        #region GetAllHostingUnits This function returns all hosting units

        /// <summary>
        /// This function returns the hosting units in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return from item in DataSource.hostingUnitsList.Clone()
                   select item;
        }

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            var res = from item in DataSource.hostingUnitsList
                      let temp = key
                      where temp == item.HostingUnitKey
                      select item;

            foreach (var it in res)
            {
                DataSource.hostingUnitsList.Remove(it);
            }
        }

        #endregion

        #region UpdateHostingUnit This function updates a hosting unit

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            int index = DataSource.hostingUnitsList.FindIndex(new Predicate<HostingUnit>(x => x.HostingUnitKey == key));
            DataSource.hostingUnitsList[index] = hu.Clone();
        }

        #endregion

        #endregion

        #region Order These functions perform actions on Orders

        #region AddOrder This function adds and order

        /// <summary>
        /// This function addes an order to the data's list
        /// </summary>
        /// <param name="ord">Order to be added to the data collection</param>
        public void AddOrder(Order ord)
        {
            var linq = from item in DataSource.ordersList
                       where item.OrderKey == ord.OrderKey
                       select new { Num = item.OrderKey };
            if (linq.Count() == 0)
            {
                ord.OrderKey = Configuration.OrderKey++;
                DataSource.ordersList.Add(ord.Clone());
            }
        }

        #endregion

        #region GetAllOrders This function returns all the orders

        /// <summary>
        /// This function returns the orders in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return from item in DataSource.ordersList.Clone()
                   select item;
        }

        #endregion

        #region UpdateOrder This function updates an order

        /// <summary>
        /// This function updates an order
        /// </summary>
        /// <param name="ord">Order to update to</param>
        /// <param name="key">Key of order to update</param>
        public void UpdateOrder(Order ord, int key)
        {
            int index = DataSource.ordersList.FindIndex(new Predicate<Order>(x => x.OrderKey == key));
            DataSource.ordersList[index] = ord.Clone();
        }

        #endregion

        #endregion

        #region BankAccount These functions perform actions on BankAccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        public IEnumerable<BankAccount> GetAllBankAccounts()
        {
            List<BankAccount> ret = new List<BankAccount>();
            ret.Add(new BankAccount
            {
                BankAccountNumber = 10000,
                BankName = "Mizrachi",
                BankNumber = 100,
                BranchAddress = "31 Maple St.",
                BranchCity = "Police",
                BranchNumber = 1221
            });
            ret.Add(new BankAccount
            {
                BankAccountNumber = 12125,
                BankName = "Discount",
                BankNumber = 326,
                BranchAddress = "5 Daisy Ave.",
                BranchCity = "New York City",
                BranchNumber = 432
            });
            ret.Add(new BankAccount
            {
                BankAccountNumber = 264162,
                BankName = "Chase",
                BankNumber = 241,
                BranchAddress = "5 North Marshall St.",
                BranchCity = "Far Rockaway",
                BranchNumber = 3235
            });
            ret.Add(new BankAccount
            {
                BankAccountNumber = 254294,
                BankName = "Amex",
                BankNumber = 3846,
                BranchAddress = "8675 Tarkiln Hill Ave.",
                BranchCity = "Reading",
                BranchNumber = 36495
            });
            ret.Add(new BankAccount
            {
                BankAccountNumber = 94646,
                BankName = "Pepper",
                BankNumber = 6461,
                BranchAddress = "606 North Marshall Drive",
                BranchCity = "North Ridgeville",
                BranchNumber = 4154945
            });

            return from item in ret.Clone()
                   select item;
        }

        #endregion

        #endregion
    }
}
