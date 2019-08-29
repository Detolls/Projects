using System.ComponentModel.DataAnnotations.Schema;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Entities
{
    /// <summary>
    /// Movie actor entity.
    /// </summary>
    public class MovieActor
    {
        /// <summary>
        /// Actor Id.
        /// </summary>
        public long ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }

        /// <summary>
        /// Movie Id.
        /// </summary>
        public long MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
