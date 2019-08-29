using Saritasa.Tools.Messages.Abstractions.Queries;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Queries
{
    /// <summary>
    /// Class that store queries for listing repository.
    /// </summary>
    [QueryHandlers]
    public class ListingsQueries
    {
        private readonly IListingRepository listingsRepository;

        /// <summary>
        /// Constructor of the <seealso cref="ListingsQueries"/> class.
        /// </summary>
        /// <param name="listingsRepository"></param>
        public ListingsQueries(IListingRepository listingsRepository)
        {
            this.listingsRepository = listingsRepository ?? throw new ArgumentNullException(nameof(listingsRepository));
        }

        /// <summary>
        /// Get listings query.
        /// </summary>
        /// <returns>List of listings.</returns>
        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            return await listingsRepository.GetListingsAsync();
        }

        /// <summary>
        /// Get listing by Id query.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <returns>Listing.</returns>
        public async Task<Listing> GetListingByIdAsync(long listingId)
        {
            return await listingsRepository.GetListingByIdAsync(listingId);
        }

        /// <summary>
        /// Get listing comments query.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <returns>List of comments.</returns>
        public async Task<IEnumerable<Comment>> GetListingCommentsAsync(long listingId)
        {
            return await listingsRepository.GetListingCommentsAsync(listingId);
        }
    }
}
