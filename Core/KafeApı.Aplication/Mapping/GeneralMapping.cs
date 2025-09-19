using AutoMapper;
using KafeApı.Aplication.DTOS.CafeInfoDtos;
using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Aplication.DTOS.ReviewDtos;
using KafeApı.Aplication.DTOS.TableDtos;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreatCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoriesWithMenuDto>().ReverseMap();

            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap(); // This line is redundant but included for completeness
            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap(); // This line is redundant but included for completeness
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap(); // This line is redundant but included for completeness
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap(); // This line is redundant but included for completeness
            CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap(); // This line is redundant but included for completeness

            CreateMap<Table, CreatTableDto>().ReverseMap();
            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();

            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();

            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            CreateMap<CafeInfo, CreateCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, DetailCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, ResultCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, UpdateCafeInfoDto>().ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, DetailReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();

        } 
    }
}
