using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using model;
using smartch.PostModel;

namespace smartch.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MatchsController : BaseController
    {
        public MatchsController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {
        }

        [HttpGet("arbitrage")]
        public async Task<IEnumerable<Match>> Get()
        {
            Account currentUser = await GetCurrentUserAsync();
            return _context.Tournaments.SelectMany(t => t.Matches).Where(t => t.Arbitre == currentUser);
        }
        
    }
}