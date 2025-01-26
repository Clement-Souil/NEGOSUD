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
    public class TypeMouvementsController : ControllerBase
    {
        private readonly NegosudContext _context;

        public TypeMouvementsController(NegosudContext context)
        {
            _context = context;
        }

        // GET: api/TypeMouvements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeMouvement>>> GetTypeMouvements()
        {
            return await _context.TypeMouvements.ToListAsync();
        }

        // GET: api/TypeMouvements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeMouvement>> GetTypeMouvement(int id)
        {
            var typeMouvement = await _context.TypeMouvements.FindAsync(id);

            if (typeMouvement == null)
            {
                return NotFound();
            }

            return typeMouvement;
        }

        // PUT: api/TypeMouvements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeMouvement(int id, TypeMouvement typeMouvement)
        {
            if (id != typeMouvement.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeMouvement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeMouvementExists(id))
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

        // POST: api/TypeMouvements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeMouvement>> PostTypeMouvement(TypeMouvement typeMouvement)
        {
            _context.TypeMouvements.Add(typeMouvement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeMouvement", new { id = typeMouvement.Id }, typeMouvement);
        }

        // DELETE: api/TypeMouvements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeMouvement(int id)
        {
            var typeMouvement = await _context.TypeMouvements.FindAsync(id);
            if (typeMouvement == null)
            {
                return NotFound();
            }

            _context.TypeMouvements.Remove(typeMouvement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeMouvementExists(int id)
        {
            return _context.TypeMouvements.Any(e => e.Id == id);
        }
    }
}
