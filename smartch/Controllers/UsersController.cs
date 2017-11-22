using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using Microsoft.EntityFrameworkCore;

namespace smartch.Controllers
{
    

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        UserContext _context;
        public UsersController()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            DbContextOptions options = builder.UseSqlServer(@"Data Source=smartchserver.database.windows.net;Initial Catalog = smartchDb; User Id = louisdeMahieu; Password = DeSmarch$MahiLoui_*").Options;

            _context = new UserContext(options);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = _context.Users;
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User user = _context.Users.Find(id);
            return user;
        }

        
        [HttpPost]
        public void Post([FromBody]User user)
        {
            _context.Users.Add(user);
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
