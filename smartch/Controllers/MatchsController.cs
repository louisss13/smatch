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
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<MatchDTO>> Get()
        {
            Account currentUser = await GetCurrentUserAsync();
            return _context.Tournaments.SelectMany(t => t.Matches).Where(t => t.Arbitre == currentUser).Include(m=>m.Joueur1).Include(m => m.Joueur2).Include(m=>m.Score).Select(m=> new MatchDTO(m));
        }
        [HttpPost("{idMatch}/point")]
        public async Task<Match> PostPoint(long idMatch, [FromBody] PointDTO pointDto)
        {
            Account currentUser = await GetCurrentUserAsync();
            var matchs = _context.Tournaments.SelectMany(t => t.Matches).Where(t => t.Id == idMatch).Include(m => m.Score);
            if (matchs.Count() > 0)
            {
                Point point = pointDto.GetPoint();
                Match match = matchs.Single();
                int maxOrder;
                if (match.Score != null && match.Score.Count()>0)
                {
                    maxOrder = match.Score.Max(p => p.Order);
                }
                else
                {
                    match.Score = new List<Point>();
                    maxOrder = 0;
                }
                
                point.Order = maxOrder + 1;
                match.Score.Add(point);
                _context.SaveChanges();
                return match;
            }
            else
            {
                return null;
            }
          
        }
        [HttpDelete("{idMatch}/point/{joueur}")]
        public async Task<IActionResult> DeletePoint(long idMatch,EJoueurs joueur)
        {
            Account currentUser = await GetCurrentUserAsync();
            var matchs = _context.Tournaments.SelectMany(t => t.Matches).Where(t => t.Id == idMatch).Include(m=>m.Score);
            if (matchs.Count() > 0)
            {
               
                Match match = matchs.Single();
                
                if (match.Score != null && match.Score.Count() > 0)
                {
                    Point lastPoint = match.Score.OrderByDescending(p => p.Order).Where(p=>p.Joueur == joueur).FirstOrDefault();
                    match.Score.Remove(lastPoint);
                    _context.SaveChanges();
                    return Ok();
                }
                
            }
            return BadRequest();

        }
    }
}