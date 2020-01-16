using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
namespace Fluentx
{
    /// <summary>
    /// Experimental class: Expresser expresses an entity's properties in a strongly typed format using lambda expressions, instead of 
    /// creating too many POCO's you can use the expresser to transfer the entity's data that you want.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>    
    public sealed class Expresser<TEntity>
    {
        readonly Dictionary<string, object> store = new Dictionary<string, object>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        public void Set<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value)
        {
            var stack = new Stack<string>();

            MemberExpression memberExpression;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var uniaryExpression = expression.Body as UnaryExpression;
                    memberExpression = (uniaryExpression?.Operand) as MemberExpression;
                    break;
                default:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }

            while (memberExpression != null)
            {
                stack.Push(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            string path = string.Join(".", stack.ToArray());

            if (store.ContainsKey(path))
                throw new Exception(string.Format("The specified Expression path {0} => {0}.{1} has already been added.", expression.Parameters[0].Name, path));
            store.Add(path, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberPath"></param>
        /// <param name="value"></param>
        private void Set(string memberPath, object value)
        {
            store.Add(memberPath, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        public void TrySet<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value)
        {
            string path = string.Empty;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                path = RecursiveExpressionPath(path, expression.Body as MemberExpression);
            }

            if (store.ContainsKey(path))
                store[path] = value;
            store.Add(path, value);
        }
        /// <summary>
        /// Expressers all direct public properties and fields within the entity EXCEPT what has has been specified.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="excepters"></param>
        public void Set(TEntity entity, params Expression<Func<TEntity, object>>[] excepters)
        {
            var properties = entity.GetType().GetTypeInfo().GetProperties();
            foreach (var property in properties)
            {
                if (excepters != null && excepters.Any(x => x.Body.NodeType == ExpressionType.MemberAccess && (x.Body as MemberExpression).Member.Name == property.Name))
                    continue;

                if (property is PropertyInfo)
                    Set(property.Name, (property as PropertyInfo).GetValue(entity, null));
            }

            var fields = entity.GetType().GetTypeInfo().GetFields();
            foreach (var field in fields)
            {
                if (excepters.Any(x => x.Body.NodeType == ExpressionType.MemberAccess && (x.Body as MemberExpression).Member.Name == field.Name))
                    continue;

                if (field is FieldInfo)
                    Set(field.Name, (field as FieldInfo).GetValue(entity));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TReturn Get<TReturn>(Expression<Func<TEntity, TReturn>> expression)
        {
            string path = string.Empty;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                path = RecursiveExpressionPath(path, expression.Body as MemberExpression);
            }

            if (!store.ContainsKey(path))
                throw new Exception(string.Format("The specified Expression path {0} => {0}.{1} is not stored.", expression.Parameters[0].Name, path));

            TReturn result = (TReturn)store[path];
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TReturn TryGet<TReturn>(Expression<Func<TEntity, TReturn>> expression)
        {
            string path = string.Empty;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                path = RecursiveExpressionPath(path, expression.Body as MemberExpression);
            }

            if (!store.ContainsKey(path))
                return default(TReturn);

            TReturn result = (TReturn)store[path];
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string RecursiveExpressionPath(string path, MemberExpression expression)
        {
            path = expression.Member.Name + (string.IsNullOrEmpty(path) ? string.Empty : "." + path);

            if (expression.Expression is MemberExpression)
            {
                path = RecursiveExpressionPath(path, expression.Expression as MemberExpression);
            }

            return path;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="excepters"></param>
        /// <returns></returns>
        public static Expresser<TEntity> Create(TEntity entity, params Expression<Func<TEntity, object>>[] excepters)
        {
            Expresser<TEntity> expresser = new Expresser<TEntity>();
            var properties = entity.GetType().GetTypeInfo().GetProperties();
            foreach (var property in properties)
            {
                if (excepters != null && excepters.Any(x => x.Body.NodeType == ExpressionType.MemberAccess && (x.Body as MemberExpression).Member.Name == property.Name))
                    continue;

                if (property is PropertyInfo)
                    expresser.Set(property.Name, (property as PropertyInfo).GetValue(entity, null));
            }

            var fields = entity.GetType().GetTypeInfo().GetFields();
            foreach (var field in fields)
            {
                if (excepters.Any(x => x.Body.NodeType == ExpressionType.MemberAccess && (x.Body as MemberExpression).Member.Name == field.Name))
                    continue;

                if (field is FieldInfo)
                    expresser.Set(field.Name, (field as FieldInfo).GetValue(entity));
            }
            return expresser;
        }

    }
}
