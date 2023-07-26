using Microsoft.AspNetCore.Identity;

namespace Fiorella.Domain.Entities;

public class AppUser:IdentityUser
{
    public string? Fullname { get; set; }
    public bool IsActive { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    public string? RefreshToken { get; set; }
}
