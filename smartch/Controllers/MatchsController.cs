using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using model;
using smartch.PostModel;
using smartch.PostModel.Validator;

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
        public async Task<IActionResult> Get()
        {
            Account currentUser = await GetCurrentUserAsync();
            var rawMatch = _context.Tournaments
                .SelectMany(t => t.Matches)
                .Where(t => t.Arbitre == currentUser)
                .Include(m => m.Joueur1)
                .Include(m => m.Joueur2)
                .Include(m => m.Score);
           
                //.Select(m => new MatchDTO(m, new CalculPointPingPong()))
            return Ok(rawMatch.Select(m => new MatchDTO(m, null, new CalculPointPingPong())));
        }
        public async Task<UserInfo> AccountToUserInfo(Account account)
        {
            Account currentUser = await GetCurrentUserAsync();
            var user = _context.UserInfo.Include(u => u.Adresse).Where(u => u.CreatedBy == currentUser && u.Owner == account);
            return user.First();
        }

        [HttpPost("{idMatch}/point")]
        public async Task<IActionResult> PostPoint(long idMatch, [FromBody] PointDTO pointDto)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            var matchs = _context.Tournaments
                .SelectMany(t => t.Matches)
                .Where(t => t.Id == idMatch && t.Arbitre == currentUser)
                .Include(m => m.Score);
            errors = PointDTOValidator.Validate(pointDto, errors);
            if (matchs.Count() > 0 && errors.Count()<=0)
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
                Score score = new CalculPointPingPong().Calcul(match.Score);
                return Created("", score);
            }
            else if(matchs.Count() <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "MatchUnknowOrUnAuhthorize",
                    Description = "Le match n'as pas été trouver ou vous n'avez pas acces au match"
                });
                return BadRequest(errors);
            }
            return BadRequest(errors);
          
        }

        [HttpDelete("{idMatch}/point/{joueur}")]
        public async Task<IActionResult> DeletePoint(long idMatch,EJoueurs joueur)
        {
            List<Error> errors = new List<Error>();
            Account currentUser = await GetCurrentUserAsync();
            var matchs = _context.Tournaments
                .SelectMany(t => t.Matches)
                .Where(t => t.Id == idMatch && t.Arbitre == currentUser)
                .Include(m=>m.Score);
            if (matchs.Count() > 0)
            {
               
                Match match = matchs.Single();
                
                if (match.Score != null && match.Score.Count() > 0)
                {
                    Point lastPoint = match.Score.OrderByDescending(p => p.Order).Where(p=>p.Joueur == joueur).FirstOrDefault();
                    match.Score.Remove(lastPoint);
                    _context.SaveChanges();
                    Score score = new CalculPointPingPong().Calcul(match.Score);
             
                    return Ok(score);
                }
                else
                {
                    errors.Add(new Error()
                    {
                        Code = "NothingToDelete",
                        Description = "impossible de supprimer un point"
                    });
                    return BadRequest(errors);
                }
                
            }
            else
            {
                errors.Add(new Error()
                {
                    Code = "MatchUnknowOrUnAuhthorize",
                    Description = "Le match n'as pas été trouver ou vous n'avez pas acces au match"
                });
                return BadRequest(errors);
            }

            

        }
    }
}