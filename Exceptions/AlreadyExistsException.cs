using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represents an error caused by a value that was attempted to be inserted, but already
    /// existed in the data structure
    /// </summary>
    public class AlreadyExistsException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception with a specific error message suited
        /// to the key that exists(<paramref name="val"/>) and type stored in the list(<paramref name="type"/>)
        /// </summary>
        /// <param name="val">Key that already exists</param>
        /// <param name="type">Data type stored in the list</param>
        /// <example>
        /// <code>
        /// var e = new AlreadyExistsException(123, "GuestRequest");
        /// Console.WriteLine(e.Message);
        /// "The key 123 already exists as a GuestRequest"
        /// </code>
        /// </example>
        public AlreadyExistsException(int val, string type)
            : base(String.Format("The key {0} already exists as a {1}", val, type))
        {

        }
    }
}
