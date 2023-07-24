using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Fiorella.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Register(RegisterDto registerDto)
    {
        AppUser appUser = new()
        {
            Fullname=registerDto.Fullname,
            UserName=registerDto.username,
            Email=registerDto.email,
            IsActive=true
        };
        IdentityResult identityResult=await _userManager.CreateAsync(appUser,registerDto.password);
        if(!identityResult.Succeeded) 
        {
            StringBuilder errorMessage = new();
            foreach(var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }

    }
}
