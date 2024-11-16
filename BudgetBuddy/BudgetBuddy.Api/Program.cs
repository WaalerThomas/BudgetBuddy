using System.Text;
using BudgetBuddy.Account;
using BudgetBuddy.Api.Service;
using BudgetBuddy.Category;
using BudgetBuddy.Client;
using BudgetBuddy.Common;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Data;
using BudgetBuddy.Transaction;
using CategoryTransfer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
{
    var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
    var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
    if (jwtKey is null || jwtIssuer is null)
    {
        throw new InvalidConfigurationException("Jwt configuration missing");
    }

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["prrrKeKeKedip"];
                    if (token is not null)
                    {
                        context.Token = token;
                    }
                    
                    return Task.CompletedTask;
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
    
    builder.Services
        .AddData()
        .AddAccount()
        .AddClient()
        .AddTransaction()
        .AddCategory()
        .AddCategoryTransfer();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IBuddyConfiguration, BuddyConfiguration>();
    builder.Services.AddScoped<ICommonValidators, CommonValidators>();
    builder.Services.AddScoped<IOperationFactory, OperationFactory>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "JWT Authentication",
            Description = "Enter your JWT token in this field",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };
        
        options.AddSecurityDefinition("Bearer", securityScheme);

        var securityRequirement = new OpenApiSecurityRequirement
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
                []
            }
        };
        
        options.AddSecurityRequirement(securityRequirement);
    });
    builder.Services.AddControllers();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors(myAllowSpecificOrigins);
    
    app.MapControllers();
    app.Run();
}
