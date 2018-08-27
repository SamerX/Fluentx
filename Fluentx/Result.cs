using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// 
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<string> ErrorMessages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasErrors { get { return ErrorMessages.IsNotNullOrEmpty(); } }
        /// <summary>
        /// 
        /// </summary>
        public Result()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(params string[] errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Result Return()
        {
            return new Result();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result Error(params string[] errorMessages)
        {
            return new Result(errorMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result Error(IEnumerable<string> errorMessages)
        {
            return new Result(errorMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Return<T>(T data, params string[] errorMessages)
        {
            return new Result<T>(data, errorMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Return<T>(T data, IEnumerable<string> errorMessages)
        {
            return new Result<T>(data, errorMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Error<T>(params string[] errorMessages)
        {
            return new Result<T>(errorMessages);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Error<T>(IEnumerable<string> errorMessages)
        {
            return new Result<T>(errorMessages);
        }


    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Result()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Result(T data)
        {
            Data = data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(params string[] errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessages"></param>
        public Result(T data, params string[] errorMessages)
        {
            Data = data;
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessages"></param>
        public Result(T data, IEnumerable<string> errorMessages)
        {
            Data = data;
            ErrorMessages = errorMessages?.ToList();
        }
    }
}
