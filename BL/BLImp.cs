using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using DAL;
using System.Linq;

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
        static  public IBL getBL()
        {
            if (instance == null)
            {
                instance = new BLImp();
                return instance;
            }
            return instance;
        }

        public void AddGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void AddOrder(Order ord)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            return DalImp.GetDal().GetAllBankAccounts();
        }

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return DalImp.GetDal().GetAllGuestRequests();
        }

        /// <summary>
        /// The Function return all the Hosting unit
        /// </summary>
        /// <returns>IEnumerator to </returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return DalImp.GetDal().GetAllHostingUnits();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return DalImp.GetDal().GetAllOrders();
        }

        public void RemoveHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest gr)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order ord)
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

        public void UpdateGuestRequest(int key, Enums.RequestStatus stat)
        {
            //TODO:do it
            throw new NotImplementedException();
        }

        public void RemoveHostingUnit(int key)
        {
            //TODO:do it
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            //TODO:do it
            throw new NotImplementedException();
        }

        public void UpdateOrder(int key, Enums.OrderStatus stat)
        {
            //TODO:do it
            throw new NotImplementedException();
        }
    }
}

/*
tasks:
1. finish to write all the definition
2. write all the definition in IBL
3. Write comments to all the function
4. write the function
     */
