using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace FR
{
    public class FR_Imp
    {
        #region Singletory Factory Methods
        private FR_Imp() { }

        static public FR_Imp instance = null;

        static public FR_Imp getFR()
        {
            if (instance == null)
            {
                instance = new FR_Imp();
            }

            return instance;
        }
        #endregion

        #region Paths

        private static readonly string guestPath = @"guest.data";
        private static readonly string hostPath = @"host.data";

        #endregion

        #region Compare Functions

        public bool GuestCompareToPasswordInFile(string username, string password)
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();
                if (line.Contains(username))
                {
                    string passwordHash = line.Substring(line.IndexOf(' '));
                    using (SHA256 sha = SHA256.Create())
                    {
                        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                        if (hash == Encoding.UTF8.GetBytes(passwordHash))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool HostCompareToPasswordInFile(string username, string password)
        {
            using (StreamReader sr = new StreamReader(guestPath))
            {
                string line = sr.ReadLine();
                if (line.Contains(username))
                {
                    string passwordHash = line.Substring(line.IndexOf(' '));
                    using (SHA256 sha = SHA256.Create())
                    {
                        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                        if (hash == Encoding.UTF8.GetBytes(passwordHash))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool AdminCompareToPasswordInFile(string username, string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var pass = new byte[] { 127, 115, 144, 82, 251, 73, 59, 118, 196, 87, 146, 6, 61, 104, 36, 115, 243, 138, 164, 19, 60, 126, 138, 62, 55, 174, 114, 193, 107, 74, 177, 237 };
                if (hash == pass)
                {
                    return true;
                }
            }

            return false;

        }

        #endregion

        #region Add Line

        public void WriteGuestToFile(string username, string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                File.AppendAllText(guestPath, username + Encoding.UTF8.GetString(hash));
            }
        }

        public void WriteHostToFile(string username, string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                File.AppendAllText(hostPath, username + Encoding.UTF8.GetString(hash));
            }
        }

        #endregion
    }
}
