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
using smartch.PostModel.Validator;

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
            IEnumerable<Tournament> tournaments = _context.Tournaments
                .Include(c => c.Club)
                .Include(t=>t.Address)
                .Include(t=>t.Participants)
                .Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0);
            List<TournamentListDTO> tournamentDTO = new List<TournamentListDTO>();
            foreach (Tournament t in tournaments)
            {
                tournamentDTO.Add(new TournamentListDTO(t));
            }
            return tournamentDTO;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament(long id)
        {
            Account currentUser = await GetCurrentUserAsync();
            var tournaments = _context.Tournaments.Where( t => t.Id == id && t.Admins.Where(a=>a.Account == currentUser).Count()>0)
                .Include(t => t.Admins)
                .Include(t => t.Participants).ThenInclude(p => p.User)
                .Include(c => c.Club)
                .Include(t => t.Address)
                .Include(t => t.Matches).ThenInclude(m => m.Joueur1)
                .Include(t => t.Matches).ThenInclude(m => m.Joueur2)
                .Include(t => t.Matches).ThenInclude(m => m.Arbitre) 
                .Include(t => t.Matches).ThenInclude(m => m.Score);
            if (tournaments.Count() > 0) { 
                Tournament tournament = tournaments.Single<Tournament>();
                return Ok(new TournamentDTO(tournament));
            }
            else
            {
                List<Error> errors = new List<Error>() { new Error() {
                    Code = "TournamentUnknowOrUnAuthorize",
                    Description = "Le tournois n'as pas été trouvé ou vous n'avez pas l'authorisation"
                } };
                return BadRequest( errors);
            }
        }

        
        [HttpGet("{id}/participants")]
        public async Task<IActionResult> GetParticipantsAsync(long id)
        {
            Account currentUser = await GetCurrentUserAsync();
            var participants = _context.Tournaments
                .Where(t => t.Id == id && t.Admins.Where(a=>a.Account.Id == currentUser.Id).Count()>0)
                .Include(t => t.Participants).ThenInclude(p => p.User)
                .Select(t => t.Participants);
            if (participants.Count() > 0)
            { 
                var user = participants.First().ToList().Select(u=> new UserDTO(u.User));
                return Ok(user as IEnumerable<UserDTO>);
            }
            else
            {
                List<Error> errors = new List<Error>() { new Error() {
                    Code = "TournamentUnknowOrUnAuthorize",
                    Description = "Le tournois n'as pas été trouvé ou vous n'avez pas l'authorisation"
                } };
                return BadRequest(errors);
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody]TournamentListDTO tournament)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            TournamentAdmin tournamentAdmin = new TournamentAdmin() { Account = currentUser };
            Club club = null;
            if (tournament.ClubId > 0)
            {
                var rawClub = _context.Clubs
                    .Where(c => c.Id == tournament.ClubId && c.Admins.Where(a=>a.Account.Id == currentUser.Id).Count() > 0);

                if (rawClub.Count() <= 0)
                {
                    errors.Add( new Error() {
                        Code = "ClubUnknowOrUnAuthorize",
                        Description = "Le club n'as pas été trouvé ou vous n'avez pas l'authorisation"
                    } );
                    
                }
                else
                    club = rawClub.First();
            }
            else
            {
                errors.Add( new Error() {
                        Code = "ClubRequired",
                        Description = "Le club ne peut pas être vide"
                    } );
                
            }
            List<TournamentJoueur> participants = new List<TournamentJoueur>();
            foreach(long participantId in tournament.ParticipantsId){
                UserInfo user = _context.UserInfo.First(u => u.Id == participantId && u.CreatedBy.Id == currentUser.Id);
                if(user != null) { 
                    TournamentJoueur tournamentJoueur = new TournamentJoueur() { User =  user};
                    participants.Add(tournamentJoueur);
                }
                else
                {
                    errors.Add(new Error()
                    {
                        Code = "JoueurUnknowOrUnAuthorize",
                        Description = "Un joueur est introuvable ou vous n'avez pas accès"
                    });
                    
                }
            }
            errors = TournamentListDTOValidator.Validate(tournament, errors);
            if (errors.Count() > 0)
            {
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
                return Created("tournament/" + tournament.Id, tournament);
            }
            else
            {
                return BadRequest(errors);
            }

            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]TournamentDTO value)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            var rawTournament = _context.Tournaments
                .Where(t => t.Id == id && t.Admins.Where(a => a.Account == currentUser).Count() > 0);
            if (rawTournament.Count() > 0)
            {
                Tournament tournament = rawTournament.First();
                List<Match> matchs = new List<Match>();
                // enregistement des matchs par default
                foreach (MatchDTO match in value.Matches)
                {
                    Match newMatch = match.GetMacth();
                    var rawUser1 = _context.UserInfo.Where(u => u.Id == match.Joueur1Id && u.CreatedBy.Id == currentUser.Id);
                    var rawUser2 = _context.UserInfo.Where(u => u.Id == match.Joueur2Id && u.CreatedBy.Id == currentUser.Id);
                    if(rawUser1.Count()<=0 || rawUser2.Count() <= 0)
                    {
                        errors.Add(new Error()
                        {
                            Code = "JoueurUnknowOrUnAuthorize",
                            Description = "Un Joueur n'existe pas ou vous n'avez pas l'authorisation d'y accéder"
                        });
                    }
                    else
                    {
                        UserInfo user1 = rawUser1.First();
                        UserInfo user2 = _context.UserInfo.Where(u => u.Id == match.Joueur2Id && u.CreatedBy.Id == currentUser.Id).First();


                        newMatch.Joueur1 = user1;
                        newMatch.Joueur2 = user2;
                        newMatch.Arbitre = currentUser;
                        newMatch.Emplacement = "terrain 1";
                        matchs.Add(newMatch);
                    }
                    
                }
                errors = TournamentDTOValidator.Validate(value, errors);
               
                if (tournament != null)
                {
                    if (errors.Count() <= 0)
                    {

                        //tournament.Club = value.Club,
                        //tournament.Admins = value.Admins ;
                        tournament.Address = value.Address;
                        tournament.BeginDate = value.BeginDate;
                        tournament.EndDate = value.EndDate;
                        tournament.Etat = value.Etat;
                        tournament.Name = value.Name;
                        tournament.Matches = matchs;
                        //tournament.Participants = value.Participants;
                        _context.SaveChanges();
                        return Ok(new TournamentDTO(tournament));
                    }
                }
                else
                {
                    errors.Add(new Error()
                    {
                        Code = "TournamentUnknowOrUnAuthorize",
                        Description = "Un tournois n'existe pas ou vous n'avez pas l'authorisation d'y accéder"
                    });
                }
            }
            else
            {
                errors.Add(new Error()
                {
                    Code = "TournamentUnknowOrUnAuthorize",
                    Description = "Un tournois n'existe pas ou vous n'avez pas l'authorisation d'y accéder"
                });
            }
            return BadRequest(errors);

        }   

        [HttpPost("{idTournament}")]
        public async Task<IActionResult> AddMatch(int idTournament, long matchId, [FromBody]MatchDTO matchDto)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            errors = MatchDTOValidator.Validate(matchDto, errors);
            Match match = matchDto.GetMacth();
            
            var tournamentQuery = _context.Tournaments
                .Where(t => t.Id == idTournament && t.Admins.Where(a=>a.Account.Id == currentUser.Id).Count()>0);
            if (errors.Count() <= 0)
            {
                if (tournamentQuery.Count() > 0)
                {
                    Tournament tounrament = tournamentQuery.First();
                    tounrament.Matches.Add(match);
                    _context.SaveChanges();
                    return Created("", match);
                }
                else
                {
                    errors.Add(new Error()
                    {
                        Code = "TournamentUnknowOrUnAuthorize",
                        Description = "Un tournois n'existe pas ou vous n'avez pas l'authorisation d'y accéder"
                    });
                }
            }
            return BadRequest(errors);


            
        }

        [HttpPut("{idTournament}/matchs/{matchId}")]
        public async Task<IActionResult> UpdateMatch(int idTournament, long matchId, [FromBody]MatchDTO matchDTO)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            var tournamentQuery = _context.Tournaments
                .Where(t => t.Id == idTournament)
                .Include(t => t.Matches).ThenInclude(m => m.Joueur1)
                .Include(t => t.Matches).ThenInclude(m => m.Joueur2)
                .Include(t => t.Matches).ThenInclude(m => m.Arbitre).First();
            var matchQuery = tournamentQuery.Matches.Where(m => m.Id == matchId && m.Arbitre.Id == currentUser.Id);
            if (matchQuery.Count() <= 0)
            {
                Match matchInDb = matchQuery.First();

                Match match = matchDTO.GetMacth();
                //if (matchDTO.Arbitre != null)
                //    match.Arbitre = matchDTO.Arbitre;
                if (match.Joueur1 != null && match.Joueur1.Id != matchInDb.Joueur1.Id)
                {
                    UserInfo joueur1 = _context.UserInfo.Find(match.Joueur1.Id);
                    matchInDb.Joueur1 = joueur1;
                }

                if (match.Joueur2 != null && match.Joueur2.Id != matchInDb.Joueur2.Id)
                {
                    UserInfo joueur2 = _context.UserInfo.Find(match.Joueur2.Id);
                    matchInDb.Joueur2 = joueur2;
                }

                if (match.Phase > 0)
                    matchInDb.Phase = match.Phase;
                if (match.DebutPrevu != null)
                    matchInDb.DebutPrevu = match.DebutPrevu;
                if (match.Emplacement != null)
                    matchInDb.Emplacement = match.Emplacement;

                _context.SaveChanges();
                return Ok(matchInDb);
            }
            else
            {
                errors.Add(new Error()
                {
                    Code = "MatchUnknowOrUnAuthorize",
                    Description = "le match n'existe pas ou vous n'avez pas l'authorisation d'y accéder"
                });
            }
            return BadRequest(errors);
        }

       
    }
}
