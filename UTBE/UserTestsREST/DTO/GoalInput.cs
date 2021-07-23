using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class GoalInput
    {
        public int CategoryId { get; set; }
        public double WPM { get; set; }
        public DateTime GoalDate { get; set; }
    }
}
