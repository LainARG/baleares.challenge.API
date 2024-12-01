using System.Text;
using baleares.challenge.API.infrastructure.repository.interfaces;
using baleares.challenge.API.infrastructure.repository;
using baleares.challenge.API.infrastructure.services.interfaces;
using baleares.challenge.API.Infrastructure.Repository.Context;
using baleares.challenge.API.Infrastructure.Services;
using baleares.challenge.API.model.users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using baleares.challenge.API.Infrastructure.Services.Token;


namespace baleares.challenge.API.infrastructure.services;

public static class ServiceManager
{
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BalearesContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole<int>>()
                    .AddEntityFrameworkStores<BalearesContext>()
                    .AddDefaultTokenProviders();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddSingleton<TokenValidationService>();
            services.AddAuthentication(options =>
            {
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidAudience = configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
             });
            services.AddControllers();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkoutBuddy", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Ingrese Bearer Token",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                       Type=ReferenceType.SecurityScheme,
                       Id="Bearer"
                    }
                },
                Array.Empty<string>()
             }
           });
        });
    }
}

