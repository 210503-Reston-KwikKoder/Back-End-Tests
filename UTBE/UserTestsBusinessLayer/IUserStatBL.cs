using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;
namespace UserTestsBL
{
    public interface IUserStatBL
    {
        /// <summary>
        /// Method that Adds test and updates the user's stats
        /// </summary>
        /// <param name="userId">userId of test taker</param>
        /// <param name="categoryId">id of the category</param>
        /// <param name="typeTest">Test user has taken</param>
        /// <returns>user stat of test taker and the average user stat</returns>
        Task<List<UserStat>>AddTestUpdateStat(string userId, int categoryId, TypeTest typeTest);

        /// <summary>
        /// Method that gets the user's stats and averages them out into overall stat
        /// </summary>
        /// <param name="userId">Id of user to find average stat for</param>
        /// <returns>User Stat with average stats</returns>
        Task<UserStat> GetAvgUserStat(string userId);
        /// <summary>
        /// Method that gets all the stats associated with a given user in order to return them through rest api
        /// </summary>
        /// <param name="userId">Id of User whose stats you are looking for</param>
        /// <returns>List of user stats associated with the user</returns>
        Task<List<UserStatCatJoin>> GetUserStats(string userId);
        /// <summary>
        /// Leaderboard method that returns a list of Users with the best WPM
        /// </summary>
        /// <returns>List of users ranked by WPM and their WPM, acuraccies and ranking</returns>
 
        Task<TypeTest> SaveTypeTest(int errors, int charactersTyped, int timeTaken, int WPM, DateTime date);
        /// <summary>
        /// DO NOT CALL THIS METHOD WITHOUT GETTING THE USCJS FIRST
        /// gets the userstat by userstat id, NOT USER ID
        /// </summary>
        /// <param name="userstatid">userstat with given userstat id</param>
        /// <returns>UserStat</returns>
        Task<UserStat> GetUserStatByUSId(int userstatid);
        /// <summary>
        /// Given a user Id, calls the database to get required typetests
        /// </summary>
        /// <param name="userId">Id of user to find type tests for</param>
        /// <returns>List of TypeTests, or empty if not found</returns>
        Task<List<TypeTest>> GetTypeTestsForUser(string userId);
        /// <summary>
        /// Gets a list of tuples for each category with a list of tests in that category
        /// </summary>
        /// <param name="userId">Id for user to get tests for</param>
        /// <returns>List of tuples for each category with a list of tests in that category, null on error</returns>
        Task<List<Tuple<int, List<TypeTest>>>> GetTypeTestForUserByCategory(string userId);
        /// <summary>
        /// Uses tracking to update user wins and losses for a user before callint the database to save changes
        /// </summary>
        /// <param name="userStats">User stats to update</param>
        /// <param name="won">true if user won false otherwise</param>
        /// <param name="winstreak">user's current winstreak</param>
        /// <returns>List of Userstats, null on error</returns>
        Task<List<UserStat>> UpdateWL(List<UserStat> userStats, bool won, int winstreak);

    }
}
