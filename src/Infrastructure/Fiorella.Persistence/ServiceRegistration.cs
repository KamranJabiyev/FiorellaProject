using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Validators.CategoryValidators;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Helpers;
using Fiorella.Persistence.Implementations.Repositories;
using Fiorella.Persistence.Implementations.Services;
using Fiorella.Persistence.MapperProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fiorella.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
           {
               options.UseSqlServer(Configuration.ConnectionString);
           });

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();

        services.AddAutoMapper(typeof(CategoryProfile).Assembly);

        services.AddReadRepositories();
        services.AddWriteRepositories();

        services.AddScoped<ICategoryService, CategoryService>();
    }

    private static void AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
    }
    private static void AddWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
    }
}
