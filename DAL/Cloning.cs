using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using BE;

namespace DAL
{
    public static class Cloning
    {
        #region Single These functions clone a single object

        #region GuestRequest-Clone Clones a GuestRequest object

        /// <summary>
        /// This function clones a GuestRequest object
        /// </summary>
        /// <param name="original">GuestRequest object to clone</param>
        /// <returns>Cloned GuestRequest object</returns>
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest ret = new GuestRequest()
            {
                Adults = original.Adults,
                Area = original.Area,
                Children = original.Children,
                ChildrensAttractions = original.ChildrensAttractions,
                EntryDate = original.EntryDate,
                FamilyName = original.FamilyName.Clone() as string,
                Garden = original.Garden,
                GuestRequestKey = original.GuestRequestKey,
                Jacuzzi = original.Jacuzzi,
                MailAddress = original.MailAddress.Clone() as string,
                Pool = original.Pool,
                PrivateName = original.PrivateName.Clone() as string,
                RegistrationDate = original.RegistrationDate,
                ReleaseDate = original.ReleaseDate,
                Status = original.Status,
                Type = original.Type
            };

            return ret;
        }

        #endregion

        #region BankAccount-Clone Clones a BankAccount object

        /// <summary>
        /// This function clones a BankAccount object
        /// </summary>
        /// <param name="original">BankAccount object to clone</param>
        /// <returns>Cloned BankAccount object</returns>
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch ret = new BankBranch()
            {
                BankAccountNumber = original.BankAccountNumber,
                BankName = original.BankName.Clone() as string,
                BankNumber = original.BankNumber,
                BranchAddress = original.BranchAddress.Clone() as string,
                BranchCity = original.BranchCity.Clone() as string,
                BranchNumber = original.BranchNumber
            };

            return ret;
        }

        #endregion

        #region Host-Clone Clones a Host object

        /// <summary>
        /// This function clones a Host object
        /// </summary>
        /// <param name="original">Host object to clone</param>
        /// <returns>Cloned Host object</returns>
        public static Host Clone(this Host original)
        {
            Host ret = new Host()
            {
                BankBranchDetails = original.BankBranchDetails.Clone() as BankBranch,
                BankAccountNumber = original.BankAccountNumber,
                CollectionClearance = original.CollectionClearance,
                FamilyName = original.FamilyName.Clone() as string,
                HostKey = original.HostKey,
                MailAddress = original.MailAddress.Clone() as string,
                PhoneNumber = original.PhoneNumber,
                PrivateName = original.PrivateName.Clone() as string
            };

            return ret;
        }

        #endregion

        #region HostingUnit-Clone Clones a HostingUnit object

        /// <summary>
        /// This function clones a HostingUnit object
        /// </summary>
        /// <param name="original">HostingUnit to clone</param>
        /// <returns>Cloned HostingUnit object</returns>
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit ret = new HostingUnit()
            {
                Diary = original.Diary.Clone() as bool[,],
                HostingUnitKey = original.HostingUnitKey,
                HostingUnitName = original.HostingUnitName.Clone() as string,
                Owner = original.Owner.Clone() as Host,
                Area = original.Area,
                Type = original.Type,
                NumberOfPlacesForAdults = original.NumberOfPlacesForAdults,
                NumberOfPlacesForChildren = original.NumberOfPlacesForChildren,
                IsTherePool = original.IsTherePool,
                IsThereJacuzzi = original.IsThereJacuzzi,
                IsThereGarden = original.IsThereGarden,
                IsThereChildrensAttractions = original.IsThereChildrensAttractions,
                Commission = original.Commission
            };

            return ret;
        }

        #endregion

        #region Order-Clone Clones an Order object

        /// <summary>
        /// This function clones an Order object
        /// </summary>
        /// <param name="original">Order to clone</param>
        /// <returns>Cloned Order object</returns>
        public static Order Clone(this Order original)
        {
            Order ret = new Order()
            {
                CreateDate = original.CreateDate,
                GuestRequestKey = original.GuestRequestKey,
                HostingUnitKey = original.HostingUnitKey,
                OrderDate = original.OrderDate,
                OrderKey = original.OrderKey,
                Status = original.Status
            };

            return ret;
        }

        #endregion

        #endregion

        #region List These functions clones a list of objects

        #region HU-List-Clone Clones HostingUnit List

        /// <summary>
        /// This function clones a HostingUnit List
        /// </summary>
        /// <param name="original">HostingUnit List to clone</param>
        /// <returns>Cloned HostingUnit List</returns>
        public static List<HostingUnit> Clone(this List<HostingUnit> original)
        {
            List<HostingUnit> ret = new List<HostingUnit>();
            foreach (HostingUnit i in original)
            {
                ret.Add(i.Clone());
            }

            return ret;
        }

        #endregion

        #region GR-List-Clone Clones GuestRequest List

        /// <summary>
        /// This function clones a GuestRequest List
        /// </summary>
        /// <param name="original">GuestRequest List to clone</param>
        /// <returns>Cloned GuestRequest List</returns>
        public static List<GuestRequest> Clone(this List<GuestRequest> original)
        {
            List<GuestRequest> ret = new List<GuestRequest>();
            foreach (GuestRequest i in original)
            {
                ret.Add(i.Clone());
            }

            return ret;
        }

        #endregion

        #region BA-List-Clone Clones BankAccounts List

        /// <summary>
        /// This function clones a BankAccount List
        /// </summary>
        /// <param name="original">BankAccount List to clone</param>
        /// <returns>Cloned BankAccount List</returns>
        public static List<BankBranch> Clone(this List<BankBranch> original)
        {
            var ret = new List<BankBranch>();
            foreach (var i in original)
            {
                ret.Add(i.Clone());
            }

            return ret;
        }

        #endregion

        #region Order-List-Clone Clones Order List

        /// <summary>
        /// This function clones an Order List
        /// </summary>
        /// <param name="original">Order List to clone</param>
        /// <returns>Cloned Order List</returns>
        public static List<Order> Clone(this List<Order> original)
        {
            var ret = new List<Order>();
            foreach (var i in original)
            {
                ret.Add(i.Clone());
            }

            return ret;
        }

        #endregion

        #region Host-List-Clone Clones Host List

        /// <summary>
        /// This function clones an Host List
        /// </summary>
        /// <param name="original">Host List to clone</param>
        /// <returns>Cloned Host List</returns>
        public static List<Host> Clone(this List<Host> original)
        {
            var ret = new List<Host>();
            foreach (var i in original)
            {
                ret.Add(i.Clone());
            }

            return ret;
        }

        #endregion

        #endregion
    }
}
