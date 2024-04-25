using Family_Meetup.FamilyEvents;
using Family_Meetup.Models;
using Family_Meetup.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Family_Meetup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyEventController : ControllerBase
    {

        private readonly IFamilyEventService _familyEventService;

        public FamilyEventController(IFamilyEventService familyEventService)
        {
            _familyEventService = familyEventService;
        }

        /*
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                    };
        */


        [HttpPut("/CreateEvent")]
        [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(CreateEventResponse))]
        public IActionResult CreateEvent(CreateEventRequest request)
        {

            var dateVoteOptions = new List<MeetupDateVoteOption>();

           foreach(DateTime dateOption in request.MeetupDateVoteOptions)
            {
                dateVoteOptions.Add(new MeetupDateVoteOption(dateOption,new List<string>(), Guid.NewGuid()));
            }

            var familyEvent = new Event(
                Guid.NewGuid(),
                request.Title,
                request.Creator,
                request.Description,
                request.Location,
                DateTime.Now,
                request.MaxVotesOnDates,
                request.MaxVotesByUser,
                new List<Comment>(),
                dateVoteOptions,
                request.UserWhitelist.ToList());

            _familyEventService.CreateEvent(familyEvent);


            return CreatedAtAction(
                nameof(CreateEvent), 
                new CreateEventResponse(Location: "/GetEvent/" + familyEvent.id.ToString()));
        }

        [HttpGet("/GetEvent/{id}")]
        [ProducesResponseType(typeof(GetEventResponse), 200)]
        public IActionResult GetEvent(Guid id, string username)
        {
            Event familyEvent = _familyEventService.getEvent(id, username);

            var dateVoteOptions = new List<DateTime>();

            foreach (MeetupDateVoteOption dateOption in familyEvent.meetupdatevoteoptions)
            {
                dateVoteOptions.Add(dateOption.date);
            }


            var response = new GetEventResponse(
                familyEvent.title,
                familyEvent.creator,
                familyEvent.description,
                familyEvent.location,
                familyEvent.creationdate,
                familyEvent.userWhiteList,
                familyEvent.maxvotesondate,
                familyEvent.maxvotesbyuser,
                dateVoteOptions,
                familyEvent.comments
                );
            return Ok(response);
        }

        [HttpGet("/GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<EventShort>))]
        public IActionResult GetAllEvent(string username, string searchUsername)
        {
            List<Event> familyEvents = _familyEventService.getAllEvents(username, searchUsername);

            List<EventShort> allShortEvents = new List<EventShort>();
            foreach (Event familyEvent in familyEvents)
            {
                allShortEvents.Add(new EventShort(familyEvent.id, familyEvent.title));
            }
            return Ok(allShortEvents);
        }


        [HttpPost]
        [Route("/VoteDate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult voteDate(Guid id, string username, [FromBody] VoteDateRequest voteDateRequest)
        {
            String respond = _familyEventService.voteDate(id, username, voteDateRequest);
            return Ok(respond);
        }

        [HttpPut]
        [Route("/AddComment/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult addComment(Guid id, string username, [FromBody] CommentRequest commentRequest)
        {
            String respond = _familyEventService.addComment(id, username, commentRequest.Comment);
            return Ok(respond);
        }

        /*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        */
    }
}