using System.ComponentModel.DataAnnotations;

namespace MarketDeskApp.Model.Entites
{
    /// <summary>
    /// Order item.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Name property.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Price property.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity property.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quantity can't be negative.")]
        public int Quantity { get; set; }
    }
}
