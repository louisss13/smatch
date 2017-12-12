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
           // return new Club[] { new Club() { Name="TEnnis1"}, new Club() { Name = "Foootttt1" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Club club)
        {
            Account currentUser = await GetCurrentUserAsync();
            if (club.Admins == null)
                club.Admins = new List<ClubAdmins>();
            ClubAdmins clubAdmins = new ClubAdmins
            {
                Account = currentUser
            };
            club.Admins.Add(clubAdmins);
            _context.Clubs.Add(club);
            _context.SaveChanges();
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
