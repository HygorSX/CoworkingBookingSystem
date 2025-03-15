using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.ApplicationInsights;

namespace CoworkingBookingSystem.Domain.Tests.Repositories;

public class FakeRoomRepository : IRoomRepository
{
    private readonly List<RoomEntity> _rooms = new List<RoomEntity>();
    public RoomEntity GetById(Guid id)
    {
        return _rooms.FirstOrDefault(room => room.Id == id);
    }
    public void CreateRoom(RoomEntity room)
    {
        _rooms.Add(room);
    }
}