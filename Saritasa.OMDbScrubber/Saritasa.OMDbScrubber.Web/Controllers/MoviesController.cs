using Microsoft.AspNetCore.Mvc;
using Saritasa.OMDbScrubber.Domain.OMDb.Commands;
using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using Saritasa.OMDbScrubber.Domain.OMDb.Queries;
using Saritasa.OMDbScrubber.Domain.OMDb.Services.Filters;
using Saritasa.OMDbScrubber.Web.Models;
using Saritasa.Tools.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Web.Controllers
{
    /// <summary>
    /// Movies controller.
    /// </summary>
    [Route("imdb")]
    public class MoviesController : Controller
    {
        private readonly IMessagePipelineService pipelineService;

        /// <summary>
        /// Consructor of the <seealso cref="MoviesController"/>.
        /// </summary>
        public MoviesController(IMessagePipelineService pipelineService)
        {
            this.pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
        }

        /// <summary>
        /// Displays a list of movies.
        /// </summary>
        /// <param name="ratingAbove">Rating above.</param>
        /// <param name="runtimeMinsAbove">Runtime mins above.</param>
        /// <param name="runtimeMinsBelow">Runtime mins below.</param>
        /// <param name="hasActor">Has actor.</param>
        [Route("list")]
        public async Task<IActionResult> List(int? ratingAbove, int? runtimeMinsAbove, int? runtimeMinsBelow, string hasActor)
        {
            MoviesFilter moviesFilter = new MoviesFilter
            {
                RatingAbove = ratingAbove,
                RuntimeMinsAbove = runtimeMinsAbove,
                RuntimeMinsBelow = runtimeMinsBelow,
                HasActor = hasActor?.TrimStart().TrimEnd()
            };

            ViewData["MoviesRatingAbove"] = ratingAbove;
            ViewData["MoviesRuntimeMinsAbove"] = runtimeMinsAbove;
            ViewData["MoviesRuntimeMinsBelow"] = runtimeMinsBelow;
            ViewData["MoviesHasActor"] = hasActor;

            var movies = await pipelineService.Query<ImdbQueries>().With(q => q.SearchMoviesAsync(moviesFilter));

            var movieModels = await GetMovieModelsAsync(movies);

            return View(movieModels);
        }

        /// <summary>
        /// Display a form for uploading movies.
        /// </summary>
        [Route("upload")]
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Upload movies to the DB.
        /// </summary>
        /// <param name="moviesIdString">Movies Id.</param>
        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(string movieIdsString)
        {
            string[] movieIds = movieIdsString.Trim(' ').Split(',', ';');

            var command = new ImportImdbMoviesInfoCommand
            {
                ImdbMovieIds = movieIds
            };

            await pipelineService.HandleCommandAsync(command);

            return View();
        }

        /// <summary>
        /// Method to get movie view models async.
        /// </summary>
        /// <param name="movies">List of movies.</param>
        /// <returns>List of movie view models.</returns>
        private async Task<IEnumerable<MovieViewModel>> GetMovieModelsAsync(IEnumerable<Movie> movies)
        {
            var movieModels = new List<MovieViewModel>(movies.Count());

            foreach (var movie in movies)
            {
                var movieModel = new MovieViewModel
                {
                    Movie = movie
                };

                foreach (var actor in await pipelineService.Query<ImdbQueries>().With(q => q.GetMovieActorsAsync(movie)))
                {
                    movieModel.Actors.Add(actor);
                }

                movieModels.Add(movieModel);
            }

            return movieModels;
        }
    }
}