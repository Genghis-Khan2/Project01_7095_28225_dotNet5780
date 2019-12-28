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
            //bool isValidNum = false;
            //string user;
            //int user_i = 0;
            //while (!isValidNum)
            //{
            //    Console.WriteLine("Hello! Who are you?");
            //    Console.WriteLine("1. Guest\n2. Host\n3. Site admin");
            //    user = Console.ReadLine();
            //    isValidNum = int.TryParse(user, out user_i);
            //    if (isValidNum)
            //    {
            //        if (user_i > 3 || user_i < 1)
            //        {
            //            isValidNum = false;
            //        }
            //    }
            //    if (!isValidNum)
            //    {
            //        Console.WriteLine("Invalid value. Please enter 1, 2 or 3");
            //    }
            //}

            //switch (user_i)
            //{
            //    case 1:
            //        handleGuest();
            //        break;
            //    case 2:
            //        handleHost();
            //        break;
            //    case 3:
            //        handleAdmin();
            //        break;
            //}

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
                CreateDate = new DateTime(2019, 29, 2),
                GuestRequestKey = gr.GuestRequestKey,
                HostingUnitKey = hu2.HostingUnitKey,
                OrderDate = new DateTime(2019, 4, 2),
                Status = Enums.OrderStatus.UnTreated
            };

            bl.AddOrder(o);

            foreach (var i in bl.GetAllAvailableHostingUnit(new DateTime(2019, 3, 2), 60))
            {
                Console.WriteLine(i);
            }

            foreach (var i in bl.getAllGuestRequestWhere(isGuestRequestWithPool)
                {
                Console.WriteLine(i);
            }

            foreach (var i in bl.GetAllBankAccounts())
            {
                Console.WriteLine(i); ;
            }

            Console.WriteLine(bl.getAmountOfOrderToGuest(gr));
        }
    }
}
