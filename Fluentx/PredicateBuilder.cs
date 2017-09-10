using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// Helps building prediates to provide And/AndNot/Or/OrNot/Not true/false for linq expressions
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Presents a true predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>() { return x => true; }
        /// <summary>
        /// Presents false predicates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>() { return x => false; }
        /// <summary>
        /// Creates a predicate using the specified expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate) { return predicate; }
        /// <summary>
        /// Ors a predicate with another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expLeft"></param>
        /// <param name="expRight"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var expInvoked = Expression.Invoke(expRight, expLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expLeft.Body, expInvoked), expLeft.Parameters);
        }
        /// <summary>
        /// And a predicate with another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expLeft"></param>
        /// <param name="expRight"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var expInvoked = Expression.Invoke(expRight, expLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expLeft.Body, expInvoked), expLeft.Parameters);
        }
        /// <summary>
        /// XOring a predicate with another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expLeft"></param>
        /// <param name="expRight"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Xor<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var expInvoked = Expression.Invoke(expRight, expLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.ExclusiveOr(expLeft.Body, expInvoked), expLeft.Parameters);
        }
        /// <summary>
        /// Negating a predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }
        /// <summary>
        /// Anding Not with another predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expLeft"></param>
        /// <param name="expRight"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndNot<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var expInvoked = Expression.Invoke(expRight.Not(), expLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expLeft.Body, expInvoked), expLeft.Parameters);
        }
        /// <summary>
        /// Oring Not with another predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expLeft"></param>
        /// <param name="expRight"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrNot<T>(this Expression<Func<T, bool>> expLeft, Expression<Func<T, bool>> expRight)
        {
            var expInvoked = Expression.Invoke(expRight.Not(), expLeft.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expLeft.Body, expInvoked), expLeft.Parameters);
        }
    }
}
