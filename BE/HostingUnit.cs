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
        public int HostingUnitKey { get; set; } = 0;
        public Host Owner { set; get; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary
        {
            get
            {
                return this.Diary;//TODO:check if we need to send a new object or just reference
            }
            set
            {
                if (value == null)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            this.Diary[i, j] = false;
                        }
                    }
                }
                else
                {
                    this.Diary = value;//TODO: is it smart?
                }
            }
        }

        #region Properties related to the hosting conditions in the hosting unit
        public Enums.Area Area { get; set; }
        public Enums.HostingUnitType Type { get; set; }
        public int NumberOfPlacesForAdults { get; set; }//TODO: ISSUSE #8
        public int NumberOfPlacesForChildren { get; set; }
        public bool IsTherePool { get; set; }
        public bool IsThereJacuzzi { get; set; }
        public bool IsThereGarden { get; set; }
        public bool IsThereChildrensAttractions { get; set; }
        #endregion
        /// <summary>
        /// The function returns the hosting unit information in a string type
        /// </summary>
        /// <returns>The hosting unit information in string type</returns>
        ///<seealso cref="Object.ToString()"/> 
        public override string ToString()//TODO: check if the function is work
        {
            string res = "";
            res += "Hosting Unit Key: " + HostingUnitKey + "\n";
            res += "Owner: " + Owner + "\n";
            res += "Hosting Unit Name: " + HostingUnitName + "\n";
            res += "Area: " + Area + "\n";
            res += "Type: " + Type + "\n";
            res += "Number of places for adults: " + NumberOfPlacesForAdults + "\n";
            res += "Number of places for children: " + NumberOfPlacesForChildren + "\n";
            res += "Is there pool: " + IsTherePool + "\n";
            res += "Is there jacuzzi: " + IsThereJacuzzi + "\n";
            res += "Is there garden: " + IsThereGarden + "\n";
            res += "Is there childrens attractions: " + IsThereChildrensAttractions + "\n";
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

        
    }//TODO:check if we choose to do it like this
}
