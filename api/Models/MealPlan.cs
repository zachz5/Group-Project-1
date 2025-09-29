using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Price { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string Duration { get; set; } = string.Empty;
        
        // Navigation property for recipes in this meal plan
        public ICollection<MealPlanRecipe> MealPlanRecipes { get; set; } = new List<MealPlanRecipe>();
    }
    
    // Junction table for many-to-many relationship
    public class MealPlanRecipe
    {
        public int MealPlanId { get; set; }
        public int RecipeId { get; set; }
        
        public MealPlan MealPlan { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}

