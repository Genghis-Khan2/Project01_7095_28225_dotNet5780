using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        #region Paths
        private readonly string hostsPath = @"Hosts.xml";
        private readonly string hostingUnitPath = @"Hostingunit.xml";
        private readonly string guestRequestPath = @"GuestRequest.xml";
        private readonly string orderPath = @"Order.xml";
        #endregion

        #region Roots
        private XElement guestRequestRoot = null;
        private XElement hostingUnitRoot = null;
        private XElement hostRoot = null;
        private XElement orderRoot = null;
        #endregion

        #region Singleton and Factory Methods
        private Dal_XML_imp()
        { }

        protected static Dal_XML_imp instance = null;

        /// <summary>
        /// This is the factory method of Dal_XML_imp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        public static IDAL GetDAL()
        {
            if (instance == null)
            {
                instance = new Dal_XML_imp();
                return instance;
            }
            return instance;
        }
        #endregion

        #region Loading Single Object Functions
        /// <summary>
        /// Loads the guest requests XML file into the root object
        /// </summary>
        /// <exception cref="UnknownException">The exception thrown is unclear</exception>
        private void LoadGuestRequests()
        {
            try
            {
                guestRequestRoot = XElement.Load(guestRequestPath);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Loads the hosting units XML file into the root object
        /// </summary>
        /// <exception cref="UnknownException">The exception thrown is unclear</exception>
        /// <summary>
        /// Loads the orders XML file into the root object
        /// </summary>
        /// <exception cref="UnknownException">The exception thrown is unclear</exception>
        private void LoadHostingUnits()
        {
            try
            {
                hostingUnitRoot = XElement.Load(hostingUnitPath);
            }
            catch
            {
                throw;
            }
        }
        private void LoadOrders()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Loads the hosts XML file into the root object
        /// </summary>
        /// <exception cref="UnknownException">The exception thrown is unclear</exception>
        private void LoadHosts()
        {
            try
            {
                hostRoot = XElement.Load(hostsPath);
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Loading Object List Functions
        private List<HostingUnit> LoadHostingUnitList()
        {
            FileStream file = new FileStream(hostingUnitPath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HostingUnit>));
            List<HostingUnit> list = (List<HostingUnit>)xmlSerializer.Deserialize(file);
            file.Close();
            return list;
        }
        private List<Host> LoadHostList()
        {
            FileStream file = new FileStream(hostsPath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Host>));
            List<Host> list = (List<Host>)xmlSerializer.Deserialize(file);
            file.Close();
            return list;
        }
        private List<GuestRequest> LoadGuestRequestList()
        {
            FileStream file = new FileStream(guestRequestPath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GuestRequest>));
            List<GuestRequest> list = (List<GuestRequest>)xmlSerializer.Deserialize(file);
            file.Close();
            return list;
        }
        private List<Order> LoadOrderList()
        {
            FileStream file = new FileStream(orderPath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            List<Order> list = (List<Order>)xmlSerializer.Deserialize(file);
            file.Close();
            return list;
        }

        #endregion

        #region Saving Functions
        private void SaveGuestRequestList(List<GuestRequest> guestRequests)
        {
            //guestRequestRoot = new XElement("guestrequests",
            //                    from g in guestRequests
            //                    select new XElement("guestrequest",
            //                        new XElement("guestrequestkey", g.GuestRequestKey),
            //                        new XElement("name",
            //                            new XElement("privatename", g.PrivateName),
            //                            new XElement("familyname", g.FamilyName)),
            //                        new XElement("mailaddress", g.MailAddress),
            //                        new XElement("status", g.Status.ToString()),
            //                        new XElement("registrationdate",
            //                            new XElement("year", g.RegistrationDate.Year),
            //                            new XElement("month", g.RegistrationDate.Month),
            //                            new XElement("day", g.RegistrationDate.Day)),
            //                        new XElement("entrydate",
            //                            new XElement("year", g.EntryDate.Year),
            //                            new XElement("month", g.EntryDate.Month),
            //                            new XElement("day", g.EntryDate.Day)),
            //                        new XElement("releasedate",
            //                            new XElement("year", g.ReleaseDate.Year),
            //                            new XElement("month", g.ReleaseDate.Month),
            //                            new XElement("day", g.ReleaseDate.Day)),
            //                        new XElement("area", g.Area),
            //                        new XElement("type", g.Type),
            //                        new XElement("adults", g.Adults),
            //                        new XElement("children", g.Children),
            //                        new XElement("pool", g.Pool),
            //                        new XElement("jacuzzi", g.Jacuzzi),
            //                        new XElement("garden", g.Garden),
            //                        new XElement("childrensattractions", g.ChildrensAttractions))
            //    );

            //guestRequestRoot.Save(guestRequestPath);

            FileStream file = new FileStream(guestRequestPath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(guestRequests.GetType());
            xmlSerializer.Serialize(file, guestRequests);
            file.Close();
        }

        private void SaveHostingUnitList(List<HostingUnit> hostingUnits)
        {
            FileStream file = new FileStream(hostingUnitPath, FileMode.Create);
            XmlSerializer xmlser = new XmlSerializer(typeof(HostingUnit));
            xmlser.Serialize(file, hostingUnits);
            file.Close();
        }

        private void SaveHostList(List<Host> hosts)
        {
            //hostRoot = new XElement("hosts",
            //                from h in hosts
            //                select new XElement("host",
            //                        new XElement("name",
            //                            new XElement("privatename", h.PrivateName),
            //                            new XElement("familyname", h.FamilyName)),
            //                        new XElement("phonenumber", h.PhoneNumber),
            //                        new XElement("mailaddress", h.MailAddress),
            //                        new XElement("brankbranchdetails",
            //                            new XElement("banknumber", h.BankBranchDetails.BankNumber),
            //                            new XElement("bankname", h.BankBranchDetails.BankName),
            //                            new XElement("branchnumber", h.BankBranchDetails.BranchNumber),
            //                            new XElement("branchaddress", h.BankBranchDetails.BranchAddress),
            //                            new XElement("branchcity", h.BankBranchDetails.BranchCity)),
            //                        new XElement("bankaccountnumber", h.BankAccountNumber),
            //                        new XElement("collectionclearance", h.CollectionClearance)));
            //hostRoot.Save(hostsPath);

            FileStream file = new FileStream(hostsPath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(hosts.GetType());
            xmlSerializer.Serialize(file, hosts);
            file.Close();
        }

        private void SaveOrderList(List<Order> orders)
        {
            FileStream file = new FileStream(orderPath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(orders.GetType());
            xmlSerializer.Serialize(file, orders);
            file.Close();
        }
        #endregion

        public void AddGuestRequest(GuestRequest gr)
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order ord)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfBankAccountExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfGuestRequestExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHostExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHostingUnitExists(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfOrderExists(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Host> GetAllHosts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public BankBranch GetBankBranch(int key)
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequest(int key)
        {
            throw new NotImplementedException();
        }

        public Host GetHost(int key)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int key)
        {
            throw new NotImplementedException();
        }

        public void RemoveHostingUnit(int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderStatus(int key, Enums.OrderStatus stat)
        {
            throw new NotImplementedException();
        }
    }
}
