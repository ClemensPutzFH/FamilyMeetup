namespace Family_Meetup.FamilyEvents
{
    public record CreateEventRequest(
        string Title,
        string Creator,
        string Description,
        string Location,
        List<string> UserWhitelist,
        int MaxVotesOnDates,
        int MaxVotesByUser,
        List<DateTime> MeetupDateVoteOptions
        );

    public record CreateEventResponse(
        string Location
        );
}
