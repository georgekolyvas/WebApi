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
        public async Task<ActionResult<IEnumerable<MatchOdd>>> GetMatchOdds()
        {
            return await _context.MatchOdds.ToListAsync();
        }

        // Get: api/MatchOdd/{id} (single MatchOdd by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchOdd>> GetMatchOddById(int id)
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

            // get db MatchOdd object from the given id   
            var dbMatchOddUpdate = await _context.MatchOdds.AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);

            // pass the api object's values to db object 
            dbMatchOddUpdate.Id = id;           
            dbMatchOddUpdate.MatchId = matchOdd.MatchId;
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
        public async Task<ActionResult<MatchOdd>> PostMatchOdd(ApiMatchOdd matchOdd)
        {
            if (!MatchOddExists(matchOdd.Id))
            {
                var dbMatchOddInsert = new MatchOdd();
                dbMatchOddInsert.Id = matchOdd.Id;
                dbMatchOddInsert.MatchId = matchOdd.MatchId;
                dbMatchOddInsert.Specifier = matchOdd.Specifier;
                dbMatchOddInsert.Odd = matchOdd.Odd;

                _context.MatchOdds.Add(dbMatchOddInsert);
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostMatchOdd", new { id = matchOdd.Id }, matchOdd);
            }
            else
            {
                return UnprocessableEntity("This Id already exists!");
            }
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

