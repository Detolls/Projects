using AutoMapper;
using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Handlers
{
    /// <summary>
    /// Class that stores listing command handlers.
    /// </summary>
    [CommandHandlers]
    public class ListingHandlers
    {
        private readonly IListingRepository listingRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor of the <seealso cref="ListingHandlers"/> class.
        /// </summary>
        /// <param name="listingRepository">Listing repository.</param>
        public ListingHandlers(IListingRepository listingRepository, IMapper mapper)
        {
            this.listingRepository = listingRepository ?? throw new ArgumentNullException(nameof(listingRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create listing command handler async.
        /// </summary>
        /// <param name="listingCommand">Create listing command.</param>
        /// <param name="listingRepository">Listing repository.</param>
        public async Task HandleCreateListingAsync(CreateListingCommand listingCommand)
        {
            listingCommand.NewListing = mapper.Map<Listing>(listingCommand);

            await listingRepository.AddListingAsync(listingCommand.NewListing);
        }

        /// <summary>
        /// Update listing command handler async.
        /// </summary>
        /// <param name="listingCommand">Update listing command.</param>
        /// <param name="listingRepository">Listing repository.</param>
        public async Task HandleUpdateListingAsync(UpdateListingCommand listingCommand)
        {
            listingCommand.UpdateListingInfo = mapper.Map<ListingDto>(listingCommand);

            await listingRepository.UpdateListingAsync(listingCommand.ListingId, listingCommand.UpdateListingInfo);
        }

        /// <summary>
        /// Delete listing command handler async.
        /// </summary>
        /// <param name="listingCommand">Delete listing command.</param>
        /// <param name="listingRepository">Listing repository.</param>
        public async Task HandleDeleteListingAsync(DeleteListingCommand listingCommand)
        {
            await listingRepository.DeleteListingAsync(listingCommand.ListingId);
        }

        /// <summary>
        /// Enable listing command handler async.
        /// </summary>
        /// <param name="listingCommand">Enable listing command.</param>
        /// <param name="listingRepository">Listing repository.</param>
        public async Task HandleEnableListingAsync(EnableListingCommand listingCommand)
        {
            await listingRepository.EnableListingAsync(listingCommand.ListingId);
        }

        /// <summary>
        /// Make listing bid command handler async.
        /// </summary>
        /// <param name="listingCommand">Make listing bid command.</param>
        /// <param name="listingRepository">Listing repository.</param>
        public async Task HandleMakeListingBidAsync(MakeListingBidCommand listingBidCommand)
        {
            listingBidCommand.MadeBid = mapper.Map<Bid>(listingBidCommand);

            await listingRepository.MakeListingBidAsync(listingBidCommand.MadeBid);
        }

        /// <summary>
        /// Add or remove listing from favorites command handler async.
        /// </summary>
        /// <param name="listingCommand">Add or remove listing from favorites command.</param>
        public async Task HandleAddOrRemoveListingFromFavoritesCommandAsync(AddOrRemoveListingFromFavoritesCommand listingCommand)
        {
            await listingRepository.AddOrRemoveListingFromFavoritesAsync(listingCommand.UserId, listingCommand.ListingId, listingCommand.IsFavorite);
        }
    }
}
