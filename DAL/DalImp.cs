using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using BE;
using DS;
using Exceptions;

namespace DAL
{
    /// <summary>
    /// Implementation of the DAL.
    /// Implemented using lists for the data types
    /// See <see cref="IDAL"/> for the DAL interface
    /// </summary>
    public class DalImp : IDAL
    {

        #region Paths and Roots

        private const string configPath = @"..\..\..\..\config.xml";
        private XElement configRoot = null;

        #endregion

        #region Singletory These parts are what make the class a singletory

        /// <summary>
        /// Private constructor for DalImp so that another instance can't be created
        /// </summary>
        private DalImp()
        {
            SetupConfigFile();
        }

        protected static DalImp instance = null;

        /// <summary>
        /// This is the factory method of DalImp
        /// </summary>
        /// <returns>The <see cref="instance"/> of the singleton factory (singletory)</returns>
        public static IDAL GetDAL()
        {
            if (instance == null)
            {
                instance = new DalImp();
                return instance;
            }
            return instance;
        }

        #endregion

        #region GuestRequest These functions perform actions on GuestRequests

        #region AddGuestRequest This function adds a guest request

        /// <summary>
        /// This function adds a <paramref name="request"/> to the data's list
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="request">GuestRequest to be added to the data collection</param>
        public void AddGuestRequest(GuestRequest request)
        {
            if (0 == request.GuestRequestKey)
            {
                request.GuestRequestKey = GetGuestRequestKey();
            }


            var linq = from item in DataSource.guestRequestsList
                       where item.GuestRequestKey == request.GuestRequestKey
                       select new { Num = item.GuestRequestKey };


            if (linq.Count() == 0)
            {
                DataSource.guestRequestsList.Add(request.Clone());
            }

            else
            {
                throw new AlreadyExistsException(request.GuestRequestKey, "GuestRequest");
            }
        }

        #endregion

        #region GetAllGuestRequests This function returns the guest requests

        /// <summary>
        /// This function returns the guest requests in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no guest requests</exception>
        /// <returns>IEnumerable to go over the list of guest requests</returns>
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            var v = from item in DataSource.guestRequestsList.Clone()
                    select item;
            if (v.Count() == 0)
            {
                throw new NoItemsException("GuestRequest");
            }

            return v;
        }

        #endregion

        #region RemoveGuestRequest This function removes a guest request

        /// <summary>
        /// This function removes a guest request from the data
        /// </summary>
        /// Important Note: It will not compare all fields. It will only compare the key 
        /// <exception cref="KeyNotFoundException">Thrown if no guest request in the data match the guest request with the <paramref name="key"/></exception>
        public void RemoveGuestRequest(int key)
        {
            var res = from item in DataSource.guestRequestsList.Clone()
                      let temp = key
                      where temp == item.GuestRequestKey
                      select item;
            if (res.Count() == 0)
            {
                throw new KeyNotFoundException("No guest request with key specified");
            }

            foreach (var it in res)
            {
                int index = DataSource.guestRequestsList.FindIndex(x => x.GuestRequestKey == it.GuestRequestKey);
                DataSource.guestRequestsList.RemoveAt(index);
            }
        }
        #endregion

        #region UpdateGuestRequestStatus This function updates a guest request

        /// <summary>
        /// This function updates a guest request of key <paramref name="key"/> to the status <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">Key of guest request to update</param>
        /// <param name="stat">Status to update guest request to</param>
        public void UpdateGuestRequestStatus(int key, Enums.RequestStatus stat)
        {
            int i = DataSource.guestRequestsList.FindIndex(t => t.GuestRequestKey == key);

            if (-1 == i)
            {
                throw new KeyNotFoundException("No guest request with key specified");
            }

            DataSource.guestRequestsList[i].Status = stat;
        }

        #endregion

        #region GetGuestRequest This function return guestRequset

        /// <summary>
        /// This function return GuestRequest according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the GuestRequest</param>
        /// <returns>The GuestRequest with the <paramref name="key"/></returns>
        public GuestRequest GetGuestRequest(int key)
        {
            int i = DataSource.guestRequestsList.FindIndex(t => t.GuestRequestKey == key);

            if (-1 == i)
            {
                throw new KeyNotFoundException("No guest request with key specified");
            }
            return DataSource.guestRequestsList[i].Clone();
        }

        #endregion

        #endregion

        #region HostingUnit These functions perform actions on HostingUnits

        #region AddHostingUnit This function adds a hosting unit

        /// <summary>
        /// This function adds a <paramref name="unit"/> to the data's list.
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="unit">HostingUnit to be added to the data collection</param>
        public void AddHostingUnit(HostingUnit unit)
        {
            if (unit.HostingUnitKey == 0)
            {
                unit.HostingUnitKey = GetHostingUnitKey();
            }


            var linq = from item in DataSource.hostingUnitsList
                       where item.HostingUnitKey == unit.HostingUnitKey
                       select new { Num = item.HostingUnitKey };

            if (linq.Count() == 0)
            {
                DataSource.hostingUnitsList.Add(unit.Clone()); // Otherwise it might be changed elsewhere
            }

            else
            {
                throw new AlreadyExistsException(unit.HostingUnitKey, "HostingUnit");
            }
        }

        #endregion

        #region GetAllHostingUnits This function returns all hosting units

        /// <summary>
        /// This function returns the hosting units in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown if there are no items in the hosting units list</exception>
        /// <returns>IEnumerable to go over the list of hosting units</returns>
        public IEnumerable<HostingUnit> GetAllHostingUnits()
        {
            var v = from item in DataSource.hostingUnitsList.Clone()
                    select item;

            if (v.Count() == 0)
            {
                throw new NoItemsException("HostingUnit");
            }

            return v;
        }

        #endregion

        #region RemoveHostingUnit This function removes a hosting unit

        /// <summary>
        /// This function removes a hosting unit from the data
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if no hosting unit with a matching <paramref name="key"/> is found</exception>
        /// <param name="key">Key to remove the hosting unit of</param>
        public void RemoveHostingUnit(int key)
        {
            var res = from item in DataSource.hostingUnitsList.Clone()
                      let temp = key
                      where temp == item.HostingUnitKey
                      select item;
            if (res.Count() == 0)
            {
                throw new KeyNotFoundException("No hosting unit with key specified");
            }

            foreach (var it in res)
            {
                int index = DataSource.hostingUnitsList.FindIndex(x => x.HostingUnitKey == it.HostingUnitKey);
                DataSource.hostingUnitsList.RemoveAt(index);
            }
        }

        #endregion

        #region UpdateHostingUnit This function updates a hosting unit

        /// <summary>
        /// This function updates a hosting unit
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when hosting unit with <paramref name="key"/> is not found</exception>
        /// <param name="hu">Hosting unit to update to</param>
        /// <param name="key">Key of hosting unit to update</param>
        public void UpdateHostingUnit(HostingUnit hu, int key)
        {
            int index = DataSource.hostingUnitsList.FindIndex(new Predicate<HostingUnit>(x => x.HostingUnitKey == key));

            if (-1 == index)
            {
                throw new KeyNotFoundException("No hosting unit with key specified found");
            }

            DataSource.hostingUnitsList[index] = hu.Clone();
        }

        #endregion

        #region GetHostingUnit This function return HostingUnit

        /// <summary>
        /// This function return HostingUnit according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the HostingUnit</param>
        /// <returns>The HostingUnit with the <paramref name="key"/></returns>
        public HostingUnit GetHostingUnit(int key)
        {
            int i = DataSource.hostingUnitsList.FindIndex(t => t.HostingUnitKey == key);

            if (-1 == i)
            {
                throw new KeyNotFoundException("No hosting unit with key specified");
            }
            return DataSource.hostingUnitsList[i].Clone();
        }

        #endregion

        #endregion

        #region Order These functions perform actions on Orders

        #region AddOrder This function adds and order

        /// <summary>
        /// This function addes an <paramref name="order"/> to the data's list
        /// </summary>
        /// <exception cref="AlreadyExistsException">Thrown when the key is already in the list</exception>
        /// <param name="order">Order to be added to the data collection</param>
        public void AddOrder(Order order)
        {

            if (DataSource.ordersList.Exists(x => x.GuestRequestKey == order.GuestRequestKey))
            {
                throw new AlreadyExistsException(order.GuestRequestKey, "Order's GuestRequest");
            }

            if (0 == order.OrderKey)
            {
                order.OrderKey = GetOrderKey();
            }

            var linq = from item in DataSource.ordersList
                       where item.OrderKey == order.OrderKey
                       select new { Num = item.OrderKey };
            if (linq.Count() == 0)
            {
                DataSource.ordersList.Add(order.Clone());
            }

            else
            {
                throw new AlreadyExistsException(order.OrderKey, "Order");
            }
        }

        #endregion

        #region GetAllOrders This function returns all the orders

        /// <summary>
        /// This function returns the orders in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no orders in the list</exception>
        /// <returns>IEnumerable to go over the list of orders</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            var v = from item in DataSource.ordersList.Clone()
                    select item;
            if (v.Count() == 0)
            {
                throw new NoItemsException("Order");
            }

            return v;
        }

        #endregion

        #region UpdateOrderStatus This function updates an order

        /// <summary>
        /// This function updates an order with a key of <paramref name="key"/> to a status of <paramref name="stat"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when an order with the specified key is not found</exception>
        /// <param name="key">Key of Order to update the status of</param>
        /// <param name="stat">Status to update Order status to</param>
        public void UpdateOrderStatus(int key, Enums.OrderStatus stat)
        {
            int index = DataSource.ordersList.FindIndex(new Predicate<Order>(x => x.OrderKey == key));

            if (-1 == index)
            {
                throw new KeyNotFoundException("There is no order with the key specified");
            }

            DataSource.ordersList[index].Status = stat;
        }

        #endregion

        #region GetOrder This function return Order

        /// <summary>
        /// This function return Order according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the Order</param>
        /// <returns>The Order with the <paramref name="key"/></returns>
        public Order GetOrder(int key)
        {
            int i = DataSource.ordersList.FindIndex(t => t.OrderKey == key);

            if (-1 == i)
            {
                throw new KeyNotFoundException("No order with key specified");
            }
            return DataSource.ordersList[i].Clone();
        }

        #endregion

        #endregion

        #region BankAccount These functions perform actions on BankAccounts

        #region GetAllBankAccounts This function returns all the bank accounts

        /// <summary>
        /// This function returns the bank accounts in the data
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no bank accounts in the list</exception>
        /// <returns>IEnumerable to go over the list of bank accounts</returns>
        public IEnumerable<BankBranch> GetAllBankAccounts()
        {
            List<BankBranch> ret = new List<BankBranch>
            {
                new BankBranch
                {
                    BankAccountNumber = 10000,
                    BankName = "Mizrachi",
                    BankNumber = 100,
                    BranchAddress = "31 Maple St.",
                    BranchCity = "Police",
                    BranchNumber = 1221
                },
                new BankBranch
                {
                    BankAccountNumber = 12125,
                    BankName = "Discount",
                    BankNumber = 326,
                    BranchAddress = "5 Daisy Ave.",
                    BranchCity = "New York City",
                    BranchNumber = 432
                },
                new BankBranch
                {
                    BankAccountNumber = 264162,
                    BankName = "Chase",
                    BankNumber = 241,
                    BranchAddress = "5 North Marshall St.",
                    BranchCity = "Far Rockaway",
                    BranchNumber = 3235
                },
                new BankBranch
                {
                    BankAccountNumber = 254294,
                    BankName = "Amex",
                    BankNumber = 3846,
                    BranchAddress = "8675 Tarkiln Hill Ave.",
                    BranchCity = "Reading",
                    BranchNumber = 36495
                },
                new BankBranch
                {
                    BankAccountNumber = 94646,
                    BankName = "Pepper",
                    BankNumber = 6461,
                    BranchAddress = "606 North Marshall Drive",
                    BranchCity = "North Ridgeville",
                    BranchNumber = 4154945
                }
            };

            var v = from item in ret.Clone()
                    select item;

            if (v.Count() == 0)
            {
                throw new NoItemsException("BankAccount");
            }

            return v;
        }

        #endregion

        #region GetBankBranch This function return BankAccount

        /// <summary>
        /// This function return BankBranch according to <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The key of the BankBranch</param>
        public BankBranch GetBankBranch(int key)
        {
            var bankList = GetAllBankAccounts();
            if (!bankList.Any((bb => bb.BankNumber == key)))
                throw new KeyNotFoundException("No BankBranch with key specified");
            return bankList.Where((bb => bb.BankNumber == key)).First();
        }

        #endregion

        #endregion

        #region Host These function perform actions on Host

        #region GetAllHosts This function return all the Hosts

        /// <summary>
        /// This function return all the Host 
        /// </summary>
        /// <exception cref="NoItemsException">Thrown when there are no Host</exception>
        /// <returns><seealso cref="IEnumerable{Host}"/> to go over the list of all the Hosts</returns>
        public IEnumerable<Host> GetAllHosts()
        {
            var listOfAllHosts = from item in DataSource.hostingUnitsList
                                 select item.Owner;
            if (listOfAllHosts.Count() == 0)
                throw new NoItemsException("Host");
            return listOfAllHosts.Distinct(new HostComparer()).ToList().Clone();
        }

        #endregion

        #region GetHost This function return host

        /// <summary>
        /// This function return the Host with the <paramref name="key"/>
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if object with key of <paramref name="key"/> does not exist</exception>
        /// <param name="key">The requested <see cref="Host"/>'s KEY</param>
        /// <returns>The Host with the  <paramref name="key"/></returns>
        public Host GetHost(int key)
        {
            try
            {
                var host = GetAllHosts().Where((x => x.HostKey == key));
                if (host.Count() == 0)
                    throw new KeyNotFoundException("No Host with key specified");
                return host.First().Clone();
            }
            catch (NoItemsException)
            {
                throw new KeyNotFoundException("No Host with key specified");
            }
        }

        #endregion

        #endregion

        #region IfExists These function check if object exsits in the data

        #region CheckIfGuestRequestExists This function check if guestRequest exists in the data

        /// <summary>
        /// This function return if guestRequest exists in the data
        /// </summary>
        /// <param name="key">The key of the guestRequest</param>
        /// <returns>boolean, if the guestRequest exists or not</returns>
        public bool CheckIfGuestRequestExists(int key)
        {
            return DataSource.guestRequestsList.Exists((gr => gr.GuestRequestKey == key));
        }

        #endregion

        #region CheckIfHostingUnitExists This function check if hostingUnit exists in the data

        /// <summary>
        /// This function return if hostingUnit exists in the data
        /// </summary>
        /// <param name="key">The key of the hostingUnit</param>
        /// <returns>boolean, if the hostingUnit exists or not</returns>
        public bool CheckIfHostingUnitExists(int key)
        {
            return DataSource.hostingUnitsList.Exists((hu => hu.HostingUnitKey == key));
        }

        #endregion

        #region CheckIfOrderExists This function check if order exists in the data

        /// <summary>
        /// This function return if order exists in the data
        /// </summary>
        /// <param name="key">The key of the order</param>
        /// <returns>boolean, if the order exists or not</returns>
        public bool CheckIfOrderExists(int key)
        {
            return DataSource.ordersList.Exists((o => o.OrderKey == key));
        }

        #endregion

        #region CheckIfBankAccountExists This function check if bankAccount exists in the data

        /// <summary>
        /// This function return if bankAccount exists in the data
        /// </summary>
        /// <param name="key">The key of the bankAccount</param>
        /// <returns>boolean, if the bankAccount exists or not</returns>
        public bool CheckIfBankAccountExists(int key)
        {
            return GetAllBankAccounts().Any((ba => ba.BankAccountNumber == key));
        }

        #endregion

        #region CheckIfHostExists This function check if host exists in the data

        /// <summary>
        /// This function return if host exists in the data
        /// </summary>
        /// <param name="key">The key of the host</param>
        /// <returns>boolean, if the host exists or not</returns>
        public bool CheckIfHostExists(int key)
        {
            return GetAllHosts().Any((h => h.HostKey == key));
        }

        #endregion

        #endregion

        #region Config Values Functions

        private void SetupConfigFile()
        {
            if (!File.Exists(configPath))
            {
                configRoot = new XElement("config",
                             new XElement("guestrequestkey", 1),
                             new XElement("banknumber", 1),
                             new XElement("hostkey", 1),
                             new XElement("hostingunitkey", 1),
                             new XElement("orderkey", 1),
                             new XElement("commission", 1),
                             new XElement("numberofdaysuntilexpired", 1),
                             new XElement("guestkey", 1));

                configRoot.Save(configPath);
            }
        }

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
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey + 1),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        private void IncrementBankNumber()
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey),
                             new XElement("banknumber", banknumber + 1),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        private void IncrementHostKey()
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey + 1),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        private void IncrementHostingUnitKey()
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey + 1),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        private void IncrementOrderKey()
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey + 1),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        public void SetCommission(float commission)
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey + 1),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", commission),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        public void SetNumberOfDaysUntilExpired(int val)
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey + 1),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", val),
                             new XElement("guestkey", guestkey));

            configRoot.Save(configPath);
        }

        private void IncrementGuestKey()
        {
            int guestrequestkey = int.Parse(configRoot.Element("guestrequestkey").Value);
            int banknumber = int.Parse(configRoot.Element("banknumber").Value);
            int hostkey = int.Parse(configRoot.Element("hostkey").Value);
            int hostingunitkey = int.Parse(configRoot.Element("hostingunitkey").Value);
            int orderkey = int.Parse(configRoot.Element("orderkey").Value);
            float commission = float.Parse(configRoot.Element("commission").Value);
            int numberofdaysuntilexpired = int.Parse(configRoot.Element("numberofdaysuntilexpired").Value);
            int guestkey = int.Parse(configRoot.Element("guestkey").Value);

            using (StreamWriter sw = new StreamWriter(configPath, false)) { } // Overwrite the file
            configRoot = new XElement("config",
                             new XElement("guestrequestkey", guestrequestkey),
                             new XElement("banknumber", banknumber),
                             new XElement("hostkey", hostkey),
                             new XElement("hostingunitkey", hostingunitkey),
                             new XElement("orderkey", orderkey),
                             new XElement("commission", commission),
                             new XElement("numberofdaysuntilexpired", numberofdaysuntilexpired),
                             new XElement("guestkey", guestkey + 1));

            configRoot.Save(configPath);
        }

        public void AddGuest(Guest g)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfGuestExists(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public Guest GetGuest(int key)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuest(int key)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfGuestExists(string username)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfHostExists(string username)
        {
            throw new NotImplementedException();
        }

        public int GetGuestKey(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetGuestUserName(int key)
        {
            throw new NotImplementedException();
        }

        public void WriteGuestToFile(string username, string password, int key)
        {
            throw new NotImplementedException();
        }

        public void WriteHostToFile(string username, string password, int hostKey)
        {
            throw new NotImplementedException();
        }

        public void AddHost(Host host)
        {
            throw new NotImplementedException();
        }

        public bool HostCompareToPasswordInFile(string username, string password)
        {
            throw new NotImplementedException();
        }

        public int GetHostKey(string username)
        {
            throw new NotImplementedException();
        }

        public bool GuestCompareToPasswordInFile(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool AdminCompareToPasswordInFile(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void RemoveHost(int key)
        {
            throw new NotImplementedException();
        }

        public void SetCommission(float? value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}

