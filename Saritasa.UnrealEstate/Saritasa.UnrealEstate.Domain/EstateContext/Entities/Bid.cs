using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// Bid entity.
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Bid Id.
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
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// Bid price.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Comment for bid.
        /// </summary>
        [Column(TypeName = "text")]
        public string Comment { get; set; }
    }
}
