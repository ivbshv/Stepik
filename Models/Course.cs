using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Summary { get; set; }
        public string? Photo { get; set; }
    }
}
