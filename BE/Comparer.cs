using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BE
{
    public class HostComparer : IEqualityComparer<Host>
    {
        public bool Equals(Host a, Host b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
            {
                return false;
            }

            return a.HostKey == b.HostKey;
        }

        public int GetHashCode(Host obj)
        {
            return obj.HostKey.GetHashCode();
        }
    }

    public class GuestRequestComparer : IEqualityComparer<GuestRequest>
    {
        public bool Equals(GuestRequest a, GuestRequest b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
            {
                return false;
            }

            return a.GuestRequestKey == b.GuestRequestKey;
        }

        public int GetHashCode(GuestRequest obj)
        {
            return obj.GuestRequestKey.GetHashCode();
        }
    }
}
