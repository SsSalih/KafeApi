using AutoMapper;
using FluentValidation;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.TableDtos;
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
    public class TableServices : ITableServices
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatTableDto> _validator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;
        private readonly ITableRepository _tableRepository2;

        public TableServices(IGenericRepository<Table> tableRepository, IMapper mapper, IValidator<CreatTableDto> validator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository2)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _validator = validator;
            _updateTableValidator = updateTableValidator;
            _tableRepository2 = tableRepository2;
        }

        public async Task<ResponseDto<object>> AddTable(CreatTableDto dto)
        {
            try
            {
                var validate = await _validator.ValidateAsync(dto);

                if(validate.IsValid == false)
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null

                    };
                }

                else { 

                    var chechTable = await _tableRepository2.GetByableNumberAsync(dto.TableNumber);
                    if (chechTable != null) 
                    {
                        return new ResponseDto<object>
                        {
                            Succes = false,
                            Message = "Eklemeye çalıştığınız masa mevcut",
                            ErrorCode = ErrorCodes.DuplicateEror,
                            Data = null
                        };
                    }

                    else { 

                        var table = _mapper.Map<Table>(dto);
                        await _tableRepository.AddAsync(table);

                            if (table == null)
                            {
                                return new ResponseDto<object>
                                {
                                    Succes = false,
                                    Message = "Table could not be created",
                                    ErrorCode = ErrorCodes.NotFound,
                                };
                            }

                        return new ResponseDto<object>
                        {
                            Succes = true,
                            Data = null,
                            Message = "Table created successfully",


                        };
                }
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

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            var table =await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                return new ResponseDto<object>
                {
                    Succes = false,
                    Message = "Table not found",
                    ErrorCode = ErrorCodes.NotFound,
                    Data = null
                }; 
            }
            await _tableRepository.DeleteAsync(table);
            return new ResponseDto<object>
            {
                Succes = true,
                Message = "Table deleted successfully",
                Data = null
            };
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric()
        {
            try
            {
                var tables = await _tableRepository.GetAllAsync();
                tables =tables.Where(x => x.IsActive ==true).ToList();
                if (tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Succes = false,
                        Message = "No tables found",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }

                var result = _mapper.Map<List<ResultTableDto>>(tables);
                //await _tableRepository.GetAllAsync();
                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = true,
                    Data = result,
                    Message = "Tables retrieved successfully"
                };
            }
            catch (Exception ex)
            {


                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = false,
                    Data = null,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception

                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables()
        {
            try
            {
                var tables = await _tableRepository2.GetAllActiveTablesAsync();
                if (tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Succes = false,
                        Message = "No tables found",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }

                var result = _mapper.Map<List<ResultTableDto>>(tables);
                //await _tableRepository.GetAllAsync();
                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = true,
                    Data = result,
                    Message = "Tables retrieved successfully"
                };
            }
            catch (Exception ex)
            {


                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = false,
                    Data = null,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception

                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var tables = await _tableRepository.GetAllAsync();
                if (tables.Count ==0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Succes = false,
                        Message = "No tables found",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }

                var result = _mapper.Map<List<ResultTableDto>>(tables);
                //await _tableRepository.GetAllAsync();
                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = true,
                    Data = result,
                    Message = "Tables retrieved successfully"
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultTableDto>>
                {
                    Succes = false,
                    Data = null,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception

                };
            }
           
        }

        public async Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
        {
            try
            {
                var table = await _tableRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Succes = false,
                        Message = "Table not found",
                        ErrorCode = ErrorCodes.NotFound,

                    };
                }

                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Succes = true,
                    Data = result,
                    Message = "Table retrieved successfully"
                };
            }
            catch (Exception ex)
            {


                return new ResponseDto<DetailTableDto>
                {
                    Succes = false,
                    Message = ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,

                };
            }
            
        }

        

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber)
        {
            try
            {
                var table =await _tableRepository2.GetByableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto> { Data = null, Succes = false, ErrorCode = ErrorCodes.NotFound, Message = "aranan masa bulunamadı" };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto> { Data = result, Succes = true, Message = "masa bulundu" };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailTableDto> { Data = null, Succes = false, Message = "bir sorun olştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
        {
            try
            {
                var validationResult = await _updateTableValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Data = null,
                        Message = string.Join(" | ", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                else {

                    var table = await _tableRepository.GetByIdAsync(dto.Id);
                    //if (table == null)
                    //{
                    //    return new ResponseDto<object>
                    //    {
                    //        Succes = false,
                    //        Message = "Table not found",
                    //        ErrorCodes = ErrorCodes.NotFound,
                    //        Data = null
                    //    };
                    //}

                    //var chechTable =await _tableRepository.GetByIdAsync(dto.TableNumber);

                    //if (chechTable != null && chechTable.Id != dto.Id)
                    //{
                    //    return new ResponseDto<object>
                    //    {
                    //        Succes = false,
                    //        Message = "Table number already exists",
                    //        ErrorCodes = ErrorCodes.DuplicateEror,
                    //        Data = null
                    //    };
                    //}

                    var response =  _mapper.Map(dto,table);
                await _tableRepository.UpdateAsync(response);

                return new ResponseDto<object>
                {
                    Succes = true,
                    Message = "Table updated successfully",
                    Data = null
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

        public async Task<ResponseDto<object>> UpdateTableStatusById(int id)
        {
            try
            {
                var table = await _tableRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Message = "Table not found",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }
                table.IsActive = !table.IsActive; // Toggle the IsActive status
                await _tableRepository.UpdateAsync(table);
                return new ResponseDto<object>
                {
                    Succes = true,
                    Message = "Table status updated successfully",
                    Data = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Succes = false,
                    Message = "Table status updated error",
                    Data = null
                };
            }
            
        }

        public async Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber)
        {
            try
            {
                var table = await _tableRepository2.GetByableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Message = "Table not found",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }
                table.IsActive = !table.IsActive; // Toggle the IsActive status
                await _tableRepository.UpdateAsync(table);
                return new ResponseDto<object>
                {
                    Succes = true,
                    Message = "Table status updated successfully",
                    Data = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Succes = false,
                    Message = "Table status updated error",
                    Data = null
                };
            }
        }
    }
}
