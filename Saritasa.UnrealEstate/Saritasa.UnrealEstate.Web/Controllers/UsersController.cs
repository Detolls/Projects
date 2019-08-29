using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Queries;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Web.Controllers
{
    /// <summary>
    /// Users api controller.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMessagePipelineService pipelineService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constuctor of the <seealso cref="UsersController"/> class.
        /// </summary>
        /// <param name="pipelineService">Pipeline service.</param>
        /// <param name="userManager">User manager.</param>
        public UsersController(IMessagePipelineService pipelineService, UserManager<User> userManager)
        {
            this.pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="filter">User filter.</param>
        [HttpGet]
        [ActionName("Get")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersAsync([FromQuery]UserFilter filter)
        {
            return await pipelineService.Query<UsersQueries>().With(q => q.GetUsersAsync(filter));
        }

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <param name="userId">User Id.</param>
        [HttpGet("{userId}")]
        [ActionName("Get")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserByIdAsync(string userId)
        {
            User user = await pipelineService.Query<UsersQueries>().With(q => q.GetUserByIdAsync(userId));

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get current logged user.
        /// </summary>
        [HttpGet("me")]
        [ActionName("Get")]
        [Authorize]
        public async Task<User> GetCurrentUser()
        {
            return await userManager.FindByEmailAsync(User.Identity.Name);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="command">Create user command.</param>
        [HttpPost]
        [ActionName("Post")]
        [Authorize]
        public async Task<ActionResult> AddUserAsync([FromBody]CreateUserCommand command)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(command.Email);

                if (user == null)
                {
                    await pipelineService.HandleCommandAsync(command);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update current logged user.
        /// </summary>
        /// <param name="command">Update current user command.</param>
        [HttpPut("me")]
        [ActionName("Put")]
        [Authorize]
        public async Task<ActionResult> UpdateCurrentUserAsync([FromBody]UpdateCurrentUserCommand command)
        {
            if (ModelState.IsValid)
            {
                command.CurrentUser = await userManager.FindByEmailAsync(User.Identity.Name);
                await pipelineService.HandleCommandAsync(command);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}