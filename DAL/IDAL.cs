using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    interface IDAL
    {
        public void AddGuestRequest(Guest gr);

        public void UpdateGuestRequest(Guest gr);


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
