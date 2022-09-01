namespace Budgetify.Common.Results;

using System.Collections.Generic;
using System.Linq;

public class Result : ResultBase
{
    private Result()
        : base(resultType: ResultType.Ok, isFailure: false, isFailureOrNull: false, message: string.Empty) { }

    internal Result(ResultType resultType, string message)
        : base(resultType: resultType, isFailure: true, isFailureOrNull: true, message: message) { }

    protected Result(object? value, ResultType resultType, string message)
        : base(resultType, isFailure: false, isFailureOrNull: value is null, message) { }

    public static Result Ok() => new();

    public static Result Invalid(string message) => new(ResultType.BadRequest, message);

    public static Result Unauthorized(string message) => new(ResultType.Unauthorized, message);

    public static Result Forbidden(string message) => new(ResultType.Forbidden, message);

    public static Result NotFound(string message) => new(ResultType.NotFound, message);

    public static Result Conflicted(string message) => new(ResultType.Conflict, message);

    public static Result Failed(string message) => new(ResultType.InternalServerError, message);

    public static Result<T> Ok<T>(T value) => new(value);

    public static Result<T> Invalid<T>(string message) => new(ResultType.BadRequest, message);

    public static Result<T> Unauthorized<T>(string message) => new(ResultType.Unauthorized, message);

    public static Result<T> Forbidden<T>(string message) => new(ResultType.Forbidden, message);

    public static Result<T> NotFound<T>(string message) => new(ResultType.NotFound, message);

    public static Result<T> Conflicted<T>(string message) => new(ResultType.Conflict, message);

    public static Result<T> Failed<T>(string message) => new(ResultType.InternalServerError, message);

    public static Result FirstFailureOrOk(params Result[] results) =>
        results.Any(x => x.IsFailure) ? results.First(x => x.IsFailure) : Ok();

    public static Result FirstFailureNullOrOk(params Result[] results) =>
        results.Any(x => x.IsFailure || x.IsFailureOrNull) ? results.First(x => x.IsFailure || x.IsFailureOrNull) : Ok();

    public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results) =>
        results.Any(x => x.IsFailure) ? results.First(x => x.IsFailure) : Ok();

    public static Result<T> FromError<T>(ResultBase result) => new(result.ResultType, result.Message);
}
