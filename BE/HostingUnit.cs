﻿using System;
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
        public bool[,] Diary { get; set; }//TODO: need to be bool?

        /// <summary>
        /// Default constructor
        /// </summary>
        public HostingUnit()
        {
            this.HostingUnitKey = Configuration.HostingUnitKey++;
            this.Owner = new Host();
            this.HostingUnitName = "";
            this.Diary = new bool[12, 31];
            //init the diary
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    this.Diary[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The owner of the hosting unit</param>
        /// <param name="hostingUnitName">Name of the hosting unit</param>
        public HostingUnit(Host owner, string hostingUnitName)
        {
            this.HostingUnitKey = Configuration.HostingUnitKey++;
            this.Owner = owner;
            this.HostingUnitName = hostingUnitName;
            this.Diary = new bool[12, 31];
            //init diary
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    this.Diary[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The owner of the hosting unit</param>
        /// <param name="diary">The status of the hosting unit for one year</param>
        public HostingUnit(Host owner, string hostingUnitName, bool[,] diary)
        {
            this.HostingUnitKey = Configuration.HostingUnitKey++;
            this.Owner = owner;
            this.HostingUnitName = hostingUnitName;
            this.Diary = diary;
        }

        /// <summary>
        /// The function returns the hosting unit information in a string type
        /// </summary>
        /// <returns>The hosting unit information in string type</returns>
        public override string ToString()//TODO: check if the function is work
        {
            string res = "";
            res += "Hosting Unit Key: " + HostingUnitKey + "\n";
            res += "Owner: " + Owner + "\n";
            res += "Hosting Unit Name: " + HostingUnitName + "\n";
            res += "Diary: \n";
            DateTime index = new DateTime(2020, 1, 1);
            DateTime endOfYear = new DateTime(2020, 1, 1);
            while (index < endOfYear)
            {
                res += index.ToString("MMMM") + ":\n";//TODO: check if it works
                int cMonth = index.Month;
                while (index.Month == cMonth)
                {
                    res += index.Day + " - " + Diary[index.Month - 1, index.Day - 1] + "\n";
                    index.AddDays(1);
                }
            }
            return res;
        }
    }
}
