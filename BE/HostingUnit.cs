using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

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

        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get
            {
                return Diary.Flatten();
            }

            set
            {
                if (value == null)
                {
                    Diary = null;
                }

                Diary = value.Expand(12);
            }
        }

        [XmlIgnore]
        public bool[,] Diary
        {
            get
            {
                return diary;
            }
            set
            {
                if (value == null)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            diary[i, j] = false;
                        }
                    }
                }
                else
                {
                    diary = value;
                }
            }
        }

        private bool[,] diary = new bool[12, 31];

        public float Commission { get; set; } = 0;
        #region Properties related to the hosting conditions in the hosting unit
        public Enums.Area Area { get; set; }
        public Enums.HostingUnitType Type { get; set; }
        public int NumberOfPlacesForAdults { get; set; }
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
        public override string ToString()
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
                res += index.ToString("MMMM") + ":\n";
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
