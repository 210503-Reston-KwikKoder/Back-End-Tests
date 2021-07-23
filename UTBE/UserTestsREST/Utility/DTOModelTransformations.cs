using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestsModels;
using UserTestsModels.Utility;
using UserTestsREST.DTO;

namespace UserTestsREST.Utility
{
    public static class DTOModelTransformations
    {
        public static Goal GoalInputToGoal(GoalInput goalInput, string userId)
        {
            Goal g = new Goal();
            g.WPM = goalInput.WPM;
            g.UserId = userId;
            g.CategoryId = goalInput.CategoryId;
            g.GoalDate = goalInput.GoalDate;
            return g;
        }
        public static GoalOutput GoalInformationToGoalOutput(GoalInformation goalInformation)
        {
            GoalOutput goalOutput = new GoalOutput();
            goalOutput.CategoryId = goalInformation.CategoryId;
            goalOutput.Checked = goalInformation.Checked;
            goalOutput.GoalDate = goalInformation.GoalDate;
            goalOutput.PlacedDate = goalInformation.PlacedDate;
            goalOutput.Success = goalInformation.Success;
            goalOutput.WPM = goalInformation.WPM;
            return goalOutput;
        }
    }
}
