namespace Budgetify.Entities.Tests;

using System;
using System.Linq;

public static class CommonTestsHelper
{
    private static readonly Random randomId = new(1);
    private static readonly Random randomString = new();

    internal static int RandomId() => randomId.Next(1, int.MaxValue);

    internal static string RandomString(int length) =>
        string.Join(
            separator: "",
            values: Enumerable.Repeat(0, length).Select(x => (char)randomString.Next(33, 127)));
}
