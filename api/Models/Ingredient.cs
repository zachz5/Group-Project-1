using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        // Foreign key
        public int RecipeId { get; set; }
        
        // Navigation property
        public Recipe Recipe { get; set; } = null!;
    }
}

