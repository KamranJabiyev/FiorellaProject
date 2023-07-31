using Fiorella.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Aplication.Abstraction.Storage;

public interface IStorage
{
    Task<UploadFileResponseDto> UploadAsync(IFormFile file,string destination);
    Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection files,string destination);
    void Delete(string fileSource, string fileName);
    bool HasFile(string fileSource, string fileName);
}
