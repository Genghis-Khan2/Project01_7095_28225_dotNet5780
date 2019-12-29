﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BE;
using DS;
using Exceptions;

namespace DAL
{
    /// <summary>
    /// Implementation of the DAL.
    /// Implemented using lists for the data types
    /// See <see cref="IDAL"/> for the DAL interface
    /// </summary>
    public class DalImp : IDAL
    {
        #region Singletory These parts are what make the class a singletory
        /// <summary>
        /// Private constructor for DalImp so that another instance can't be created
        /// </summary>
        private DalImp() { }

        protected static DalImp instance = null;

        /// <summary>
        /// This is the factory method of DalImp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        public static IDAL GetDal()
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
        /// This function adds a <paramref name="request"/> to the data's list
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="request">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest request)
        {
            if (0 == request.GuestRequestKey)
            {
                request.GuestRequestKey = Configuration.GuestRequestKey;
            }

            var linq = from item in DataSource.guestRequestsList
                       where item.GuestRequestKey == request.GuestRequestKey
                       select new { Num = item.GuestRequestKey };

            if (linq.Count() == 0)
            {
                DataSource.guestRequestsList.Add(request.Clone());
            }

            else
            {
                throw new AlreadyExistsException(request.GuestRequestKey, "GuestRequest");
            }
        }

        #endregion

        #region GetAllGuestRequests This function returns the guest requests

        /// <summary>
        /// This function returns the guest requests in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no guest requests</exception>
        /// <returns>IEnumerable to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            var v = from item in DataSource.guestRequestsList.Clone()
                    select item;
            if (v.Count() == 0)
            {
                throw new NoItemsException("GuestRequest");
            }

            return v;
        }

        #endregion

        #region UpdateGuestRequest This function updates a guest request

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        public void UpdateGuestRequest(int key, Enums.RequestStatus stat)
        {
            int i = DataSource.guestRequestsList.FindIndex(t => t.GuestRequestKey == key);

            if (-1 == i)
            {
                throw new KeyNotFoundException("No guest request with key specified");
            }

            DataSource.guestRequestsList[i].Status = stat;
        }

        #endregion

        #endregion

        #region HostingUnit These functions perform actions on HostingUnits

        #region AddHostingUnit This function adds a hosting unit

        /// <summary>
        /// This function adds a <paramref name="unit"/> to the data's list.
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="unit">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit unit)
        {
            if (unit.HostingUnitKey == 0)
            {
                unit.HostingUnitKey = Configuration.HostingUnitKey;
            }
            var linq = from item in DataSource.hostingUnitsList
                       where item.HostingUnitKey == unit.HostingUnitKey
                       select new { Num = item.HostingUnitKey };

            if (linq.Count() == 0)
            {
                DataSource.hostingUnitsList.Add(unit.Clone()); // Otherwise it might be changed elsewhere
            }

            else
            {
                throw new AlreadyExistsException(unit.HostingUnitKey, "HostingUnit");
            }
        }

        #endregion

        #region GetAllHostingUnits This function returns all hosting units

        /// <summary>
        /// This function returns the hosting units in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no items in the hosting units list</exception>
        /// <returns>IEnumerable to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            var v = from item in DataSource.hostingUnitsList.Clone()
                    select item;

            if (v.Count() == 0)
            {
                throw new NoItemsException("HostingUnit");
            }

            return v;
        }

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit with a matching <paramref name="key"/> is found</exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            var res = from item in DataSource.hostingUnitsList
                      let temp = key
                      where temp == item.HostingUnitKey
                      select item;
            if (res.Count() == 0)
            {
                throw new KeyNotFoundException("No hosting unit with key specified");
            }

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
        /// <exception cref="KeyNotFoundException">Thrown when hosting unit with <paramref name="key"/> is not found</exception>
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            int index = DataSource.hostingUnitsList.FindIndex(new Predicate<HostingUnit>(x => x.HostingUnitKey == key));

            if (-1 == index)
            {
                throw new KeyNotFoundException("No hosting unit with key specified found");
            }

            DataSource.hostingUnitsList[index] = hu.Clone();
        }

        #endregion

        #endregion

        #region Order These functions perform actions on Orders

        #region AddOrder This function adds and order

        /// <summary>
        /// This function addes an <paramref name="order"/> to the data's list
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <exception cref="AlreadyExistsException">Thrown when guestRequest</exception>
        /// <exception cref="InfoNotExists">Thrown when the GuestRequest of the Order already exists</exception>
        /// <param name="order">Order to be added to the data collection</param>
        public void AddOrder(Order order)
        {
            if (!DataSource.guestRequestsList.Exists(x => x.GuestRequestKey == order.GuestRequestKey))
            {
                throw new InfoNotExists("GuestRequest", "Order");
            }

            if (!DataSource.hostingUnitsList.Exists(x => x.HostingUnitKey == order.HostingUnitKey))
            {
                throw new InfoNotExists("HostingUnit", "Order");
            }

            if (DataSource.ordersList.Exists(x => x.GuestRequestKey == order.GuestRequestKey))
            {
                throw new AlreadyExistsException(order.GuestRequestKey, "Order's GuestRequest");
            }

            if (0 == order.OrderKey)
            {
                order.OrderKey = Configuration.OrderKey;
            }

            var linq = from item in DataSource.ordersList
                       where item.OrderKey == order.OrderKey
                       select new { Num = item.OrderKey };
            if (linq.Count() == 0)
            {
                DataSource.ordersList.Add(order.Clone());
            }

            else
            {
                throw new AlreadyExistsException(order.OrderKey, "Order");
            }
        }

        #endregion

        #region GetAllOrders This function returns all the orders

        /// <summary>
        /// This function returns the orders in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no orders in the list</exception>
        /// <returns>IEnumerable to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            var v = from item in DataSource.ordersList.Clone()
                    select item;
            if (v.Count() == 0)
            {
                throw new NoItemsException("Order");
            }

            return v;
        }

        #endregion

        #region UpdateOrder This function updates an order

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        public void UpdateOrder(int key, Enums.OrderStatus stat)
        {
            int index = DataSource.ordersList.FindIndex(new Predicate<Order>(x => x.OrderKey == key));

            if (-1 == index)
            {
                throw new KeyNotFoundException("There is no order with the key specified");
            }

            DataSource.ordersList[index].Status = stat;
        }

        #endregion

        #endregion

        #region BankAccount These functions perform actions on BankAccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            List<BankBranch> ret = new List<BankBranch>
            {
                new BankBranch
                {
                    BankAccountNumber = 10000,
                    BankName = "Mizrachi",
                    BankNumber = 100,
                    BranchAddress = "31 Maple St.",
                    BranchCity = "Police",
                    BranchNumber = 1221
                },
                new BankBranch
                {
                    BankAccountNumber = 12125,
                    BankName = "Discount",
                    BankNumber = 326,
                    BranchAddress = "5 Daisy Ave.",
                    BranchCity = "New York City",
                    BranchNumber = 432
                },
                new BankBranch
                {
                    BankAccountNumber = 264162,
                    BankName = "Chase",
                    BankNumber = 241,
                    BranchAddress = "5 North Marshall St.",
                    BranchCity = "Far Rockaway",
                    BranchNumber = 3235
                },
                new BankBranch
                {
                    BankAccountNumber = 254294,
                    BankName = "Amex",
                    BankNumber = 3846,
                    BranchAddress = "8675 Tarkiln Hill Ave.",
                    BranchCity = "Reading",
                    BranchNumber = 36495
                },
                new BankBranch
                {
                    BankAccountNumber = 94646,
                    BankName = "Pepper",
                    BankNumber = 6461,
                    BranchAddress = "606 North Marshall Drive",
                    BranchCity = "North Ridgeville",
                    BranchNumber = 4154945
                }
            };

            var v = from item in ret.Clone()
                    select item;

            if (v.Count() == 0)
            {
                throw new NoItemsException("BankAccount");
            }

            return v;
        }

        #endregion

        #endregion
    }
}
