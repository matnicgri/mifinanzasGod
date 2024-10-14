using Microsoft.EntityFrameworkCore;
using Mifinanzas.God.Domain.Entitites;

namespace Mifinanazas.God.Persistence.Context
{
    public class GodDbContext : DbContext
    {
        public GodDbContext(DbContextOptions<GodDbContext> options) : base(options)
        { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<MoveOptions> MoveOptions { get; set; }
        public DbSet<Movements> Movements { get; set; }
        public DbSet<Totals> Totals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(g => g.player1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne<Player>()
                .WithMany()
                .HasForeignKey(g => g.player2Id)
                .OnDelete(DeleteBehavior.Restrict);                       
        }

    }
}
