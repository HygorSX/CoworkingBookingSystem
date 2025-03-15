using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Commands.UserCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.SpaceHandlerTests;

[TestClass]
public sealed class DeleteUserHandlerTests
{
    private DeleteSpaceCommand _invalidCommand;
    private DeleteSpaceCommand _validCommand;
    private SpaceHandler _spaceHandler;
    private FakeSpaceRepository _fakeSpaceRepository;
    private SpaceEntity _spaceToDelete;

    [TestInitialize]
    public void Setup()
    {
        _fakeSpaceRepository = new FakeSpaceRepository();
        _spaceHandler = new SpaceHandler(_fakeSpaceRepository);
        
        _spaceToDelete = new SpaceEntity("Space One");
        _fakeSpaceRepository.CreateSpace(_spaceToDelete);
        
        _invalidCommand = new DeleteSpaceCommand(Guid.Empty);
        _validCommand = new DeleteSpaceCommand(_spaceToDelete.Id);
    }

    [TestMethod]
    public void Given_an_invalid_command_it_should_stop_the_application()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success, "Space removal should fail due to an invalid command.");
    }

    [TestMethod]
    public void Given_a_null_space_the_application_stops()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(new DeleteSpaceCommand(Guid.NewGuid()));
        Assert.IsFalse(result.Success, "Space removal should fail because the user does not exist.");
    }

    [TestMethod]
    public void Given_a_valid_command_it_should_delete_a_space()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_validCommand);
        Assert.IsTrue(result.Success, $"Expected success, but failed with message: {result.Message}");
    }
}