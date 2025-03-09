
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegosudLibrary.DAO;
using NegosudLibrary.DBContext;
using NegosudWebApp.Models;
using BCrypt.Net; 

namespace ApiNegosud.Controllers;


[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly NegosudContext _context;

    public AuthController(NegosudContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ConnexionModel.RegisterModel model)
    {
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            return BadRequest("Cet email est déjà utilisé.");

        var user = new User
        {
            Prenom = model.Prenom,
            Nom = model.Nom,
            Email = model.Email,
            Tel = model.Tel,
            Adresse = model.Adresse,
            Mdp = BCrypt.Net.BCrypt.HashPassword(model.Mdp),
            RoleId = 3 // IdRole Client par défaut
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Utilisateur créé avec succès" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ConnexionModel.LoginModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Mdp, user.Mdp))
            return Unauthorized("Email ou mot de passe incorrect.");

        return Ok(new { message = "Connexion réussie", userId = user.Id });
    }
}
