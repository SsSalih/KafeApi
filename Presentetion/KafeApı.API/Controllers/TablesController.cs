using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.TableDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableServices _tableServices;
       

        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableServices.GetAllTables();
            if (!tables.Succes)
            {
                if (tables.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(tables);
                }
                return BadRequest(tables);
            }
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var table = await _tableServices.GetByIdTable(id);
            if (!table.Succes)
            {
                if (table.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(table);
                }
                return BadRequest(table);
            }
            return Ok(table);
        }

        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var table = await _tableServices.GetByTableNumber(tableNumber);
            if (!table.Succes)
            {
                if (table.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(table);
                }
                return BadRequest(table);
            }
            return Ok(table);
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(CreatTableDto dto)
        {
            var entity = await _tableServices.AddTable(dto);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.DuplicateEror)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable(int id, UpdateTableDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Succes = false,
                    ErrorCodes = ErrorCodes.ValidationError,
                    Message = "Id mismatch"
                });
            }

            var entity = await _tableServices.UpdateTable(dto);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }

            return Ok(entity);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var entity = await _tableServices.DeleteTable(id);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }
            return Ok(entity);
        }


        [HttpGet("getallisactivetablesgeneric")]
        public async Task<IActionResult> GetAllActiveTablesGeneric()
        {
            var tables = await _tableServices.GetAllActiveTablesGeneric();
            if (!tables.Succes)
            {
                if (tables.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(tables);
                }
                return BadRequest(tables);
            }
            return Ok(tables);
        }

        [HttpGet("getallisactivetables")]
        public async Task<IActionResult> GetAllActiveTables()
        {
            var tables = await _tableServices.GetAllActiveTables();
            if (!tables.Succes)
            {
                if (tables.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(tables);
                }
                return BadRequest(tables);
            }
            return Ok(tables);
        }


        [HttpPut("updatetablestatusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
           

            var entity = await _tableServices.UpdateTableStatusById(id);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }

            return Ok(entity);

        }


        [HttpPut("updatetablestatusbytablenumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {


            var entity = await _tableServices.UpdateTableStatusByTableNumber(tableNumber);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }

            return Ok(entity);

        }
    }
}
