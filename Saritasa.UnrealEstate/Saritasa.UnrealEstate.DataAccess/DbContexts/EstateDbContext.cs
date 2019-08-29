using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Diagnostics;

namespace Saritasa.UnrealEstate.DataAccess.DbContexts
{
    /// <summary>
    /// Estate DB context.
    /// </summary>
    public class EstateDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Listings table.
        /// </summary>
        public DbSet<Listing> Listings { get; set; }

        /// <summary>
        /// Listing notes table.
        /// </summary>
        public DbSet<ListingNote> ListingNotes { get; set; }

        /// <summary>
        /// Listing photos table.
        /// </summary>
        public DbSet<ListingPhoto> ListingPhotos { get; set; }

        /// <summary>
        /// Bids table.
        /// </summary>
        public DbSet<Bid> Bids { get; set; }

        /// <summary>
        /// Comments table.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Favorites table.
        /// </summary>
        public DbSet<Favorites> Favorites { get; set; }

        /// <summary>
        /// Constructor of the <seealso cref="EstateDbContext"/> class.
        /// </summary>
        /// <param name="options">DB context options.</param>
        public EstateDbContext(DbContextOptions options) : base(options)
        {
        }

        public EstateDbContext()
        {
        }

        /// <summary>
        /// Method <seealso cref="OnModelCreating(ModelBuilder)"/> override.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorites>()
                .HasKey(x => new { x.ListingId, x.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
