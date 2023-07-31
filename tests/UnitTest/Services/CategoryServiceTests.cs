using AutoMapper;
using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Fiorella.Persistence.Implementations.Services;
using Fiorella.Persistence.Resources;
using Microsoft.Extensions.Localization;
using Moq;

namespace UnitTest.Services;

public class CategoryServiceTests
{
    private readonly CategoryService _categoryService;
    private readonly Mock<ICategoryReadRepository> _readRepoMock = new();
    private readonly Mock<ICategoryWriteRepository> _writeRepoMock=new();
    private readonly Mock<IMapper> _map=new();
    private readonly Mock<IStringLocalizer<ErrorMessages>> _localizer=new();
    public CategoryServiceTests()
    {
        _categoryService=new CategoryService(_readRepoMock.Object,
                                             _writeRepoMock.Object,
                                             _map.Object,
                                             _localizer.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategoryDto_WhenExistId()
    {
        //Arrange
        int Id = 1;
        string name = "Category1";
        string desc = "Test category";
        Category category = new()
        {
            Id= Id,
            Name=name,
            Description=desc
        };

        _readRepoMock.Setup(x=>x.GetByIdAsync(1)).ReturnsAsync(category);
        _map.Setup(m => m.Map<CategoryGetDto>(It.IsAny<Category>()))
            .Returns((Category src) => new CategoryGetDto(Id,name,desc));

        //Act
        CategoryGetDto categoryDTO=await _categoryService.GetByIdAsync(1);

        //Assert
        Assert.Equal(categoryDTO.Id, category.Id);
        Assert.Equal(categoryDTO.name, category.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnNotFoundExp_WhenIdNotExist()
    {
        //Arrange
        _readRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(() => null);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _categoryService.GetByIdAsync(1));
    }
}
