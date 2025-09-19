using AspNetCoreRateLimit;
using FluentValidation;
using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Aplication.DTOS.TableDtos;
using KafeApı.Aplication.DTOS.UserDtos; // ✅ RegisterDto için gerekli
using KafeApı.Aplication.Helpers;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Mapping;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Aplication.Services.Concreate;
using KafeApı.Aplication.Validators.CafeInfo;
using KafeApı.Aplication.Validators.Review;
using KafeApı.Persistance.Context;
using KafeApı.Persistance.Context.Identity;
using KafeApı.Persistance.Middlewares;
using KafeApı.Persistance.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Settings.Configuration;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppIdentityDbContext>()
   .AddDefaultTokenProviders();

builder.Services.AddControllers();

// Interface ve repository bağlamaları
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderReposiory>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<TokenHelpers>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICafeInfoServices, CafeInfoServices>();
builder.Services.AddScoped<IReviewServices, ReviewServices>();

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GeneralMapping>());

// Validation konfigürasyonları
builder.Services.AddValidatorsFromAssemblyContaining<CreatCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatTableDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();

// ✅ RegisterDto validator eklendi
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDto>();

builder.Services.AddValidatorsFromAssemblyContaining<AddCafeInfoValidate>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCafeInfoValidate>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateReviewValidate>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateReviewValidate>();

// OpenAPI
builder.Services.AddOpenApi();

// JWT yapılandırması
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        //RoleClaimType = "role"
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor(); 

// Serilog config

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Host.UseSerilog();

//fazla atilan istekleri engelleme

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

app.MapScalarApiReference(opt =>
{
    opt.Title = "Kafe Apı v1";
    opt.Theme = ScalarTheme.BluePlanet;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<SerilogMiddleware>();

app.MapControllers();

app.Run();