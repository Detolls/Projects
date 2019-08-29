using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Entities
{
    /// <summary>
    /// Listing note entity.
    /// </summary>
    public class ListingNote
    {
        /// <summary>
        /// Listing note Id.
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
        /// Created by user Id.
        /// </summary>
        public string CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// Text of listing note.
        /// </summary>
        [Column(TypeName = "text")]
        public string Text { get; set; }
    }
}
