using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.TableDtos
{
    public class CreatTableDto
    {
     
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
