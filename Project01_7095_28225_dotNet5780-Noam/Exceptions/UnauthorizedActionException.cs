using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represents an error caused by Attempt to do action that isnt alowed for some reason
    /// </summary>
    public class UnauthorizedActionException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception
        /// </summary>
        /// <example>
        /// <code>
        /// var e = new UnauthorizedActionException();
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "Unauthorized Action"
        /// </code>
        /// </example>
        public UnauthorizedActionException()
            : base("Unauthorized Action")
        {

        }

        /// <summary>
        /// Constructor that creates an exception
        /// </summary>
        /// <example>
        /// <code>
        /// var e = new UnauthorizedActionException("you smell like a donkey");
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "This is Unauthorized Action because you smell like a donkey"
        /// </code>
        /// </example>
        public UnauthorizedActionException(string reason)
            : base(string.Format("This is Unauthorized Action because {0}", reason))
        {

        }
    }
}
