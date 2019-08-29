namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands
{
    /// <summary>
    /// Delete listing command.
    /// </summary>
    public class DeleteListingCommand
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }
    }
}
