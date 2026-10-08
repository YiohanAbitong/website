using System.ComponentModel.DataAnnotations;

namespace website.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}