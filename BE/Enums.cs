using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class contains all the required enums
    /// </summary>
    public class Enums
    {
        public enum HostingUnitType { All, Zimmer, AccommodationApartment, Hotel, Camping }
        public enum Area { All, North, South, Center, Jerusalem }
        public enum RequestStatus { Open, ClosedWithDeal, CloseWithExpired }
        public enum OrderStatus { UnTreated, SentMail, ClosedByCustomerUnresponsiveness, ClosedByCustomerResponsiveness, ClosedByHost}
        public enum IsInterested { Necessary, Possible, Uninterested }


    }
}
