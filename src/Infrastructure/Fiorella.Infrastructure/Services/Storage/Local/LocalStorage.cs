﻿using Fiorella.Aplication.Abstraction.Storage.Local;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    public void Delete(string fileSource, string fileName)
    {
        throw new NotImplementedException();
    }

    public bool HasFile(string fileSource, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<UploadFileResponseDto> UploadAsync(IFormFile file, string destination)
    {
        throw new NotImplementedException();
    }

    public Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection files, string destination)
    {
        throw new NotImplementedException();
    }
}
