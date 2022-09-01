namespace Budgetify.Storage.Common.Extensions;

using Budgetify.Entities.Common.Enumerations;

public static class EnumExtensions
{
    /// <summary>
    /// Returns <see cref="Microsoft.EntityFrameworkCore.EntityState"/> .NET EntityState from a given <see cref="EntityState"/> Budgetify EntityState.
    /// </summary>
    public static Microsoft.EntityFrameworkCore.EntityState GetState(this EntityState entityState)
    {
        return entityState switch
        {
            EntityState.Added => Microsoft.EntityFrameworkCore.EntityState.Added,
            EntityState.Deleted => Microsoft.EntityFrameworkCore.EntityState.Deleted,
            EntityState.Modified => Microsoft.EntityFrameworkCore.EntityState.Modified,
            _ => Microsoft.EntityFrameworkCore.EntityState.Unchanged,
        };
    }
}
