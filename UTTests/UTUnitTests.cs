using System;
using Xunit;
using UserTestsBL;
using UserTestsDL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Serilog;
using UserTestsModels;
using System.Collections.Generic;

namespace GACDTests
{
    public class UTUnitTests
    {
        private readonly DbContextOptions<UserTestDBContext> options;
        public UTUnitTests()
        {
            options = new DbContextOptionsBuilder<UserTestDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        /// <summary>
        /// Method to make sure AddUser adds a user to the db
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddUserShouldAddUserAsync()
        {
            using(var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                User user = new User();
                user.Auth0Id = "test";
                await userBL.AddUser(user);
                int userCount = (await userBL.GetUsers()).Count;
                int expected = 1;
                Assert.Equal(expected, userCount);
            }
        }

        [Fact]
        public async Task GetAllUsersShouldReturnAList()
        {
        //Given
        using(var context=new UserTestDBContext(options)){

            IUserBL userBL=new UserBL(context);
            User user1=new User();
            user1.Auth0Id="Auth0Id001";
            User user2=new User();
            user2.Auth0Id="Auth0Id002";

            userBL.AddUser(user1);
            userBL.AddUser(user2);
            int size=(await  userBL.GetUsers()).Count;

            Assert.True(size==2);
        }
        
        //When
        
        //Then
        }

        /// <summary>
        /// Makes sure that Categories can be added
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
        [Fact]
        public async Task AddCatShouldAddCatAsync()
        {
            using(var context = new UserTestDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                Category category1 = new Category();
                category1.Name = 2;
                await categoryBL.AddCategory(category1);
                Category category2 = new Category();
                category2.Name = 3;
                await categoryBL.AddCategory(category2);
                int catCount = (await categoryBL.GetAllCategories()).Count;
                int expected = 3;
                Assert.Equal(expected, catCount);
            }
        }
        /// <summary>
        /// Makes sure UserStats updates and doesn't fail
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
        [Fact]
        public async Task UserStatShouldAddUserStatAsync()
        {
            using(var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                User user = new User();
                user.Auth0Id = "testid";
                user = await userBL.AddUser(user);
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                category = await categoryBL.AddCategory(category);
                IUserStatBL userStatBL = new UserStatBL(context);
                TypeTest typeTest = new TypeTest();
                typeTest.Date = DateTime.Now;
                typeTest.NumberOfErrors = 1;
                typeTest.NumberOfWords = 3;
                typeTest.WPM = 30;
                typeTest.TimeTaken = 5;
                UserStat ust = (await userStatBL.AddTestUpdateStat(1, 1, typeTest))[0];
                Assert.NotNull(ust);
            }
        }
       
        /// <summary>
        /// Makes sure Average WPM is actual average
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
        [Fact]
        public async Task AverageWPMShouldBeAverage()
        {
            using (var context = new UserTestDBContext(options))
            {
                User user = new User();
                user.Auth0Id = "test";
                IUserBL userBL = new UserBL(context);
                ICategoryBL categoryBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                await userBL.AddUser(user);
                Double avgExpected;
                TypeTest testToBeInserted = await userStatBL.SaveTypeTest(1, 50, 100, 100, DateTime.Now);
                avgExpected = testToBeInserted.WPM/2;
                await userStatBL.AddTestUpdateStat(1, 1, testToBeInserted);
                TypeTest testToBeInserted1 = await userStatBL.SaveTypeTest(1, 50, 100, 100, DateTime.Now);
                avgExpected += testToBeInserted1.WPM/2;
                Double actual = (await userStatBL.AddTestUpdateStat(1, 1, testToBeInserted))[0].AverageWPM;
                Assert.Equal(avgExpected, actual);
            }
        }
       
        /// <summary>
        /// Makes sure adding two of the same category returns null
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task AddingCategoryTwiceShouldBeNull()
        {
            using( var context  = new UserTestDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                Assert.Null(await categoryBL.AddCategory(category));
            }        
        }
        /// <summary>
        /// Makes sure adding two of the same user returns null
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task AddingUserTwiceShouldBeNull()
        {
            using (var context = new UserTestDBContext(options)) {
                IUserBL userBL = new UserBL(context);
                User user  = new User();
                user.Auth0Id = "test";
                await userBL.AddUser(user);
                Assert.Null(await userBL.AddUser(user));
                
            }
            
        }
        /// <summary>
        /// Makes sure we are able to get a user by their id
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetUserByIdShouldWork()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                User user = new User();
                user.Auth0Id = "test";
                await userBL.AddUser(user);
                string expected = "test";
                string actual = (await userBL.GetUser(1)).Auth0Id;
                Assert.Equal(expected, actual);
            }
        }
        /// <summary>
        /// Makes sure that a user not in the database returns null
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetBadUserIdShouldBeNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                Assert.Null(await userBL.GetUser(1));
            }
        }

        [Fact]
        public async Task GetUserByuserID()
        {
            using (var context = new UserTestDBContext(options))
            {
                User user = new User();
                user.Auth0Id = "testid";
                IUserBL userBL = new UserBL(context);
                userBL.AddUser(user);
                

                Assert.NotNull(await userBL.GetUser("testid"));
            }
        }
      
        /// <summary>
        /// Makes sure that we are able to get category by id
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetCategoryByIdShouldWork()
        {
            using (var context = new UserTestDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 3;
                await categoryBL.AddCategory(category);
                Category category1 = await categoryBL.GetCategoryById(1);
                int expected = 3;
                int actual = category1.Name;
                Assert.Equal(expected, actual);
            }
        }
        
        /// <summary>
        /// Makes sure the typetest getting method returns a test we add
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetTypeTestsShouldGetaTypeTest()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                User user = new User();
                user.Auth0Id = "testid";
                user = await userBL.AddUser(user);
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                category = await categoryBL.AddCategory(category);
                IUserStatBL userStatBL = new UserStatBL(context);
                TypeTest typeTest = new TypeTest();
                typeTest.Date = DateTime.Now;
                typeTest.NumberOfErrors = 1;
                typeTest.NumberOfWords = 3;
                typeTest.WPM = 30;
                typeTest.TimeTaken = 5;
                UserStat ust = (await userStatBL.AddTestUpdateStat(1, 1, typeTest))[0];
                int expected = 1;
                int actual = (await userStatBL.GetTypeTestsForUser(1)).Count;
                Assert.Equal(expected,actual);
            }
        }
        [Fact]
        public async Task UpdateWLShouldWork()
        {
            using (var context = new UserTestDBContext(options))
            {
                User user = new User();
                user.Auth0Id = "test";
                IUserBL userBL = new UserBL(context);
                ICategoryBL categoryBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                await userBL.AddUser(user);
                Double avgExpected;
                TypeTest testToBeInserted = await userStatBL.SaveTypeTest(1, 50, 100, 100, DateTime.Now);
                List<UserStat> userStats = await userStatBL.AddTestUpdateStat(1, 1, testToBeInserted);
                Assert.NotNull(userStatBL.UpdateWL(userStats,true, 5));
            }
        }
        [Fact]
        public async Task UpdateWLShouldHaveCorrectWins()
        {
            using (var context = new UserTestDBContext(options))
            {
                User user = new User();
                user.Auth0Id = "test";
                IUserBL userBL = new UserBL(context);
                ICategoryBL categoryBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                await userBL.AddUser(user);
                Double avgExpected;
                TypeTest testToBeInserted = await userStatBL.SaveTypeTest(1, 50, 100, 100, DateTime.Now);
                List<UserStat> userStats = await userStatBL.AddTestUpdateStat(1, 1, testToBeInserted);
                int expected = 1;
                Assert.Equal(expected,(await userStatBL.UpdateWL(userStats, true, 5))[0].Wins);
            }
        }
        private void Seed()
        {
            using(var context = new UserTestDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
