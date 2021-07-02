using System.Net.Http;
using System;
using Xunit;
using UserTestsBL;
using UserTestsREST.Controllers;
using Microsoft.AspNetCore.Authorization;
//using System.Web.;
using Moq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using UserTestsREST;
using UserTestsREST.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UTTests
{
    public class TypeTestControllerTest
    {

        [Fact]
        public void CreateTypeTest_Should_require_Autorization(){
             var mockSnippets=new Mock<ISnippets>();
            var mockUserStat = new Mock<IUserStatBL>();
            var mockUserBL = new Mock<IUserBL>();
            var mockCategoryBL = new Mock<ICategoryBL>();
            var mockUserSettings = new Mock<IOptions<ApiSettings>>();
            TypeTestController controller=new TypeTestController(mockSnippets.Object, mockUserStat.Object, mockUserBL.Object, mockCategoryBL.Object, mockUserSettings.Object);
     
            var actualAtributes=controller.GetType().GetMethod("CreateTypeTest").GetCustomAttributes(typeof(AuthorizeAttribute),true);
            Assert.Equal(typeof(AuthorizeAttribute),actualAtributes[0].GetType());
        }

        [Fact]
        public async Task GetQuoteTest(){
            var mockSnippets=new Mock<ISnippets>();
            var mockUserStat = new Mock<IUserStatBL>();
            var mockUserBL = new Mock<IUserBL>();
            var mockCategoryBL = new Mock<ICategoryBL>();
            var mockUserSettings = new Mock<IOptions<ApiSettings>>();

            mockSnippets.Setup(x => x.GetRandomQuote()).Returns(Task.FromResult(new TestMaterial("content", "author", 7)));
            //new Task<TestMaterial>(() => { return new TestMaterial("content", "author", 7);})
            TypeTestController controller=new TypeTestController(mockSnippets.Object, mockUserStat.Object, mockUserBL.Object, mockCategoryBL.Object, mockUserSettings.Object);

            var quote = await controller.GetQuote();

            Assert.Equal("content", quote.content);
            Assert.Equal("author", quote.author);
            Assert.Equal(7, quote.length);
        }

        [Fact]
        public async Task GetSnippetTest(){
            var mockSnippets=new Mock<ISnippets>();
            var mockUserStat = new Mock<IUserStatBL>();
            var mockUserBL = new Mock<IUserBL>();
            var mockCategoryBL = new Mock<ICategoryBL>();
            var mockUserSettings = new Mock<IOptions<ApiSettings>>();
            mockSnippets.Setup(x => x.GetCodeSnippet(32)).Returns(Task.FromResult(new TestMaterial("content", "author", 7)));
            mockSnippets.Setup(x => x.GetRandomQuote()).Returns(Task.FromResult(new TestMaterial("content", "author", 7)));
            //new Task<TestMaterial>(() => { return new TestMaterial("content", "author", 7);})
            TypeTestController controller=new TypeTestController(mockSnippets.Object, mockUserStat.Object, mockUserBL.Object, mockCategoryBL.Object, mockUserSettings.Object);

            var quote = await controller.CodeSnippet(32);
            var quote2 = await controller.CodeSnippet(-1);
;
            Assert.Equal("content", quote.content);
            Assert.Equal("author", quote.author);
            Assert.Equal(7, quote.length);

            Assert.Equal("content", quote2.content);
            Assert.Equal("author", quote2.author);
            Assert.Equal(7, quote2.length);
        }

        [Fact]
        public async Task test(){
            var mockSnippets=new Mock<ISnippets>();
            var mockUserStat = new Mock<IUserStatBL>();
            var mockUserBL = new Mock<IUserBL>();
            var mockCategoryBL = new Mock<ICategoryBL>();
            var mockUserSettings = new Mock<IOptions<ApiSettings>>();

    
            TypeTestController controller=new TypeTestController(mockSnippets.Object, mockUserStat.Object, mockUserBL.Object, mockCategoryBL.Object, mockUserSettings.Object);

            var ttest = new TypeTestInput();

           await Assert.ThrowsAsync<NullReferenceException>(async() => { await controller.CreateTypeTest(ttest);});
        }
        
    }
}