using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    class Enums
    {
        public enum HostingUnitType { Zimmer, AccommodationApartment, HotelRoom, Encampment}
        public enum Area { North, South, Center, Jerusalem }
        public enum RequestStatus { UnTreated, SentMail, CustomerUnresponsiveness, CustomerResponsiveness }
        public enum OrderStatus { open, ClosedWithDeal, CloseWithExpired}
    }
}
