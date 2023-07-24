using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Aplication.DTOs.ResponseDTOs;

namespace Fiorella.Aplication.Abstraction.Services;

public interface IAuthService
{
    Task Register(RegisterDto registerDto);
    Task<TokenResponseDto> Login(SignInDto signInDto);
}
