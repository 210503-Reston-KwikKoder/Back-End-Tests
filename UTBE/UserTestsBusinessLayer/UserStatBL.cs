using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;
using Serilog;
using UserTestsDL;
using UserTestsModels.Utility;

namespace UserTestsBL
{
    public class UserStatBL : IUserStatBL
    {
        private readonly Repo _repo;
        public UserStatBL(UserTestDBContext context)
        {
            _repo = new Repo(context);
        }

        public async Task<List<UserStat>> AddTestUpdateStat(string userId, int categoryId, TypeTest typeTest)
        {
           UserStat userStat;
            try
            {
                if(await _repo.GetSatUserCat(categoryId,userId) != null) userStat = await _repo.GetSatUserCat(categoryId, userId);
                else
                {
                    userStat = new UserStat();
                    userStat.AverageAccuracy = 0;
                    userStat.AverageWPM = 0;
                    userStat.NumberOfTests = 0;
                    userStat.TotalTestTime = 0;
                    userStat.Wins = 0;
                    userStat.Losses = 0;
                    userStat.WLRatio = 0.0;
                    userStat.WinStreak= 0;
                }

                userStat.TotalTestTime += typeTest.TimeTaken;
                double numWords = (double)typeTest.NumberOfWords;
                numWords = numWords / 5;
                double numErrors = (double)typeTest.NumberOfErrors;
                numErrors = numErrors / 5;
                double intermediateCalc = numWords / (numWords+numErrors);
                userStat.AverageAccuracy = ((userStat.AverageAccuracy * userStat.NumberOfTests) + intermediateCalc) / (userStat.NumberOfTests + 1);
                userStat.AverageWPM = ((userStat.AverageWPM * userStat.NumberOfTests) + typeTest.WPM) / (userStat.NumberOfTests + 1);
                userStat.NumberOfTests += 1;
                userStat = await _repo.AddUpdateStats(categoryId, userId, userStat);
                await _repo.AddCategory(new Category() { Id = CategoryDefinitions.Average });
                UserStat usAvg = new UserStat();
                Category avgCat = await _repo.GetCategoryById(CategoryDefinitions.Average);
                if (await _repo.GetSatUserCat(avgCat.Id, userId) != null) usAvg = await _repo.GetSatUserCat(avgCat.Id, userId);
                else
                {
                    usAvg = new UserStat();
                    usAvg.AverageAccuracy = 0;
                    usAvg.AverageWPM = 0;
                    usAvg.NumberOfTests = 0;
                    usAvg.TotalTestTime = 0;
                    usAvg.Wins = 0;
                    usAvg.Losses = 0;
                    usAvg.WLRatio = 0;
                    usAvg.WinStreak = 0;  
                }
                usAvg.TotalTestTime += typeTest.TimeTaken;
                usAvg.NumberOfTests += 1;
                if (usAvg.AverageAccuracy != 0) usAvg.AverageAccuracy = ((intermediateCalc * typeTest.TimeTaken) / usAvg.TotalTestTime) + ((usAvg.AverageAccuracy * (usAvg.TotalTestTime - typeTest.TimeTaken)) / usAvg.TotalTestTime);
                else usAvg.AverageAccuracy = intermediateCalc;
                if (usAvg.AverageWPM != 0) usAvg.AverageWPM = ((typeTest.WPM * typeTest.TimeTaken) / usAvg.TotalTestTime) + ((usAvg.AverageWPM * (usAvg.TotalTestTime - typeTest.TimeTaken)) / usAvg.TotalTestTime);
                else usAvg.AverageWPM = typeTest.WPM;
                typeTest.UserStatId = userStat.Id;
                await _repo.AddUpdateStats(avgCat.Id, userId, usAvg);
                await _repo.AddTest(typeTest);
                User u = await _repo.GetUser(userId);
                u.Revapoints += (int)(typeTest.WPM * intermediateCalc);
                await _repo.UpdateUser(u);
                List<UserStat> userStats = new List<UserStat>();
                userStats.Add(userStat);
                userStats.Add(usAvg);
                return userStats;


            }catch (Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error(e.Message);
                Log.Error("Error occured in AddTest BL");
                return null;
            }
        }

        public async Task<UserStat> GetAvgUserStat(string userId)
        {
            List <UserStatCatJoin> uscjs = await _repo.GetUserStats(userId);
            List<UserStat> userStats = new List<UserStat>();
            foreach(UserStatCatJoin uscj in uscjs)
            {
                UserStat userStat = await _repo.GetUserStatById(uscj.UserStatId);
                userStats.Add(userStat);
            }
            if (userStats.Count == 0) return new UserStat();
            else
            {
                UserStat userStat = new UserStat();
                userStat.TotalTestTime = 0;
                userStat.NumberOfTests = 0;
                userStat.AverageAccuracy = 0;
                userStat.AverageWPM = 0;
                foreach (UserStat u in userStats) userStat.TotalTestTime += u.TotalTestTime;
                foreach (UserStat us in userStats)
                {
                    userStat.NumberOfTests += us.NumberOfTests;
                    userStat.AverageAccuracy += (us.AverageAccuracy * us.TotalTestTime) / userStat.TotalTestTime;
                    userStat.AverageWPM += (us.AverageWPM * us.TotalTestTime) / userStat.TotalTestTime;
                }
                return userStat;
            }
            
        }

        public async Task<List<Tuple<int, List<TypeTest>>>> GetTypeTestForUserByCategory(string userId)
        {
            return await _repo.GetTypeTestForUserByCategory(userId);
        }

        public async Task<List<TypeTest>> GetTypeTestsForUser(string userId)
        {
            return await _repo.GetTypeTestsForUser(userId);
        }

        public async Task<UserStat> GetUserStatByUSId(int userstatid)
        {
            return await _repo.GetUserStatById(userstatid);
        }

        public async Task<List<UserStatCatJoin>> GetUserStats(string userId)
        {
            return await _repo.GetUserStats(userId);
        }

        public async Task<TypeTest> SaveTypeTest(int errors, int charactersTyped, int timeTaken, int WPM, DateTime date)
        {
            return await Task.Run(() =>
            {
                TypeTest test = new TypeTest();
                test.NumberOfErrors = errors;
                test.NumberOfWords = charactersTyped;
                test.TimeTaken = timeTaken;
                test.Date = date;
                test.WPM = WPM;
                return test;
            });
               

        }

        public async Task<List<UserStat>> UpdateWL(List<UserStat> userStats, bool won, int winstreak)
        {
            try
            {
                foreach(UserStat userStat in userStats)
                {
                    if (won) ++userStat.Wins;
                    else ++userStat.Losses;
                    double winner = userStat.Wins;
                    double loser = userStat.Losses;
                    if (userStat.Losses != 0)
                    {
                        userStat.WLRatio = winner / (winner + loser);
                    }
                    if (winstreak > userStat.WinStreak) userStat.WinStreak = winstreak;
                }
                await _repo.SaveChanges();
                return userStats;
            }catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Unexpected error in UpdateWL");
                return null;
            }
        }
    }
}
