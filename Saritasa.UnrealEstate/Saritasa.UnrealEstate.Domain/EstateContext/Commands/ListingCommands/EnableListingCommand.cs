namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands
{
    /// <summary>
    /// Enable listing command.
    /// </summary>
    public class EnableListingCommand
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }
    }
}
