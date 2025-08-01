using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.TableDtos
{
    public class DetailTableDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }

        public static implicit operator List<object>(DetailTableDto v)
        {
            throw new NotImplementedException();
        }
    }
}
