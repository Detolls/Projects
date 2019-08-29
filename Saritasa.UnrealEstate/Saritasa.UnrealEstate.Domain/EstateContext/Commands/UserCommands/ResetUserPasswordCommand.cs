using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands
{
    /// <summary>
    /// Reset user password command.
    /// </summary>
    public class ResetUserPasswordCommand
    {
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
        /// Reset token.
        /// </summary>
        [IgnoreDataMember]
        public string ResetToken { get; set; }
    }
}
