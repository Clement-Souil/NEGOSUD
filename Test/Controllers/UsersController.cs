using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;
using NegosudLibrary.DTO;

namespace ApiNegosud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NegosudContext _context;

        public UsersController(NegosudContext context)
        {
            _context = context;
        }

        // Récupérer la liste des utilisateurs
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDTOs = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Tel = user.Tel,
                Adresse = user.Adresse,
                Email = user.Email,
                RoleId = user.RoleId
            }).ToList();

            return userDTOs;
        }

        // Récupérer un utilisateur par ID
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Tel = user.Tel,
                Adresse = user.Adresse,
                Email = user.Email,
                RoleId = user.RoleId
            };
        }

        // Connexion utilisateur
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            // Recherche l'utilisateur dans la table Users (et PAS Identity)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null || user.Mdp != login.Mdp) // ⚠️ Vérifie le mot de passe (à hacher en prod)
            {
                return Unauthorized(new { message = "Identifiants incorrects" });
            }

            // Générer un JWT (token)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MaSuperCleSecreteTresLongueEtSecurisee1234"); // ⚠️ À stocker en sécurisé

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString()) // Ajoute le rôle si besoin
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString, UserId = user.Id, Email = user.Email });
        }




        // Inscription utilisateur
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == register.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email déjà utilisé" });
            }

            var newUser = new User
            {
                Email = register.Email,
                Mdp = register.Mdp,  // ❌ Pas de hachage ici
                Nom = register.Nom,
                Prenom = register.Prenom,
                Tel = register.Tel,
                Adresse = register.Adresse,
                RoleId = 2 // Client par défaut
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscription réussie" });
        }

        // Modifier un utilisateur
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // Supprimer un utilisateur
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Vérifie si un utilisateur existe
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    // Modèle de connexion
    public class LoginModel
    {
        public string Email { get; set; }
        public string Mdp { get; set; }
    }

    // Modèle d'inscription
    public class RegisterModel
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }
        public string Mdp { get; set; }
    }
}
