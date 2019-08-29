using AutoMapper;
using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.CommentCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Handlers
{
    /// <summary>
    /// Class that stores comment command handlers.
    /// </summary>
    [CommandHandlers]
    public class CommentHandlers
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor of the <seealso cref="CommentHandlers"/> class.
        /// </summary>
        /// <param name="commentRepository">Comment repository.</param>
        public CommentHandlers(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create comment command handler async.
        /// </summary>
        /// <param name="commentCommand">Create comment command.</param>
        /// <param name="commentsRepository">Comment repository.</param>
        public async Task HandleCreateCommentAsync(CreateCommentCommand commentCommand)
        {
            commentCommand.NewComment = mapper.Map<Comment>(commentCommand);

            await commentRepository.AddCommentAsync(commentCommand.NewComment);
        }

        /// <summary>
        /// Update comment command handler async.
        /// </summary>
        /// <param name="commentCommand">Update comment command.</param>
        /// <param name="commentRepository">Comment repository.</param>
        public async Task HandleUpdateCommentAsync(UpdateCommentCommand commentCommand)
        {
            commentCommand.UpdateCommentInfo = new CommentDto
            {
                Text = commentCommand.Text
            };

            await commentRepository.UpdateCommentAsync(commentCommand.CommentId, commentCommand.UpdateCommentInfo);
        }

        /// <summary>
        /// Delete comment command handler async
        /// </summary>
        /// <param name="commentCommand">Delete comment command.</param>
        /// <param name="commentRepository">Comment repository.</param>
        public async Task HandleDeleteCommentAsync(DeleteCommentCommand commentCommand)
        {
            await commentRepository.DeleteCommentAsync(commentCommand.CommentId);
        }
    }
}
