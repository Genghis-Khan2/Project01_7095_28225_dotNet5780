using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a hosting unit
    /// </summary>
    public class HostingUnit
    {
        private int HostingUnitKey { get; }
        public Host Owner { set; get; }
        public string HostingUnitName { get; set; }
        public bool[][] Diary { get; set; }//TODO: need to be bool?
        public override string ToString()//TODO: check if the function is work
        {
            string res = "";
            res += "Hosting Unit Key: " + HostingUnitKey+"\n";
            res += "Owner: " + Owner + "\n";
            res += "Hosting Unit Name: " + HostingUnitName + "\n";
            res += "Diary: \n";
            DateTime index = new DateTime(1, 1, 2048);
            DateTime endOfYear = new DateTime(1, 1, 2049);
            while (index<endOfYear)
            {
                res += index.ToString("MMMM")+":\n";//TODO: check if it works
                int cMonth = index.Month;
                while (index.Month==cMonth)
                {
                    res += index.Day + " - " + Diary[index.Month - 1][index.Day - 1]+"\n";
                    index.AddDays(1);
                }
            }
            return res;
        }
    }
}
