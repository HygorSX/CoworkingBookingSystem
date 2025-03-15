using CoworkingBookingSystem.Domain.Commands.UserCommands;

namespace CoworkingBookingSystem.Domain.Tests.CommandTests.UserCommandTests;

[TestClass]
public sealed class DeleteUserCommandTests
{
    private DeleteUserCommand _invalidCommand;
    private DeleteUserCommand _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new DeleteUserCommand(Guid.Empty);
        _validCommand = new DeleteUserCommand(Guid.NewGuid());
    }

    [TestMethod]
    public void Should_return_invalid_when_user_id_is_empty()
    {
        _invalidCommand.Validate();
        Assert.IsFalse(_invalidCommand.IsValid);
    }

    [TestMethod]
    public void Should_return_valid_when_user_id_is_valid()
    {
        _validCommand.Validate();
        Assert.IsTrue(_validCommand.IsValid);
    }
}