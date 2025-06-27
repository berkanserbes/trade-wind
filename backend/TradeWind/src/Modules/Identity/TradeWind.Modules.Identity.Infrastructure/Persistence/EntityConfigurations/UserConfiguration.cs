using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeWind.Modules.Identity.Domain.Entities;

namespace TradeWind.Modules.Identity.Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("users");
		builder.HasKey(e => e.Id);
		builder.HasIndex(e => e.Email).IsUnique();

		builder.Property(e => e.Id).HasColumnName("id");

		builder.Property(e => e.Email)
			   .IsRequired()
			   .HasColumnName("email")
			   .HasMaxLength(255);

		builder.Property(e => e.PasswordHash)
			   .IsRequired()
			   .HasColumnName("password_hash");


		builder.Property(e => e.Role)
			   .IsRequired(false)
			   .HasColumnName("role")
			   .HasConversion<string>();

		builder.Property(e => e.CreatedAt)
			   .IsRequired()
			   .HasColumnName("created_at")
			   .HasColumnType("timestamp with time zone")
			   .HasDefaultValueSql("NOW()");

		builder.Property(e => e.UpdatedAt)
			.IsRequired()
			   .HasColumnName("updated_at")
			   .HasColumnType("timestamp with time zone")
			   .HasDefaultValueSql("NOW()");

		builder.Property(e => e.LastLoginAt)
		       .IsRequired(false)
			   .HasColumnName("last_login_at")
			   .HasColumnType("timestamp with time zone");
	}
}
