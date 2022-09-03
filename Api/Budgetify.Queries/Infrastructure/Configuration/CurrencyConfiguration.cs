namespace Budgetify.Queries.Infrastructure.Configuration;

using Budgetify.Queries.Common.Configuration;
using Budgetify.Queries.Currency.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class CurrencyConfiguration : EntityTypeConfigurationBase<Currency>, IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currency", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Code).HasColumnName("Code").HasColumnType("nvarchar(3)").IsRequired();
        builder.Property(x => x.Symbol).HasColumnName("Symbol").HasColumnType("nvarchar(10)");
    }
}
