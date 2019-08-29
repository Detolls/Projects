using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.UnrealEstate.Domain.EstateContext.Abstract;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Senders;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Web.Controllers
{
    /// <summary>
    /// Account api controller.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AccountController : ControllerBase
    {
        private readonly IMessagePipelineService pipelineService;
        private readonly ITokenGenerator tokenGenerator;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor of the <seealso cref="AccountController"/> class.
        /// </summary>
        /// <param name="pipelineService">Pipeline service.</param>
        /// <param name="tokenGenerator">Token generator.</param>
        /// <param name="userManager">User manager.</param>
        public AccountController(IMessagePipelineService pipelineService, ITokenGenerator tokenGenerator, UserManager<User> userManager)
        {
            this.pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            this.tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="loginInfo">Login info.</param>
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult> LoginAsync([FromBody]LoginDto loginInfo)
        {
            string userToken = await tokenGenerator.GenerateTokenAsync(loginInfo.Email, loginInfo.Password);


            if (userToken != null)
            {
                var response = new
                {
                    accessToken = $"Bearer {userToken}"
                };

                Response.ContentType = "application/json";
                await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Send forgot user password email.
        /// </summary>
        /// <param name="email">Email.</param>
        [HttpPost("forgot-password")]
        [ActionName("Post")]
        public async Task<ActionResult> SendForgotPasswordEmailAsync(string email)
        {
            User user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest();
            }

            string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            string callbackUrl = Url.Action("ResetPasswordAsync", "Account", new ResetUserPasswordCommand { ResetToken = resetToken }, protocol: HttpContext.Request.Scheme);

            await EmailSender.SendEmailAsync(email, "Reset password", callbackUrl);

            return Ok(resetToken);
        }

        /// <summary>
        /// Reset user password.
        /// </summary>
        /// <param name="command">Reset user password command.</param>
        [HttpPost("reset-password")]
        [ActionName("Post")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody]ResetUserPasswordCommand command)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(command.Email);

                if (user == null)
                {
                    return BadRequest();
                }

                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                command.ResetToken = resetToken;

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