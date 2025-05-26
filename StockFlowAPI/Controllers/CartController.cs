using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/1
        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetCart(int saleId)
        {
            var items = await _cartService.GetCartItemsAsync(saleId);
            return Ok(items);
        }

        // POST: api/cart/1/add
        [HttpPost("{saleId}/add")]
        public async Task<IActionResult> AddItem(int saleId, [FromBody] SaleItem item)
        {
            var result = await _cartService.AddItemAsync(saleId, item);
            return Ok(result);
        }

        // PUT: api/cart/1/update/5
        [HttpPut("{saleId}/update/{itemId}")]
        public async Task<IActionResult> UpdateItem(int saleId, int itemId, [FromBody] int quantity)
        {
            var result = await _cartService.UpdateItemQuantityAsync(saleId, itemId, quantity);
            return Ok(result);
        }

        // DELETE: api/cart/1/remove/5
        [HttpDelete("{saleId}/remove/{itemId}")]
        public async Task<IActionResult> RemoveItem(int saleId, int itemId)
        {
            var success = await _cartService.RemoveItemAsync(saleId, itemId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
