using FluentValidation;
using KafeAp�.Aplication.DTOS.CategoryDtos;
using KafeAp�.Aplication.DTOS.MenuItemDtos;
using KafeAp�.Aplication.DTOS.MenuItemsDtos;
using KafeAp�.Aplication.DTOS.TableDtos;
using KafeAp�.Aplication.Interfaces;
using KafeAp�.Aplication.Mapping;
using KafeAp�.Aplication.Services.Abstract;
using KafeAp�.Aplication.Services.Concreate;
using KafeAp�.Persistance.Context;
using KafeAp�.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

//burada interface ve repository ba�lamalar� yap�ld�

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();


//builder.Services.AddAutoMapper(typeof(GeneralMapping).Assembly); bu ks��m hata verdi ve alternatifini yazd�m kod eksik �al���rsa ��z buray�



builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GeneralMapping>());

// validation tamamlalar� burada yap�ld�
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
        opt.Title = "Kafe Ap� v1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    }
    );//thema tan�mlar� burada yap�ld�

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
