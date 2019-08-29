using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands
{
    /// <summary>
    /// Create user command.
    /// </summary>
    public class CreateUserCommand
    {
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
        [Required]
        public string PhoneNumer { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Confirm user password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// User birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// User gender.
        /// </summary>
        public char Gender { get; set; }

        [IgnoreDataMember]
        public User NewUser { get; set; }
    }
}
