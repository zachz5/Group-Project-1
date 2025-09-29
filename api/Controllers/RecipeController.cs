using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .ToListAsync();
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // GET: api/Recipe/category/No-Cook
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByCategory(string categoryName)
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Where(r => r.Category.Name == categoryName)
                .ToListAsync();

            return recipes;
        }

        // POST: api/Recipe
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
