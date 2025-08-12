using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.TableDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : BaseController
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
            return CreateResponse(tables);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var table = await _tableServices.GetByIdTable(id);
            return CreateResponse(table);

        }

        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var table = await _tableServices.GetByTableNumber(tableNumber);
            return CreateResponse(table);

        }

        [HttpPost]
        public async Task<IActionResult> AddTable(CreatTableDto dto)
        {
            var entity = await _tableServices.AddTable(dto);
            return CreateResponse(entity);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable( UpdateTableDto dto)
        {
            

            var entity = await _tableServices.UpdateTable(dto);
            return CreateResponse(entity);


            

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var entity = await _tableServices.DeleteTable(id);
            return CreateResponse(entity);

        }


        [HttpGet("getallisactivetablesgeneric")]
        public async Task<IActionResult> GetAllActiveTablesGeneric()
        {
            var tables = await _tableServices.GetAllActiveTablesGeneric();
            return CreateResponse(tables);

        }

        [HttpGet("getallisactivetables")]
        public async Task<IActionResult> GetAllActiveTables()
        {
            var tables = await _tableServices.GetAllActiveTables();
            return CreateResponse(tables);

        }


        [HttpPut("updatetablestatusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
           

            var entity = await _tableServices.UpdateTableStatusById(id);
            return CreateResponse(entity);


        }


        [HttpPut("updatetablestatusbytablenumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {


            var entity = await _tableServices.UpdateTableStatusByTableNumber(tableNumber);
            return CreateResponse(entity);


        }
    }
}
