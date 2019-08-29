using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Saritasa.UnrealEstate.DataAccess.DbContexts;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.DataAccess.Repositories
{
    /// <summary>
    /// Listing repository.
    /// </summary>
    public class ListingRepository : IListingRepository
    {
        private readonly EstateDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor of the <seealso cref="ListingRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ListingRepository(EstateDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc cref="IListingRepository.GetListingsAsync" />
        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            return await context.Listings.ToListAsync();
        }

        /// <inheritdoc cref="IListingRepository.GetListingByIdAsync(long)" />
        public async Task<Listing> GetListingByIdAsync(long listingId)
        {
            return await context.Listings.FindAsync(listingId);
        }

        /// <inheritdoc cref="IListingRepository.GetListingCommentsAsync(long)" />
        public async Task<IEnumerable<Comment>> GetListingCommentsAsync(long listingId)
        {
            return await context.Comments.Where(c => c.ListingId == listingId).ToListAsync();
        }

        /// <inheritdoc cref="IListingRepository.AddListingAsync(Listing)" />
        public async Task AddListingAsync(Listing listing)
        {
            await context.Listings.AddAsync(listing);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IListingRepository.UpdateListingAsync(long, ListingDto)" />
        public async Task UpdateListingAsync(long listingId, ListingDto listingUpdateInfo)
        {
            Listing listing = await context.Listings.FindAsync(listingId);

            if (listing != null)
            {
                listing = mapper.Map<Listing>(listingUpdateInfo);

                context.Listings.Update(listing);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc cref="IListingRepository.DeleteListingAsync(long)" />
        public async Task DeleteListingAsync(long listingId)
        {
            context.Remove(await context.Listings.FindAsync(listingId));
            await context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IListingRepository.EnableListingAsync(long)" />
        public async Task EnableListingAsync(long listingId)
        {
            Listing listing = await context.Listings.FindAsync(listingId);

            if (listing.Status == ListingStatus.NotAvailable)
            {
                listing.Status = ListingStatus.Active;

                context.Listings.Update(listing);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc cref="IListingRepository.MakeListingBidAsync(long, Bid)" />
        public async Task MakeListingBidAsync(Bid bid)
        {
            await context.Bids.AddAsync(bid);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IListingRepository.AddOrRemoveListingFromFavoritesAsync(string, long, bool)" />
        public async Task AddOrRemoveListingFromFavoritesAsync(string userId, long listingId, bool isFavorite)
        {
            if (isFavorite == true)
            {
                if (context.Favorites.Any(f => f.UserId == userId && f.ListingId == listingId) == false)
                {
                    await context.Favorites.AddAsync(new Favorites { UserId = userId, ListingId = listingId });
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                context.Favorites.Remove(await context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId));
                await context.SaveChangesAsync();
            }
        }
    }
}
