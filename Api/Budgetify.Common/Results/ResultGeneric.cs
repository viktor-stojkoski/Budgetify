namespace Budgetify.Common.Results
{
    using System.Diagnostics.CodeAnalysis;

    public class Result<T> : ResultBase
    {
        private static readonly T? Empty = default;

        [AllowNull]
        public T Value { get; }

        [MemberNotNullWhen(false, nameof(Value))]
        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        //[MemberNotNullWhen(false, nameof(Value))]
        public new bool IsFailureOrNull { get; }

        internal Result(ResultType resultType, string message)
            : base(resultType: resultType, isFailure: true, isFailureOrNull: true, message: message) => Value = default;

        internal Result(T value)
            : base(resultType: ResultType.Ok, isFailure: false, isFailureOrNull: value is null, message: string.Empty)
        {
            IsFailureOrNull = value is null;
            Value = value;
        }

        public static implicit operator Result(Result<T> result) =>
            result.IsSuccess ? Result.Ok() : new Result(result.ResultType, result.Message);
    }
}
