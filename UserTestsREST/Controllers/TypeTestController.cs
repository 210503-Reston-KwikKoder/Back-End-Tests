using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestsBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using UserTestsModels;
using UserTestsREST.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RestSharp;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;

namespace UserTestsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeTestController : ControllerBase
	{
		private readonly ISnippets _snippetsService;
        
        private readonly IUserStatBL _userStatService;
        private readonly IUserBL _userBL;
        private readonly ICategoryBL _categoryBL;
        private readonly ApiSettings _ApiSettings;
        private List<UserStat> userStats; 
        public TypeTestController(ISnippets snip, IUserStatBL _userstat, IUserBL userBL, ICategoryBL categoryBL, IOptions<ApiSettings> settings)

        {
            _userBL = userBL;
            _snippetsService = snip;
            _userStatService = _userstat;
            _categoryBL = categoryBL;
            _ApiSettings = settings.Value;
        }
        /// <summary>
        /// GET /api/TypeTest
        /// Gets a random quote, author, and length from http://api.quotable.io/random
        /// </summary>
        /// <returns>TestMaterial DTO or 500 on internal server error</returns>
        [HttpGet]
        public async Task<TestMaterial> GetQuote()
        {
            return await _snippetsService.GetRandomQuote();
        }
        /// <summary>
        /// GET /api/TypeTest/{id}
        /// Used to get a random quote on -1 or language number from Octokit.Language to search in 
        /// https://raw.githubusercontent.com/ for a random file in that language to get for coding test
        /// </summary>
        /// <param name="id">Category to get test from</param>
        /// <returns> TestMaterial DTO or 500 on internal server error</returns>
        [HttpGet("{id}")]
        public async Task<TestMaterial> CodeSnippet(int id)
        {
            if (id == -1) return await _snippetsService.GetRandomQuote();
            else return await _snippetsService.GetCodeSnippet(id);
        }

        /// <summary>
        /// POST /api/TypeTest
        /// Used to post the results of a type test to the database, adding a category and/or user if necessary and 
        /// updating user stats.
        /// </summary>
        /// <param name="typeTest">Typetest to insert</param>
        /// <returns>400 if request can't be processed, 200 if successful</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateTypeTest(TypeTestInput typeTest)
        {
            //get userID and then go to type test creation
            Log.Information(typeTest.categoryId.ToString());
            string UserID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await TrueAddTest(typeTest,UserID);
        }
        [HttpPost("comptest")]
        public async Task<ActionResult> CompTypeTest(CompTestInput compTestInput)
        {
            TypeTestInput typeTestInput = compTestInput;
            await TrueAddTest(typeTestInput, compTestInput.auth0Id);
            try
            {
                //update wins and losses
               await _userStatService.UpdateWL(userStats, compTestInput.won, compTestInput.winStreak);
                return NoContent();
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Bad request in comptest");
                return BadRequest();
            }
        }
        private async Task<ActionResult>TrueAddTest(TypeTestInput typeTest, string UserID)
        {
            //check if user and category exists before adding test   
            if (await _userBL.GetUser(UserID) == null)
            {
                UserTestsModels.User user = new UserTestsModels.User();
                user.Auth0Id = UserID;
                await _userBL.AddUser(user);
            }
            if (await _categoryBL.GetCategory(typeTest.categoryId) == null)
            {
                Category category = new Category();
                category.Name = typeTest.categoryId;
                await _categoryBL.AddCategory(category);
            }
            Category category1 = await _categoryBL.GetCategory(typeTest.categoryId);
            UserTestsModels.User user1 = await _userBL.GetUser(UserID);
            TypeTest testToBeInserted = await _userStatService.SaveTypeTest(typeTest.numberoferrors, typeTest.numberofcharacters, typeTest.timetakenms, typeTest.wpm, typeTest.date);
            
            try
            {
                //get user's info for leaderboard
                userStats = await _userStatService.AddTestUpdateStat(user1.Id, category1.Id, testToBeInserted);
                dynamic AppBearerToken = GetApplicationToken();
                var client = new RestClient($"https://kwikkoder.us.auth0.com/api/v2/users/{UserID}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", "Bearer " + AppBearerToken.access_token);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic deResponse = JsonConvert.DeserializeObject(restResponse.Content);
                List<LBModel> lbModelsToSend = new List<LBModel>();
                LBModel lbModel = new LBModel();
                try
                {
                    lbModel.UserName = deResponse.username;
                    lbModel.Name = deResponse.name;
                    lbModel.CatID = typeTest.categoryId;
                    lbModel.AuthId = UserID;
                    lbModel.AverageWPM = userStats[0].AverageWPM;
                    lbModel.AverageAcc = userStats[0].AverageAccuracy;
                }
                catch (Exception e) { Log.Information(e.Message); }
                lbModelsToSend.Add(lbModel);
                LBModel avglbModel = new LBModel();
                try
                {
                    avglbModel.UserName = deResponse.username;
                    avglbModel.Name = deResponse.name;
                    avglbModel.CatID = -2;
                    avglbModel.AuthId = UserID;
                    avglbModel.AverageWPM = userStats[1].AverageWPM;
                    avglbModel.AverageAcc = userStats[1].AverageAccuracy;
                }
                catch (Exception e) { Log.Information(e.Message); }
                lbModelsToSend.Add(avglbModel);
                //Convert userstat and info into json for leaderboard backend to receive
                string modelsJson = JsonConvert.SerializeObject(lbModelsToSend);
                StringContent content = new StringContent(modelsJson, Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();
                //call leaderboard put endpoint with new info
                HttpResponseMessage result = await httpClient.PutAsync("http://20.69.69.228/lb/api/LB", content);
            }

            catch (Exception e)
            {
                Log.Error("Bad Request received in TypeTest Post");
                Log.Error(e.StackTrace);
                return BadRequest();
            }


            return Ok();
        }
        private dynamic GetApplicationToken()
        {
            var client = new RestClient("https://kwikkoder.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", _ApiSettings.authString, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Log.Information("Response: {0}", response.Content);
            return JsonConvert.DeserializeObject(response.Content);
        }

    }
}
