using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models
{
    public class OrderDto
    {
        public int orderId {get; set;}

        [Required]
        public DateTime OrderDate { get; set; }

        [MaxLength(100)]
        public string OrederName { get; set;  }

        public ICollection<OrderItemDto> items { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        [Required]
        public int itemId { get; set;}
        public string? itemName { get; set; }

        [Required]
        public decimal price { get; set; }
        
        public int quantity { get; set; }
    }

}
