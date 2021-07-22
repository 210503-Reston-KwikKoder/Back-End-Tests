using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;
using UserTestsModels.Utility;

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
        /// Gets a list of goals for a user
        /// </summary>
        /// <param name="userId">user to get goals for</param>
        /// <returns>List of goals, empty if not found</returns>
        Task<List<Goal>> GetAllGoals(string userId);
        /// <summary>
        /// Claims all goals and removes old goals from the database
        /// </summary>
        /// <param name="userId">ID of user to claim goals for</param>
        /// <returns>List of goals, whether they were claimed or successful</returns>
        Task<List<GoalInformation>> ClaimGoals(string userId);
    }
}
