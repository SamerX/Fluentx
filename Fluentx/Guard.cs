using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// Behavioural presenation of throwing expected exceptions
    /// </summary>
    /// 
    public class Guard
    {
        /// <summary>
        /// Will throw a <see cref="InvalidOperationException"/> if the conditionToThroughException
        /// is true, with the specificied message.
        /// </summary>
        /// <param name="conditionToThroughException">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <example>
        /// Sample usage:
        /// <code>
        /// Guard.Against(string.IsNullOrEmpty(name), "Name must have a value");
        /// </code>
        /// </example>
        public static void Against(bool conditionToThroughException, string message = null)
        {
            if (conditionToThroughException == false)
                return;
            throw new InvalidOperationException(message);
        }
        /// <summary>
        /// Will throw a <see cref="InvalidOperationException"/> if the conditionActionToThroughException
        /// is true, with the specificied message.
        /// </summary>
        /// <param name="conditionActionToThroughException"></param>
        /// <param name="message"></param>
        public static void Against(Func<bool> conditionActionToThroughException, string message = null)
        {
            if (conditionActionToThroughException() == false)
                return;
            throw new InvalidOperationException(message);
        }
        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the conditionToThroughException is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="conditionToThroughException">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <example>
        /// Sample usage:
        /// <code>
        /// <![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]>
        /// </code>
        /// </example>
        public static void Against<TException>(bool conditionToThroughException, string message = null) where TException : Exception
        {
            if (conditionToThroughException == false)
                return;
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the conditionActionToThroughException is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="conditionActionToThroughException"></param>
        /// <param name="message"></param>
        public static void Against<TException>(Func<bool> conditionActionToThroughException, string message = null) where TException : Exception
        {
            if (conditionActionToThroughException() == false)
                return;
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }
}
