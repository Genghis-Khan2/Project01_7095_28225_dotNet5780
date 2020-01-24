using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class BL_Adapter
    {
        public static IBL GetBL()
        {
            return BLImp.getBL();
        }
    }
}
