namespace Budgetify.Queries.Infrastructure.Configuration
{
    using Budgetify.Queries.Common.Configuration;
    using Budgetify.Queries.User.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : EntityTypeConfigurationBase<User>, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Budgetify");

            ConfigureDefaultColumns(builder);

            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("nvarchar(255)").IsRequired();
        }
    }
}
