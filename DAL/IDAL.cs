using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
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
        /// This function updates a guest request
        /// </summary>
        /// <param name="gr">Guest request to update to</param>
        /// <param name="key">Key of guest request to update</param>
        void UpdateGuestRequest(GuestRequest gr, int key);
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
        /// This function updates an order
        /// </summary>
        /// <param name="ord">Order to update to</param>
        /// <param name="key">Key of order to update</param>
        void UpdateOrder(Order ord, int key);
        #endregion
        #endregion

        #region GetAllBankAccounts This function returns all the bank accounts
        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        IEnumerable<BankAccount> GetAllBankAccounts();
        #endregion
    }
}
