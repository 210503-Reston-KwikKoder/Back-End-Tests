using UserTestsModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UserTestsDL
{
    public class Repo : IRepo
    {
        private readonly UserTestDBContext _context;
        public Repo(UserTestDBContext context)
        {
            _context = context;
            Log.Debug("Repo instantiated");
        }

        public async Task<Category> AddCategory(Category cat)
        {
            try
            {
                //Make sure category doesn't already exists
                await (from c in _context.Categories
                       where c.Id == cat.Id
                       select c).SingleAsync();
                return null;
            }
            catch (Exception)
            {
                await _context.Categories.AddAsync(cat);
                await _context.SaveChangesAsync();
                Log.Information("New category created.");
                return cat;
            }
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                if (await GetUser(user.Auth0Id) != null) return null;
                user.Revapoints = 0;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }


        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await (from c in _context.Categories
                              select c).ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await (from u in _context.Users
                              select u).ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return new List<User>();
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                return await (from c in _context.Categories
                              where c.Id == id
                              select c).SingleAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Error finding category returning null");
                return null;
            }
        }

        public async Task<User> GetUser(string auth0id)
        {
            try
            {
                return await (from u in _context.Users
                              where u.Auth0Id == auth0id
                              select u).SingleAsync();
            }
            catch (Exception)
            {
                Log.Error("User not found");
                return null;
            }
        }
        public async Task<List<UserStatCatJoin>> GetUserStats(string userId)
        {
            try
            {
                List<UserStatCatJoin> uscjs = await (from uscj in _context.UserStatCatJoins
                                                     where uscj.UserId == userId
                                                     select uscj).ToListAsync();
                return uscjs;
            }
            catch (Exception e)
            {
                Log.Error("No stats for user were found");
                Log.Error(e.Message);
                return new List<UserStatCatJoin>();
            }
            throw new NotImplementedException();
        }
        public async Task<UserStat> GetSatUserCat(int categoryId, string userId)
        {
            try
            {
                int userStatId = await (from uscj in _context.UserStatCatJoins
                                        where uscj.CategoryId == categoryId &&
                                        uscj.UserId == userId
                                        select uscj.UserStatId).SingleAsync();
                UserStat uStatInDB = await (from uS in _context.UserStats
                                            where uS.Id == userStatId
                                            select uS).SingleAsync();
                return uStatInDB;
            }
            catch (Exception)
            {
                Log.Debug("Stat not found, returning null");
                return null;
            }
        }
        public async Task<UserStat> AddUpdateStats(int categoryid, string userid, UserStat userStat)
        {
            string userIdsave = userid;
            //Assuming these categories and users exist
            try
            {
                int userStatId = await (from uscj in _context.UserStatCatJoins
                                        where uscj.CategoryId == categoryid &&
                                        uscj.UserId == userid
                                        select uscj.UserStatId).SingleAsync();
                UserStat uStatInDB = await (from uS in _context.UserStats
                                            where uS.Id == userStatId
                                            select uS).SingleAsync();
                uStatInDB.AverageWPM = userStat.AverageWPM;
                uStatInDB.AverageAccuracy = userStat.AverageAccuracy;
                uStatInDB.NumberOfTests = userStat.NumberOfTests;
                uStatInDB.TotalTestTime = userStat.TotalTestTime;
                
                uStatInDB.TotalTestTime = userStat.TotalTestTime;
                uStatInDB.TotalTestTime = userStat.TotalTestTime;
                await _context.SaveChangesAsync();
                return uStatInDB;
            }
            catch (Exception)
            {
                await _context.UserStats.AddAsync(userStat);
                await _context.SaveChangesAsync();
          
                UserStatCatJoin uscj = new UserStatCatJoin();
                uscj.CategoryId = categoryid;
                uscj.UserId = userIdsave;
                uscj.UserStatId = userStat.Id;
                await _context.UserStatCatJoins.AddAsync(uscj);
                await _context.SaveChangesAsync();
            }
            return userStat;
        }
        public async Task<UserStat> GetUserStatById(int id)
        {
            try
            {
                return await (from u in _context.UserStats
                              where u.Id == id
                              select u
                        ).SingleAsync();
            }
            catch (Exception)
            {
                Log.Fatal("UserStatJoin not maintained correctly returning null");
                return null;
            }
        }
        public async Task<List<TypeTest>> GetTypeTestsForUser(string userId)
        {
            try
            {
                List<int> userStatIds = await (from uscj in _context.UserStatCatJoins
                                               where uscj.UserId == userId
                                               select uscj.UserStatId).ToListAsync();
                List<TypeTest> typeTests = new List<TypeTest>();
                foreach (int usId in userStatIds)
                {
                    List<TypeTest> testForStat = await (from test in _context.TypeTests
                                                        where test.UserStatId == usId
                                                        select test).ToListAsync();
                    typeTests.AddRange(testForStat);
                }
                typeTests = typeTests.OrderByDescending(t => t.Date).ToList();
                typeTests = typeTests.Take(100).ToList();
                typeTests = typeTests.OrderBy(t => t.Date).ToList();
                return typeTests;
            }
            catch (Exception)
            {
                Log.Error("No Type Tests found returning empty list");
                return new List<TypeTest>();
            }
        }
        public async Task<TypeTest> AddTest(TypeTest typeTest)
        {
            try
            {
                await _context.TypeTests.AddAsync(typeTest);
                await _context.SaveChangesAsync();
                Log.Information("Test added");
                return typeTest;
            }
            catch (Exception)
            {
                Log.Error("Issue adding test");
                return null;
            }
        }

        public async Task<User> UpdateUser(User u)
        {
            try
            {
                return await Task.Run(() =>
                {
                    _context.Users.Update(u);
                    _context.SaveChanges();
                    return u;
                });
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }

        public async Task<List<Tuple<int, List<TypeTest>>>> GetTypeTestForUserByCategory(string userId)
        {
            try
            {
                List<UserStatCatJoin> userStatCats = await(from uscj in _context.UserStatCatJoins
                                              where uscj.UserId == userId
                                              select uscj).ToListAsync();
                List<Tuple<int, List<TypeTest>>> returnTuples = new List<Tuple<int, List<TypeTest>>>();
                foreach ( UserStatCatJoin userStatCatJoin in userStatCats)
                {
                    try
                    {
                        if(userStatCatJoin.CategoryId != -2)
                        {
                            List<TypeTest> typeTestsForStat = (from test in _context.TypeTests
                                                               where test.UserStatId == userStatCatJoin.UserStatId
                                                               orderby test.Date ascending
                                                               select test).ToList();
                            returnTuples.Add(Tuple.Create(userStatCatJoin.CategoryId, typeTestsForStat));
                        }
                    }
                    catch(Exception e)
                    {
                        Log.Fatal(e.Message);
                        Log.Fatal(e.StackTrace);
                    }

                }
                return returnTuples;

            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Goal> AddGoal(Goal goal)
        {
            try
            {
                await _context.Goals.AddAsync(goal);
                await _context.SaveChangesAsync();
                return goal;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Could not add goal, returning null");
                return null;
            }
        }

        public async Task<Goal> GetGoal(int categoryId, string userId)
        {
            try
            {
                Goal goal = await (from g in _context.Goals
                                   where g.CategoryId == categoryId
                                   && g.UserId == userId
                                   && !g.Checked
                                   && g.GoalDate < DateTime.Now
                                   select g).SingleAsync();
                return goal;
            }catch(Exception e)
            {
                Log.Information(e.StackTrace);
                Log.Information("Goal not found");
                return null;
            }
        }

        public async Task<List<Goal>> GetGoalsForUser(string userId)
        {
            try
            {
                List<Goal> goals = await (from g in _context.Goals
                                   where g.UserId == userId
                                   && !g.Checked
                                   && g.GoalDate < DateTime.Now
                                   select g).ToListAsync();
                return goals;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Could not find any goals for the user");
                return new List<Goal>();
            }
        }
    }
}
