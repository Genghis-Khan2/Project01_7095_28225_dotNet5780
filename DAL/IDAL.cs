using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    interface IDAL
    {
        public void AddGuestRequest(BE.GuestRequest gr);

        public void UpdateGuestRequest(BE.GuestRequest gr);


        public void AddHostingUnit(HostingUnit hu);

        public void RemoveHostingUnit(HostingUnit hu);

        public void UpdateHostingUnit(HostingUnit hu);


        public void AddOrder(Order ord);

        public void UpdateOrder(Order ord);


        public List<HostingUnit> GetHostingUnits();

        public List<Guest> GetGuests();

        public List<Order> GetOrders();

        public List<BankAccount> GetBankAccounts();
    }
}
