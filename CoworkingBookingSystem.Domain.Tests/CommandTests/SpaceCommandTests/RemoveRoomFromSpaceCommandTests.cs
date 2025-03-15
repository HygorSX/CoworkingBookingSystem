using CoworkingBookingSystem.Domain.Commands.SpaceCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public class RemoveRoomFromSpaceCommandTests
{
    [TestMethod]
    public void Should_Return_Error_When_SpaceId_Is_Empty()
    {
        var command = new RemoveRoomFromSpaceCommand(Guid.Empty, Guid.NewGuid());
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when SpaceId is empty.");
    }

    [TestMethod]
    public void Should_Return_Error_When_RoomId_Is_Empty()
    {
        var command = new RemoveRoomFromSpaceCommand(Guid.NewGuid(), Guid.Empty);
        command.Validate();

        Assert.IsFalse(command.IsValid, "The command should be invalid when RoomId is empty.");
    }

    [TestMethod]
    public void Should_Be_Valid_When_All_Fields_Are_Correct()
    {
        var command = new RemoveRoomFromSpaceCommand(Guid.NewGuid(), Guid.NewGuid());
        command.Validate();

        Assert.IsTrue(command.IsValid, "The command should be valid when all fields are correct.");
    }
}