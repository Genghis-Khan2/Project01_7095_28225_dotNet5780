using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// This class represents an exception that is formed when an operation is being called when there are no items
    /// when items are necessary for a certain operation
    /// </summary>
    public class NoItemsException : Exception
    {
        /// <summary>
        /// Standard constructor.
        /// See <see cref="NoItemsException(string)"/> for a specific exception
        /// </summary>
        /// <remarks>
        /// Message is non-specific to a certain type
        /// </remarks>
        public NoItemsException()
            : base("No items exist")
        {
        }

        /// <summary>
        /// Constructor that creates a specific error message
        /// </summary>
        /// <param name="message">Data type stored in the list</param>
        public NoItemsException(string message)
            : base(String.Format("No items exist in the {0} list", message))
        {

        }
    }

}
