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
using smartch.PostModel;

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
        public async Task<IEnumerable<TournamentListDTO>> Get()
        {
            
            Account currentUser = await GetCurrentUserAsync();
            IEnumerable<Tournament> tournaments = _context.Tournaments.Include(c => c.Club).Include(t=>t.Address).Include(t=>t.Participants).Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0);
            List<TournamentListDTO> tournamentDTO = new List<TournamentListDTO>();
            foreach (Tournament t in tournaments)
            {
                tournamentDTO.Add(new TournamentListDTO(t));
            }
            return tournamentDTO;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<TournamentDTO> GetAsync(long id)
        {
            Account currentUser = await GetCurrentUserAsync();
            var tournament = _context.Tournaments.Where(t=>t.Id == id).Include(c => c.Club).Include(t => t.Address).Include(t => t.Participants).Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0).Select(t=>new TournamentDTO(t));
            return tournament as TournamentDTO;
        }

        // POST api/<controller>
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody]TournamentListDTO tournament)
        {
            Account currentUser = await GetCurrentUserAsync();
            TournamentAdmin tournamentAdmin = new TournamentAdmin() { Account = currentUser };
            Club club = null;
            if (tournament.ClubId >= 0)
            {
                club = _context.Clubs.Where(c => c.ClubId == tournament.ClubId).First();
                if (club == null)
                {
                    return BadRequest("Club not exist");
                }  
            }
            List<TournamentJoueur> participants = new List<TournamentJoueur>();
            foreach(long participantId in tournament.ParticipantsId){
                TournamentJoueur tournamentJoueur = new TournamentJoueur() { User = _context.UserInfo.First(u=> u.Id == participantId) };
                participants.Add(tournamentJoueur);
            }
            Tournament newTournament = new Tournament()
            {
                Club = club,
                Admins = new List<TournamentAdmin>() { tournamentAdmin },
                Address = tournament.Address,
                BeginDate = tournament.BeginDate,
                EndDate = tournament.EndDate,
                Etat = tournament.Etat,
                Name = tournament.Name,
                Participants = participants
        };
           
            _context.Tournaments.Add(newTournament);
            _context.SaveChanges();

            tournament.Id = newTournament.Id;
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
