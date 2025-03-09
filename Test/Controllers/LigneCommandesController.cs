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
using NegosudLibrary.DTO;

namespace ApiNegosud.Controllers
{
    //[Authorize]
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

        [HttpGet("byCommande/{commandeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<LigneCommandeDTO>> GetLignesCommandeByCommandeId(int commandeId)
        {
            var lignesCommande = await _context.LigneCommandes
                .Where(lc => lc.CommandeId == commandeId)
                .ToListAsync();

            if (lignesCommande == null || !lignesCommande.Any())
            {
                return new List<LigneCommandeDTO>();
            }

            List<LigneCommandeDTO> lignesCommandeDTO = new List<LigneCommandeDTO>();


            foreach (var ligne in lignesCommande)
            {
                var article = await _context.Articles
                                .Include(c => c.Fournisseur)
                                .Include(x => x.FamilleArticle)
                                .FirstOrDefaultAsync(a => a.Id == ligne.ArticleId );
                var dto = new LigneCommandeDTO
                {
                    Id = ligne.Id,
                    Prix = ligne.Prix,
                    Quantite = ligne.Quantite,
                    ArticleId = ligne.ArticleId,
                    Article = new ArticleDTO
                    {
                        Id = article.Id,
                        Nom = article.Nom,
                        PrixVente = article.PrixVente

                    }
                };

                lignesCommandeDTO.Add(dto);
            }

            return lignesCommandeDTO;
        }

    }
}
