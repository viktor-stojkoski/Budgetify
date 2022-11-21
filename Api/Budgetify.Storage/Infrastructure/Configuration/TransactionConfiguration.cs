namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Common.Configuration;
using Budgetify.Storage.Transaction.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TransactionConfiguration : EntityTypeConfigurationBase<Transaction>, IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.AccountId).HasColumnName("AccountFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.CategoryId).HasColumnName("CategoryFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.CurrencyId).HasColumnName("CurrencyFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.MerchantId).HasColumnName("MerchantFk").HasColumnType("int");
        builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("decimal(19,4)").IsRequired();
        builder.Property(x => x.Date).HasColumnName("Date").HasColumnType("datetime2(0)").IsRequired();
        builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar(MAX)");

        builder.HasOne(x => x.User).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Account).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountId);
        builder.HasOne(x => x.Category).WithMany(x => x.Transactions).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Currency).WithMany(x => x.Transactions).HasForeignKey(x => x.CurrencyId);
        builder.HasOne(x => x.Merchant).WithMany(x => x.Transactions).HasForeignKey(x => x.MerchantId);

        builder.Ignore(x => x.DomainEvents);
    }
}
