using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.SpaceHandlerTests;

[TestClass]
public sealed class CreateSpaceHandlerTests
{
    private CreateSpaceCommand _invalidCommand;
    private CreateSpaceCommand _validCommandWithoutRooms;
    private CreateSpaceCommand _validCommandWithRooms;
    private SpaceHandler _spaceHandler;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateSpaceCommand("", new List<string>());
        _validCommandWithoutRooms = new CreateSpaceCommand("Space One", new List<string>());
        _validCommandWithRooms = new CreateSpaceCommand("Space Two", new List<string> { "Room A", "Room B" });

        _spaceHandler = new SpaceHandler(new FakeSpaceRepository());
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_stop_the_application()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success, "The space creation should fail for an invalid command.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_create_a_space_without_rooms()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_validCommandWithoutRooms);
        Assert.IsTrue(result.Success, $"Expected success, but failed with message: {result.Message}");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_create_a_space_with_rooms()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_validCommandWithRooms);
        Assert.IsTrue(result.Success, "Space should be created successfully with rooms.");
    }
}
