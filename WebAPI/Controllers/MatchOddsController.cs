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
    public class MatchOddsController : ControllerBase
    {
        private readonly FootballDataContext _context;

        public MatchOddsController(FootballDataContext context)
        {
            _context = context;
        }

        // GET: api/MatchOdds (all the MatchOdds)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbMatchOdd>>> GetMatchOdds()
        {
            return await _context.MatchOdds.ToListAsync();
        }

        // Get: api/MatchOdd/{id} (single MatchOdd by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<DbMatchOdd>> GetMatchOddById(int id)
        {
            var matchOdds = await _context.MatchOdds.FindAsync(id);

            if (matchOdds == null)
            {
                return NotFound();
            }

            return matchOdds;
        }

        // PUT: api/MatchOdd/{id}        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchOddById(int id, DbMatchOdd matchOdd)
        {

            if (id != matchOdd.Id)
            {
                return BadRequest();
            }

            _context.Entry(matchOdd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchOddExists(id))
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

        private bool MatchOddExists(int id)
        {
            return _context.MatchOdds.Any(e => e.Id == id);
        }

        // POST: api/MatchOdd        
        [HttpPost]
        public async Task<ActionResult<DbMatch>> PostMatchOdd(DbMatchOdd matchOdd)
        {
            _context.MatchOdds.Add(matchOdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMatchOdd", new { id = matchOdd.Id }, matchOdd);
        }

        // DELETE: api/MatchOdd/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchOdd(int id)
        {
            var matchOdd = await _context.MatchOdds.FindAsync(id);
            if (matchOdd == null)
            {
                return NotFound();
            }

            _context.MatchOdds.Remove(matchOdd);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

