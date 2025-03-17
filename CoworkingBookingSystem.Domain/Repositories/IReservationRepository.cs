using CoworkingBookingSystem.Domain.Entities;

namespace CoworkingBookingSystem.Domain.Repositories;

public interface IReservationRepository
{
    void CreateReservation(ReservationEntity reservation);
    void UpdateReservation(ReservationEntity reservation);
    bool HasConflict(Guid commandRoomId, DateTime commandStartTime, DateTime commandEndTime);
    ReservationEntity GetReservationForUserById(Guid ReservationId, Guid UserId);
    List<ReservationEntity> GetReservationsByUser(Guid UserId);
    List<ReservationEntity> GetFutureReservationsForRoom(Guid RoomId);
    void MarkReservationAsCompleted(Guid ReservationId);

}