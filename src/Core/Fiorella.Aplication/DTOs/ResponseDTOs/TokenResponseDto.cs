namespace Fiorella.Aplication.DTOs.ResponseDTOs;

public record TokenResponseDto(string token,DateTime expireDate, DateTime refreshTokenExpiration, string refreshToken);
