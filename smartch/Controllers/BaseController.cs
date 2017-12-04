using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using model;

namespace smartch.Controllers
{
    public abstract class BaseController : Controller
    {
        private UserManager<Account> _uMgr;
        protected SmartchDbContext _context;
        public BaseController(UserManager<Account> uMgr, SmartchDbContext dbContext)
        {
            _context = dbContext;
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _uMgr = uMgr;
        }
        protected async Task<Account> GetCurrentUserAsync()
        {
            if (this.HttpContext.User == null)
                throw new Exception("L'utilisateur n'est pas identifié");
            Claim userNameClaim = this.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userNameClaim == null)
                throw new Exception("Le token JWT semble ne pas avoir été interprété correctement");
            return await _uMgr.FindByNameAsync(userNameClaim.Value);
        }
    }
}