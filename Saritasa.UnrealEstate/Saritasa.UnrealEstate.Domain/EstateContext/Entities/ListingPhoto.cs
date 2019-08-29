using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// Listing photo entity.
    /// </summary>
    public class ListingPhoto
    {
        /// <summary>
        /// Listing photo Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }

        [ForeignKey("ListingId")]
        public virtual Listing Listing { get; set; }

        /// <summary>
        /// Photo URL.
        /// </summary>
        [Required]
        public string PhotoUrl { get; set; }
    }
}
