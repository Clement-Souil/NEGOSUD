using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;
using NegosudLibrary.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNegosud.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly NegosudContext _context;

        public CommandesController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/Commandes (Liste de toutes les commandes)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeDTO>>> GetCommandes()
        {
            var commandes = await _context.Commandes
                .Include(c => c.StatutCommande)
                .Include(c => c.User)
                .Include(c => c.Fournisseur)
                .Include(c => c.LignesCommande)
                .ThenInclude(lc => lc.Article)
                .ToListAsync();

            List<CommandeDTO> commandeDTOs = new List<CommandeDTO>();

            foreach (var item in commandes)
            {
                double prixtotal = 0;

                CommandeDTO dto = new CommandeDTO
                {
                    Id = item.Id,
                    Date = item.Date,
                    UserId = item.UserId,
                    StatutCommandeId = item.StatutCommandeId,
                    FournisseurId = item.FournisseurId,
                    FournisseurNom = item.Fournisseur.NomDomaine,
                    PrixTotal = prixtotal,
                    UserNom = item.User.Nom + " " + item.User.Prenom,
                    UserAdresse = item.User.Adresse, // ✅ Adresse utilisateur ajoutée
                    StatutCommande = item.StatutCommande.Statut,
                    IsClient = item.IsClient, // ✅ Ajout du champ IsClient
                    LignesCommandes = new List<LigneCommandeDTO>()
                };

                foreach (var ligne in item.LignesCommande)
                {
                    prixtotal += ligne.Prix * ligne.Quantite; // ✅ Multiplication correcte avec quantité

                    LigneCommandeDTO ligneCommandeDto = new LigneCommandeDTO
                    {
                        Id = ligne.Id,
                        Prix = ligne.Prix,
                        Quantite = ligne.Quantite,
                        ArticleId = ligne.ArticleId,
                        CommandeId = ligne.CommandeId,
                        Article = new ArticleDTO
                        {
                            Id = ligne.Article.Id,
                            Nom = ligne.Article.Nom,
                            PrixVente = ligne.Article.PrixVente
                        }
                    };

                    dto.LignesCommandes.Add(ligneCommandeDto);
                }

                dto.PrixTotal = prixtotal; // ✅ Mise à jour correcte du prix total
                commandeDTOs.Add(dto);
            }

            return commandeDTOs;
        }

        // GET: api/Commandes/{id} (Commande spécifique)
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeDTO>> GetCommandeById(int id)
        {
            var item = await _context.Commandes
                .Include(c => c.StatutCommande)
                .Include(c => c.User)
                .Include(c => c.Fournisseur)
                .Include(c => c.LignesCommande)
                .ThenInclude(lc => lc.Article)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return new CommandeDTO
            {
                Id = item.Id,
                Date = item.Date,
                UserId = item.UserId,
                UserNom = $"{item.User.Nom} {item.User.Prenom}".Trim(),
                UserAdresse = item.User.Adresse, // ✅ Adresse utilisateur ajoutée
                StatutCommandeId = item.StatutCommandeId,
                StatutCommande = item.StatutCommande.Statut,
                FournisseurId = item.FournisseurId,
                FournisseurNom = item.Fournisseur.NomDomaine,
                PrixTotal = item.LignesCommande.Sum(lc => lc.Prix * lc.Quantite),
                LignesCommandes = item.LignesCommande.Select(lc => new LigneCommandeDTO
                {
                    Id = lc.Id,
                    Prix = lc.Prix,
                    Quantite = lc.Quantite,
                    ArticleId = lc.ArticleId,
                    Article = new ArticleDTO
                    {
                        Id = lc.Article.Id,
                        Nom = lc.Article.Nom,
                        PrixVente = lc.Article.PrixVente
                    }
                }).ToList()
            };
        }

        // PUT: api/Commandes/{id} (Modification)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.Id)
            {
                return BadRequest();
            }

            _context.Entry(commande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeExists(id))
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

        // POST: api/Commandes (Création)
        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommandeById", new { id = commande.Id }, commande);
        }

        // DELETE: api/Commandes/{id} (Suppression)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await _context.Commandes
                .Include(c => c.LignesCommande) // 🔥 Supprime les lignes associées
                .FirstOrDefaultAsync(c => c.Id == id);

            if (commande == null)
            {
                return NotFound();
            }

            _context.LigneCommandes.RemoveRange(commande.LignesCommande);
            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Vérification d'existence
        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.Id == id);
        }
    }
}
