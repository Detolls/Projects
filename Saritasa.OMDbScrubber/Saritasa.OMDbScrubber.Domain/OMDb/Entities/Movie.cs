using Saritasa.OMDbScrubber.Domain.OMDb.Entities.Base;
using System;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Entities
{
    /// <summary>
    /// Movie entity.
    /// </summary>
    public class Movie : BaseEntity
    {
        /// <summary>
        /// IMDb Id movie.
        /// </summary>
        public string ImdbId { get; set; }

        /// <summary>
        /// Title movie.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Genre movie.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Release date movie.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Runtime mins movie.
        /// </summary>
        public int RuntimeMins { get; set; }

        /// <summary>
        /// IMDb rating movie.
        /// </summary>
        public decimal ImdbRating { get; set; }

        /// <summary>
        /// Date of record of the film in the DB.
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}
