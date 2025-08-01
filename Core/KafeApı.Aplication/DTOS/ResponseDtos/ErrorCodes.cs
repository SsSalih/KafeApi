using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.ResponseDtos
{
    public static class ErrorCodes
    {
        public const string NotFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTOHORIZED";
        public const string Exception = "EXCEPTION";
        public const string ValidationError = "VALIDATION_ERROR";
        public const string DuplicateEror = "DUPLICATE_EROOR";
        public const string Forbidden = "FORBIDDEN";//yetkili ama erişim yok
        public const string BadRequest = "BAD_REQUEST";
        
    }
}
