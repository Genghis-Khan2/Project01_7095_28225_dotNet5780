﻿using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
        // No need for the singletory functions to be here... They are in
        /// <see cref="DalImp"/>

        #region GuestRequest These functions perform actions on GuestRequests

        #region AddGuestRequest This function adds a guest request

        /// <summary>
        /// This function adds a guest request to the data's list
        /// </summary>
        /// <param name="gr">GuestRequest to be added to the data collection</param>
        void AddGuestRequest(GuestRequest gr);

        #endregion

        #region GetAllGuestRequests This function returns the guest requests

        /// <summary>
        /// This function returns the guest requests in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of guest requests</returns>
        IEnumerable<GuestRequest> GetAllGuestRequests();

        #endregion

        #region UpdateGuestRequest This function updates a guest request

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        void UpdateGuestRequest(int key, Enums.RequestStatus stat);

        #endregion

        #region GetGuestRequest This function return guestRequset

        /// <summary>
        /// This function return GuestRequest according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the GuestRequest</param>
        /// <returns>The GuestRequest with the <paramref name="key"/></returns>
        GuestRequest GetGuestRequest(int key);

        #endregion

        #endregion

        #region HostingUnit These functions perform actions on HostingUnits

        #region AddHostingUnit This function adds a hosting unit

        /// <summary>
        /// This function adds a hosting unit to the data's list
        /// </summary>
        /// <param name="hu">HostingUnit to be added to the data collection</param>
        void AddHostingUnit(HostingUnit hu);

        #endregion

        #region GetAllHostingUnits This function returns all hosting units

        /// <summary>
        /// This function returns the hosting units in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of hosting units</returns>
        IEnumerable<HostingUnit> GetAllHostingUnits();

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <param name="key">Key to remove the hosting unit of</param>
        void RemoveHostingUnit(int key);

        #endregion

        #region UpdateHostingUnit This function updates a hosting unit

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        void UpdateHostingUnit(HostingUnit hu, int key);

        #endregion

        #region GetHostingUnit This function return HostingUnit

        /// <summary>
        /// This function return HostingUnit according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the HostingUnit</param>
        /// <returns>The HostingUnit with the <paramref name="key"/></returns>
        HostingUnit GetHostingUnit(int key);

        #endregion

        #endregion

        #region Order These functions perform actions on Orders

        #region AddOrder This function adds and order

        /// <summary>
        /// This function addes an order to the data's list
        /// </summary>
        /// <param name="ord">Order to be added to the data collection</param>
        void AddOrder(Order ord);

        #endregion

        #region GetAllOrders This function returns all the orders

        /// <summary>
        /// This function returns the orders in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of orders</returns>
        IEnumerable<Order> GetAllOrders();

        #endregion

        #region UpdateOrder This function updates an order

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        void UpdateOrder(int key, Enums.OrderStatus stat);

        #endregion

        #region GetOrder This function return Order

        /// <summary>
        /// This function return Order according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the Order</param>
        /// <returns>The Order with the <paramref name="key"/></returns>
        Order GetOrder(int key);

        #endregion

        #endregion

        #region BankAccount These functions perform actions on BankAcccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        IEnumerable<BankBranch> GetAllBankAccounts();

        #endregion

        #endregion

        #region Host These function perform actions on Host

        #region GetAllHosts This function return all the Hosts

        /// <summary>
        /// This function return all the Host 
        /// </summary>
        /// <returns><seealso cref="IEnumerable{Host}"/> to go over the list of all the Hosts</returns>
        IEnumerable<Host> GetAllHosts();

        #endregion

        #region GetHost This function return host

        /// <summary>
        /// This function return the Host with the <paramref name="key"/>
        /// </summary>
        /// <param name="key">The requested <see cref="Host"/>'s KEY</param>
        /// <returns>The Host with the  <paramref name="key"/></returns>
        Host GetHost(int key);

        #endregion
        
        #endregion
    }
}
