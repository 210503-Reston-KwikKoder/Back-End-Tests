using System;
using Xunit;
using UserTestsBL;
using UserTestsDL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Serilog;
using UserTestsModels;
using System.Collections.Generic;

namespace UTTests
{
    public class UTRepoUnitTest
    {
        private readonly DbContextOptions<UserTestDBContext> options;
        public UTRepoUnitTest()
        {
            options = new DbContextOptionsBuilder<UserTestDBContext>().UseSqlite("Filename=TestRepo.db").Options;
            Seed();
        }

        [Fact]
        public async Task VerifyAddUserShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                User user = new User();
                user.Auth0Id = null;
                User expected = null;
                var test = await _repo.AddUser(user);
                Assert.Equal(test, expected);
            }
        }

         [Fact]
        public async Task VerifyGetCategoryByIdShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var test = await _repo.GetCategoryById(-1);
                Category expected = null;
                Assert.Equal(test, expected);
            }
        }       

         [Fact]
        public async Task VerifyGetCategoryByNameShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var test = await _repo.GetCategoryByName(-1);
                Category expected = null;
                Assert.Equal(test, expected);
            }
        }   

        [Fact]
        public async Task VerifyGetUserStatsShouldReturnEmptyList()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var test = await _repo.GetUserStats(-1);
                List<UserStatCatJoin> expected = new List<UserStatCatJoin>();
                Assert.Equal(test, expected);
                Assert.True(test.Count == 0);
                Assert.Empty(test);
            }
        }  

        [Fact]
        public async Task VerifyGetUserStatsShouldReturnListOfUserStatCatJoin()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = 1;
                var test = await _repo.GetUserStats(target);
                Assert.True(test.Count == 1);
            }
        }  

        [Fact]
        public async Task BL_VerifyGetUserStatsShouldReturnListOfUserStatCatJoin()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = 1;
                var test = await _bl.GetUserStats(target);
                Assert.True(test.Count == 1);
            }
        }  

        [Fact]
        public async Task VerifyGetUserStatsByIdShouldReturnUserStat()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = 1;
                var test = await _repo.GetUserStatById(target);
                Assert.True(test.Id == target);
            }
        } 

        [Fact]
        public async Task BL_VerifyGetUserStatsByIdShouldReturnUserStat()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = 1;
                var test = await _bl.GetUserStatByUSId(target);
                Assert.True(test.Id == target);
            }
        } 

        [Fact]
        public async Task VerifyAddTestUpdateStatShouldReturnListOfUserStat()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var userId = -1;
                var categoryId = -1;
                TypeTest typeTest = new TypeTest()
                {
                    Id = 1
                };
                var test = await _bl.AddTestUpdateStat(userId, categoryId, typeTest);
                List<UserStat> expected = null;
                Assert.Equal(expected, test);
            }
        }

        [Fact]
        public async Task VerifyGetAvgUserStatShouldReturnUserStat()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = 1;
                var test = await _bl.GetAvgUserStat(target);
                var expected = 50;
                Assert.Equal(expected, test.AverageWPM);
            }
        }

        [Fact]

        public async Task VerifyGetAvgUserStatReturnsNewUserStat(){
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = 2;
                var test = await _bl.GetAvgUserStat(target);
                var expected = 0;
                Assert.Equal(expected, test.AverageWPM);
            }
        }

        [Fact]
        public async Task VerifyGetUserStatsByIdShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = -1;
                var test = await _repo.GetUserStatById(target);
                UserStat expected = null;
                Assert.Equal(test, expected);
            }
        } 

        [Fact]
        public async Task VerifyGetTypeTestsForUserReturnEmptyList()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = -1;
                var test = await _repo.GetTypeTestsForUser(target);
                List<TypeTest> expected = new List<TypeTest>();
                Assert.Equal(test, expected);
                Assert.True(test.Count == 0);
                Assert.Empty(test);
            }
        } 

        [Fact]
        public async Task VerifyGetTypeTestForUserByCategoryShoudReturnEmptyList()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = -1;
                var test = await _repo.GetTypeTestForUserByCategory(target);
                List<Tuple<int, List<TypeTest>>> expected = new List<Tuple<int, List<TypeTest>>>();
                Assert.Equal(test, expected);
            }
        }

        [Fact]
        public async Task BL_VerifyGetTypeTestForUserByCategoryShoudReturnEmptyList()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = -1;
                var test = await _bl.GetTypeTestForUserByCategory(target);
                List<Tuple<int, List<TypeTest>>> expected = new List<Tuple<int, List<TypeTest>>>();
                Assert.Equal(test, expected);
            }
        }

        [Fact]
        public async Task VerifyGetTypeTestForUserByCategoryShoudReturnListOfTuple()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var target = 1;
                 List<Tuple<int, List<TypeTest>>> test = await _repo.GetTypeTestForUserByCategory(target);
                Assert.True(test.Count == 1);
                Assert.True(test[0].Item2.Count == 1);
            }
        }

        [Fact]
        public async Task BL_VerifyGetTypeTestForUserByCategoryShoudReturnListOfTuple()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserStatBL _bl = new UserStatBL(context);
                var target = 1;
                 List<Tuple<int, List<TypeTest>>> test = await _bl.GetTypeTestForUserByCategory(target);
                Assert.True(test.Count == 1);
                Assert.True(test[0].Item2.Count == 1);
            }
        }

        private void Seed()
        {
            using(var context = new UserTestDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Auth0Id = "abc"
                    },
                    new User
                    {
                        Id = 2,
                        Auth0Id = "def"
                    }
                );
                context.UserStats.AddRange(
                    new UserStat
                    {
                        Id = 1,
                        TotalTestTime = 1,
                        AverageAccuracy = 1,
                        AverageWPM = 50,
                        UserStatCatJoin = new UserStatCatJoin
                        {
                            UserId = 1,
                            UserStatId = 1,
                            CategoryId = 1
                        },
                        TypeTests = new List<TypeTest>(){
                            new TypeTest {
                                Id = 1,
                                UserStatId = 1
                            }
                        }
                    }
                );
                context.Categories.AddRange(
                    new Category
                    {
                        Id = 1,
                        Name = 1
                    }
                );
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Danger Zone - Ensure Return null situations that match the DL Repo Calls
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerifyGetAllCategoriesShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                context.Database.EnsureDeleted();
                var test = await _repo.GetAllCategories();
                List<Category> expected = null;
                Assert.Equal(test, expected);
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task VerifyGetTypeTestForUserByCategoryShoudReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                context.Database.EnsureDeleted();
                var target = -1;
                var test = await _repo.GetTypeTestForUserByCategory(target);
                List<Tuple<int, List<TypeTest>>> expected = null;
                Assert.Equal(test, expected);
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task VerifyGetAllUsersShouldReturnEmptyList()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                context.Database.EnsureDeleted();
                var test = await _repo.GetAllUsers();
                List<User> expected = new List<User>();
                Assert.Equal(test, expected);
                Assert.True(test.Count == 0);
                Assert.Empty(test);
                context.Database.EnsureCreated();
            }
        }


        [Fact]

        public async Task VerifyGetUserStatsReturnsNewListOnError(){
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var list = await _repo.GetUserStats(2);

                Assert.Empty(list);
            }
        }
        
        [Fact]
        public async Task VerifyGetTypeTestsForUserReturnsNewList(){
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);
                var list = await _repo.GetTypeTestsForUser(2);

                Assert.Empty(list);                
            }

        }

    }
}