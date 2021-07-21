using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsDL;
using UserTestsModels;

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
            return await _repo.AddGoal(g);
        }

        public async Task<List<Goal>> GetAllGoals(string userId)
        {
            return await _repo.GetGoalsForUser(userId);
        }

        public async Task<Goal> GetGoal(int categoryId, string userId)
        {
            return await _repo.GetGoal(categoryId, userId);
        }
    }
}
