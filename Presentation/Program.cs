using BusinessLogic.IServices;
using BusinessLogic.Services;
using DataAccess.UnitOfWork.Interface;
using DataAccess.UnitOfWork;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.ValidationRules.ProductValidators;
using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.ValidationRules.CategoryValidators;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.ValidationRules.UserValidators;
using BusinessLogic.ValidationRules.CartValidators;
using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.ValidationRules.CartItemValidators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// UnitOfWork
builder.Services.AddScoped<IUOW, UOW>();

// Services
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

// Validators
builder.Services.AddTransient<IValidator<CartItemCreateDTO>, CartItemCreateDTOValidator>();
builder.Services.AddTransient<IValidator<CartItemUpdateDTO>, CartItemUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<CartCreateDTO>, CartCreateDTOValidator>();
builder.Services.AddTransient<IValidator<CartUpdateDTO>, CartUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<CategoryCreateDTO>, CategoryCreateDTOValidator>();
builder.Services.AddTransient<IValidator<CategoryUpdateDTO>, CategoryUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<ProductCreateDTO>, ProductCreateDTOValidator>();
builder.Services.AddTransient<IValidator<ProductUpdateDTO>, ProductUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<UserCreateDTO>, UserCreateDTOValidator>();
builder.Services.AddTransient<IValidator<UserUpdateDTO>, UserUpdateDTOValidator>();

// Mapster
builder.Services.AddScoped<IMapper, Mapper>();

// JWT Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
