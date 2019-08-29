using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Saritasa.OMDbScrubber.DataAccess.Context;
using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using Saritasa.OMDbScrubber.Domain.OMDb.Repositories;
using Saritasa.OMDbScrubber.Domain.OMDb.Services;
using Saritasa.OMDbScrubber.Domain.OMDb.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Infrastructure.DI.Implementations.Sql
{
    /// <summary>
    /// Сlass that stores the logic for recording movies in the DB and getting movies from the DB.
    /// </summary>
    public class EFCoreMoviesService : IMoviesService
    {
        /// <summary>
        /// OMDb Scrubber DB context.
        /// </summary>
        private readonly OMDbScrubberContext context;

        /// <summary>
        /// App configuration.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constuctor of the <seealso cref="EFCoreMoviesService"/> class.
        /// </summary>
        /// <param name="context">OMDb Scrabber DB context.</param>
        public EFCoreMoviesService(OMDbScrubberContext context, IConfiguration configuration)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Method for getting movies from the DB async.
        /// </summary>
        /// <param name="moviesFilter">Movies filter.</param>
        /// <returns>Filtered movies.</returns>
        public async Task<IEnumerable<Movie>> GetMoviesAsync(MoviesFilter moviesFilter = null)
        {
            IQueryable<Movie> movies = context.Movies.AsQueryable();

            if (moviesFilter.RatingAbove != null)
            {
                movies = movies.Where(m => m.ImdbRating > moviesFilter.RatingAbove);
            }

            if (moviesFilter.RuntimeMinsAbove != null && moviesFilter.RuntimeMinsBelow != null)
            {
                movies = movies.Where(m => m.RuntimeMins > moviesFilter.RuntimeMinsAbove || m.RuntimeMins < moviesFilter.RuntimeMinsBelow);
            }
            else if (moviesFilter.RuntimeMinsAbove != null)
            {
                movies = movies.Where(m => m.RuntimeMins > moviesFilter.RuntimeMinsAbove);
            }
            else if (moviesFilter.RuntimeMinsBelow != null)
            {
                movies = movies.Where(m => m.RuntimeMins < moviesFilter.RuntimeMinsBelow);
            }


            if (moviesFilter.HasActor != null)
            {
                movies = movies.Where(m => context.MovieActors.Any(ma => ma.MovieId == m.Id
                    && context.Actors.FirstOrDefault(a => a.Id == ma.ActorId).Name == moviesFilter.HasActor) != false);
            }

            return await movies.ToListAsync();

        }

        /// <summary>
        /// Method for getting movie actors from the DB async.
        /// </summary>
        /// <param name="movie">Movie.</param>
        /// <returns>List of actors.</returns>
        public async Task<IEnumerable<Actor>> GetMovieActorsAsync(Movie movie)
        {
            if (movie is null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            return await context.Actors.Where(a => context.MovieActors.Any(ma => ma.MovieId == movie.Id && ma.ActorId == a.Id)).ToListAsync();
        }

        /// <summary>
        /// Method for recording movies from OMDb API in the DB async.
        /// </summary>
        /// <param name="movieIds">Movie Ids.</param>
        public async Task UploadAsync(string[] movieIds)
        {
            OMDbService omdbService = new OMDbService(configuration.GetValue<string>("OMDbApiKey"));

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                foreach (var movieId in movieIds)
                {
                    if (!context.Movies.Any(m => m.ImdbId == movieId))
                    {
                        Movie movie = await omdbService.GetMovieFromJsonAsync(movieId);

                        await context.Movies.AddAsync(movie);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        transaction.Rollback();
                    }

                    foreach (var actorName in await omdbService.GetMovieActorsNamesFromJsonAsync(movieId))
                    {
                        var actor = context.Actors.FirstOrDefault(a => a.Name == actorName);

                        if (actor == null)
                        {
                            actor = new Actor
                            {
                                Name = actorName.TrimStart(' ')
                            };

                            await context.Actors.AddAsync(actor);
                        }

                        var movieActor = new MovieActor
                        {
                            ActorId = actor.Id,
                            MovieId = context.Movies.Last().Id
                        };

                        await context.MovieActors.AddAsync(movieActor);
                    }

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
            }
        }
    }
}
