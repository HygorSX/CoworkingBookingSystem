using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public sealed class CreateSpaceCommandTests
{
    private CreateSpaceCommand _invalidCommand;
    private CreateSpaceCommand _validCommandWithoutRooms;
    private CreateSpaceCommand _validCommandWithRooms;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateSpaceCommand("", new List<string>());
        _validCommandWithoutRooms = new CreateSpaceCommand("Space One", new List<string>());
        _validCommandWithRooms = new CreateSpaceCommand("Space Two", new List<string> { "Room A", "Room B" });
    }

    [TestMethod]
    public void Should_return_invalid_when_command_has_empty_fields()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid, "Command should be invalid when space name is empty.");
    }

    [TestMethod]
    public void Should_return_valid_when_command_has_correct_values_without_rooms()
    {
        _validCommandWithoutRooms.Validate();
        Assert.IsTrue(_validCommandWithoutRooms.IsValid, "Command should be valid with correct values, even without rooms.");
    }

    [TestMethod]
    public void Should_return_valid_when_command_has_correct_values_with_rooms()
    {
        _validCommandWithRooms.Validate();
        Assert.IsTrue(_validCommandWithRooms.IsValid, "Command should be valid when space name and rooms are provided.");
    }
}