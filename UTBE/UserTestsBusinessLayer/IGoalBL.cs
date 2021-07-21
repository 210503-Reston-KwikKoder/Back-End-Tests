using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;

namespace UserTestsBL
{
    public interface IGoalBL
    {
        /// <summary>
        /// Calls the database to get a goal for a given user
        /// </summary>
        /// <param name="categoryId">category for goal</param>
        /// <param name="userId">user for goal</param>
        /// <returns>Goal if found or null otherwise</returns>
        Task<Goal> GetGoal(int categoryId, string userId);
        /// <summary>
        /// Sends a given goal to the data layer to be added
        /// </summary>
        /// <param name="g">goal to be added to the database</param>
        /// <returns>goal or null on error</returns>
        Task<Goal> AddGoal(Goal g);
        /// <summary>
        /// Gets a list of goals that are available to be claimed
        /// </summary>
        /// <param name="userId">user to get goals for</param>
        /// <returns>List of goals, empty if not found</returns>
        Task<List<Goal>> GetAllGoals(string userId);
    }
}
