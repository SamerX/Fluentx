
namespace Fluentx
{
    /// <summary>
    /// Possible ways to generate a singleton class based on Jon Skeets book "C# In Depth", more details in the link: http://csharpindepth.com/Articles/General/Singleton.aspx
    /// </summary>
    public enum SingletonType
    {
        /// <summary>
        /// A singleton class that is very simple but not thread safe
        /// </summary>
        NotThreadSafeNotLazy,
        /// <summary>
        /// A singleton class that is simple and thread safe but uses a lock for thread safety, which in terms of performance is not the best
        /// </summary>
        ThreadSafeUsingLockNotLazy,
        /// <summary>
        /// A singleton class that is simple and thread safe but uses a lock for thread safety and double checking trying to minimize the locks acquired.
        /// </summary>
        ThreadSafeUsingLockDoubleCheckingNotLazy,
        /// <summary>
        /// A singleton class that is thread safe without using locks, so it has a good performance, one take is that if the class has has other static members and got called then the initialization is not lazy any more.
        /// </summary>
        ThreadSafeNoLocksSemiLazy,
        /// <summary>
        /// A singleton class that is thread safe without any performance complication and its fully lazy since the initialization will only happen on the call to the static instance only.
        /// </summary>
        ThreadSafeFullLazy,
        /// <summary>
        /// A singleton class that is thread safe with a good performance and its fully lazy initialized using .net framework Lazy class.
        /// </summary>
        ThreadSafeFullLazyUsingLazyClass
    }
}
