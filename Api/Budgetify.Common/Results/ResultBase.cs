namespace Budgetify.Common.Results;

using System;
using System.Net;

using Budgetify.Common.Extensions;

public abstract class ResultBase
{
    public bool IsFailure { get; }

    public bool IsFailureOrNull { get; }

    public string Message { get; }

    public bool IsSuccess => !IsFailure;

    public bool IsNotFound => IsFailure && HttpStatusCode is HttpStatusCode.NotFound;

    public ResultType ResultType { get; }

    public HttpStatusCode HttpStatusCode => ResultType switch
    {
        ResultType.Ok => HttpStatusCode.OK,
        ResultType.BadRequest => HttpStatusCode.BadRequest,
        ResultType.Unauthorized => HttpStatusCode.Unauthorized,
        ResultType.Forbidden => HttpStatusCode.Forbidden,
        ResultType.NotFound => HttpStatusCode.NotFound,
        ResultType.Conflict => HttpStatusCode.Conflict,
        _ => HttpStatusCode.InternalServerError,
    };

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
