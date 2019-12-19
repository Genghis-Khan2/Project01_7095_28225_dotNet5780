using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
        //Functions for working with customer requirement
        void AddGuestRequest(GuestRequest gr);

        void UpdateGuestRequest(GuestRequest gr, int key);

        //Functions for working with hosting units
        void AddHostingUnit(HostingUnit hu);

        void RemoveHostingUnit(int key);

        void UpdateHostingUnit(HostingUnit hu, int key);

        //Functions for working with Orders
        void AddOrder(Order ord);

        void UpdateOrder(Order ord, int key);


        IEnumerable<HostingUnit> GetAllHostingUnits();

        IEnumerable<GuestRequest> GetAllGuestRequests();

        IEnumerable<Order> GetAllOrders();

        IEnumerable<BankAccount> GetAllBankAccounts();
    }
}
