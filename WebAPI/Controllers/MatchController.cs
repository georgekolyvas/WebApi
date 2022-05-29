using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ApiModels;

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
        public async Task<ActionResult<IEnumerable<DbMatch>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();            
        }

        // Get: api/Matches/{id} (single match by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<DbMatch>> GetMatchById(int id)
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
        public async Task<IActionResult> PutMatchById(int id, ApiMatch match)
        {
            // Validate Input


            //           
            if (id != match.Id)
            {
                return BadRequest();
            }

            // get dbmatch object from the given id
            var dbMatchUpdate = _context.Matches.AsNoTracking()
                .Single(q => q.Id == id);

            // create the right format for timestamp
            var matchtime = new TimeSpan(match.Hour, match.Minutes, 00);
            

            dbMatchUpdate.Description = match.Description;

            // accepteble format "YYYYY-MM-dd"
            dbMatchUpdate.MatchDate = match.MatchDate;
            dbMatchUpdate.MatchTime = matchtime;
            dbMatchUpdate.TeamA = match.TeamA;
            dbMatchUpdate.TeamB = match.TeamB;
            dbMatchUpdate.Sport = match.Sport;

            //.Select(q => new DbMatch
            //{
            //    dbMatchUpdate.Id = q.Id,
            //    dbMatchUpdate.Description = match.Description,
            //    dbMatchUpdate.MatchDate = match.MatchDate,
            //    dbMatchUpdate.MatchTime = TimeSpan.FromHours(12),
            //    dbMatchUpdate.TeamA = match.TeamA,
            //    dbMatchUpdate.TeamB = match.TeamB,
            //    dbMatchUpdate.Sport = match.Sport
            //});

            //_context.Entry(match).State = EntityState.Modified;

            _context.Entry(dbMatchUpdate).State = EntityState.Modified;

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
        public async Task<ActionResult<DbMatch>> PostMatch(DbMatch match)
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
