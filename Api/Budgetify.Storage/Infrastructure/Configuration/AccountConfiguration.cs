namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Account.Entities;
using Budgetify.Storage.Common.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class AccountConfiguration : EntityTypeConfigurationBase<Account>, IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Balance).HasColumnName("Balance").HasColumnType("decimal(19,4)").IsRequired();
        builder.Property(x => x.CurrencyId).HasColumnName("CurrencyFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("nvarchar(MAX)");

        builder.HasOne(x => x.User).WithMany(x => x.Accounts).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Currency).WithMany(x => x.Accounts).HasForeignKey(x => x.CurrencyId);

        builder.Ignore(x => x.DomainEvents);
    }
}
