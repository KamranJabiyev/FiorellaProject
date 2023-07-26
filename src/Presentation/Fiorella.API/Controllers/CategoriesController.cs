using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiorella.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CategoryGetDto result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto categoryCreateDto)
        {
            await _categoryService.CreateAsync(categoryCreateDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryGetDto> result = await _categoryService.GetAllAsync();
            return Ok(result);
        }
    }
}
