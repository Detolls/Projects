using Saritasa.Tools.Domain.Exceptions;
using System;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Exceptions
{
    /// <summary>
    /// User not found exception.
    /// </summary>
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not found.")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
