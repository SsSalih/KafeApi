using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.ResponseDtos
{
    public class ResponseDto<T> where T : class
    {
        public bool Succes { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public string? ErrorCode { get; set; }
    }
}
