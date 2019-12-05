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
        //TODO: Check if everyone needs the get and set
        //TODO:Continue to insert the properties
    }

}
