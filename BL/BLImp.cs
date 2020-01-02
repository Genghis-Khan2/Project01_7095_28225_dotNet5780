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
    /// <remarks>
    /// For represent dates we use the <see cref="DateTime"/> class But because we are always talking about the range
    /// of that year (<see cref="HostingUnit.Diary"/> represents one year) we do not refer to the "year" part of the object, 
    /// so for convenience in calculations we always place it to 2020 
    /// </remarks>
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
            if (!IsLeastThenOneDay(gr.EntryDate, gr.ReleaseDate))
            {
                throw new ArgumentException("The release date must be at least one day after the entry date", "ReleaseDate");
            }
            DalImp.GetDal().AddGuestRequest(gr);
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
            return DalImp.GetDal().GetAllGuestRequests();
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
            GuestRequest gr = DalImp.GetDal().GetGuestRequest(key);
            if (IsClosed(gr.Status) && !IsClosed(stat))
                throw new AlreadyClosedException("GuestRequest", gr.GuestRequestKey);//TODO: write all the AlreadyClosedException if's in this form
            DalImp.GetDal().UpdateGuestRequestStatus(key, stat);
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
            return DalImp.GetDal().GetGuestRequest(key);
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
        ///<exception cref="ChangedWhileLinkedException">Thrown if there is any open <see cref="Order"/> linked to the hosting unit with the <paramref name="key"/> and you try to delete it</exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            //we assume that An Order considered "open" if  its status is "Enums.OrderStatus.UnTreated" and also "Enums.OrderStatus.SentMail"
            //REMARK לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.
            var linkedOpenOrderList = from order in DalImp.GetDal().GetAllOrders()
                                      where (order.HostingUnitKey == key && (order.Status == Enums.OrderStatus.UnTreated || order.Status == Enums.OrderStatus.SentMail))
                                      select order;
            if (linkedOpenOrderList.Count() != 0)
                throw new ChangedWhileLinkedException("delete", "HostingUnit", key, "Order", linkedOpenOrderList.First().OrderKey);
            DalImp.GetDal().RemoveHostingUnit(key);
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
            var hostBeforeUpdating = DalImp.GetDal().GetHost(key);
            if (!hostingUnit.Owner.CollectionClearance && hostBeforeUpdating.CollectionClearance)//If you try to cancel the account cancellation permission
            {
                var linkedOpenOrderList = from order in DalImp.GetDal().GetAllOrders()
                                          where (order.HostingUnitKey == key && (order.Status == Enums.OrderStatus.UnTreated || order.Status == Enums.OrderStatus.SentMail))
                                          select order;
                if (linkedOpenOrderList.Count() != 0)
                    throw new ChangedWhileLinkedException("change CollectionClearance of", "HostingUnit", key, "Order", linkedOpenOrderList.First().OrderKey);

            }
            DalImp.GetDal().UpdateHostingUnit(hostingUnit, key);
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
            return DalImp.GetDal().GetHostingUnit(key);
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
            if (!CheckIfAvailable(DalImp.GetDal().GetHostingUnit(ord.HostingUnitKey).Diary, DalImp.GetDal().GetGuestRequest(ord.GuestRequestKey).EntryDate, DalImp.GetDal().GetGuestRequest(ord.GuestRequestKey).ReleaseDate))
                throw new OccupiedDatesException();

            DalImp.GetDal().AddOrder(ord);
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
            //TODO: write the function
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
                DalImp.GetDal().UpdateOrderStatus(key, Enums.OrderStatus.SentMail);
            }

            //TODO: finish the get Commission here
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
                    hostingUnit.Diary = MarkingInTheDiary(hostingUnit.Diary, guestRequest.EntryDate, guestRequest.ReleaseDate);
                    UpdateHostingUnit(hostingUnit, ord.HostingUnitKey);
                }
                else
                {
                    DalImp.GetDal().UpdateOrderStatus(key, Enums.OrderStatus.ClosedByHost);
                    throw new OccupiedDatesException(guestRequest.EntryDate.Day + "." + guestRequest.EntryDate.Month + " - " + guestRequest.ReleaseDate.Day + "." + guestRequest.ReleaseDate.Month);
                }

                UpdateGuestRequestStatus(ord.GuestRequestKey, Enums.RequestStatus.ClosedWithDeal);
                var linkedOrder = from order in GetAllOrders()
                                  where order.GuestRequestKey == ord.GuestRequestKey
                                  select order;
                linkedOrder.AsParallel().ForAll((x => UpdateOrderStatus(x.OrderKey, Enums.OrderStatus.ClosedByHost)));
            }
            if (stat == Enums.OrderStatus.CustomerResponsiveness)//TODO: finish the get Commission here

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
            return DalImp.GetDal().GetOrder(key);
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

        #region Host These function perform actions on Host

        #region GetAllHosts This function return all the Hosts

        /// <summary>
        /// This function return all the Host 
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no Host</exception>
        /// <returns><seealso cref="IEnumerable{Host}"/> to go over the list of all the Hosts</returns>
        public IEnumerable<Host> GetAllHosts()
        {
            return DalImp.GetDal().GetAllHosts();
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
            return DalImp.GetDal().GetHost(key);
        }

        #endregion

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
            return DalImp.GetDal().CheckIfGuestRequestExists(key);
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
            return DalImp.GetDal().CheckIfHostingUnitExists(key);
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
            return DalImp.GetDal().CheckIfOrderExists(key);
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
            return DalImp.GetDal().CheckIfBankAccountExists(key);
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
            return DalImp.GetDal().CheckIfHostExists(key);
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

        //TODO: CR GK: Should this really be public?

        /// <summary>
        /// This function check if <paramref name="date1"/> is at least one day before the <paramref name="date2"/>
        /// </summary>
        /// <param name="date1">First date</param>
        /// <param name="date2">Second date</param>
        /// <returns>Boolean, if date is at least one day before the second date</returns>
        public bool IsLeastThenOneDay(DateTime date1, DateTime date2)
        {
            DateTime dt1 = new DateTime(2020, date1.Month, date1.Day);
            DateTime dt2 = new DateTime(2020, date2.Month, date2.Day);
            if (date1.AddDays(1) > date2)
                return false;
            return true;
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

        #region GetAllAvailableHostingUnit This function return all the Available Hosting unit int range

        /// <summary>
        /// The function returns the list of all available hosting units starting in the <paramref name="date"/> and ending <paramref name="days"/> days later
        /// </summary>
        /// <param name="date">Start date</param>
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
        /// <param name="diary">The array represent all the days in the year</param>
        /// <param name="enteryDate">Start date of the vaction</param>
        /// <param name="releaseDate">End date of the vaction</param>
        /// <remarks>This function assume that the range is available and dosnt check it, to check use the<see cref="CheckIfAvailable(bool[,], DateTime, DateTime)"/> function</remarks>
        public bool[,] MarkingInTheDiary(bool[,] diary, DateTime enteryDate, DateTime releaseDate)
        {
            DateTime endDt = new DateTime(2020, releaseDate.Month, releaseDate.Day);
            for (DateTime dt = new DateTime(2020, enteryDate.Month, enteryDate.Day); dt < endDt; dt.AddDays(1))
            {
                diary[dt.Month, dt.Day] = true;
            }
            return diary;
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
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by area</returns>
        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> GetAllGuestByArea()
        {
            var allGuestRequest = GetAllGuestRequests();
            var groupedList = from gr in allGuestRequest
                              group gr by gr.Area into t
                              select t;
            return groupedList;
        }

        #endregion

        #region GetAllGuestByNumerOfVacationers This function return all the GuestRequest group by number of Vacationers

        /// <summary>
        /// The function return all the GuestRequest group by number of Vacationers
        /// </summary>
        /// <returns><see cref="IEnumerable{IGrouping}"/> to go over the list of all guestRequest group by number of Vacationers</returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestByNumerOfVacationers()
        {
            var allGuestRequest = GetAllGuestRequests();
            var groupedList = from gr in allGuestRequest
                              group gr by (gr.Children + gr.Adults);//TODO: check if it work

            return groupedList;
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

    }
}

/*
tasks:
7. check that all the function in BLImp also in IBL
6. לכתוב את הפונקציות
7.לוודא שכל התנאים מומשו(כל הלינקיו וכו')
8. לבדוק את הפונקציות
    TODO: do auto refactor to all code
     * */


