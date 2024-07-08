using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluentx
{
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
        /// Or a specification with another
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
        /// Oring with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        ISpecification<T> OrNot(ISpecification<T> specification);
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
            return new AndSpecification<T>(this, specification);
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
        /// Creates a new specification holding current specification XOR specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> Xor(ISpecification<T> specification)
        {
            return new XorSpecification<T>(this, specification);
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
        /// Oring with a negate to the specified specification
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ISpecification<T> OrNot(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification.Not());
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
                else
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            return Result.Return(false);
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
                else
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
                }
            }
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
            }

            return Result.Return(false);
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

            return Result.Return(result);
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

            return Result.Return(result);
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
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();

                Result<bool> rightResult;
                if ((rightResult = this.rightSpecification.Validate(instance)).Data)
                {
                    (this.Messages as IList<string>)?.Clear();
                    return Result.Return(true);
                }
                else
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();
                }
            }

            return Result.Return(false);
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
            else
            {
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages).ToList();

                Result<bool> rightResult;
                if ((rightResult = await this.rightSpecification.ValidateAsync(instance)).Data)
                {
                    (this.Messages as IList<string>)?.Clear();
                    return Result.Return(true);
                }
                else
                {
                    this.Messages = this.Messages.Concat(rightResult.ErrorMessages).ToList();
                }
            }

            return Result.Return(false);
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

            return Result.Return(result);
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

            return Result.Return(result);
        }
    }

    /// <summary>
    /// Represnts XOR specification
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
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            return Result.Return(xorResult);
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
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            return Result.Return(xorResult);
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
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            return Result.Return(xorResult);
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
                this.Messages = this.Messages.Concat(leftResult.ErrorMessages);
                this.Messages = this.Messages.Concat(rightResult.ErrorMessages);
            }

            return Result.Return(xorResult);
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
            else
            {
                foreach (var errorMessage in result.ErrorMessages)
                {
                    (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
                }
            }

            return result;
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
            else
            {
                foreach (var errorMessage in result.ErrorMessages)
                {
                    (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
                }
            }

            return result;
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
            else
            {
                foreach (var errorMessage in result.ErrorMessages)
                {
                    (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
                }
            }

            return result;
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
            else
            {
                foreach (var errorMessage in result.ErrorMessages)
                {
                    (this.Messages as IList<string>)?.Add("NOT:" + errorMessage);
                }
            }

            return result;
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

        /// <summary>
        /// Creates an expression specification
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
        /// Creates an expression specification
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
        /// Creates an expression specification
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public ExpressionSpecification(Func<T, bool> expression, string message)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
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
            if (expression == null)
                throw new ArgumentNullException();
            else
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
            var result = this.expression?.Invoke(instance) ?? this.asyncExpression(instance).Result;
            return Result.Return(result, this.Messages);
        }

        /// <summary>
        /// Validate the specification and return true or false
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Result<bool>> ValidateAsync(T instance)
        {
            var result = this.asyncExpression != null
                ? await this.asyncExpression(instance)
                : this.expression(instance);
            return Result.Return(result, this.Messages);
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