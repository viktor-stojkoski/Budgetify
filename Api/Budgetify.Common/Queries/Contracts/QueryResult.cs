namespace Budgetify.Common.Queries
{
    using System.Net;

    public class QueryResult<TResult>
    {
        internal QueryResult(
            bool isSuccess,
            string? message,
            TResult? value,
            HttpStatusCode httpStatusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
            HttpStatusCode = httpStatusCode;
            IsEmpty = Equals(value, default(TResult));
        }

        public TResult? Value { get; }

        public string? Message { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public bool IsEmpty { get; }

        public HttpStatusCode HttpStatusCode { get; }

        public bool IsNotFound => IsFailure && HttpStatusCode is HttpStatusCode.NotFound;
    }
}
