using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
        // No need for the singletory functions to be here... They are in
        /// <see cref="DalImp"/>

        #region Guest

        void AddGuest(Guest g);

        bool CheckIfGuestExists(int key);

        bool CheckIfGuestExists(string username);

        IEnumerable<Guest> GetAllGuests();

        Guest GetGuest(int key);

        void RemoveGuest(int key);

        #endregion

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

        #region RemoveGuestRequest This function removes a guest request

        /// <summary>
        /// This function removes a guest request from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <exception cref="KeyNotFoundException">Thrown if no guest request in the data match the guest request with the <paramref name="key"/></exception>
        void RemoveGuestRequest(int key);

        #endregion

        #region UpdateGuestRequestStatus This function updates a guest request

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat);

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

        #region UpdateOrderStatus This function updates an order

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
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

        #region BankAccount These functions perform actions on BankAccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
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

        bool CheckIfHostExists(string username);

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

        #region Config Values Functions.

        int GetGuestRequestKey();


        int GetBankNumber();


        int GetHostingUnitKey();


        int GetOrderKey();


        float GetCommission();


        int GetNumberOfDaysUntilExpired();


        int GetGuestKey(string userName);
        string GetGuestUserName(int key);
        void WriteGuestToFile(string username, string password, int key);
        void WriteHostToFile(string username, string password, int hostKey);
        void AddHost(Host host);
        bool HostCompareToPasswordInFile(string username, string password);
        int GetHostKey(string username);
        bool GuestCompareToPasswordInFile(string username, string password);
        bool AdminCompareToPasswordInFile(string username, string password);


        #endregion
    }
}
