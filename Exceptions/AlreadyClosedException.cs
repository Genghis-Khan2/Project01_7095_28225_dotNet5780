using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represents an error caused by trying to change object that is status is closed
    /// </summary>
    public class AlreadyClosedException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception with a specific error message
        /// </summary>
        /// <param name="objectType">The type of object that was trying to change it</param>
        /// <param name="objectId">The ID number of the object that was trying to change it</param>
        /// <example>
        /// <code>
        /// var e = new AlreadyClosedException("GuestRequest", 1234);
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "Cannot change GuestRequest(ID: 1234) object because he already closed"
        /// </code>
        /// </example>
        public AlreadyClosedException(string objectType, int objectId)
            : base(String.Format("Cannot change {0}(ID: {1}) object because he already closed", objectType, objectId))
        {


        }
    }
}
