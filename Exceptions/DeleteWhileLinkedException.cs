using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represent error caused by trying to delete object when there are objects linked with it
    /// </summary>
    class DeleteWhileLinkedException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception with a specific error message suited
        /// to the object trying to delete(Type: <paramref name="ObjectType1"/> and ID: <paramref name="ID1"/>)
        /// and the object who linked to it(Type: <paramref name="ObjectType2"/> and ID: <paramref name="ID2"/>)
        /// </summary>
        /// <param name="ObjectType1">The type of object they were trying to delete</param>
        /// <param name="ID1">The ID number of the object they were trying to delete</param>
        /// <param name="ObjectType2">The object type linked with the object they were trying to delete</param>
        /// <param name="ID2">The ID number of the object linked to the object that they were trying to delete</param>
        /// <example>
        /// <code>
        /// var e = new DeleteWhileLinked("HostingUnit", 1234, "Order", 1024);
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "Cannot delete the HostingUnit object(ID: 1234) because the Order(ID: 1024) object attached to it"
        /// </code>
        /// </example>
        public DeleteWhileLinkedException(string ObjectType1, int ID1, string ObjectType2, int ID2)
            : base(String.Format("Cannot delete the {0} object(ID: {1}) because the {2}(ID: {3}) object attached to it", ObjectType1, ID1, ObjectType2, ID2))
        {

        }
    }
}
