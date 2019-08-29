using MarketDeskApp.Model.Entites.Base.Interfaces;

namespace MarketDeskApp.Model.Entites.Base
{
    /// <summary>
    /// Base enity.
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Id for entity.
        /// </summary>
        public int Id { get; set; }
    }
}
