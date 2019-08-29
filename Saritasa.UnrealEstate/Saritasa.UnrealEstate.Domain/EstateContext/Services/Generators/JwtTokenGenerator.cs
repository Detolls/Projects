using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Saritasa.UnrealEstate.Domain.EstateContext.Abstract;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Exceptions;
using Saritasa.UnrealEstate.Web;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Generators
{
    /// <summary>
    /// Class that stores logic for genereate jwt tokens.
    /// </summary>
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor of the <seealso cref="JwtTokenGenerator"/> class.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        public JwtTokenGenerator(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Method to generate token async.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">User password.</param>
        public async Task<string> GenerateTokenAsync(string username, string password)
        {
            ClaimsIdentity identity = await GetIdentityAsync(username, password);

            if (identity != null)
            {
                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get user identity async.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">User password.</param>
        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = await userManager.FindByEmailAsync(username);

            if (user != null)
            {
                bool isCorrectPassword = await userManager.CheckPasswordAsync(user, password);

                if (isCorrectPassword)
                {
                    IList<string> roles = await userManager.GetRolesAsync(user);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, roles[0])
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                    return claimsIdentity;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
    }
}
