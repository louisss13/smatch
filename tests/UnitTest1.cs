using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using model;
using System.Threading.Tasks;

namespace tests
{
    [TestClass]
    public class UnitTestSmartch
    {
        SmartchDbContext _context;

        [TestInitialize]
        public void InitTestUser(SmartchDbContext dbContext)
        {
           
            _context = dbContext;

           // _context.Database.EnsureDeleted();
           // _context.Database.EnsureCreated();
            //_context.Users.Add(new User() {
             //   FirstName = "Louis"
            //});
            //_context.SaveChanges();

        }
        [TestMethod]
        public async Task TestUser() {
            //User user = await _context.Users.FirstAsync();
            //Assert.AreEqual("Louis", user.FirstName);

        }
    }
}
