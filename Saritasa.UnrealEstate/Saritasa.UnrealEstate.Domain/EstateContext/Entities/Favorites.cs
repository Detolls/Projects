using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// Favorites entity.
    /// </summary>
    public class Favorites
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }

        [ForeignKey("ListingId")]
        public Listing Listing { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
