﻿using CoworkingBookingSystem.Domain.Entities;
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

    public bool HasConflict(Guid commandRoomId, DateTime commandStartTime, DateTime commandEndTime)
    {
        return _reservations.Any(reservation =>
            reservation.RoomId == commandRoomId &&
            reservation.StartTime < commandEndTime && 
            reservation.EndTime > commandStartTime);
    }

    public ReservationEntity GetReservationForUserById(Guid reservationId, Guid userId)
    {
        return _reservations.FirstOrDefault(r => r.Id == reservationId && r.UserId == userId);
    }
}