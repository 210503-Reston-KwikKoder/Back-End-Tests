using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestsModels.Utility
{
    public class GoalInformation: Goal
    {
        public GoalInformation(Goal g)
        {
            this.UserId = g.UserId;
            this.WPM = g.WPM;
            this.CategoryId = g.CategoryId;
            this.GoalDate = g.GoalDate;
            this.PlacedDate = g.PlacedDate;
            this.Success = false;
            this.Checked = false;
        }
        public bool Success { get; set; }
        public bool Checked { get; set; }
    }
}
