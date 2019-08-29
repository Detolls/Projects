using AutoMapper;
using Saritasa.UnrealEstate.DataAccess.DbContexts;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.DataAccess.Repositories
{
    /// <summary>
    /// Comment repository.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly EstateDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Constuctor of the <seealso cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public CommentRepository(EstateDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc cref="ICommentRepository.AddCommentAsync(Comment)" />
        public async Task AddCommentAsync(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc cref="ICommentRepository.UpdateCommentAsync(long, CommentDto)" />
        public async Task UpdateCommentAsync(long commentId, CommentDto commentUpdateInfo)
        {
            Comment comment = await context.Comments.FindAsync(commentId);

            if (comment != null)
            {
                comment = mapper.Map<Comment>(commentUpdateInfo);

                context.Comments.Update(comment);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc cref="ICommentRepository.DeleteCommentAsync(long)" />
        public async Task DeleteCommentAsync(long commentId)
        {
            context.Comments.Remove(await context.Comments.FindAsync(commentId));
            await context.SaveChangesAsync();
        }
    }
}
