using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestsBL;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using RestSharp;
using Newtonsoft.Json;
using UserTestsModels;
using Serilog;
using UserTestsREST.DTO;


namespace UserTestsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserBL _userBL;
        private readonly ApiSettings _ApiSettings;
        public UserController(IUserBL userBL, IOptions<ApiSettings> settings)
        {
            _userBL = userBL;
            _ApiSettings = settings.Value;
        }
        /// <summary>
        /// GET /api/User/username
        /// Gets a username and basic information with a given user
        /// </summary>
        /// <returns>DTO with username / basic information associated with user or 404 if user can't be found</returns>
        // GET api/<UserController>/5
        [Authorize]
        [HttpGet("username")]
        public async Task<ActionResult<UserNameModel>> Get()
        {
            try
            {
                string UserID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (await _userBL.GetUser(UserID) == null)
                {
                    UserTestsModels.User user = new UserTestsModels.User();
                    user.Auth0Id = UserID;
                    await _userBL.AddUser(user);
                }
                User u = await _userBL.GetUser(UserID);
                dynamic AppBearerToken = GetApplicationToken();
                var client = new RestClient($"https://kwikkoder.us.auth0.com/api/v2/users/{u.Auth0Id}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", "Bearer " + AppBearerToken.access_token);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic deResponse = JsonConvert.DeserializeObject(restResponse.Content);
                UserNameModel userNameModel = new UserNameModel();
                try
                {
                    userNameModel.UserName = deResponse.username;
                    userNameModel.Name = deResponse.name;
                    userNameModel.Revapoints = u.Revapoints;
                }
                catch (Exception e) { Log.Information(e.Message); }
                return userNameModel;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error("Unexpected error occured in LBController");
                return NotFound();
            }
        }

        /// <summary>
        /// Private method to get application token for auth0 management 
        /// </summary>
        /// <returns>dynamic object with token for Auth0 call</returns>
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
