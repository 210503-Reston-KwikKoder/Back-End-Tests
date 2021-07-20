using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestsModels
{
    public class Goal
    {
        public Goal() { }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public double WPM { get; set; }
        public DateTime GoalDate { get; set; }
        public bool Checked { get; set; }
    }
}
