using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockFlowAPI.Data;
using StockFlowAPI.Models;

namespace StockFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaleItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SaleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleItem>>> GetSaleItems()
        {
            return await _context.SaleItems
                .Include(si => si.Sale)
                .Include(si => si.Material)
                .ToListAsync();
        }

        // GET: api/SaleItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleItem>> GetSaleItem(int id)
        {
            var saleItem = await _context.SaleItems
                .Include(si => si.Sale)
                .Include(si => si.Material)
                .FirstOrDefaultAsync(si => si.Id == id);

            if (saleItem == null)
            {
                return NotFound();
            }

            return saleItem;
        }

        // POST: api/SaleItems
        [HttpPost]
        public async Task<ActionResult<SaleItem>> PostSaleItem(SaleItem saleItem)
        {
            _context.SaleItems.Add(saleItem);
            await _context.SaveChangesAsync();

            // Atualiza o total da venda após adicionar o item
            await UpdateSaleTotal(saleItem.SaleId);

            return CreatedAtAction(nameof(GetSaleItem), new { id = saleItem.Id }, saleItem);
        }

        // PUT: api/SaleItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleItem(int id, SaleItem saleItem)
        {
            if (id != saleItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(saleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Atualiza o total da venda após edição do item
                await UpdateSaleTotal(saleItem.SaleId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemExists(id))
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

        // DELETE: api/SaleItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleItem(int id)
        {
            var saleItem = await _context.SaleItems.FindAsync(id);
            if (saleItem == null)
            {
                return NotFound();
            }

            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync();

            //  Atualiza o total da venda após remover o item
            await UpdateSaleTotal(saleItem.SaleId);

            return NoContent();
        }

        private bool SaleItemExists(int id)
        {
            return _context.SaleItems.Any(e => e.Id == id);
        }

        // Método privado para atualizar o total da venda automaticamente
        private async Task UpdateSaleTotal(int saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale != null)
            {
                sale.Total = sale.SaleItems.Sum(i => i.Quantity * i.UnitPrice);
                _context.Sales.Update(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
