using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MealPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MealPlan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealPlan>>> GetMealPlans()
        {
            return await _context.MealPlans
                .Include(mp => mp.MealPlanRecipes)
                    .ThenInclude(mpr => mpr.Recipe)
                .ToListAsync();
        }

        // GET: api/MealPlan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlan>> GetMealPlan(int id)
        {
            var mealPlan = await _context.MealPlans
                .Include(mp => mp.MealPlanRecipes)
                    .ThenInclude(mpr => mpr.Recipe)
                .FirstOrDefaultAsync(mp => mp.Id == id);

            if (mealPlan == null)
            {
                return NotFound();
            }

            return mealPlan;
        }

        // POST: api/MealPlan
        [HttpPost]
        public async Task<ActionResult<MealPlan>> PostMealPlan(MealPlan mealPlan)
        {
            _context.MealPlans.Add(mealPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMealPlan", new { id = mealPlan.Id }, mealPlan);
        }

        // PUT: api/MealPlan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMealPlan(int id, MealPlan mealPlan)
        {
            if (id != mealPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(mealPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/MealPlan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealPlan(int id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            _context.MealPlans.Remove(mealPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/MealPlan/5/recipes
        [HttpPost("{id}/recipes")]
        public async Task<IActionResult> AddRecipeToMealPlan(int id, int recipeId)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound("Meal plan not found");
            }

            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            var mealPlanRecipe = new MealPlanRecipe
            {
                MealPlanId = id,
                RecipeId = recipeId
            };

            _context.MealPlanRecipes.Add(mealPlanRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/MealPlan/5/recipes/10
        [HttpDelete("{id}/recipes/{recipeId}")]
        public async Task<IActionResult> RemoveRecipeFromMealPlan(int id, int recipeId)
        {
            var mealPlanRecipe = await _context.MealPlanRecipes
                .FirstOrDefaultAsync(mpr => mpr.MealPlanId == id && mpr.RecipeId == recipeId);

            if (mealPlanRecipe == null)
            {
                return NotFound();
            }

            _context.MealPlanRecipes.Remove(mealPlanRecipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealPlanExists(int id)
        {
            return _context.MealPlans.Any(e => e.Id == id);
        }
    }
}
