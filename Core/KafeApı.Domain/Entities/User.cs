using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }

        public string Name { get; set; }

        public string Surnamme { get; set; }
        public string EmİL { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
