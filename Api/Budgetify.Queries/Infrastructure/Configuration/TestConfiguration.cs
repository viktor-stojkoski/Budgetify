namespace Budgetify.Queries.Infrastructure.Configuration;

using Budgetify.Queries.Common.Configuration;
using Budgetify.Queries.Test.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TestConfiguration : EntityTypeConfigurationBase<Test>, IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Test", "dbo");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(250)");
        builder.Property(x => x.Address).HasColumnName("Address").HasColumnType("nvarchar(250)");
    }
}
