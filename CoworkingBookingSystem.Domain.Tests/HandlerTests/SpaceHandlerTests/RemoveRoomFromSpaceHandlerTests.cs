using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Tests.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.HandlerTests.SpaceHandlerTests;

[TestClass]
public class RemoveRoomFromSpaceHandlerTests
{
    private SpaceHandler _spaceHandler;
    private FakeSpaceRepository _fakeSpaceRepository;

    [TestInitialize]
    public void Setup()
    {
        _fakeSpaceRepository = new FakeSpaceRepository();
        _spaceHandler = new SpaceHandler(_fakeSpaceRepository);
    }
    
    [TestMethod]
    public void Should_Return_Error_When_Command_Is_Invalid()
    {
        var command = new RemoveRoomFromSpaceCommand(Guid.Empty, Guid.Empty);

        var result = (GenericCommandResult)_spaceHandler.Handle(command);

        Assert.IsFalse(result.Success, "The command should fail when the data is invalid.");
    }

    [TestMethod]
    public void Should_Return_Error_When_Space_Not_Found()
    {
        var command = new RemoveRoomFromSpaceCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = (GenericCommandResult)_spaceHandler.Handle(command);

        Assert.IsFalse(result.Success, "The command should fail when the space does not exist.");
    }

    [TestMethod]
    public void Should_Remove_Room_When_Command_Is_Valid()
    {
        var space = new SpaceEntity("Test Space");
        var room = new RoomEntity("Meeting Room", space.Id);
        space.AddRoom(room);
        _fakeSpaceRepository.CreateSpace(space);

        var command = new RemoveRoomFromSpaceCommand(space.Id, room.Id);
        var result = (GenericCommandResult)_spaceHandler.Handle(command);

        Assert.IsTrue(result.Success, "The command should succeed.");
    }
}