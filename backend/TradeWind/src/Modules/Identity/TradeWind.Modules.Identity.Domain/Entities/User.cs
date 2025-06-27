using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Shared.Enums;

namespace TradeWind.Modules.Identity.Domain.Entities;

public class User
{
	public Guid Id { get; set; }
	public string Email { get; set; } = null!;
	public string PasswordHash { get; set; } = null!;
	public UserRole Role { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? LastLoginAt { get; set; }

	public User()
	{
		Id = Guid.NewGuid();
		Role = UserRole.User;
	}
}
