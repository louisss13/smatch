using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace model
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public UserContext(DbContextOptions options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubAdmin>().HasKey(x => new { x.ClubId, x.UserId });
            modelBuilder.Entity<ClubMember>().HasKey(x => new { x.ClubId, x.UserId });
            modelBuilder.Entity<TournamentAdmin>().HasKey(x => new { x.TournamentId, x.UserId });
            modelBuilder.Entity<TournamentJoueur>().HasKey(x => new { x.TournamentId, x.UserId });
        }
    }
}
