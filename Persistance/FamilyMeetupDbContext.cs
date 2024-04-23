using Family_Meetup.Models;
using Family_Meetup.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Family_Meetup.Persistance
{
    public class FamilyMeetupDbContext : DbContext 
    {
        public FamilyMeetupDbContext(DbContextOptions<FamilyMeetupDbContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new FamilyMeetupConfiguration());
            modelBuilder.ApplyConfiguration(new MeetupDateVoteOptionConfiguration());
        }
    }
}
