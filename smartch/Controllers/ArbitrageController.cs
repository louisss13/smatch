using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using model;

namespace smartch.Controllers
{
    [Produces("application/json")]
    [Route("api/arbitrage")]
    public class ArbitrageController : BaseController
    {
        public ArbitrageController(UserManager<Account> uMgr, SmartchDbContext dbContext) : base(uMgr, dbContext)
        {
        }
        [HttpGet]
        public async Task<IEnumerable<Match>> Get() {
            //Account currentUser = await GetCurrentUserAsync();
            Account currentUser = _context.Account.Where(a => a.UserName == "louisss13@gmail.com").First();
            return _context.Tournaments.SelectMany(t => t.Matches).Where(t => t.Arbitre == currentUser);
        }
    }
}