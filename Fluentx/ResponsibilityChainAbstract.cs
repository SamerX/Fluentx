using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A shell implementation for the Chain of responsibility design pattern, you need to access the nested class with this class to use the AbstractHandler
    /// </summary>
    public sealed class ResponsibilityChainAbstract
    {
        private ResponsibilityChainAbstract() { }
        /// <summary>
        /// When overriden its a node in the chain of responsibility.
        /// </summary>
        public abstract class AbstractHandler
        {
            /// <summary>
            /// The next handler in the chain.
            /// </summary>
            public virtual AbstractHandler Next { get; private set; }
            /// <summary>
            /// Passes the next handler to the current handler in the chain of responsibility
            /// </summary>
            /// <param name="next"></param>
            public AbstractHandler(AbstractHandler next)
            {
                this.Next = next;
            }
            /// <summary>
            /// When overriden it will send the package to the next handler to continue the chain of resposiblity.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="package"></param>
            public abstract void Handle<T>(T package);
        }
    }
}
