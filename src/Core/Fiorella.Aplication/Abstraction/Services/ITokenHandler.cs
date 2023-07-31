using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Domain.Entities;

namespace Fiorella.Aplication.Abstraction.Services;

public interface ITokenHandler
{
    public Task<TokenResponseDto> CreateAccessToken(int minutes,int refreshTokenMinutes,AppUser user);
}
