using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Entities.Enum;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.UserHandlerTests;

[TestClass]
public sealed class DeleteUserHandlerTests
{
    private DeleteUserCommand _invalidCommand;
    private DeleteUserCommand _validCommand;
    private UserHandler _userHandler;
    private FakeUserRepository _fakeUserRepository;
    private UserEntity _userToDelete;

    [TestInitialize]
    public void Setup()
    {
        _fakeUserRepository = new FakeUserRepository();
        _userHandler = new UserHandler(_fakeUserRepository);
        
        _userToDelete = new UserEntity("userTest", "user@gmail.com", "user12345", EUserType.Common);
        _fakeUserRepository.Create(_userToDelete);
        
        _invalidCommand = new DeleteUserCommand(Guid.Empty);
        _validCommand = new DeleteUserCommand(_userToDelete.Id);
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_stop_the_application()
    {
        var result = (GenericCommandResult)_userHandler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success, "User removal should fail due to an invalid command.");
    }

    [TestMethod]
    public void Given_a_null_user_the_application_stops()
    {
        var result = (GenericCommandResult)_userHandler.Handle(new DeleteUserCommand(Guid.NewGuid()));
        Assert.IsFalse(result.Success, "User removal should fail because the user does not exist.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_delete_a_user()
    {
        var result = (GenericCommandResult)_userHandler.Handle(_validCommand);
        Assert.IsTrue(result.Success, $"Expected success, but failed with message: {result.Message}");
    }
}