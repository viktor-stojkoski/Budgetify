namespace Budgetify.Entities.Common.Enumerations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Base enumeration class.
/// </summary>
public abstract class Enumeration : IComparable
{
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Enumeration id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Enumeration string value.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Returns all the enumerations for the given type.
    /// </summary>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return typeof(T)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null))
            .Cast<T>();
    }

    public static bool operator ==(object left, Enumeration right) =>
        left is not Enumeration ? right is null : left.Equals(right);

    public static bool operator !=(object left, Enumeration right) =>
        left is not Enumeration ? right is null : !(left == right);

    public int CompareTo(object? obj) =>
        obj is null ? 1 : Name.CompareTo(((Enumeration)obj).Name);

    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Name.Equals(otherValue.Name);

        return typeMatches && valueMatches;
    }
}
