using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Repositories
{
    /// <summary>
    /// Comment repository interface.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Add comment to the repository async.
        /// </summary>
        /// <param name="comment">Comment entity.</param>
        Task AddCommentAsync(Comment comment);

        /// <summary>
        /// Update comment in the repository async.
        /// </summary>
        /// <param name="commentId">Comment Id.</param>
        /// <param name="comment">Comment update info.</param>
        Task UpdateCommentAsync(long commentId, CommentDto commentUpdateInfo);

        /// <summary>
        /// Delete comment from the repository async.
        /// </summary>
        /// <param name="commentId">Comment Id.</param>
        Task DeleteCommentAsync(long commentId);
    }
}
