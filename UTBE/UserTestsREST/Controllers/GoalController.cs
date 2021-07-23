using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserTestsBL;
using UserTestsModels;
using UserTestsModels.Utility;
using UserTestsREST.DTO;
using UserTestsREST.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserTestsREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IUserStatBL _userStatBL;
        private readonly IUserBL _userBL;
        private readonly ICategoryBL _categoryBL;
        private readonly IGoalBL _goalBL;
        public GoalController(IUserStatBL userstat, IUserBL userBL, ICategoryBL categoryBL, IGoalBL goalBL)

        {
            _userStatBL = userstat;
            _userBL = userBL;
            _categoryBL = categoryBL;
            _goalBL = goalBL;
        }
        // GET: api/<GoalController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GoalOutput>>> Get()
        {
            string UserID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _userBL.GetUser(UserID) == null) return NotFound("No such user found");
            else if ((await _goalBL.ClaimGoals(UserID)).Count == 0) return NotFound("No goals found for user");
            else
            {
                List<GoalInformation> goalInformations = await _goalBL.ClaimGoals(UserID);
                List<GoalOutput> goalOutputs = new List<GoalOutput>();
                foreach (GoalInformation goalInformation in goalInformations) goalOutputs.Add(DTOModelTransformations.GoalInformationToGoalOutput(goalInformation));
                return goalOutputs;
            }
        }

        // GET api/<GoalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GoalController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(GoalInput goalInput)
        {
            string UserID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //check if user and category exists before adding goal   
            if (await _userBL.GetUser(UserID) == null)
            {
                UserTestsModels.User user = new UserTestsModels.User();
                user.Auth0Id = UserID;
                await _userBL.AddUser(user);
            }
            if (await _categoryBL.GetCategoryById(goalInput.CategoryId) == null)
            {
                Category category = new Category();
                category.Id = goalInput.CategoryId;
                await _categoryBL.AddCategory(category);
            }
            if (goalInput.GoalDate <= DateTime.Now) return BadRequest("Goal date has already passed");
            Goal goal = DTOModelTransformations.GoalInputToGoal(goalInput, UserID);
            if (await _goalBL.AddGoal(goal) == null) return BadRequest("Can't set goal, make sure WPM makes sense");
            else return Ok();
        }

        // PUT api/<GoalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GoalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
