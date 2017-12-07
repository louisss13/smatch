using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace smartch.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserInfoController : BaseController
    {
        public UserInfoController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<UserInfo>> Get()
        {
            Account currentUser = await GetCurrentUserAsync();
            IEnumerable<UserInfo> users = _context.UserInfo.Include(u => u.Adresse).Where(u=> u.CreatedBy == currentUser) ;
            IEnumerable<UserInfo> users2 = _context.Clubs.Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0).SelectMany(c => c.Members).Select(m => m.UserInfo);
           
            IEnumerable<UserInfo> returnUsers =  users.Union(users2);
            return returnUsers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserInfo Get(int id)
        {
            UserInfo user = _context.UserInfo.Find(id);
            return user;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInfo user)
        {
            if (user == null) { return BadRequest(); }
            Account currentUser = await GetCurrentUserAsync();
            user.CreatedBy = currentUser;
            _context.UserInfo.Add(user);
            _context.SaveChanges();
            
            return Created("users", user.Id);
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
