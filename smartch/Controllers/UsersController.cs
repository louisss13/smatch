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
        public IEnumerable<UserInfo> Get()
        {
            IEnumerable<UserInfo> users = _context.UserInfo;
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserInfo Get(int id)
        {
            UserInfo user = _context.UserInfo.Find(id);
            return user;
        }

        
        [HttpPost]
        public void Post([FromBody]UserInfo user)
        {
            if(user == null) { return;}
            _context.UserInfo.Add(user);
            _context.SaveChanges();
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
