using System;
using Xunit;
using UserTestsBL;
using UserTestsREST.Controllers;
using Microsoft.AspNetCore.Authorization;
//using System.Web.;
using Moq;
using Microsoft.Extensions.Options;

namespace UTTests
{
    public class TypeTestControllerTest
    {

        [Fact]
        public void CreateTypeTest_Should_require_Autorization(){
            var mockTypeTest=new Mock<ISnippets>();
            var mockUserStat = new Mock<IUserStatBL>();
            var mockUserBL = new Mock<IUserBL>();
            var mockCategoryBL = new Mock<ICategoryBL>();
            var mockUserSettings = new Mock<IOptions<ApiSettings>>();
            TypeTestController controller=new TypeTestController(mockTypeTest.Object, mockUserStat.Object, mockUserBL.Object, mockCategoryBL.Object, mockUserSettings.Object);
     
            var actualAtributes=controller.GetType().GetMethod("CreateTypeTest").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }
        
    }
}