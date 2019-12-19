using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;

namespace BL
{
    public interface IBL
    {

        IBL getDal();

        //Functions for working with customer requirement
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

        IEnumerable<BankAccount> GetAllBankAccounts();
    }
}
