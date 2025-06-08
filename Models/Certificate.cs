using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Models
{
    public class Certificate
    {
        public string Title { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public int Grade { get; set; }
    }
}
