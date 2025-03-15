using CoworkingBookingSystem.Domain.Commands.SpaceCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.SpaceCommandTests;

[TestClass]
public class UpdateSpaceCommandTests
{
    [TestMethod]
    public void Should_Be_Valid_When_All_Properties_Are_Correct()
    {
        var command = new UpdateSpaceCommand("New Space", Guid.NewGuid());
        command.Validate();

        Assert.IsTrue(command.IsValid, "Command should be valid with correct data.");
    }

    [TestMethod]
    public void Should_Be_Invalid_When_Name_Is_Empty()
    {
        var command = new UpdateSpaceCommand("", Guid.NewGuid());
        command.Validate();

        Assert.IsFalse(command.IsValid);
    }

    [TestMethod]
    public void Should_Be_Invalid_When_Id_Is_Empty()
    {
        var command = new UpdateSpaceCommand("Valid Name", Guid.Empty);
        command.Validate();

        Assert.IsFalse(command.IsValid);
    }
}