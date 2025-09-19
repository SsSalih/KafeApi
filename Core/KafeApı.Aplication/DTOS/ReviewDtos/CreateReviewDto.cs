using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.ReviewDtos
{
    public class CreateReviewDto
    {
        
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; } 
        public DateTime CreadeAt { get; set; } = DateTime.Now;
    }
}
