using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands
{
    /// <summary>
    /// Create listing command.
    /// </summary>
    public class CreateListingCommand
    {
        /// <summary>
        /// Listing city.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Listing state.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string State { get; set; }

        /// <summary>
        /// Listing zip code.
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$")]
        public string Zip { get; set; }

        /// <summary>
        /// Listing address line 1.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Listing address line 2.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Listing status.
        /// </summary>
        public virtual ListingStatus Status { get; set; }

        /// <summary>
        /// Count of listing house beds.
        /// </summary>
        public short Beds { get; set; }

        /// <summary>
        /// Size of listing house.
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// Built year of listing house.
        /// </summary>
        [Required]
        public int BuiltYear { get; set; }

        /// <summary>
        /// Listing starting price.
        /// </summary>
        public decimal StartingPrice { get; set; }

        /// <summary>
        /// Listing due date.
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Listing description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Output of created listing.
        /// </summary>
        [CommandOut]
        [IgnoreDataMember]
        public Listing NewListing { get; set; }
    }
}
