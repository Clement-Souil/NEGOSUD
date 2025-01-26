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
    public class LigneCommandesController : ControllerBase
    {
        private readonly NegosudContext _context;

        public LigneCommandesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/LigneCommandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigneCommande>>> GetLigneCommandes()
        {
            return await _context.LigneCommandes.ToListAsync();
        }

        // GET: api/LigneCommandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LigneCommande>> GetLigneCommande(int id)
        {
            var ligneCommande = await _context.LigneCommandes.FindAsync(id);

            if (ligneCommande == null)
            {
                return NotFound();
            }

            return ligneCommande;
        }

        // PUT: api/LigneCommandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLigneCommande(int id, LigneCommande ligneCommande)
        {
            if (id != ligneCommande.Id)
            {
                return BadRequest();
            }

            _context.Entry(ligneCommande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigneCommandeExists(id))
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

        // POST: api/LigneCommandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LigneCommande>> PostLigneCommande(LigneCommande ligneCommande)
        {
            _context.LigneCommandes.Add(ligneCommande);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLigneCommande", new { id = ligneCommande.Id }, ligneCommande);
        }

        // DELETE: api/LigneCommandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLigneCommande(int id)
        {
            var ligneCommande = await _context.LigneCommandes.FindAsync(id);
            if (ligneCommande == null)
            {
                return NotFound();
            }

            _context.LigneCommandes.Remove(ligneCommande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LigneCommandeExists(int id)
        {
            return _context.LigneCommandes.Any(e => e.Id == id);
        }
    }
}
