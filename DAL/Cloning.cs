using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static GuestRequest Clone(this GuestRequest original)
        {
            //TODO: Finish up the function
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
            };

            return ret;
        }

        public static BankAccount Clone(this BankAccount original)
        {
            BankAccount ret = new BankAccount()
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

        public static Host Clone(this Host original)
        {
            Host ret = new Host()
            {
                BankAccount = original.BankAccount.Clone() as BankAccount,
                CollectionClearance = original.CollectionClearance,
                FamilyName = original.FamilyName.Clone() as string,
                HostKey = original.HostKey,
                MailAddress = original.MailAddress.Clone() as string,
                PhoneNumber = original.PhoneNumber,
                PrivateName = original.PrivateName.Clone() as string
            };

            return ret;
        }

        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit ret = new HostingUnit()
            {
                Diary = original.Diary.Clone() as int[][],
                HostingUnitKey = original.HostingUnitKey,
                HostingUnitName = original.HostingUnitName.Clone() as string,
                Owner = original.Owner.Clone() as Host
            };

            return ret;
        }
    }
}
