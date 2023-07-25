using Microsoft.AspNetCore.Identity;

namespace Fiorella.Domain.Entities;

public class AppUser:IdentityUser
{
    public string? Fullname { get; set; }
    public bool IsActive { get; set; }
}
