using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Application.Functionals
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        //public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string message)
        {
            if (isSuccess && message != string.Empty)
                throw new InvalidOperationException();
            if (!isSuccess && message == string.Empty)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            //foreach (Result result in results)
            //{
            //    if (result.IsFailure)
            //        return result;
            //}

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private readonly T _data;
        public T Data
        {
            get
            {
                //if (!IsSuccess)
                //    throw new InvalidOperationException();

                return _data;
            }
        }

        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            _data = value;
        }
    }
}
