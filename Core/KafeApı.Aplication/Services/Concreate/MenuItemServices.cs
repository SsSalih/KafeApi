using AutoMapper;
using FluentValidation;
using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KafeApı.Aplication.Services.Concreate
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemDto;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemDto, IGenericRepository<Category> categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemDto = updateMenuItemDto;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>{
                        Succes = false, 
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)), 
                        ErrorCode = ErrorCodes.ValidationError, 
                        Data = null  };
                }

                else { 
                    var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);

                    if(checkCategory == null) 
                    {
                        return new ResponseDto<object> { Succes = false, Data = dto, Message = "kategori bulunamadı", ErrorCode = ErrorCodes.Exception };
                    }

                    var menuItem = _mapper.Map<MenuItem>(dto);

                if (menuItem == null)
                {
                    return new ResponseDto<object> { Message = "Menü öğesi oluşturulamadı", Succes = false, ErrorCode = ErrorCodes.NotFound,Data=null };
                }

                await _menuItemRepository.AddAsync(menuItem);
                return new ResponseDto<object> { Succes = true, Message = "Menü öğesi başarıyla eklendi.", Data = null };
            }

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Message = ex.Message,
                    Succes = false,
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
            
        }

      

        public async Task<ResponseDto<object>> DelteMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if(menuItem == null)
                {
                    return new ResponseDto<object> { Message = "Menü öğesi bulunamadı", Succes = false, ErrorCode = ErrorCodes.NotFound };
                }
                await _menuItemRepository.DeleteAsync(menuItem);
                return new ResponseDto<object> { Succes = true, Message = "Menü öğesi başarıyla silindi.", Data = null };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object> { Succes = false, Message = ex.Message, Data = null, ErrorCode= ErrorCodes.Exception };

            }

        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                if(menuItems ==null || menuItems.Count == 0) 
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                       
                        Message = "Menü öğeleri bulunamadı",
                        Succes = false,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>> {
                    Data = result,
                    Message = "Menü öğeleri başarıyla getirildi.",
                    Succes = true
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultMenuItemDto>>
                {

                    Message = ex.Message,
                    Succes = false,
                    ErrorCode = ErrorCodes.Exception,

                };
            }
            
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if(menuItem == null) 
                {
                return new ResponseDto<DetailMenuItemDto> { Data=null, Succes = false,ErrorCode= ErrorCodes.NotFound,Message ="aranılan ürün bulunamadı" };
                }
                var category = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
                var result =_mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto> 
                {
                    Data = result,
                    Succes = true,
                    
                };

            }
            catch (Exception)
            {

                return new ResponseDto<DetailMenuItemDto> { Data = null, Succes = false, ErrorCode = ErrorCodes.Exception, Message = "bir hata oluştu" };
            }
            

        }

        

        

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validation = await _updateMenuItemDto.ValidateAsync(dto);
                if (!validation.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Message = string.Join(" | ", validation.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                else { 
                    var menuitem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuitem == null)
                {
                    return new ResponseDto<object> { Message = "Menü öğesi bulunamadı", Succes = false, ErrorCode = ErrorCodes.NotFound };
                }
                    var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);

                    if (checkCategory == null)
                    {
                        return new ResponseDto<object> { Succes = false, Data = dto, Message = "kategori bulunamadı", ErrorCode = ErrorCodes.Exception };
                    }

                    var newmenuitem = _mapper.Map(dto, menuitem);
                    await _menuItemRepository.UpdateAsync(newmenuitem);

                return new ResponseDto<object> { Succes = true, Message = "Menü öğesi başarıyla güncellendi.", Data = null };
            }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Message = ex.Message,
                    Succes = false,
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
            
        }

       
    }
}
