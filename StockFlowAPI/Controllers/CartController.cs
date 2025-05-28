using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Dtos;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IInventoryService _inventoryService;
        private readonly IMaterialService _materialService;

        public CartController(
            ICartService cartService,
            IInventoryService inventoryService,
            IMaterialService materialService)
        {
            _cartService = cartService;
            _inventoryService = inventoryService;
            _materialService = materialService;
        }

        //GET: api/cart/{saleId}
        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetCart(int saleId)
        {
            var items = await _cartService.GetCartItemsAsync(saleId);
            return Ok(items);
        }

        //POST: api/cart/add-item
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemDto dto)
        {
            var inventory = await _inventoryService.GetByMaterialIdAsync(dto.MaterialId);
            if (inventory == null) return NotFound("Material não encontrado.");

            if (inventory.Quantity < dto.Quantity)
                return BadRequest("Estoque insuficiente.");

            var material = await _materialService.GetByIdAsync(dto.MaterialId);
            if (material == null) return NotFound("Material não encontrado.");

            var item = new SaleItem
            {
                MaterialId = dto.MaterialId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice > 0 ? dto.UnitPrice : material.Price
            };

            var createdItem = await _cartService.AddItemAsync(dto.SaleId, item);

            return Ok(createdItem);
        }

        //PUT: api/cart/update-item
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateCartItemDto dto)
        {
            var item = await _cartService.UpdateItemQuantityAsync(dto.SaleItemId, dto.SaleItemId, dto.Quantity);
            return Ok(item);
        }

        //DELETE: api/cart/remove-item/{itemId}
        [HttpDelete("remove-item/{saleId}/{itemId}")]
        public async Task<IActionResult> RemoveItem(int saleId, int itemId)
        {
            var removed = await _cartService.RemoveItemAsync(saleId, itemId);
            if (!removed) return NotFound("Item não encontrado no carrinho.");

            return Ok("Item removido do carrinho.");
        }
    }
}
