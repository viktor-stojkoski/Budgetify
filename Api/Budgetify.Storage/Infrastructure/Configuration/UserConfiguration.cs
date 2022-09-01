namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Common.Configuration;
using Budgetify.Storage.User.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserConfiguration : EntityTypeConfigurationBase<User>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("LastName").HasColumnType("nvarchar(255)").IsRequired();
        builder.Property(x => x.City).HasColumnName("City").HasColumnType("nvarchar(255)").IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}
