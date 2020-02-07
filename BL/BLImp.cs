using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
using Exceptions;
using System.Net;
using System.Net.Mail;

namespace BL
{
    /// <summary>
    /// Implementation of the BL.
    /// Implemented using lists for the data types
    /// See <see cref="IBL"/> for the BL interface
    /// </summary>
    /// <remarks>
    /// For represent dates we use the <see cref="DateTime"/> class But because we are always talking about the range
    /// of that year (<see cref="HostingUnit.Diary"/> represents one year) we do not refer to the "year" part of the object, 
    /// so for convenience in calculations we always place it to 2020 
    /// </remarks>
    public class BLImp : IBL
    {
        #region Singletory These parts are what make the class a singletory

        private BLImp() { }

        protected static BLImp instance = null;

        /// <summary>
        /// This is the factory method of BLImp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        static public IBL GetBL()
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
            if (!IsLeastThenOneDay(gr.EntryDate, gr.ReleaseDate))
            {
                throw new ArgumentException("The release date must be at least one day after the entry date", "ReleaseDate");
            }
            try
            {
                DAL_Adapter.GetDAL().AddGuestRequest(gr);
            }
            catch (AlreadyExistsException e)
            {
                throw new AlreadyExistsException(e.Message);
            }
        }

        #endregion

        #region GetAllGuestRequests This function returns all the guest requests

        /// <summary>
        /// This function return all the GuestRequest in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no guest requests</exception>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            try
            {
                return DAL_Adapter.GetDAL().GetAllGuestRequests();
            }
            catch (NoItemsException e)
            {
                throw new NoItemsException(e.Message);
            }
        }

        #endregion

        #region RemoveGuestRequest This function removes a guest request

        /// <summary>
        /// This function removes a guest request from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <exception cref="KeyNotFoundException">Thrown if no guest request in the data match the guest request with the <paramref name="key"/></exception>
        ///<exception cref="ChangedWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to the guest request with the <paramref name="key"/> and you try to delete it</exception>        /// <param name="key">Key to remove the guest request of</param>
        public void RemoveGuestRequest(int key)
        {
            //TODO:do it
            //we assume that An Order considered "open" if  its status is "Enums.OrderStatus.UnTreated" and also "Enums.OrderStatus.SentMail"
            bool isEmpty = false;
            try // We do this since if we want to delete a hosting unit before an order is made
            {
                DAL_Adapter.GetDAL().GetAllOrders();
            }
            catch (NoItemsException)
            {
                isEmpty = true;
            }
            if (!isEmpty)
            {
                var linkedOpenOrderList = from order in DAL_Adapter.GetDAL().GetAllOrders()
                                          where order.GuestRequestKey == key && (!IsClosed(order.Status))
                                          select order;
                if (linkedOpenOrderList.Count() != 0)
                    throw new ChangedWhileLinkedException("delete", "GuestRequest", key, "Order", linkedOpenOrderList.First().OrderKey);
            }
            try
            {
                DAL_Adapter.GetDAL().RemoveGuestRequest(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        #region UpdateGuestRequestStatus This function updates a guest request status

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        ///<exception cref="AlreadyClosedException">Thrown when tryin to change the status of GuestRequest Whose status has already been set to "closed"</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        ///<remarks>I assume that like <see cref="UpdateOrderStatus(int, Enums.OrderStatus)"/> if the status is already close itsn't need to throw Exception</remarks>
        public void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat)
        {
            if (!CheckIfGuestRequestExists(key))
                throw new KeyNotFoundException("There is no order with the key specified");
            GuestRequest gr = DAL_Adapter.GetDAL().GetGuestRequest(key);
            if (IsClosed(gr.Status) && !IsClosed(stat))
                throw new AlreadyClosedException("GuestRequest", gr.GuestRequestKey);
            try
            {
                DAL_Adapter.GetDAL().UpdateGuestRequestStatus(key, stat);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        #region GetGuestRequest This function return guest request

        /// <summary>
        /// This function return GuestRequest according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the GuestRequest</param>
        /// <returns>The GuestRequest with the <paramref name="key"/></returns>
        public GuestRequest GetGuestRequest(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetGuestRequest(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
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
            try
            {
                DAL_Adapter.GetDAL().AddHostingUnit(hostingUnit);
            }
            catch (AlreadyExistsException e)
            {

                throw new AlreadyExistsException(e.Message);
            }
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
            try
            {
                return DAL_Adapter.GetDAL().GetAllHostingUnits();
            }
            catch (NoItemsException)
            {
                throw;
            }
        }

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit in the data match the hosting unit with the <paramref name="key"/></exception>
        ///<exception cref="ChangedWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to the hosting unit with the <paramref name="key"/> and you try to delete it</exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            //we assume that An Order considered "open" if  its status is "Enums.OrderStatus.UnTreated" and also "Enums.OrderStatus.SentMail"
            //REMARK לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.
            bool isEmpty = false;
            try // We do this since if we want to delete a hosting unit before an order is made
            {
                DAL_Adapter.GetDAL().GetAllOrders();
            }
            catch (NoItemsException)
            {
                isEmpty = true;
            }
            if (!isEmpty)
            {
                var linkedOpenOrderList = from order in DAL_Adapter.GetDAL().GetAllOrders()
                                          where order.HostingUnitKey == key && (!IsClosed(order.Status))
                                          select order;
                if (linkedOpenOrderList.Count() != 0)
                    throw new ChangedWhileLinkedException("delete", "HostingUnit", key, "Order", linkedOpenOrderList.First().OrderKey);
            }
            try
            {
                DAL_Adapter.GetDAL().RemoveHostingUnit(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        #region UpdateHostingUnit This function updates a hosting unit

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when hosting unit with <paramref name="key"/> is not found</exception>
        ///<exception cref="ChangedWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to the hosting unit with the <paramref name="key"/> and you try to change the <see cref="Host.CollectionClearance"/> property in the <see cref="HostingUnit.Owner"/> property</exception>
        /// <param name="hostingUnit">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hostingUnit, int key)
        {
            //we assume that An Order considered "open" if  its status is "Enums.OrderStatus.UnTreated" and also "Enums.OrderStatus.SentMail
            //REMARK: • לא ניתן לבטל הרשאה לחיוב חשבון כאשר יש הצעה הקשורה אליה במצב פתוח.
            Host hostBeforeUpdating;
            try
            {
                hostBeforeUpdating = DAL_Adapter.GetDAL().GetHostingUnit(key).Owner;
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }

            if (!hostingUnit.Owner.CollectionClearance && hostBeforeUpdating.CollectionClearance)//If you try to cancel the account cancellation permission
            {
                var linkedOpenOrderList = from order in DAL_Adapter.GetDAL().GetAllOrders()
                                          where order.HostingUnitKey == key && (order.Status == Enums.OrderStatus.UnTreated || order.Status == Enums.OrderStatus.SentMail)
                                          select order;
                if (linkedOpenOrderList.Count() != 0)
                    throw new ChangedWhileLinkedException("change CollectionClearance of", "HostingUnit", key, "Order", linkedOpenOrderList.First().OrderKey);

            }
            DAL_Adapter.GetDAL().UpdateHostingUnit(hostingUnit, key);
        }
        #endregion

        #region GetHostingUnit This function return HostingUnit

        /// <summary>
        /// This function return HostingUnit according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the HostingUnit</param>
        /// <returns>The HostingUnit with the <paramref name="key"/></returns>
        public HostingUnit GetHostingUnit(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetHostingUnit(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
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
        /// <exception cref="OccupiedDatesException">Thrown When requested dates are not available in the hosting unit (ie occupied by another)</exception>
        /// <param name="ord">Order to add</param>
        public void AddOrder(Order ord)
        {
            ///<remarks>we dont need to check if the entry date and the release date are good because its checked when you create the guestRequest(in <see cref="AddGuestRequest(GuestRequest)"/>)</remarks>
            //REMARK: יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו.
            //REMARK: לא ניתן לקבוע אירוח לתאריך שכבר תפוס ע"י לקוח אחר
            //REMARK: • אם מוסיפים הזמנה, אזי יש לוודא שהלקוח ויחידת האירוח אכן קיימים.

            //check that the GuestRequest and HostingUnit of the Order exist
            if (!CheckIfHostingUnitExists(ord.HostingUnitKey))
                throw new InfoNotExistsException("HostingUnit", "Order");
            if (!CheckIfGuestRequestExists(ord.GuestRequestKey))
                throw new InfoNotExistsException("GuestRequest", "Order");

            //check that the requested dates are available in the hosting unit (ie. not occupied by another), 
            //its will never throw the "KeyNotFoundException" because we already check that the HostingUnit and the GuestRequest are exists
            if (!CheckIfAvailable(DAL_Adapter.GetDAL().GetHostingUnit(ord.HostingUnitKey).Diary, DAL_Adapter.GetDAL().GetGuestRequest(ord.GuestRequestKey).EntryDate, DAL_Adapter.GetDAL().GetGuestRequest(ord.GuestRequestKey).ReleaseDate))
                throw new OccupiedDatesException();
            try
            {
                DAL_Adapter.GetDAL().AddOrder(ord);
            }
            catch (AlreadyExistsException e)
            {
                throw new AlreadyExistsException(e.Message);
            }
            catch (SmtpException)
            {
                throw;
            }
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
            try
            {
                return DAL_Adapter.GetDAL().GetAllOrders();
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        #region UpdateOrderStatus This function updates an order status

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        ///<exception cref="AlreadyClosedException">Thrown when tryin to change the status of Order Whose status has already been set to "closed"</exception>
        ///<exception cref="UnauthorizedActionException">Throw when try to change the status to <see cref="Enums.OrderStatus.SentMail"/> but the <see cref="Host.CollectionClearance"/> is false</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        public void UpdateOrderStatus(int key, Enums.OrderStatus stat)
        {
            //REMARK: בעל יחידת אירוח יוכל לשלוח הזמנה ללקוח )שינוי הסטטוס ל "נשלח מייל"(, רק אם חתם על הרשאה לחיוב חשבון בנק - done
            //REMARK: לאחר שסטטוס ההזמנה השתנה לסגירת עיסקה – לא ניתן לשנות יותר את הסטטוס שלה. - done
            //REMARK: כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לבצע חישוב עמלה בגובה של 10 ₪ ליום אירוח. )עיין הערה למטה(
            //REMARK: כאשר סטטוס ההזמנה משתנה בגלל סגירת עסקה – יש לסמן במטריצה את התאריכים הרלוונטיים. - done
            //REMARK: כאשר סטטוס הזמנה משתנה עקב סגירת עסקה – יש לשנות את הסטטוס של דרישת הלקוח בהתאם, וכן לשנות את הסטטוס של כל ההזמנות האחרות של אותו לקוח. - done
            //REMARK:   כאשר סטטוס ההזמנה משתנה ל"נשלח מייל" – המערכת תשלח באופן אוטומטי מייל  ללקוח עם פרטי ההזמנה. ניתן לדחות את הביצוע בפועל של שליחת המייל לשלב הבא, וכעת רק להדפיס הודעה על המסך. -done
            if (!CheckIfOrderExists(key))
                throw new KeyNotFoundException("There is no order with the key specified");

            Order ord = GetOrder(key);

            //I assumed that when the status is changed to close ("CustomerResponsiveness" or "CustomerUnresponsiveness") 
            //you can still change the type of close but not to any open status("UnTreated" or "SentMail")
            if (IsClosed(ord.Status) && !IsClosed(stat))
                throw new AlreadyClosedException("Order", key);

            if (stat == Enums.OrderStatus.SentMail)
            {
                if (!GetHostingUnit(ord.HostingUnitKey).Owner.CollectionClearance)
                    throw new UnauthorizedAccessException("a host cannot send an email if it does not authorize an account billing authorization");
                Console.WriteLine("Send mail");
                DAL_Adapter.GetDAL().UpdateOrderStatus(key, Enums.OrderStatus.SentMail);
            }

            if (stat == Enums.OrderStatus.ClosedByCustomerResponsiveness)
            {
                HostingUnit hostingUnit = GetHostingUnit(ord.HostingUnitKey);
                GuestRequest guestRequest = GetGuestRequest(ord.GuestRequestKey);

                ///<remarks>
                ///This log check is here in addition to the check when creating the order(<see cref="AddOrder(Order)"/>)
                ///to avoid a situation where 2 guestRequest have booked the same day and the one host send order to each of them 
                ///and one of them approved then if the other approve and mark even though the dates are busy, he can do it
                ///so we added another check her
                ///</remarks>
                if (CheckIfAvailable(hostingUnit.Diary, guestRequest.EntryDate, guestRequest.ReleaseDate))
                {
                    hostingUnit.Diary = MarkingInTheDiary(hostingUnit, guestRequest.EntryDate, guestRequest.ReleaseDate);
                    UpdateHostingUnit(hostingUnit, ord.HostingUnitKey);
                }
                else
                {
                    DAL_Adapter.GetDAL().UpdateOrderStatus(key, Enums.OrderStatus.ClosedByHost);
                    throw new OccupiedDatesException(guestRequest.EntryDate.Day + "." + guestRequest.EntryDate.Month + " - " + guestRequest.ReleaseDate.Day + "." + guestRequest.ReleaseDate.Month);
                }

                //close all the orders to this guestRequest
                UpdateGuestRequestStatus(ord.GuestRequestKey, Enums.RequestStatus.ClosedWithDeal);
                var linkedOrder = from order in GetAllOrders()
                                  where order.GuestRequestKey == ord.GuestRequestKey
                                  select order;
                linkedOrder.AsParallel().ForAll((x => UpdateOrderStatus(x.OrderKey, Enums.OrderStatus.ClosedByHost)));

                //calculate the Commission
                hostingUnit.Commission += GetNumberOfDateInRange(guestRequest.EntryDate, guestRequest.ReleaseDate) * DAL_Adapter.GetDAL().GetCommission();
                UpdateHostingUnit(hostingUnit, hostingUnit.HostingUnitKey);
            }


        }

        #endregion

        #region GetOrder This function return Order

        /// <summary>
        /// This function return Order according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the Order</param>
        /// <returns>The Order with the <paramref name="key"/></returns>
        public Order GetOrder(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetOrder(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
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
            try
            {
                return DAL_Adapter.GetDAL().GetAllBankAccounts();
            }
            catch (NoItemsException e)
            {
                throw new NoItemsException(e.Message);
            }
        }

        #endregion

        #region GetBankBranch This function return BankAccount

        /// <summary>
        /// This function return BankBranch according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the BankBranch</param>
        public BankBranch GetBankBranch(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetBankBranch(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        #endregion

        #region Host These function perform actions on Host

        #region GetAllHosts This function return all the Hosts

        /// <summary>
        /// This function return all the Host 
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no Host</exception>
        /// <returns><seealso cref="IEnumerable{Host}"/> to go over the list of all the Hosts</returns>
        public IEnumerable<Host> GetAllHosts()
        {
            try
            {
                return DAL_Adapter.GetDAL().GetAllHosts();
            }
            catch (NoItemsException e)
            {
                throw new NoItemsException(e.Message);
            }
        }

        #endregion

        #region GetHost This function return host

        /// <summary>
        /// This function return the Host with the <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The requested <see cref="Host"/>'s KEY</param>
        /// <returns>The Host with the  <paramref name="key"/></returns>
        public Host GetHost(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetHost(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        #endregion

        public bool CheckIfHostExists(string username)
        {
            return DAL_Adapter.GetDAL().CheckIfHostExists(username);
        }
        #endregion

        #region Guest

        public IEnumerable<Guest> GetAllGuests()
        {
            return DAL_Adapter.GetDAL().GetAllGuests();
        }

        public Guest GetGuest(int key)
        {
            return DAL_Adapter.GetDAL().GetGuest(key);
        }

        public bool CheckIfGuestExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfGuestExists(key);
        }

        public bool CheckIfGuestExists(string username)
        {
            return DAL_Adapter.GetDAL().CheckIfGuestExists(username);
        }


        #endregion

        #region IfExists These function check if object exsits in the data

        #region CheckIfGuestRequestExists This function check if guestRequest exists in the data

        /// <summary>
        /// This function return if guestRequest exists in the data
        /// </summary>
        /// <param name="key">The key of the guestRequest</param>
        /// <returns>boolean, if the guestRequest exists or not</returns>
        public bool CheckIfGuestRequestExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfGuestRequestExists(key);
        }

        #endregion

        #region CheckIfHostingUnitExists This function check if hostingUnit exists in the data

        /// <summary>
        /// This function return if hostingUnit exists in the data
        /// </summary>
        /// <param name="key">The key of the hostingUnit</param>
        /// <returns>boolean, if the hostingUnit exists or not</returns>
        public bool CheckIfHostingUnitExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfHostingUnitExists(key);
        }

        #endregion

        #region CheckIfOrderExists This function check if order exists in the data

        /// <summary>
        /// This function return if order exists in the data
        /// </summary>
        /// <param name="key">The key of the order</param>
        /// <returns>boolean, if the order exists or not</returns>
        public bool CheckIfOrderExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfOrderExists(key);
        }

        #endregion

        #region CheckIfBankAccountExists This function check if bankAccount exists in the data

        /// <summary>
        /// This function return if bankAccount exists in the data
        /// </summary>
        /// <param name="key">The key of the bankAccount</param>
        /// <returns>boolean, if the bankAccount exists or not</returns>
        public bool CheckIfBankAccountExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfBankAccountExists(key);
        }

        #endregion

        #region CheckIfHostExists This function check if host exists in the data

        /// <summary>
        /// This function return if host exists in the data
        /// </summary>
        /// <param name="key">The key of the host</param>
        /// <returns>boolean, if the host exists or not</returns>
        public bool CheckIfHostExists(int key)
        {
            return DAL_Adapter.GetDAL().CheckIfHostExists(key);
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
            return (DateTime.Now - startDay).Days;
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
            return (endDay - startDay).Days;
        }

        #endregion

        #region IsLeastThenOneDay This function check if date is at least one day before the second date

        /// <summary>
        /// This function check if <paramref name="date1"/> is at least one day before the <paramref name="date2"/>
        /// </summary>
        /// <param name="date1">First date</param>
        /// <param name="date2">Second date</param>
        /// <returns>Boolean, if date is at least one day before the second date</returns>
        public bool IsLeastThenOneDay(DateTime date1, DateTime date2)
        {
            if (date1.AddDays(1) > date2)
                return false;
            return true;
        }

        #endregion

        #endregion

        #region Function help to work with GuestRequest

        #region GetAllGuestRequestToGuest This function return all the GuestRequest send from user

        //TODO:check!
        /// <summary>
        /// This function return all the guest Request asociation to specific guest
        /// </summary>
        /// <param name="key">The guest key to check</param>
        /// <returns><see cref="IEnumerable{GuestRequest}"/>All guest request the specific guest have</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequestToGuest(int key)
        {
            var GRkeys = GetGuest(key).GuestRequests;
            List<GuestRequest> list = new List<GuestRequest>();
            foreach (var GRkey in GRkeys)
            {
                list.Add(GetGuestRequest(GRkey));
            }

            return list;
        }

        #endregion

        #region GetAllGuestRequestWhere This function return all the GuestRequest that match condition

        /// <summary>
        /// The function return all the GuestRequest That matches certain conditions (defined by <see cref="isMeetTheDefinition"/>)
        /// </summary>
        /// <param name="func">Function(defined by <see cref="isMeetTheDefinition"/>)</param>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of all the GuestRequest That match the condition</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequestWhere(isMeetTheDefinition func)
        {
            var selectedList = from gr in GetAllGuestRequests()
                               where func(gr)
                               select gr;
            return selectedList;
        }

        #endregion

        #region GetAmountOfOrderToGuest This function return the amount of order sent to GuestRequst

        /// <summary>
        /// The function return all the <see cref="Order"/> sent to <paramref name="guestRequest"/>
        /// </summary>
        /// <param name="key">The Guest Request key to check how many <see cref="Order"/> where sent to her </param>
        /// <exception cref="KeyNotFoundException">Thrown when there isnt GuestRequst in data that exsist the <paramref name="key"/></exception>
        /// <returns>The amount of order sent to the GuestRequest</returns>
        public int GetAmountOfOrderToGuest(int key)
        {
            if (!CheckIfGuestRequestExists(key))
                throw new KeyNotFoundException("There is no order with the key specified");
            var allOrderToGuest = from ord in GetAllOrders()
                                  where ord.GuestRequestKey == key
                                  select ord;
            return allOrderToGuest.Count();
        }

        #endregion

        #endregion

        #region GetAllAvailableHostingUnit This function return all the Available Hosting unit in range

        /// <summary>
        /// The function returns the list of all available hosting units starting in the <paramref name="date"/> and ending <paramref name="days"/> days later
        /// </summary>
        /// <param name="entryDate">Start date</param>
        /// <param name="days">How many days</param>
        /// <returns><see cref="IEnumerable{HostingUnit}"/> to go over the list of all free hosting unit in the range</returns>
        public IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime entryDate, int days)
        {
            DateTime releaseDate = entryDate.AddDays(days);
            var list = from hostingUnit in GetAllHostingUnits()
                       where CheckIfAvailable(hostingUnit.Diary, entryDate, releaseDate)
                       select hostingUnit;
            return list;
        }

        #endregion

        #region GetAllOrderInRange This function return all the order where sent in some days

        /// <summary>
        /// The function return all the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </summary>
        /// <param name="numberOfDays">The amount of day to check</param>
        /// <returns>
        /// All the <see cref="Order"/>s that the amount of day from there creation\since they sent email to the client 
        /// is greater or equal to <paramref name="numberOfDays"/>
        /// </returns>
        public IEnumerable<Order> GetAllOrderInRange(int numberOfDays)
        {
            var list = from ord in GetAllOrders()
                       where ((DateTime.Now - ord.CreateDate).TotalDays >= numberOfDays || (DateTime.Now - ord.OrderDate).TotalDays >= numberOfDays)
                       select ord;
            return list;
        }

        #endregion

        #region GetAllsuccessfulOrder This function return the amount of successful order sent to the HostingUnit

        /// <summary>
        /// The function returns the number of orders sent\the number
        /// of successfully closed orders for <paramref name="hostingUnit"/>
        /// </summary>
        /// <param name="key">The hosting unit <paramref name="key"/> to check</param>
        /// <returns>The number of orders sent\the number  of successfully closed orders for <paramref name="hostingUnit"/>
        /// </returns>
        public int GetAllsuccessfulOrder(int key)
        {
            var list = from ord in GetAllOrders()
                       where (ord.HostingUnitKey == key && (ord.Status == Enums.OrderStatus.ClosedByCustomerResponsiveness || ord.Status == Enums.OrderStatus.SentMail))
                       select ord;
            return list.Count();
        }

        #endregion

        #region GetMatchingGuestRequests This function gets a list of the GuestRequests whose requirement are fulfilled by the HostingUnit

        /// <summary>
        /// This function return all the GuestRequest match the <paramref name="hu"/>
        /// </summary>
        /// <param name="hu">The hostingUnit to return all the guestRequest match him</param>
        /// <exception cref="NoItemsException">Thrown when there isnt any guestRequest in the data</exception>
        /// <returns><see cref="List{GuestRequest}"/> of all the GuestRequest match <paramref name="hu"/></returns>
        public List<GuestRequest> GetMatchingGuestRequests(HostingUnit hu)
        {
            try
            {
                var linq = from i in DAL_Adapter.GetDAL().GetAllGuestRequests()
                           where i.Adults <= hu.NumberOfPlacesForAdults &&
                           i.Children <= hu.NumberOfPlacesForChildren &&
                           CheckIfAvailable(hu.Diary, i.EntryDate, i.ReleaseDate) &&
                           IsRelevant(i.ChildrensAttractions, hu.IsThereChildrensAttractions) &&
                           IsRelevant(i.Garden, hu.IsThereGarden) &&
                           IsRelevant(i.Jacuzzi, hu.IsThereJacuzzi) &&
                           IsRelevant(i.Pool, hu.IsTherePool) &&
                           IsRelevant(i.Area, hu.Area) &&
                           IsRelevant(i.Type, hu.Type) &&
                           i.Status == Enums.RequestStatus.Open

                           select i;

                List<GuestRequest> ret = new List<GuestRequest>();
                foreach (var i in linq)
                {
                    ret.Add(i);
                }

                return ret;
            }
            catch (NoItemsException)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// This function return all the HostingUnit match the <paramref name="gr"/> and belong to <paramref name="host"/>
        /// </summary>
        /// <param name="gr">The guestRequest to return all the guestRequest match him</param>
        ///<param name="host">The Host that all the hostingUnit in the result must belong to him</param>
        /// <exception cref="NoItemsException">Thrown when there are no HostingUnit in the list</exception>
        /// <returns><see cref="List{Hos}"/> of all the HostingUnit match <paramref name="gr"/> and belong to<paramref name="host"/></returns>
        public List<HostingUnit> GetMatchingHostingUnits(GuestRequest gr, Host host)
        {
            var linq = from i in DAL_Adapter.GetDAL().GetAllHostingUnits()
                       where gr.Adults <= i.NumberOfPlacesForAdults &&
                       gr.Children <= i.NumberOfPlacesForChildren &&
                       CheckIfAvailable(i.Diary, gr.EntryDate, gr.ReleaseDate) &&
                       IsRelevant(gr.ChildrensAttractions, i.IsThereChildrensAttractions) &&
                       IsRelevant(gr.Garden, i.IsThereGarden) &&
                       IsRelevant(gr.Jacuzzi, i.IsThereJacuzzi) &&
                       IsRelevant(gr.Pool, i.IsTherePool) &&
                       IsRelevant(gr.Area, i.Area) &&
                       IsRelevant(gr.Type, i.Type) &&
                       gr.Status == Enums.RequestStatus.Open &&
                       i.Owner.HostKey == host.HostKey

                       select i;

            List<HostingUnit> ret = new List<HostingUnit>();
            foreach (var i in linq)
            {
                ret.Add(i);
            }

            return ret;
        }


        #region Function to work with Diary array

        #region CheckIfAvailable This function check if the diary available in the range

        /// <summary>
        /// The function return if the range between <paramref name="entryDate"/> to <paramref name="ReleaseDate"/> is Available
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the vacation start date is not at least one day before the vacation end date</exception>
        /// <exception cref="FormatException">Thrown when the Format of the <paramref name="diary"/> isnt good</exception>
        /// <param name="diary">The Array of all the date in the year</param>
        /// <param name="entryDate">Entry date of the range</param>
        /// <param name="ReleaseDate">Release date of the range</param>
        /// <returns>Boolean, if the range is available</returns>
        public bool CheckIfAvailable(bool[,] diary, DateTime entryDate, DateTime ReleaseDate)
        {
            if (!IsLeastThenOneDay(entryDate, ReleaseDate))
                throw new ArgumentException("The release date must be at least one day after the entry date", "ReleaseDate");
            try
            {
                DateTime end = new DateTime(2020, ReleaseDate.Month, ReleaseDate.Day);

                for (DateTime dt = new DateTime(2020, entryDate.Month, entryDate.Day); dt < end; dt = dt.AddDays(1))
                {
                    //We assume that the array is defined in this way: bool[12,31]
                    if (diary[dt.Month - 1, dt.Day - 1])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                throw new FormatException("Diary must be 12*31 array");
            }
        }

        #endregion

        #region MarkingInTheDiary This function mark a vaction in the diary

        /// <summary>
        /// This function mark a vaction(<paramref name="enteryDate"/> - <paramref name="releaseDate"/>) in <paramref name="diary"/>
        /// </summary>
        /// <param name="hu">The Hosting unit whose array represent all the days in the year</param>
        /// <param name="enteryDate">Start date of the vaction</param>
        /// <param name="releaseDate">End date of the vaction</param>
        /// <remarks>This function assume that the range is available and dosnt check it, to check use the<see cref="CheckIfAvailable(bool[,], DateTime, DateTime)"/> function</remarks>
        public bool[,] MarkingInTheDiary(HostingUnit hu, DateTime enteryDate, DateTime releaseDate)
        {
            DateTime endDt = new DateTime(2020, releaseDate.Month, releaseDate.Day);
            for (DateTime dt = new DateTime(2020, enteryDate.Month, enteryDate.Day); dt < endDt; dt = dt.AddDays(1))
            {
                hu.Diary[dt.Month - 1, dt.Day - 1] = true;
            }
            return hu.Diary;
        }

        #endregion

        #endregion

        #region Function to work with status

        #region IsClosed This function return if Order is closed

        /// <summary>
        /// This function return if Order is closed 
        /// </summary>
        /// <param name="ord">The order to check is status</param>
        /// <returns>boolean, if the status is closed or not</returns>
        public bool IsClosed(Enums.OrderStatus stat)
        {
            if (stat == Enums.OrderStatus.ClosedByCustomerResponsiveness || stat == Enums.OrderStatus.ClosedByCustomerUnresponsiveness || stat == Enums.OrderStatus.ClosedByHost)
                return true;
            return false;
        }

        #endregion

        #region IsClosed This function return if GuestRequest is closed

        /// <summary>
        /// This function return if guestRequest is closed 
        /// </summary>
        /// <param name="ord">The guestRequest to check is status</param>
        /// <returns>boolean, if the status is closed or not</returns>
        public bool IsClosed(Enums.RequestStatus stat)
        {
            if (stat == Enums.RequestStatus.ClosedWithDeal || stat == Enums.RequestStatus.CloseWithExpired)
                return true;
            return false;
        }

        #endregion

        #endregion


        #endregion

        #region Grouping functions

        #region GetAllGuestByArea This function return all the GuestRequest group by Area

        /// <summary>
        /// The function return all the GuestRequest group by <see cref="Enums.Area"/> 
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no GuestRequest in the list</exception>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> GetAllGuestByArea()
        {
            try
            {
                var allGuestRequest = GetAllGuestRequests();
                var groupedList = from gr in allGuestRequest
                                  group gr by gr.Area into t
                                  select t;
                return groupedList;
            }
            catch (NoItemsException)
            {
                throw;
            }
        }

        #endregion

        #region GetAllGuestByNumerOfVacationers This function return all the GuestRequest group by number of Vacationers

        /// <summary>
        /// The function return all the GuestRequest group by number of Vacationers
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by number of Vacationers</returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestByNumerOfVacationers()
        {
            try
            {
                var allGuestRequest = GetAllGuestRequests();
                var groupedList = from gr in allGuestRequest
                                  group gr by (gr.Children + gr.Adults);

                return groupedList;
            }
            catch (NoItemsException)
            {
                throw;
            }
        }

        #endregion

        #region GetAllHostByNumberOfHostingUnits This function return all the Host group by number of Hosting unit

        /// <summary>
        /// The function return all the Host group by the number of Hosting unit they have
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all Host group by the number of Hosting unit they have</returns>
        public IEnumerable<IGrouping<int, Host>> GetAllHostByNumberOfHostingUnits()
        {
            var listOfAllHostingUnitGroupByHost = getHostingUnitByHost();
            var groupedList = from item in listOfAllHostingUnitGroupByHost
                              group item.Key by item.Count();
            return groupedList;
        }

        #endregion

        #region GetHostingUnitByArea This function return all the HostingUnit group by Area

        /// <summary>
        /// The function return all the Hosting unit group by area
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all the Hosting unit group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea()
        {
            var allHostingUnit = GetAllHostingUnits();
            var groupedList = from hu in allHostingUnit
                              group hu by hu.Area;
            return groupedList;
        }

        #endregion

        #region getHostingUnitByHost This function return all the HostingUnit group by There Host

        /// <summary>
        ///  This function return all the HostingUnit group by There Host
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all HostingUnit group by there host</returns>
        public IEnumerable<IGrouping<Host, HostingUnit>> getHostingUnitByHost()
        {
            var allHostingUnit = GetAllHostingUnits();
            var groupedList = from hu in allHostingUnit
                              group hu by hu.Owner;
            return groupedList;
        }

        #endregion

        #endregion

        #region Just a Few Help Functions

        /// <summary>
        /// The function check if the Hosting unit property is relevant, matchig the guestRequest
        /// </summary>
        /// <param name="desired">The request</param>
        /// <param name="has">If the HostingUnit have this</param>
        /// <returns>Boolean, if the HostingUnit relevant to the GuestRequest</returns>
        /// <remarks>
        ///           has  |    false  |  true  |   
        ///   IsInterested\|           |        |
        ///  --------------|-----------|--------|
        ///   Necessary    |    false  |  true  |
        ///  --------------|-----------|--------|
        ///   Possible     |    true   |  true  |
        ///  --------------|-----------|--------|
        ///   Uninterested:|    true   |  false |
        ///  --------------|-----------|--------|
        /// </remarks>
        private bool IsRelevant(Enums.IsInterested desired, bool has)
        {

            switch (desired)
            {
                case Enums.IsInterested.Necessary:
                    return has == true;
                case Enums.IsInterested.Possible:
                    return true;
                default:
                    return has == false;
            }
        }

        /// <summary>
        /// The function check if the Hosting unit "Area" property is relevant, matchig the guestRequest
        /// </summary>
        /// <param name="desired">The HostingUnit area in </param>
        /// <param name="area">The area in the request</param>
        /// <returns>Boolean, if the HostingUnit relevant to the GuestRequest</returns>
        private bool IsRelevant(Enums.Area desired, Enums.Area area)
        {
            if (area == Enums.Area.All)
            {
                return true;
            }

            return desired == area;
        }

        /// <summary>
        /// The function return if the <see cref="Enums.HostingUnitType"/> in the HostingUnit match the request
        /// </summary>
        /// <param name="desired">The Hosting unit type</param>
        /// <param name="type">The request type</param>
        /// <returns></returns>
        private bool IsRelevant(Enums.HostingUnitType desired, Enums.HostingUnitType type)
        {
            if (type == Enums.HostingUnitType.All)
            {
                return true;
            }

            return desired == type;
        }

        //TODO: check this
        public string GetGuestUsername(int key)
        {
            try
            {
                return DAL_Adapter.GetDAL().GetGuestUserName(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void WriteGuestToFile(string username, string password, int key)
        {
            try
            {
                DAL_Adapter.GetDAL().WriteGuestToFile(username, password, key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddGuest(Guest guest)
        {
            DAL_Adapter.GetDAL().AddGuest(guest);
        }

        public void WriteHostToFile(string username, string password, int hostKey)
        {
            DAL_Adapter.GetDAL().WriteHostToFile(username, password, hostKey);
        }

        public void AddHost(Host host)
        {
            DAL_Adapter.GetDAL().AddHost(host);
        }

        public int GetGuestKey(string userName)
        {
            return DAL_Adapter.GetDAL().GetGuestKey(userName);
        }

        public bool HostCompareToPasswordInFile(string username, string password)
        {
            return DAL_Adapter.GetDAL().HostCompareToPasswordInFile(username, password);
        }

        public int GetHostKey(string userName)
        {
            return DAL_Adapter.GetDAL().GetHostKey(userName);
        }

        public bool GuestCompareToPasswordInFile(string username, string password)
        {
            return DAL_Adapter.GetDAL().GuestCompareToPasswordInFile(username, password);
        }

        public bool AdminCompareToPasswordInFile(string username, string password)
        {
            return DAL_Adapter.GetDAL().AdminCompareToPasswordInFile(username, password);
        }

        public void RemoveGuest(string username)
        {
            DAL_Adapter.GetDAL().RemoveGuest(GetGuestKey(username));
        }

        public int GetHostKey()
        {
            return DAL_Adapter.GetDAL().GetHostKey();
        }

        public void RemoveHost(int key)
        {
            DAL_Adapter.GetDAL().RemoveHost(key);
        }

        public void SetCommission(float? value)
        {
            if (value != null)
            {
                DAL_Adapter.GetDAL().SetCommission(value);
            }
        }

        public void SubmitHostComment(string comment)
        {
            DAL_Adapter.GetDAL().SubmitHostComment(comment);
        }

        public List<string> GetAllGuestComments()
        {
            return DAL_Adapter.GetDAL().GetAllGuestComments();
        }

        public List<string> GetAllHostComments()
        {
            return DAL_Adapter.GetDAL().GetAllHostComments();
        }

        #endregion

    }
}



