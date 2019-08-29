using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// Listing entity.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

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
        [MaxLength(10)]
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
        [Column(TypeName = "money")]
        public decimal StartingPrice { get; set; }

        /// <summary>
        /// Listing due date.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Listing description.
        /// </summary>
        [Column(TypeName = "text")]
        public string Description { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Listing photos.
        /// </summary>
        public virtual ICollection<ListingPhoto> Photos { get; set; }
    }
}
