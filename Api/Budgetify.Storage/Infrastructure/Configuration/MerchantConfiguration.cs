namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Common.Configuration;
using Budgetify.Storage.Merchant.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class MerchantConfiguration : EntityTypeConfigurationBase<Merchant>, IEntityTypeConfiguration<Merchant>
{
    public void Configure(EntityTypeBuilder<Merchant> builder)
    {
        builder.ToTable("Merchant", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.CategoryId).HasColumnName("CategoryFk").HasColumnType("int").IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.Merchants).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Category).WithMany(x => x.Merchants).HasForeignKey(x => x.CategoryId);

        builder.Ignore(x => x.DomainEvents);
    }
}
