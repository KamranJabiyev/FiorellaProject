using AutoMapper;
using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Fiorella.Persistence.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Persistence.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryReadRepository readRepository,
                           ICategoryWriteRepository writeRepository,
                           IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        if (await _readRepository.GetByExpressionAsync(c => c.Name.ToLower() == categoryCreateDto.name.ToLower()) is not null)
        {
            throw new DuplicatedException("Duplicated Category Name");
        }

        var newCategory = _mapper.Map<Category>(categoryCreateDto);
        await _writeRepository.AddAsync(newCategory);
        await _writeRepository.SaveChangeAsync();
    }

    public Task<List<CategoryGetDto>> GetAllAsync()
    {
        return _readRepository.GetAll()
              .Select(category => new CategoryGetDto(category.Id, category.Name, category.Description))
              .ToListAsync();
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _readRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryGetDto>(category);
    }
}
