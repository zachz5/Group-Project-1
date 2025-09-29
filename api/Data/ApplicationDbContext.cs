using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanRecipe> MealPlanRecipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between MealPlan and Recipe
            modelBuilder.Entity<MealPlanRecipe>()
                .HasKey(mr => new { mr.MealPlanId, mr.RecipeId });

            modelBuilder.Entity<MealPlanRecipe>()
                .HasOne(mr => mr.MealPlan)
                .WithMany(mp => mp.MealPlanRecipes)
                .HasForeignKey(mr => mr.MealPlanId);

            modelBuilder.Entity<MealPlanRecipe>()
                .HasOne(mr => mr.Recipe)
                .WithMany()
                .HasForeignKey(mr => mr.RecipeId);

            // Configure User relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Cart relationships
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Recipe)
                .WithMany()
                .HasForeignKey(ci => ci.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order relationships
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Recipe)
                .WithMany()
                .HasForeignKey(oi => oi.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "No-Cook", Icon = "bi-snow", Color = "info" },
                new Category { Id = 2, Name = "Microwave", Icon = "bi-lightning-charge", Color = "warning" },
                new Category { Id = 3, Name = "Stovetop", Icon = "bi-fire", Color = "danger" }
            );

            // Seed data for recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Title = "Overnight Oats",
                    Description = "Creamy overnight oats with peanut butter and fruit - perfect for busy mornings.",
                    PrepTime = "5 min prep + overnight chill",
                    Difficulty = "Easy",
                    Price = "$1.00",
                    Image = "./client/images/OvernightOats.jpg",
                    CategoryId = 1
                },
                new Recipe
                {
                    Id = 2,
                    Title = "Tuna Salad Wraps",
                    Description = "Quick and protein-packed tuna wraps perfect for lunch.",
                    PrepTime = "5 min",
                    Difficulty = "Easy",
                    Price = "$1.50",
                    Image = "./client/images/TunaWrap.jpg",
                    CategoryId = 1
                },
                new Recipe
                {
                    Id = 3,
                    Title = "Hummus & Veggie Pita",
                    Description = "Fresh and healthy pita stuffed with hummus and crisp vegetables.",
                    PrepTime = "5 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/PitaWrap.jpg",
                    CategoryId = 1
                },
                new Recipe
                {
                    Id = 4,
                    Title = "Peanut Butter Banana Sandwich",
                    Description = "Classic comfort food with a sweet honey drizzle.",
                    PrepTime = "3 min",
                    Difficulty = "Easy",
                    Price = "$0.75",
                    Image = "./client/images/PenutButterandBanana.jpg",
                    CategoryId = 1
                },
                new Recipe
                {
                    Id = 5,
                    Title = "Greek Yogurt Parfait",
                    Description = "Layered parfait with Greek yogurt, granola, and fresh berries.",
                    PrepTime = "3 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/YogurtParfait.jpg",
                    CategoryId = 1
                },
                new Recipe
                {
                    Id = 6,
                    Title = "Microwave Mac & Cheese",
                    Description = "Creamy mac and cheese made entirely in the microwave.",
                    PrepTime = "8 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/MicrowaveMac.jpg",
                    CategoryId = 2
                },
                new Recipe
                {
                    Id = 7,
                    Title = "Microwave Baked Potato",
                    Description = "Perfectly cooked baked potato with your favorite toppings.",
                    PrepTime = "6 min",
                    Difficulty = "Easy",
                    Price = "$0.80",
                    Image = "./client/images/BakedPotato.jpg",
                    CategoryId = 2
                },
                new Recipe
                {
                    Id = 8,
                    Title = "Microwave Egg Scramble",
                    Description = "Fluffy scrambled eggs with cheese and veggies - microwave style.",
                    PrepTime = "4 min",
                    Difficulty = "Easy",
                    Price = "$1.00",
                    Image = "./client/images/ScrambledEggs.jpg",
                    CategoryId = 2
                },
                new Recipe
                {
                    Id = 9,
                    Title = "Microwave Quesadilla",
                    Description = "Cheesy quesadilla made quickly in the microwave.",
                    PrepTime = "4 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/Quesadilla.jpg",
                    CategoryId = 2
                },
                new Recipe
                {
                    Id = 10,
                    Title = "Microwave Mug Pizza",
                    Description = "Personal pizza made entirely in a mug - perfect for one!",
                    PrepTime = "7 min",
                    Difficulty = "Medium",
                    Price = "$1.50",
                    Image = "./client/images/MugPizza.jpg",
                    CategoryId = 2
                },
                new Recipe
                {
                    Id = 11,
                    Title = "One-Pot Ramen Upgrade",
                    Description = "Transform basic ramen into a gourmet meal with egg and veggies.",
                    PrepTime = "7 min",
                    Difficulty = "Easy",
                    Price = "$1.00",
                    Image = "./client/images/OnePotRamen.jpg",
                    CategoryId = 3
                },
                new Recipe
                {
                    Id = 12,
                    Title = "Grilled Cheese & Tomato Soup",
                    Description = "Classic comfort food combo - crispy grilled cheese with warm tomato soup.",
                    PrepTime = "10 min",
                    Difficulty = "Easy",
                    Price = "$1.75",
                    Image = "./client/images/GrilledCheese.jpg",
                    CategoryId = 3
                },
                new Recipe
                {
                    Id = 13,
                    Title = "Fried Rice with Egg",
                    Description = "Quick and satisfying fried rice using leftover rice and frozen veggies.",
                    PrepTime = "12 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/FriedRiceEgg.jpg",
                    CategoryId = 3
                },
                new Recipe
                {
                    Id = 14,
                    Title = "Simple Pasta with Garlic & Oil",
                    Description = "Elegant pasta dish with garlic, olive oil, and red pepper flakes.",
                    PrepTime = "12 min",
                    Difficulty = "Easy",
                    Price = "$1.25",
                    Image = "./client/images/Pasta.jpg",
                    CategoryId = 3
                },
                new Recipe
                {
                    Id = 15,
                    Title = "Veggie Stir-Fry with Rice",
                    Description = "Colorful vegetable stir-fry served over rice - healthy and delicious.",
                    PrepTime = "15 min",
                    Difficulty = "Easy",
                    Price = "$1.50",
                    Image = "./client/images/StirFry.jpg",
                    CategoryId = 3
                }
            );

            // Seed ingredients for recipes
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "½ cup rolled oats", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "½ cup milk (or yogurt)", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "1 tbsp peanut butter", RecipeId = 1 },
                new Ingredient { Id = 4, Name = "fruit", RecipeId = 1 },
                new Ingredient { Id = 5, Name = "chia seeds (optional)", RecipeId = 1 },
                
                new Ingredient { Id = 6, Name = "1 can tuna", RecipeId = 2 },
                new Ingredient { Id = 7, Name = "1 tbsp mayo or Greek yogurt", RecipeId = 2 },
                new Ingredient { Id = 8, Name = "tortilla", RecipeId = 2 },
                new Ingredient { Id = 9, Name = "lettuce", RecipeId = 2 },
                
                new Ingredient { Id = 10, Name = "½ cup pasta", RecipeId = 6 },
                new Ingredient { Id = 11, Name = "½ cup water", RecipeId = 6 },
                new Ingredient { Id = 12, Name = "¼ cup shredded cheese", RecipeId = 6 },
                new Ingredient { Id = 13, Name = "2 tbsp milk", RecipeId = 6 },

                // Recipe 3 - Hummus & Veggie Pita
                new Ingredient { Id = 14, Name = "1 pita", RecipeId = 3 },
                new Ingredient { Id = 15, Name = "3 tbsp hummus", RecipeId = 3 },
                new Ingredient { Id = 16, Name = "sliced cucumber", RecipeId = 3 },
                new Ingredient { Id = 17, Name = "shredded carrot", RecipeId = 3 },

                // Recipe 4 - Peanut Butter Banana Sandwich
                new Ingredient { Id = 18, Name = "2 slices bread", RecipeId = 4 },
                new Ingredient { Id = 19, Name = "1 tbsp peanut butter", RecipeId = 4 },
                new Ingredient { Id = 20, Name = "banana slices", RecipeId = 4 },
                new Ingredient { Id = 21, Name = "honey (optional)", RecipeId = 4 },

                // Recipe 5 - Greek Yogurt Parfait
                new Ingredient { Id = 22, Name = "1 cup Greek yogurt", RecipeId = 5 },
                new Ingredient { Id = 23, Name = "1/3 cup granola", RecipeId = 5 },
                new Ingredient { Id = 24, Name = "1/2 cup berries", RecipeId = 5 },

                // Recipe 7 - Microwave Baked Potato
                new Ingredient { Id = 25, Name = "1 large potato", RecipeId = 7 },
                new Ingredient { Id = 26, Name = "1 tbsp butter", RecipeId = 7 },
                new Ingredient { Id = 27, Name = "2 tbsp shredded cheddar", RecipeId = 7 },

                // Recipe 8 - Microwave Egg Scramble
                new Ingredient { Id = 28, Name = "2 eggs", RecipeId = 8 },
                new Ingredient { Id = 29, Name = "2 tbsp milk", RecipeId = 8 },
                new Ingredient { Id = 30, Name = "2 tbsp shredded cheese", RecipeId = 8 },

                // Recipe 9 - Microwave Quesadilla
                new Ingredient { Id = 31, Name = "2 tortillas", RecipeId = 9 },
                new Ingredient { Id = 32, Name = "1/2 cup shredded cheese", RecipeId = 9 },

                // Recipe 10 - Microwave Mug Pizza
                new Ingredient { Id = 33, Name = "4 tbsp flour", RecipeId = 10 },
                new Ingredient { Id = 34, Name = "2 tbsp milk", RecipeId = 10 },
                new Ingredient { Id = 35, Name = "1 tbsp oil", RecipeId = 10 },
                new Ingredient { Id = 36, Name = "2 tbsp pizza sauce", RecipeId = 10 },
                new Ingredient { Id = 37, Name = "2 tbsp mozzarella", RecipeId = 10 },

                // Recipe 11 - One-Pot Ramen Upgrade
                new Ingredient { Id = 38, Name = "1 pack ramen", RecipeId = 11 },
                new Ingredient { Id = 39, Name = "1 egg", RecipeId = 11 },
                new Ingredient { Id = 40, Name = "1/4 cup frozen veggies", RecipeId = 11 },

                // Recipe 12 - Grilled Cheese & Tomato Soup
                new Ingredient { Id = 41, Name = "2 slices bread", RecipeId = 12 },
                new Ingredient { Id = 42, Name = "2 slices cheese", RecipeId = 12 },
                new Ingredient { Id = 43, Name = "1 can tomato soup", RecipeId = 12 },

                // Recipe 13 - Fried Rice with Egg
                new Ingredient { Id = 44, Name = "1 cup cooked rice", RecipeId = 13 },
                new Ingredient { Id = 45, Name = "1 egg", RecipeId = 13 },
                new Ingredient { Id = 46, Name = "1/2 cup mixed veggies", RecipeId = 13 },
                new Ingredient { Id = 47, Name = "1 tbsp soy sauce", RecipeId = 13 },

                // Recipe 14 - Simple Pasta with Garlic & Oil
                new Ingredient { Id = 48, Name = "1 cup pasta", RecipeId = 14 },
                new Ingredient { Id = 49, Name = "2 tbsp olive oil", RecipeId = 14 },
                new Ingredient { Id = 50, Name = "2 cloves garlic", RecipeId = 14 },

                // Recipe 15 - Veggie Stir-Fry with Rice
                new Ingredient { Id = 51, Name = "1 cup rice", RecipeId = 15 },
                new Ingredient { Id = 52, Name = "1 cup mixed vegetables", RecipeId = 15 },
                new Ingredient { Id = 53, Name = "1 tbsp soy sauce", RecipeId = 15 }
            );

            // Seed instructions for a few recipes
            modelBuilder.Entity<Instruction>().HasData(
                new Instruction { Id = 1, StepNumber = 1, InstructionText = "In a jar or container, mix oats and milk", RecipeId = 1 },
                new Instruction { Id = 2, StepNumber = 2, InstructionText = "Add peanut butter and stir well", RecipeId = 1 },
                new Instruction { Id = 3, StepNumber = 3, InstructionText = "Add fruit and chia seeds if desired", RecipeId = 1 },
                new Instruction { Id = 4, StepNumber = 4, InstructionText = "Cover and refrigerate overnight", RecipeId = 1 },
                
                new Instruction { Id = 5, StepNumber = 1, InstructionText = "Drain tuna and mix with mayo or Greek yogurt", RecipeId = 2 },
                new Instruction { Id = 6, StepNumber = 2, InstructionText = "Spoon tuna mixture into tortilla", RecipeId = 2 },
                new Instruction { Id = 7, StepNumber = 3, InstructionText = "Add lettuce leaves", RecipeId = 2 },
                new Instruction { Id = 8, StepNumber = 4, InstructionText = "Roll up tightly and serve", RecipeId = 2 },
                
                new Instruction { Id = 9, StepNumber = 1, InstructionText = "In a microwave-safe mug, add pasta and water", RecipeId = 6 },
                new Instruction { Id = 10, StepNumber = 2, InstructionText = "Microwave 4-5 minutes until pasta is soft, stirring halfway", RecipeId = 6 },
                new Instruction { Id = 11, StepNumber = 3, InstructionText = "Stir in cheese and milk until creamy", RecipeId = 6 },
                new Instruction { Id = 12, StepNumber = 4, InstructionText = "Microwave 30 seconds more if needed", RecipeId = 6 }
            );

            // Seed meal plans
            modelBuilder.Entity<MealPlan>().HasData(
                new MealPlan
                {
                    Id = 1,
                    Title = "Budget Week",
                    Description = "Affordable meals under $20 for the week",
                    Price = "$15-20",
                    Duration = "7 days"
                },
                new MealPlan
                {
                    Id = 2,
                    Title = "Protein Power",
                    Description = "High-protein meals for active students",
                    Price = "$25-30",
                    Duration = "7 days"
                },
                new MealPlan
                {
                    Id = 3,
                    Title = "Quick & Easy",
                    Description = "All meals ready in under 10 minutes",
                    Price = "$20-25",
                    Duration = "5 days"
                }
            );

            // Seed meal plan recipes
            modelBuilder.Entity<MealPlanRecipe>().HasData(
                new MealPlanRecipe { MealPlanId = 1, RecipeId = 6 },
                new MealPlanRecipe { MealPlanId = 1, RecipeId = 9 },
                new MealPlanRecipe { MealPlanId = 1, RecipeId = 11 },
                
                new MealPlanRecipe { MealPlanId = 2, RecipeId = 8 },
                new MealPlanRecipe { MealPlanId = 2, RecipeId = 9 },
                new MealPlanRecipe { MealPlanId = 2, RecipeId = 11 },
                
                new MealPlanRecipe { MealPlanId = 3, RecipeId = 6 },
                new MealPlanRecipe { MealPlanId = 3, RecipeId = 8 },
                new MealPlanRecipe { MealPlanId = 3, RecipeId = 11 }
            );
        }
    }
}
