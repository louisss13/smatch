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
using smartch.PostModel.Validator;

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
            IEnumerable<UserInfo> users2 = _context.Clubs.Include(c=>c.Tournaments).Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0).SelectMany(c => c.Members).Select(m => m.UserInfo);
           
            IEnumerable<UserInfo> returnUsers =  users.Union(users2);
            return returnUsers;
        }

        [HttpGet("account")]
        public async Task<IEnumerable<UserInfo>> GetAccount()
        {
            Account currentUser = await GetCurrentUserAsync();
            IEnumerable<UserInfo> users = _context.UserInfo.Include(u => u.Adresse).Where(u => u.CreatedBy == currentUser && u.Owner != null);
            IEnumerable<UserInfo> users2 = _context.Clubs.Where(c => c.Admins.Where(a => a.Account == currentUser).Count() > 0).SelectMany(c => c.Members).Select(m => m.UserInfo).Where(u=>u.Owner != null);

            IEnumerable<UserInfo> returnUsers = users.Union(users2);
            return returnUsers;
        }

        public async Task<UserInfo> AccountToUserInfo(Account account)
        {
            Account currentUser = await GetCurrentUserAsync();
            var user = _context.UserInfo.Include(u => u.Adresse).Where(u => u.CreatedBy == currentUser && u.Owner == account);
            return user.First();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            UserInfo user = _context.UserInfo.Find(id);
            if(user == null || (user.CreatedBy.Id != currentUser.Id && user.Owner.Id != currentUser.Id))
            {
                errors.Add(new Error()
                {
                    Code = "UserNotFoundOrUnAuthorize", 
                    Description = "l'utilisateur n'existe pas ou vous n'avez pas la permission d'y accéder"

                });
            }
            else
            {
                return Ok(user);
            }
                
            return BadRequest(errors);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInfo user)
        {
            List<Error> errors = new List<Error>();
            if (user == null) { return BadRequest(); }

            Account currentUser = await GetCurrentUserAsync();
            errors = UserInfoValidator.Validate(user, errors);
            if (errors.Count() <= 0)
            {
                user.CreatedBy = currentUser;
                _context.UserInfo.Add(user);
                _context.SaveChanges();
                var linkAccountRaw = _context.Account.Where(a => a.Email == user.Email);
                if (linkAccountRaw.Count() > 0)
                {
                    Account linkAccount = linkAccountRaw.Single();
                    if (linkAccount.Infos == null)
                    {
                        linkAccount.Infos = new List<UserInfo>();
                    }
                    linkAccount.Infos.Add(user);
                    _context.SaveChanges();
                }
                return Created("users/" + user.Id, user);
            }
            return BadRequest(errors);
        }

        
    }
}
