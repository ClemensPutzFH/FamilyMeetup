using Family_Meetup.FamilyEvents;

namespace Family_Meetup.Models.Services
{
    public interface IFamilyEventService
    {
        void CreateEvent(Event familyEvent);
        Event getEvent(Guid id, string username);
        List<Event> getAllEvents(string username, string searchUsername);
        string voteDate(Guid id, string username, VoteDateRequest voteDateRequest);
        string addComment(Guid id, string username, string comment);
    }
}
