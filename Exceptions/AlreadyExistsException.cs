using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class AlreadyExistsException : Exception
    {

        public AlreadyExistsException(int val, string type)
            : base(String.Format("The key {0} already exists as a {1}", val, type))
        {

        }
    }
}
