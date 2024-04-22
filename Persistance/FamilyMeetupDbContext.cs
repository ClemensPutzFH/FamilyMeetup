using Family_Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Family_Meetup.Persistance
{
    public class FamilyMeetupDbContext : DbContext 
    {
        public FamilyMeetupDbContext(DbContextOptions<FamilyMeetupDbContext> options)
        {

        }

        public DbSet<Event> Events { get; set; } = null!;
    }
}
