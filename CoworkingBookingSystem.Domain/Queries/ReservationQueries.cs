using System.Linq.Expressions;
using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Queries;

public static class ReservationQueries
{
    public static Expression<Func<ReservationEntity, bool>> GetReservationForUserById(Guid reservationId, Guid userId)
    {
        return r => r.Id == reservationId && r.UserId == userId;
    }

    public static Expression<Func<ReservationEntity, bool>> GetReservationsByUser(Guid userId)
    {
        return r => r.UserId == userId;
    }

    public static Expression<Func<ReservationEntity, bool>> GetFutureReservationsForRoom(Guid roomId)
    {
        return r => r.RoomId == roomId && r.StartTime > DateTime.UtcNow;
    }

    public static Expression<Func<ReservationEntity, bool>> GetConflictingReservationsForRoom(Guid roomId, DateTime startTime, DateTime endTime)
    {
        return r => r.RoomId == roomId && r.StartTime < endTime && r.EndTime > startTime;
    }
}