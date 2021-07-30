namespace Budgetify.Storage.Infrastructure.Configuration
{
    using Budgetify.Storage.Common.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("User", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn").HasColumnType("smalldatetime").IsRequired();
            builder.Property(x => x.DeletedOn).HasColumnName("DeletedOn").HasColumnType("smalldatetime");
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(250)");
            builder.Property(x => x.Address).HasColumnName("Address").HasColumnType("nvarchar(250)");

            builder.Ignore(x => x.DomainEvents);
        }
    }

    public class Test : Entity
    {
        public string? Name { get; protected internal set; }

        public string? Address { get; protected internal set; }
    }
}
