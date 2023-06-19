namespace Budgetify.Queries.Infrastructure.Configuration;

using Budgetify.Queries.Budget.Entities;
using Budgetify.Queries.Common.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class BudgetConfiguration : EntityTypeConfigurationBase<Budget>, IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budget", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.CategoryId).HasColumnName("CategoryFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.StartDate).HasColumnName("StartDate").HasColumnType("datetime2(0)").IsRequired();
        builder.Property(x => x.EndDate).HasColumnName("EndDate").HasColumnType("datetime2(0)").IsRequired();
        builder.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("decimal(19,4)").IsRequired();
        builder.Property(x => x.AmountSpent).HasColumnName("AmountSpent").HasColumnType("decimal(19,4)").IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.Budgets).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Category).WithMany(x => x.Budgets).HasForeignKey(x => x.CategoryId);
    }
}
