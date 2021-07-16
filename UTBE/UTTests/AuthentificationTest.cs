using System;
using Xunit;
using Microsoft.AspNetCore.Authorization;
//using System.Web.;
using Microsoft.Extensions.Options;
using Moq;

using UserTestsREST.Controllers;
using UserTestsBL;

namespace DFTests
{
    public class AuthentificationTest
    {
     

         [Fact]
        public void GetAllStat_Should_require_Autorization(){
            var mockIUserBL=new Mock<IUserBL>();
            var mockIUserStatBL=new Mock<IUserStatBL>();
            var mockICategoryBL=new Mock<ICategoryBL>();

            UserStatController controller=new UserStatController(mockIUserStatBL.Object,mockIUserBL.Object,mockICategoryBL.Object);
     
            var actualAtributes=controller.GetType().GetMethod("GetAsync").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }

         [Fact]
        public void GetTests_Should_require_Autorization(){
            var mockIUserBL=new Mock<IUserBL>();
            var mockIUserStatBL=new Mock<IUserStatBL>();
            var mockICategoryBL=new Mock<ICategoryBL>();

             UserStatController controller=new UserStatController(mockIUserStatBL.Object,mockIUserBL.Object,mockICategoryBL.Object);
     
            var actualAtributes=controller.GetType().GetMethod("GetTests").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }
        
        
         [Fact]
        public void GetAllTests_Should_require_Autorization(){
            var mockIUserBL=new Mock<IUserBL>();
            var mockIUserStatBL=new Mock<IUserStatBL>();
            var mockICategoryBL=new Mock<ICategoryBL>();

             UserStatController controller=new UserStatController(mockIUserStatBL.Object,mockIUserBL.Object,mockICategoryBL.Object);
     
            var actualAtributes=controller.GetType().GetMethod("GetAllTests").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }      

         [Fact]
        public void GetAvgAsync_Should_require_Autorization(){
            var mockIUserBL=new Mock<IUserBL>();
            var mockIUserStatBL=new Mock<IUserStatBL>();
            var mockICategoryBL=new Mock<ICategoryBL>();

             UserStatController controller=new UserStatController(mockIUserStatBL.Object,mockIUserBL.Object,mockICategoryBL.Object);
     
            var actualAtributes=controller.GetType().GetMethod("GetAvgAsync").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }    


         [Fact]
        public void GetUvserByUsername_Should_require_Autorization(){
            var mockIUserBL=new Mock<IUserBL>();
            
            var settings = Options.Create(new ApiSettings());
       

             UserController controller=new UserController(mockIUserBL.Object,settings);
     
            var actualAtributes=controller.GetType().GetMethod("Get").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }                
    }
}