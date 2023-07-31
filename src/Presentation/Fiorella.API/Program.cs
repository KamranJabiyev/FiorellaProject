using Fiorella.API.Middlewares;
using Fiorella.Persistence;
using Fiorella.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Fiorella.Infrastructure;
using System.Globalization;
using Fiorella.Infrastructure.Services.Storage.Local;
using Fiorella.Infrastructure.Services.Storage.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddLocalization();

List<CultureInfo> cultures = new() {
    new CultureInfo("ar-SA"),
    new CultureInfo("en-US"),

};

RequestLocalizationOptions localizationOptions = new()
{
    ApplyCurrentCultureToResponseHeaders = true,
    SupportedCultures= cultures,
    SupportedUICultures= cultures
};
localizationOptions.SetDefaultCulture("en-US");

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();

builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),
        LifetimeValidator=(_,expire,_,_)=>expire>DateTime.UtcNow,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await instance.InitializeAsync();
    await instance.RoleSeedAsync();
    await instance.UserSeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
