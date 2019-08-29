using Saritasa.OMDbScrubber.Domain.OMDb.Entities.Base;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Entities
{
    /// <summary>
    /// Actor entity.
    /// </summary>
    public class Actor : BaseEntity
    {
        /// <summary>
        /// Actor name.
        /// </summary>
        public string Name { get; set; }
    }
}
