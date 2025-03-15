using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IRoomRepository
{
    RoomEntity GetById(Guid id);
}