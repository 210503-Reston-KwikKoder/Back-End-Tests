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
    public class UserControllerTest
    {
        private readonly DbContextOptions<UserTestDBContext> options;

        //Xunit creates new instances of test classes, you need to make sure that you seed your db for each class
        public UserControllerTest()
        {
            options = new DbContextOptionsBuilder<UserTestDBContext>().UseSqlite("Filename=UTest.db;foreign keys=false").Options;
            Seed();
        }

        [Fact]
        public async void GetShouldReturnAUser()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                var settings = Options.Create(new ApiSettings());

                UserController controller = new UserController(userBL, settings);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "BZ")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

                var result = await controller.Get();
                UserNameModel u = result.Value as UserNameModel;

                int expected = 12;

                Assert.IsType<UserNameModel>(result.Value);
                Assert.Equal(u.Revapoints, expected);
            }
        }

        [Fact]
        public async void GetShouldReturnANewUser()
        {
            using (var context = new UserTestDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                var settings = Options.Create(new ApiSettings());

                UserController controller = new UserController(userBL, settings);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "AB")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

                var result = await controller.Get();

                Assert.IsType<UserNameModel>(result.Value);
            }
        }

        [Fact]
        public async void GetErrorShouldReturnNotFound()
        {
            using (var context = new UserTestDBContext(options))
            {
                var MockUserBL = new Mock<IUserBL>();
                MockUserBL.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new Exception("test"));
                var settings = Options.Create(new ApiSettings());

                UserController controller = new UserController(MockUserBL.Object, settings);

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "AB")
                }));

                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

                var result = await controller.Get();

                Assert.IsType<NotFoundResult>(result.Result);
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

                context.SaveChanges();
            }
        }

    }
}
