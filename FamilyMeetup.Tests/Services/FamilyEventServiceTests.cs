using Xunit;
using Moq;
using FamilyMeetup.Models.Services;
using FamilyMeetup.Persistance;
using FamilyMeetup.Models;
using System.Threading.Tasks;

public class FamilyEventServiceTests
{
    private readonly Mock<IFamilyEventService> _serviceMock;

    public FamilyEventServiceTests()
    {
        _serviceMock = new Mock<IFamilyEventService>();
    }

    [Fact]
    public async Task CreateEvent_ShouldReturnCreatedEvent()
    {
        // Arrange
        var newEvent = new Event { Name = "Family Gathering", Description = "Annual family gathering." };
        _serviceMock.Setup(service => service.CreateEvent(It.IsAny<Event>())).ReturnsAsync(newEvent);

        // Act
        var result = await _serviceMock.Object.CreateEvent(newEvent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Family Gathering", result.Name);
    }
}
