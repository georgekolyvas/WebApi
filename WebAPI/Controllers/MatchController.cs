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
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            // changing Date / Time formats from sql server
            //foreach (var match in matches)
            //{
            //    match.MatchDate = DateTime.Parse(match.MatchDate.ToString("yyyy-MM-dd"));
            //    match.MatchTime = new TimeSpan(match.MatchTime.Hours, match.MatchTime.Minutes, 00);
            //}
            return matches;
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
        // Update an existing entity
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchById(int id, ApiMatch match)
        {                      
            if (id != match.Id)
            {
                return BadRequest("Parameter's Id and Object's Id must be the same!");
            }

            // get db Match object from the given id   
            var dbMatchUpdate = await _context.Matches.AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);

            // create the right format for time
            var matchtime = new TimeSpan(match.Hour, match.Minutes, 00);

            // pass the api object's values to db object
            dbMatchUpdate.Id = id;
            dbMatchUpdate.Description = match.Description;           
            dbMatchUpdate.MatchDate = match.MatchDate;  // acceptable format "yyyy/MM/dd/"
            dbMatchUpdate.MatchTime = matchtime;
            dbMatchUpdate.TeamA = match.TeamA;
            dbMatchUpdate.TeamB = match.TeamB;
            dbMatchUpdate.Sport = match.Sport; 
            
            // set the new entity 
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

        // POST: api/Match
        // Insert a new entity
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(ApiMatch match)
        {
            if (!MatchExists(match.Id))
            {
                var dbMatchInsert = new Match();
                // create the right format for time
                var matchtime = new TimeSpan(match.Hour, match.Minutes, 00);

                dbMatchInsert.Id = match.Id;
                dbMatchInsert.Description = match.Description;
                dbMatchInsert.MatchDate = match.MatchDate;  // acceptable format "yyyy/MM/dd/"
                dbMatchInsert.MatchTime = matchtime;
                dbMatchInsert.TeamA = match.TeamA;
                dbMatchInsert.TeamB = match.TeamB;
                dbMatchInsert.Sport = match.Sport;

                _context.Matches.Add(dbMatchInsert);
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostMatch", new { id = match.Id }, match);            
            }
            else
            {
                return UnprocessableEntity("This Id already exists!");
            }
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
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
