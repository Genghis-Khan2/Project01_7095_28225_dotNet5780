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
        public int HostingUnitKey { get; set; }
        public Host Owner { set; get; }
        public string HostingUnitName { get; set; }
        public int[][] Diary { get; set; }
        //TODO: ToString
    }
}
