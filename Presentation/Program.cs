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
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Validators
builder.Services.AddTransient<IValidator<CategoryCreateDTO>, CategoryCreateDTOValidator>();
builder.Services.AddTransient<IValidator<CategoryUpdateDTO>, CategoryUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<ProductCreateDTO>, ProductCreateDTOValidator>();
builder.Services.AddTransient<IValidator<ProductUpdateDTO>, ProductUpdateDTOValidator>();

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
