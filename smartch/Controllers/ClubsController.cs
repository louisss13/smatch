using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using smartch.PostModel;
using System.Net.Mail;
using smartch.PostModel.Validator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smartch.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ClubsController : BaseController
    {
        public ClubsController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Account currentUser = await GetCurrentUserAsync();
            return Ok(_context.Clubs.Where(c => c.Admins.Where(a=>a.Account == currentUser).Count() > 0).Include(c => c.Tournaments).Include(c=>c.Adresse)); 
          
        }

        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> Get(int idUser)
        {
            Account currentUser = await GetCurrentUserAsync();
            List<Error> errors = new List<Error>();
            int isCorrectUser = _context.UserInfo.Where(u => u.Owner.Id == currentUser.Id && u.Id == idUser).Count();
            if(isCorrectUser <= 0)
            {
                errors.Add(new Error()
                {
                    Code="UserInexistantOrInaccessible",
                    Description="Cest utilisateur n'existe pas ou vous ne possédez pas les droits pour y accéder"
                });
                return BadRequest(errors);
            }
            var clubs = _context.Clubs.Where(c => c.Members.Where(m => m.UserInfo.Id == idUser).Count() > 0).Include(c=>c.Adresse).Select(c=>new ClubDTO(c)).ToList();
            
            return Ok(clubs);
        }
        
        // POST api/values
        [HttpPost ]
        public async Task<IActionResult> Post([FromBody]ClubDTO club)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            List<ClubAdmins> listClubAdmin = new List<ClubAdmins>() { new ClubAdmins() { Account = currentUser  } };
            List<ClubMember> membres = new List<ClubMember>();
            foreach (UserInfo user in club.Members)
            {
                var userRaw = _context.UserInfo.Where(u => u.Id == user.Id && u.CreatedBy.Id == currentUser.Id);
                if (userRaw.Count() > 0) { 
                    UserInfo userInfo = userRaw.First();
                    membres.Add(new ClubMember()
                    {
                        UserInfo = userInfo
                    });
                }
                else
                {
                    errors.Add(new Error() {
                        Code = "MembersUnAuthorizeOrUnknow",
                        Description="Un des membres que vous essayer d'ajouter n'existe pas ou vous n'êtes pas authoriser a y accéder"
                    });
                }
            }
            errors = ClubDaoValidator.Validate(club, errors);
            if (errors.Count <= 0) { 
                Club newClub = new Club
                {
                    Admins = listClubAdmin,
                    Members = membres, 
                    Adresse = club.Adresse,
                    ContactMail = club.ContactMail,
                    Name = club.Name,
                    Phone = club.Phone
                };

                _context.Clubs.Add(newClub);
                _context.SaveChanges();
                club.Id = newClub.Id;

                return Created("clubs",club );
            }
            return BadRequest(errors);
        }

        
    }
}
