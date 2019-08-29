using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands
{
    /// <summary>
    /// Update current user command.
    /// </summary>
    public class UpdateCurrentUserCommand
    {
        /// <summary>
        /// Current user Id.
        /// </summary>
        [IgnoreDataMember]
        public string CurrentUserId { get; set; }

        /// <summary>
        /// User first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// User phone number.
        /// </summary>
        public string PhoneNumer { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// User gender.
        /// </summary>
        public char Gender { get; set; }

        /// <summary>
        /// Input of update user info.
        /// </summary>
        [CommandOut]
        [IgnoreDataMember]
        public UserDto UpdateUserInfo { get; set; }

        [CommandOut]
        [IgnoreDataMember]
        public User CurrentUser { get; set; }
    }
}
