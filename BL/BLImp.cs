using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using Exceptions;

namespace BL
{
    /// <summary>
    /// Implementation of the BL.
    /// Implemented using lists for the data types
    /// See <see cref="IBL"/> for the BL interface
    /// </summary>
    public class BLImp : IBL
    {
        #region Singletory These parts are what make the class a singletory

        private BLImp() { }

        //TODO: check all the Exeption DAL level throw and check they treated
        protected static BLImp instance = null;

        /// <summary>
        /// This is the factory method of BLImp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        static public IBL getBL()
        {
            if (instance == null)
            {
                instance = new BLImp();
                return instance;
            }
            return instance;
        }

        #endregion

        #region Functions used to work with the data also exist in the DAL layer

        #region GuestRequest These functions perform actions on GuestRequests

        #region AddGuestRequest This function adds a guest request

        /// <summary>
        /// This function add GuestRequest to the data
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <exception cref="ArgumentException">Thrown when the vacation start date is not at least one day before the vacation end date</exception>
        /// <param name="gr">The GuestRequst to add</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            //TODO:do it
            //תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
            throw new NotImplementedException();
        }

        #endregion

        #region GetAllGuestRequests This function returns the guest requests

        /// <summary>
        /// This function return all the GuestRequest in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no guest requests</exception>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return DalImp.GetDal().GetAllGuestRequests();
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
            //TODO: write the function
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region HostingUnit These functions perform actions on HostingUnits

        #region AddHostingUnit This function adds a hosting unit

        /// <summary>
        /// Add HostingUnit to the data
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="hostingUnit">The HostingUnit to add</param>
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            //TODO: we need notItems for Host?
            throw new NotImplementedException();
        }

        #endregion

        #region GetAllHostingUnits This function returns all hosting units

        /// <summary>
        /// The Function return all the Hosting unit
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no items in the hosting units list</exception>
        /// <returns><see cref="IEnumerable{HostingUnit}"/> to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return DalImp.GetDal().GetAllHostingUnits();
        }

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit in the data match the <paramref name="hostingUnit"/></exception>
        ///<exception cref="DeleteWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to <paramref name="hostingUnit"/> </exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            //TODO: write the function
            //• לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.
            throw new NotImplementedException();
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
            //TODO: write the function
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Order These functions perform actions on Orders

        #region AddOrder This function adds and order

        /// <summary>
        /// Add Order to the data
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <exception cref="InfoNotExistsException">Thrown when the GuestRequest or HostingUnit of the Order does not exist</exception>
        /// <param name="ord">Order to add</param>
        public void AddOrder(Order ord)
        {
            //TODO: write the function
            //TODO: dont need exception when HostingUnit and GuestRequest isnt exist?
            //יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו.
            throw new NotImplementedException();
        }

        #endregion

        #region GetAllOrders This function returns all the orders

        /// <summary>
        /// This function return all the Order
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no orders in the list</exception>
        /// <returns><see cref="IEnumerable{Order}"/> to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return DalImp.GetDal().GetAllOrders();
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
            //TODO: write the function
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region BankAccount These functions perform actions on BankAccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// The function returns the list of all existing bank branches in Israel
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns><see cref="IEnumerable{BankBranch}"/> to go over the list of bank accounts</returns>
        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            return DalImp.GetDal().GetAllBankAccounts();
        }

        #endregion

        #endregion

        #endregion


        /// <summary>
        /// The function return all the GuestRequest That matches certain conditions (defined by <see cref="isMeetTheDefinition"/>)
        /// </summary>
        /// <param name="func">Function(defined by <see cref="isMeetTheDefinition"/>)</param>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of all the GuestRequest That match the condition</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequestWhere(isMeetTheDefinition func)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the <see cref="Order"/> sent to <paramref name="guestRequest"/>
        /// </summary>
        /// <param name="guestRequest">The Guest Request to check how many <see cref="Order"/> where sent to her </param>
        /// <exception cref="KeyNotFoundException">Thrown when there isnt GuestRequst in data that exsist the <paramref name="key"/></exception>
        /// <returns>The amount of order sent to the GuestRequest</returns>
        public int GetAmountOfOrderToGuest(GuestRequest guestRequest)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the list of all available hosting units starting in the <paramref name="date"/> and ending <paramref name="days"/> days later
        /// </summary>
        /// <param name="date">Start date</param>
        /// <param name="days">How many days</param>
        /// <returns><see cref="IEnumerable{HostingUnit}"/> to go over the list of all free hosting unit in the range</returns>
        public IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the days from the <paramref name="startDay"/> to now
        /// </summary>
        /// <param name="startDay">Start day of the count</param>
        /// <returns>All the day from <paramref name="startDay"/> to now</returns>
        public int GetNumberOfDateInRange(DateTime startDay)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the days from the <paramref name="startDay"/> to <paramref name="endDay"/>
        /// </summary>
        /// <param name="startDay">Start date for counting</param>
        /// <param name="endDay">End day of the counting</param>
        /// <returns>All the days from <paramref name="startDay"/> to <paramref name="endDay"/></returns>
        public int GetNumberOfDateInRange(DateTime startDay, DateTime endDay)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </summary>
        /// <param name="numberOfDays">The amount of day to check</param>
        /// <returns>All the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </returns>
        public IEnumerable<Order> GetAllOrderInRange(int numberOfDays)
        {
            //TODO: do it
            throw new NotImplementedException();
        }



        /// <summary>
        /// The function returns the number of orders sent\the number
        /// of successfully closed orders for <paramref name="hostingUnit"/>
        /// </summary>
        /// <param name="hostingUnit">The hosting unit to check</param>
        /// <returns>The number of orders sent\the number  of successfully closed orders for <paramref name="hostingUnit"/>
        /// </returns>
        public int GetAllsuccessfulOrder(HostingUnit hostingUnit)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        #region Functions used for grouping


        /// <summary>
        /// The function return all the GuestRequest group by <see cref="Enums.Area"/> 
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> GetAllGuestByArea()
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the GuestRequest group by number of Vacationers
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by number of Vacationers</returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestByNumerOfVacationers(Enums.Area area)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }//TODO:need to check if did all the grouping function

        /// <summary>
        /// The function return all the Host group by the number of Hosting unit they have
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all Host group by the number of Hosting unit they have</returns>
        public IEnumerable<IGrouping<int, Host>> GetAllHostByNumberOfHostingUnits()
        {
            //TODO: do it
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function return all the Hosting unit group by area
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all the Hosting unit group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea(Enums.Area area)
        {
            //TODO: do it
            throw new NotImplementedException();
        }

        #endregion

    }
}

/*
tasks:
2. write all the definition in IBL
3. Write comments to all the function in BLimp
4. write coment in IBL
4.5. לסדר ע"י רגין
5. לעבור על התנאים בתרגיל ולוודא שנזרקים חריגות בהתאם
6. לכתוב את הפונקציות
     */
