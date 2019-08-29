using AutoMapper;
using Saritasa.UnrealEstate.Domain.EstateContext.Commands.ListingCommands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Mappings
{
    /// <summary>
    /// DTO mapping profile.
    /// </summary>
    public class DtoMappingProfile : Profile
    {
        /// <summary>
        /// Constructor of the <seealso cref="DtoMappingProfile"/> class.
        /// </summary>
        public DtoMappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UpdateListingCommand, ListingDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<ListingDto, Listing>();
        }
    }
}
