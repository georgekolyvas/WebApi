using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireHttps]
    public class MatchController : ControllerBase
    {
        private readonly FootballDataContext _context;

        public MatchController(FootballDataContext context)
        {
            _context = context;
        }

        // GET: api/Matches (all the matches)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();            
        }

        // Get: api/Matches/{id} (single match by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatchById(int id)
        {            
            var match = await _context.Matches.FindAsync(id); 

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Match/{id}        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchById(int id, Match match)
        {           

            if (id != match.Id)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }

        // POST: api/Match        
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMatch", new { id = match.Id }, match);
        }

        // DELETE: api/Match/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
