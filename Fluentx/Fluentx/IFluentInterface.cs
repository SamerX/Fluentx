using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// Represnt the core of a fluent interface in fluentx
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
        /// <summary>
        /// Redeclaration that hides the <see cref="object.GetType()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Redeclaration that hides the <see cref="object.GetHashCode()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        /// <summary>
        /// Redeclaration that hides the <see cref="object.ToString()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
        /// <summary>
        /// Redeclaration that hides the <see cref="object.Equals(object)"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
    /// <summary>
    /// Represnt an Action for fluentx
    /// </summary>
    public interface IAction : IFluentInterface
    {
        
    }
    /// <summary>
    /// Any condition builder.
    /// </summary>
    public interface IConditionBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IConditionalAction Then(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder And(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder And(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder AndNot(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder AndNot(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Or(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Or(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder OrNot(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder OrNot(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Xor(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Xor(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Xnor(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder Xnor(bool condition);
    }
    /// <summary>
    /// Any condition action.
    /// </summary>
    public interface IConditionalAction : IAction, IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction Else(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder ElseIf(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IConditionBuilder ElseIf(bool condition);

    }
    /// <summary>
    /// Any loop action.
    /// </summary>
    public interface ILoopAction : IAction, IFluentInterface
    {

    }
    /// <summary>
    /// Any early loop builder (e.g while)
    /// </summary>
    public interface IEarlyLoopBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ILoopAction Do(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop LateBreakOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop EarlyBreakOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop LateContinueOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEarlyLoop EarlyContinueOn(Func<bool> condition);

        //IEarlyLoop EarlyContinueOn(bool condition);
    }
    /// <summary>
    /// Any early loop (e.g while).
    /// </summary>
    public interface IEarlyLoop : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ILoopAction Do(Action action);
    }
    /// <summary>
    /// Any late loop (e.g Do-While).
    /// </summary>
    public interface ILateLoop : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILoopAction While(Func<bool> condition);
    }
    /// <summary>
    /// Any late loop builder (e.g Do-While)
    /// </summary>
    public interface ILateLoopBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILoopAction While(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop LateBreakOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop EarlyBreakOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop LateContinueOn(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ILateLoop EarlyContinueOn(Func<bool> condition);
    }
    /// <summary>
    /// Any action might or might not complete successfully.
    /// </summary>
    public interface ITriableAction : IAction, IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAction Swallow();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <returns></returns>
        IAction SwallowIf<Exception1>() where Exception1 : Exception;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <returns></returns>
        IAction SwallowIf<Exception1, Exception2>()
            where Exception1 : Exception
            where Exception2 : Exception;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <typeparam name="Exception3"></typeparam>
        /// <returns></returns>
        IAction SwallowIf<Exception1, Exception2, Exception3>()
            where Exception1 : Exception
            where Exception2 : Exception
            where Exception3 : Exception;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Exception1"></typeparam>
        /// <typeparam name="Exception2"></typeparam>
        /// <typeparam name="Exception3"></typeparam>
        /// <typeparam name="Exception4"></typeparam>
        /// <returns></returns>
        IAction SwallowIf<Exception1, Exception2, Exception3, Exception4>()
            where Exception1 : Exception
            where Exception2 : Exception
            where Exception3 : Exception
            where Exception4 : Exception;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction Catch(Action<Exception> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction Catch<T>(Action<T> action) where T : Exception;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="action1"></param>
        /// <param name="action2"></param>
        /// <returns></returns>
        IAction Catch<T1, T2>(Action<T1> action1, Action<T2> action2)
            where T1 : Exception
            where T2 : Exception;


    }
    /// <summary>
    /// Switch statement builder.
    /// </summary>
    public interface ISwitchBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareOperand"></param>
        /// <returns></returns>
        ISwitchCaseBuilder Case<T>(T compareOperand);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction Default(Action action);
    }
    
    /// <summary>
    /// Switch statement for Types builder.
    /// </summary>
    public interface ISwitchTypeBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISwitchTypeCaseBuilder Case<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IAction Default(Action action);

    }
    /// <summary>
    /// Switch case statement builder.
    /// </summary>
    public interface ISwitchCaseBuilder: IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ISwitchBuilder Execute(Action action);
        
    }
    
    /// <summary>
    /// Switch case statement for types builder.
    /// </summary>
    public interface ISwitchTypeCaseBuilder : IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ISwitchTypeBuilder Execute(Action action);
    }
}
