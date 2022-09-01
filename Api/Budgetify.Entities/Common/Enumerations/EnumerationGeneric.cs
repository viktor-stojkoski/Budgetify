namespace Budgetify.Entities.Common.Enumerations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Base generic enumeration class.
/// </summary>
/// <typeparam name="T">Type of the Id.</typeparam>
public abstract class Enumeration<T> : IComparable
{
    protected Enumeration(T id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Enumeration id.
    /// </summary>
    public T Id { get; }

    /// <summary>
    /// Enumeration string value.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Returns all the enumerations for the given type.
    /// </summary>
    public static IEnumerable<TResult> GetAll<TResult>() where TResult : Enumeration<T>
    {
        return typeof(TResult)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null))
            .Cast<TResult>();
    }

    public static bool operator ==(object left, Enumeration<T> right) =>
        left is not Enumeration<T> ? right is null : left.Equals(right);

    public static bool operator !=(object left, Enumeration<T> right) =>
        left is not Enumeration<T> ? right is null : !(left == right);

    public int CompareTo(object? obj) =>
        obj is null ? 1 : Name.CompareTo(((Enumeration<T>)obj).Name);

    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<T> otherValue)
        {
            return false;
        }

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Name.Equals(otherValue.Name);

        return typeMatches && valueMatches;
    }
}
