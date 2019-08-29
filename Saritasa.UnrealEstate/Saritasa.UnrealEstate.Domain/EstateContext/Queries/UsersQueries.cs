using Saritasa.Tools.Messages.Abstractions.Queries;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Queries
{
    /// <summary>
    /// Class that stores queries for user repository.
    /// </summary>
    [QueryHandlers]
    public class UsersQueries
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Constructor of the <seealso cref="UsersQueries"/> class.
        /// </summary>
        /// <param name="usersRepository"></param>
        public UsersQueries(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Get users query.
        /// </summary>
        /// <returns>List of users.</returns>
        public async Task<IEnumerable<User>> GetUsersAsync(UserFilter userFilter)
        {
            return await userRepository.GetUsersAsync();
        }

        /// <summary>
        /// Get user by Id query.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>User.</returns>
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }
    }
}
