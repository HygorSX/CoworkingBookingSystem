using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities.Enum;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.UserCommandTests;

[TestClass]
public sealed class CreateUserCommandTests
{
    private CreateUserCommand _invalidCommand;
    private CreateUserCommand _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateUserCommand("", "", "", EUserType.Common);
        _validCommand = new CreateUserCommand("User", "user@gmail.com", "user123", EUserType.Common);
    }

    [TestMethod]
    public void Should_return_invalid_when_command_has_empty_fields()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid);
        Assert.IsTrue(_invalidCommand.Notifications.Count > 0);
    }

    [TestMethod]
    public void Should_return_valid_when_command_has_correct_values()
    {
        _validCommand.Validate();
        Assert.IsTrue(_validCommand.IsValid);
        Assert.AreEqual(0, _validCommand.Notifications.Count);
    }
}
