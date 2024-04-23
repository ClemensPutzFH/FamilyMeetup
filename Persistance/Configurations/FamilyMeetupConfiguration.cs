using Family_Meetup.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family_Meetup.Persistance.Configurations
{
   public class FamilyMeetupConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(b => b.title).HasMaxLength(Event.maxTitleLength);
            builder.Property(b => b.userWhiteList).HasConversion(
                v => string.Join(",", v),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }

    public class MeetupDateVoteOptionConfiguration : IEntityTypeConfiguration<MeetupDateVoteOption>
    {
        public void Configure(EntityTypeBuilder<MeetupDateVoteOption> builder)
        {
            builder.Property(b => b.votedusers).HasConversion(
                v => string.Join(",", v),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}
