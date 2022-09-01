namespace Budgetify.Queries.Common.Configuration;

using Budgetify.Queries.Common.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EntityTypeConfigurationBase<TEntity> where TEntity : Entity
{
    public void ConfigureDefaultColumns(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("smalldatetime").IsRequired();
        builder.Property(x => x.DeletedOn).HasColumnName("DeletedOn").HasColumnType("smalldatetime");
    }
}
