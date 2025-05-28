using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _movementService;

        public InventoryMovementController(IInventoryMovementService movementService)
        {
            _movementService = movementService;
        }

        // GET: api/InventoryMovement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMovement>>> GetMovements()
        {
            var result = await _movementService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/InventoryMovement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovement>> GetMovement(int id)
        {
            var movement = await _movementService.GetByIdAsync(id);
            if (movement == null)
                return NotFound();

            return Ok(movement);
        }

        // GET: api/InventoryMovement/material/3
        [HttpGet("material/{materialId}")]
        public async Task<ActionResult<IEnumerable<InventoryMovement>>> GetByMaterial(int materialId)
        {
            var result = await _movementService.GetByMaterialIdAsync(materialId);
            return Ok(result);
        }

        // POST: api/InventoryMovement
        [HttpPost]
        public async Task<ActionResult<InventoryMovement>> PostMovement(InventoryMovement movement)
        {
            var created = await _movementService.CreateAsync(movement);
            return CreatedAtAction(nameof(GetMovement), new { id = created.Id }, created);
        }
    }
}
