using AutoMapper;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Fiorella.Domain.Entities;

namespace Fiorella.Persistence.MapperProfiles;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();
    }
}
