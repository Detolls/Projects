using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Repositories
{
    /// <summary>
    /// User repository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get users from the repository async.
        /// </summary>
        /// <returns>List of users.</returns>
        Task<IEnumerable<User>> GetUsersAsync(UserFilter userFilter = null);

        /// <summary>
        /// Get user by Id from the repository async.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>User entity.</returns>
        Task<User> GetUserByIdAsync(string userId);

        /// <summary>
        /// Add user to the repository async.
        /// </summary>
        /// <param name="user">User entity.</param>
        Task AddUserAsync(User user);
    }
}
