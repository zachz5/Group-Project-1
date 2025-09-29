using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Color { get; set; } = string.Empty;
        
        // Navigation property
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

