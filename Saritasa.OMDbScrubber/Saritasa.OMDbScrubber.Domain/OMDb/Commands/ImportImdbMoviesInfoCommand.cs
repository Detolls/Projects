namespace Saritasa.OMDbScrubber.Domain.OMDb.Commands
{
    /// <summary>
    /// Command for import IMDb movies info.
    /// </summary>
    public class ImportImdbMoviesInfoCommand
    {
        /// <summary>
        /// IMDb movie ids.
        /// </summary>
        public string[] ImdbMovieIds { get; set; }
    }
}
