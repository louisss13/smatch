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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smartch.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ClubsController : BaseController
    {
        public ClubsController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Club>> Get()
        {
            Account currentUser = await GetCurrentUserAsync();

            return _context.Clubs.Include(c => c.Tournaments).Where(c => c.Admins.Where(a=>a.Account == currentUser).Count() > 0); 
          
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost ]
        public async Task<IActionResult> Post([FromBody]ClubDTO club)
        {

            Account currentUser = await GetCurrentUserAsync();
            List<ClubAdmins> listClubAdmin = new List<ClubAdmins>() { new ClubAdmins() { Account = currentUser  } };
            List<ClubMember> membres = new List<ClubMember>();
            foreach (UserInfo user in club.Members)
            {
                UserInfo userInfo = _context.UserInfo.Where(u => u.Id == user.Id).First();
                membres.Add(new ClubMember()
                {
                    UserInfo = userInfo
                });
            }
            /*
            foreach (UserInfo user in club.Admins)
            {
                ClubAdmins newClubAdmins = new ClubAdmins();
                newClubAdmins.Account = _context.UserInfo.Where(u => u.Id == user.Id).Select(u=>u.Account);
            }*/

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
            club.ClubId = newClub.ClubId;

            return Created("clubs",club );
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
