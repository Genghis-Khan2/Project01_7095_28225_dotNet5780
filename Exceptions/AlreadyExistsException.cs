using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// 
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
        /// 
        /// </code>
        /// </example>
        public AlreadyExistsException(int val, string type)
            : base(String.Format("The key {0} already exists as a {1}", val, type))
        {

        }
    }
}
