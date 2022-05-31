namespace Budgetify.Common.Results
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;

    using Budgetify.Common.Extensions;

    public class Result : ResultBase
    {
        private Result()
            : base(resultType: ResultType.Ok, isFailure: false, isFailureOrNull: false, message: string.Empty) { }

        internal Result(ResultType resultType, string message)
            : base(resultType: resultType, isFailure: true, isFailureOrNull: true, message: message) { }

        public static Result Conflicted(string message) => new(ResultType.Conflicted, message);

        public static Result Failed(string message) => new(ResultType.InternalError, message);

        public static Result Forbidden(string message) => new(ResultType.Forbidden, message);

        public static Result Invalid(string message) => new(ResultType.Invalid, message);

        public static Result NotFound(string message) => new(ResultType.NotFound, message);

        public static Result Unauthorized(string message) => new(ResultType.Unauthorized, message);

        public static Result Ok() => new();

        public static Result<T> Conflicted<T>(string message) => new(ResultType.Conflicted, message);

        public static Result<T> Failed<T>(string message) => new(ResultType.InternalError, message);

        public static Result<T> Forbidden<T>(string message) => new(ResultType.Forbidden, message);

        public static Result<T> Invalid<T>(string message) => new(ResultType.Invalid, message);

        public static Result<T> NotFound<T>(string message) => new(ResultType.NotFound, message);

        public static Result<T> Unauthorized<T>(string message) => new(ResultType.Unauthorized, message);

        public static Result<T> Ok<T>([NotNullIfNotNull("value")][DisallowNull] T value) => new(value);

        public static Result FirstFailureNullOrOk(params dynamic[] results)
        {
            //List<Result<object>> list = (List<Result<object>>)results.Select(x => (Result<object>)Convert.ChangeType(x, x.GetType()));

            if (results.Any(x => x.IsFailure || x.IsFailureOrNull))
            {
                return results.First(x => x.IsFailure || x.IsFailureOrNull);
            }

            return Ok();
        }

        public static Result FirstFailureOrOk(params Result[] results)
        {
            if (results.Any(x => x.IsFailure))
            {
                return results.First(x => x.IsFailure);
            }

            return Ok();
        }

        public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results)
        {
            if (results.Any(x => x.IsFailure))
            {
                return results.First(x => x.IsFailure);
            }

            return Ok();
        }

        public static Result<T> FromError<T>(ResultBase result) => new(result.ResultType, result.Message);
    }

    public class Result<T> : ResultBase
    {
        private static readonly T? Empty = default;

        //[NotNull]
        public T? Value { get; }

        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        [MemberNotNullWhen(false, nameof(Value))]
        public bool IsFailureOrNull { get; }

        internal Result(ResultType resultType, string message)
            : base(resultType: resultType, isFailure: true, isFailureOrNull: true, message: message) => Value = default;

        internal Result([NotNullIfNotNull("value")][NotNullIfNotNull("Value")][DisallowNull] T value)
            : base(
                  resultType: ResultType.Ok,
                  isFailure: false,
                  isFailureOrNull: value == null,
                  message: string.Empty,
                  value: value,
                  type: typeof(T))
        {
            Value = value;
            //Value = value;
            //Type = typeof(T);

            //Value = Convert.ChangeType(Value, Type);
        }

        //public static Result IsNull(params Result[] results)
        //{
        //    return results.First(x => x.IsFailureOrNull) ?? Result.Ok();
        //}

        //public bool AreFailureOrNull<TResults>(this Result<T> result, params Result<TResults>[] results)
        //{
        //    return result.IsFailureOrNull || results.Any(x => x.IsFailureOrNull);
        //}

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Result.Ok();
            }

            return new Result(result.ResultType, result.Message);
        }

        //public override Type? Type { get; }

        //public static implicit operator T(Result<T> result) => result.Value;

        //[NotNullIfNotNull(nameof(Value))]
        //public T? Data => (T?)Value;

        //[MemberNotNull(nameof(Value))]
        //public static implicit operator T(Result<T> result)
        //{
        //    //#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //    //#pragma warning disable CS8603 // Possible null reference return.
        //    return (T)result.Value;
        //    //#pragma warning restore CS8603 // Possible null reference return.
        //    //#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        //}

        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        //public static implicit operator Result<T>(T value) => Result.Ok(value);

        //public override object? Value { get; }
        //{
        //    get
        //    {
        //        return Convert.ChangeType(Value, Type);
        //    }
        //}
    }

    public abstract class ResultBase
    {
        public bool IsFailure { get; }

        //[MemberNotNullWhen(false, nameof(Value), nameof(Type))]
        //public abstract bool IsFailureOrNull { get; }

        public string Message { get; }

        public bool IsSuccess => !IsFailure;

        public bool IsNotFound => IsFailure && HttpStatusCode is HttpStatusCode.NotFound;

        public ResultType ResultType { get; }

        //public Type? Type { get; }

        //public dynamic? Value
        //{
        //    get => Type is not null ? Convert.ChangeType(Value, Type) : null;

        //    set
        //    {

        //    }
        //}

        public HttpStatusCode HttpStatusCode
        {
            get
            {
                HttpStatusCode statusCode = ResultType switch
                {
                    ResultType.Ok => HttpStatusCode.OK,
                    ResultType.NotFound => HttpStatusCode.NotFound,
                    ResultType.Forbidden => HttpStatusCode.Forbidden,
                    ResultType.Conflicted => HttpStatusCode.Conflict,
                    ResultType.Invalid => HttpStatusCode.NotAcceptable,
                    ResultType.Unauthorized => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError,
                };

                return statusCode;
            }
        }

        protected ResultBase(ResultType resultType, bool isFailure, bool isFailureOrNull, string message)
        {
            if (isFailure)
            {
                if (message.IsEmpty())
                {
                    throw new ArgumentNullException(nameof(message), "There must be error message for failure.");
                }

                if (resultType is ResultType.Ok)
                {
                    throw new ArgumentException("There should be error type for failure.", nameof(resultType));
                }
            }
            else
            {
                if (message.HasValue())
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(message));
                }

                if (resultType is not ResultType.Ok)
                {
                    throw new ArgumentException("There should be no error type for success.", nameof(resultType));
                }
            }

            ResultType = resultType;
            IsFailure = isFailure;
            Message = message;
        }

        protected ResultBase(
            ResultType resultType,
            bool isFailure,
            bool isFailureOrNull,
            string message,
            Type type,
            dynamic? value)
        {
            if (isFailure)
            {
                if (message.IsEmpty())
                {
                    throw new ArgumentNullException(nameof(message), "There must be error message for failure.");
                }

                if (resultType is ResultType.Ok)
                {
                    throw new ArgumentException("There should be error type for failure.", nameof(resultType));
                }
            }
            else
            {
                if (message.HasValue())
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(message));
                }

                if (resultType is not ResultType.Ok)
                {
                    throw new ArgumentException("There should be no error type for success.", nameof(resultType));
                }
            }

            ResultType = resultType;
            IsFailure = isFailure;
            Message = message;
            //Type = type;
            //Value = value;
        }
    }

    public interface IResult
    {
        Type? Type { get; }

        object? Value { get; }

        bool IsSuccess => !IsFailure;

        bool IsFailure { get; }

        [MemberNotNullWhen(true, nameof(Value), nameof(Type))]
        bool IsFailureOrNull { get; }

        string? Message { get; }
    }

    //public static class ResultExtensions
    //{
    //    public static Result IsNull<T, TResults[]>(Result<T> result, params Result<TResults[]>[] results)
    //    {
    //        return Result.FromError<T>(result);
    //    }
    //}
}
