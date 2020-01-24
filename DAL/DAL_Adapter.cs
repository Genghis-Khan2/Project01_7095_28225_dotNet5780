using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public static class DAL_Adapter
    {
        public static IDAL GetDAL()
        {
            return Dal_XML_imp.GetDAL();
        }
    }
}
