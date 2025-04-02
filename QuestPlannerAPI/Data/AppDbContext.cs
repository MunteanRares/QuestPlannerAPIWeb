using Microsoft.EntityFrameworkCore;

namespace QuestPlannerAPI.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Itineraries> Itinieraries { get; set; }
        public DbSet<Days> Days { get; set; }
        public DbSet<Activities> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Itineraries>().HasOne(i => i.User).WithMany(i => i.Itineraries).HasForeignKey(i => i.UsersId);
            modelBuilder.Entity<Days>().HasOne(i => i.Itinerary).WithMany(i => i.Days).HasForeignKey(i => i.ItinerariesId);
            modelBuilder.Entity<Activities>().HasOne(i => i.Day).WithMany(i => i.Activities).HasForeignKey(i => i.DaysId);
        }
    }
}
