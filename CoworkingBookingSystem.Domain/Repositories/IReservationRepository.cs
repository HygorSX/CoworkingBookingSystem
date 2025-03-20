using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IReservationRepository
{
    void CreateReservation(ReservationEntity reservation);
    void UpdateReservation(ReservationEntity reservation);
    ReservationEntity GetReservationForUserById(Guid ReservationId, Guid UserId);
    IEnumerable<ReservationEntity> GetReservationsByUser(Guid UserId);
    IEnumerable<ReservationEntity> GetFutureReservationsForRoom(Guid RoomId);
    IEnumerable<ReservationEntity> GetConflictingReservationsForRoom(Guid RoomId, DateTime StartTime, DateTime EndTime);

}