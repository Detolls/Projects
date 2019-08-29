using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands
{
    /// <summary>
    /// Make listing bid command.
    /// </summary>
    public class MakeListingBidCommand
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        [IgnoreDataMember]
        public long ListingId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Bid price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Comment for bid.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Output of made bid.
        /// </summary>
        [CommandOut]
        [IgnoreDataMember]
        public Bid MadeBid { get; set; }
    }
}
