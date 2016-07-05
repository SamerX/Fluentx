using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// Main Interface for fluentx object to object mapper
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Mapper Source Type 
        /// </summary>
        Type SourceType { get; }
        /// <summary>
        /// Mapper Destination Type
        /// </summary>
        Type DestinationType { get; }
    }
    /// <summary>
    /// When overriden it provides functionalities to map from object to object (source to destination) using convention based mapping and a set of custom user rules mapping.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IMapper<TSource, TDestination> : IMapper where TDestination : new()
    {
        /// <summary>
        /// Conditionally maps the specified destination member from the source instance if found according to conditionalAction value.
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="conditionalAction">The action which its value will determine if to ignore or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> Conditional(Expression<Func<TDestination, object>> destinationMember, Func<TSource, bool> conditionalAction);
        /// <summary>
        /// Maps the specified destination member using the specified resolver.
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="resolver">The func action which will return the value and update the destination member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> For<T>(Expression<Func<TDestination, T>> destinationMember, Func<TSource, T> resolver);
        /// <summary>
        /// Maps the specified destination member using the specified resolver if the conditionalAction evaluated to true
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="resolver">The func action which will return the value and update the destination member</param>
        /// <param name="conditionalAction">The action which its value will determine if to map using the resolver specified or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> ForIf<T>(Expression<Func<TDestination, T>> destinationMember, Func<TSource, T> resolver, Func<TSource, bool> conditionalAction);
        /// <summary>
        /// Ignores mapping of the specified destination member
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be ignored from mapping</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> Ignore(Expression<Func<TDestination, object>> destinationMember);
        /// <summary>
        /// Conditionally ignores the specified destination member from mapping
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be ignored from mapping</param>
        /// <param name="conditionalAction">The action which its value will determine if to ignore or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> IgnoreIf(Expression<Func<TDestination, object>> destinationMember, Func<TSource, bool> conditionalAction);
        /// <summary>
        /// Adds the specified mapper to the list of mappers that will be used in case a name match and types match found during mapping.
        /// </summary>
        /// <typeparam name="TSrc">Type of source instance to map from</typeparam>
        /// <typeparam name="TDest">Type of destination instance to map to</typeparam>
        /// <param name="subMapper">An instance of a mapper that will be used as a sub mapper in the current mapper in case a match is found for mapping</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> UseMapper<TSrc, TDest>(IMapper<TSrc, TDest> subMapper) where TDest : new();
        /// <summary>
        /// Creates and adds a new mapper to list of mappers that will be used in case a name match and types match found during mapping.
        /// </summary>
        /// <typeparam name="TSrc">Type of source instance to map from</typeparam>
        /// <typeparam name="TDest">Type of destination instance to map to</typeparam>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> UseMapper<TSrc, TDest>() where TDest : new();
        /// <summary>
        /// Use this method to do custom actions through the mapper, handy to tigh custom resolvings with the mapper. Resolvers are the last things to be executed throught the mapper.
        /// </summary>
        /// <param name="resolver">Custom action to do whatever on the mapper</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        IMapper<TSource, TDestination> Resolve(Action<TSource, TDestination> resolver);
        /// <summary>
        /// Executes mapping and returns the destination instance mapped using the mapping rules specified in the mapper instance.
        /// </summary>
        /// <param name="source">The source instance to map from</param>
        /// <returns>The destination instance which will be returned from the process of mapping</returns>
        TDestination Map(TSource source);
        /// <summary>
        /// Executes mapping between source and destination instances
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        TDestination Map(TSource source, TDestination dest);
    }
    /// <summary>
    /// This class is used to do object to object mapping (source to destination) using convention based mapping and custom rules mapping
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class Mapper<TSource, TDestination> : IMapper<TSource, TDestination> where TDestination : new()
    {
        private readonly IList<IMapper> mappings = new List<IMapper>();
        private readonly IList<Action<TSource, TDestination>> actionResolvers = new List<Action<TSource, TDestination>>();
        private readonly Dictionary<Expression<Func<TDestination, object>>, KeyValuePair<Func<TSource, object>, Func<TSource, bool>>> membersResolved = new Dictionary<Expression<Func<TDestination, object>>, KeyValuePair<Func<TSource, object>, Func<TSource, bool>>>();
        private readonly Dictionary<Expression<Func<TDestination, object>>, Func<TSource, bool>> membersIgnored = new Dictionary<Expression<Func<TDestination, object>>, Func<TSource, bool>>();
        private readonly Dictionary<Expression<Func<TDestination, object>>, Func<TSource, bool>> membersConditional = new Dictionary<Expression<Func<TDestination, object>>, Func<TSource, bool>>();

        /// <summary>
        /// Creates a mapper using the specified source type and destination type
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destType"></param>
        /// <returns></returns>
        public static IMapper Create(Type sourceType, Type destType)
        {
            var mainType = typeof(Mapper<,>);
            Type[] innerType = { sourceType, destType };
            var mapperType = mainType.MakeGenericType(innerType);
            return (IMapper)Activator.CreateInstance(mapperType);
        }
        /// <summary>
        /// Mapper source type
        /// </summary>
        public Type SourceType
        {
            get
            {
                return typeof(TSource);
            }
        }
        /// <summary>
        /// Mapper destination type
        /// </summary>
        public Type DestinationType
        {
            get
            {
                return typeof(TDestination);
            }
        }
        /// <summary>
        /// Executes mapping
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        private void MapInstanceToInstance(TSource source, TDestination dest)
        {
            var destinationProperties = typeof(TDestination).GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite).ToList();

            foreach (var destProp in destinationProperties)
            {
                var sourceProp =
                    typeof(TSource).GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.Name == destProp.Name)
                    .FirstOrDefault();

                //When a match for the source and destination is found
                if (sourceProp != null)
                {
                    //IGNORED: Lets check if the destination property should be ignored or not
                    var memberIgnored = membersIgnored.Where(x => x.Key.Body is MemberExpression ? ((PropertyInfo)((MemberExpression)x.Key.Body).Member).Name == destProp.Name : ((PropertyInfo)((MemberExpression)(((UnaryExpression)x.Key.Body).Operand)).Member).Name == destProp.Name).FirstOrDefault();
                    if (memberIgnored.Key != null && memberIgnored.Value != null)
                    {
                        //if condition is true then dont map it
                        if (memberIgnored.Value(source))
                        {
                            continue;
                        }
                    }

                    //CONDITIONAL MAPPING: Lets check if this property should be mapped according to a condition or not
                    var memberConditional = membersConditional.Where(x => x.Key.Body is MemberExpression ? ((PropertyInfo)((MemberExpression)x.Key.Body).Member).Name == destProp.Name : ((PropertyInfo)((MemberExpression)(((UnaryExpression)x.Key.Body).Operand)).Member).Name == destProp.Name).FirstOrDefault();
                    if (memberConditional.Key != null && memberConditional.Value != null)
                    {
                        //if condition is false dont map it
                        if (!memberConditional.Value(source))
                        {
                            continue;
                        }
                    }

                    //MEMBER RESOLVERS: Lets check if this property has a member resolver or not
                    var memberResolved = membersResolved.Where(x => x.Key.Body is MemberExpression ? ((PropertyInfo)((MemberExpression)x.Key.Body).Member).Name == destProp.Name : ((PropertyInfo)((MemberExpression)(((UnaryExpression)x.Key.Body).Operand)).Member).Name == destProp.Name).FirstOrDefault();
                    if (memberResolved.Key != null)
                    {
                        var resolver = memberResolved.Value.Key;
                        var conditionalAction = memberResolved.Value.Value;

                        if (conditionalAction(source))
                        {
                            destProp.SetValue(dest, resolver(source), null);
                            continue;
                        }
                    }

                    //NORMAL MAPPING
                    else
                    {
                        if (source != null)
                        {
                            //Handles the straight forward case: same type, mostly premitives and normal reference types
                            if (sourceProp.PropertyType == destProp.PropertyType)
                            {
                                destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                            }
                            //Handles the case of Nullable to Premitive and ViseVersa
                            else if (destProp.PropertyType.GetTypeInfo().IsValueType && sourceProp.PropertyType.GetTypeInfo().IsValueType &&
                                (sourceProp.PropertyType.GetTypeInfo().IsAssignableFrom(destProp.PropertyType) || destProp.PropertyType.GetTypeInfo().IsAssignableFrom(sourceProp.PropertyType)) &&
                                sourceProp.GetValue(source, null) != null)
                            {
                                destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                            }
                            //Handles the case of IEnumerable
                            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(sourceProp.PropertyType) && typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(destProp.PropertyType))
                            {
                                //Get the inner types for source and destination proprties
                                var sourceInnerType = sourceProp.PropertyType.IsArray ? sourceProp.PropertyType.GetElementType() : sourceProp.PropertyType.GenericTypeArguments.First();
                                var destInnerType = destProp.PropertyType.IsArray ? destProp.PropertyType.GetElementType() : destProp.PropertyType.GenericTypeArguments.First();

                                //In case the inner types are the same then do normal mapping

                                //Fetch the mapping using the inner types of source and destination 
                                var mapping = sourceInnerType == destInnerType ?
                                    Create(sourceInnerType, destInnerType)// In case its the same type lets create a selfie map
                                    :
                                    mappings.Where(x => x.SourceType == sourceInnerType && x.DestinationType == destInnerType).FirstOrDefault();

                                if (mapping == null)
                                    continue;

                                if (sourceProp.PropertyType.IsArray)
                                {
                                    //Get source property value
                                    Array sourcePropValue = sourceProp.GetValue(source, null) as Array;

                                    #region Arrary to Array
                                    if (destProp.PropertyType.IsArray)
                                    {
                                        var destPropValue = Array.CreateInstance(destInnerType, sourcePropValue.Length);

                                        if (sourcePropValue != null)
                                        {
                                            for (int i = 0; i < sourcePropValue.Length; i++)
                                            {
                                                //Map the value
                                                var value = mapping.GetType().GetTypeInfo().GetMethod("Map", BindingFlags.InvokeMethod).Invoke(mapping, new object[] { sourcePropValue.GetValue(i) });

                                                //Add the value to 
                                                destPropValue.SetValue(value, i);
                                            }

                                            //Mapping finished then set the destination property to the newly created property value
                                            destProp.SetValue(dest, destPropValue, null);
                                        }
                                        else
                                        {
                                            destProp.SetValue(dest, null, null);
                                        }
                                    }
                                    #endregion

                                    #region Array to IEnumerable
                                    else
                                    {
                                        //Create destination property type as List<destination inner type>
                                        var destPropValueType = typeof(List<>).MakeGenericType(destInnerType);

                                        //Create instance of destination property using an empty constructor
                                        var destPropValue = Activator.CreateInstance(destPropValueType);

                                        if (sourcePropValue != null)
                                        {
                                            //Loop over srouce property value
                                            foreach (var item in sourcePropValue)
                                            {
                                                //Map the value
                                                var value = mapping.GetType().GetTypeInfo().GetMethod("Map", BindingFlags.InvokeMethod).Invoke(mapping, new object[] { item });

                                                //Add the mapped value to destination property value
                                                destPropValue.GetType().GetTypeInfo().GetMethod("Add", BindingFlags.InvokeMethod).Invoke(destPropValue, new object[] { value });
                                            }
                                            //Mapping finished then set the destination property to the newly created property value
                                            destProp.SetValue(dest, destPropValue, null);
                                        }
                                        else
                                        {
                                            destProp.SetValue(dest, null, null);
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    //Get source property value
                                    IEnumerable sourcePropValue = sourceProp.GetValue(source, null) as IEnumerable;

                                    #region IEnumerable to Array
                                    if (destProp.PropertyType.IsArray)
                                    {
                                        ArrayList tempDestValues = new ArrayList();

                                        if (sourcePropValue != null)
                                        {
                                            foreach (var item in sourcePropValue)
                                            {
                                                //Map the value
                                                var value = mapping.GetType().GetTypeInfo().GetMethod("Map", BindingFlags.InvokeMethod).Invoke(mapping, new object[] { item });

                                                //Add the value to temp destination array
                                                tempDestValues.Add(value);
                                            }
                                            //Create destination property value from the temp array
                                            Array destPropValue = tempDestValues.ToArray(destInnerType);

                                            //Mapping finished then set the destination property to the newly created property value
                                            destProp.SetValue(dest, destPropValue, null);
                                        }
                                        else
                                        {
                                            destProp.SetValue(dest, null, null);
                                        }
                                    }
                                    #endregion

                                    #region IEnumerable to IEnumerable
                                    else
                                    {
                                        //Create destination property type as List<destination inner type>
                                        var destPropValueType = typeof(List<>).MakeGenericType(destInnerType);

                                        //Create instance of destination property using an empty constructor
                                        var destPropValue = Activator.CreateInstance(destPropValueType);

                                        if (sourcePropValue != null)
                                        {
                                            //Loop over srouce property value
                                            foreach (var item in sourcePropValue)
                                            {
                                                //Map the value
                                                var value = mapping.GetType().GetTypeInfo().GetMethod("Map", BindingFlags.InvokeMethod).Invoke(mapping, new object[] { item });

                                                //Add the mapped value to destination property value
                                                destPropValue.GetType().GetTypeInfo().GetMethod("Add", BindingFlags.InvokeMethod).Invoke(destPropValue, new object[] { value });
                                            }
                                            //Mapping finished then set the destination property to the newly created property value
                                            destProp.SetValue(dest, destPropValue, null);
                                        }

                                        else
                                        {
                                            destProp.SetValue(dest, null, null);
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                //Lets find a mapping that might map this property
                                var mapping = mappings.Where(x => x.SourceType == sourceProp.PropertyType && x.DestinationType == destProp.PropertyType).FirstOrDefault();

                                //A map exist in the list and we can use it to map this property
                                if (mapping != null)
                                {
                                    var value = mapping.GetType().GetTypeInfo().GetMethod("Map", BindingFlags.InvokeMethod).Invoke(mapping, new object[] { sourceProp.GetValue(source, null) });
                                    destProp.SetValue(dest, value, null);
                                }
                                else
                                {
                                    //Nothing to do here so far
                                }
                            }
                        }
                    }
                }
                else
                {
                    //MEMBER RESOLVERS: Lets check if this property has a member resolver or not
                    var memberResolved = membersResolved.Where(x => x.Key.Body is MemberExpression ? ((PropertyInfo)((MemberExpression)x.Key.Body).Member).Name == destProp.Name : ((PropertyInfo)((MemberExpression)(((UnaryExpression)x.Key.Body).Operand)).Member).Name == destProp.Name).FirstOrDefault();
                    if (memberResolved.Key != null)
                    {
                        var resolver = memberResolved.Value.Key;
                        var conditionalAction = memberResolved.Value.Value;

                        if (conditionalAction(source))
                        {
                            destProp.SetValue(dest, resolver(source), null);
                            continue;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Conditionally maps the specified destination member from the source instance if found according to conditionalAction value.
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="conditionalAction">The action which its value will determine if to ignore or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> Conditional(Expression<Func<TDestination, object>> destinationMember, Func<TSource, bool> conditionalAction)
        {
            membersConditional.Add(destinationMember, conditionalAction);
            return this;
        }
        /// <summary>
        /// Maps the specified destination member using the specified resolver.
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="resolver">The func action which will return the value and update the destination member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> For<T>(Expression<Func<TDestination, T>> destinationMember, Func<TSource, T> resolver)
        {
            var tempDestinationMember = typeof(T).GetTypeInfo().IsValueType ?
                Expression.Lambda<Func<TDestination, object>>(Expression.Convert(destinationMember.Body, typeof(object)), destinationMember.Parameters)
                :
                Expression.Lambda<Func<TDestination, object>>(destinationMember.Body, destinationMember.Parameters);

            Func<TSource, object> tempResolver = (src) => resolver(src);

            membersResolved.Add(tempDestinationMember, new KeyValuePair<Func<TSource, object>, Func<TSource, bool>>(tempResolver, src => true));
            return this;
        }
        /// <summary>
        /// Maps the specified destination member using the specified resolver if the conditionalAction evaluated to true
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be mapped (its value updated)</param>
        /// <param name="resolver">The func action which will return the value and update the destination member</param>
        /// <param name="conditionalAction">The action which its value will determine if to map using the resolver specified or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> ForIf<T>(Expression<Func<TDestination, T>> destinationMember, Func<TSource, T> resolver, Func<TSource, bool> conditionalAction)
        {
            var tempDestinationMember = typeof(T).GetTypeInfo().IsValueType ?
               Expression.Lambda<Func<TDestination, object>>(Expression.Convert(destinationMember.Body, typeof(object)), destinationMember.Parameters)
               :
               Expression.Lambda<Func<TDestination, object>>(destinationMember.Body, destinationMember.Parameters);

            Func<TSource, object> tempResolver = (src) => resolver(src);

            membersResolved.Add(tempDestinationMember, new KeyValuePair<Func<TSource, object>, Func<TSource, bool>>(tempResolver, conditionalAction));
            return this;
        }
        /// <summary>
        /// Ignores mapping of the specified destination member
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be ignored from mapping</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> Ignore(Expression<Func<TDestination, object>> destinationMember)
        {
            membersIgnored.Add(destinationMember, src => true);
            return this;
        }
        /// <summary>
        /// Conditionally ignores the specified destination member from mapping
        /// </summary>
        /// <param name="destinationMember">The member on destination which will be ignored from mapping</param>
        /// <param name="conditionalAction">The action which its value will determine if to ignore or not mapping this member</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> IgnoreIf(Expression<Func<TDestination, object>> destinationMember, Func<TSource, bool> conditionalAction)
        {
            membersIgnored.Add(destinationMember, src => true);
            return this;
        }
        /// <summary>
        /// Adds the specified mapper to the list of mappers that will be used in case a name match and types match found during mapping.
        /// </summary>
        /// <typeparam name="TSrc">Type of source instance to map from</typeparam>
        /// <typeparam name="TDest">Type of destination instance to map to</typeparam>
        /// <param name="subMapper">An instance of a mapper that will be used as a sub mapper in the current mapper in case a match is found for mapping</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> UseMapper<TSrc, TDest>(IMapper<TSrc, TDest> subMapper) where TDest : new()
        {
            if (mappings.Where(x => x.SourceType == typeof(TSrc) && x.DestinationType == typeof(TDest)).Any())
            {
                throw new InvalidOperationException("There is already a mapping for Mapper<{0},{1}> registered in this mapper");
            }
            mappings.Add(subMapper);
            return this;
        }
        /// <summary>
        /// Creates and adds a new mapper to list of mappers that will be used in case a name match and types match found during mapping.
        /// </summary>
        /// <typeparam name="TSrc">Type of source instance to map from</typeparam>
        /// <typeparam name="TDest">Type of destination instance to map to</typeparam>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> UseMapper<TSrc, TDest>() where TDest : new()
        {
            if (mappings.Where(x => x.SourceType == typeof(TSrc) && x.DestinationType == typeof(TDest)).Any())
            {
                throw new InvalidOperationException("There is already a mapping for Mapper<{0},{1}> registered in this mapper");
            }
            mappings.Add(new Mapper<TSrc, TDest>());
            return this;
        }
        /// <summary>
        /// Use this method to do custom actions through the mapper, handy to tigh custom resolvings with the mapper. Resolvers are the last things to be executed throught the mapper.
        /// </summary>
        /// <param name="resolver">Custom action to do whatever on the mapper</param>
        /// <returns>Returns instance of IMapper for chaining purposes</returns>
        public IMapper<TSource, TDestination> Resolve(Action<TSource, TDestination> resolver)
        {
            actionResolvers.Add(resolver);
            return this;
        }
        /// <summary>
        /// Executes mapping and returns the destination instance mapped using the mapping rules specified in the mapper instance.
        /// </summary>
        /// <param name="source">The source instance to map from</param>
        /// <returns>The destination instance which will be returned from the process of mapping</returns>
        public TDestination Map(TSource source)
        {
            TDestination dest = new TDestination();

            MapInstanceToInstance(source, dest);

            foreach (var action in actionResolvers)
            {
                action(source, dest);
            }

            return dest;
        }
        /// <summary>
        /// Executes mapping between source and destination instances
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public TDestination Map(TSource source, TDestination dest)
        {
            if (dest == null)
            {
                dest = new TDestination();
            }

            MapInstanceToInstance(source, dest);

            foreach (var action in actionResolvers)
            {
                action(source, dest);
            }

            return dest;
        }
    }
}
