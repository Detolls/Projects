using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Dtos
{
    /// <summary>
    /// Listing DTO.
    /// </summary>
    public class ListingDto
    {
        /// <summary>
        /// Listing city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Listing state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Listing zip code.
        /// </summary>
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$")]
        public string Zip { get; set; }

        /// <summary>
        /// Listing address line 1.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Listing address line 2.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Listing status.
        /// </summary>
        public ListingStatus Status { get; set; }

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
        public int BuiltYear { get; set; }

        /// <summary>
        /// Listing starting price.
        /// </summary>
        public decimal StartingPrice { get; set; }

        /// <summary>
        /// Listing due date.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Listing description.
        /// </summary>
        public string Description { get; set; }
    }
}
