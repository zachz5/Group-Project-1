using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled
        
        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed, Refunded
        
        public decimal TotalAmount { get; set; }
        
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;
        
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        
        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    
    public class OrderItem
    {
        public int Id { get; set; }
        
        // Foreign keys
        public int OrderId { get; set; }
        public int RecipeId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string RecipeTitle { get; set; } = string.Empty; // Store recipe title at time of order
        
        [Required]
        [MaxLength(20)]
        public string RecipePrice { get; set; } = string.Empty; // Store recipe price at time of order
        
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Navigation properties
        public Order Order { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
