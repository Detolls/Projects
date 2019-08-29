using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Web.Controllers
{
    /// <summary>
    /// Listings api controller.
    /// </summary>
    [ApiController]
    [Route("api/listings")]
    [Produces("application/json")]
    public class ListingsController : ControllerBase
    {
        private readonly IMessagePipelineService pipelineService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor of the <seealso cref="ListingsController"/> class.
        /// </summary>
        /// <param name="pipelineService">Message pipeline service.</param>
        /// <param name="userManager">User manager.</param>
        public ListingsController(IMessagePipelineService pipelineService, UserManager<User> userManager)
        {
            this.pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get listings.
        /// </summary>
        [HttpGet]
        [ActionName("Get")]
        [Authorize]
        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            return await pipelineService.Query<ListingsQueries>().With(q => q.GetListingsAsync());
        }

        /// <summary>
        /// Get listing by Id.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        [HttpGet("{listingId}")]
        [ActionName("Get")]
        [AllowAnonymous]
        public async Task<ActionResult<Listing>> GetListingByIdAsync(long listingId)
        {
            Listing listing = await pipelineService.Query<ListingsQueries>().With(q => q.GetListingByIdAsync(listingId));

            if (listing != null)
            {
                return Ok(listing);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get listing comments.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        [HttpGet("{listingId}/comments")]
        [ActionName("Get")]
        [AllowAnonymous]
        public async Task<IEnumerable<Comment>> GetListingCommentsAsync(long listingId)
        {
            return await pipelineService.Query<ListingsQueries>().With(q => q.GetListingCommentsAsync(listingId));
        }

        /// <summary>
        /// Create new listing.
        /// </summary>
        /// <param name="command">Create listing command.</param>
        [HttpPost]
        [ActionName("Post")]
        [Authorize]
        public async Task<ActionResult> AddListingAsync([FromBody]CreateListingCommand command)
        {
            if (ModelState.IsValid)
            {
                await pipelineService.HandleCommandAsync(command);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update listing.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="command">Update listing command.</param>
        [HttpPut("{listingId}")]
        [ActionName("Put")]
        [Authorize]
        public async Task<ActionResult> UpdateListingAsync(long listingId, [FromBody]UpdateListingCommand command)
        {
            if (ModelState.IsValid)
            {
                command.ListingId = listingId;
                await pipelineService.HandleCommandAsync(command);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete listing.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        [HttpDelete("{listingId}")]
        [ActionName("Delete")]
        [Authorize]
        public async Task DeleteListingAsync(long listingId)
        {
            var command = new DeleteListingCommand
            {
                ListingId = listingId
            };

            await pipelineService.HandleCommandAsync(command);
        }

        /// <summary>
        /// Enable listing.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        [HttpPost("{listingId}/enable")]
        [ActionName("Post")]
        [Authorize]
        public async Task EnableListingAsync(long listingId)
        {
            var command = new EnableListingCommand
            {
                ListingId = listingId
            };

            await pipelineService.HandleCommandAsync(command);
        }

        /// <summary>
        /// Make listing bid.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="command">Make listing bid command.</param>
        [HttpPost("{listingId}/bid")]
        [ActionName("Post")]
        [Authorize]
        public async Task MakeListingBidAsync(long listingId, [FromBody]MakeListingBidCommand command)
        {
            User user = await userManager.FindByEmailAsync(User.Identity.Name);

            command.UserId = user.Id;
            command.ListingId = listingId;

            await pipelineService.HandleCommandAsync(command);
        }

        /// <summary>
        /// Add or remove listing from favorites.
        /// </summary>
        /// <param name="listingId">Listing Id.</param>
        /// <param name="isFavorite">Is this favorite?.</param>
        [HttpPost("{listingId}/favorite")]
        [ActionName("Post")]
        [Authorize]
        public async Task AddOrRemoveListingFromFavoritesAsync(long listingId, bool isFavorite)
        {
            User user = await userManager.FindByEmailAsync(User.Identity.Name);

            var command = new AddOrRemoveListingFromFavoritesCommand
            {
                UserId = user.Id,
                ListingId = listingId,
                IsFavorite = isFavorite
            };

            await pipelineService.HandleCommandAsync(command);
        }
    }
}