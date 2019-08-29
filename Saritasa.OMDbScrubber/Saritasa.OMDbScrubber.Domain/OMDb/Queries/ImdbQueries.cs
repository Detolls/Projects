using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using Saritasa.OMDbScrubber.Domain.OMDb.Repositories;
using Saritasa.OMDbScrubber.Domain.OMDb.Services.Filters;
using Saritasa.Tools.Messages.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Queries
{
    /// <summary>
    /// IMDb queries.
    /// </summary>
    [QueryHandlers]
    public class ImdbQueries
    {
        private readonly IMoviesService moviesService;

        /// <summary>
        /// Constuctor of the <seealso cref="ImdbQueries"/> class.
        /// </summary>
        public ImdbQueries(IMoviesService moviesService)
        {
            this.moviesService = moviesService ?? throw new ArgumentNullException(nameof(moviesService));
        }

        /// <summary>
        /// Query for searching movies by filter async.
        /// </summary>
        /// <param name="moviesFilter">Movies filter.</param>
        /// <returns>Filtered movies.</returns>
        public async Task<IEnumerable<Movie>> SearchMoviesAsync(MoviesFilter moviesFilter)
        {
            return await moviesService.GetMoviesAsync(moviesFilter);
        }

        /// <summary>
        /// Query for getting all movies async.
        /// </summary>
        /// <returns>All movies.</returns>
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await moviesService.GetMoviesAsync();
        }

        /// <summary>
        /// Query for getting movie actors async.
        /// </summary>
        /// <param name="movie">Movie.</param>
        /// <returns>Movie actors.</returns>
        public async Task<IEnumerable<Actor>> GetMovieActorsAsync(Movie movie)
        {
            return await moviesService.GetMovieActorsAsync(movie);
        }
    }
}
