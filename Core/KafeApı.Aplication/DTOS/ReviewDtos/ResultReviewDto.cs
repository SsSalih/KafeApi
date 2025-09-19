using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.ReviewDtos
{
    public class ResultReviewDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; } // 1 - 5 arası yıldız
        public DateTime CreadeAt { get; set; }
    }
}
