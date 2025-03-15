using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities.Enum;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.UserHandlerTests;

[TestClass]
public sealed class CreateUserHandlerTests
{
    private CreateUserCommand _invalidCommand;
    private CreateUserCommand _validCommand;
    private UserHandler _userHandler;

    [TestInitialize]
    public void Setup()
    {
        _invalidCommand = new CreateUserCommand("", "", "", EUserType.Common);
        _validCommand = new CreateUserCommand("User", "user@gmail.com.br", "user123", EUserType.Common);
        _userHandler = new UserHandler(new FakeUserRepository());
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_stop_the_application()
    {
        var result = (GenericCommandResult)_userHandler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success, "The user creation should fail for an invalid command.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_create_a_user()
    {
        var result = (GenericCommandResult)_userHandler.Handle(_validCommand);
        Assert.IsTrue(result.Success, $"Expected success, but failed with message: {result.Message}");
    }
}