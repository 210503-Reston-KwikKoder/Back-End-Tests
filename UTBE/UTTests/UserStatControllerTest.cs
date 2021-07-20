
using System;
using Xunit;
using UserTestsDL;
using UserTestsBL;
using UserTestsModels;
using UserTestsREST.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using UserTestsREST;
using System.Security.Claims;
using UserTestsREST.Controllers;

namespace UTTests
{
    public class UserStatControllerTest
    {
        private readonly DbContextOptions<UserTestDBContext> options;

        //Xunit creates new instances of test classes, you need to make sure that you seed your db for each class
        public UserStatControllerTest()
        {
            options = new DbContextOptionsBuilder<UserTestDBContext>().UseSqlite("Filename=CTest.db;foreign keys=false").Options;
            Seed();
        }

        [Fact]
        public async void GetShouldWork()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                IUserBL userBL = new UserBL(context);


                UserStatController controller = new UserStatController(userStatBL, userBL, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                ActionResult<IEnumerable<StatModel>> action = await controller.GetAsync();
                List<StatModel> UserStat = action.Value.ToList();

                int expected = 2;
                Assert.IsType<List<StatModel>>(action.Value);
                Assert.Equal(expected, UserStat.Count);
            }



        }

        [Fact]
        public async void GetAsyncErrorShouldReturnNotFound()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                //IUserBL userBL = new UserBL(context);

                var MockUserBL = new Mock<IUserBL>();
                MockUserBL.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new Exception("test"));

                UserStatController controller = new UserStatController(userStatBL, MockUserBL.Object, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                var action = await controller.GetAsync();

                Assert.IsType<NotFoundResult>(action.Result);
            }
        }


        [Fact]
        public async void GetTestsWorks()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                IUserBL userBL = new UserBL(context);


                UserStatController controller = new UserStatController(userStatBL, userBL, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                ActionResult<IEnumerable<TestStatOutput>> action = await controller.GetTests();
                List<TestStatOutput> UserStat = action.Value.ToList();

                int expected = 1;
                Assert.IsType<List<TestStatOutput>>(action.Value);
                Assert.Equal(expected, UserStat.Count);
            }
        }

        [Fact]
        public async void GetTestsErrorShouldReturnNull()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                //IUserBL userBL = new UserBL(context);

                var MockUserBL = new Mock<IUserBL>();
                MockUserBL.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new Exception("test"));

                UserStatController controller = new UserStatController(userStatBL, MockUserBL.Object, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                var action = await controller.GetTests();

                Assert.Null(action.Result);
            }
        }

        [Fact]
        public async void GetAllTestsShouldWork()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                IUserBL userBL = new UserBL(context);


                UserStatController controller = new UserStatController(userStatBL, userBL, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                ActionResult<IEnumerable<IEnumerable<TestStatCatOutput>>> action = await controller.GetAllTests();
                List<List<TestStatCatOutput>> UserStat = new List<List<TestStatCatOutput>>();

                foreach (IEnumerable<TestStatCatOutput> test in action.Value)
                {
                    UserStat.Add(test.ToList());
                }

                int expected = 1;
                Assert.IsType<List<List<TestStatCatOutput>>>(action.Value);
                Assert.Equal(expected, UserStat.Count);
            }
        }

        [Fact]
        public async void GetAllTestsErrorShouldReturnNotFound()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                //IUserBL userBL = new UserBL(context);

                var MockUserBL = new Mock<IUserBL>();
                MockUserBL.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new Exception("test"));

                UserStatController controller = new UserStatController(userStatBL, MockUserBL.Object, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                var action = await controller.GetAllTests();

                Assert.IsType<NotFoundResult>(action.Result);
            }
        }

        [Fact]
        public async void GetAvgAsync()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                IUserBL userBL = new UserBL(context);


                UserStatController controller = new UserStatController(userStatBL, userBL, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                ActionResult<StatModel> action = await controller.GetAvgAsync();
                StatModel UserStat = action.Value;

                string expected = "BZ";
                Assert.IsType<StatModel>(action.Value);
                Assert.Equal(expected, UserStat.userID);
            }
        }

        [Fact]
        public async void GetAvgAsyncErrorShouldReturnNotFound()
        {
            using (var context = new UserTestDBContext(options))
            {
                IRepo _repo = new Repo(context);

                ICategoryBL catBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                //IUserBL userBL = new UserBL(context);

                var MockUserBL = new Mock<IUserBL>();
                MockUserBL.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new Exception("test"));

                UserStatController controller = new UserStatController(userStatBL, MockUserBL.Object, catBL);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };



                var action = await controller.GetAvgAsync();

                Assert.IsType<NotFoundResult>(action.Result);
            }
        }


        private void Seed()
        {
            using (var context = new UserTestDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                context.Users.AddRange(
                    new User
                    {
                        Auth0Id = "BZ",
                        
                        Revapoints = 12
                    },
                    new User
                    {
                        Auth0Id = "test",
                        
                        Revapoints = 22
                    },

                    new User
                    {
                        Auth0Id = "test1",
                        
                        Revapoints = 22
                    }

                    );

                context.UserStatCatJoins.AddRange(
                    new UserStatCatJoin
                    {
                        UserId = "BZ",
                        UserStatId = 12,
                        CategoryId = 1
                    },
                    new UserStatCatJoin
                    {
                        UserId = "BZ",
                        UserStatId = 13,
                        CategoryId = -2
                    }
                    );


                context.UserStats.AddRange(
                    new UserStat
                    {
                        AverageAccuracy = 92,
                        Id = 12,
                        AverageWPM = 36,
                        Losses = 2,
                        WLRatio = 1.5,
                        NumberOfTests = 20,
                        TotalTestTime = 33,
                        Wins = 12,
                        WinStreak = 3,
                    },
                    new UserStat
                    {
                        AverageAccuracy = 92,
                        Id = 13,
                        AverageWPM = 36,
                        Losses = 2,
                        WLRatio = 1.5,
                        NumberOfTests = 20,
                        TotalTestTime = 33,
                        Wins = 12,
                        WinStreak = 3,

                    }


                    );

                context.TypeTests.Add(
                    new TypeTest
                    {
                        Id = 1,
                        UserStatId = 12,
                        TimeTaken = 12,
                        Date = DateTime.Now,
                        NumberOfErrors = 2,
                        NumberOfWords = 3,
                        WPM = 22,

                    }
                    );



                context.Categories.AddRange(
                    new Category
                    {
                        Id = 1,
                        
                    },
                    new Category
                    {
                        Id = -2,
                       
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}