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
        private DalImp() { }

        protected static DalImp instance = null;

        public static IDAL getDal()
        {
            if (instance == null)
            {
                instance = new DalImp();
                return instance;
            }
            return instance;
        }

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

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return from item in DataSource.guestRequestsList.Clone()
                   select item;
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return from item in DataSource.hostingUnitsList.Clone()
                   select item;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return from item in DataSource.ordersList.Clone()
                   select item;
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            var res = from item in DataSource.hostingUnitsList
                      let temp = hu.HostingUnitKey
                      where temp == hu.HostingUnitKey
                      select item;

            foreach (var it in res)
            {
                DataSource.hostingUnitsList.Remove(it);
            }
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            var linq = from item in DataSource.guestRequestsList
                       where item.GuestRequestKey == gr.GuestRequestKey
                       select item;
            for (int i = 0; i < linq.Count(); i++)
            {

            }
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
