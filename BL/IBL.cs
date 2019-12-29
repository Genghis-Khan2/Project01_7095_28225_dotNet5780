using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;
using System.Linq;

namespace BL
{
    /// <summary>
    /// This delegate defines a function that accepts a hosting unit and returns the result of some logical operation on it
    /// </summary>
    /// <param name="hostingUnit">The hosting unit to work on it</param>
    /// <returns>The result of func</returns>
    /// <remarks>
    /// We chose to put this delegate here even though it only used the BL layer (*** function)
    /// because we couldn't find another place to put it,
    /// it couldn't be put in the IBL interface (put delegate in interface is problematic)
    /// and we couldn't put it in implementation (BLImp ) because That is already expressed in the declaration of the function in the interface.
    /// We also couldn't put it in the configuration class because it was defined as a class that is responsible for global variables
    /// and all is variables are static.
    /// So, the only option we are given is to put it here and hope that our severe grime in breaking the layer model is understood
    /// </remarks>
    public delegate bool isMeetTheDefinition(HostingUnit hostingUnit);//TODO: fix the coment
    public interface IBL
    {

        //TODO: add all the function prototype in BLImp to here
        #region Functions to work with the data, similar to the functions in IDAL

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
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        void UpdateOrder(int key, Enums.OrderStatus stat);

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

        #endregion

        #region Functions specialy added in BL layer, not found in IDAL

        #region Other function
        //TDOO:find good defintion to this functions
        IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days);

        int getNumberOfDateInRange(DateTime startDay);

        int getNumberOfDateInRange(DateTime startDay, DateTime endDay);

        IEnumerable<Order> getAllOrderInRange(int numberOfDays);
        IEnumerable<GuestRequest> getAllGuestRequestWhere(isMeetTheDefinition func);

        int getAmountOfOrderToGuest(GuestRequest guestRequest);

        int getAllsuccessfulOrder(HostingUnit hostingUnit);
        #endregion

        #region Grouping functions
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> getAllGuestByArea();
        IEnumerable<IGrouping<int, GuestRequest>> getAllGuestByNumerOfVacationers(Enums.Area area);//TODO:need to check if did all the grouping function
        IEnumerable<IGrouping<int, Host>> getAllHostByNumberOfHostingUnits();
        IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea(Enums.Area area);
        #endregion

        #endregion
    }

}
