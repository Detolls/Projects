using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User first name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// User birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// User gender.
        /// </summary>
        public char Gender { get; set; }
    }
}
