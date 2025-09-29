using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdate_Ingredients : Migration
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
                    { 14, "1 pita", 3 },
                    { 15, "3 tbsp hummus", 3 },
                    { 16, "sliced cucumber", 3 },
                    { 17, "shredded carrot", 3 },
                    { 18, "2 slices bread", 4 },
                    { 19, "1 tbsp peanut butter", 4 },
                    { 20, "banana slices", 4 },
                    { 21, "honey (optional)", 4 },
                    { 22, "1 cup Greek yogurt", 5 },
                    { 23, "1/3 cup granola", 5 },
                    { 24, "1/2 cup berries", 5 },
                    { 25, "1 large potato", 7 },
                    { 26, "1 tbsp butter", 7 },
                    { 27, "2 tbsp shredded cheddar", 7 },
                    { 28, "2 eggs", 8 },
                    { 29, "2 tbsp milk", 8 },
                    { 30, "2 tbsp shredded cheese", 8 },
                    { 31, "2 tortillas", 9 },
                    { 32, "1/2 cup shredded cheese", 9 },
                    { 33, "4 tbsp flour", 10 },
                    { 34, "2 tbsp milk", 10 },
                    { 35, "1 tbsp oil", 10 },
                    { 36, "2 tbsp pizza sauce", 10 },
                    { 37, "2 tbsp mozzarella", 10 },
                    { 38, "1 pack ramen", 11 },
                    { 39, "1 egg", 11 },
                    { 40, "1/4 cup frozen veggies", 11 },
                    { 41, "2 slices bread", 12 },
                    { 42, "2 slices cheese", 12 },
                    { 43, "1 can tomato soup", 12 },
                    { 44, "1 cup cooked rice", 13 },
                    { 45, "1 egg", 13 },
                    { 46, "1/2 cup mixed veggies", 13 },
                    { 47, "1 tbsp soy sauce", 13 },
                    { 48, "1 cup pasta", 14 },
                    { 49, "2 tbsp olive oil", 14 },
                    { 50, "2 cloves garlic", 14 },
                    { 51, "1 cup rice", 15 },
                    { 52, "1 cup mixed vegetables", 15 },
                    { 53, "1 tbsp soy sauce", 15 }
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
                    { 12, "Microwave 30 seconds more if needed", 6, 4 }
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
