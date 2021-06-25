using System;
using Xunit;
using UserTestsBL;
using UserTestsREST.Controllers;
using Microsoft.AspNetCore.Authorization;
//using System.Web.;
using Moq;
namespace UTTests
{
    public class TypeTestControllerTest
    {

        [Fact]
        public void CreateTypeTest_Should_require_Autorization(){
            var mockTypeTest=new Mock<ISnippets>();

            TypeTestController controller=new TypeTestController(mockTypeTest.Object);
     
            var actualAtributes=controller.GetType().GetMethod("CreateTypeTest").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }
        
    }
}