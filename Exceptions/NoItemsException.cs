using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class NoItemsException : Exception
    {
        public NoItemsException()
            : base("No items exist")
        {

        }

        public NoItemsException(string message)
            : base(String.Format("No items exist in the {0} list", message))
        {

        }
    }
}
