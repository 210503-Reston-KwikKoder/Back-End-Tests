using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTestsREST.DTO
{
    public class GoalOutput
    {
        public int CategoryId { get; set; }
        public double WPM { get; set; }
        public DateTime GoalDate { get; set; }
        public DateTime PlacedDate { get; set; }
        public bool Success { get; set; }
        public bool Checked { get; set; }
    }
}
