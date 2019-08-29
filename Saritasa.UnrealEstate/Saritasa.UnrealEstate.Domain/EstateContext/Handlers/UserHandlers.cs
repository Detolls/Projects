using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Handlers
{
    /// <summary>
    /// Class that stores user command handlers.
    /// </summary>
    [CommandHandlers]
    public class UserHandlers
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor of the <seealso cref="UserHandlers"/> class.
        /// </summary>
        /// <param name="userRepository">User repository.</param>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sign In manager.</param>
        public UserHandlers(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create user command handler async.
        /// </summary>
        /// <param name="userCommand">Create user command.</param>
        public async Task HandleCreateUserAsync(CreateUserCommand userCommand)
        {
            userCommand.NewUser = new User
            {
                UserName = userCommand.Email,
                Email = userCommand.Email,
                PhoneNumber = userCommand.PhoneNumer,
                FirstName = userCommand.FirstName,
                LastName = userCommand.LastName,
                Birthday = userCommand.Birthday,
                Gender = userCommand.Gender
            };

            IdentityResult result = await userManager.CreateAsync(userCommand.NewUser, userCommand.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(userCommand.NewUser, "Regular");
            }
        }

        public async Task HandleResetUserPasswordAsync(ResetUserPasswordCommand userCommand)
        {
            User user = await userManager.FindByEmailAsync(userCommand.Email);
            await userManager.ResetPasswordAsync(user, userCommand.ResetToken, userCommand.Password);
        }

        public async Task HandleUpadteCurrentUserAsync(UpdateCurrentUserCommand userCommand)
        {
            userCommand.CurrentUser = mapper.Map<User>(userCommand.UpdateUserInfo);

            await userManager.UpdateAsync(userCommand.CurrentUser);
        }
    }
}
