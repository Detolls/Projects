namespace Saritasa.OMDbScrubber.Domain.OMDb.Services.Filters
{
    /// <summary>
    /// Movies filter.
    /// </summary>
    public class MoviesFilter
    {
        /// <summary>
        /// Rating above.
        /// </summary>
        public decimal? RatingAbove { get; set; }

        /// <summary>
        /// Runtime mins above.
        /// </summary>
        public int? RuntimeMinsAbove { get; set; }

        /// <summary>
        /// Runtime mins below.
        /// </summary>
        public int? RuntimeMinsBelow { get; set; }

        /// <summary>
        /// Has actor.
        /// </summary>
        public string HasActor { get; set; }
    }
}
