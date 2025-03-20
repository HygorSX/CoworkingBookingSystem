using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Repositories;

namespace CoworkingBookingSystem.Domain.Tests.Repositories;

public class FakeReservationRepository : IReservationRepository
{
    private readonly List<ReservationEntity> _reservations = new List<ReservationEntity>();
    public void CreateReservation(ReservationEntity reservation)
    {
        _reservations.Add(reservation);
    }

    public void UpdateReservation(ReservationEntity reservation)
    {
        throw new NotImplementedException();
    }

    public ReservationEntity GetReservationForUserById(Guid reservationId, Guid userId)
    {
        return _reservations.FirstOrDefault(r => r.Id == reservationId && r.UserId == userId);
    }

    public IEnumerable<ReservationEntity> GetReservationsByUser(Guid UserId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ReservationEntity> GetFutureReservationsForRoom(Guid RoomId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ReservationEntity> GetConflictingReservationsForRoom(Guid RoomId, DateTime StartTime, DateTime EndTime)
    {
        throw new NotImplementedException();
    }
}