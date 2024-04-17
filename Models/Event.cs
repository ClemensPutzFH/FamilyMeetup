namespace Family_Meetup.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Comment
    {
        public Comment(string username, string comment)
        {
            this.username = username;
            this.comment = comment;
        }

        public string username { get;  }
        public string comment { get;  }
    }

    public class MeetupDateVoteOption
    {
        public MeetupDateVoteOption(DateTime date, List<string> votedusers)
        {
            this.date = date;
            this.votedusers = votedusers;
        }

        public DateTime date { get;  }
        public List<string> votedusers { get;  }
    }

    public class Event
    {
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

        public Guid id { get;  }
        public string title { get;  }
        public string creator { get;  }
        public string description { get;  }
        public string location { get;  }
        public DateTime creationdate { get;  }
        public int maxvotesondate { get;  }
        public int maxvotesbyuser { get;  }
        public List<string> userWhiteList { get; }
        public List<Comment> comments { get;  }
        public List<MeetupDateVoteOption> meetupdatevoteoptions { get;  }

    }

    

}
