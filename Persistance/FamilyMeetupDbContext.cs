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
        public DbSet<MeetupDateVoteOption> MeetupDateVoteOptions { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Event>().Navigation(e => e.meetupdatevoteoptions)
                .AutoInclude()
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Event>().Navigation(e => e.comments)
                .AutoInclude()
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.ApplyConfiguration(new FamilyMeetupConfiguration());
            modelBuilder.ApplyConfiguration(new MeetupDateVoteOptionConfiguration());
        }
    }
}
