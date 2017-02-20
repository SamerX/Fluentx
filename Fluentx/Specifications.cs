using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluentx
{
    /// <summary>
    /// Represents the core of the specificaiton pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// When overriden in a derived class does the validation on the specification (Rule(s)) and return if it succeeds
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        bool Validate(T o);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        bool ValidateAndContinue(T o);
        /// <summary>
        /// When overriden in a derived class does the validaiton on the specification (Rule(s)) and return list of validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IEnumerable<string> ValidateWithMessages(T instance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IEnumerable<string> ValidateWithMessagesAndContinue(T instance);
        /// <summary>
        /// And a specification with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> specification);
        /// <summary>
        /// Or a specificaiton with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> specification);
        /// <summary>
        /// Xor a specification with another
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Xor(ISpecification<T> specification);
    }
    /// <summary>
    /// Base abstract class for custom specifications 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Message returned for the specificaiton validation
        /// </summary>
        protected IEnumerable<string> Messages { get; set; }
        /// <summary>
        /// Executes and Validates the specificaiton
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract bool Validate(T instance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract bool ValidateAndContinue(T instance);
        /// <summary>
        /// Executes and validates the specificaiton and return validation messages.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract IEnumerable<string> ValidateWithMessages(T instance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract IEnumerable<string> ValidateWithMessagesAndContinue(T instance);
        /// <summary>
        /// Current specification AND specified specification 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
        /// <summary>
        /// Current specification OR specified specification 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
        /// <summary>
        /// Current specification XOR specified specification 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(ISpecification<T> specification)
        {
            return new XorSpecification<T>(this, specification);
        }
    }

    /// <summary>
    /// Represents the And Specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AndSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }
        /// <summary>
        /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            return this.leftSpecification.Validate(instance)
                && this.rightSpecification.Validate(instance);
        }
        /// <summary>
        /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            return this.leftSpecification.ValidateAndContinue(instance)
                & this.rightSpecification.ValidateAndContinue(instance);
        }
        /// <summary>
        /// Executes and validates the specification and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessages(instance)).IsNullOrEmpty() && (rightSpecResult = this.rightSpecification.ValidateWithMessages(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty() & (rightSpecResult = this.rightSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }
    }
    /// <summary>
    /// Represents Or specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }
        /// <summary>
        /// Executes and valiates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            return this.leftSpecification.Validate(instance)
                || this.rightSpecification.Validate(instance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            return this.leftSpecification.ValidateAndContinue(instance)
                | this.rightSpecification.ValidateAndContinue(instance);
        }
        /// <summary>
        /// Executes and validates the specification and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessages(instance)).IsNullOrEmpty() || (rightSpecResult = this.rightSpecification.ValidateWithMessages(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty() | (rightSpecResult = this.rightSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }

    }
    /// <summary>
    /// Represnts XOR specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class XorSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public XorSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }
        /// <summary>
        /// Executes and validates the specification
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            return this.leftSpecification.Validate(instance)
                ^ this.rightSpecification.Validate(instance);
        }
        /// <summary>
        /// Executes and validates the specification (for Xor its no difference with Validate Method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            return this.leftSpecification.ValidateAndContinue(instance)
                ^ this.rightSpecification.ValidateAndContinue(instance);
        }
        /// <summary>
        /// Executes and validates the specification and return validation meessages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessages(instance)).IsNullOrEmpty() ^ (rightSpecResult = this.rightSpecification.ValidateWithMessages(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }
        /// <summary>
        /// Executes and validates the specification and return validation meessages (for Xor no difference from Validate method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            IEnumerable<string> leftSpecResult = new List<string>();
            IEnumerable<string> rightSpecResult = new List<string>();

            if ((leftSpecResult = this.leftSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty() ^ (rightSpecResult = this.rightSpecification.ValidateWithMessagesAndContinue(instance)).IsNullOrEmpty())
            {
                return new List<string>();
            }
            else
            {
                return leftSpecResult.Concat(rightSpecResult).ToList();
            }
        }
    }
    /// <summary>
    /// Represents an expression based specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private Func<T, bool> expression;
        private Func<T, IEnumerable<string>> expressionWithMessage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Func<T, bool> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public ExpressionSpecification(Func<T, bool> expression, IEnumerable<string> message)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
            this.Messages = message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Func<T, IEnumerable<string>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expressionWithMessage = expression;
            //this.Message = message;
        }
        /// <summary>
        /// Validate the specification and return true or false
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            if (this.expression != null)
            {
                return this.expression(instance);
            }
            else
            {
                return this.expressionWithMessage(instance).IsNullOrEmpty();
            }
        }

        public override bool ValidateAndContinue(T instance)
        {
            if (this.expression != null)
            {
                return this.expression(instance);
            }
            else
            {
                return this.expressionWithMessage(instance).IsNullOrEmpty();
            }
        }
        /// <summary>
        /// validates the specificaiton and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            if (this.expression != null)
            {
                return (this.expression(instance) ? new List<string>() : Messages );
            }
            else
            {
                this.Messages = this.expressionWithMessage(instance);
                return (this.Messages.IsNullOrEmpty() ? new List<string>() : Messages);
            }
        }

        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            if (this.expression != null)
            {
                return (this.expression(instance) ? new List<string>() : Messages);
            }
            else
            {
                this.Messages = this.expressionWithMessage(instance);
                return (this.Messages.IsNullOrEmpty() ? new List<string>() : Messages);
            }
        }
    }
}
