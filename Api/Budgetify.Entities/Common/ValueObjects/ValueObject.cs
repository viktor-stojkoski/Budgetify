namespace Budgetify.Entities.Common.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Base Value Object class.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Determines the properties for comparing.
    /// </summary>
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject
            && GetType() == obj.GetType()
                && GetEqualityComponents()
                    .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return (current * 23) + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(ValueObject a, ValueObject b) =>
        (a is null && b is null) || (a is not null && b is not null && a.Equals(b));

    public static bool operator !=(ValueObject a, ValueObject b) => !(a == b);
}
