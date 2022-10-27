namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Category.Entities;
using Budgetify.Storage.Common.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class CategoryConfiguration : EntityTypeConfigurationBase<Category>, IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.UserId).HasColumnName("UserFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.Type).HasColumnName("Type").HasColumnType("nvarchar(50)").IsRequired().HasDefaultValue("");

        builder.HasOne(x => x.User).WithMany(x => x.Categories).HasForeignKey(x => x.UserId);

        builder.Ignore(x => x.DomainEvents);
    }
}
