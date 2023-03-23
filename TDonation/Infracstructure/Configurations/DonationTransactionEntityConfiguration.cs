using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TDonation.Entities;
using TDonation.Enums;

namespace TDonation.Infracstructure.Configurations;

public class DonationTransactionEntityConfiguration : BaseEntityTypeConfiguration<DonationTransactionEntity>
{
    public override void Configure(EntityTypeBuilder<DonationTransactionEntity> builder)
    {
        // Set table name
        builder.ToTable("donation_transaction");

        // Set primary key
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

        // Set indexes
        builder.HasIndex(e => e.PostId).IsUnique(false);
        builder.HasIndex(e => e.InternalSenderId).IsUnique(false);
        builder.HasIndex(e => e.InternalReceiverId).IsUnique(false);
        builder.HasIndex(e => e.InternalTransactionId).IsUnique();

        // Set column types
        builder.Property(e => e.Amount).HasColumnName("amount").HasColumnType("bigint");
        builder.Property(e => e.InternalSenderId).HasColumnName("internal_sender_id").HasColumnType("varchar(255)");
        builder.Property(e => e.InternalReceiverId).HasColumnName("internal_receiver_id").HasColumnType("varchar(255)");
        builder.Property(e => e.InternalTransactionId).HasColumnName("internal_transaction_id")
            .HasColumnType("varchar(255)");
        builder.Property(e => e.TransactionTypeEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<TransactionTypeEnum>(v)
            )
            .HasColumnName("transaction_type");
        builder.Property(e => e.CurrencyEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<CurrencyEnum>(v)
            );
        builder.Property(e => e.PaymentServiceEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<PaymentServiceEnum>(v)
            )
            .HasColumnName("payment_service");
        builder.Property(e => e.TransactionToken).HasColumnName("transaction_token").HasColumnType("varchar(255)");
        builder.Property(e => e.Description).HasColumnName("description").HasColumnType("text");
        builder.Property(e => e.StatusEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<TransactionStatusEnum>(v)
            )
            .HasColumnName("transaction_status");
        builder.Property(e => e.Message).HasColumnName("message").HasColumnType("text");
        builder.Property(e => e.AdditionalData).HasColumnName("additional_data").HasColumnType("text");
    }
}