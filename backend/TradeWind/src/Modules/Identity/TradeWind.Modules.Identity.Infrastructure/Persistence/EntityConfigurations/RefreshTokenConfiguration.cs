using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Modules.Identity.Domain.Entities;

namespace TradeWind.Modules.Identity.Infrastructure.Persistence.EntityConfigurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.ToTable("refresh_tokens");
		builder.HasKey(r => r.Id);
		builder.HasIndex(r => r.Token).IsUnique();

		builder.Property(r => r.Id)
			.HasColumnName("id")
			.IsRequired();

		builder.Property(r => r.Token)
			.HasColumnName("token")
			.HasColumnType("")
			.IsRequired();

		builder.Property(r => r.ExpiresAt)
			.HasColumnName("expires_at")
			.IsRequired()
			.HasColumnType("timestamp with time zone");

		builder.Property(r => r.CreatedAt)
			.HasColumnName("created_at")
			.IsRequired()
			.HasColumnType("timestamp with time zone")
			.HasDefaultValueSql("NOW()");

		builder.Property(r => r.UserId)
			.HasColumnName("user_id")
			.IsRequired();

		builder.HasOne(r => r.User)
			.WithMany()
			.HasForeignKey(r => r.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
