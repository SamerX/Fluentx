using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Validate the specification expression, if the expression is async it will run it sync.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool Validate(T instance);

        /// <summary>
        /// Validate the specification expression and continue regardless of the result, if the expression is async it will run it sync.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool ValidateAndContinue(T instance);

        /// <summary>
        /// Validate the specification expression and return the list of messages, if the expression is async it will run it sync.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IEnumerable<string> ValidateWithMessages(T instance);

        /// <summary>
        /// Validate the specification expression and return list the of messages regardless of the evaluation result, if the expression is async it will run it sync.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IEnumerable<string> ValidateWithMessagesAndContinue(T instance);

        /// <summary>
        /// Validate the specification expression.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<bool> ValidateAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<bool> ValidateAndContinueAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> Xor(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISpecification<T> Not();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(ISpecification<T> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(ISpecification<T> specification);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<bool> ValidateAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<bool> ValidateAndContinueAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        /// <summary>
        /// 
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
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(ISpecification<T> specification)
        {
            return new XorSpecification<T>(this, specification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        /// <summary>
        /// 
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
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification.Not());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static ISpecification<T> operator &(CompositeSpecification<T> first, ISpecification<T> second)
        {
            return first.And(second);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static ISpecification<T> operator |(CompositeSpecification<T> first, ISpecification<T> second)
        {
            return first.Or(second);
        }

        /// <summary>
        /// 
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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

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
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            if (leftResult && rightResult)
            {
                return true;
            }

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.ValidateAndContinue(instance);
            var rightResult = this.rightSpecification.ValidateAndContinue(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult && rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAsync(instance);

            if (leftResult && rightResult)
            {
                return true;
            }

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAndContinueAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAndContinueAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAndContinueAsync(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult && rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance)
        {
            await this.ValidateAsync(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance)
        {
            await this.ValidateAndContinueAsync(instance);
            return this.Messages;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

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
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            if (leftResult || rightResult)
            {
                return true;
            }

            this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();

            this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.ValidateAndContinue(instance);
            var rightResult = this.rightSpecification.ValidateAndContinue(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult || rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAsync(instance);

            if (leftResult || rightResult)
            {
                return true;
            }

            this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();

            this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAndContinueAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAndContinueAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAndContinueAsync(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult || rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance)
        {
            await this.ValidateAsync(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance)
        {
            await this.ValidateAndContinueAsync(instance);
            return this.Messages;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class XorSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

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
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.Validate(instance);
            var rightResult = this.rightSpecification.Validate(instance);

            if (leftResult ^ rightResult)
            {
                return true;
            }

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = this.leftSpecification.ValidateAndContinue(instance);
            var rightResult = this.rightSpecification.ValidateAndContinue(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult ^ rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAsync(instance);

            if (leftResult ^ rightResult)
            {
                return true;
            }

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAndContinueAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var leftResult = await this.leftSpecification.ValidateAndContinueAsync(instance);
            var rightResult = await this.rightSpecification.ValidateAndContinueAsync(instance);

            if (!leftResult)
            {
                this.Messages = this.Messages.Concat(this.leftSpecification.Messages).ToList();
            }

            if (!rightResult)
            {
                this.Messages = this.Messages.Concat(this.rightSpecification.Messages).ToList();
            }

            return leftResult ^ rightResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance)
        {
            await this.ValidateAsync(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance)
        {
            await this.ValidateAndContinueAsync(instance);
            return this.Messages;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> specification;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        public NotSpecification(ISpecification<T> specification)
        {
            this.Messages = new List<string>();
            this.specification = specification;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = !this.specification.Validate(instance);

            if (!result)
            {
                this.Messages = this.Messages.Concat(this.specification.Messages).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = !this.specification.ValidateAndContinue(instance);

            if (!result)
            {
                this.Messages = this.Messages.Concat(this.specification.Messages).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = !await this.specification.ValidateAsync(instance);

            if (!result)
            {
                this.Messages = this.Messages.Concat(this.specification.Messages).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAndContinueAsync(T instance)
        {
            (this.Messages as IList<string>)?.Clear();
            var result = !await this.specification.ValidateAndContinueAsync(instance);

            if (!result)
            {
                this.Messages = this.Messages.Concat(this.specification.Messages).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance)
        {
            await this.ValidateAsync(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance)
        {
            await this.ValidateAndContinueAsync(instance);
            return this.Messages;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private readonly Func<T, bool> expression;
        private readonly Func<T, Task<bool>> asyncExpression;
        private readonly Func<T, IEnumerable<string>> expressionMessages;
        private readonly Func<T, Task<IEnumerable<string>>> asyncExpressionMessages;

        /// <summary>
        ///Constructor
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
        ///Constructor
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Func<T, Task<bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.asyncExpression = expression;
        }

        /// <summary>
        ///Creates an expression specification
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

        /// <summary>
        ///Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="messages"></param>
        public ExpressionSpecification(Func<T, Task<bool>> expression, IEnumerable<string> messages)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.asyncExpression = expression;
            this.Messages = messages;
        }

        /// <summary>
        ///Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Func<T, IEnumerable<string>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expressionMessages = expression;
        }

        /// <summary>
        ///Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionSpecification(Func<T, Task<IEnumerable<string>>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.asyncExpressionMessages = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool Validate(T instance)
        {
            if (this.expression != null)
            {
                var result = this.expression(instance);
                this.Messages = result ? Enumerable.Empty<string>() : this.expressionMessages(instance);
                return result;
            }
            else
            {
                var result = this.asyncExpression(instance).Result;
                this.Messages = result ? Enumerable.Empty<string>() : this.asyncExpressionMessages(instance).Result;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override bool ValidateAndContinue(T instance)
        {
            if (expression != null)
            {
                var result = this.expression(instance);
                this.Messages = this.expressionMessages(instance);
                return result;
            }
            else
            {
                var result = this.asyncExpression(instance).Result;
                this.Messages = this.asyncExpressionMessages(instance).Result;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessages(T instance)
        {
            this.Validate(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override IEnumerable<string> ValidateWithMessagesAndContinue(T instance)
        {
            this.ValidateAndContinue(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAsync(T instance)
        {
            if (this.asyncExpression != null)
            {
                var result = await this.asyncExpression(instance);
                this.Messages = result ? Enumerable.Empty<string>() : await this.asyncExpressionMessages(instance);
                return result;
            }
            else
            {
                var result = this.expression(instance);
                this.Messages = result ? Enumerable.Empty<string>() : this.expressionMessages(instance);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<bool> ValidateAndContinueAsync(T instance)
        {
            if (this.asyncExpression != null)
            {
                var result = await this.asyncExpression(instance);
                this.Messages = await this.asyncExpressionMessages(instance);
                return result;
            }
            else
            {
                var result = this.expression(instance);
                this.Messages = this.expressionMessages(instance);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAsync(T instance)
        {
            await this.ValidateAsync(instance);
            return this.Messages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<string>> ValidateWithMessagesAndContinueAsync(T instance)
        {
            await this.ValidateAndContinueAsync(instance);
            return this.Messages;
        }
    }
}