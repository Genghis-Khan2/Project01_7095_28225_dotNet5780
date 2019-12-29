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

        #region Functions used to work with the data, also exist in the DAL layer

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
            //REMARK: תאריך תחילת הנופש קודם לפחות ביום אחד לתאריך סיום הנופש
            if (gr.EntryDate.AddDays(1) > gr.ReleaseDate)
            {
                throw new ArgumentException("The release date must be at least one day after the entry date", "ReleaseDate");
            }
            DalImp.GetDal().AddGuestRequest(gr);
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

        #region UpdateGuestRequest This function updates a guest request status

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        ///<exception cref="AlreadyClosedException">Thrown when tryin to change the status of GuestRequest Whose status has already been set to "closed"</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        ///<remarks>I assume that like <see cref="UpdateOrder(int, Enums.OrderStatus)"/> if the status is already close its need to throw Exception</remarks>
        public void UpdateGuestRequest(int key, Enums.RequestStatus stat)
        {
            GuestRequest gr = DalImp.GetDal().GetGuestRequest(key);
            if (gr.Status == Enums.RequestStatus.ClosedWithDeal || gr.Status == Enums.RequestStatus.CloseWithExpired)
                throw new AlreadyClosedException("GuestRequest", gr.GuestRequestKey);
            DalImp.GetDal().UpdateGuestRequest(key, stat);
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
            DalImp.GetDal().AddHostingUnit(hostingUnit);
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
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit in the data match the hosting unit with the <paramref name="key"/></exception>
        ///<exception cref="ChangedWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to the hosting unit with the <paramref name="key"/> and you try to change the <see cref="Host.CollectionClearance"/> property in the <see cref="HostingUnit.Owner"/> property</exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            //TODO: write the function
            //REMARK לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.

            var allOrders = DalImp.GetDal().GetAllOrders();
var all
            throw new NotImplementedException();
        }

        #endregion

        #region UpdateHostingUnit This function updates a hosting unit

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when hosting unit with <paramref name="key"/> is not found</exception>
        /// <exception cref="de"
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            //TODO: write the function
            //REMARK: • לא ניתן לבטל הרשאה לחיוב חשבון כאשר יש הצעה הקשורה אליה במצב פתוח.
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
            //REMARK: יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו.
            //REMARK: לא ניתן לקבוע אירוח לתאריך שכבר תפוס ע"י לקוח אחר
            //REMARK: • אם מוסיפים הזמנה, אזי יש לוודא שהלקוח ויחידת האירוח אכן קיימים.
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

        #region UpdateOrder This function updates an order status

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        ///<exception cref="AlreadyClosedException">Thrown when tryin to change the status of Order Whose status has already been set to "closed"</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        public void UpdateOrder(int key, Enums.OrderStatus stat)
        {
            //TODO: write the function
            //REMARK: בעל יחידת אירוח יוכל לשלוח הזמנה ללקוח )שינוי הסטטוס ל "נשלח מייל"(, רק אם חתם על הרשאה לחיוב חשבון בנק
            //REMARK: לאחר שסטטוס ההזמנה השתנה לסגירת עיסקה – לא ניתן לשנות יותר את הסטטוס שלה.
            //REMARK: כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לבצע חישוב עמלה בגובה של 10 ₪ ליום אירוח. )עיין הערה למטה(
            //REMARK: כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לסמן במטריצה את התאריכים הרלוונטיים.
            //REMARK: כאשר סטטוס הזמנה משתנה עקב סגירת עסקה – יש לשנות את הסטטוס של דרישת הלקוח בהתאם, וכן לשנות את הסטטוס של כל ההזמנות האחרות של אותו לקוח.
            //REMARK:   כאשר סטטוס ההזמנה משתנה ל"נשלח מייל" – המערכת תשלח באופן אוטומטי מייל  ללקוח עם פרטי ההזמנה. ניתן לדחות את הביצוע בפועל של שליחת המייל לשלב הבא, וכעת רק להדפיס הודעה על המסך.
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

        #region Auxiliary functions used to process data from the DAL layer

        #region Functions that perform date-related calculations

        #region GetNumberOfDateInRange This function return the amount of date in range

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

        #endregion

        #region GetNumberOfDateInRange This function return the amount of day in range

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

        #endregion

        #endregion

        #region GetAllGuestRequestWhere This function return all the GuestRequest that match condition

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

        #endregion

        #region GetAmountOfOrderToGuest This function return the amount of order sent to GuestRequst

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

        #endregion

        #region GetAllAvailableHostingUnit This function return all the Available Hosting unit int range

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
        public IEnumerable<Order> GetAllOrderInRange(int numberOfDays)
        {
            //TODO: do it
            throw new NotImplementedException();
        }

        #endregion

        #region GetAllsuccessfulOrder This function return the amount of successful order sent to the HostingUnit

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

        #endregion 

        #endregion

        #region Grouping functions

        #region GetAllGuestByArea This function return all the GuestRequest group by Area

        /// <summary>
        /// The function return all the GuestRequest group by <see cref="Enums.Area"/> 
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> GetAllGuestByArea()
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        #endregion

        #region GetAllGuestByNumerOfVacationers This function return all the GuestRequest group by number of Vacationers

        /// <summary>
        /// The function return all the GuestRequest group by number of Vacationers
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by number of Vacationers</returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestByNumerOfVacationers(Enums.Area area)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }//TODO:need to check if did all the grouping function

        #endregion

        #region GetAllHostByNumberOfHostingUnits This function return all the Host group by number of Hosting unit

        /// <summary>
        /// The function return all the Host group by the number of Hosting unit they have
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all Host group by the number of Hosting unit they have</returns>
        public IEnumerable<IGrouping<int, Host>> GetAllHostByNumberOfHostingUnits()
        {
            //TODO: do it
            throw new NotImplementedException();
        }

        #endregion

        #region GetHostingUnitByArea This function return all the HostingUnit group by Area

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

        #endregion

    }
}

/*
tasks:
6. לכתוב את הפונקציות
7.לוודא שכל התנאים מומשו(כל הלינקיו וכו')
8. לבדוק את הפונקציות
    TODO: do auto refactor to all code
     * */


