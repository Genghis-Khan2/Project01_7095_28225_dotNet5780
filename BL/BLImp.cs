using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DAL;

namespace BL
{
    class BLImp : IBL
    {

        public delegate bool isMeetTheDefinition(HostingUnit hostingUnit);
        private BLImp() { }
        //TODO: check all the Exeption DAL level throw and check they treated
        protected static BLImp instance = null;

        /// <summary>
        /// This function return the BLImp Object according to singelton
        /// </summary>
        /// <returns>The BLImp Object</returns>
        public IBL getBL()
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

        public IEnumerable<BankAccount> GetAllBankAccounts()
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

        public IEnumerable<GuestRequest> getAllGuestRequestWhere(isMeetTheDefinition func)
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

        public IEnumerable<HostingUnits> getAllGuestByArea(Configuration.Area area)
        {
            //TODO: write the function
            throw new NotImplementedException();
        }
        /*
         

 פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז שנוצרו /
מאז שנשלח מייל ללקוח גדול או שווה למספר הימים שהפונקציה קיבלה. 

הגדר פונקציות המחזירות את הקבוצות הבאות )ע"י שימוש ב-Grouping )
 רשימת לקוחות מקובצת )Grouping )ע"פ אזור הנופש הנדרש.
 רשימת לקוחות מקובצת )Grouping )ע"פ מספר הנופשים.
 רשימת מארחים מקובצת )Grouping )ע"פ מספר יחידות האירוח שהם מחזיקים
 רשימת יחידות אירוח מקובצת )Grouping )ע"פ אזור הנופש הנדרש.
         */
    }
}
