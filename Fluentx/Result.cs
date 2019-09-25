using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A class that represents a proper data with structured schema for info, wanrings and errors
    /// </summary>
    public class Result
    {
        /// <summary>
        /// List of error messages
        /// </summary>
        public IList<string> ErrorMessages { get; set; }
        /// <summary>
        /// List of warning messages
        /// </summary>
        public IList<string> WarningMessages { get; set; }
        /// <summary>
        /// List of info messages
        /// </summary>
        public IList<string> InfoMessages { get; set; }
        /// <summary>
        /// Returns if there is any errors
        /// </summary>
        public bool HasErrors { get { return ErrorMessages.IsNotNullOrEmpty(); } }
        /// <summary>
        /// Return if there is any warnings
        /// </summary>
        public bool HasWarnings { get { return WarningMessages.IsNotNullOrEmpty(); } }
        /// <summary>
        /// Returns if there is any infos
        /// </summary>
        public bool HasInfos { get { return InfoMessages.IsNotNullOrEmpty(); } }
        /// <summary>
        /// Constructor
        /// </summary>
        public Result()
        {

        }
        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(params string[] errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// Returns an empty result object
        /// </summary>
        /// <returns></returns>
        public static Result Return()
        {
            return new Result();
        }
        /// <summary>
        /// Returns a result object with the specified error messages
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result Error(params string[] errorMessages)
        {
            return new Result(errorMessages);
        }
        /// <summary>
        /// Returns a result object with the specified error messages
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result Error(IEnumerable<string> errorMessages)
        {
            return new Result(errorMessages);
        }
        /// <summary>
        /// Returns a result object with the specified data
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
        /// Returns a result object with the specified data
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
        /// Returns a result object with the specified error messages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> Error<T>(params string[] errorMessages)
        {
            return new Result<T>(errorMessages);
        }
        /// <summary>
        /// Returns a result object with the specified error messages
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
    /// Represent a result object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Data persisted with the results objects that to be returned
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Result()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public Result(T data)
        {
            Data = data;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(params string[] errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessages"></param>
        public Result(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessages"></param>
        public Result(T data, params string[] errorMessages)
        {
            Data = data;
            ErrorMessages = errorMessages?.ToList();
        }
        /// <summary>
        /// Constructor
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
