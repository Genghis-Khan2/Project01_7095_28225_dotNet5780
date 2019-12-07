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
        //TODO: comments
        public GuestRequest()
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.UnTreated;

            this.PrivateName = "";
            this.FamilyName = "";
            this.MailAddress = "plony@almony.com";
            this.RegistrationDate = new DateTime(1, 1, 2020);
            this.EntryDate = new DateTime(1, 1, 2020);
            this.ReleaseDate = new DateTime(1, 1, 2020);//Note that this is a default date but it will create an error if it tries to be fulfilled
            this.Area = Enums.Area.All;
            //this.SubArea=all;
            this.Type = Enums.HostingUnitType.All;
            this.Adults = 2;
            this.Children = 0;
            this.Pool = Enums.IsInterested.Possible;
            this.Jacuzzi = Enums.IsInterested.Possible;
            this.Garden = Enums.IsInterested.Possible;
            this.ChildrensAttractions = Enums.IsInterested.Possible;
        }

        public GuestRequest(string privateName, string familyName, string mailAddress, DateTime registrationDate, DateTime entryDate, DateTime releaseDate, Enums.Area area, Enums.HostingUnitType type)
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.UnTreated;

            //User-defined properties
            this.PrivateName = privateName;
            this.FamilyName = familyName;
            this.MailAddress = mailAddress;
            this.RegistrationDate = registrationDate;
            this.EntryDate = entryDate;
            this.ReleaseDate = releaseDate;
            this.Area = area;
            this.Type = type;

            //default value
            this.Pool = Enums.IsInterested.Possible;
            this.Jacuzzi = Enums.IsInterested.Possible;
            this.Garden = Enums.IsInterested.Possible;
            this.ChildrensAttractions = Enums.IsInterested.Possible;
        }

        public GuestRequest(string privateName, string familyName, string mailAddress, DateTime registrationDate, DateTime entryDate, DateTime releaseDate, Enums.Area area, Enums.HostingUnitType type, Enums.IsInterested pool, Enums.IsInterested jacuzzi, Enums.IsInterested garden, Enums.IsInterested childrensAttractions)
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.UnTreated;

            //User-defined properties
            this.PrivateName = privateName;
            this.FamilyName = familyName;
            this.MailAddress = mailAddress;
            this.RegistrationDate = registrationDate;
            this.EntryDate = entryDate;
            this.ReleaseDate = releaseDate;
            this.Area = area;
            this.Type = type;
            this.Pool = pool;
            this.Garden = garden;
            this.Jacuzzi = jacuzzi;
            this.ChildrensAttractions = childrensAttractions;
        }

        /// <summary>
        /// The function returns the GuestRequest information in a string type
        /// </summary>
        /// <returns>The GuestRequest information in string type</returns>
        public override string ToString()
        {
            string res = "";
            res += "Guest Request Key: " + GuestRequestKey + "\n";
            res += "Private Name: " + PrivateName + "\n";
            res += "Family Name: " + FamilyName + "\n";
            res += "Mail Address: " + MailAddress + "\n";
            res += "Status: " + Status + "\n";
            res += "Registration Date: " + RegistrationDate + "\n";
            res += "Entry Date: " + EntryDate + "\n";
            res += "Release Date: " + ReleaseDate + "\n";
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
        //TODO: SubArea?

        private bool IsValidEmail(string email)//TODO:test it
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

}
