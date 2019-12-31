using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represents an error caused by Attempt to set a vacation on a date already occupied
    /// </summary>
    public class OccupiedDatesException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception
        /// </summary>
        /// <example>
        /// <code>
        /// var e = new OccupiedDatesException();
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "A vacation cannot be scheduled on a date already occupied"
        /// </code>
        /// </example>
        public OccupiedDatesException()
            : base("A vacation cannot be scheduled on a date already occupied")
        {

        }

        /// <summary>
        /// Constructor that creates an exception
        /// </summary>
        /// <param name="rangeOccupied">The range of the date that is occupied</param>
        /// <example>
        /// <code>
        /// var e = new OccupiedDatesException("12.1 - 15.1");
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "The range 12.1 - 15.1 is already occupied, You can't schedule a vacation there"
        /// </code>
        /// </example>
        public OccupiedDatesException(string rangeOccupied) : base(string.Format("The range {0} is already occupied, You can't schedule a vacation there", rangeOccupied))
        {

        }
    }
}
