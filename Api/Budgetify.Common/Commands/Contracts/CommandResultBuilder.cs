namespace Budgetify.Common.Commands
{
    using System.Net;

    using Budgetify.Common.Extensions;
    using Budgetify.Common.Results;

    public class CommandResultBuilder<TValue>
    {
        private TValue? _value;

        private bool _isSuccess = true;

        private string _message = string.Empty;

        private HttpStatusCode _httpStatusCode = HttpStatusCode.OK;

        public void SetValue(TValue? value) => _value = value;

        public void SetMessage(string message) => _message = message;

        public void SetStatusCode(HttpStatusCode httpStatusCode) => _httpStatusCode = httpStatusCode;

        public CommandResult<TValue> FailWith(string message)
        {
            SetMessage(message);

            return Build();
        }

        public CommandResult<TValue> FailWith(Result result)
        {
            SetMessage(result.Message);
            SetStatusCode(result.HttpStatusCode);

            return Build();
        }

        public CommandResult<TValue> Build()
        {
            if (_message.HasValue())
            {
                _isSuccess = false;
                _httpStatusCode = _httpStatusCode is HttpStatusCode.OK
                    ? HttpStatusCode.BadRequest
                    : _httpStatusCode;
            }

            return new CommandResult<TValue>(_isSuccess, _message, _value, _httpStatusCode);
        }
    }

    public class CommandResultBuilder : CommandResultBuilder<EmptyValue>
    {
        public CommandResultBuilder() => SetValue(null);
    }
}
