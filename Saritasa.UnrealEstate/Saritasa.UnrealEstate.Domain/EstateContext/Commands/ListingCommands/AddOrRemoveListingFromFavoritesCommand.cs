namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands
{
    /// <summary>
    /// Add or remove listing from favorites command.
    /// </summary>
    public class AddOrRemoveListingFromFavoritesCommand
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }

        /// <summary>
        /// Is this favorite?.
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}
