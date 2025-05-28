using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Data;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IInventoryService _inventoryService;

        public InventoryController(AppDbContext context, IInventoryService inventoryService)
        {
            _context = context;
            _inventoryService = inventoryService;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
        {
            return await _context.Inventory
                .Include(i => i.Material) 
                .ToListAsync();
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventory
                .Include(i => i.Material)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventory), new { id = inventory.Id }, inventory);
        }

        [HttpPost("movement")]
        public async Task<IActionResult> ApplyInventoryMovement([FromBody] InventoryMovementRequestDto movement)
        {
            try
            {
                await _inventoryService.ApplyInventoryMovementAsync(movement);
                return Ok(new { message = "Movimentação realizada com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Id == id);
        }
    }
}