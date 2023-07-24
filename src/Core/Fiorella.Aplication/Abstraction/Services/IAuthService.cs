using Fiorella.Aplication.DTOs.AuthDTOs;

namespace Fiorella.Aplication.Abstraction.Services;

public interface IAuthService
{
    Task Register(RegisterDto registerDto);
}
