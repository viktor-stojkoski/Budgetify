namespace Budgetify.Common.Commands
{
    using System.Net;

    public class CommandResult<TValue>
    {
        internal CommandResult(
            bool isSuccess,
            string? message,
            TValue? value,
            HttpStatusCode httpStatusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
            HttpStatusCode = httpStatusCode;
            IsEmpty = value?.GetType().Equals(typeof(EmptyValue)) ?? true;
        }

        public TValue? Value { get; }

        public string? Message { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public bool IsEmpty { get; }

        public HttpStatusCode HttpStatusCode { get; }

        public bool IsNotFound => IsFailure && HttpStatusCode is HttpStatusCode.NotFound;
    }

    public class CommandResult : CommandResult<EmptyValue>
    {
        internal CommandResult(bool isSuccess, string? message, HttpStatusCode httpStatusCode)
            : base(isSuccess: isSuccess, message: message, value: null, httpStatusCode: httpStatusCode) { }
    }
}
