using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;

namespace ApiNegosud.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MouvementStocksController : ControllerBase
    {
        private readonly NegosudContext _context;

        public MouvementStocksController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/MouvementStocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MouvementStock>>> GetMouvementStocks()
        {
            return await _context.MouvementStocks.ToListAsync();
        }

        // GET: api/MouvementStocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MouvementStock>> GetMouvementStock(int id)
        {
            var mouvementStock = await _context.MouvementStocks.FindAsync(id);

            if (mouvementStock == null)
            {
                return NotFound();
            }

            return mouvementStock;
        }

        // PUT: api/MouvementStocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMouvementStock(int id, MouvementStock mouvementStock)
        {
            if (id != mouvementStock.Id)
            {
                return BadRequest();
            }

            _context.Entry(mouvementStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MouvementStockExists(id))
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

        // POST: api/MouvementStocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MouvementStock>> PostMouvementStock(MouvementStock mouvementStock)
        {
            _context.MouvementStocks.Add(mouvementStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMouvementStock", new { id = mouvementStock.Id }, mouvementStock);
        }

        // DELETE: api/MouvementStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMouvementStock(int id)
        {
            var mouvementStock = await _context.MouvementStocks.FindAsync(id);
            if (mouvementStock == null)
            {
                return NotFound();
            }

            _context.MouvementStocks.Remove(mouvementStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MouvementStockExists(int id)
        {
            return _context.MouvementStocks.Any(e => e.Id == id);
        }
    }
}
