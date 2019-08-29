using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.CommentCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Web.Controllers
{
    /// <summary>
    /// Comments api controller.
    /// </summary>
    [ApiController]
    [Route("api/comments")]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly IMessagePipelineService pipelineService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor of the <seealso cref="CommentsController"/> class.
        /// </summary>
        /// <param name="pipelineService">Message pipeline service.</param>
        public CommentsController(IMessagePipelineService pipelineService, UserManager<User> userManager)
        {
            this.pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Create new comment.
        /// </summary>
        /// <param name="command">Create comment command.</param>
        [HttpPost]
        [ActionName("Post")]
        [Authorize]
        public async Task AddCommentAsync([FromBody]CreateCommentCommand command)
        {
            command.CreatedByUserId = (await userManager.FindByEmailAsync(User.Identity.Name)).Id;
            await pipelineService.HandleCommandAsync(command);
        }

        /// <summary>
        /// Update comment.
        /// </summary>
        /// <param name="commentId">Comment Id.</param>
        /// <param name="command">Update comment command.</param>
        [HttpPut("{commentId}")]
        [ActionName("Put")]
        [Authorize]
        public async Task UpdateCommentAsync(long commentId, [FromBody]UpdateCommentCommand command)
        {
            command.CommentId = commentId;

            await pipelineService.HandleCommandAsync(command);
        }

        /// <summary>
        /// Delete comment.
        /// </summary>
        /// <param name="commentId">Comment Id.</param>
        [HttpDelete("{commentId}")]
        [ActionName("Delete")]
        [Authorize]
        public async Task DeleteCommentAsync(long commentId)
        {
            var command = new DeleteCommentCommand
            {
                CommentId = commentId
            };

            await pipelineService.HandleCommandAsync(command);
        }
    }
}