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
        private int GuestRequestKey { get; }//No need to change, determined when creating the object
        private string PrivateName { get; set; }
        private string FamilyName { get; set; }
        private string MailAddress { get; set; }
        private Enums.RequestStatus Status { get; set; }
        private DateTime RegistrationDate { get; set; }//TODO:In this way? Shouldn't the SET be changed?
        private DateTime EntryDate { get; set; }
        private DateTime ReleaseDate { get; set; }
        private Enums.Area Area { get; set; }
        private Enums.HostingUnitType Type { get; set; }
        private int Adults { get; set; }
        private int Children { get; set; }
        private Enums.IsInterested Pool { get; set; }
        private Enums.IsInterested Jacuzzi { get; set; }
        private Enums.IsInterested Garden { get; set; }
        private Enums.IsInterested ChildrensAttractions { get; set; }
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
