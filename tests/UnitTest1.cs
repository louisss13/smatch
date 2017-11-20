using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using model;
using System.Threading.Tasks;

namespace tests
{
    [TestClass]
    public class UnitTestSmartch
    {
        UserContext _context;

        [TestInitialize]
        public void InitTestUser()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            DbContextOptions options = builder.UseSqlServer(@"Data Source:ADRESSE_SERVEUR;Initial Catalog = VOTRE_DB; User Id = ; Password = ").Options;

            _context = new UserContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Users.Add(new User() { });
            _context.SaveChanges();

        }
        [TestMethod]
        public async Task TestUser() {
            User user = await _context.Users.FirstAsync();
            Assert.AreEqual("", user.PostCode);

        }
    }
}
