namespace Budgetify.Queries.Infrastructure.Configuration;

using Budgetify.Queries.Common.Configuration;
using Budgetify.Queries.ExchangeRate.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ExchangeRateConfiguration : EntityTypeConfigurationBase<ExchangeRate>, IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ToTable("ExchangeRate", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.FromCurrencyId).HasColumnName("FromCurrencyFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.ToCurrencyId).HasColumnName("ToCurrencyFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.FromDate).HasColumnName("FromDate").HasColumnType("datetime2(6)");
        builder.Property(x => x.ToDate).HasColumnName("ToDate").HasColumnType("datetime2(6)");
        builder.Property(x => x.Rate).HasColumnName("Rate").HasColumnType("decimal(10,4)").IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.ExchangeRates).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.FromCurrency).WithMany(x => x.ExchangeRatesFromCurrency).HasForeignKey(x => x.FromCurrencyId);
        builder.HasOne(x => x.ToCurrency).WithMany(x => x.ExchangeRatesToCurrency).HasForeignKey(x => x.ToCurrencyId);
    }
}
