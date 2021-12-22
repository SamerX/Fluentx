using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Fluentx
{
#if (NET452_OR_GREATER || NETSTANDARD2_0_OR_GREATER)
    public static partial class Extensions
    {
        /// <summary>
        /// The Clone Method that will be recursively used for the deep clone.
        /// </summary>
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Returns TRUE if the type is a primitive one, FALSE otherwise.
        /// </summary>
        private static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        /// <summary>
        /// Returns a Deep Clone/Deep Copy of an object using a recursive call to all objects in the graph.
        /// </summary>
        public static object DeepClone(this Object obj)
        {
            return DeepClone_Internal(obj, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }

        /// <summary>
        /// Returns a Deep Clone / Deep Copy of an object of type T using a recursive call to the CloneMethod specified above.
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            return (T)DeepClone((object)obj);
        }

        private static Object DeepClone_Internal(Object obj, IDictionary<Object, Object> visited)
        {
            if (obj == null) return null;
            var typeToReflect = obj.GetType();
            if (IsPrimitive(typeToReflect)) return obj;
            if (visited.ContainsKey(obj)) return visited[obj];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(obj, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(DeepClone_Internal(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(obj, cloneObject);
            CopyFields(obj, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(obj, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = DeepClone_Internal(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
        private static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0) return;
            ArrayTraverse walker = new ArrayTraverse(array);
            do action(array, walker.Position);
            while (walker.Step());
        }

        private class ReferenceEqualityComparer : EqualityComparer<Object>
        {
            public override bool Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }
            public override int GetHashCode(object obj)
            {
                if (obj == null) return 0;
                return obj.GetHashCode();
            }
        }
        private class ArrayTraverse
        {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse(Array array)
            {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }
#endif
}