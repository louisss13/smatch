using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using model;
using Microsoft.AspNetCore.Identity;
using smartch.PostModel;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace smartch.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private UserManager<Account> _userManager;
        public AccountController(UserManager<Account> userManager, SmartchDbContext dbContext): base(userManager,  dbContext)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {
            ICollection<UserInfo> profils = _context.UserInfo.Where(u => u.Email == dto.Email.ToLower()).ToList();
            var newAccount = new Account
            {
                UserName = dto.Email.ToLower(),
                Email = dto.Email,
                DateInscription = DateTime.Now,
                DateDerniereConnection = DateTime.Now,
                Active = 1, 
                Infos = profils
            };
            IdentityResult result = await _userManager.CreateAsync(newAccount, dto.Password);
            
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            
            return Created("", new AccountDTO(newAccount));
        }
        [HttpGet]
        public async Task<AccountDTO> GetAccount()
        {
            Account currentUser = await GetCurrentUserAsync();
            var completeCurrentUserRaw = _context.Account.Where(a => a.Id == currentUser.Id).Include(a=>a.Infos);
            if(completeCurrentUserRaw.Count() > 0)
            {
                Account completeCurrentUser = completeCurrentUserRaw.Single();
                AccountDTO accountDTO = new AccountDTO(completeCurrentUser);
                return accountDTO;
            }
            
            return new AccountDTO(currentUser);
        }
    }
   
}