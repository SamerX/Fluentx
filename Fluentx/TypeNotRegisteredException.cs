using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeNotRegisteredException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TypeNotRegisteredException(string message)
            : base(message)
        {

        }
    }
}
