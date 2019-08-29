using Saritasa.Tools.Messages.Abstractions.Commands;
using Saritasa.UnrealEstate.Domain.EstateContext.Dtos;
using System.Runtime.Serialization;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Commands.CommentCommands
{
    /// <summary>
    /// Update comment command.
    /// </summary>
    public class UpdateCommentCommand
    {
        /// <summary>
        /// Comment Id.
        /// </summary>
        [IgnoreDataMember]
        public long CommentId { get; set; }

        /// <summary>
        /// Text of comment.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Output update comment info.
        /// </summary>
        [CommandOut]
        [IgnoreDataMember]
        public CommentDto UpdateCommentInfo { get; set; }
    }
}
