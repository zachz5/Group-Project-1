using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
    
    public class CartItem
    {
        public int Id { get; set; }
        
        // Foreign keys
        public int CartId { get; set; }
        public int RecipeId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;
        
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Cart Cart { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
