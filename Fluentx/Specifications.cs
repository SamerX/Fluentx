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
        /// List of validation messages when it failed
        /// </summary>
        IEnumerable<string> Messages { get; set; }
        /// <summary>
        /// When overriden in a derived class does the validation on the specification (Rule(s)) and return if it succeeds
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool Validate(T instance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool ValidateAndContinue(T instance);
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
        //ISpecification<T> Not();
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
        public IEnumerable<string> Messages { get; set; }
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
        //public ISpecification<T> Not()
        //{
        //    return new NotSpecification<T>(this);
        //}
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

    //public sealed class NotSpecification<T> : CompositeSpecification<T>
    //{
    //    ISpecification<T> specification;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="specification"></param>
    //    /// <param name="right"></param>
    //    public NotSpecification(ISpecification<T> specification)
    //    {
    //        this.specification = specification;
    //        this.Messages = new List<string>();
    //    }
    //    /// <summary>
    //    /// Executes and valiates the specification
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override bool Validate(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();
    //        var result = !this.specification.Validate(instance);
    //        if (!result)
    //        {
    //            this.specification.Messages.ForEach(x =>
    //            {
    //                (this.Messages as IList<string>).Add("NOT " + x);
    //            });
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override bool ValidateAndContinue(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();
    //        var result = !this.specification.ValidateAndContinue(instance);
    //        if (!result)
    //        {
    //            this.specification.Messages.ForEach(x =>
    //            {
    //                (this.Messages as IList<string>).Add("NOT " + x);
    //            });
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// Executes and validates the specification and return validation messages
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override IEnumerable<string> ValidateWithMessages(T instance)
    //    {
    //        this.Validate(instance);
    //        return this.Messages;
    //    }
    //    public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
    //    {
    //        this.ValidateAndContinue(instance);
    //        return this.Messages;
    //    }

    //}
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
            this.Messages = new List<string>();
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
            (this.Messages as IList<string>).Clear();

            var leftResult = false;
            var rightResult = false;

            if (leftResult = this.leftSpecification.Validate(instance))
            {
                if (rightResult = this.rightSpecification.Validate(instance))
                {
                    return true;
                }
                else
                {
                    this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
                }
            }
            else
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
            }
            return false;
        }
        /// <summary>
        /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>).Clear();

            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
            }

            var result = leftResult & rightResult;

            if (result)
            {
                (this.Messages as IList<string>).Clear();
            }
            return result;
        }
        /// <summary>
        /// Executes and validates the specification and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }
        /// <summary>
        /// Executes and validate hte specification returning validation messages, this will return all failed validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }
    }
    //public sealed class AndNotSpecification<T> : CompositeSpecification<T>
    //{
    //    ISpecification<T> leftSpecification;
    //    ISpecification<T> rightSpecification;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="left"></param>
    //    /// <param name="right"></param>
    //    public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
    //    {
    //        this.Messages = new List<string>();
    //        this.leftSpecification = left;
    //        this.rightSpecification = right;
    //    }

    //    public override bool Validate(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();

    //        var leftResult = false;
    //        var rightResult = false;

    //        if (leftResult = this.leftSpecification.Validate(instance))
    //        {
    //            if (rightResult = !this.rightSpecification.Validate(instance))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
    //            }
    //        }
    //        else
    //        {
    //            this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
    //        }
    //        return false;
    //    }
    //    /// <summary>
    //    /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override bool ValidateAndContinue(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();

    //        var leftResult = this.leftSpecification.Validate(instance);
    //        var rightResult = !this.rightSpecification.Validate(instance);

    //        if (!leftResult)
    //        {
    //            this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
    //        }

    //        if (!rightResult)
    //        {
    //            this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
    //        }

    //        var result = leftResult & rightResult;
    //        if (result)
    //        {
    //            (this.Messages as IList<string>).Clear();
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// Executes and validates the specification and return validation messages
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override IEnumerable<string> ValidateWithMessages(T instance)
    //    {
    //        this.Validate(instance);
    //        return this.Messages;
    //    }
    //    public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
    //    {
    //        this.ValidateAndContinue(instance);
    //        return this.Messages;
    //    }
    //}
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
            this.Messages = new List<string>();
            this.leftSpecification = left;
            this.rightSpecification = right;
        }
        /// <summary>
        /// Executes and validates the specificadtion returning the boolean result, in case of failed validation it will stop on the first failed validation without executing the remaining ones in the chain of validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            (this.Messages as IList<string>).Clear();

            var leftResult = false;
            var rightResult = false;

            if (leftResult = this.leftSpecification.Validate(instance))
            {
                return true;
            }
            else
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();

                if (rightResult = this.rightSpecification.Validate(instance))
                {
                    (this.Messages as IList<string>).Clear();
                    return true;
                }
                else
                {
                    this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
                }
            }
            return false;
        }
        /// <summary>
        /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>).Clear();

            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }


            var result = leftResult | rightResult;
            if (result)
            {
                (this.Messages as IList<string>).Clear();
            }
            return result;
        }
        /// <summary>
        /// Executes and validates the specification and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }
        /// <summary>
        /// Executes and validates the specification and returns the messages and will continue even if the validation failed on the first one, all failed validation messages will return.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

    }
    //public sealed class OrNotSpecification<T> : CompositeSpecification<T>
    //{
    //    ISpecification<T> leftSpecification;
    //    ISpecification<T> rightSpecification;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="left"></param>
    //    /// <param name="right"></param>
    //    public OrNotSpecification(ISpecification<T> left, ISpecification<T> right)
    //    {
    //        this.Messages = new List<string>();
    //        this.leftSpecification = left;
    //        this.rightSpecification = right;
    //    }

    //    public override bool Validate(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();

    //        var leftResult = false;
    //        var rightResult = false;

    //        if (leftResult = this.leftSpecification.Validate(instance))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            if (rightResult = !this.rightSpecification.Validate(instance))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
    //                this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
    //            }
    //        }
    //        return false;
    //    }
    //    /// <summary>
    //    /// Executes and validates the specificaiton, this will NOT continue to the next specification if the validation fails
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override bool ValidateAndContinue(T instance)
    //    {
    //        (this.Messages as IList<string>).Clear();

    //        var leftResult = this.leftSpecification.Validate(instance);
    //        var rightResult = !this.rightSpecification.Validate(instance);

    //        if (!leftResult)
    //        {
    //            this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
    //        }

    //        if (!rightResult)
    //        {
    //            this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
    //        }


    //        var result = leftResult | rightResult;
    //        if (result)
    //        {
    //            (this.Messages as IList<string>).Clear();
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// Executes and validates the specification and return validation messages
    //    /// </summary>
    //    /// <param name="instance"></param>
    //    /// <returns></returns>
    //    public override IEnumerable<string> ValidateWithMessages(T instance)
    //    {
    //        this.Validate(instance);
    //        return this.Messages;
    //    }
    //    public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
    //    {
    //        this.ValidateAndContinue(instance);
    //        return this.Messages;
    //    }

    //}
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
            this.Messages = new List<string>();
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
            (this.Messages as IList<string>).Clear();

            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            var xorResult = leftResult ^ rightResult;

            if (!xorResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
            }
            return xorResult;
        }
        /// <summary>
        /// Executes and validates the specification (for Xor its no difference with Validate Method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>).Clear();

            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            var xorResult = leftResult ^ rightResult;

            if (!xorResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages);
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages);
            }
            return xorResult;
        }
        /// <summary>
        /// Executes and validates the specification and return validation meessages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }
        /// <summary>
        /// Executes and validates the specification and return validation meessages (for Xor no difference from Validate method)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }
    }

    /// <summary>
    /// Represents an expression based specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private Func<T, bool> expression;
        //private Func<T, IEnumerable<string>> expressionWithMessage;
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
        /// <param name="messages"></param>
        public ExpressionSpecification(Func<T, bool> expression, IEnumerable<string> messages)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
            this.Messages = messages;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="expression"></param>
        //public ExpressionSpecification(Func<T, IEnumerable<string>> expression)
        //{
        //    if (expression == null)
        //        throw new ArgumentNullException();
        //    else
        //        this.expressionWithMessage = expression;
        //    //this.Message = message;
        //}
        /// <summary>
        /// Validate the specification and return true or false
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            return this.expression(instance);
        }
        /// <summary>
        /// Will validate the specification and continue to the next node in rules chain.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            return this.expression(instance);
        }
        /// <summary>
        /// validates the specificaiton and return validation messages
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            return (this.expression(instance) ? new List<string>() : Messages);
        }
        /// <summary>
        /// Executes and validates the specification and returns all messages, it will not stop on the first failed validation.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            return (this.expression(instance) ? new List<string>() : Messages);
        }
    }
}
