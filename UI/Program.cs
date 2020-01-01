using System;
using BL;
using BE;

namespace UI
{
    class Program
    {

        public static void handleGuest()
        {
            //TODO: Implement the 3 functions
        }

        public static void handleHost()
        {

        }

        public static void handleAdmin()
        {

        }

        public static bool isGuestRequestWithPool(GuestRequest gr)
        {
            return gr.Pool == Enums.IsInterested.Necessary || gr.Pool == Enums.IsInterested.Possible;
        }
        static void Main(string[] args)
        {
            IBL bl = BLImp.getBL();
            GuestRequest gr = new GuestRequest
            {
                Adults = 2,
                Area = Enums.Area.Jerusalem,
                Children = 2,
                ChildrensAttractions = Enums.IsInterested.Uninterested,
                EntryDate = new DateTime(2019, 5, 2),
                FamilyName = "Ben Komay",
                Garden = Enums.IsInterested.Necessary,
                Jacuzzi = Enums.IsInterested.Necessary,
                MailAddress = "snugglebunch@gmail.co.il",
                Pool = Enums.IsInterested.Possible,
                PrivateName = "Ellen",
                RegistrationDate = new DateTime(2019, 1, 12),
                ReleaseDate = new DateTime(2019, 7, 30),
                Status = Enums.RequestStatus.Open,
                Type = Enums.HostingUnitType.Hotel
            };
            bl.AddGuestRequest(gr);

            HostingUnit hu = new HostingUnit
            {
                Area = Enums.Area.North,
                HostingUnitName = "Black Nose World",
                Diary = null,
                IsThereChildrensAttractions = false,
                IsThereGarden = true,
                IsThereJacuzzi = true,
                IsTherePool = true,
                NumberOfPlacesForAdults = 4,
                NumberOfPlacesForChildren = 1,
                Type = Enums.HostingUnitType.Zimmer
            };
            Host schnorer = new Host
            {
                BankAccountNumber = 5000,
                BankBranchDetails = new BankBranch
                {
                    BankAccountNumber = 10000,
                    BankName = "Mizrachi",
                    BankNumber = 100,
                    BranchAddress = "31 Maple St.",
                    BranchCity = "Police",
                    BranchNumber = 1221
                },
                CollectionClearance = true,
                FamilyName = "Slaggish",
                HostKey = 31262,
                MailAddress = "BlorgalorgPhat@hotpolls.pizzazz",
                PhoneNumber = "12015684586",
                PrivateName = "Schnorer",
            };
            hu.Owner = schnorer;
            bl.AddHostingUnit(hu);

            HostingUnit hu2 = new HostingUnit()
            {
                Area = Enums.Area.Jerusalem,
                HostingUnitName = "Black Falcon",
                IsThereChildrensAttractions = false,
                IsThereGarden = true,
                IsThereJacuzzi = true,
                IsTherePool = true,
                NumberOfPlacesForAdults = 3,
                NumberOfPlacesForChildren = 2,
                Owner = schnorer,
                Type = Enums.HostingUnitType.Hotel
            };
            bl.AddHostingUnit(hu2);

            Order o = new Order()
            {
                CreateDate = new DateTime(2019, 2, 25),
                GuestRequestKey = gr.GuestRequestKey,
                HostingUnitKey = hu2.HostingUnitKey,
                OrderDate = new DateTime(2019, 4, 2),
                Status = Enums.OrderStatus.UnTreated
            };

            bl.AddOrder(o);

            Console.WriteLine("Available HostingUnits:");
            foreach (var i in bl.GetAllAvailableHostingUnit(new DateTime(2019, 3, 2), 60))
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("GuestRequests requiring a pool:");
            foreach (var i in bl.GetAllGuestRequestWhere(isGuestRequestWithPool))
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("All bank accounts:");
            foreach (var i in bl.GetAllBankAccounts())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Amount of orders to the guest:");
            Console.WriteLine(bl.GetAmountOfOrderToGuest(gr));

            Console.WriteLine("Dates from 15.5.2019 to now");
            bl.GetNumberOfDateInRange(new DateTime(2019, 5, 15));

            Console.WriteLine("Dates from 25.2.2019 to 25.3.2019");
            bl.GetNumberOfDateInRange(new DateTime(2019, 2, 25), new DateTime(2019, 3, 25));

            Console.WriteLine("There are {0} orders to the guestRequest", bl.GetAmountOfOrderToGuest(gr));

            Console.WriteLine("Orders between now and 60 days from now:");
            foreach (var i in bl.GetAllOrderInRange(60))
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(bl.GetAllsuccessfulOrder(hu));

            Console.WriteLine("Lets check if the hostingUnit is available from 25.4.2019 to 1.6.2019");
            if (bl.CheckIfAvailable(hu.Diary, new DateTime(2017, 4, 25), new DateTime(2019, 6, 1)))
            {
                Console.WriteLine("It is");
            }

            else
            {
                Console.WriteLine("It isn't");
            }

            Console.WriteLine("Let's mark those days");
            bool[,] meow = new bool[12, 31];
            bl.MarkingInTheDiary(meow, new DateTime(2017, 4, 25), new DateTime(2019, 6, 1));

            Console.WriteLine("Now is it marked?");
            if (bl.CheckIfAvailable(meow, new DateTime(2017, 4, 25), new DateTime(2019, 6, 1)))
            {
                Console.WriteLine("It is");
            }

            else
            {
                Console.WriteLine("It isn't");
            }

            var areaToGR = bl.GetAllGuestByArea();
            foreach (var i in areaToGR)
            {
                switch (i.Key)
                {
                    case Enums.Area.All:
                        Console.WriteLine("All:");
                        Console.WriteLine(i);
                        break;
                    case Enums.Area.Center:
                        Console.WriteLine("Center:");
                        Console.WriteLine(i);
                        break;
                    case Enums.Area.Jerusalem:
                        Console.WriteLine("Jerusalem:");
                        Console.WriteLine(i);
                        break;
                    case Enums.Area.North:
                        Console.WriteLine("North:");
                        Console.WriteLine(i);
                        break;
                    case Enums.Area.South:
                        Console.WriteLine("South:");
                        Console.WriteLine(i);
                        break;
                }
            }
        }
    }
}
