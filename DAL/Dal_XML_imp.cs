using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using Exceptions;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        #region Paths
        private readonly string hostsPath = @"..\..\..\..\Host.xml";
        private readonly string hostingUnitPath = @"..\..\..\..\HostingUnit.xml";
        private readonly string guestRequestPath = @"..\..\..\..\GuestRequest.xml";
        private readonly string orderPath = @"..\..\..\..\Order.xml";
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

        #region Loading Functions

        #region Loading Object List Functions
        private List<HostingUnit> LoadHostingUnitList()
        {
            using (StreamReader sr = new StreamReader(hostingUnitPath))
            {
                if (sr.Peek() != -1)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HostingUnit>));
                    List<HostingUnit> list = (List<HostingUnit>)xmlSerializer.Deserialize(sr);
                    return list;
                }
            }

            return new List<HostingUnit>();
        }

        private List<Host> LoadHostList()
        {
            using (StreamReader sr = new StreamReader(hostsPath))
            {
                if (sr.Peek() != -1)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Host>));
                    List<Host> list = (List<Host>)xmlSerializer.Deserialize(sr);
                    return list;
                }
            }

            return new List<Host>();

        }

        private List<GuestRequest> LoadGuestRequestList()
        {
            using (StreamReader sr = new StreamReader(guestRequestPath))
            {
                if (sr.Peek() != -1)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GuestRequest>));
                    List<GuestRequest> list = (List<GuestRequest>)xmlSerializer.Deserialize(sr);
                    return list;
                }
            }

            return new List<GuestRequest>();
        }

        private void LoadOrderData()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);
            }
            catch
            {
                throw new FileLoadException("File loading problem");
            }
        }

        private List<Order> LoadOrderList()
        {
            try
            {
                LoadOrderData();
            }
            catch (FileLoadException)
            {
                return new List<Order>();
            }
            List<Order> orders;
            try
            {
                orders = (from ord in orderRoot.Elements("order")
                          select new Order()
                          {
                              CreateDate = new DateTime
                              (
                                  int.Parse(ord.Element("createdate").Element("year").Value),
                                  int.Parse(ord.Element("createdate").Element("month").Value),
                                  int.Parse(ord.Element("createdate").Element("day").Value)
                              ),

                              OrderDate = new DateTime
                              (
                                  int.Parse(ord.Element("orderdate").Element("year").Value),
                                  int.Parse(ord.Element("orderdate").Element("month").Value),
                                  int.Parse(ord.Element("orderdate").Element("day").Value)
                              ),

                              GuestRequestKey = int.Parse(ord.Element("guestrequestkey").Value),
                              HostingUnitKey = int.Parse(ord.Element("hostingunitkey").Value),
                              OrderKey = int.Parse(ord.Element("orderkey").Value),
                              Status = (Enums.OrderStatus)Enum.Parse(typeof(Enums.OrderStatus), ord.Element("status").Value)
                          }
            ).ToList();
            }

            catch
            {
                orders = new List<Order>();
            }

            return orders;
        }

        #endregion

        #endregion

        #region Saving Functions

        private void SaveOrders(List<Order> list)
        {
            orderRoot = new XElement("orders",
                         from order in list
                         select new XElement("order",
                            new XElement("hostingunitkey", order.HostingUnitKey),
                            new XElement("guestrequestkey", order.GuestRequestKey),
                            new XElement("orderkey", order.OrderKey),
                            new XElement("status", order.Status),
                            new XElement("createdate",
                                new XElement("year", order.CreateDate.Year),
                                new XElement("month", order.CreateDate.Month),
                                new XElement("day", order.CreateDate.Day)),
                            new XElement("orderdate",
                                new XElement("year", order.OrderDate.Year),
                                new XElement("month", order.OrderDate.Month),
                                new XElement("day", order.OrderDate.Day))));

            orderRoot.Save(orderPath);

        }

        #region Saving Object List Function
        private void SaveObjectList<T>(List<T> objects, string path)
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
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(objects.GetType());
                xmlSerializer.Serialize(sw, objects);
            }
        }

        #endregion
        #endregion

        public void AddGuestRequest(GuestRequest gr)
        {
            if (gr.GuestRequestKey == 0)
            {
                gr.GuestRequestKey = Configuration.GuestRequestKey;
            }
            var list = LoadGuestRequestList();
            if (list.Exists(s => s.GuestRequestKey == gr.GuestRequestKey))
            {
                throw new AlreadyExistsException(gr.GuestRequestKey, "GuestRequest");
            }

            list.Add(gr);

            SaveObjectList(list, guestRequestPath);
        }

        public void AddHostingUnit(HostingUnit hu)
        {
            if (hu.HostingUnitKey == 0)
            {
                hu.HostingUnitKey = Configuration.HostingUnitKey;
            }

            var list = LoadHostingUnitList();

            if (list.Exists(s => s.HostingUnitKey == hu.HostingUnitKey))
            {
                throw new AlreadyExistsException(hu.HostingUnitKey, "HostingUnit");
            }

            list.Add(hu);

            SaveObjectList(list, hostingUnitPath);
        }

        public void AddHost(Host host)
        {
            if (host.HostKey == 0)
            {
                host.HostKey = Configuration.HostKey;
            }

            var list = LoadHostList();

            if (list.Exists(s => s.HostKey == host.HostKey))
            {
                throw new AlreadyExistsException(host.HostKey, "Host");
            }

            list.Add(host);

            SaveObjectList(list, hostsPath);
        }

        public void AddOrder(Order ord)
        {
            if (ord.OrderKey == 0)
            {
                ord.OrderKey = Configuration.OrderKey;
            }

            var list = LoadOrderList();

            if (list.Exists(s => s.OrderKey == ord.OrderKey))
            {
                throw new AlreadyExistsException(ord.OrderKey, "Order");
            }

            list.Add(ord);

            SaveOrders(list);
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

        public void RemoveGuestRequest(int key)
        {
            throw new NotImplementedException();
        }
    }
}
