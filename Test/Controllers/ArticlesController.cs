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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly NegosudContext _context;

        public ArticlesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
        {
            List<ArticleDTO> articleDTOs = new List<ArticleDTO>();

            var l =  await _context.Articles
                        .Include(c => c.FamilleArticle)
                        .Include(x => x.Fournisseur)
                        .ToListAsync();

            foreach(var article in l)
            {
                ArticleDTO articleDTO = new ArticleDTO
                {
                    Id = article.Id,
                    Nom = article.Nom,
                    PrixAchat = article.PrixAchat,
                    PrixVente = article.PrixVente,
                    Annee = article.Annee,
                    Degre = article.Degre,
                    Description = article.Description,
                    Famille = article.FamilleArticle.Nom,
                    Cepage = article.Cepage,
                    Quantite = article.Quantite,
                    Volume = article.Volume,
                    SeuilMinimal = article.SeuilMinimal,
                    SeuilReappro = article.SeuilReappro,
                    Fournisseur = article.Fournisseur.NomDomaine
                };
                articleDTOs.Add(articleDTO);
            }
            return articleDTOs;
            
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles
                            .Include(c => c.Fournisseur)
                            .Include(x => x.FamilleArticle)
                            .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            Article n = new Article
            {
                Annee = article.Annee,
                FamilleArticleId = article.FamilleArticleId,
                FournisseurId = article.FournisseurId,
                Nom = article.Nom,
                Cepage = article.Cepage,
                PrixAchat = article.PrixAchat,
                PrixVente = article.PrixVente,
                Degre = article.Degre,
                Description = article.Description,
                Quantite = article.Quantite,
                SeuilMinimal = article.SeuilMinimal,
                SeuilReappro = article.SeuilReappro,
                Volume = article.Volume
            };

            _context.Articles.Add(n);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
