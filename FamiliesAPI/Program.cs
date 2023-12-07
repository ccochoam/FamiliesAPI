using Microsoft.IdentityModel.Tokens;
using System.Text;
using FamiliesAPI.Services.Implementation;
using FamiliesAPI.Services.Interface;
using Microsoft.OpenApi.Models;
using FamiliesAPI.Data;
using FamiliesAPI.Data.Repositories;
using FamiliesAPI.Data.Interfaces;
using FamiliesAPI.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configuración del esquema de seguridad
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFamilyGroupRepository, FamilyGroupRepository>();
builder.Services.AddScoped<IFamilyGroupService, FamilyGroupService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(amc => amc.AddProfile<AutoMapperProfile>());
builder.Services.AddScoped<DbConnectionFactory>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DbConnectionFactory(connectionString);
});

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://midasoftapi.com",
        ValidAudience = "https://midasoftapp.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uBtAbOs/RgDuGK7+NnzRgHvX5Gt6lgg/OG5UYnfwJmw="))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
