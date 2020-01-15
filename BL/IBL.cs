using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;
using System.Linq;

namespace BL
{
    /// <summary>
    /// This delegate defines a function that accepts a Guest request and returns the result of some logical operation on it
    /// </summary>
    /// <param name="guestRequest">The hosting unit to work on it</param>
    /// <returns>The result of func</returns>
    /// <remarks>
    /// We chose to put this delegate here even though its not the ideal way to do it
    /// because we couldn't find another place to put it,
    /// it couldn't be put in the IBL interface (put delegate in interface is problematic)
    /// and we couldn't put it in implementation (BLImp ) because That is already expressed in the declaration of the function in the interface.
    /// We also couldn't put it in the configuration class because it was defined as a class that is responsible for global variables
    /// and all is variables are static.
    /// So, the only option we are given is to put it here and hope that our severe grime in breaking the layer model is understood
    /// </remarks>
    public delegate bool isMeetTheDefinition(Object obj);

    /// <summary>
    /// This class is the interface of the BL layer
    /// </summary>
    public interface IBL
    {
        ///<remarks>No need for the singletory functions to be here... They are in <see cref="DalImp"/></remarks>
        #region Functions used to work with the data, also exist in the DAL layer

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
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat);

        #endregion

        #region GetGuestRequest This function return guest request

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
        /// <param name="hostingUnit">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        void UpdateHostingUnit(HostingUnit hostingUnit, int key);

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

        #region UpdateOrderStatus This function updates an order

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        void UpdateOrderStatus(int key, Enums.OrderStatus stat);

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

        #region GetBankBranch This function return BankAccount

        /// <summary>
        /// This function return BankBranch according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the BankBranch</param>
        BankBranch GetBankBranch(int key);

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

        #region IfExists These function check if object exsits in the data

        #region CheckIfGuestRequestExists This function check if guestRequest exists in the data

        /// <summary>
        /// This function return if guestRequest exists in the data
        /// </summary>
        /// <param name="key">The key of the guestRequest</param>
        /// <returns>boolean, if the guestRequest exists or not</returns>
        bool CheckIfGuestRequestExists(int key);

        #endregion

        #region CheckIfHostingUnitExists This function check if hostingUnit exists in the data

        /// <summary>
        /// This function return if hostingUnit exists in the data
        /// </summary>
        /// <param name="key">The key of the hostingUnit</param>
        /// <returns>boolean, if the hostingUnit exists or not</returns>
        bool CheckIfHostingUnitExists(int key);

        #endregion

        #region CheckIfOrderExists This function check if order exists in the data

        /// <summary>
        /// This function return if order exists in the data
        /// </summary>
        /// <param name="key">The key of the order</param>
        /// <returns>boolean, if the order exists or not</returns>
        bool CheckIfOrderExists(int key);

        #endregion

        #region CheckIfBankAccountExists This function check if bankAccount exists in the data

        /// <summary>
        /// This function return if bankAccount exists in the data
        /// </summary>
        /// <param name="key">The key of the bankAccount</param>
        /// <returns>boolean, if the bankAccount exists or not</returns>
        bool CheckIfBankAccountExists(int key);

        #endregion

        #region CheckIfHostExists This function check if host exists in the data

        /// <summary>
        /// This function return if host exists in the data
        /// </summary>
        /// <param name="key">The key of the host</param>
        /// <returns>boolean, if the host exists or not</returns>
        bool CheckIfHostExists(int key);

        #endregion

        #endregion

        #endregion

        #region Auxiliary functions used to process data from the DAL layer

        #region Functions that perform date-related calculations

        #region GetNumberOfDateInRange This function return the amount of date in range

        int GetNumberOfDateInRange(DateTime startDay);

        #endregion

        #region GetNumberOfDateInRange This function return the amount of day in range

        int GetNumberOfDateInRange(DateTime startDay, DateTime endDay);


        #endregion

        #region IsLeastThenOneDay This function check if date is at least one day before the second date

        /// <summary>
        /// This function check if <paramref name="date1"/> is at least one day before the <paramref name="date2"/>
        /// </summary>
        /// <param name="date1">First date</param>
        /// <param name="date2">Second date</param>
        /// <returns>Boolean, if date is at least one day before the second date</returns>
        bool IsLeastThenOneDay(DateTime date1, DateTime date2);

        #endregion

        #endregion

        #region Function to work with GuestRequest

        #region GetAllGuestRequestWhere This function return all the GuestRequest that match condition

        /// <summary>
        /// The function return all the GuestRequest That matches certain conditions (defined by <see cref="isMeetTheDefinition"/>)
        /// </summary>
        /// <param name="func">Function(defined by <see cref="isMeetTheDefinition"/>)</param>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of all the GuestRequest That match the condition</returns>
        IEnumerable<GuestRequest> GetAllGuestRequestWhere(isMeetTheDefinition func);

        #endregion

        #region GetAmountOfOrderToGuest This function return the amount of order sent to GuestRequst

        /// <summary>
        /// The function return all the <see cref="Order"/> sent to <paramref name="guestRequest"/>
        /// </summary>
        /// <param name="key">The Guest Request key to check how many <see cref="Order"/> where sent to her </param>
        /// <returns>The amount of order sent to the GuestRequest</returns>
        int GetAmountOfOrderToGuest(int key);

        #endregion

        #region GetAllGuestRequestToGuest This function return all the GuestRequest send from user

        IEnumerable<GuestRequest> GetAllGuestRequestToGuest(int key);
        
        #endregion

        #endregion

        #region GetAllAvailableHostingUnit This function return all the Available Hosting unit int range

        /// <summary>
        /// The function returns the list of all available hosting units starting in the <paramref name="date"/> and ending <paramref name="days"/> days later
        /// </summary>
        /// <param name="date">Start date</param>
        /// <param name="days">How many days</param>
        /// <returns><see cref="IEnumerable{HostingUnit}"/> to go over the list of all free hosting unit in the range</returns>
        IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days);

        #endregion

        #region GetAllOrderInRange This function return all the order where sent in some days

        /// <summary>
        /// The function return all the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </summary>
        /// <param name="numberOfDays">The amount of day to check</param>
        /// <returns>All the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </returns>
        IEnumerable<Order> GetAllOrderInRange(int numberOfDays);

        #endregion

        #region GetAllsuccessfulOrder This function return the amount of successful order sent to the HostingUnit

        /// <summary>
        /// The function returns the number of orders sent\the number
        /// of successfully closed orders for <paramref name="hostingUnit"/>
        /// </summary>
        /// <param name="key">The hosting unit key to check</param>
        /// <returns>The number of orders sent\the number  of successfully closed orders for <paramref name="hostingUnit"/>
        /// </returns>
        int GetAllsuccessfulOrder(int key);

        #endregion

        #region Function to work with Diary array

        #region CheckIfAvailable This function check if the diary available in the range

        /// <summary>
        /// The function return if the range between <paramref name="entryDate"/> to <paramref name="ReleaseDate"/> is Available
        /// </summary>
        /// <param name="diary">The Array of all the date in the year</param>
        /// <param name="entryDate">Entry date of the range</param>
        /// <param name="ReleaseDate">Release date of the range</param>
        /// <returns>Boolean, if the range is available</returns>
        bool CheckIfAvailable(bool[,] diary, DateTime entryDate, DateTime ReleaseDate);

        #endregion

        #region MarkingInTheDiary This function mark a vaction in the diary

        /// <summary>
        /// This function mark a vaction(<paramref name="enteryDate"/> - <paramref name="releaseDate"/>) in <paramref name="diary"/>
        /// </summary>
        /// <param name="diary">The array represent all the days in the year</param>
        /// <param name="enteryDate">Start date of the vaction</param>
        /// <param name="releaseDate">End date of the vaction</param>
        /// <remarks>This function assume that the range is available and dosnt check it, to check use the<see cref="CheckIfAvailable(bool[,], DateTime, DateTime)"/> function</remarks>
        bool[,] MarkingInTheDiary(bool[,] diary, DateTime enteryDate, DateTime releaseDate);

        #endregion

        #endregion

        #region Function to work with status

        #region IsClosed This function return if Order is closed

        /// <summary>
        /// This function return if Order is closed 
        /// </summary>
        /// <param name="ord">The order to check is status</param>
        /// <returns>boolean, if the status is closed or not</returns>
        bool IsClosed(Enums.OrderStatus stat);

        #endregion

        #region IsClosed This function return if GuestRequest is closed

        /// <summary>
        /// This function return if guestRequest is closed 
        /// </summary>
        /// <param name="ord">The guestRequest to check is status</param>
        /// <returns>boolean, if the status is closed or not</returns>
        bool IsClosed(Enums.RequestStatus stat);

        #endregion

        #endregion

        #endregion

        #region Grouping functions

        #region GetAllGuestByArea This function return all the GuestRequest group by Area

        /// <summary>
        /// The function return all the GuestRequest group by area
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by area</returns>
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> GetAllGuestByArea();

        #endregion

        #region GetAllGuestByNumerOfVacationers This function return all the GuestRequest group by number of Vacationers

        /// <summary>
        /// The function return all the GuestRequest group by number of Vacationers
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by number of Vacationers</returns>
        IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestByNumerOfVacationers();

        #endregion

        #region GetAllHostByNumberOfHostingUnits This function return all the Host group by number of Hosting unit

        /// <summary>
        /// The function return all the Host group by the number of Hosting unit they have
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all Host group by the number of Hosting unit they have</returns>
        IEnumerable<IGrouping<int, Host>> GetAllHostByNumberOfHostingUnits();

        #endregion

        #region GetHostingUnitByArea This function return all the HostingUnit group by Area

        /// <summary>
        /// The function return all the Hosting unit group by area
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all the Hosting unit group by area</returns>
        IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea();

        #endregion

        #region getHostingUnitByHost This function return all the HostingUnit group by There Host

        /// <summary>
        ///  This function return all the HostingUnit group by There Host
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all HostingUnit group by there host</returns>
        IEnumerable<IGrouping<Host, HostingUnit>> getHostingUnitByHost();
        #endregion

        #endregion

    }

}
