using Microsoft.EntityFrameworkCore;
using Saritasa.OMDbScrubber.Domain.OMDb.Entities;

namespace Saritasa.OMDbScrubber.DataAccess.Context
{
    /// <summary>
    /// OMDb scrabber DB context.
    /// </summary>
    public class OMDbScrubberContext : DbContext
    {
        /// <summary>
        /// Constructor of the <seealso cref="OMDbScrubberContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public OMDbScrubberContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Actors table.
        /// </summary>
        public DbSet<Actor> Actors { get; set; }

        /// <summary>
        /// Movies table.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Movie actors table.
        /// </summary>
        public DbSet<MovieActor> MovieActors { get; set; }

        /// <summary>
        /// Method <seealso cref="OnModelCreating(ModelBuilder)"/> override.
        /// </summary>
        /// <param name="builder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieActor>()
                .HasKey(x => new { x.MovieId, x.ActorId });
        }
    }
}
