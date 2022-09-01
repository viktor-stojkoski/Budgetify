namespace Budgetify.Common.Results;

using System.Diagnostics.CodeAnalysis;

public class Result<T> : Result
{
    private static readonly T? Empty = default;

    [AllowNull]
    public T Value { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsEmpty => Value?.Equals(Empty) ?? true;

    internal Result(ResultType resultType, string message)
        : base(resultType: resultType, message: message) => Value = default;

    internal Result(T value)
        : base(value: value, resultType: ResultType.Ok, message: string.Empty) => Value = value;
}
