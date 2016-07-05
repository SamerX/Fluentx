using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Fluentx
{
    /// <summary>
    /// Fx is the main class for Fluentx and its a shortened name for Fluentx, Fx also is equivelant for the mathematical representation of F(x) :)
    /// </summary>
    public sealed class Fx : IFluentInterface, IAction, ITriableAction, IConditionBuilder, IConditionalAction, IEarlyLoopBuilder, ILoopAction, ILateLoopBuilder, IEarlyLoop, ILateLoop, ISwitchBuilder, ISwitchCaseBuilder, ISwitchTypeBuilder, ISwitchTypeCaseBuilder
    {
        private static readonly Random _random = new Random();
        private const string alphabetCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private Fx()
        {
            //Not to be initialized from the out side.
        }

        #region Internal Definitions
        private Func<bool> ConditionValue { get; set; }

        private bool StopConditionEvaluation { get; set; }
        /// <summary>
        /// Used for a single default action
        /// </summary>
        private Action Action { get; set; }

        private IEnumerable InternalList { get; set; }

        private Func<bool> LoopStoperConditionalAction { get; set; }

        private bool LoopStoperCondition { get; set; }

        private enum LoopStopers { Break, Continue };
        private LoopStopers LoopStoper { get; set; }

        private enum LoopStoperLocations { BeginingOfTheLoop, EndOfTheLoop }
        private LoopStoperLocations LoopStoperLocation { get; set; }

        private object SwitchMainOperand { get; set; }

        private List<CaseInfo> SwitchCases { get; set; }




        #endregion

        /// <summary>
        /// Performs a while control as long the action is evaluating to true.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction WhileTrue(Func<bool> action)
        {
            Fx instance = new Fx();

            bool loop = true;
            while (loop)
            {
                loop = action();
            }
            return instance;
        }
        /// <summary>
        /// Performs a while control as long the action is evaluating to false.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction WhileFalse(Func<bool> action)
        {
            Fx instance = new Fx();

            bool loop = false;
            while (!loop)
            {
                loop = action();
            }
            return instance;
        }
        /// <summary>
        /// Performs a while control as long the action is evaluating to true for a maximum of <paramref name="maxLoops"/>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="maxLoops"></param>
        /// <returns></returns>
        public static IAction WhileTrueFor(Func<bool> action, ushort maxLoops)
        {
            Fx instance = new Fx();

            int loops = 0;
            bool loop = true;
            while (loop && loops < maxLoops)
            {
                ++loops;
                loop = action();
            }
            return instance;
        }
        /// <summary>
        /// Performs a while control as long the action is evaluating to false for a maximum of <paramref name="maxLoops"/>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="maxLoops"></param>
        /// <returns></returns>
        public static IAction WhileFalseFor(Func<bool> action, ushort maxLoops)
        {
            Fx instance = new Fx();

            int loops = 0;

            bool loop = false;
            while (!loop && loops < maxLoops)
            {
                ++loops;
                loop = action();
            }
            return instance;
        }
        /// <summary>
        /// Performs a while control using the evaluation condition for the specified action.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction While(Func<bool> condition, Action action)
        {
            Fx instance = new Fx();

            while (condition())
            {
                action();
            }
            return instance;
        }
        /// <summary>
        /// Performs a while control using specified condition for the specified action.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction While(bool condition, Action action)
        {
            Fx instance = new Fx();

            while (condition)
            {
                action();
            }
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of a while statement using the specified condition, this requires the call to Do eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IEarlyLoopBuilder While(Func<bool> condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = condition;
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of a Do-While statement using the specified condition, this requires the call to While eventually.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ILateLoopBuilder Do(Action action)
        {
            Fx instance = new Fx();
            instance.Action = action;
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF statement, requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder If(Func<bool> condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = condition;
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF statement, requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder If(bool condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = () => { return condition; };
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF statement (alternative for IF), requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder When(Func<bool> condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = condition;
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF statement (alternative for IF), requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder When(bool condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = () => { return condition; };
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF NOT statement, requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder IfNot(Func<bool> condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = () => { return !condition(); };
            return instance;
        }
        /// <summary>
        /// Prepare for the excution of IF NOT statement, requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IConditionBuilder IfNot(bool condition)
        {
            Fx instance = new Fx();
            instance.ConditionValue = () => { return !condition; };
            return instance;
        }
        /// <summary>
        /// Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction ForEach<T>(IEnumerable<T> list, Action<T, int> action)
        {
            Fx instance = new Fx();
            int index = 0;
            foreach (var item in list)
            {
                action(item, index);
                index += 1;
            }
            return instance;
        }
        /// <summary>
        /// Performs a foreach loop on the specified list by excuting action for each item in the Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction ForEach<T>(IEnumerable<T> list, Action<T> action)
        {
            Fx instance = new Fx();
            foreach (var item in list)
            {
                action(item);
            }
            return instance;
        }
        /// <summary>
        /// Prepares for the execution of a foreach statement, this requires the call to Do eventually.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEarlyLoopBuilder ForEach<T>(IEnumerable<T> list)
        {
            Fx instance = new Fx();
            instance.InternalList = list;
            return instance;
        }

        /// <summary>
        /// (Synonym to ForEach) Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction ForEvery<T>(IEnumerable<T> list, Action<T, int> action)
        {
            Fx instance = new Fx();
            int index = 0;
            foreach (var item in list)
            {
                action(item, index);
                index += 1;
            }
            return instance;
        }
        /// <summary>
        /// Performs a foreach loop on the specified list by excuting action for each item in the Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction ForEvery<T>(IEnumerable<T> list, Action<T> action)
        {
            Fx instance = new Fx();
            foreach (var item in list)
            {
                action(item);
            }
            return instance;
        }
        /// <summary>
        /// (Synonym to ForEach) Prepares for the execution of a foreach statement, this requires the call to Do eventually.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEarlyLoopBuilder ForEvery<T>(IEnumerable<T> list)
        {
            Fx instance = new Fx();
            instance.InternalList = list;
            return instance;
        }
        /// <summary>
        /// Prepares for the excution of a Try/Catch action, this requires the call to one of the following actions eventually: Catch, Swallow, SwalloIf.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ITriableAction Try(Action action)
        {
            Fx instance = new Fx();
            instance.Action = action;
            return instance;
        }
        /// <summary>
        /// Performs a using statement for disposable objects by executing action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IAction Using<T>(T obj, Action<T> action) where T : IDisposable
        {
            Fx instance = new Fx();
            using (obj)
            {
                action(obj);
            }
            return instance;
        }
        /// <summary>
        /// Prepares for a switch statement over the specified mainOperand, this requires the call to Default eventually.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mainOperand"></param>
        /// <returns></returns>
        public static ISwitchBuilder Switch<T>(T mainOperand)
        {
            Fx instance = new Fx();
            instance.SwitchCases = new List<CaseInfo>();
            instance.SwitchMainOperand = mainOperand;
            return instance;
        }
        /// <summary>
        /// Prepares for a switch statement over the specified type, this requires the call to Default eventually.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ISwitchTypeBuilder Switch(Type type)
        {
            Fx instance = new Fx();
            instance.SwitchCases = new List<CaseInfo>();
            instance.SwitchMainOperand = type;
            return instance;
        }
        /// <summary>
        /// Prepares for a switch statement over the specified type T, this requires the call to Default eventually.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ISwitchTypeBuilder Switch<T>()
        {
            Fx instance = new Fx();
            instance.SwitchCases = new List<CaseInfo>();
            instance.SwitchMainOperand = typeof(T);
            return instance;
        }
        /// <summary>
        /// Performs an action, if the action failed (returned false) it re-attempts to do the action again for <paramref name="attempts"/>, and waits for <paramref name="attemptSleepInMilliSeconds"/> between each attempt.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attempts"></param>
        /// <param name="attemptSleepInMilliSeconds"></param>
        public static void RetryOnFail(Func<bool> action, ushort attempts = 3, ushort attemptSleepInMilliSeconds = 1000)
        {
            int counter = 0;
            var isSuccess = false;
            do
            {
                counter++;
                isSuccess = action();
                if (!isSuccess) System.Threading.Thread.Sleep(attemptSleepInMilliSeconds);
            }
            while (!isSuccess && counter < attempts);
        }
        /// <summary>
        /// Tries to parse specified string to Int32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(string strValue, int defaultValue = default(int))
        {
            Int32 x;
            if (Int32.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to UInt32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt32(string strValue, uint defaultValue = default(uint))
        {
            UInt32 x;
            if (UInt32.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to Int32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(string strValue, int defaultValue = default(int))
        {
            Int32 x;
            if (Int32.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to UInt32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt(string strValue, uint defaultValue = default(uint))
        {
            UInt32 x;
            if (UInt32.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to Int32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(string strValue, long defaultValue = default(long))
        {
            long x;
            if (long.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to UInt32, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToULong(string strValue, ulong defaultValue = default(ulong))
        {
            ulong x;
            if (ulong.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to Int16, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(string strValue, short defaultValue = default(short))
        {
            Int16 x;
            if (Int16.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to UInt16, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUInt16(string strValue, ushort defaultValue = default(ushort))
        {
            UInt16 x;
            if (UInt16.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to Int64, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(string strValue, long defaultValue = default(long))
        {
            Int64 x;
            if (Int64.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to UInt64, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(string strValue, ulong defaultValue = default(ulong))
        {
            UInt64 x;
            if (UInt64.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to double, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(string strValue, double defaultValue = default(double))
        {
            double x;
            if (Double.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to float, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(string strValue, float defaultValue = default(float))
        {
            float x;
            if (Single.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to decimal, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string strValue, decimal defaultValue = default(decimal))
        {
            decimal x;
            if (Decimal.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to byte, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(string strValue, byte defaultValue = default(byte))
        {
            byte x;
            if (Byte.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to sbyte, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(string strValue, sbyte defaultValue = default(sbyte))
        {
            sbyte x;
            if (SByte.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to bool, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(string strValue, bool defaultValue = default(bool))
        {
            bool x;
            if (bool.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to DateTime, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string strValue, DateTime defaultValue = default(DateTime))
        {
            DateTime x;
            if (DateTime.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Tries to parse specified string to Guid, if it fails it returns the default value specified.
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(string strValue, Guid defaultValue = default(Guid))
        {
            Guid x;
            if (Guid.TryParse(strValue, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Is(Func<bool> action)
        {
            try
            {
                return action();
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Performs a lock operation (using a private object) on the specified action.
        /// </summary>
        /// <param name="action"></param>
        public static void Lock(Action action)
        {
            object @this = new object();
            lock (@this)
            {
                action();
            }
        }
        /// <summary>
        /// Performs a lock operation (using a private object) on the specified action and return the operation return value;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Lock<T>(Func<T> action)
        {
            object @this = new object();
            lock (@this)
            {
                return action();
            }
        }
        /// <summary>
        /// Performs a lock operation (using a private object) on the specified action and return @this;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T Lock<T>(T @this, Action<T> action)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                action(@this);
            }
            return @this;
        }
        /// <summary>
        /// Performs the else part of the if statement its chained to.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction IConditionalAction.Else(Action action)
        {
            if (!ConditionValue() && !StopConditionEvaluation)
            {
                StopConditionEvaluation = true;
                action();
            }
            return this;
        }
        /// <summary>
        /// Prepares for the extra ElseIf condition, this requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionalAction.ElseIf(Func<bool> condition)
        {
            this.ConditionValue = condition;
            return this;
        }
        /// <summary>
        /// Prepares for the extra ElseIf condition, this requires the call to Then eventually.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionalAction.ElseIf(bool condition)
        {
            this.ConditionValue = () => { return condition; };
            return this;
        }
        /// <summary>
        /// Performs the action for the previous conditional control statment (If, ElseIf).
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IConditionalAction IConditionBuilder.Then(Action action)
        {
            if (ConditionValue() && !StopConditionEvaluation)
            {
                StopConditionEvaluation = true;
                action();
            }
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using AND.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.And(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation && condition(); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using AND.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.And(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation && condition; };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using AND NOT.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.AndNot(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation && !condition(); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using AND NOT.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.AndNot(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation && !condition; };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using OR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Or(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation || condition(); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using OR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Or(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation || condition; };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using OR NOT.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.OrNot(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation || !condition(); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using OR NOT.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.OrNot(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation || !condition; };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using XOR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Xor(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation ^ condition(); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using XOR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Xor(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return previousEvaluation ^ condition; };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using XNOR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Xnor(Func<bool> condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return !(previousEvaluation ^ condition()); };
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition with the previously chained condition using XNOR.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder IConditionBuilder.Xnor(bool condition)
        {
            var previousEvaluation = ConditionValue();
            ConditionValue = () => { return !(previousEvaluation ^ condition); };
            return this;
        }
        /// <summary>
        /// Performs the specified action after evaluating the previous looping statement.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ILoopAction IEarlyLoopBuilder.Do(Action action)
        {
            while (this.ConditionValue())
            {
                action();
            }
            return this;
        }
        /// <summary>
        /// Performs the while statement using the specified condition statement after evaluating the previous Do statement.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILoopAction ILateLoopBuilder.While(Func<bool> condition)
        {
            do
            {
                this.Action();
            }
            while (condition());
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to break the looping statment lately (before the end of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop IEarlyLoopBuilder.LateBreakOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.EndOfTheLoop;
            this.LoopStoper = LoopStopers.Break;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to break the looping statment early (at the begining of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop IEarlyLoopBuilder.EarlyBreakOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.BeginingOfTheLoop;
            this.LoopStoper = LoopStopers.Break;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to continue the looping statment lately (before the end of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop IEarlyLoopBuilder.LateContinueOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.EndOfTheLoop;
            this.LoopStoper = LoopStopers.Continue;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to continue the looping statment early (at the begining of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop IEarlyLoopBuilder.EarlyContinueOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.BeginingOfTheLoop;
            this.LoopStoper = LoopStopers.Continue;
            return this;
        }
        /// <summary>
        /// Performs the Do statement after evaluating the previous looping statement.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ILoopAction IEarlyLoop.Do(Action action)
        {
            while (this.ConditionValue())
            {
                if (this.LoopStoperLocation == LoopStoperLocations.BeginingOfTheLoop)
                {
                    if (this.LoopStoperConditionalAction != null)
                    {
                        if (this.LoopStoperConditionalAction())
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (this.LoopStoperCondition)
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                action();

                if (this.LoopStoperLocation == LoopStoperLocations.EndOfTheLoop)
                {
                    if (this.LoopStoperConditionalAction != null)
                    {
                        if (this.LoopStoperConditionalAction())
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (this.LoopStoperCondition)
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to break the looping statment lately (before the end of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop ILateLoopBuilder.LateBreakOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.EndOfTheLoop;
            this.LoopStoper = LoopStopers.Break;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to break the looping statment early (at the begining of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop ILateLoopBuilder.EarlyBreakOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.BeginingOfTheLoop;
            this.LoopStoper = LoopStopers.Break;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to continue the looping statment lately (before the end of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop ILateLoopBuilder.LateContinueOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.EndOfTheLoop;
            this.LoopStoper = LoopStopers.Continue;
            return this;
        }
        /// <summary>
        /// Evaluates the specified condition to be used to continue the looping statment early (at the begining of the loop).
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop ILateLoopBuilder.EarlyContinueOn(Func<bool> condition)
        {
            this.LoopStoperConditionalAction = condition;
            this.LoopStoperLocation = LoopStoperLocations.BeginingOfTheLoop;
            this.LoopStoper = LoopStopers.Continue;
            return this;
        }
        /// <summary>
        /// Performs the while statement using the specifed condition after it has evaluated the previous chained Do statement.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILoopAction ILateLoop.While(Func<bool> condition)
        {
            do
            {
                if (this.LoopStoperLocation == LoopStoperLocations.BeginingOfTheLoop)
                {
                    if (this.LoopStoperConditionalAction != null)
                    {
                        if (this.LoopStoperConditionalAction())
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (this.LoopStoperCondition)
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                this.Action();

                if (this.LoopStoperLocation == LoopStoperLocations.EndOfTheLoop)
                {
                    if (this.LoopStoperConditionalAction != null)
                    {
                        if (this.LoopStoperConditionalAction())
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (this.LoopStoperCondition)
                        {
                            if (this.LoopStoper == Fx.LoopStopers.Break)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            while (condition());
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and swallow any exception that might occur.
        /// </summary>
        /// <returns></returns>
        IAction ITriableAction.Swallow()
        {
            try
            {
                this.Action();
            }
            catch
            {
            }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and swallow only the specified Exception(s).
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <returns></returns>
        IAction ITriableAction.SwallowIf<Exception1>()
        {
            try
            {
                this.Action();
            }
            catch (Exception1) { }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and swallow only the specified Exception(s).
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <returns></returns>
        IAction ITriableAction.SwallowIf<Exception1, Exception2>()
        {
            try
            {
                this.Action();
            }
            catch (Exception1) { }
            catch (Exception2) { }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and swallow only the specified Exception(s).
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <typeparam name="Exception3"></typeparam>
        /// <returns></returns>
        IAction ITriableAction.SwallowIf<Exception1, Exception2, Exception3>()
        {
            try
            {
                this.Action();
            }
            catch (Exception1) { }
            catch (Exception2) { }
            catch (Exception3) { }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and swallow only the specified Exception(s).
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <typeparam name="Exception3"></typeparam>
        /// <typeparam name="Exception4"></typeparam>
        /// <returns></returns>
        IAction ITriableAction.SwallowIf<Exception1, Exception2, Exception3, Exception4>()
        {
            try
            {
                this.Action();
            }
            catch (Exception1) { }
            catch (Exception2) { }
            catch (Exception3) { }
            catch (Exception4) { }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and catches any exception and performs the specified action for the catch.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction ITriableAction.Catch(Action<Exception> action)
        {
            try
            {
                this.Action();
            }
            catch (Exception exception)
            {
                action(exception);
            }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and catches the specified exception(s) and performs the specified action for each catch.
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <param name="action1"></param>
        /// <returns></returns>
        IAction ITriableAction.Catch<Exception1>(Action<Exception1> action1)
        {
            try
            {
                this.Action();
            }
            catch (Exception1 exception)
            {
                action1(exception);
            }
            return this;
        }
        /// <summary>
        /// Performs the previously chained Try action and catches the specified exception(s) and performs the specified action for each catch.
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <param name="action1"></param>
        /// <param name="action2"></param>
        /// <returns></returns>
        IAction ITriableAction.Catch<Exception1, Exception2>(Action<Exception1> action1, Action<Exception2> action2)
        {
            try
            {
                this.Action();
            }
            catch (Exception1 exception)
            {
                action1(exception);
            }
            catch (Exception2 exception)
            {
                action2(exception);
            }
            return this;
        }
        /// <summary>
        /// Prepares a Case statement for the previously chained Switch statement, this requires the usage of Execute after it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareOperand"></param>
        /// <returns></returns>
        ISwitchCaseBuilder ISwitchBuilder.Case<T>(T compareOperand)
        {
            this.SwitchCases.Add(new CaseInfo(compareOperand, null));
            return this;
        }
        /// <summary>
        /// Prepares a Case statement for the previously chained Switch statement, this requires the usage of Execute after it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISwitchTypeCaseBuilder ISwitchTypeBuilder.Case<T>()
        {
            this.SwitchCases.Add(new CaseInfo(typeof(T), null));
            return this;
        }
        /// <summary>
        /// Prepares for the execution of the specified action in case its chained Case has been evaluated.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ISwitchBuilder ISwitchCaseBuilder.Execute(Action action)
        {
            this.SwitchCases.Last().Action = action;
            return this;
        }

        /// <summary>
        /// Prepares for the execution of the specified action in case its chained Case has been evaluated.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ISwitchTypeBuilder ISwitchTypeCaseBuilder.Execute(Action action)
        {
            this.SwitchCases.Last().Action = action;
            return this;
        }
        /// <summary>
        /// Performs the previously chained switch statement along with its chained cases.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction ISwitchBuilder.Default(Action action)
        {
            bool excuteDefault = true;
            foreach (var switchCase in this.SwitchCases)
            {
                if (object.Equals(this.SwitchMainOperand, switchCase.Operand))
                {
                    switchCase.Action();
                    excuteDefault = false;
                    break;
                }
            }
            if (excuteDefault)
                action();
            return this;
        }
        /// <summary>
        /// Performs the previously chained switch statement along with its chained cases.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction ISwitchTypeBuilder.Default(Action action)
        {
            bool executeDefault = true;
            foreach (var switchCase in this.SwitchCases)
            {
                if (object.Equals(this.SwitchMainOperand, switchCase.Operand))
                {
                    switchCase.Action();
                    executeDefault = false;
                    break;
                }
            }
            if (executeDefault)
                action();
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trueAction"></param>
        /// <param name="falseAction"></param>
        public static void TernaryOperator(bool condition, Action trueAction, Action falseAction)
        {
            if (condition)
            {
                trueAction();
            }
            else
            {
                falseAction();
            }
        }
        /// <summary>
        /// Returns a random value of boolean
        /// </summary>
        /// <returns></returns>
        public static bool RandomBoolean()
        {
            return _random.Next(0, 1) == 1;
        }
        /// <summary>
        /// Returns a random value of boolean
        /// </summary>
        /// <returns></returns>
        public static bool FlipCoin()
        {
            return RandomBoolean();
        }
        /// <summary>
        /// Returns a random string with a specified length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length = 8)
        {            
            return new string(Enumerable.Repeat(alphabetCharacters, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Private class to hold information about switch case statement.
        /// </summary>
        private class CaseInfo
        {
            public object Operand { get; set; }

            public Action Action { get; set; }

            public CaseInfo(object operand, Action action, bool isDefault = false)
            {
                Operand = operand;
                Action = action;
            }
        }
    }
}
