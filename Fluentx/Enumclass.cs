using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fluentx
{
    /// <summary>
    /// A class representation for Enums, use it to create enums as classes, its advised to create the class as follows: class TestEnum : Enumclass &lt;TestEnum&gt; { protected TestEnum(int value, string name) : base(value, name){}}
    /// where the constructor of the class is protected, you can declare the static instances inside the class e.g.: public static TestEnum one = new TestEnum(100, "one"); if you want to create an instance outside the scope of
    /// the enum it self then use the Create(int value, string name) static method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Enumclass<T> : IComparable<T> where T : Enumclass<T>
    {
        /// <summary>
        /// Creates an enum class using the specified value and name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected Enumclass(int value, string name)
        {
            Value = value;
            Name = name;
        }
        /// <summary>
        /// Creates an enum class using the specified value, value will be also used for the name.
        /// </summary>
        /// <param name="value"></param>
        protected Enumclass(int value)
            : this(value, value.ToString())
        {
        }
        /// <summary>
        /// Enum value
        /// </summary>
        public virtual int Value { get; private set; }
        /// <summary>
        /// Enum name or display name
        /// </summary>
        public virtual string Name { get; private set; }
        /// <summary>
        /// Returns the enums Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Returns all public static declared enums of the current Enumclass
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> List()
        {
            var fields = typeof(T).GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                var locatedValue = (T)field.GetValue(null);

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        /// <summary>
        /// Parses a value to the correspondent enumclass instance
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse(int value)
        {
            return Parse(value, "value", item => item.Value == value);
        }
        /// <summary>
        /// Parses a name to the correspondent enumclass instance
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Parse(string name)
        {
            return Parse(name, "name", item => item.Name.IgnoreCaseEqual(name));
        }

        private static T Parse<K>(K value, string description, Func<T, bool> predicate)
        {
            var matchingItem = List().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                throw new Exception($"{value} is not a valid {description} in {typeof(T)}");
            }

            return matchingItem;
        }
        /// <summary>
        /// Tries to parse a value to the correspondent enumclass instance
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T TryParse(int value)
        {
            return TryParse(value, "value", item => item.Value == value);
        }
        /// <summary>
        /// Tries to parse a name to the correspondent enumclass instance
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T TryParse(string name)
        {
            return TryParse(name, "name", item => item.Name.IgnoreCaseEqual(name));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static T TryParse<K>(K value, string description, Func<T, bool> predicate)
        {
            var matchingItem = List().FirstOrDefault(predicate);
            return matchingItem;
        }
        /// <summary>
        /// Evaluates based on nullability and value
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator ==(Enumclass<T> first, Enumclass<T> second)
        {
            if (first.IsNull() && second.IsNull())
            {
                return true;
            }
            else if (first.IsNotNull() && second.IsNotNull())
            {
                return first.Value == second.Value;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Evaluates based on nullability and value
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator !=(Enumclass<T> first, Enumclass<T> second)
        {
            if (first.IsNull() && second.IsNull())
            {
                return false;
            }
            else if (first.IsNotNull() && second.IsNotNull())
            {
                return first.Value != second.Value;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Note that a public constructor should exist in order for bitwise to work
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Enumclass<T> operator |(Enumclass<T> first, Enumclass<T> second)
        {
            if (first == null || second == null)
            {
                return null;
            }
            var result = first.Value | second.Value;
            return (Enumclass<T>)Activator.CreateInstance(typeof(T), result);
        }
        /// <summary>
        /// Note that a public constructor should exist in order for bitwise to work
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Enumclass<T> operator &(Enumclass<T> first, Enumclass<T> second)
        {
            if (first == null || second == null)
            {
                return null;
            }
            var result = first.Value & second.Value;
            return TryParse(result) ?? (Enumclass<T>)Activator.CreateInstance(typeof(T), result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is T otherValue))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Compares two instances.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(T other)
        {
            return Value.CompareTo((other).Value);
        }
    }
}
