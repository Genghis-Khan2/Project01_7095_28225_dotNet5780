using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represents a hosting unit
    /// </summary>
    class HostingUnit
    {
        private int HostingUnitKey { get; set; }
        private Host Owner { set; get; }
        private string HostingUnitName { get; set; }
        private int[][] Diary { get; set; }
        //TODO: ToString
    }
}
