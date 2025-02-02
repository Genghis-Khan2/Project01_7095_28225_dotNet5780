﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace FR
{
    /// <summary>
    /// This class represents a file reader and writer (FR = File Reader)
    /// It's used to store information about accounts
    /// </summary>
    public class FR_Imp
    {

        // Structure Of A Host Line:
        // Username Password bytes


        #region Singletory Factory Methods
        /// <summary>
        /// Private constructor so no instances can be created
        /// </summary>
        private FR_Imp() { }

        /// <summary>
        /// The only instance that can exist
        /// </summary>
        static public FR_Imp instance = null;

        /// <summary>
        /// Get the only FR instance
        /// </summary>
        /// <returns>The only FR instance</returns>
        static public FR_Imp GetFR()
        {
            if (instance == null)
            {
                instance = new FR_Imp();
            }

            return instance;
        }
        #endregion

        #region Paths

        /// <summary>
        /// The path where the guest account information is stored
        /// </summary>
        private static readonly string guestPath = @"..\..\..\..\guest.data";

        /// <summary>
        /// The path where the host account information is stored
        /// </summary>
        private static readonly string hostPath = @"..\..\..\..\host.data";

        //private static readonly string confPath = @"..\..\..\..\conf.data";

        private static readonly string tmpPath = @"..\..\..\..\tmp.data";

        #endregion

        #region Compare Functions

        /// <summary>
        /// This function checks if the <paramref name="username"/> and <paramref name="password"/>
        /// exist in the guest account file
        /// </summary>
        /// <param name="username">The username to search for in the file</param>
        /// <param name="password">The password to search the has for in the file</param>
        /// <returns>If the <paramref name="username"/> and <paramref name="password"/>
        /// exist in the guest account file</returns>
        public bool GuestCompareToPasswordInFile(string username, string password)
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        string bytes = line.Substring(line.IndexOf(" ") + 1);
                        var byteArray = bytes.Split(' ');
                        byte[] actualBytes = new byte[32];

                        for (int i = 0; i < 32; i++)
                        {
                            actualBytes[i] = byte.Parse(byteArray[i]);
                        }

                        using (SHA256 sha = SHA256.Create())
                        {
                            var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));

                            return hash.SequenceEqual(actualBytes);
                        }
                    }

                    line = sr.ReadLine();
                }


            }

            return false;
        }

        /// <summary>
        /// This function checks if the <paramref name="username"/> and <paramref name="password"/>
        /// exist in the host account file
        /// </summary>
        /// <param name="username">The username to search for in the file</param>
        /// <param name="password">The password to search the has for in the file</param>
        /// <returns>If the <paramref name="username"/> and <paramref name="password"/>
        /// exist in the host account file</returns>
        public bool HostCompareToPasswordInFile(string username, string password)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        string bytes = line.Substring(line.IndexOf(" ") + 1);
                        var byteArray = bytes.Split(' ');
                        byte[] actualBytes = new byte[32];

                        for (int i = 0; i < 32; i++)
                        {
                            actualBytes[i] = byte.Parse(byteArray[i]);
                        }

                        using (SHA256 sha = SHA256.Create())
                        {
                            var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));

                            return hash.SequenceEqual(actualBytes);
                        }
                    }

                    line = sr.ReadLine();

                }

                return false;
            }
        }

        /// <summary>
        /// This function checks if the <paramref name="username"/> and <paramref name="password"/>
        /// are the correct admin username and password
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <param name="password">The password to check</param>
        /// <returns>If the <paramref name="username"/> and <paramref name="password"/>
        /// are the correct admin credentials</returns>
        public bool AdminCompareToPasswordInFile(string username, string password)
        {
            if (username.ToLower() != "admin")
            {
                return false;
            }

            using (SHA256 sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
                var pass = new byte[] { 127, 115, 144, 82, 251, 73, 59, 118, 196, 87, 146, 6, 61, 104, 36, 115, 243, 138, 164, 19, 60, 126, 138, 62, 55, 174, 114, 193, 107, 74, 177, 237 };
                return hash.SequenceEqual(pass);
            }
        }

        #endregion

        #region Add Line

        /// <summary>
        /// Add a guest account in the proper format
        /// </summary>
        /// <param name="username">Username of the account to be added</param>
        /// <param name="password">Password of the account to be added</param>
        /// <param name="guestKey">Key of the guest to be added</param>
        public void WriteGuestToFile(string username, string password, int guestKey)
        {
            using (SHA256 sha = SHA256.Create())
            {
                using (StreamWriter sw = new StreamWriter(guestPath, true))
                {
                    sw.Write(username.ToLower() + " ");
                    var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
                    foreach (var i in hash)
                    {
                        sw.Write(i.ToString() + " ");
                    }

                    sw.WriteLine(" " + guestKey.ToString());
                }
            }
        }

        /// <summary>
        /// Add a host account in the proper format
        /// </summary>
        /// <param name="username">Username of the account to be added</param>
        /// <param name="password">Password of the account to be added</param>
        /// <param name="hostKey">Key of the host to be added</param>
        public void WriteHostToFile(string username, string password, int hostKey, string mail, string firstName, string lastName, string phoneNumber)
        {
            using (SHA256 sha = SHA256.Create())
            {
                using (StreamWriter sw = new StreamWriter(hostPath, true))
                {
                    sw.Write(username.ToLower() + " ");
                    var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
                    foreach (var i in hash)
                    {
                        sw.Write(i.ToString() + " ");
                    }

                    sw.WriteLine(hostKey.ToString() + " " + mail + " " + firstName + " " + lastName + " " + phoneNumber);
                }
            }
        }

        #endregion

        #region Get Key

        public int GetGuestKey(string username)
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return int.Parse(strings[34]);
                    }

                    line = sr.ReadLine();

                }

            }

            return -1;
        }

        public int GetHostKey(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return int.Parse(strings[32]);
                    }

                    line = sr.ReadLine();
                }

            }

            return -1;
        }

        #endregion

        #region Get Username

        public string GetHostUserNameFromKey(int key)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (int.Parse(line.Split(' ')[32]) == key)
                    {
                        return line.Split(' ')[0];
                    }
                }

                return null;
            }
        }

        #endregion

        #region Check If Username Exists

        public bool CheckIfGuestExists(string username)
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        return true;
                    }
                    line = sr.ReadLine();
                }
            }

            return false;
        }

        public bool CheckIfHostExists(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        return true;
                    }

                    line = sr.ReadLine();

                }
            }

            return false;
        }

        #endregion

        //#region Get Config Value

        //public int GetGuestRequestKey()
        //{
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        return int.Parse(sr.ReadLine());
        //    }
        //}

        //public int GetHostKey()
        //{
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        sr.ReadLine();
        //        return int.Parse(sr.ReadLine());
        //    }
        //}

        //public int GetHostingUnitKey()
        //{
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        return int.Parse(sr.ReadLine());
        //    }
        //}

        //public int GetOrderKey()
        //{
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        return int.Parse(sr.ReadLine());
        //    }
        //}

        //public int GetGuestKey()
        //{
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        sr.ReadLine();
        //        return int.Parse(sr.ReadLine());
        //    }
        //}

        //#endregion

        //#region Set Config Value

        //public void SetGuestRequestKey(int key)
        //{
        //    int GuestRequestKey;
        //    int HostKey;
        //    int HostingUnitKey;
        //    int OrderKey;
        //    int GuestKey;
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        GuestRequestKey = int.Parse(sr.ReadLine());
        //        HostKey = int.Parse(sr.ReadLine());
        //        HostingUnitKey = int.Parse(sr.ReadLine());
        //        OrderKey = int.Parse(sr.ReadLine());
        //        GuestKey = int.Parse(sr.ReadLine());
        //    }

        //    using (StreamWriter sw = new StreamWriter(confPath, false))
        //    {
        //        sw.WriteLine(key);
        //        sw.WriteLine(HostKey);
        //        sw.WriteLine(HostingUnitKey);
        //        sw.WriteLine(OrderKey);
        //        sw.WriteLine(GuestKey);
        //    }

        //}

        //public void SetHostKey(int key)
        //{
        //    int GuestRequestKey;
        //    int HostKey;
        //    int HostingUnitKey;
        //    int OrderKey;
        //    int GuestKey;
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        GuestRequestKey = int.Parse(sr.ReadLine());
        //        HostKey = int.Parse(sr.ReadLine());
        //        HostingUnitKey = int.Parse(sr.ReadLine());
        //        OrderKey = int.Parse(sr.ReadLine());
        //        GuestKey = int.Parse(sr.ReadLine());
        //    }

        //    using (StreamWriter sw = new StreamWriter(confPath, false))
        //    {
        //        sw.WriteLine(GuestRequestKey);
        //        sw.WriteLine(key);
        //        sw.WriteLine(HostKey);
        //        sw.WriteLine(HostingUnitKey);
        //        sw.WriteLine(OrderKey);
        //        sw.WriteLine(GuestKey);
        //    }

        //}

        //public void SetHostingUnitKey(int key)
        //{
        //    int GuestRequestKey;
        //    int HostKey;
        //    int HostingUnitKey;
        //    int OrderKey;
        //    int GuestKey;
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        GuestRequestKey = int.Parse(sr.ReadLine());
        //        HostKey = int.Parse(sr.ReadLine());
        //        HostingUnitKey = int.Parse(sr.ReadLine());
        //        OrderKey = int.Parse(sr.ReadLine());
        //        GuestKey = int.Parse(sr.ReadLine());
        //    }

        //    using (StreamWriter sw = new StreamWriter(confPath, false))
        //    {
        //        sw.WriteLine(GuestRequestKey);
        //        sw.WriteLine(HostKey);
        //        sw.WriteLine(key);
        //        sw.WriteLine(OrderKey);
        //        sw.WriteLine(GuestKey);
        //    }

        //}

        //public void SetOrderKey(int key)
        //{
        //    int GuestRequestKey;
        //    int HostKey;
        //    int HostingUnitKey;
        //    int OrderKey;
        //    int GuestKey;
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        GuestRequestKey = int.Parse(sr.ReadLine());
        //        HostKey = int.Parse(sr.ReadLine());
        //        HostingUnitKey = int.Parse(sr.ReadLine());
        //        OrderKey = int.Parse(sr.ReadLine());
        //        GuestKey = int.Parse(sr.ReadLine());
        //    }

        //    using (StreamWriter sw = new StreamWriter(confPath, false))
        //    {
        //        sw.WriteLine(GuestRequestKey);
        //        sw.WriteLine(HostKey);
        //        sw.WriteLine(HostingUnitKey);
        //        sw.WriteLine(key);
        //        sw.WriteLine(GuestKey);
        //    }

        //}


        //public void SetGuestKey(int key)
        //{
        //    int GuestRequestKey;
        //    int HostKey;
        //    int HostingUnitKey;
        //    int OrderKey;
        //    int GuestKey;
        //    using (StreamReader sr = new StreamReader(confPath))
        //    {
        //        GuestRequestKey = int.Parse(sr.ReadLine());
        //        HostKey = int.Parse(sr.ReadLine());
        //        HostingUnitKey = int.Parse(sr.ReadLine());
        //        OrderKey = int.Parse(sr.ReadLine());
        //        GuestKey = int.Parse(sr.ReadLine());
        //    }

        //    using (StreamWriter sw = new StreamWriter(confPath, false))
        //    {
        //        sw.WriteLine(GuestRequestKey);
        //        sw.WriteLine(HostKey);
        //        sw.WriteLine(HostingUnitKey);
        //        sw.WriteLine(OrderKey);
        //        sw.WriteLine(key);
        //    }

        //}

        //#endregion

        #region Get Info

        public string GetHostMailAddress(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return strings[34];
                    }

                    line = sr.ReadLine();
                }

            }

            return null;
        }

        public string GetHostPrivateName(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return strings[35];
                    }

                    line = sr.ReadLine();
                }

            }

            return null;
        }

        public string GetHostFamilyName(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return strings[36];
                    }

                    line = sr.ReadLine();
                }

            }

            return null;
        }

        public string GetHostPhoneNumber(string username)
        {
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.StartsWith(username.ToLower()))
                    {
                        var strings = line.Split(' ');
                        return strings[37];
                    }

                    line = sr.ReadLine();
                }

            }

            return null;
        }

        #endregion

        #region Remove Functions

        public void RemoveHostFromFile(string username)
        {
            using (StreamWriter sw = new StreamWriter(tmpPath, false)) { }
            using (StreamReader sr = new StreamReader(hostPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username))
                    {
                        line = sr.ReadLine();
                        continue;
                    }

                    using (StreamWriter sw = new StreamWriter(tmpPath, false))
                    {
                        sw.WriteLine(line);
                    }

                    line = sr.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(hostPath, false)) { }
            using (StreamReader sr = new StreamReader(tmpPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    using (StreamWriter sw = new StreamWriter(hostPath, false))
                    {
                        sw.WriteLine(line);
                    }

                    line = sr.ReadLine();
                }
            }
        }

        public void RemoveGuestFromFile(string username)
        {
            using (StreamWriter sw = new StreamWriter(tmpPath, false)) { }
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.StartsWith(username))
                    {
                        line = sr.ReadLine();
                        continue;
                    }

                    using (StreamWriter sw = new StreamWriter(tmpPath, false))
                    {
                        sw.WriteLine(line);
                    }

                    line = sr.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(guestPath, false)) { }
            using (StreamReader sr = new StreamReader(tmpPath))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    using (StreamWriter sw = new StreamWriter(guestPath, false))
                    {
                        sw.WriteLine(line);
                    }

                    line = sr.ReadLine();
                }
            }
        }

        #endregion

        #region Return List of Users

        public List<string> GetListOfGuestNames()
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                List<string> list = new List<string>();
                string line = sr.ReadLine();
                while (line != null)
                {
                    list.Add(line.Split(' ')[0]);
                    line = sr.ReadLine();
                }

                return list;
            }
        }


        #endregion

    }
}