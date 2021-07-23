using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsDL;
using UserTestsModels;
using UserTestsModels.Utility;

namespace UserTestsBL
{
    public class GoalBL : IGoalBL
    {
        private readonly Repo _repo;
        public GoalBL(UserTestDBContext context)
        {
            _repo = new Repo(context);
        }
        public async Task<Goal> AddGoal(Goal g)
        {
            if(await _repo.GetSatUserCat(g.CategoryId,g.UserId) != null)
            {
                UserStat userStat= await _repo.GetSatUserCat(g.CategoryId, g.UserId);

                //check to see if user has already met goal
                if (userStat.AverageWPM >= g.WPM) {
                    Log.Error("Goal's WPM too low, returning 400");
                    return null; }
            }
            g.PlacedDate = DateTime.Now;
            return await _repo.AddGoal(g);
        }

        public async Task<List<GoalInformation>> ClaimGoals(string userId)
        {
            List<Goal> goals = await _repo.GetAllGoalsForUser(userId);
            User u = await _repo.GetUser(userId);
            List<GoalInformation> goalInformations = new List<GoalInformation>();
            foreach( Goal g in goals)
            {
                GoalInformation goalInformation = new GoalInformation(g);
                if (await _repo.GetSatUserCat(g.CategoryId, userId) == null) continue;
                UserStat userStat = await _repo.GetSatUserCat(g.CategoryId, userId);
                
                if(DateTime.Now > g.GoalDate)
                {
                    if(g.WPM <= userStat.AverageWPM)
                    {
                        goalInformation.Success = true;
                        TimeSpan timeSpan = goalInformation.GoalDate - goalInformation.PlacedDate;
                        u.Revapoints += (int) Math.Ceiling(userStat.AverageWPM * timeSpan.TotalHours);

                    }
                    goalInformation.Checked = true;
                    await _repo.DeleteGoal(g);
                }
                goalInformations.Add(goalInformation);
            }
            return goalInformations;
        }

        public async Task<List<Goal>> GetAllGoals(string userId)
        {
            return await _repo.GetAllGoalsForUser(userId);
        }

        public async Task<Goal> GetGoal(int categoryId, string userId)
        {
            return await _repo.GetGoal(categoryId, userId);
        }
    }
}
