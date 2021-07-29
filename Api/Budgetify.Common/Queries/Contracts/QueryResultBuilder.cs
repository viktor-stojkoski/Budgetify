namespace Budgetify.Common.Queries
{
    using System.Net;

    using Budgetify.Common.Extensions;
    using Budgetify.Common.Results;

    public class QueryResultBuilder<TResult>
    {
        private TResult? _value;

        private bool _isSuccess = true;

        private string _message = string.Empty;

        private HttpStatusCode _httpStatusCode = HttpStatusCode.OK;

        public void SetValue(TResult? value) => _value = value;

        public void SetMessage(string message) => _message = message;

        public void SetStatusCode(HttpStatusCode httpStatusCode) => _httpStatusCode = httpStatusCode;

        public QueryResult<TResult> FailWith(string message)
        {
            SetMessage(message);

            return Build();
        }

        public QueryResult<TResult> FailWith(Result result)
        {
            SetMessage(result.Message);
            SetStatusCode(result.HttpStatusCode);

            return Build();
        }

        public QueryResult<TResult> Build()
        {
            if (_message.HasValue())
            {
                _isSuccess = false;
                _httpStatusCode = _httpStatusCode is HttpStatusCode.OK
                    ? HttpStatusCode.BadRequest
                    : _httpStatusCode;
            }

            return new QueryResult<TResult>(_isSuccess, _message, _value, _httpStatusCode);
        }
    }
}
