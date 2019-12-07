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
        public string MailAddress
        {
            get
            {
                return this.MailAddress;
            }
            set
            {
                //Input integrity checking by using the email address integrity check in the System.Net.Mail.MailAddress function
                try
                {
                    new System.Net.Mail.MailAddress(value);
                    this.MailAddress = value;
                    //if the email address valid
                }
                catch
                {
                    //if the email address is invalid
                    this.MailAddress = "plony@almony.com";
                }
            }
        }
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
        /// Default constructor
        /// </summary>
        public GuestRequest()
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.Open;

            this.PrivateName = "";
            this.FamilyName = "";
            this.MailAddress = "plony@almony.com";
            this.RegistrationDate = new DateTime(2020, 1, 1);
            this.EntryDate = new DateTime(2020, 1, 1);
            this.ReleaseDate = new DateTime(2020, 1, 1);//Note that this is a default date but it will create an error if it tries to be fulfilled
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privateName">Customer first name</param>
        /// <param name="familyName">Customer's last name</param>
        /// <param name="mailAddress">Customer Email Address</param>
        /// <param name="registrationDate">System registration date</param>
        /// <param name="entryDate">Preferred date for starting the vacation</param>
        /// <param name="releaseDate">Preferred date for ending the vacation</param>
        /// <param name="area">The desired resort in Israel</param>
        /// <param name="type">Type of hosting unit desired</param>
        public GuestRequest(string privateName, string familyName, string mailAddress, DateTime registrationDate, DateTime entryDate, DateTime releaseDate, Enums.Area area, Enums.HostingUnitType type)
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.Open;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privateName">Customer first name</param>
        /// <param name="familyName">Customer's last name</param>
        /// <param name="mailAddress">Customer Email Address</param>
        /// <param name="status">Status of the </param>
        /// <param name="registrationDate">System registration date</param>
        /// <param name="entryDate">Preferred date for starting the vacation</param>
        /// <param name="releaseDate">Preferred date for ending the vacation</param>
        /// <param name="area">The desired resort in Israel</param>
        /// <param name="type">Type of hosting unit desired</param>
        public GuestRequest(string privateName, string familyName, string mailAddress, Enums.RequestStatus status, DateTime registrationDate, DateTime entryDate, DateTime releaseDate, Enums.Area area, Enums.HostingUnitType type)
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;

            //User-defined properties
            this.PrivateName = privateName;
            this.FamilyName = familyName;
            this.MailAddress = mailAddress;
            this.Status = status;
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
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privateName">Customer first name</param>
        /// <param name="familyName">Customer's last name</param>
        /// <param name="mailAddress">Customer Email Address</param>
        /// <param name="registrationDate">System registration date</param>
        /// <param name="entryDate">Preferred date for starting the vacation</param>
        /// <param name="releaseDate">Preferred date for ending the vacation</param>
        /// <param name="area">The desired resort in Israel</param>
        /// <param name="type">Type of hosting unit desired</param>
        /// <param name="pool">Is interested in the pool</param>
        /// <param name="jacuzzi">Is interested in Jacuzziparam>
        /// <param name="garden">Is interested in the garden</param>
        /// <param name="childrensAttractions">Is interested in children's attractions</param>
        public GuestRequest(string privateName, string familyName, string mailAddress, DateTime registrationDate, DateTime entryDate, DateTime releaseDate, Enums.Area area, Enums.HostingUnitType type, Enums.IsInterested pool, Enums.IsInterested jacuzzi, Enums.IsInterested garden, Enums.IsInterested childrensAttractions)
        {
            //Values that are independent of the user
            this.GuestRequestKey = Configuration.GuestRequestKey++;
            this.Status = Enums.RequestStatus.Open;

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
        //TODO: SubArea?

    }

}
