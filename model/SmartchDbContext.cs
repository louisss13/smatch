using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace model
{
    public class SmartchDbContext :  IdentityDbContext<Account>
    {
        
        public DbSet<Club> Clubs { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Account> Account { get; set; }
        //public DbSet<Match> Matchs { get; set; }


        public SmartchDbContext(DbContextOptions options) : base(options) {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Joueur1)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Joueur2)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Point>()
            .HasIndex(p => new { p.MatchId, p.Order }).IsUnique();

            modelBuilder.Entity<ClubAdmins>().HasKey(x => new { x.ClubId, x.AccountId });
            
            modelBuilder.Entity<ClubMember>().HasKey(x => new { x.ClubId, x.UserInfoId });
            modelBuilder.Entity<TournamentAdmin>().HasKey(x => new { x.TournamentId, x.AccountId });
            modelBuilder.Entity<TournamentJoueur>().HasKey(x => new { x.TournamentId, x.UserInfoId });
        }
    }
}
