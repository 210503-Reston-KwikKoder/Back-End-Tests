using System;
using System.Collections.Generic;
using UserTestsModels;
using System.Threading.Tasks;

namespace UserTestsDL
{
    public interface IRepo
    {
        /// <summary>
        /// Get a user based on username and email
        /// </summary>
        /// <param name="userName">username of user</param>
        /// <param name="email">email of user</param>
        /// /// <returns>User with the given username and email</returns>
        Task<User> GetUser(string auth0id);
  
        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns>List of Users</returns>
        Task<List<User>> GetAllUsers();
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">user to add</param>
        /// <returns>user aded</returns>
        Task<User> AddUser(User user);
        /// <summary>
        /// Adds a category to the database
        /// </summary>
        /// <param name="cat">category to be added</param>
        /// <returns>category added to the database</returns>
        Task<Category> AddCategory(Category cat);
        /// <summary>
        /// Retrieves all categories currently in the database
        /// </summary>
        /// <returns>List of categories found</returns>
        Task<List<Category>> GetAllCategories();
        
        /// <summary>
        /// Versatile method to update a user's stats for a given category
        /// </summary>
        /// <param name="categoryid">category user participated in</param>
        /// <param name="userid">user id related to user</param>
        /// <returns>userstat updated</returns>
        Task<UserStat> AddUpdateStats(int categoryid, string userid, UserStat userStat);
        /// <summary>
        /// Method that returns a user statistics for a given category, null if not found
        /// </summary>
        /// <param name="categoryId">category id for stat</param>
        /// <param name="userId">user id for stat</param>
        /// <returns>Userstat if found null otherwise</returns>
        Task<UserStat> GetSatUserCat(int categoryId, string userId);
        /// <summary>
        /// Method that adds a test to the database
        /// </summary>
        /// <param name="typeTest">TypeTest to add</param>
        /// <returns>test added</returns>
        Task<TypeTest> AddTest(TypeTest typeTest);
        /// <summary>
        /// Method that returns all stats for a given user
        /// </summary>
        /// <param name="userId">Id for user whose stats are being requested</param>
        /// <returns>List of stats if found, null otherwise</returns>
        Task<List<UserStatCatJoin>> GetUserStats(string userId);
        Task<Category> GetCategoryById(int id);
        
        //Task<UserStat> GetUserStatById(int id);
        /// <summary>
        /// Gets the relevant type tests for a given user Id
        /// </summary>
        /// <param name="id">User Id to get tests for</param>
        /// <returns>List of type tests user has taken</returns>
        Task<List<TypeTest>> GetTypeTestsForUser(string userId);
        /// <summary>
        /// Gets a userstat by the user stat id
        /// </summary>
        /// <param name="id">id of userstat to get</param>
        /// <returns>userstat if found null otherwise</returns>
        Task<UserStat> GetUserStatById(int id);
        /// <summary>
        /// Updates the 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        Task<User> UpdateUser(User u);
        /// <summary>
        /// Gets a list of tuples for each category with a list of tests in that category
        /// </summary>
        /// <param name="userId">Id for user to get tests for</param>
        /// <returns>List of tuples for each category with a list of tests in that category, null on error</returns>
        Task<List<Tuple<int, List<TypeTest>>>> GetTypeTestForUserByCategory(string userId);
        /// <summary>
        /// Extremely lightweight method to quickly update any tracked rows in the database
        /// </summary>
        /// <returns>null</returns>
        Task SaveChanges();
        /// <summary>
        /// Adds a goal to the db 
        /// </summary>
        /// <param name="goal">goal to add to the db</param>
        /// <returns>goal added or null if error is thrown</returns>
        Task<Goal> AddGoal(Goal goal);
        /// <summary>
        /// Get a goal for a given user in a given category
        /// </summary>
        /// <param name="categoryId">id of category to find goal in</param>
        /// <param name="userId">id of User to find goal for </param>
        /// <returns>Goal or null if none found</returns>
        Task<Goal> GetGoal(int categoryId, string userId);
        /// <summary>
        /// Gets a list of all claimable goals for a user with a given userId 
        /// </summary>
        /// <param name="userId">Id of user to find goals avaiable for user</param>
        /// <returns>List of claimable goals</returns>
        Task<List<Goal>> GetAllGoalsForUser(string userId);
        /// <summary>
        /// Deletes a goal from the database with the given category and user ID
        /// </summary>
        /// <param name="goal">Goal to be deleted</param>
        /// <returns>goal deleted, null on error</returns>
        Task<Goal> DeleteGoal(Goal goal);
    }
}
