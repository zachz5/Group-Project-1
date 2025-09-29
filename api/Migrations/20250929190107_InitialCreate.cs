using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Price = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Duration = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    PrepTime = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Price = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructionText = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanRecipes",
                columns: table => new
                {
                    MealPlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanRecipes", x => new { x.MealPlanId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_MealPlanRecipes_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RecipePrice = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "info", "bi-snow", "No-Cook" },
                    { 2, "warning", "bi-lightning-charge", "Microwave" },
                    { 3, "danger", "bi-fire", "Stovetop" }
                });

            migrationBuilder.InsertData(
                table: "MealPlans",
                columns: new[] { "Id", "Description", "Duration", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Affordable meals under $20 for the week", "7 days", "$15-20", "Budget Week" },
                    { 2, "High-protein meals for active students", "7 days", "$25-30", "Protein Power" },
                    { 3, "All meals ready in under 10 minutes", "5 days", "$20-25", "Quick & Easy" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "Description", "Difficulty", "Image", "PrepTime", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Creamy overnight oats with peanut butter and fruit - perfect for busy mornings.", "Easy", "./client/images/OvernightOats.jpg", "5 min prep + overnight chill", "$1.00", "Overnight Oats" },
                    { 2, 1, "Quick and protein-packed tuna wraps perfect for lunch.", "Easy", "./client/images/TunaWrap.jpg", "5 min", "$1.50", "Tuna Salad Wraps" },
                    { 3, 1, "Fresh and healthy pita stuffed with hummus and crisp vegetables.", "Easy", "./client/images/PitaWrap.jpg", "5 min", "$1.25", "Hummus & Veggie Pita" },
                    { 4, 1, "Classic comfort food with a sweet honey drizzle.", "Easy", "./client/images/PenutButterandBanana.jpg", "3 min", "$0.75", "Peanut Butter Banana Sandwich" },
                    { 5, 1, "Layered parfait with Greek yogurt, granola, and fresh berries.", "Easy", "./client/images/YogurtParfait.jpg", "3 min", "$1.25", "Greek Yogurt Parfait" },
                    { 6, 2, "Creamy mac and cheese made entirely in the microwave.", "Easy", "./client/images/MicrowaveMac.jpg", "8 min", "$1.25", "Microwave Mac & Cheese" },
                    { 7, 2, "Perfectly cooked baked potato with your favorite toppings.", "Easy", "./client/images/BakedPotato.jpg", "6 min", "$0.80", "Microwave Baked Potato" },
                    { 8, 2, "Fluffy scrambled eggs with cheese and veggies - microwave style.", "Easy", "./client/images/ScrambledEggs.jpg", "4 min", "$1.00", "Microwave Egg Scramble" },
                    { 9, 2, "Cheesy quesadilla made quickly in the microwave.", "Easy", "./client/images/Quesadilla.jpg", "4 min", "$1.25", "Microwave Quesadilla" },
                    { 10, 2, "Personal pizza made entirely in a mug - perfect for one!", "Medium", "./client/images/MugPizza.jpg", "7 min", "$1.50", "Microwave Mug Pizza" },
                    { 11, 3, "Transform basic ramen into a gourmet meal with egg and veggies.", "Easy", "./client/images/OnePotRamen.jpg", "7 min", "$1.00", "One-Pot Ramen Upgrade" },
                    { 12, 3, "Classic comfort food combo - crispy grilled cheese with warm tomato soup.", "Easy", "./client/images/GrilledCheese.jpg", "10 min", "$1.75", "Grilled Cheese & Tomato Soup" },
                    { 13, 3, "Quick and satisfying fried rice using leftover rice and frozen veggies.", "Easy", "./client/images/FriedRiceEgg.jpg", "12 min", "$1.25", "Fried Rice with Egg" },
                    { 14, 3, "Elegant pasta dish with garlic, olive oil, and red pepper flakes.", "Easy", "./client/images/Pasta.jpg", "12 min", "$1.25", "Simple Pasta with Garlic & Oil" },
                    { 15, 3, "Colorful vegetable stir-fry served over rice - healthy and delicious.", "Easy", "./client/images/StirFry.jpg", "15 min", "$1.50", "Veggie Stir-Fry with Rice" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "RecipeId" },
                values: new object[,]
                {
                    { 1, "½ cup rolled oats", 1 },
                    { 2, "½ cup milk (or yogurt)", 1 },
                    { 3, "1 tbsp peanut butter", 1 },
                    { 4, "fruit", 1 },
                    { 5, "chia seeds (optional)", 1 },
                    { 6, "1 can tuna", 2 },
                    { 7, "1 tbsp mayo or Greek yogurt", 2 },
                    { 8, "tortilla", 2 },
                    { 9, "lettuce", 2 },
                    { 10, "½ cup pasta", 6 },
                    { 11, "½ cup water", 6 },
                    { 12, "¼ cup shredded cheese", 6 },
                    { 13, "2 tbsp milk", 6 },
                    { 14, "1 whole wheat pita", 3 },
                    { 15, "3 tbsp hummus", 3 },
                    { 16, "¼ cucumber, sliced", 3 },
                    { 17, "½ tomato, sliced", 3 },
                    { 18, "¼ red bell pepper, sliced", 3 },
                    { 19, "2 lettuce leaves", 3 },
                    { 20, "Salt & pepper to taste", 3 },
                    { 21, "2 slices bread", 4 },
                    { 22, "2 tbsp peanut butter", 4 },
                    { 23, "1 banana, sliced", 4 },
                    { 24, "1 tsp honey (optional)", 4 },
                    { 25, "1 cup Greek yogurt", 5 },
                    { 26, "¼ cup granola", 5 },
                    { 27, "½ cup mixed berries", 5 },
                    { 28, "1 tbsp honey", 5 },
                    { 29, "1 large potato", 7 },
                    { 30, "1 tbsp butter", 7 },
                    { 31, "Salt to taste", 7 },
                    { 32, "Black pepper to taste", 7 },
                    { 33, "2 tbsp shredded cheese (optional)", 7 },
                    { 34, "2 eggs", 8 },
                    { 35, "2 tbsp milk", 8 },
                    { 36, "2 tbsp shredded cheese", 8 },
                    { 37, "Salt to taste", 8 },
                    { 38, "Black pepper to taste", 8 },
                    { 39, "1 tsp butter", 8 },
                    { 40, "1 large tortilla", 9 },
                    { 41, "½ cup shredded cheese", 9 },
                    { 42, "2 tbsp cooked chicken (optional)", 9 },
                    { 43, "2 tbsp salsa", 9 },
                    { 44, "4 tbsp flour", 10 },
                    { 45, "¼ tsp baking powder", 10 },
                    { 46, "Pinch of salt", 10 },
                    { 47, "3 tbsp milk", 10 },
                    { 48, "1 tsp olive oil", 10 },
                    { 49, "2 tbsp pizza sauce", 10 },
                    { 50, "2 tbsp shredded mozzarella", 10 },
                    { 51, "Toppings of choice", 10 },
                    { 52, "1 package ramen noodles", 11 },
                    { 53, "2 cups water", 11 },
                    { 54, "1 egg", 11 },
                    { 55, "2 green onions, chopped", 11 },
                    { 56, "1 tbsp soy sauce", 11 },
                    { 57, "½ tsp sesame oil", 11 },
                    { 58, "2 slices bread", 12 },
                    { 59, "2 slices cheese", 12 },
                    { 60, "1 tbsp butter", 12 },
                    { 61, "1 can tomato soup", 12 },
                    { 62, "½ cup milk", 12 },
                    { 63, "1 cup cooked rice", 13 },
                    { 64, "2 eggs", 13 },
                    { 65, "½ cup frozen mixed vegetables", 13 },
                    { 66, "2 tbsp soy sauce", 13 },
                    { 67, "1 tbsp oil", 13 },
                    { 68, "2 green onions, chopped", 13 },
                    { 69, "½ cup pasta", 14 },
                    { 70, "3 tbsp olive oil", 14 },
                    { 71, "3 garlic cloves, minced", 14 },
                    { 72, "¼ tsp red pepper flakes", 14 },
                    { 73, "Salt to taste", 14 },
                    { 74, "1 tbsp fresh parsley", 14 },
                    { 75, "1 cup mixed vegetables", 15 },
                    { 76, "1 cup cooked rice", 15 },
                    { 77, "2 tbsp soy sauce", 15 },
                    { 78, "2 garlic cloves, minced", 15 },
                    { 79, "1 tsp fresh ginger, grated", 15 },
                    { 80, "1 tbsp oil", 15 }
                });

            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "Id", "InstructionText", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 1, "In a jar or container, mix oats and milk", 1, 1 },
                    { 2, "Add peanut butter and stir well", 1, 2 },
                    { 3, "Add fruit and chia seeds if desired", 1, 3 },
                    { 4, "Cover and refrigerate overnight", 1, 4 },
                    { 5, "Drain tuna and mix with mayo or Greek yogurt", 2, 1 },
                    { 6, "Spoon tuna mixture into tortilla", 2, 2 },
                    { 7, "Add lettuce leaves", 2, 3 },
                    { 8, "Roll up tightly and serve", 2, 4 },
                    { 9, "In a microwave-safe mug, add pasta and water", 6, 1 },
                    { 10, "Microwave 4-5 minutes until pasta is soft, stirring halfway", 6, 2 },
                    { 11, "Stir in cheese and milk until creamy", 6, 3 },
                    { 12, "Microwave 30 seconds more if needed", 6, 4 },
                    { 13, "Cut cucumber, tomato, and bell pepper into thin slices", 3, 1 },
                    { 14, "Warm pita bread in microwave for 15 seconds", 3, 2 },
                    { 15, "Spread hummus evenly inside the pita", 3, 3 },
                    { 16, "Add lettuce leaves, then layer in vegetables", 3, 4 },
                    { 17, "Season with salt and pepper to taste", 3, 5 },
                    { 18, "Toast bread slices if desired", 4, 1 },
                    { 19, "Spread peanut butter on both slices of bread", 4, 2 },
                    { 20, "Slice banana into thin rounds", 4, 3 },
                    { 21, "Arrange banana slices on one slice of bread", 4, 4 },
                    { 22, "Drizzle honey over bananas if desired", 4, 5 },
                    { 23, "Top with second slice of bread and serve", 4, 6 },
                    { 24, "Add half the Greek yogurt to a glass or bowl", 5, 1 },
                    { 25, "Sprinkle half the granola over the yogurt", 5, 2 },
                    { 26, "Add half the berries on top", 5, 3 },
                    { 27, "Repeat layers with remaining ingredients", 5, 4 },
                    { 28, "Drizzle honey over the top and serve", 5, 5 },
                    { 29, "Wash potato and pierce several times with a fork", 7, 1 },
                    { 30, "Place potato on a microwave-safe plate", 7, 2 },
                    { 31, "Microwave on high for 4-6 minutes, flipping halfway", 7, 3 },
                    { 32, "Let cool for 1 minute, then cut open lengthwise", 7, 4 },
                    { 33, "Add butter, salt, pepper, and cheese if desired", 7, 5 },
                    { 34, "Beat eggs with milk, salt, and pepper in a microwave-safe bowl", 8, 1 },
                    { 35, "Add butter to the bowl", 8, 2 },
                    { 36, "Microwave for 30 seconds, then stir", 8, 3 },
                    { 37, "Continue microwaving in 30-second intervals, stirring each time", 8, 4 },
                    { 38, "When eggs are almost set, stir in cheese and microwave 15 more seconds", 8, 5 },
                    { 39, "Place tortilla on a microwave-safe plate", 9, 1 },
                    { 40, "Sprinkle half the cheese on one half of the tortilla", 9, 2 },
                    { 41, "Add chicken if using, then remaining cheese", 9, 3 },
                    { 42, "Fold tortilla in half", 9, 4 },
                    { 43, "Microwave for 1-2 minutes until cheese melts", 9, 5 },
                    { 44, "Let cool slightly, then serve with salsa", 9, 6 },
                    { 45, "Mix flour, baking powder, and salt in a large mug", 10, 1 },
                    { 46, "Add milk and olive oil, stir until smooth", 10, 2 },
                    { 47, "Spread pizza sauce over the dough", 10, 3 },
                    { 48, "Top with cheese and desired toppings", 10, 4 },
                    { 49, "Microwave for 1-2 minutes until cheese melts", 10, 5 },
                    { 50, "Bring water to a boil in a small pot", 11, 1 },
                    { 51, "Add ramen noodles and cook for 2 minutes", 11, 2 },
                    { 52, "Crack egg into the pot and let it poach for 1 minute", 11, 3 },
                    { 53, "Add soy sauce and sesame oil", 11, 4 },
                    { 54, "Garnish with chopped green onions and serve", 11, 5 },
                    { 55, "Heat a pan over medium heat", 12, 1 },
                    { 56, "Butter one side of each bread slice", 12, 2 },
                    { 57, "Place cheese between bread slices, buttered sides out", 12, 3 },
                    { 58, "Cook sandwich 2-3 minutes per side until golden", 12, 4 },
                    { 59, "Meanwhile, heat tomato soup with milk in microwave or stovetop", 12, 5 },
                    { 60, "Serve grilled cheese with warm soup", 12, 6 },
                    { 61, "Heat oil in a pan over medium-high heat", 13, 1 },
                    { 62, "Scramble eggs in the pan, then remove and set aside", 13, 2 },
                    { 63, "Add frozen vegetables to the pan and cook 2-3 minutes", 13, 3 },
                    { 64, "Add cooked rice and stir-fry for 2 minutes", 13, 4 },
                    { 65, "Return eggs to pan, add soy sauce and green onions", 13, 5 },
                    { 66, "Stir everything together and serve hot", 13, 6 },
                    { 67, "Cook pasta according to package directions", 14, 1 },
                    { 68, "Heat olive oil in a pan over medium heat", 14, 2 },
                    { 69, "Add minced garlic and red pepper flakes, cook 1 minute", 14, 3 },
                    { 70, "Add cooked pasta to the pan and toss", 14, 4 },
                    { 71, "Season with salt and garnish with parsley", 14, 5 },
                    { 72, "Heat oil in a large pan over high heat", 15, 1 },
                    { 73, "Add minced garlic and ginger, cook 30 seconds", 15, 2 },
                    { 74, "Add mixed vegetables and stir-fry for 3-4 minutes", 15, 3 },
                    { 75, "Add cooked rice and soy sauce", 15, 4 },
                    { 76, "Continue stir-frying for 2-3 minutes until heated through", 15, 5 }
                });

            migrationBuilder.InsertData(
                table: "MealPlanRecipes",
                columns: new[] { "MealPlanId", "RecipeId" },
                values: new object[,]
                {
                    { 1, 6 },
                    { 1, 9 },
                    { 1, 11 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 11 },
                    { 3, 6 },
                    { 3, 8 },
                    { 3, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_RecipeId",
                table: "CartItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanRecipes_RecipeId",
                table: "MealPlanRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_RecipeId",
                table: "OrderItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "MealPlanRecipes");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
