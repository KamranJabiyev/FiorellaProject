using Fiorella.Aplication.DTOs.CategoryDTOs;

namespace Fiorella.Aplication.Abstraction.Services;

public interface ICategoryService
{
    Task CreateAsync(CategoryCreateDto categoryCreateDto);
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<List<CategoryGetDto>> GetAllAsync();
}
