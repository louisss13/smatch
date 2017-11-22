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
            DbContextOptions options = builder.UseSqlServer(@"Data Source=smartchserver.database.windows.net;Initial Catalog = smartchDb; User Id = louisdeMahieu; Password = DeSmarch$MahiLoui_*").Options;

            _context = new UserContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Users.Add(new User() {
                FirstName = "Louis"
            });
            _context.SaveChanges();

        }
        [TestMethod]
        public async Task TestUser() {
            User user = await _context.Users.FirstAsync();
            Assert.AreEqual("Louis", user.FirstName);

        }
    }
}
