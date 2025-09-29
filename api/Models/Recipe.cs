using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string PrepTime { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Difficulty { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Price { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Image { get; set; } = string.Empty;
        
        // Foreign key
        public int CategoryId { get; set; }
        
        // Navigation properties
        public Category Category { get; set; } = null!;
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
    }
}

