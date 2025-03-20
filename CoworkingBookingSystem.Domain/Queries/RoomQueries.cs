using System.Linq.Expressions;
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Queries;

public static class RoomQueries
{
    public static Expression<Func<RoomEntity, bool>> GetRoomById(Guid roomId)
    {
        return r => r.Id == roomId;
    }
}