using FluentValidation;
using KafeApý.Aplication.DTOS.CategoryDtos;
using KafeApý.Aplication.DTOS.MenuItemDtos;
using KafeApý.Aplication.DTOS.MenuItemsDtos;
using KafeApý.Aplication.DTOS.TableDtos;
using KafeApý.Aplication.Interfaces;
using KafeApý.Aplication.Mapping;
using KafeApý.Aplication.Services.Abstract;
using KafeApý.Aplication.Services.Concreate;
using KafeApý.Persistance.Context;
using KafeApý.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

//burada interface ve repository baðlamalarý yapýldý

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();


//builder.Services.AddAutoMapper(typeof(GeneralMapping).Assembly); bu ksýým hata verdi ve alternatifini yazdým kod eksik çalýþýrsa çöz burayý



builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GeneralMapping>());

// validation tamamlalarý burada yapýldý
builder.Services.AddValidatorsFromAssemblyContaining<CreatCategoryDto>(); 
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatTableDto>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();

app.MapScalarApiReference(
    opt =>
    {
        opt.Title = "Kafe Apý v1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    }
    );//thema tanýmlarý burada yapýldý

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
