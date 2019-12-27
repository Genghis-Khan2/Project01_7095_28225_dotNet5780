using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;
using System.Linq;

namespace BL
{
    public interface IBL
    {

        //TODO: add all the function prototype in BLImp to here
        //Functions for working with customer requirement
        void AddGuestRequest(GuestRequest gr);
        void AddHostingUnit(HostingUnit hu);

        void UpdateGuestRequest(GuestRequest gr);

        //Functions for working with hosting units

        void RemoveHostingUnit(HostingUnit hu);

        void UpdateHostingUnit(HostingUnit hu);

        void AddOrder(Order ord);

        void UpdateOrder(Order ord);

        IEnumerable<HostingUnit> GetAllHostingUnits();

        IEnumerable<GuestRequest> GetAllGuestRequests();

        IEnumerable<Order> GetAllOrders();

        IEnumerable<BankBranch> GetAllBankAccounts();

        IEnumerable<HostingUnit> GetAllAvailableHostingUnit(DateTime date, int days);


        int getNumberOfDateInRange(DateTime startDay);

        int getNumberOfDateInRange(DateTime startDay, DateTime endDay);

        IEnumerable<Order> getAllOrderInRange(int numberOfDays);
        IEnumerable<GuestRequest> getAllGuestRequestWhere(HostingUnit.isMeetTheDefinition func);

        int getAmountOfOrderToGuest(GuestRequest guestRequest);

        int getAllsuccessfulOrder(HostingUnit hostingUnit);
      
        //TODO:work on it
        IEnumerable<IGrouping<Enums.Area, GuestRequest>> getAllGuestByArea();
        IEnumerable<IGrouping<int, GuestRequest>> getAllGuestByNumerOfVacationers(Enums.Area area);//TODO:need to check if did all the grouping function
        IEnumerable<IGrouping<int, Host>> getAllHostByNumberOfHostingUnits();
        IEnumerable<IGrouping<Enums.Area, HostingUnit>> GetHostingUnitByArea(Enums.Area area);

    }
}
