using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    interface IDAL
    {
        public void addGuestRequest(Guest gr)
        {

        }

        public void updateGuestRequest(Guest gr)
        {

        }


        public void addHostingUnit(HostingUnit hu)
        {

        }

        public void removeHostingUnit(HostingUnit hu)
        {

        }

        public void updateHostingUnit(HostingUnit hu)
        {

        }


        public void addOrder(Order ord)
        {

        }

        public void updateOrder(Order ord)
        {

        }


        public List<HostingUnit> getHostingUnits()
        {

        }

        public List<Guest> getGuests()
        {

        }

        public List<Order> getOrders()
        {

        }
    }
}
