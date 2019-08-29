using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Repositories
{
    /// <summary>
    /// Listing repository interface.
    /// </summary>
    public interface IListingRepository
    {
        /// <summary>
        /// Get listings from the repository async.
        /// </summary>
        /// <returns>List of listing.</returns>
        Task<IEnumerable<Listing>> GetListingsAsync();

        /// <summary>
        /// Get listing by id from repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <returns>Listing entity.</returns>
        Task<Listing> GetListingByIdAsync(long listingId);

        /// <summary>
        /// Get listing comments from the repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <returns>List of comments.</returns>
        Task<IEnumerable<Comment>> GetListingCommentsAsync(long listingId);

        /// <summary>
        /// Add listing to the repostory async.
        /// </summary>
        /// <param name="listing">Listing entity.</param>
        Task AddListingAsync(Listing listing);

        /// <summary>
        /// Update listing in the repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="listing">Listing update info.</param>
        Task UpdateListingAsync(long listingId, ListingDto listingUpdateInfo);

        /// <summary>
        /// Delete listing from the repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        Task DeleteListingAsync(long listingId);

        /// <summary>
        /// Enable listing in the repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        Task EnableListingAsync(long listingId);

        /// <summary>
        /// Make listing bid to the repository async.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="bid">Bid entity.</param>
        Task MakeListingBidAsync(Bid bid);

        /// <summary>
        /// Add/Remove listing from favorites to/from repository async.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="isFavorite">Is this favorite?</param>
        Task AddOrRemoveListingFromFavoritesAsync(string userId, long listingId, bool isFavorite);
    }
}
