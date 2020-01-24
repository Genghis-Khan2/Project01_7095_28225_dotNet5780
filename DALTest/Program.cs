using System;
using Exceptions;
using DAL;
using BE;
using FR;

namespace DALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL dal = Dal_XML_imp.GetDAL();
            GuestRequest gr = new GuestRequest()
            {
                Adults = 4,
                Area = Enums.Area.Jerusalem,
                Children = 2,
                ChildrensAttractions = Enums.IsInterested.Necessary,
                EntryDate = new DateTime(2020, 2, 13),
                FamilyName = "Komet",
                Garden = Enums.IsInterested.Possible,
                Jacuzzi = Enums.IsInterested.Necessary,
                MailAddress = "gabikomet@gmail.com",
                Pool = Enums.IsInterested.Necessary,
                PrivateName = "Gabi",
                RegistrationDate = DateTime.Today,
                ReleaseDate = new DateTime(2020, 2, 20),
                Status = Enums.RequestStatus.Open,
                Type = Enums.HostingUnitType.Hotel
            };

            // dal.AddGuestRequest(gr);

            HostingUnit hu = new HostingUnit()
            {
                Area = Enums.Area.Jerusalem,
                HostingUnitName = "Sleepy Palms",
                IsThereChildrensAttractions = false,
                IsThereGarden = false,
                IsThereJacuzzi = true,
                IsTherePool = true,
                NumberOfPlacesForAdults = 4,
                NumberOfPlacesForChildren = 2,
                Owner = new Host()
                {
                    FamilyName = "Komet",
                    PrivateName = "Gabi"
                },
                Type = Enums.HostingUnitType.Hotel
            };

            //dal.AddHostingUnit(hu);

            dal.AddOrder(
                new Order()
                {
                    CreateDate = DateTime.Today,
                    GuestRequestKey = gr.GuestRequestKey,
                    HostingUnitKey = hu.HostingUnitKey,
                    Status = Enums.OrderStatus.UnTreated,
                    OrderDate = DateTime.Today
                }
                );

        }
    }
}
