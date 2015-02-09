using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message)
            : base(message)
        {

        }
    }
}
