using System.Text;
using MapsterMapper;
using FluentValidation;
using DataAccess.Context;
using DataAccess.UnitOfWork;
using BusinessLogic.Services;
using BusinessLogic.IServices;
using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.DTOs.OrderDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.DTOs.OrderItemDTOs;
using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.ValidationRules.UserValidators;
using BusinessLogic.ValidationRules.OrderValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessLogic.ValidationRules.PaymentValidators;
using BusinessLogic.ValidationRules.ProductValidators;
using BusinessLogic.ValidationRules.CategoryValidators;
using BusinessLogic.ValidationRules.CartItemValidators;
using BusinessLogic.ValidationRules.DiscountValidators;
using BusinessLogic.ValidationRules.OrderItemValidators;
using BusinessLogic.ValidationRules.ShippingDetailValidators;
using DataAccess.Repository;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Connection String
builder.Services.AddDbContext<EcommerceContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// UnitOfWork
// builder.Services.AddScoped<IUOW, UOW>();

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Mapster
builder.Services.AddScoped<IMapper, Mapper>();

// JWT Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

// Services
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShippingDetailService, ShippingDetailService>();
builder.Services.AddScoped<IUserService, UserService>();

// Validators
builder.Services.AddTransient<IValidator<CartItemUpdateDTO>, CartItemUpdateDTOValidator>();
builder.Services.AddTransient<IValidator<CartItemCreateDTO>, CartItemCreateDTOValidator>();

builder.Services.AddTransient<IValidator<CategoryCreateDTO>, CategoryCreateDTOValidator>();
builder.Services.AddTransient<IValidator<CategoryUpdateDTO>, CategoryUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<DiscountCreateDTO>, DiscountCreateDTOValidator>();
builder.Services.AddTransient<IValidator<DiscountUpdateDTO>, DiscountUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<OrderCreateDTO>, OrderCreateDTOValidator>();
builder.Services.AddTransient<IValidator<OrderUpdateDTO>, OrderUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<OrderItemCreateDTO>, OrderItemCreateDTOValidator>();
builder.Services.AddTransient<IValidator<OrderItemUpdateDTO>, OrderItemUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<PaymentCreateDTO>, PaymentCreateDTOValidator>();
builder.Services.AddTransient<IValidator<PaymentUpdateDTO>, PaymentUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<ProductCreateDTO>, ProductCreateDTOValidator>();
builder.Services.AddTransient<IValidator<ProductUpdateDTO>, ProductUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<ShippingDetailCreateDTO>, ShippingDetailCreateDTOValidator>();
builder.Services.AddTransient<IValidator<ShippingDetailUpdateDTO>, ShippingDetailUpdateDTOValidator>();

builder.Services.AddTransient<IValidator<UserCreateDTO>, UserCreateDTOValidator>();
builder.Services.AddTransient<IValidator<UserUpdateDTO>, UserUpdateDTOValidator>();

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
