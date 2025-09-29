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

            // Seed ingredients for a few recipes
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
                
                // Hummus & Veggie Pita (Recipe ID 3)
                new Ingredient { Id = 14, Name = "1 whole wheat pita", RecipeId = 3 },
                new Ingredient { Id = 15, Name = "3 tbsp hummus", RecipeId = 3 },
                new Ingredient { Id = 16, Name = "¼ cucumber, sliced", RecipeId = 3 },
                new Ingredient { Id = 17, Name = "½ tomato, sliced", RecipeId = 3 },
                new Ingredient { Id = 18, Name = "¼ red bell pepper, sliced", RecipeId = 3 },
                new Ingredient { Id = 19, Name = "2 lettuce leaves", RecipeId = 3 },
                new Ingredient { Id = 20, Name = "Salt & pepper to taste", RecipeId = 3 },
                
                // Peanut Butter Banana Sandwich (Recipe ID 4)
                new Ingredient { Id = 21, Name = "2 slices bread", RecipeId = 4 },
                new Ingredient { Id = 22, Name = "2 tbsp peanut butter", RecipeId = 4 },
                new Ingredient { Id = 23, Name = "1 banana, sliced", RecipeId = 4 },
                new Ingredient { Id = 24, Name = "1 tsp honey (optional)", RecipeId = 4 },
                
                // Greek Yogurt Parfait (Recipe ID 5)
                new Ingredient { Id = 25, Name = "1 cup Greek yogurt", RecipeId = 5 },
                new Ingredient { Id = 26, Name = "¼ cup granola", RecipeId = 5 },
                new Ingredient { Id = 27, Name = "½ cup mixed berries", RecipeId = 5 },
                new Ingredient { Id = 28, Name = "1 tbsp honey", RecipeId = 5 },
                
                // Microwave Baked Potato (Recipe ID 7)
                new Ingredient { Id = 29, Name = "1 large potato", RecipeId = 7 },
                new Ingredient { Id = 30, Name = "1 tbsp butter", RecipeId = 7 },
                new Ingredient { Id = 31, Name = "Salt to taste", RecipeId = 7 },
                new Ingredient { Id = 32, Name = "Black pepper to taste", RecipeId = 7 },
                new Ingredient { Id = 33, Name = "2 tbsp shredded cheese (optional)", RecipeId = 7 },
                
                // Microwave Egg Scramble (Recipe ID 8)
                new Ingredient { Id = 34, Name = "2 eggs", RecipeId = 8 },
                new Ingredient { Id = 35, Name = "2 tbsp milk", RecipeId = 8 },
                new Ingredient { Id = 36, Name = "2 tbsp shredded cheese", RecipeId = 8 },
                new Ingredient { Id = 37, Name = "Salt to taste", RecipeId = 8 },
                new Ingredient { Id = 38, Name = "Black pepper to taste", RecipeId = 8 },
                new Ingredient { Id = 39, Name = "1 tsp butter", RecipeId = 8 },
                
                // Microwave Quesadilla (Recipe ID 9)
                new Ingredient { Id = 40, Name = "1 large tortilla", RecipeId = 9 },
                new Ingredient { Id = 41, Name = "½ cup shredded cheese", RecipeId = 9 },
                new Ingredient { Id = 42, Name = "2 tbsp cooked chicken (optional)", RecipeId = 9 },
                new Ingredient { Id = 43, Name = "2 tbsp salsa", RecipeId = 9 },
                
                // Microwave Mug Pizza (Recipe ID 10)
                new Ingredient { Id = 44, Name = "4 tbsp flour", RecipeId = 10 },
                new Ingredient { Id = 45, Name = "¼ tsp baking powder", RecipeId = 10 },
                new Ingredient { Id = 46, Name = "Pinch of salt", RecipeId = 10 },
                new Ingredient { Id = 47, Name = "3 tbsp milk", RecipeId = 10 },
                new Ingredient { Id = 48, Name = "1 tsp olive oil", RecipeId = 10 },
                new Ingredient { Id = 49, Name = "2 tbsp pizza sauce", RecipeId = 10 },
                new Ingredient { Id = 50, Name = "2 tbsp shredded mozzarella", RecipeId = 10 },
                new Ingredient { Id = 51, Name = "Toppings of choice", RecipeId = 10 },
                
                // One-Pot Ramen Upgrade (Recipe ID 11)
                new Ingredient { Id = 52, Name = "1 package ramen noodles", RecipeId = 11 },
                new Ingredient { Id = 53, Name = "2 cups water", RecipeId = 11 },
                new Ingredient { Id = 54, Name = "1 egg", RecipeId = 11 },
                new Ingredient { Id = 55, Name = "2 green onions, chopped", RecipeId = 11 },
                new Ingredient { Id = 56, Name = "1 tbsp soy sauce", RecipeId = 11 },
                new Ingredient { Id = 57, Name = "½ tsp sesame oil", RecipeId = 11 },
                
                // Grilled Cheese & Tomato Soup (Recipe ID 12)
                new Ingredient { Id = 58, Name = "2 slices bread", RecipeId = 12 },
                new Ingredient { Id = 59, Name = "2 slices cheese", RecipeId = 12 },
                new Ingredient { Id = 60, Name = "1 tbsp butter", RecipeId = 12 },
                new Ingredient { Id = 61, Name = "1 can tomato soup", RecipeId = 12 },
                new Ingredient { Id = 62, Name = "½ cup milk", RecipeId = 12 },
                
                // Fried Rice with Egg (Recipe ID 13)
                new Ingredient { Id = 63, Name = "1 cup cooked rice", RecipeId = 13 },
                new Ingredient { Id = 64, Name = "2 eggs", RecipeId = 13 },
                new Ingredient { Id = 65, Name = "½ cup frozen mixed vegetables", RecipeId = 13 },
                new Ingredient { Id = 66, Name = "2 tbsp soy sauce", RecipeId = 13 },
                new Ingredient { Id = 67, Name = "1 tbsp oil", RecipeId = 13 },
                new Ingredient { Id = 68, Name = "2 green onions, chopped", RecipeId = 13 },
                
                // Simple Pasta with Garlic & Oil (Recipe ID 14)
                new Ingredient { Id = 69, Name = "½ cup pasta", RecipeId = 14 },
                new Ingredient { Id = 70, Name = "3 tbsp olive oil", RecipeId = 14 },
                new Ingredient { Id = 71, Name = "3 garlic cloves, minced", RecipeId = 14 },
                new Ingredient { Id = 72, Name = "¼ tsp red pepper flakes", RecipeId = 14 },
                new Ingredient { Id = 73, Name = "Salt to taste", RecipeId = 14 },
                new Ingredient { Id = 74, Name = "1 tbsp fresh parsley", RecipeId = 14 },
                
                // Veggie Stir-Fry with Rice (Recipe ID 15)
                new Ingredient { Id = 75, Name = "1 cup mixed vegetables", RecipeId = 15 },
                new Ingredient { Id = 76, Name = "1 cup cooked rice", RecipeId = 15 },
                new Ingredient { Id = 77, Name = "2 tbsp soy sauce", RecipeId = 15 },
                new Ingredient { Id = 78, Name = "2 garlic cloves, minced", RecipeId = 15 },
                new Ingredient { Id = 79, Name = "1 tsp fresh ginger, grated", RecipeId = 15 },
                new Ingredient { Id = 80, Name = "1 tbsp oil", RecipeId = 15 }
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
                new Instruction { Id = 12, StepNumber = 4, InstructionText = "Microwave 30 seconds more if needed", RecipeId = 6 },
                
                // Hummus & Veggie Pita (Recipe ID 3)
                new Instruction { Id = 13, StepNumber = 1, InstructionText = "Cut cucumber, tomato, and bell pepper into thin slices", RecipeId = 3 },
                new Instruction { Id = 14, StepNumber = 2, InstructionText = "Warm pita bread in microwave for 15 seconds", RecipeId = 3 },
                new Instruction { Id = 15, StepNumber = 3, InstructionText = "Spread hummus evenly inside the pita", RecipeId = 3 },
                new Instruction { Id = 16, StepNumber = 4, InstructionText = "Add lettuce leaves, then layer in vegetables", RecipeId = 3 },
                new Instruction { Id = 17, StepNumber = 5, InstructionText = "Season with salt and pepper to taste", RecipeId = 3 },
                
                // Peanut Butter Banana Sandwich (Recipe ID 4)
                new Instruction { Id = 18, StepNumber = 1, InstructionText = "Toast bread slices if desired", RecipeId = 4 },
                new Instruction { Id = 19, StepNumber = 2, InstructionText = "Spread peanut butter on both slices of bread", RecipeId = 4 },
                new Instruction { Id = 20, StepNumber = 3, InstructionText = "Slice banana into thin rounds", RecipeId = 4 },
                new Instruction { Id = 21, StepNumber = 4, InstructionText = "Arrange banana slices on one slice of bread", RecipeId = 4 },
                new Instruction { Id = 22, StepNumber = 5, InstructionText = "Drizzle honey over bananas if desired", RecipeId = 4 },
                new Instruction { Id = 23, StepNumber = 6, InstructionText = "Top with second slice of bread and serve", RecipeId = 4 },
                
                // Greek Yogurt Parfait (Recipe ID 5)
                new Instruction { Id = 24, StepNumber = 1, InstructionText = "Add half the Greek yogurt to a glass or bowl", RecipeId = 5 },
                new Instruction { Id = 25, StepNumber = 2, InstructionText = "Sprinkle half the granola over the yogurt", RecipeId = 5 },
                new Instruction { Id = 26, StepNumber = 3, InstructionText = "Add half the berries on top", RecipeId = 5 },
                new Instruction { Id = 27, StepNumber = 4, InstructionText = "Repeat layers with remaining ingredients", RecipeId = 5 },
                new Instruction { Id = 28, StepNumber = 5, InstructionText = "Drizzle honey over the top and serve", RecipeId = 5 },
                
                // Microwave Baked Potato (Recipe ID 7)
                new Instruction { Id = 29, StepNumber = 1, InstructionText = "Wash potato and pierce several times with a fork", RecipeId = 7 },
                new Instruction { Id = 30, StepNumber = 2, InstructionText = "Place potato on a microwave-safe plate", RecipeId = 7 },
                new Instruction { Id = 31, StepNumber = 3, InstructionText = "Microwave on high for 4-6 minutes, flipping halfway", RecipeId = 7 },
                new Instruction { Id = 32, StepNumber = 4, InstructionText = "Let cool for 1 minute, then cut open lengthwise", RecipeId = 7 },
                new Instruction { Id = 33, StepNumber = 5, InstructionText = "Add butter, salt, pepper, and cheese if desired", RecipeId = 7 },
                
                // Microwave Egg Scramble (Recipe ID 8)
                new Instruction { Id = 34, StepNumber = 1, InstructionText = "Beat eggs with milk, salt, and pepper in a microwave-safe bowl", RecipeId = 8 },
                new Instruction { Id = 35, StepNumber = 2, InstructionText = "Add butter to the bowl", RecipeId = 8 },
                new Instruction { Id = 36, StepNumber = 3, InstructionText = "Microwave for 30 seconds, then stir", RecipeId = 8 },
                new Instruction { Id = 37, StepNumber = 4, InstructionText = "Continue microwaving in 30-second intervals, stirring each time", RecipeId = 8 },
                new Instruction { Id = 38, StepNumber = 5, InstructionText = "When eggs are almost set, stir in cheese and microwave 15 more seconds", RecipeId = 8 },
                
                // Microwave Quesadilla (Recipe ID 9)
                new Instruction { Id = 39, StepNumber = 1, InstructionText = "Place tortilla on a microwave-safe plate", RecipeId = 9 },
                new Instruction { Id = 40, StepNumber = 2, InstructionText = "Sprinkle half the cheese on one half of the tortilla", RecipeId = 9 },
                new Instruction { Id = 41, StepNumber = 3, InstructionText = "Add chicken if using, then remaining cheese", RecipeId = 9 },
                new Instruction { Id = 42, StepNumber = 4, InstructionText = "Fold tortilla in half", RecipeId = 9 },
                new Instruction { Id = 43, StepNumber = 5, InstructionText = "Microwave for 1-2 minutes until cheese melts", RecipeId = 9 },
                new Instruction { Id = 44, StepNumber = 6, InstructionText = "Let cool slightly, then serve with salsa", RecipeId = 9 },
                
                // Microwave Mug Pizza (Recipe ID 10)
                new Instruction { Id = 45, StepNumber = 1, InstructionText = "Mix flour, baking powder, and salt in a large mug", RecipeId = 10 },
                new Instruction { Id = 46, StepNumber = 2, InstructionText = "Add milk and olive oil, stir until smooth", RecipeId = 10 },
                new Instruction { Id = 47, StepNumber = 3, InstructionText = "Spread pizza sauce over the dough", RecipeId = 10 },
                new Instruction { Id = 48, StepNumber = 4, InstructionText = "Top with cheese and desired toppings", RecipeId = 10 },
                new Instruction { Id = 49, StepNumber = 5, InstructionText = "Microwave for 1-2 minutes until cheese melts", RecipeId = 10 },
                
                // One-Pot Ramen Upgrade (Recipe ID 11)
                new Instruction { Id = 50, StepNumber = 1, InstructionText = "Bring water to a boil in a small pot", RecipeId = 11 },
                new Instruction { Id = 51, StepNumber = 2, InstructionText = "Add ramen noodles and cook for 2 minutes", RecipeId = 11 },
                new Instruction { Id = 52, StepNumber = 3, InstructionText = "Crack egg into the pot and let it poach for 1 minute", RecipeId = 11 },
                new Instruction { Id = 53, StepNumber = 4, InstructionText = "Add soy sauce and sesame oil", RecipeId = 11 },
                new Instruction { Id = 54, StepNumber = 5, InstructionText = "Garnish with chopped green onions and serve", RecipeId = 11 },
                
                // Grilled Cheese & Tomato Soup (Recipe ID 12)
                new Instruction { Id = 55, StepNumber = 1, InstructionText = "Heat a pan over medium heat", RecipeId = 12 },
                new Instruction { Id = 56, StepNumber = 2, InstructionText = "Butter one side of each bread slice", RecipeId = 12 },
                new Instruction { Id = 57, StepNumber = 3, InstructionText = "Place cheese between bread slices, buttered sides out", RecipeId = 12 },
                new Instruction { Id = 58, StepNumber = 4, InstructionText = "Cook sandwich 2-3 minutes per side until golden", RecipeId = 12 },
                new Instruction { Id = 59, StepNumber = 5, InstructionText = "Meanwhile, heat tomato soup with milk in microwave or stovetop", RecipeId = 12 },
                new Instruction { Id = 60, StepNumber = 6, InstructionText = "Serve grilled cheese with warm soup", RecipeId = 12 },
                
                // Fried Rice with Egg (Recipe ID 13)
                new Instruction { Id = 61, StepNumber = 1, InstructionText = "Heat oil in a pan over medium-high heat", RecipeId = 13 },
                new Instruction { Id = 62, StepNumber = 2, InstructionText = "Scramble eggs in the pan, then remove and set aside", RecipeId = 13 },
                new Instruction { Id = 63, StepNumber = 3, InstructionText = "Add frozen vegetables to the pan and cook 2-3 minutes", RecipeId = 13 },
                new Instruction { Id = 64, StepNumber = 4, InstructionText = "Add cooked rice and stir-fry for 2 minutes", RecipeId = 13 },
                new Instruction { Id = 65, StepNumber = 5, InstructionText = "Return eggs to pan, add soy sauce and green onions", RecipeId = 13 },
                new Instruction { Id = 66, StepNumber = 6, InstructionText = "Stir everything together and serve hot", RecipeId = 13 },
                
                // Simple Pasta with Garlic & Oil (Recipe ID 14)
                new Instruction { Id = 67, StepNumber = 1, InstructionText = "Cook pasta according to package directions", RecipeId = 14 },
                new Instruction { Id = 68, StepNumber = 2, InstructionText = "Heat olive oil in a pan over medium heat", RecipeId = 14 },
                new Instruction { Id = 69, StepNumber = 3, InstructionText = "Add minced garlic and red pepper flakes, cook 1 minute", RecipeId = 14 },
                new Instruction { Id = 70, StepNumber = 4, InstructionText = "Add cooked pasta to the pan and toss", RecipeId = 14 },
                new Instruction { Id = 71, StepNumber = 5, InstructionText = "Season with salt and garnish with parsley", RecipeId = 14 },
                
                // Veggie Stir-Fry with Rice (Recipe ID 15)
                new Instruction { Id = 72, StepNumber = 1, InstructionText = "Heat oil in a large pan over high heat", RecipeId = 15 },
                new Instruction { Id = 73, StepNumber = 2, InstructionText = "Add minced garlic and ginger, cook 30 seconds", RecipeId = 15 },
                new Instruction { Id = 74, StepNumber = 3, InstructionText = "Add mixed vegetables and stir-fry for 3-4 minutes", RecipeId = 15 },
                new Instruction { Id = 75, StepNumber = 4, InstructionText = "Add cooked rice and soy sauce", RecipeId = 15 },
                new Instruction { Id = 76, StepNumber = 5, InstructionText = "Continue stir-frying for 2-3 minutes until heated through", RecipeId = 15 }
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
