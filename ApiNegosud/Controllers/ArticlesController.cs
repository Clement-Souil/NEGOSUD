using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;

namespace ApiNegosud.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly NegosudContext _context;

        public ArticlesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var negosudContext = _context.Articles.Include(a => a.FamilleArticle).Include(a => a.Fournisseur);
            return View(await negosudContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.FamilleArticle)
                .Include(a => a.Fournisseur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["FamilleArticleId"] = new SelectList(_context.FamilleArticles, "Id", "Nom");
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs, "Id", "NomDomaine");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,PrixVente,PrixAchat,Annee,Description,Degre,Cepage,Quantite,SeuilReappro,FamilleArticleId,FournisseurId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilleArticleId"] = new SelectList(_context.FamilleArticles, "Id", "Nom", article.FamilleArticleId);
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs, "Id", "NomDomaine", article.FournisseurId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["FamilleArticleId"] = new SelectList(_context.FamilleArticles, "Id", "Nom", article.FamilleArticleId);
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs, "Id", "NomDomaine", article.FournisseurId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,PrixVente,PrixAchat,Annee,Description,Degre,Cepage,Quantite,SeuilReappro,FamilleArticleId,FournisseurId")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilleArticleId"] = new SelectList(_context.FamilleArticles, "Id", "Nom", article.FamilleArticleId);
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs, "Id", "NomDomaine", article.FournisseurId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.FamilleArticle)
                .Include(a => a.Fournisseur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
