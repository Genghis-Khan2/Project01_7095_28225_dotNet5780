using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Enums
    {
        public enum HostingUnitType { Zimmer, AccommodationApartment, HotelRoom, Encampment}
        public enum Area {All, North, South, Center, Jerusalem }
        public enum RequestStatus { UnTreated, SentMail, CustomerUnresponsiveness, CustomerResponsiveness }
        public enum OrderStatus { Open, ClosedWithDeal, CloseWithExpired}
    }
}
