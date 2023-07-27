using Fiorella.Aplication.Abstraction.Storage;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public void Delete(string fileSource, string fileName)
    => _storage.Delete(fileSource, fileName);

    public bool HasFile(string fileSource, string fileName)
    =>_storage.HasFile(fileSource, fileName);

    public Task<UploadFileResponseDto> UploadAsync(IFormFile file, string destination)
    =>_storage.UploadAsync(file, destination);

    public Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection files, string destination)
    => _storage.UploadAsync(files, destination);
}
