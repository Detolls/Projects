using Saritasa.OMDbScrubber.Domain.OMDb.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Entities.Base
{
    /// <summary>
    /// Base entity.
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Id entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
