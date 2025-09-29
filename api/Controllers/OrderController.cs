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
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<OrderResponse>> Checkout(CheckoutRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            // Get user's cart
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Recipe)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return BadRequest("Cart is empty");
            }

            // Create order
            var order = new Order
            {
                UserId = userId.Value,
                OrderNumber = GenerateOrderNumber(),
                Status = "Pending",
                PaymentStatus = "Pending",
                TotalAmount = CalculateTotal(cart.CartItems),
                Notes = request.Notes
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create order items
            foreach (var cartItem in cart.CartItems)
            {
                var unitPrice = ParsePrice(cartItem.Recipe.Price);
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    RecipeId = cartItem.RecipeId,
                    Quantity = cartItem.Quantity,
                    RecipeTitle = cartItem.Recipe.Title,
                    RecipePrice = cartItem.Recipe.Price,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * cartItem.Quantity
                };
                _context.OrderItems.Add(orderItem);
            }

            // Clear cart
            _context.CartItems.RemoveRange(cart.CartItems);
            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Return order details
            var orderResponse = await GetOrderDetails(order.Id);
            return Ok(orderResponse);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Recipe)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponses = orders.Select(o => new OrderResponse
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Status = o.Status,
                PaymentStatus = o.PaymentStatus,
                TotalAmount = o.TotalAmount,
                Notes = o.Notes,
                OrderDate = o.OrderDate,
                ShippedDate = o.ShippedDate,
                DeliveredDate = o.DeliveredDate,
                Items = o.OrderItems.Select(oi => new OrderItemResponse
                {
                    Id = oi.Id,
                    RecipeId = oi.RecipeId,
                    RecipeTitle = oi.RecipeTitle,
                    RecipePrice = oi.RecipePrice,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            }).ToList();

            return Ok(orderResponses);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int orderId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Recipe)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(await GetOrderDetails(order.Id));
        }

        private async Task<OrderResponse> GetOrderDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                TotalAmount = order.TotalAmount,
                Notes = order.Notes,
                OrderDate = order.OrderDate,
                ShippedDate = order.ShippedDate,
                DeliveredDate = order.DeliveredDate,
                Items = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    Id = oi.Id,
                    RecipeId = oi.RecipeId,
                    RecipeTitle = oi.RecipeTitle,
                    RecipePrice = oi.RecipePrice,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };
        }

        private string GenerateOrderNumber()
        {
            return $"EZ-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }

        private decimal CalculateTotal(ICollection<CartItem> cartItems)
        {
            return cartItems.Sum(ci => ParsePrice(ci.Recipe.Price) * ci.Quantity);
        }

        private decimal ParsePrice(string priceString)
        {
            if (string.IsNullOrEmpty(priceString)) return 0;
            
            // Remove $ and parse
            var cleanPrice = priceString.Replace("$", "").Trim();
            if (decimal.TryParse(cleanPrice, out decimal price))
            {
                return price;
            }
            return 0;
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

    public class CheckoutRequest
    {
        public string Notes { get; set; } = string.Empty;
    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
    }

    public class OrderItemResponse
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; } = string.Empty;
        public string RecipePrice { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
