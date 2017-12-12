using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smartch.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TournamentsController : BaseController
    {
        public TournamentsController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Tournament>> Get()
        {
            
            Account currentUser = await GetCurrentUserAsync();
            IEnumerable<Tournament> tournaments = _context.Tournaments.Include(c => c.Club).Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0);
            return tournaments;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody]Tournament tournament)
        {
            if(tournament.Club != null)
            {
                Club club = _context.Clubs.Where(c => c.ClubId == tournament.Club.ClubId).First();
                if(club == null)
                {
                    return BadRequest("Club not exist");
                }
                tournament.Club = club;
            }
            Account currentUser = await GetCurrentUserAsync();
            TournamentAdmin tournamentAdmin = new TournamentAdmin() { Account = currentUser };

            tournament.Admins = new List<TournamentAdmin>();
            tournament.Admins.Add(tournamentAdmin);



            _context.Tournaments.Add(tournament);
            _context.SaveChanges();
            return Created("tournament/"+tournament.Id, tournament);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
