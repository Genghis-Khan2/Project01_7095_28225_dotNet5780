using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using Exceptions;
using System.Security.Cryptography;
using System.Net;
using System.ComponentModel;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        #region Paths
        private const string hostsPath = @"..\..\..\..\Host.xml";
        private const string hostingUnitPath = @"..\..\..\..\HostingUnit.xml";
        private const string guestRequestPath = @"..\..\..\..\GuestRequest.xml";
        private const string orderPath = @"..\..\..\..\Order.xml";
        private const string bankBranchPath = @"..\..\..\..\atm.xml";
        private const string configPath = @"..\..\..\..\config.xml";
        private const string guestPath = @"..\..\..\..\Guest.xml";
        private const string usersPath = @"..\..\..\..\Users.xml";
        #endregion

        #region Roots
        private XElement orderRoot = null;
        private XElement configRoot = null;
        private XElement userRoot = null;
        private XElement atmRoot = null;


        bool isBankFileAvailable = false;
        #endregion

        #region Singleton and Factory Methods
        private Dal_XML_imp()
        {
            CreateConfigFile();
            CreateOrderFile();
            CreatUsersFile();
            BackgroundWorker bw = new BackgroundWorker
            {
                WorkerReportsProgress = false
            };
            bw.DoWork += DownloadBankAccountInfo;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

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

        #region Private Help Methods

        #region Make Linq Files Correct Format

        private void CreateOrderFile()
        {
            using (StreamReader sr = new StreamReader(orderPath))
            {
                if (!File.Exists(orderPath) || sr.ReadLine() == null)
                {
                    orderRoot = new XElement("orders");
                    orderRoot.Save(orderPath);
                }

                else
                {
                    orderRoot = XElement.Load(orderPath);
                }
            }
        }

        private void CreateConfigFile()
        {
            using (StreamReader sr = new StreamReader(configPath))
            {
                if (!File.Exists(configPath) || sr.ReadLine() == null)
                {
                    configRoot = new XElement("config",
                                    new XElement("guestrequestkey", 1),
                                    new XElement("banknumber", 1),
                                    new XElement("hostkey", 1),
                                    new XElement("hostingunitkey", 1),
                                    new XElement("orderkey", 1),
                                    new XElement("commission"),
                                    new XElement("numberofdaysuntilexpired", 1),
                                    new XElement("guestkey", 1));
                    configRoot.Save(configPath);
                }

                else
                {
                    configRoot = XElement.Load(configPath);
                }
            }
        }

        private void CreatUsersFile()
        {
            using (StreamReader sr = new StreamReader(usersPath))
            {
                if (!File.Exists(usersPath) || sr.ReadLine() == null)
                {
                    userRoot = new XElement("users");
                    userRoot.Save(usersPath);
                }

                else
                {
                    userRoot = XElement.Load(usersPath);
                }
            }
        }

        #endregion

        #region Loading Functions


        #region LoadHostingUnitList Function

        /// <summary>
        /// This function loads a list containing the hosting unit from the files
        /// </summary>
        /// <returns>List of hosting units that are in file</returns>
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

        #endregion

        #region LoadHostList Function
        /// <summary>
        /// This function loads a list containing the hosts from the files
        /// </summary>
        /// <returns>List of hosts that are in file</returns>
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
        #endregion

        #region LoadGuestRequestList Function

        /// <summary>
        /// This function loads a list containing the guest requests from the files
        /// </summary>
        /// <returns>List of guest requests that are in file</returns>
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
        #endregion

        #region LoadOrderData Function
        /// <summary>
        /// This function loads the root of the order file into the root item
        /// </summary>
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
        #endregion

        #region LoadOrderList Function
        /// <summary>
        /// This function loads a list containing the orders from the files
        /// </summary>
        /// <returns>List of orders that are in file</returns>
        private List<Order> LoadOrderList()
        {
            try
            {

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

        private List<BankBranch> LoadBankBranchList()
        {
            if (!isBankFileAvailable)
            {
                return new List<BankBranch>();
            }
            List<BankBranch> bankBranches;
            try
            {
                bankBranches = (from branch in atmRoot.Elements("ATM")
                                select new BankBranch()
                                {
                                    BankNumber = int.Parse(branch.Element("קוד_בנק").Value),
                                    BankName = branch.Element("שם_בנק").Value,
                                    BankAccountNumber = GetBankNumber(),
                                    BranchAddress = branch.Element("כתובת_ה-ATM").Value,
                                    BranchCity = branch.Element("ישוב").Value,
                                    BranchNumber = int.Parse(branch.Element("קוד_סניף").Value)
                                }
            ).ToList();
            }

            catch
            {
                bankBranches = new List<BankBranch>();
            }

            return bankBranches;
        }

        public List<Guest> LoadGuestList()
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                if (sr.Peek() != -1)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Guest>));
                    List<Guest> list = (List<Guest>)xmlSerializer.Deserialize(sr);
                    return list;
                }
            }

            return new List<Guest>();

        }

        #endregion

        #region Saving Functions

        #region SaveOrders Function

        /// <summary>
        /// This function saves a <paramref name="list"/> of orders to the orders file.
        /// We made this seperate since we were required to make at least one linq function
        /// </summary>
        /// <param name="list">List of orders to add to the orders file</param>
        private void SaveOrders(List<Order> list)
        {
            orderRoot.Add(from order in list
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

        #endregion

        #region Saving Object List Function

        /// <summary>
        /// This function saves a list of <paramref name="objects"/> to the file at the <paramref name="path"/> specified.
        /// IMPORTANT: THIS FUNCTION OVERWRITES THE EXISTING FILE!
        /// </summary>
        /// <typeparam name="T">Type of objects to be saved to file</typeparam>
        /// <param name="objects">List of objects to save to file</param>
        /// <param name="path">Path of the file to add the list to</param>
        private void SaveObjectList<T>(List<T> objects, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false)) // This will create a new file if it does not exist, so there is no reason for a special function to do so
            {
                XmlSerializer xmlSerializer = new XmlSerializer(objects.GetType());
                xmlSerializer.Serialize(sw, objects);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Net Functions

        private void DownloadBankAccountInfo(object sender, DoWorkEventArgs e)
        {
            const string xmlLocalPath = bankBranchPath;
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                atmRoot = XElement.Load(bankBranchPath);
                isBankFileAvailable = true;
            }

            else
            {
                throw e.Error;
            }

        }


        #endregion

        #region General functions

        #region Guest

        public void AddGuest(Guest g)
        {
            if (g.GuestKey == 0)
            {
                g.GuestKey = GetGuestKey();
            }

            var list = LoadGuestList();
            if (list.Exists(s => s.GuestKey == g.GuestKey))
            {
                DecrementGuestKey();
                throw new AlreadyExistsException(g.GuestKey, "Guest");
            }

            list.Add(g);

            SaveObjectList(list, guestPath);
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            return LoadGuestList();
        }

        public bool CheckIfGuestExists(int key)
        {
            return LoadGuestList().Exists(s => key == s.GuestKey);
        }

        public bool CheckIfGuestExists(string username)
        {
            var list = (from guestUser in userRoot.Elements("users").Elements("guest")
                        select new
                        {
                            Key = guestUser.Element("key").Value,
                            Username = guestUser.Element("username").Value,
                            Password = guestUser.Element("password").Value
                        }
                        ).ToList();

            return list.Exists(s => s.Username == username);
        }

        public Guest GetGuest(int key)
        {
            return LoadGuestList().Find(s => s.GuestKey == key);
        }

        public void RemoveGuest(int key)
        {
            var list = LoadGuestList();
            list.RemoveAll(s => s.GuestKey == key);
            SaveObjectList(list, guestPath);

            // User file removal
            userRoot.Elements("users").Elements("guest")
                .Where(s => int.Parse(s.Element("key").Value) == key)
                .Remove();
        }

        public string GetGuestUserName(int key)
        {
            var guestList = (from guest in userRoot.Elements("guest")
                             where int.Parse(guest.Element("key").Value) == key
                             select new
                             {
                                 Username = guest.Element("username").Value
                             }).ToList();

            if (guestList.Count > 0)
            {
                return guestList[0].Username;
            }

            return null;
        }

        public int GetGuestKey(string userName)
        {
            var guestList = (from guest in userRoot.Elements("guest")
                             where guest.Element("username").Value == userName
                             select new
                             {
                                 Key = int.Parse(guest.Element("key").Value)
                             }).ToList();

            if (guestList.Count > 0)
            {
                return guestList.First().Key;
            }

            return -1;
        }

        public bool GuestCompareToPasswordInFile(string username, string password)
        {
            var guestList = (from host in userRoot.Elements("guest")
                             where host.Element("username").Value.ToLower() == username
                             select new
                             {
                                 Password = host.Element("password").Value
                             }).ToList();

            if (guestList.Count > 0)
            {
                var passBytesStr = guestList.First().Password.Split(' ');
                byte[] passBytes = new byte[passBytesStr.Length];
                for (int i = 0; i < passBytesStr.Length; i++)
                {
                    passBytes[i] = byte.Parse(passBytesStr[i]);
                }

                using (SHA256 sha = SHA256.Create())
                {
                    return Enumerable.SequenceEqual(passBytes, sha.ComputeHash(Encoding.ASCII.GetBytes(password)));
                }

            }

            return false;
        }

        public void WriteGuestToFile(string username, string password, int key)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string passStr = "";
                foreach (var i in sha.ComputeHash(Encoding.ASCII.GetBytes(password)))
                {
                    passStr += i.ToString() + " ";
                }

                passStr = passStr.Trim();

                userRoot.Add(new XElement("guest",
                                new XElement("key", key),
                                new XElement("username", username),
                                new XElement("password", passStr)));

                userRoot.Save(usersPath);
            }
        }

        #endregion

        #region Order

        /// <summary>
        /// This function addes an order to the data's list
        /// </summary>
        /// <param name="ord">Order to be added to the data collection</param>
        public void AddOrder(Order ord)
        {
            if (ord.OrderKey == 0)
            {
                ord.OrderKey = GetOrderKey();
            }

            var list = LoadOrderList();

            if (list.Exists(s => s.OrderKey == ord.OrderKey))
            {
                DecrementOrderKey();
                throw new AlreadyExistsException(ord.OrderKey, "Order");
            }

            list.Add(ord);

            SaveOrders(list);
        }

        /// <summary>
        /// This function return if order exists in the data
        /// </summary>
        /// <param name="key">The key of the order</param>
        /// <returns>boolean, if the order exists or not</returns>
        public bool CheckIfOrderExists(int key)
        {
            var list = LoadOrderList();
            return list.Exists(s => s.OrderKey == key);
        }

        /// <summary>
        /// This function returns the orders in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of orders</returns>
        /// <returns>IEnumerable to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return LoadOrderList();
        }

        /// <summary>
        /// This function return Order according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the Order</param>
        /// <returns>The Order with the <paramref name="key"/></returns>
        public Order GetOrder(int key)
        {
            return LoadOrderList().Find(s => s.OrderKey == key);
        }

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        public void UpdateOrderStatus(int key, Enums.OrderStatus stat)
        {
            var list = LoadOrderList();
            int index = list.FindIndex(s => s.OrderKey == key);
            list[index].Status = stat;

            SaveObjectList(list, orderPath);
        }

        #endregion

        #region GuestRequest

        /// <summary>
        /// This function adds a guest request to the data's list
        /// </summary>
        /// <param name="gr">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest gr)
        {
            if (gr.GuestRequestKey == 0)
            {
                gr.GuestRequestKey = GetGuestRequestKey();
            }

            var list = LoadGuestRequestList();
            if (list.Exists(s => s.GuestRequestKey == gr.GuestRequestKey))
            {
                DecrementGuestRequestKey();
                throw new AlreadyExistsException(gr.GuestRequestKey, "GuestRequest");
            }

            list.Add(gr);

            SaveObjectList(list, guestRequestPath);

            gr.Requester.GuestRequests.Add(gr.GuestRequestKey);
            RemoveGuest(gr.Requester.GuestKey);
            AddGuest(gr.Requester);
        }

        /// <summary>
        /// This function return if guestRequest exists in the data
        /// </summary>
        /// <param name="key">The key of the guestRequest</param>
        /// <returns>boolean, if the guestRequest exists or not</returns>
        public bool CheckIfGuestRequestExists(int key)
        {
            var list = LoadGuestRequestList();
            return list.Exists(s => s.GuestRequestKey == key);
        }

        /// <summary>
        /// This function returns the guest requests in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return LoadGuestRequestList();
        }
        /// <summary>
        /// This function return GuestRequest according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the GuestRequest</param>
        /// <returns>The GuestRequest with the <paramref name="key"/></returns>
        public GuestRequest GetGuestRequest(int key)
        {
            return LoadGuestRequestList().Find(s => s.GuestRequestKey == key);
        }

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        public void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat)
        {
            var list = LoadGuestRequestList();
            int index = list.FindIndex(s => s.GuestRequestKey == key);
            list[index].Status = stat;
            SaveObjectList(list, guestRequestPath);
        }

        /// <summary>
        /// This function removes a guest request from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <exception cref="KeyNotFoundException">Thrown if no guest request in the data match the guest request with the <paramref name="key"/></exception>
        public void RemoveGuestRequest(int key)
        {
            var list = LoadGuestRequestList();
            list.RemoveAll(s => s.GuestRequestKey == key);
            SaveObjectList(list, guestRequestPath);
        }


        #endregion

        #region HostingUnit

        /// <summary>
        /// This function adds a hosting unit to the data's list
        /// </summary>
        /// <param name="hu">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit hu)
        {
            if (hu.HostingUnitKey == 0)
            {
                hu.HostingUnitKey = GetHostingUnitKey();
            }

            var list = LoadHostingUnitList();

            if (list.Exists(s => s.HostingUnitKey == hu.HostingUnitKey))
            {
                DecrementHostingUnitKey();
                throw new AlreadyExistsException(hu.HostingUnitKey, "HostingUnit");
            }

            list.Add(hu);

            SaveObjectList(list, hostingUnitPath);
        }

        /// <summary>
        /// This function return if hostingUnit exists in the data
        /// </summary>
        /// <param name="key">The key of the hostingUnit</param>
        /// <returns>boolean, if the hostingUnit exists or not</returns>
        public bool CheckIfHostingUnitExists(int key)
        {
            var list = LoadHostingUnitList();
            return list.Exists(s => s.HostingUnitKey == key);
        }

        /// <summary>
        /// This function returns the hosting units in the data
        /// </summary>
        /// <returns>IEnumerable to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            return LoadHostingUnitList();
        }

        /// <summary>
        /// This function return HostingUnit according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the HostingUnit</param>
        /// <returns>The HostingUnit with the <paramref name="key"/></returns>
        public HostingUnit GetHostingUnit(int key)
        {
            return LoadHostingUnitList().Find(s => s.HostingUnitKey == key);
        }

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            var list = LoadHostingUnitList();
            list.RemoveAll(s => s.HostingUnitKey == key);
            SaveObjectList(list, hostingUnitPath);
        }

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            var list = LoadHostingUnitList();
            list.RemoveAll(s => s.HostingUnitKey == key);
            list.Add(hu);

            SaveObjectList(list, hostingUnitPath);
        }

        #endregion

        #region Host
        public void AddHost(Host host)
        {
            if (host.HostKey == 0)
            {
                host.HostKey = GetHostKey();
            }

            var list = LoadHostList();

            if (list.Exists(s => s.HostKey == host.HostKey))
            {
                DecrementHostKey();
                throw new AlreadyExistsException(host.HostKey, "Host");
            }

            list.Add(host);

            SaveObjectList(list, hostsPath);
        }

        /// <summary>
        /// This function return if host exists in the data
        /// </summary>
        /// <param name="key">The key of the host</param>
        /// <returns>boolean, if the host exists or not</returns>
        public bool CheckIfHostExists(int key)
        {
            var list = LoadHostList();
            return list.Exists(s => s.HostKey == key);
        }

        public bool CheckIfHostExists(string username)
        {
            var list = (from guestUser in userRoot.Elements("users").Elements("host")
                        select new
                        {
                            Key = guestUser.Element("key").Value,
                            Username = guestUser.Element("username").Value,
                            Password = guestUser.Element("password").Value
                        }
                        ).ToList();

            return list.Exists(s => s.Username == username);
        }

        /// <summary>
        /// This function return all the Host 
        /// </summary>
        /// <returns><seealso cref="IEnumerable{Host}"/> to go over the list of all the Hosts</returns>
        public IEnumerable<Host> GetAllHosts()
        {
            return LoadHostList();
        }

        /// <summary>
        /// This function return the Host with the <paramref name="key"/>
        /// </summary>
        /// <param name="key">The requested <see cref="Host"/>'s KEY</param>
        /// <returns>The Host with the  <paramref name="key"/></returns>
        public Host GetHost(int key)
        {
            return LoadHostList().Find(s => s.HostKey == key);
        }

        public void WriteHostToFile(string username, string password, int hostKey)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string passStr = "";
                foreach (var i in sha.ComputeHash(Encoding.ASCII.GetBytes(password)))
                {
                    passStr += i.ToString() + " ";
                }

                passStr = passStr.Trim();

                userRoot.Add(new XElement("host",
                                new XElement("key", hostKey),
                                new XElement("username", username),
                                new XElement("password", passStr)));

                userRoot.Save(usersPath);
            }
        }

        public bool HostCompareToPasswordInFile(string username, string password)
        {
            var hostList = (from host in userRoot.Elements("host")
                            where host.Element("username").Value.ToLower() == username
                            select new
                            {
                                Password = host.Element("password").Value
                            }).ToList();

            if (hostList.Count > 0)
            {
                var passBytesStr = hostList.First().Password.Split(' ');
                byte[] passBytes = new byte[passBytesStr.Length];
                for (int i = 0; i < passBytesStr.Length; i++)
                {
                    passBytes[i] = byte.Parse(passBytesStr[i]);
                }

                using (SHA256 sha = SHA256.Create())
                {
                    return Enumerable.SequenceEqual(passBytes, sha.ComputeHash(Encoding.ASCII.GetBytes(password)));
                }

            }

            return false;
        }

        public int GetHostKey(string username)
        {
            var hostList = (from host in userRoot.Elements("host")
                            where host.Element("username").Value == username
                            select new
                            {
                                Key = int.Parse(host.Element("key").Value)
                            }
                            ).ToList();
            if (hostList.Count > 0)
            {
                return hostList.First().Key;
            }

            return -1;
        }

        public void RemoveHost(int key)
        {
            var list = LoadHostList();
            list.RemoveAll(s => s.HostKey == key);
            SaveObjectList(list, hostsPath);

            // User file removal
            userRoot.Elements("users").Elements("host")
                .Where(s => int.Parse(s.Element("key").Value) == key)
                .Remove();
        }


        #endregion

        #region BankAccount
        public void AddBankAccount(BankBranch branch)
        {
            if (branch.BankNumber == 0)
            {
                branch.BankNumber = GetBankNumber();
            }

            var list = LoadBankBranchList();

            if (list.Exists(s => s.BankNumber == branch.BankNumber))
            {
                DecrementBankNumber();
                throw new AlreadyExistsException(branch.BankNumber, "BankBranch");
            }

            list.Add(branch);

            SaveObjectList(list, bankBranchPath);
        }

        /// <summary>
        /// This function return if bankAccount exists in the data
        /// </summary>
        /// <param name="key">The key of the bankAccount</param>
        /// <returns>boolean, if the bankAccount exists or not</returns>
        public bool CheckIfBankAccountExists(int key)
        {
            var list = LoadBankBranchList();
            return list.Exists(s => s.BankNumber == key);
        }

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            return LoadBankBranchList();
        }

        /// <summary>
        /// This function return BankBranch according to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key of the BankBranch</param>
        public BankBranch GetBankBranch(int key)
        {
            return LoadBankBranchList().Find(s => s.BankNumber == key);
        }

        #endregion

        #endregion

        #region Config Values Functions

        #region Get Config Values

        public int GetGuestRequestKey()
        {
            int key = int.Parse(configRoot.Element("guestrequestkey").Value);
            IncrementGuestRequestKey();
            return key;
        }

        public int GetBankNumber()
        {
            int num = int.Parse(configRoot.Element("banknumber").Value);
            IncrementBankNumber();
            return num;
        }

        public int GetHostKey()
        {
            int key = int.Parse(configRoot.Element("hostkey").Value);
            IncrementHostKey();
            return key;
        }

        public int GetHostingUnitKey()
        {
            int key = int.Parse(configRoot.Element("hostingunitkey").Value);
            IncrementHostingUnitKey();
            return key;
        }

        public int GetOrderKey()
        {
            int key = int.Parse(configRoot.Element("orderkey").Value);
            IncrementOrderKey();
            return key;
        }

        public float GetCommission()
        {
            return float.Parse(configRoot.Element("commission").Value);
        }

        public int GetNumberOfDaysUntilExpired()
        {
            return int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
        }

        public int GetGuestKey()
        {
            int key = int.Parse(configRoot.Element("guestkey").Value);
            IncrementGuestKey();
            return key;
        }

        #endregion

        #region Set Config Values

        private void IncrementGuestRequestKey()
        {
            configRoot.Element("guestrequestkey").Value = (int.Parse(configRoot.Element("guestrequestkey").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        private void IncrementBankNumber()
        {
            configRoot.Element("banknumber").Value = (int.Parse(configRoot.Element("banknumber").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        private void IncrementHostKey()
        {
            configRoot.Element("hostkey").Value = (int.Parse(configRoot.Element("hostkey").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        private void IncrementHostingUnitKey()
        {
            configRoot.Element("hostingunitkey").Value = (int.Parse(configRoot.Element("hostingunitkey").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        private void IncrementOrderKey()
        {
            configRoot.Element("orderkey").Value = (int.Parse(configRoot.Element("orderkey").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        public void SetCommission(float commission)
        {
            configRoot.Element("commission").Value = commission.ToString();
            configRoot.Save(configPath);
        }

        public void SetNumberOfDaysUntilExpired(int val)
        {
            configRoot.Element("numberofdaysuntilexpired").Value = val.ToString();
            configRoot.Save(configPath);
        }

        private void IncrementGuestKey()
        {
            configRoot.Element("guestkey").Value = (int.Parse(configRoot.Element("guestkey").Value) + 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementGuestRequestKey()
        {
            configRoot.Element("guestrequestkey").Value = (int.Parse(configRoot.Element("guestrequestkey").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementBankNumber()
        {
            configRoot.Element("banknumber").Value = (int.Parse(configRoot.Element("banknumber").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementHostKey()
        {
            configRoot.Element("hostkey").Value = (int.Parse(configRoot.Element("hostkey").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementHostingUnitKey()
        {
            configRoot.Element("hostingunitkey").Value = (int.Parse(configRoot.Element("hostingunitkey").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementOrderKey()
        {
            configRoot.Element("orderkey").Value = (int.Parse(configRoot.Element("orderkey").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        private void DecrementGuestKey()
        {
            configRoot.Element("guestkey").Value = (int.Parse(configRoot.Element("guestkey").Value) - 1).ToString();
            configRoot.Save(configPath);
        }

        #endregion

        #endregion

        public bool AdminCompareToPasswordInFile(string username, string password)
        {
            if (username != "admin")
            {
                return false;
            }

            using (SHA256 sha = SHA256.Create())
            {
                var passBytes = new byte[32] { 127, 115, 144, 82, 251, 73, 59, 118, 196, 87, 146, 6, 61, 104, 36, 115, 243, 138, 164, 19, 60, 126, 138, 62, 55, 174, 114, 193, 107, 74, 177, 237 };
                return Enumerable.SequenceEqual(passBytes, sha.ComputeHash(Encoding.ASCII.GetBytes(password)));
            }
        }
    }
}
