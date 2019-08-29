using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using Saritasa.OMDbScrubber.Domain.OMDb.Services.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Repositories
{
    /// <summary>
    /// Movies service interface.
    /// </summary>
    public interface IMoviesService
    {
        /// <summary>
        /// Get movies async.
        /// </summary>
        /// <returns>List of movies.</returns>
        Task<IEnumerable<Movie>> GetMoviesAsync(MoviesFilter moviesFilter = null);

        /// <summary>
        /// Get movie actors async.
        /// </summary>
        /// <param name="movie">Movie.</param>
        /// <returns>List of actors.</returns>
        Task<IEnumerable<Actor>> GetMovieActorsAsync(Movie movie);

        /// <summary>
        /// Async upload movies.
        /// </summary>
        /// <param name="movieIds">Movie Ids.</param>
        Task UploadAsync(string[] movieIds);
    }
}
