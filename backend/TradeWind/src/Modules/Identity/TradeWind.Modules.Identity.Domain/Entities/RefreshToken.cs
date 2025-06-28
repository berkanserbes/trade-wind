using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeWind.Modules.Identity.Domain.Entities;

public sealed class RefreshToken
{
	public Guid Id { get; set; }
	public string Token { get; set; } = null!;
	public DateTime ExpiresAt { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	//public DateTime? RevokedAt { get; set; }
	//public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
	public Guid UserId { get; set; }

	public User User { get; set; } = null!;

	public RefreshToken()
	{
		Id = Guid.NewGuid();	
	}
}
