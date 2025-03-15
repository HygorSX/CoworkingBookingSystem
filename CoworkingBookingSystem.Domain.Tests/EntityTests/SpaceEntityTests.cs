using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Tests.EntityTests;

[TestClass]
public class SpaceEntityTests
{
    private SpaceEntity _space;
    private RoomEntity _room;
    
    [TestInitialize]
    public void Setup()
    {
        _space = new SpaceEntity("Main Space");
        _room = new RoomEntity("Conference Room", _space.Id);
    }
    
    [TestMethod]
    public void Should_Create_Space_Correctly()
    {
        Assert.AreEqual("Main Space", _space.Name);
        Assert.AreEqual(0, _space.Rooms.Count, "New space should have no rooms.");
    }

    [TestMethod]
    public void Should_Add_Room_Successfully()
    {
        _space.AddRoom(_room);

        Assert.AreEqual(1, _space.Rooms.Count, "Room should be added.");
        Assert.IsTrue(_space.Rooms.Contains(_room), "Space should contain the added room.");
    }
    
    [TestMethod]
    public void Should_Throw_Exception_When_Adding_Null_Room()
    {
        var exception = Assert.ThrowsException<ArgumentNullException>(() => _space.AddRoom(null));
        Assert.AreEqual("The room cannot be null", exception.ParamName);
    }

    [TestMethod]
    public void Should_Throw_Exception_When_Adding_Duplicate_Room()
    {
        _space.AddRoom(_room);

        var exception = Assert.ThrowsException<InvalidOperationException>(() => _space.AddRoom(_room));
        Assert.AreEqual("This room is already added to the space.", exception.Message);
    }

    [TestMethod]
    public void Should_Remove_Room_Successfully()
    {
        _space.AddRoom(_room);
        _space.RemoveRoom(_room);

        Assert.AreEqual(0, _space.Rooms.Count, "Room should be removed.");
    }
    
    [TestMethod]
    public void Should_Not_Throw_When_Removing_Nonexistent_Room()
    {
        var nonExistentRoom = new RoomEntity("Unused Room", _space.Id);

        _space.RemoveRoom(nonExistentRoom);

        Assert.AreEqual(0, _space.Rooms.Count, "Room list should remain unchanged.");
    }
    
    [TestMethod]
    public void Should_Throw_Exception_When_Removing_Null_Room()
    {
        var exception = Assert.ThrowsException<ArgumentNullException>(() => _space.RemoveRoom(null));
        Assert.AreEqual("room", exception.ParamName);
    }
}