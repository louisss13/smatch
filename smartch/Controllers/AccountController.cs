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

namespace smartch.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private UserManager<Account> _userManager;
        public AccountController(UserManager<Account> userManager, SmartchDbContext dbContext): base(userManager,  dbContext)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {

            var newAccount = new Account
            {
                UserName = dto.Email.ToLower(),
                Email = dto.Email,
                DateInscription = DateTime.Now,
                DateDerniereConnection = DateTime.Now,
                Active = 1
            };
            IdentityResult result = await _userManager.CreateAsync(newAccount, dto.Password);
            
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            
            return Created("test", newAccount);
        }
    }
   
}