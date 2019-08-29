using Microsoft.EntityFrameworkCore;
using Saritasa.UnrealEstate.DataAccess.DbContexts;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.DataAccess.Repositories
{
    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly EstateDbContext context;

        /// <summary>
        /// Constructor of the <seealso cref="UserRepository"/> class.
        /// </summary>
        public UserRepository(EstateDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IUserRepository.GetUsersAsync" />
        public async Task<IEnumerable<User>> GetUsersAsync(UserFilter userFilter)
        {
            IQueryable<User> users = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(userFilter.Email))
            {
                users = users.Where(u => u.Email == userFilter.Email);
            }

            if (!string.IsNullOrEmpty(userFilter.FirstName))
            {
                users = users.Where(u => u.FirstName == userFilter.FirstName);
            }

            if (!string.IsNullOrEmpty(userFilter.LastName))
            {
                users = users.Where(u => u.LastName == userFilter.LastName);
            }

            if (userFilter.OrderBy == "name")
            {
                users = users.OrderBy(u => u.FirstName + u.LastName);
            }
            else if (userFilter.OrderBy == "email")
            {
                users.OrderBy(u => u.Email);
            }

            if (userFilter.Limit != 0)
            {
                users = users.Take(userFilter.Limit);
            }

            return await users.ToListAsync();
        }

        /// <inheritdoc cref="IUserRepository.GetUserByIdAsync(string)" />
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await context.Users.FindAsync(userId);
        }

        /// <inheritdoc cref="IUserRepository.AddUserAsync(User)" />
        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
