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
        public int[][] Diary { get; set; }
        public override string ToString()
        {
            string res = "";
            res += "Hosting Unit Key: " + HostingUnitKey+"\n";
            res += "Owner: " + Owner + "\n";
            res += "Hosting Unit Name: " + HostingUnitName + "\n";
            for (int i = 0; i < Diary.Length; i++)
            {
                res
            }
        }
    }
}
