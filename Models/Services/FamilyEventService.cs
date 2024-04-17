using Family_Meetup.FamilyEvents;

namespace Family_Meetup.Models.Services
{
    public class FamilyEventService : IFamilyEventService
    {
        private static readonly Dictionary<Guid, Event> _events = new Dictionary<Guid, Event>();

        public string addComment(Guid id, string username, string comment)
        {

            if(_events[id].userWhiteList.Count != 0 && !_events[id].userWhiteList.Contains(username))
            {
                return "Error user not in whitelist";
            }

            _events[id].comments.Add(new Comment(username, comment));

            return "Comment Added";
        }

        public void CreateEvent(Event familyEvent)
        {
            _events.Add(familyEvent.id, familyEvent);
        }

        public List<Event> getAllEvents(string username, string searchUsername)
        {
            return _events.Values.Where(familyEvent => {

                if (familyEvent.userWhiteList.Count != 0 && !familyEvent.userWhiteList.Contains(username))
                {
                    return false;
                }

                return familyEvent.creator.Equals(username); 
            
            }).ToList();

        }

        public Event getEvent(Guid id, string username)
        {
            if (_events[id].userWhiteList.Count != 0 && !_events[id].userWhiteList.Contains(username))
            {
                return null;
            }

            return _events[id];
        }

        public string voteDate(Guid id, string username, VoteDateRequest voteDateRequest)
        {

            if (_events[id].userWhiteList.Count != 0 && !_events[id].userWhiteList.Contains(username))
            {
                return "Error user not in whitelist";
            }

            int userCount = 0;
            try
            {
                userCount = _events[id].meetupdatevoteoptions.Where(dateVoteOption => dateVoteOption.votedusers.Contains("username")).Count();
            }
            catch (Exception ex)
            { }


            if (userCount >= _events[id].maxvotesbyuser)
            {
                return "Vote not added. User has its max votes reached";
            }

            foreach (DateTime date in voteDateRequest.Dates)
            {
                int usersPerDateCount = 0;
                try
                {
                    usersPerDateCount = _events[id].meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Count;
                }
                catch (Exception ex) { }


                if (_events[id].meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Count >= _events[id].maxvotesondate)
                {
                    return "Vote not added. Date(s) has reachd its max votes on";
                }

            }

            foreach (DateTime date in voteDateRequest.Dates)
            {
                try
                {
                    _events[id].meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return "Error. At least one date not found. No user votes have been counted";
                }
            }

            foreach (DateTime date in voteDateRequest.Dates)
            {

                _events[id].meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Add(username);

            }

            return "Success. Vote(s) added";
        }
    }
}
