using System.ComponentModel.DataAnnotations;

namespace Family_Meetup.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Comment
    {
        public Comment() { }
        public Comment(string username, string comment)
        {
            this.username = username;
            this.comment = comment;
        }

        [Key]
        public Guid Id { get; set; }

        public string username { get; private set;  }
        public string comment { get; private set;  }
    }

    public class MeetupDateVoteOption
    {
        public MeetupDateVoteOption() { }
        public MeetupDateVoteOption(DateTime date, List<string> votedusers, Guid id)
        {
            this.Id = Id;
            this.date = date;
            this.votedusers = votedusers;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime date { get; private set;  }
        public List<string> votedusers { get; private set;  }
    }

    public class Event
    {
        public Event() { }
        public Event(Guid id, string title, string creator, string description, string location, DateTime creationdate, int maxvotesondate, int maxvotesbyuser, List<Comment> comments, List<MeetupDateVoteOption> meetupdatevoteoptions, List<string> userWhiteList)
        {
            this.id = id;
            this.title = title;
            this.creator = creator;
            this.description = description;
            this.location = location;
            this.creationdate = creationdate;
            this.maxvotesondate = maxvotesondate;
            this.maxvotesbyuser = maxvotesbyuser;
            this.comments = comments;
            this.meetupdatevoteoptions = meetupdatevoteoptions;
            this.userWhiteList = userWhiteList;
        }

        public const int maxTitleLength = 200;
        [Key]
        public Guid id { get; private set;  }
        public string title { get; private set;  }
        public string creator { get; private set;  }
        public string description { get; private set;  }
        public string location { get; private set;  }
        public DateTime creationdate { get; private set;  }
        public int maxvotesondate { get; private set;  }
        public int maxvotesbyuser { get; private set;  }
        public List<string> userWhiteList { get; private set; }
        public List<Comment> comments { get; private set;  }
        public List<MeetupDateVoteOption> meetupdatevoteoptions { get; private set;  }

    }

    

}
