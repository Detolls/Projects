using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.CommentCommands
{
    /// <summary>
    /// Create comment command.
    /// </summary>
    public class CreateCommentCommand
    {
        /// <summary>
        /// Listing Id.
        /// </summary>
        public long ListingId { get; set; }

        /// <summary>
        /// Created by user Id.
        /// </summary>
        [IgnoreDataMember]
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// Text of comment.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Output of created comment.
        /// </summary>
        [CommandOut]
        [IgnoreDataMember]
        public Comment NewComment { get; set; }
    }
}
