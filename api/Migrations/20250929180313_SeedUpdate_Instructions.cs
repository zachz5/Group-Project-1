using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdate_Instructions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "Id", "InstructionText", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 13, "Warm pita briefly (optional)", 3, 1 },
                    { 14, "Spread hummus inside pita", 3, 2 },
                    { 15, "Fill with cucumber and carrot", 3, 3 },
                    { 16, "Spread peanut butter on bread", 4, 1 },
                    { 17, "Layer banana slices and drizzle honey", 4, 2 },
                    { 18, "Close sandwich and slice", 4, 3 },
                    { 19, "Layer yogurt in a cup", 5, 1 },
                    { 20, "Add granola and berries", 5, 2 },
                    { 21, "Repeat layers and serve", 5, 3 },
                    { 22, "Pierce potato with fork", 7, 1 },
                    { 23, "Microwave 6–8 minutes until tender", 7, 2 },
                    { 24, "Split and add butter and cheese", 7, 3 },
                    { 25, "Beat eggs with milk in bowl", 8, 1 },
                    { 26, "Microwave 30s, stir, repeat until set", 8, 2 },
                    { 27, "Stir in cheese and season", 8, 3 },
                    { 28, "Place cheese between tortillas", 9, 1 },
                    { 29, "Microwave ~1 minute until melted", 9, 2 },
                    { 30, "Slice and serve", 9, 3 },
                    { 31, "Mix flour, milk, oil in mug", 10, 1 },
                    { 32, "Top with sauce and cheese", 10, 2 },
                    { 33, "Microwave 1–2 minutes until set", 10, 3 },
                    { 34, "Boil noodles with veggies", 11, 1 },
                    { 35, "Add egg in last minute", 11, 2 },
                    { 36, "Season and serve", 11, 3 },
                    { 37, "Butter bread and add cheese", 12, 1 },
                    { 38, "Grill until golden both sides", 12, 2 },
                    { 39, "Heat soup and serve together", 12, 3 },
                    { 40, "Scramble egg, set aside", 13, 1 },
                    { 41, "Stir-fry rice and veggies", 13, 2 },
                    { 42, "Add soy sauce and egg", 13, 3 },
                    { 43, "Cook pasta until al dente", 14, 1 },
                    { 44, "Sauté garlic in olive oil", 14, 2 },
                    { 45, "Toss pasta with oil and chili", 14, 3 },
                    { 46, "Cook rice (or use leftover)", 15, 1 },
                    { 47, "Stir-fry vegetables", 15, 2 },
                    { 48, "Season with soy and serve", 15, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Instructions",
                keyColumn: "Id",
                keyValue: 48);
        }
    }
}
