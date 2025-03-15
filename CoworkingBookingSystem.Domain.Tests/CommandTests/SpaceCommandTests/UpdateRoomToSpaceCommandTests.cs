using CoworkingBookingSystem.Domain.Commands.SpaceCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public class UpdateRoomToSpaceCommandTests
{
    [TestMethod]
    public void Should_Return_Error_When_SpaceId_Is_Empty()
    {
        var command = new UpdateRoomToSpaceCommand(Guid.Empty, Guid.NewGuid(), "Updated Room");
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when SpaceId is empty.");
    }

    [TestMethod]
    public void Should_Return_Error_When_RoomId_Is_Empty()
    {
        var command = new UpdateRoomToSpaceCommand(Guid.NewGuid(), Guid.Empty, "Updated Room");
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when RoomId is empty.");
    }

    [TestMethod]
    public void Should_Return_Error_When_NewName_Is_Empty()
    {
        var command = new UpdateRoomToSpaceCommand(Guid.NewGuid(), Guid.NewGuid(), "");
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when NewName is empty.");
    }

    [TestMethod]
    public void Should_Be_Valid_When_All_Fields_Are_Correct()
    {
        var command = new UpdateRoomToSpaceCommand(Guid.NewGuid(), Guid.NewGuid(), "Updated Room Name");
        command.Validate();

        Assert.IsTrue(command.IsValid, "The command should be valid when all fields are correct.");
    }
}