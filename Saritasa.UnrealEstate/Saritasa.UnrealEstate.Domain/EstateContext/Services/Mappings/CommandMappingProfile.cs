using AutoMapper;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.CommentCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.UserCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Mappings
{
    /// <summary>
    /// Command mapping profile.
    /// </summary>
    public class CommandMappingProfile : Profile
    {
        /// <summary>
        /// Constructor of the <seealso cref="CommandMappingProfile"/> class.
        /// </summary>
        public CommandMappingProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<CreateListingCommand, Listing>();
            CreateMap<CreateCommentCommand, Comment>();
            CreateMap<MakeListingBidCommand, Bid>();
        }
    }
}
