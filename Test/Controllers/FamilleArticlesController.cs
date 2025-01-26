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
    public class FamilleArticlesController : ControllerBase
    {
        private readonly NegosudContext _context;

        public FamilleArticlesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/FamilleArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilleArticle>>> GetFamilleArticles()
        {
            return await _context.FamilleArticles.ToListAsync();
        }

        // GET: api/FamilleArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilleArticle>> GetFamilleArticle(int id)
        {
            var familleArticle = await _context.FamilleArticles.FindAsync(id);

            if (familleArticle == null)
            {
                return NotFound();
            }

            return familleArticle;
        }

        // PUT: api/FamilleArticles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamilleArticle(int id, FamilleArticle familleArticle)
        {
            if (id != familleArticle.Id)
            {
                return BadRequest();
            }

            _context.Entry(familleArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilleArticleExists(id))
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

        // POST: api/FamilleArticles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FamilleArticle>> PostFamilleArticle(FamilleArticle familleArticle)
        {
            _context.FamilleArticles.Add(familleArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamilleArticle", new { id = familleArticle.Id }, familleArticle);
        }

        // DELETE: api/FamilleArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilleArticle(int id)
        {
            var familleArticle = await _context.FamilleArticles.FindAsync(id);
            if (familleArticle == null)
            {
                return NotFound();
            }

            _context.FamilleArticles.Remove(familleArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FamilleArticleExists(int id)
        {
            return _context.FamilleArticles.Any(e => e.Id == id);
        }
    }
}
