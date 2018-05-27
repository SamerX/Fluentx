using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A general class for the abstract pattern, inherit this class where the derived class is the main bridge and T is the implementation type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractBridge<T>
    {
        /// <summary>
        /// The implementation Instance
        /// </summary>
        public virtual T Implementation { get; set; }
    }
}
