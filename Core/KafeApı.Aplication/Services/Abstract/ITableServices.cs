using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.TableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    
        public interface ITableServices
        {
            Task<ResponseDto<List<ResultTableDto>>> GetAllTables();
            Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables();
            Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric();
            Task<ResponseDto<DetailTableDto>> GetByIdTable(int id);
            Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber);
            Task<ResponseDto<object>> AddTable(CreatTableDto dto);
            Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto);
            Task<ResponseDto<object>> DeleteTable(int id);
            Task<ResponseDto<object>> UpdateTableStatusById(int id);
            Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber);

    }
    
}
