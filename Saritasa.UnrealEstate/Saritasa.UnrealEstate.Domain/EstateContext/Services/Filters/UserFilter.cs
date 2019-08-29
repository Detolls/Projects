namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Filters
{
    /// <summary>
    /// User filter.
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Limit count of users.
        /// </summary>
        public int Limit { get; set; } = 0;

        /// <summary>
        /// Sort users.
        /// </summary>
        public string OrderBy { get; set; }
    }
}
