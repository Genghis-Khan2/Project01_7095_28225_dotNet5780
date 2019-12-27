using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;

namespace BL
{
    public interface IBL
    {
        //TODO: add all the function prototype in BLImp to here
        //Functions for working with customer requirement
        #region Functions also found in IDAL that are used to work with data
        void AddGuestRequest(GuestRequest gr);

        void UpdateGuestRequest(GuestRequest gr);

        //Functions for working with hosting units
        void AddHostingUnit(HostingUnit hu);

        void RemoveHostingUnit(HostingUnit hu);

        void UpdateHostingUnit(HostingUnit hu);

        //Functions for working with Orders
        void AddOrder(Order ord);

        void UpdateOrder(Order ord);

        //Get Functions
        IEnumerable<HostingUnit> GetAllHostingUnits();

        IEnumerable<GuestRequest> GetAllGuestRequests();

        IEnumerable<Order> GetAllOrders();

        IEnumerable<BankBranch> GetAllBankAccounts();
        #endregion

        #region Functions specially added to BL

        #endregion
    }
}
