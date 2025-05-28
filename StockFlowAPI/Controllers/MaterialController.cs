using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET: api/Material
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            var materials = await _materialService.GetAllAsync();
            return Ok(materials);
        }

        // GET: api/Material/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(int id)
        {
            var material = await _materialService.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            return Ok(material);
        }

        // POST: api/Material
        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial(Material material)
        {
            try
            {
                var created = await _materialService.CreateAsync(material);
                return CreatedAtAction(nameof(GetMaterial), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/Material/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, Material material)
        {
            if (id != material.Id)
                return BadRequest(new { error = "ID do material não corresponde." });

            try
            {
                var updated = await _materialService.UpdateAsync(material);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/Material/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            var success = await _materialService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
