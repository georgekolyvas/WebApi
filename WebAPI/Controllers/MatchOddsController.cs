using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ApiModels;

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
        // Update an existing entity
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchOddById(int id, ApiMatchOdd matchOdd)
        {

            if (id != matchOdd.Id)
            {
                return BadRequest("Parameter's Id and Object's Id must be the same!");
            }

            // get dbMatchOdd object from the given id   
            var dbMatchOddUpdate = await _context.MatchOdds.AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
          
            dbMatchOddUpdate.Id = id;           
            dbMatchOddUpdate.MatchId = matchOdd.Id;
            dbMatchOddUpdate.Specifier = matchOdd.Specifier;
            dbMatchOddUpdate.Odd = matchOdd.Odd;

            // set the entity
            _context.Entry(dbMatchOddUpdate).State = EntityState.Modified;

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

        // POST: api/MatchOdd
        // Insert a new entity
        [HttpPost]
        public async Task<ActionResult<DbMatch>> PostMatchOdd(DbMatchOdd matchOdd)
        {
            _context.MatchOdds.Add(matchOdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMatchOdd", new { id = matchOdd.Id }, matchOdd);
        }

        private bool MatchOddExists(int id)
        {
            return _context.MatchOdds.Any(e => e.Id == id);
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

