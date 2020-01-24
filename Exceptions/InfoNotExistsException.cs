using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class InfoNotExistsException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception with a specific error message suited
        /// to the type who's info does not exist (<paramref name="typeNotExisting"/>), and which type tried to get the info that deos not exist (<paramref name="typeCheckingExisting"/>)
        /// </summary>
        /// <param name="typeNotExisting">The type who's info does not exist in the database</param>
        /// <param name="typeCheckingExisting">The type that needs the info that does not exist</param>
        /// <example>
        /// <code>
        /// var e = new InfoNotExistsException("GuestRequest", "Order");
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "The GuestRequest does not exist for Order"
        /// </code>
        /// </example>
        public InfoNotExistsException(string typeNotExisting, string typeCheckingExisting)
            : base(String.Format("The {0} does not exist for {1}", typeNotExisting, typeCheckingExisting))
        {

        }
    }
}
