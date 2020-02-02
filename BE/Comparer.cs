using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BE
{
    /// <summary>
    /// The class represent Host Comparer
    /// </summary>
    public class HostComparer : IEqualityComparer<Host>
    {
        /// <summary>
        /// Determines whether the specified Host instances are considered equal.
        /// </summary>
        /// <param name="a">First Host to compare</param>
        /// <param name="b">Second Host to compare</param>
        ///<returns> true if the Hosts are considered equal; otherwise, false</returns>
        public bool Equals(Host a, Host b)
        {
            if (ReferenceEquals(a, b))//if equal
            {
                return true;
            }

            if (ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))//if one of the Hosts in null
            {
                return false;
            }

            return a.HostKey == b.HostKey;//check the keys are equal
        }

        /// <summary>
        /// The funtion return the Hash Code for this Host
        /// </summary>
        /// <param name="obj">The Host to return the hash code for</param>
        /// <returns>Hash code of <paramref name="obj"/></returns>
        public int GetHashCode(Host obj)
        {
            return obj.HostKey.GetHashCode();
        }
    }

    /// <summary>
    /// The class represent GuestRequest Comparer
    /// </summary>
    public class GuestRequestComparer : IEqualityComparer<GuestRequest>
    {
        /// <summary>
        /// Determines whether the specified Guest Request instances are considered equal.
        /// </summary>
        /// <param name="a">First GuestRequest to compare</param>
        /// <param name="b">Second GuestRequest to compare</param>
        ///<returns> true if the Guest Requests are considered equal; otherwise, false</returns>
        public bool Equals(GuestRequest a, GuestRequest b)
        {
            if (ReferenceEquals(a, b))//if equal
            {
                return true;
            }

            if (ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))//if one of the GuestRequests is null
            {
                return false;
            }

            return a.GuestRequestKey == b.GuestRequestKey;//check if keys are equal
        }

        /// <summary>
        /// The funtion return the Hash Code for this GuestRequest
        /// </summary>
        /// <param name="obj">The GuestRequest to return the hash code for</param>
        /// <returns>Hash code of <paramref name="obj"/></returns>
        public int GetHashCode(GuestRequest obj)
        {
            return obj.GuestRequestKey.GetHashCode();
        }
    }

    /// <summary>
    /// The class represent Order Comparer
    /// </summary>
    public class OrderComparer : IEqualityComparer<Order>
    {
        /// <summary>
        /// Determines whether the specified Orders instances are considered equal.
        /// </summary>
        /// <param name="a">First Order to compare</param>
        /// <param name="b">Second Order to compare</param>
        ///<returns> true if the Orders are considered equal; otherwise, false</returns>
        public bool Equals(Order a, Order b)
        {
            if (ReferenceEquals(a, b))//if equal
            {
                return true;
            }

            if (ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))//if one of the Order is null
            {
                return false;
            }

            return a.OrderKey == b.OrderKey;//check if the keys are equal
        }

        /// <summary>
        /// The funtion return the Hash Code for this Order
        /// </summary>
        /// <param name="obj">The Order to return the hash code for</param>
        /// <returns>Hash code of <paramref name="obj"/></returns>
        public int GetHashCode(Order obj)
        {
            return obj.OrderKey.GetHashCode();
        }
    }
}
