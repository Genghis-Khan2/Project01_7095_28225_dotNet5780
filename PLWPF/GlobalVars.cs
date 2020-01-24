using System;
using System.Collections.Generic;
using System.Text;

namespace PLWPF
{
    static class GlobalVars
    {
        internal static string UserName;

        internal static BL.IBL myBL = BL.BL_Adapter.GetBL();
    }
}
