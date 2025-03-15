using CoworkingBookingSystem.Domain.Commands.SpaceCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public class AddRoomToSpaceCommandTests
{
    [TestMethod]
    public void Should_Return_Error_When_SpaceId_Is_Empty()
    {
        var command = new AddRoomToSpaceCommand(Guid.Empty, "Meeting Room");
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when SpaceId is empty.");
    }

    [TestMethod]
    public void Should_Return_Error_When_RoomName_Is_Empty()
    {
        var command = new AddRoomToSpaceCommand(Guid.NewGuid(), "");
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when the room name is empty.");
    }

    [TestMethod]
    public void Should_Be_Valid_When_All_Fields_Are_Correct()
    {
        var command = new AddRoomToSpaceCommand(Guid.NewGuid(), "Conference Room");
        command.Validate();

        Assert.IsTrue(command.IsValid, "The command should be valid when all fields are correct");
    }
}