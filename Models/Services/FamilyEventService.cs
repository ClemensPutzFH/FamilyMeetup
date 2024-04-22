using Family_Meetup.FamilyEvents;
using Family_Meetup.Persistance;

namespace Family_Meetup.Models.Services
{
    public class FamilyEventService : IFamilyEventService
    {
        private readonly FamilyMeetupDbContext _dbContext;

        public FamilyEventService(FamilyMeetupDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public string addComment(Guid id, string username, string comment)
        {
            Event addCommentEvent = _dbContext.Events.Find(id);
            if (addCommentEvent.userWhiteList.Count != 0 && !addCommentEvent.userWhiteList.Contains(username))
            {
                return "Error user not in whitelist";
            }

            addCommentEvent.comments.Add(new Comment(username, comment));

            _dbContext.Events.Update(addCommentEvent);
            _dbContext.SaveChanges();

            return "Comment Added";
        }

        public void CreateEvent(Event familyEvent)
        {
            _dbContext.Add(familyEvent);
            _dbContext.SaveChanges();
        }

        public List<Event> getAllEvents(string username, string searchUsername)
        {
            return _dbContext.Events.Where(familyEvent => filterEventByUsername(familyEvent,username)).ToList();

        }

        public Event getEvent(Guid id, string username)
        {
            Event getEvent = _dbContext.Events.Find(id);
            
            if (getEvent.userWhiteList.Count != 0 && !getEvent.userWhiteList.Contains(username))
            {
                return null;
            }

            return getEvent;
        }

        public string voteDate(Guid id, string username, VoteDateRequest voteDateRequest)
        {
            Event voteDateEvent = _dbContext.Events.Find(id);

            if (voteDateEvent.userWhiteList.Count != 0 && !voteDateEvent.userWhiteList.Contains(username))
            {
                return "Error user not in whitelist";
            }

            int userCount = 0;
            try
            {
                userCount = voteDateEvent.meetupdatevoteoptions.Where(dateVoteOption => dateVoteOption.votedusers.Contains("username")).Count();
            }
            catch (Exception ex)
            { }


            if (userCount >= voteDateEvent.maxvotesbyuser)
            {
                return "Vote not added. User has its max votes reached";
            }

            foreach (DateTime date in voteDateRequest.Dates)
            {
                int usersPerDateCount = 0;
                try
                {
                    usersPerDateCount = voteDateEvent.meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Count;
                }
                catch (Exception ex) { }


                if (voteDateEvent.meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Count >= voteDateEvent.maxvotesondate)
                {
                    return "Vote not added. Date(s) has reachd its max votes on";
                }

            }

            foreach (DateTime date in voteDateRequest.Dates)
            {
                try
                {
                    voteDateEvent.meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return "Error. At least one date not found. No user votes have been counted";
                }
            }

            foreach (DateTime date in voteDateRequest.Dates)
            {

                voteDateEvent.meetupdatevoteoptions.Where(dateVoteOption => 0 == dateVoteOption.date.CompareTo(date)).FirstOrDefault().votedusers.Add(username);

            }

            _dbContext.Events.Update(voteDateEvent);
            _dbContext.SaveChanges();

            return "Success. Vote(s) added";
        }

        private bool filterEventByUsername(Event testEvent, string username)
        {
            {

                if (testEvent.userWhiteList.Count != 0 && !testEvent.userWhiteList.Contains(username))
                {
                    return false;
                }

                return testEvent.creator.Equals(username);

            }
        }
    }
}
