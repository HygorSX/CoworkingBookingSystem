using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Infra.Contexts;
using CoworkingBookingSystem.Domain.Queries;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBookingSystem.Domain.Infra.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _db;

    public ReservationRepository(AppDbContext db)
    {
        _db = db;
    }

    public ReservationEntity GetReservationForUserById(Guid ReservationId, Guid UserId)
    {
        return _db.Reservations
            .AsNoTracking()
            .FirstOrDefault(r => r.UserId == UserId && r.Id == ReservationId);
    }

    public IEnumerable<ReservationEntity> GetReservationsByUser(Guid UserId)
    {
        return _db.Reservations
            .AsNoTracking()
            .Where(ReservationQueries.GetReservationsByUser(UserId))
            .OrderBy(x => x.StartTime);
    }

    public IEnumerable<ReservationEntity> GetFutureReservationsForRoom(Guid RoomId)
    {
        return _db.Reservations
            .AsNoTracking()
            .Where(ReservationQueries.GetFutureReservationsForRoom(RoomId))
            .OrderBy(x => x.StartTime);
    }

    public IEnumerable<ReservationEntity> GetConflictingReservationsForRoom(Guid RoomId, DateTime StartTime, DateTime EndTime)
    {
        return _db.Reservations
            .AsNoTracking()
            .Where(ReservationQueries.GetConflictingReservationsForRoom(RoomId, StartTime, EndTime))
            .ToList();
    }

    public void CreateReservation(ReservationEntity reservation)
    {
        _db.Reservations.Add(reservation);
        _db.SaveChanges();
    }

    public void UpdateReservation(ReservationEntity reservation)
    {
        _db.Entry(reservation).State = EntityState.Modified;
        _db.SaveChanges();
    }
    
}