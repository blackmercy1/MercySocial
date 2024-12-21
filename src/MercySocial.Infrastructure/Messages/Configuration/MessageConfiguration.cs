using MercySocial.Domain.MessageAggregate;
using MercySocial.Domain.MessageAggregate.ValueObjects;
using MercySocial.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MercySocial.Infrastructure.Messages.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(
        EntityTypeBuilder<Message> builder)
    {
        builder
            .ToTable("Messages");

        builder
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MessageId.Create(value))
            .ValueGeneratedNever();

        builder
            .Property(m => m.SenderId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .IsRequired();

        builder
            .Property(m => m.ReceiverId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .IsRequired();

        builder
            .Property(m => m.Content)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(m => m.SentAt)
            .IsRequired();

        builder
            .HasIndex(m => m.SenderId)
            .HasDatabaseName("IX_Messages_SenderId");

        builder
            .HasIndex(m => m.ReceiverId)
            .HasDatabaseName("IX_Messages_ReceiverId");

        builder
            .HasIndex(m => m.SentAt)
            .HasDatabaseName("IX_Messages_SentAt");
    }
}