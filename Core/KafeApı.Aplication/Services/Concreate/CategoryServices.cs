using AutoMapper;
using FluentValidation;
using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Concreate
{
    public class CategoryServices : ICategoryServices
    {
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreatCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryDto;



        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreatCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryDto, IMenuItemRepository menuItemRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryDto = updateCategoryDto;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ResponseDto<object>> AddCategory(CreatCategoryDto dto)
        {
            try
            {
                var validationResult = await _createCategoryValidator.ValidateAsync(dto);
                if (!validationResult.IsValid) 
                {
                    return new ResponseDto<object>
                    { 
                        Succes = false, 
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)), 
                        ErrorCode = ErrorCodes.ValidationError, 
                        Data = null 
                    };
                }

                else { 
                    var category = _mapper.Map<Category>(dto);
                if(category == null)
                {
                    return new ResponseDto<object> { Data = null, Succes = false, Message = "Kategori oluşturulamadı", ErrorCode = ErrorCodes.NotFound };
                }
                await _categoryRepository.AddAsync(category);

                return new ResponseDto<object>
                { 
                    Succes = true, 
                    Data = null, 
                    Message = "Kategori başarıyla eklendi",
                };
                }
            }

            catch (Exception ex) 
            {
                return new ResponseDto<object>
                { 
                    Succes = false, 
                    Message = ex.Message, 
                    ErrorCode = ErrorCodes.Exception, 
                    Data = null 
                };
            }

          
        }

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try 
            {

                var category =await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    return new ResponseDto<object> { Succes = false, Message = "Kategori bulunamadı", ErrorCode = ErrorCodes.NotFound,Data=null };
                }

                await _categoryRepository.DeleteAsync(category);

                return new ResponseDto<object> { Succes = true, Message ="Kategori başarıyla silindi",  Data = null };

            }

            catch (Exception ex)
            {
                return new ResponseDto<object> { Succes = false, Message = ex.Message, ErrorCode = ErrorCodes.Exception,Data=null };
            }
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>> { Succes = false, Message = "Sayfa bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>> { Succes = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCategoryDto>> { Succes = false, Message = ex.Message, ErrorCode =ErrorCodes.Exception };
            }
            

        }

        public async Task<ResponseDto<List<DetailCategoryDto>>> GetByIdCategory(int id)
        {
            
            try 
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if(category == null)
                {
                    return new ResponseDto<List<DetailCategoryDto>> { Succes = false, Message = "Kategori bulunamadı", ErrorCode = ErrorCodes.NotFound };
                }
                var menuItem = await _menuItemRepository.GetMenuItemFilterByCategoryId(category.Id);
                var result = _mapper.Map<DetailCategoryDto>(category);

                var newList =_mapper.Map<List<CategoriesMenuItemDto>>(menuItem);
                result.MenuItems =newList;

                return new ResponseDto<List<DetailCategoryDto>> { Succes = true, Data = new List<DetailCategoryDto> { result } };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<DetailCategoryDto>> { Succes = false, Message = ex.Message, ErrorCode = ErrorCodes.Exception };
            }
           
        }

        public async Task<ResponseDto<List<ResultCategoriesWithMenuDto>>> GetCategoriesWithMenuItem()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if(categories.Count == 0) 
                {
                return new ResponseDto<List<ResultCategoriesWithMenuDto>> { Succes = false, Message = "aranlılan öğe bulunamadı", ErrorCode = ErrorCodes.NotFound };

                }

                var result = _mapper.Map<List<ResultCategoriesWithMenuDto>>(categories);

                foreach (var category in result) 
                {
                    var listMenuItem = await _menuItemRepository.GetMenuItemFilterByCategoryId(category.Id);
                    var newList = _mapper.Map<List<CategoriesMenuItemDto>>(listMenuItem);
                    category.MenuItems = newList;
                }

                return new ResponseDto<List<ResultCategoriesWithMenuDto>> { Succes = true, Data = result };


            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultCategoriesWithMenuDto>> { Succes = false, Message = ex.Message, ErrorCode = ErrorCodes.Exception };

            }
        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            var validation = await _updateCategoryDto.ValidateAsync(dto);

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
                try
                {
                    var categoryDb = await _categoryRepository.GetByIdAsync(dto.Id);
                    if (categoryDb == null)
                    {
                        return new ResponseDto<object> { ErrorCode = ErrorCodes.NotFound, Succes = false, Message = "Kategori bulunamadı", Data = null };
                    }
                    var category = _mapper.Map(dto, categoryDb);

                    await _categoryRepository.UpdateAsync(category);
                    return new ResponseDto<object>
                    {
                        Succes = true,
                        Message = "Kategori başarıyla güncellendi",
                        Data = null
                    };
                }

                catch (Exception ex)
                {
                    return new ResponseDto<object> { Succes = false, Message = ex.Message, ErrorCode = ErrorCodes.Exception, Data = null };
                }
            }

        }
    }
}

