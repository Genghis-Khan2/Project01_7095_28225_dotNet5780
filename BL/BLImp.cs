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
        //TODO: orgenize using #regine
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
        
        /// <summary>
        /// This function add GuestRequest to the data
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="gr">The GuestRequst to add</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            //TODO:do it
            throw new NotImplementedException();
        }

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
        
        /// <summary>
        /// Add Order to the data
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <exception cref="InfoNotExists">Thrown when the GuestRequest or HostingUnit of the Order does not exist</exception>
        /// <param name="ord">Order to add</param>
        public void AddOrder(Order ord)
        {
            //TODO: write the function
            //TODO: dont need exception when HostingUnit and GuestRequest isnt exist?
            //יש לוודא בעת יצירת הזמנה ללקוח, שהתאריכים המבוקשים פנויים ביחידת האירוח שמוצעת לו.
            throw new NotImplementedException();
        }

        /// <summary>
        /// The function returns the list of all existing bank branches in Israel
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns><see cref="IEnumerable{BankBranch}"/> to go over the list of bank accounts</returns>
        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            return DalImp.GetDal().GetAllBankAccounts();
        }

        /// <summary>
        /// This function return all the GuestRequest in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no guest requests</exception>
        /// <returns><see cref="IEnumerable{GuestRequest}"/> to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return DalImp.GetDal().GetAllGuestRequests();
        }

        /// <summary>
        /// The Function return all the Hosting unit
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no items in the hosting units list</exception>
        /// <returns><see cref="IEnumerable{HostingUnit}"/> to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return DalImp.GetDal().GetAllHostingUnits();
        }

        /// <summary>
        /// This function return all the Order
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no orders in the list</exception>
        /// <returns><see cref="IEnumerable{Order}"/> to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return DalImp.GetDal().GetAllOrders();
        }

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit in the data match the <paramref name="hostingUnit"/></exception>
        ///<exception cref="DeleteWhileLinked">Thrown if there is any open <see cref="Order"/> linked to <paramref name="hostingUnit"/> </exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            //TODO: write the function
            //• לא ניתן למחוק יחידת אירוח כל עוד יש הצעה הקשורה אליה במצב פתוח.
            throw new NotImplementedException();
        }

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

        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateOrder(int key, Enums.OrderStatus stat)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public int getNumberOfDateInRange(DateTime startDay)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public int getNumberOfDateInRange(DateTime startDay, DateTime endDay)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public IEnumerable<Order> getAllOrderInRange(int numberOfDays)
        {
            //TODO: do it
            throw new NotImplementedException();
        }
        public IEnumerable<GuestRequest> getAllGuestRequestWhere(HostingUnit.isMeetTheDefinition func)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public int getAmountOfOrderToGuest(GuestRequest guestRequest)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public int getAllsuccessfulOrder(HostingUnit hostingUnit)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }
        //TODO:work on it
        public IEnumerable<IGrouping<Enums.Area, GuestRequest>> getAllGuestByArea()//is it a good prototype?
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public IEnumerable<IGrouping<int, GuestRequest>> getAllGuestByNumerOfVacationers(Enums.Area area)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }//TODO:need to check if did all the grouping function
        public IEnumerable<IGrouping<int, Host>> getAllHostByNumberOfHostingUnits()
        {
            //TODO: do it
            throw new NotImplementedException();
        }
        public IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea(Enums.Area area)
        {
            //TODO: do it
            throw new NotImplementedException();
        }


    }
}

/*
tasks:
2. write all the definition in IBL
3. Write comments to all the function
4. write the function
     */
