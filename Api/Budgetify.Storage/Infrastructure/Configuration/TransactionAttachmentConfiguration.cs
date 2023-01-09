namespace Budgetify.Storage.Infrastructure.Configuration;

using Budgetify.Storage.Common.Configuration;
using Budgetify.Storage.Transaction.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TransactionAttachmentConfiguration : EntityTypeConfigurationBase<TransactionAttachment>, IEntityTypeConfiguration<TransactionAttachment>
{
    public void Configure(EntityTypeBuilder<TransactionAttachment> builder)
    {
        builder.ToTable("TransactionAttachment", "Budgetify");

        ConfigureDefaultColumns(builder);

        builder.Property(x => x.TransactionId).HasColumnName("TransactionFk").HasColumnType("int").IsRequired();
        builder.Property(x => x.FilePath).HasColumnName("FilePath").HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(255)").IsRequired();

        builder.HasOne(x => x.Transaction).WithMany(x => x.TransactionAttachments).HasForeignKey(x => x.TransactionId);

        builder.Ignore(x => x.DomainEvents);
    }
}
