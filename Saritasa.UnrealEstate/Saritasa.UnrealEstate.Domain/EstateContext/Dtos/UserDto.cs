using System;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Dtos
{
    /// <summary>
    /// User DTO.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// User first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name.
        /// </summary>
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
        /// User password.
        /// </summary>
        public string Password { get; set; }

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
