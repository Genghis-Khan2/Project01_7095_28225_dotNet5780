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
        public int GuestRequestKey { get; set; }//No need to change, determined when creating the object
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public Enums.RequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }//TODO:In this way? Shouldn't the SET be changed?
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
        public override string ToString()
        {
            string res = "";
            res += "Guest Request Key: " + GuestRequestKey;
            res += "Private Name: " + PrivateName;
            res += "Family Name: " + FamilyName;
            res += "Mail Address: " + MailAddress;
            res += "Status: " + Status;
            res += "Registration Date: " + RegistrationDate;
            res += "Entry Date: " + EntryDate;
            res += "Release Date: " + ReleaseDate;
            res += "Area: " + Area;
            res += "Type: " + Type;
            res += "Adults: " + Adults;
            res += "Children: " + Children;
            res += "Pool: " + Pool;
            res += "Jacuzzi: " + Jacuzzi;
            res += "Garden" + Garden;
            res += "Childrens Attractions: " + ChildrensAttractions;
            return res;
        }
        //TODO: SubArea?
        //TODO: Check if everyone needs the get and set
        //TODO:Continue to insert the properties
    }

}
