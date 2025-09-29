using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CartResponse>> GetCart()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Recipe)
                        .ThenInclude(r => r.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Create cart if it doesn't exist
                cart = new Cart { UserId = userId.Value };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItems = cart.CartItems.Select(ci => new CartItemResponse
            {
                Id = ci.Id,
                RecipeId = ci.RecipeId,
                RecipeTitle = ci.Recipe.Title,
                RecipePrice = ci.Recipe.Price,
                RecipeImage = ci.Recipe.Image,
                CategoryName = ci.Recipe.Category.Name,
                Quantity = ci.Quantity,
                AddedAt = ci.AddedAt
            }).ToList();

            return Ok(new CartResponse
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cartItems,
                TotalItems = cartItems.Sum(ci => ci.Quantity),
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt
            });
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddToCart(AddToCartRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId.Value };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if recipe exists
            var recipe = await _context.Recipes.FindAsync(request.RecipeId);
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            // Check if item already exists in cart
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.RecipeId == request.RecipeId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    RecipeId = request.RecipeId,
                    Quantity = request.Quantity
                };
                _context.CartItems.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok("Item added to cart");
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateCartItem(UpdateCartItemRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == request.CartItemId && ci.Cart.UserId == userId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            if (request.Quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = request.Quantity;
            }

            cartItem.Cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok("Cart item updated");
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<ActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart.UserId == userId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            _context.CartItems.Remove(cartItem);
            cartItem.Cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok("Item removed from cart");
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCart()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            _context.CartItems.RemoveRange(cart.CartItems);
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok("Cart cleared");
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }
    }

    public class AddToCartRequest
    {
        public int RecipeId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartItemRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
        public int TotalItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CartItemResponse
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; } = string.Empty;
        public string RecipePrice { get; set; } = string.Empty;
        public string RecipeImage { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
