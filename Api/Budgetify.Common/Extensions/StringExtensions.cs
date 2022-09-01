namespace Budgetify.Common.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public static class StringExtensions
{
    /// <summary>
    /// Indicates whether a specified string is null, empty, or contains only whitespace characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is null or System.String.Empty, 
    /// or if value contains only whitespace characters.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Indicates whether a specified string is not null, 
    /// not empty, and does not contain only whitespace characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns>
    /// true if the value parameter is not null or not System.String.Empty, 
    /// or if value does not contain only whitespace characters.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasValue(this string? value) => !value.IsEmpty();
}
