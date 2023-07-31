using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Domain.Enums;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiorella.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly AppDbContext _context;
    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       ITokenHandler tokenHandler,
                       AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
        _tokenHandler = tokenHandler;
    }

    public async Task<TokenResponseDto> Login(SignInDto signInDto)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(signInDto.UsernameOrEmail);
        if (appUser is null)
        {
            appUser=await _userManager.FindByNameAsync(signInDto.UsernameOrEmail);
            if (appUser is null) throw new SignInFailerException("Invalid login!!!");
        }
        SignInResult signInResult=await _signInManager.CheckPasswordSignInAsync(appUser, signInDto.password,true);
        if (!signInResult.Succeeded)
        {
            throw new SignInFailerException("Invalid login!!!");
        }
        if (!appUser.IsActive)
        {
            throw new UserBlockedException("User blocked!!!");
        }
        
        var tokenResponse = await _tokenHandler.CreateAccessToken(2,3,appUser);
        appUser.RefreshToken = tokenResponse.refreshToken;
        appUser.RefreshTokenExpiration = tokenResponse.refreshTokenExpiration;
        await _userManager.UpdateAsync(appUser);
        return tokenResponse;
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
        var result=await _userManager.AddToRoleAsync(appUser, Role.Member.ToString());
        if (!result.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in result.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }
    }

    public async Task<TokenResponseDto> ValidateRefreshToken(string refreshToken)
    {
        if (refreshToken is null)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }
        AppUser? user = await _context.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (user is null)
        {
            throw new NotFoundException("User does not exist");
        }
        if (user.RefreshTokenExpiration < DateTime.UtcNow)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }
        var tokenResponse = await _tokenHandler.CreateAccessToken(2, 3, user);
        user.RefreshToken = tokenResponse.refreshToken;
        user.RefreshTokenExpiration = tokenResponse.refreshTokenExpiration;
        await _userManager.UpdateAsync(user);
        return tokenResponse;
    }
}
