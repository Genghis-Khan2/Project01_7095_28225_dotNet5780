using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class NoItemsException : Exception
    {
        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <remarks>
        /// Message is non-specific to a certain type
        /// </remarks>
        /// See <see cref="NoItemsException(string)"/> for a specific exception
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
