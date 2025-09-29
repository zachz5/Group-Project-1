using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        
        [Required]
        public int StepNumber { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string InstructionText { get; set; } = string.Empty;
        
        // Foreign key
        public int RecipeId { get; set; }
        
        // Navigation property
        public Recipe Recipe { get; set; } = null!;
    }
}

