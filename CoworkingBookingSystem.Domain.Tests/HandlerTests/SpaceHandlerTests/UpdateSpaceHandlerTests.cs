using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.SpaceHandlerTests;

[TestClass]
public class UpdateSpaceCommandHandlerTests
{
    private FakeSpaceRepository _spaceRepository;
    private SpaceHandler _spaceHandler;
    private SpaceEntity _space;
    private UpdateSpaceCommand _invalidCommand;
    private UpdateSpaceCommand _validCommand;

    [TestInitialize]
    public void Setup()
    {
        _spaceRepository = new FakeSpaceRepository();
        _spaceHandler = new SpaceHandler(_spaceRepository);
        
        _space = new SpaceEntity("Old Space Name");
        _spaceRepository.CreateSpace(_space);
        
        _invalidCommand = new UpdateSpaceCommand("", Guid.Empty);
        _validCommand = new UpdateSpaceCommand("New Space Name", _space.Id);
    }

    [TestMethod]
    public void Should_Return_Error_When_Command_Is_Invalid()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_invalidCommand);

        Assert.IsFalse(result.Success, "Should fail due to an invalid command.");
    }

    [TestMethod]
    public void Should_Return_Error_When_Space_Not_Found()
    {
        var command = new UpdateSpaceCommand("New Space Name", Guid.NewGuid());
        var result = (GenericCommandResult)_spaceHandler.Handle(command);
        
        Assert.IsFalse(result.Success, "Should fail when not finding a space");
    }

    [TestMethod]
    public void Should_Update_Space_Successfully()
    {
        var result = (GenericCommandResult)_spaceHandler.Handle(_validCommand);

        Assert.IsTrue(result.Success, "Should update when giving a valid command");
    }
}