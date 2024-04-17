using Family_Meetup.Models;

namespace Family_Meetup.FamilyEvents
{
    public record GetEventResponse(
        string Title,
        string Creator,
        string Description,
        string Location,
        DateTime CreationDate,
        List<string> UserWhitelist,
        int MaxVotesOnDates,
        int MaxVotesByUser,
        List<DateTime> MeetupDateVoteOptions,
        List<Comment> Comments
        );

    public record DateOptionWithUserVotes(
        DateTime Date,
        List<string> VotedUsers
        );
}
