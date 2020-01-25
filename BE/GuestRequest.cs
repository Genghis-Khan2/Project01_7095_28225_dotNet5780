using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a customer hosting requirement 
    /// </summary>
    public class GuestRequest
    {
        public Guest Requester { get; set; }
        public int GuestRequestKey { get; set; } = 0;//No need to change, determined when creating the object
        public Enums.RequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Enums.Area Area { get; set; }
        public Enums.HostingUnitType Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Enums.IsInterested Pool { get; set; }
        public Enums.IsInterested Jacuzzi { get; set; }
        public Enums.IsInterested Garden { get; set; }
        public Enums.IsInterested ChildrensAttractions { get; set; }

        /// <summary>
        /// The function returns the GuestRequest information in a string type
        /// </summary>
        /// <returns>The GuestRequest information in string type</returns>
        ///<seealso cref="Object.ToString()"/>
        public override string ToString()
        {
            string res = "";
            res += "Guest Request Key: " + GuestRequestKey + "\n";
            res += "Status: " + Status + "\n";
            res += "Registration Date: " + RegistrationDate.ToString("dd.MM.yyyy") + "\n";
            res += "Entry Date: " + EntryDate.ToString("dd.MM.yyyy") + "\n";
            res += "Release Date: " + ReleaseDate.ToString("dd.MM.yyyy") + "\n";
            res += "Area: " + Area + "\n";
            res += "Type: " + Type + "\n";
            res += "Adults: " + Adults + "\n";
            res += "Children: " + Children + "\n";
            res += "Pool: " + Pool + "\n";
            res += "Jacuzzi: " + Jacuzzi + "\n";
            res += "Garden" + Garden + "\n";
            res += "Childrens Attractions: " + ChildrensAttractions + "\n";
            return res;
        }


    }

}
