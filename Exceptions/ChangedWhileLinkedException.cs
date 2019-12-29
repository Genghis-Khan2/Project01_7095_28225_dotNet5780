using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    /// <summary>
    /// <c>Exception</c> that represent error caused by trying to delete object when there are objects linked with it
    /// </summary>
    class ChangedWhileLinkedException : Exception
    {
        /// <summary>
        /// Constructor that creates an exception with a specific error message suited
        /// to the object trying to change(the type of change: <paramref name="ChangedType"/>) object(Type: <paramref name="ObjectType1"/> and ID: <paramref name="ID1"/>)
        /// and the object who linked to it(Type: <paramref name="ObjectType2"/> and ID: <paramref name="ID2"/>)
        /// </summary>
        /// <param name="ChangedType">The type of change caused the error</param>
        /// <param name="ObjectType1">The type of object they were trying to delete</param>
        /// <param name="ID1">The ID number of the object they were trying to delete</param>
        /// <param name="ObjectType2">The object type linked with the object they were trying to delete</param>
        /// <param name="ID2">The ID number of the object linked to the object that they were trying to delete</param>
        /// <example>
        /// example 1:
        /// <code>
        /// var e = new DeleteWhileLinked("delete","HostingUnit", 1234, "Order", 1024);
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "Cannot delete the HostingUnit object(ID: 1234) because the Order(ID: 1024) object attached to it"
        /// </code>
        /// 
        /// example 2:
        ///<code>
        /// var e = new DeleteWhileLinked("change CollectionClearance of","HostingUnit", 1234, "Order", 1024);
        /// Console.WriteLine(e.Message);
        /// OUTPUT: "Cannot change CollectionClearance of the HostingUnit object(ID: 1234) because the Order(ID: 1024) object attached to it"
        /// </code>
        /// </example>
        public ChangedWhileLinkedException(string ChangedType, string ObjectType1, int ID1, string ObjectType2, int ID2)
            : base(String.Format("Cannot {0} the {1} object(ID: {2}) because the {3}(ID: {4}) object attached to it", ChangedType, ObjectType1, ID1, ObjectType2, ID2))
        {

        }
    }
}
