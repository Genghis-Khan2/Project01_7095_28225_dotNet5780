using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {

        IDAL getDal();
        void AddGuestRequest(GuestRequest gr);

        void UpdateGuestRequest(GuestRequest gr);


        void AddHostingUnit(HostingUnit hu);

        void RemoveHostingUnit(HostingUnit hu);

        void UpdateHostingUnit(HostingUnit hu);


        void AddOrder(Order ord);

        void UpdateOrder(Order ord);


        List<HostingUnit> GetHostingUnits();

        List<GuestRequest> GetGuestRequests();

        List<Order> GetOrders();

        List<BankAccount> GetBankAccounts();
    }
}
