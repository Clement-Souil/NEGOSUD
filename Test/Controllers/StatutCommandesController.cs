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
    public class StatutCommandesController : ControllerBase
    {
        private readonly NegosudContext _context;

        public StatutCommandesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/StatutCommandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatutCommande>>> GetStatutCommandes()
        {
            return await _context.StatutCommandes.ToListAsync();
        }

        // GET: api/StatutCommandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatutCommande>> GetStatutCommande(int id)
        {
            var statutCommande = await _context.StatutCommandes.FindAsync(id);

            if (statutCommande == null)
            {
                return NotFound();
            }

            return statutCommande;
        }

        // PUT: api/StatutCommandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatutCommande(int id, StatutCommande statutCommande)
        {
            if (id != statutCommande.Id)
            {
                return BadRequest();
            }

            _context.Entry(statutCommande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatutCommandeExists(id))
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

        // POST: api/StatutCommandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatutCommande>> PostStatutCommande(StatutCommande statutCommande)
        {
            _context.StatutCommandes.Add(statutCommande);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatutCommande", new { id = statutCommande.Id }, statutCommande);
        }

        // DELETE: api/StatutCommandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatutCommande(int id)
        {
            var statutCommande = await _context.StatutCommandes.FindAsync(id);
            if (statutCommande == null)
            {
                return NotFound();
            }

            _context.StatutCommandes.Remove(statutCommande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatutCommandeExists(int id)
        {
            return _context.StatutCommandes.Any(e => e.Id == id);
        }
    }
}
