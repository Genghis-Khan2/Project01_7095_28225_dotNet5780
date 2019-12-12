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
        public enum HostingUnitType { Zimmer, AccommodationApartment, Hotel, Camping }
        public enum Area { All, North, South, Center, Jerusalem }
        public enum RequestStatus { UnTreated, SentMail, CustomerUnresponsiveness, CustomerResponsiveness }
        public enum OrderStatus { Open, ClosedWithDeal, CloseWithExpired }
        public enum IsInterested { Necessary, Possible, Uninterested }

        
    }
}
