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

        public static Result<T> Ok<T>(T value) => new(value);

        public static Result FirstFailureNullOrOk(params Result[] results)
        {
            if (results.Any(x => x.IsFailure || x.IsFailureOrNull))
            {
                return results.First(x => x.IsFailure || x.IsFailureOrNull);
            }

            return Ok();
        }

        public static Result FirstFailureOrOk(params Result[] results) =>
            results.Any(x => x.IsFailure) ? results.First(x => x.IsFailure) : Ok();

        public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results) =>
            results.Any(x => x.IsFailure) ? results.First(x => x.IsFailure) : Ok();

        public static Result<T> FromError<T>(ResultBase result) => new(result.ResultType, result.Message);
    }

    public class Result<T> : ResultBase
    {
        private static readonly T? Empty = default;

        [AllowNull]
        public T Value { get; }

        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        [MemberNotNullWhen(false, nameof(Value))]
        public new bool IsFailureOrNull { get; }

        internal Result(ResultType resultType, string message)
            : base(resultType: resultType, isFailure: true, isFailureOrNull: true, message: message) => Value = default;

        internal Result(T value)
            : base(resultType: ResultType.Ok, isFailure: false, isFailureOrNull: value is null, message: string.Empty) => Value = value;

        public static implicit operator Result(Result<T> result) =>
            result.IsSuccess ? Result.Ok() : new Result(result.ResultType, result.Message);
    }

    public abstract class ResultBase
    {
        public bool IsFailure { get; }

        public bool IsFailureOrNull { get; }

        public string Message { get; }

        public bool IsSuccess => !IsFailure;

        public bool IsNotFound => IsFailure && HttpStatusCode is HttpStatusCode.NotFound;

        public ResultType ResultType { get; }

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
            IsFailureOrNull = isFailureOrNull;
            Message = message;
        }
    }
}
