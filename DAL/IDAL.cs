using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface IDAL
    {
        public void AddGuestRequest(GuestRequest gr);

        public void UpdateGuestRequest(GuestRequest gr);


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
