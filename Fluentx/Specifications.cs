using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// 
    /// </summary>
    public static class Specification
    {
        /// <summary>
        /// Use it as a starting point for your specifications.
        /// </summary>
        /// <returns></returns>
        public static ISpecification<T> True<T>()
        {
            return new ExpressionSpecification<T>((x) => true, "##!True##");
        }

        /// <summary>
        /// Use it as starting point for rare conditions for purposes of testing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ISpecification<T> False<T>()
        {
            return new ExpressionSpecification<T>((x) => false, "##False##");
        }

        /// <summary>
        /// Supply a starting point condition to start your specifications dynamically.
        /// </summary>
        /// <param name="startingCondition"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ISpecification<T> Default<T>(bool startingCondition = true)
        {
            return startingCondition ? True<T>() : False<T>();
        }
    }

    /// <summary>
    /// Represents the core of the specification pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// List of validation messages when it failed
        /// </summary>
        IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Validate the specification and return the boolean result along with any error messages in Result.ErrorMessages, might not all specification run.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Result<bool> Validate(T instance);

        /// <summary>
        /// Validate the specification and return the boolean result along with any error messages in Result.ErrorMessages, might not all specification run.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<Result<bool>> ValidateAsync(T instance);

        /// <summary>
        /// Validate the specification and return the boolean result along with any error messages in Result.ErrorMessages, all specifications will run regardless of any shortcuts.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Result<bool> ValidateAll(T instance);

        /// <summary>
        /// Validate the specification and return the boolean result along with any error messages in Result.ErrorMessages, all specifications will run regardless of any shortcuts.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<Result<bool>> ValidateAllAsync(T instance);

        //ISpecification<T> Not();
        /// <summary>
        /// And a specification with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> specification);

        /// <summary>
        /// And a specification with another
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, bool> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, Task<bool>> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, bool> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, Task<bool>> expression, string message);

        /// <summary>
        /// Or a specification with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> specification);

        /// <summary>
        /// Or a specification with another
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, bool> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, Task<bool>> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, bool> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, Task<bool>> expression, string message);

        /// <summary>
        /// Xor a specification with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Xor(ISpecification<T> specification);

        /// <summary>
        /// Xor a specification with another
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, bool> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, Task<bool>> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, bool> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, Task<bool>> expression, string message);

        /// <summary>
        /// Negates the current specification
        /// </summary>
        /// <returns></returns>
        ISpecification<T> Not();

        /// <summary>
        /// Anding with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, bool> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, Task<bool>> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, bool> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, Task<bool>> expression, string message);

        /// <summary>
        /// Oring with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, bool> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, Task<bool>> expression, IEnumerable<string> messages);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, bool> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, Task<bool>> expression, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, string> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, Task<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, IEnumerable<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> And(Func<T, Task<IEnumerable<string>>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, string> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, Task<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, IEnumerable<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Or(Func<T, Task<IEnumerable<string>>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, string> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, Task<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, IEnumerable<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> Xor(Func<T, Task<IEnumerable<string>>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, string> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, Task<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, IEnumerable<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(Func<T, Task<IEnumerable<string>>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, string> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, Task<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, IEnumerable<string>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(Func<T, Task<IEnumerable<string>>> expression);
    }

    /// <summary>
    /// Base abstract class for custom specifications
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Message returned for the specification validation
        /// </summary>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Executes and Validates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Result<bool> Validate(T instance);

        /// <summary>
        /// Executes and Validates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<Result<bool>> ValidateAsync(T instance);

        /// <summary>
        /// Validate the specification and returns success or failure.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Result<bool> ValidateAll(T instance);

        /// <summary>
        /// Validate the specification and returns success or failure.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<Result<bool>> ValidateAllAsync(T instance);

        /// <summary>
        /// Creates a new Specification holding current specification AND specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> And(ISpecification<T> specification)
        {
            /*if (this.Messages.FirstOrDefault() == "##DEFAULT##")
            {
                return new AndSpecification<T>(Specification.True<T>(), specification);
            }*/

            return new AndSpecification<T>(this, specification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, string> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, Task<string>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, IEnumerable<string>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, Task<IEnumerable<string>>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, bool> expression, IEnumerable<string> messages)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, bool> expression, string message)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> And(Func<T, Task<bool>> expression, string message)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// Creates a new specification holding current specification OR specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, string> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, Task<string>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, IEnumerable<string>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, Task<IEnumerable<string>>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, bool> expression, IEnumerable<string> messages)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, bool> expression, string message)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> Or(Func<T, Task<bool>> expression, string message)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// Creates a new specification holding current specification XOR specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(ISpecification<T> specification)
        {
            return new XorSpecification<T>(this, specification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, string> expression)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, Task<string>> expression)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, IEnumerable<string>> expression)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, Task<IEnumerable<string>>> expression)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, bool> expression, IEnumerable<string> messages)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression, messages));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> Xor(Func<T, bool> expression, string message)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(Func<T, Task<bool>> expression, string message)
        {
            return new XorSpecification<T>(this, new ExpressionSpecification<T>(expression, message));
        }

        /// <summary>
        /// Negates the current specification
        /// </summary>
        /// <returns></returns>
        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        /// <summary>
        /// Anding with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> AndNot(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification.Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> AndNot(Func<T, string> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> AndNot(Func<T, Task<string>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> AndNot(Func<T, IEnumerable<string>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> AndNot(Func<T, Task<IEnumerable<string>>> expression)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> AndNot(Func<T, bool> expression, IEnumerable<string> messages)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, messages).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> AndNot(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, messages).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> AndNot(Func<T, bool> expression, string message)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, message).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> AndNot(Func<T, Task<bool>> expression, string message)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(expression, message).Not());
        }

        /// <summary>
        /// Oring with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification.Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, string> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, Task<string>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, IEnumerable<string>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, Task<IEnumerable<string>>> expression)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression).Not());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, bool> expression, IEnumerable<string> messages)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, messages).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> OrNot(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, messages).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(Func<T, bool> expression, string message)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, message).Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ISpecification<T> OrNot(Func<T, Task<bool>> expression, string message)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(expression, message).Not());
        }

        /// <summary>
        /// And Specification
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static ISpecification<T> operator &(CompositeSpecification<T> first, ISpecification<T> second)
        {
            return first.And(second);
        }

        /// <summary>
        /// OR Specification
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static ISpecification<T> operator |(CompositeSpecification<T> first, ISpecification<T> second)
        {
            return first.Or(second);
        }

        /// <summary>
        /// XOR specification
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static ISpecification<T> operator ^(CompositeSpecification<T> first, ISpecification<T> second)
        {
            return first.Xor(second);
        }
    }

    /// <summary>
    /// Represents the And Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.Messages = new List<string>();
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        /// <summary>
        /// Executes and validates the specification, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            Result<bool> leftResult;

            if ((leftResult = this.leftSpecification.Validate(instance)).Data)
            {
                Result<bool> rightResult;
                if ((rightResult = this.rightSpecification.Validate(instance)).Data)
                {
                    return Result.Return(true);
                }

                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            return Result.Return(false, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            Result<bool> leftResult;

            if ((leftResult = await this.leftSpecification.ValidateAsync(instance)).Data)
            {
                Result<bool> rightResult;
                if ((rightResult = await this.rightSpecification.ValidateAsync(instance)).Data)
                {
                    return Result.Return(true);
                }

                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            return Result.Return(false, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification, this will continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> ValidateAll(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = this.leftSpecification.ValidateAll(instance);
            var rightResult = this.rightSpecification.ValidateAll(instance);

            if (!leftResult.Data)
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            if (!rightResult.Data)
            {
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            var result = leftResult.Data & rightResult.Data;

            if (result)
            {
                (this.Messages as IList<string>)?.Clear();
            }

            return Result.Return(result, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification, this will continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAllAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = await this.leftSpecification.ValidateAllAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAllAsync(instance);

            if (!leftResult.Data)
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            if (!rightResult.Data)
            {
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            var result = leftResult.Data & rightResult.Data;

            if (result)
            {
                (this.Messages as IList<string>)?.Clear();
            }

            return Result.Return(result, this.Messages);
        }
    }

    /// <summary>
    /// Represents Or specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.Messages = new List<string>();
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        /// <summary>
        /// Executes and validates the specification returning the boolean result, in case of failed validation it will stop on the first failed validation without executing the remaining ones in the chain of validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            Result<bool> leftResult;
            if ((leftResult = this.leftSpecification.Validate(instance)).Data)
            {
                return Result.Return(true);
            }

            this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();

            Result<bool> rightResult;
            if ((rightResult = this.rightSpecification.Validate(instance)).Data)
            {
                (this.Messages as IList<string>)?.Clear();
                return Result.Return(true);
            }

            this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();

            return Result.Return(false, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification returning the boolean result, in case of failed validation it will stop on the first failed validation without executing the remaining ones in the chain of validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            Result<bool> leftResult;
            if ((leftResult = await this.leftSpecification.ValidateAsync(instance)).Data)
            {
                return Result.Return(true);
            }

            this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();

            Result<bool> rightResult;
            if ((rightResult = await this.rightSpecification.ValidateAsync(instance)).Data)
            {
                (this.Messages as IList<string>)?.Clear();
                return Result.Return(true);
            }

            this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();

            return Result.Return(false, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> ValidateAll(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = this.leftSpecification.ValidateAll(instance);
            var rightResult = this.rightSpecification.ValidateAll(instance);

            if (!leftResult.Data)
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();
            }

            if (!rightResult.Data)
            {
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();
            }

            var result = leftResult.Data | rightResult.Data;
            if (result)
            {
                (this.Messages as IList<string>)?.Clear();
            }

            return Result.Return(result, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAllAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = await this.leftSpecification.ValidateAllAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAllAsync(instance);

            if (!leftResult.Data)
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();
            }

            if (!rightResult.Data)
            {
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();
            }

            var result = leftResult.Data | rightResult.Data;

            if (result)
            {
                (this.Messages as IList<string>)?.Clear();
            }

            return Result.Return(result, this.Messages);
        }
    }

    /// <summary>
    /// Represents XOR specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class XorSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public XorSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.Messages = new List<string>();
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        /// <summary>
        /// Executes and validates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            var xorResult = leftResult.Data ^ rightResult.Data;

            if (!xorResult)
            {
                if (!leftResult.Data)
                {
                    this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                }

                if (!rightResult.Data)
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }

            return Result.Return(xorResult, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = await this.leftSpecification.ValidateAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAsync(instance);

            var xorResult = leftResult.Data ^ rightResult.Data;

            if (!xorResult)
            {
                if (!leftResult.Data)
                {
                    this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                }

                if (!rightResult.Data)
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }

            return Result.Return(xorResult, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification (for Xor its no difference with Validate Method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> ValidateAll(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = this.leftSpecification.ValidateAll(instance);
            var rightResult = this.rightSpecification.ValidateAll(instance);

            var xorResult = leftResult.Data ^ rightResult.Data;

            if (!xorResult)
            {
                if (!leftResult.Data)
                {
                    this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                }

                if (!rightResult.Data)
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }

            return Result.Return(xorResult, this.Messages);
        }

        /// <summary>
        /// Executes and validates the specification (for Xor its no difference with Validate Method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAllAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();

            var leftResult = await this.leftSpecification.ValidateAllAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAllAsync(instance);

            var xorResult = leftResult.Data ^ rightResult.Data;

            if (!xorResult)
            {
                if (!leftResult.Data)
                {
                    this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                }

                if (!rightResult.Data)
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }

            return Result.Return(xorResult, this.Messages);
        }
    }

    /// <summary>
    /// Represents a negate based specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> specification;

        /// <summary>
        /// Represents a negate based specification
        /// </summary>
        /// <param name="specification"></param>
        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Executes and validate the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = this.specification.Validate(instance);

            if (!result.Data)
            {
                //Validation is passed (as its a negate)
                return Result.Return(true);
            }

            foreach (var errorMessage in result.ErrorMessages)
            {
                (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
            }

            return Result.Return(!result.Data, this.Messages);
        }

        /// <summary>
        /// Executes and validate the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = await this.specification.ValidateAsync(instance);

            if (!result.Data)
            {
                //Validation is passed (as its a negate)
                return Result.Return(true);
            }

            foreach (var errorMessage in result.ErrorMessages)
            {
                (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
            }

            return Result.Return(!result.Data, this.Messages);
        }

        /// <summary>
        /// Validates and continues
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> ValidateAll(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = this.specification.ValidateAll(instance);

            if (!result.Data)
            {
                //Validation is passed (as its a negate)
                return Result.Return(true);
            }

            foreach (var errorMessage in result.ErrorMessages)
            {
                (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
            }

            return Result.Return(!result.Data, this.Messages);
        }

        /// <summary>
        /// Validates and continues
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAllAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = await this.specification.ValidateAllAsync(instance);

            if (!result.Data)
            {
                //Validation is passed (as its a negate)
                return Result.Return(true);
            }

            foreach (var errorMessage in result.ErrorMessages)
            {
                (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
            }

            return Result.Return(!result.Data, this.Messages);
        }
    }

    /// <summary>
    /// Represents an expression based specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private readonly Func<T, bool> expression;
        private readonly Func<T, Task<bool>> asyncExpression;

        private readonly Func<T, IEnumerable<string>> expressionWithMessages;
        private readonly Func<T, Task<IEnumerable<string>>> asyncExpressionWithMessages;
        
        private readonly Func<T, string> expressionWithMessage;
        private readonly Func<T, Task<string>> asyncExpressionWithMessage;
        
        

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExpressionSpecification(Func<T, string> expression)
        {
            this.expressionWithMessage = expression ?? throw new ArgumentNullException();
        }
        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExpressionSpecification(Func<T, Task<string>> expression)
        {
            this.asyncExpressionWithMessage = expression ?? throw new ArgumentNullException();
        }
        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExpressionSpecification(Func<T, IEnumerable<string>> expression)
        {
            this.expressionWithMessages = expression ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExpressionSpecification(Func<T, Task<IEnumerable<string>>> expression)
        {
            this.asyncExpressionWithMessages = expression ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        public ExpressionSpecification(Func<T, bool> expression, IEnumerable<string> messages)
        {
            if (expression == null || messages.IsNullOrEmpty())
                throw new ArgumentNullException();
            this.expression = expression;
            this.Messages = messages;
        }

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        public ExpressionSpecification(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            if (expression == null || messages.IsNullOrEmpty())
                throw new ArgumentNullException();
            this.asyncExpression = expression;
            this.Messages = messages;
        }

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public ExpressionSpecification(Func<T, bool> expression, string message)
        {
            if (expression == null || message == null)
                throw new ArgumentNullException();
            this.expression = expression;
            this.Messages = new[] { message };
        }

        /// <summary>
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public ExpressionSpecification(Func<T, Task<bool>> expression, string message)
        {
            if (expression == null || message == null)
                throw new ArgumentNullException();
            this.asyncExpression = expression;
            this.Messages = new[] { message };
        }

        /// <summary>
        /// Validate the specification and return true or false
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> Validate(T instance)
        {
            try
            {
                if (expression != null)
                {
                    var result = this.expression.Invoke(instance);
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (expressionWithMessages != null)
                {
                    this.Messages = this.expressionWithMessages.Invoke(instance)?.ToList() ?? new List<string>();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (expressionWithMessage != null)
                {
                    var message = this.expressionWithMessage.Invoke(instance);
                    this.Messages = message is null ? new List<string>() : message.WrapAsList();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (asyncExpression != null)
                {
                    var result = this.asyncExpression.Invoke(instance).Result;
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (asyncExpressionWithMessages != null)
                {
                    this.Messages = this.asyncExpressionWithMessages.Invoke(instance).Result?.ToList() ?? new List<string>();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (asyncExpressionWithMessage != null)
                {
                    var message = this.asyncExpressionWithMessage.Invoke(instance).Result;
                    this.Messages = message is null ? new List<string>() : message.WrapAsList();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }

                throw new InvalidOperationException("Expression provided is null");
                //var result = this.expression?.Invoke(instance) ?? this.asyncExpression(instance).Result;
                //return Result.Return(result, result ? Enumerable.Empty<string>() : this.Messages);
            }
            catch (Exception ex)
            {
                return Result.Return(false,
                    $"Validate for specification with messages: \"{this.Messages?.ToCSV()}\" threw an exception:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Validate the specification and return true or false
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            try
            {
                if (asyncExpression != null)
                {
                    var result = await this.asyncExpression.Invoke(instance);
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (asyncExpressionWithMessages != null)
                {
                    this.Messages = (await this.asyncExpressionWithMessages.Invoke(instance))?.ToList() ?? new List<string>();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (asyncExpressionWithMessage != null)
                {
                    var message = (await this.asyncExpressionWithMessage.Invoke(instance));
                    this.Messages = message is null ? new List<string>() : message.WrapAsList();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (expression != null)
                {
                    var result = this.expression.Invoke(instance);
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (expressionWithMessages != null)
                {
                    this.Messages = this.expressionWithMessages.Invoke(instance)?.ToList();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }
                else if (expressionWithMessage != null)
                {
                    var message = this.expressionWithMessage.Invoke(instance);
                    this.Messages = message is null ? new List<string>() : message.WrapAsList();
                    var result = this.Messages.IsNullOrEmpty();
                    return Result.Return(result, result ? new List<string>() : this.Messages);
                }

                throw new InvalidOperationException("Expression provided is null");
                /*var result = this.asyncExpression != null
                    ? await this.asyncExpression(instance)
                    : this.expression(instance);
                return Result.Return(result, result ? Enumerable.Empty<string>() : this.Messages);*/
            }
            catch (Exception ex)
            {
                return Result.Return(false,
                    $"ValidateAsync for specification with messages: \"{this.Messages?.ToCSV()}\" threw an exception:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Will validate the specification and continue to the next node in rules chain.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result<bool> ValidateAll(T instance)
        {
            return Validate(instance);
        }

        /// <summary>
        /// Will validate the specification and continue to the next node in rules chain.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAllAsync(T instance)
        {
            return await ValidateAsync(instance);
        }
    }
}